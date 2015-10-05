using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using Microsoft.SqlServer.Types;

namespace Quartz.GIS
{
    public class qGis_Point
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public qGis_Point()
        {
        }

        public qGis_Point(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public string AsText()
        {
            return string.Format("{0} {1}", Longitude, Latitude);
        }
    }

    public class qGis_Rectangle
    {
        public qGis_Point NorthWest { get; set; }
        public qGis_Point NorthEast { get; set; }
        public qGis_Point SouthWest { get; set; }
        public qGis_Point SouthEast { get; set; }

        public double Width { get { return NorthEast.Longitude - NorthWest.Longitude; } }
        public double Height { get { return NorthEast.Latitude - SouthEast.Latitude; } }

        public qGis_Rectangle()
        {
        }

        public qGis_Rectangle(double nw_long, double nw_lat, double se_long, double se_lat)
        {
            NorthWest = new qGis_Point(nw_long, nw_lat);
            SouthEast = new qGis_Point(se_long, se_lat);
            SouthWest = new qGis_Point(nw_long, se_lat);
            NorthEast = new qGis_Point(se_long, nw_lat);
        }

        public override string ToString()
        {
            return string.Format("geography::Parse('POLYGON(({0}, {1}, {2}, {3}, {0}))')", NorthWest.AsText (), SouthWest.AsText (), SouthEast.AsText (), NorthEast.AsText ()); 
        }
    }

    public class qGis_ReferenceObject
    {
        private readonly DbRow container;

        private static readonly DbRow schema = (new qGis_ReferenceObject ()).container;

        private readonly DbColumn<int> reference_object_id;
        private readonly DbColumn<SqlGeography> geography;

        public int ReferenceObjectID { get { return reference_object_id.Value; } set { reference_object_id.Value = value; } }
        public qGis_Point[] Points { get; set; }

        public qGis_ReferenceObject ()
            : this(new DbRow())
        {
        }

        private qGis_ReferenceObject (DbRow existing_container)
        {
            container = existing_container;

            container.SetConnectionName("GisReferenceObjects");
            container.SetContainerName("qGis_ReferenceObjects");

            reference_object_id = container.NewColumn<int>("ID", true);
            geography = container.NewColumn<SqlGeography>("geom");
        }

        public static qGis_ReferenceObject[] GetReferenceObjects()
        {
            DbRow[] objects = DbRow.Select(qGis_ReferenceObject.schema, "SELECT TOP 100 * FROM qGis_ReferenceObjects", null);

            qGis_ReferenceObject[] reference_objects = DbRow.CreateArray<qGis_ReferenceObject>(objects, current_container => new qGis_ReferenceObject(current_container));

            if (reference_objects != null)
                foreach (qGis_ReferenceObject reference_object in reference_objects)
                    reference_object.LoadPointsFromGeographyObject();

            return reference_objects;
        }

        public static qGis_ReferenceObject[] GetReferenceObjects(int rule_id, qGis_Rectangle boundary)
        {
            string boundary_as_text = boundary.ToString ();

            qGis_ClientGeography [] geo_objects = qGis_ClientGeography.GetObjects(rule_id);

            List<qGis_ReferenceObject> reference_objects_list = new List<qGis_ReferenceObject>();

            if (geo_objects != null)
            {
                foreach (var geo_object in geo_objects)
                {
                    string sql = string.Format("SELECT {0} FROM qGis_ReferenceObjects WHERE geom.Filter ({1}) = 1 {2}",
                        string.IsNullOrEmpty(geo_object.ReferenceColumns) ? "*" : geo_object.ReferenceColumns,
                        boundary_as_text,
                        string.IsNullOrEmpty(geo_object.ReferenceFilter) ? string.Empty : string.Format("AND {0}", geo_object.ReferenceFilter));

                    DbRow[] objects = DbRow.Select(qGis_ReferenceObject.schema, sql, null);

                    qGis_ReferenceObject[] reference_objects = DbRow.CreateArray<qGis_ReferenceObject>(objects, c => new qGis_ReferenceObject(c));

                    if (reference_objects != null)
                    {
                        foreach (qGis_ReferenceObject reference_object in reference_objects)
                        {
                            reference_object.LoadPointsFromGeographyObject();
                            reference_objects_list.Add(reference_object);
                        }
                    }
                }
            }

            return reference_objects_list.ToArray ();
        }

        private void LoadPointsFromGeographyObject()
        {
            if (geography.Value == null) return;

            int points_count = geography.Value.STNumPoints().Value;

            Points = new qGis_Point[points_count];

            for (int i = 1; i <= points_count; i++)
            {
                SqlGeography sql_point = geography.Value.STPointN(i);

                qGis_Point point = new qGis_Point (sql_point.Long.Value, sql_point.Lat.Value);

                Points[i - 1] = point;
            }
        }
    }

    public class qGis_Cluster
    {
        private List<qGis_Object> object_list;

        public qGis_Point Location { get; set; }
        public qGis_Object[] Objects { get { return object_list.ToArray(); } set { } }

        public qGis_Cluster()
        {
            object_list = new List<qGis_Object>(); 
        }

        public qGis_Cluster(double longitude, double latitude)
            : this ()
        {
            Location = new qGis_Point(longitude, latitude);
        }

        public void AddObject (qGis_Object gis_object)
        {
            object_list.Add(gis_object);
        }
    }


    public class qGis_ClusterGrid
    {
        public qGis_Rectangle Boundary { get; set; }
        public int RuleID { get; set; }
        public int SearchID { get; set; }

        public qGis_Cluster[] Clusters { get; set; }

        public qGis_ClusterGrid()
        {
        }

        public qGis_ClusterGrid(qGis_Rectangle boundary, int rule_id)
            : this ()
        {
            Boundary = boundary;
            RuleID = rule_id;
        }

        public qGis_ClusterGrid(qGis_Rectangle boundary, int rule_id, int search_id)
            : this (boundary, rule_id)
        {
            SearchID = search_id;
        }

        public void Build ()
        {
            qGis_Object[] objects = null;

            if (SearchID > 0) objects = qGis_Object.GetObjects (Boundary, SearchID);
            else objects = qGis_Object.GetObjects(Boundary);

            if (objects == null) return;

            qGis_ClientRule rule = new qGis_ClientRule(RuleID);

            double long_step = Boundary.Width / rule.GridHorizontalDivisions;
            double lat_step = Boundary.Height / rule.GridVerticalDivisions;

            qGis_Cluster [] temp_clusters = new qGis_Cluster [rule.GridHorizontalDivisions * rule.GridVerticalDivisions];

            foreach (qGis_Object gis_object in objects)
            {
                for (int row = 0; row < rule.GridVerticalDivisions; row++)
                {
                    for (int col = 0; col < rule.GridHorizontalDivisions; col++)
                    {
                        double left = Boundary.NorthWest.Longitude + col * long_step;
                        double right = left + long_step;
                        double top = Boundary.NorthWest.Latitude - row * lat_step;
                        double bottom = top - lat_step;

                        if (gis_object.Latitude > top || gis_object.Latitude < bottom) break;
                        if (gis_object.Longitude > right || gis_object.Longitude < left) continue;

                        if (temp_clusters [row * col] == null) temp_clusters[row * col] = new qGis_Cluster(gis_object.Longitude, gis_object.Latitude);

                        temp_clusters[row * col].AddObject (gis_object);
                    }
                }
            }

            List<qGis_Cluster> clusters = new List<qGis_Cluster>();

            for (int i = temp_clusters.Length - 1; i >= 0; i--)
                if (temp_clusters[i] != null)
                    clusters.Add(temp_clusters[i]);

            Clusters = clusters.ToArray();
        }
    }
}
