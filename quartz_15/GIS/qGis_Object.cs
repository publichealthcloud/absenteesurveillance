using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System.Text;

using Quartz.Data;

namespace Quartz.GIS
{
    public class qGis_Object
    {
        protected static qGis_Object schema = new qGis_Object();
        protected static DbRow schema2 = new DbRow((new qGis_Object()).container);

        protected DbRow container;
        protected readonly DbColumn<int> gis_object_id;
        protected readonly DbColumn<string> spatial_type;
        protected readonly DbColumn<int> data_group_id;
        protected readonly DbColumn<int> content_type_id;
        protected readonly DbColumn<int> reference_id;
        protected readonly DbColumn<double> longitude;
        protected readonly DbColumn<double> latitude;
        protected readonly DbColumn<string> title;
        protected readonly DbColumn<string> description;
        protected readonly DbColumn<string> image_url;

        public int GISObjectID { get { return gis_object_id.Value; } set { gis_object_id.Value = value; } }
        public string SpatialType { get { return spatial_type.Value; } set { spatial_type.Value = value; } }
        public int DataGroupID { get { return data_group_id.Value; } set { data_group_id.Value = value; } }
        public int ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public int ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public double Longitude { get { return longitude.Value; } set { longitude.Value = value; } }
        public double Latitude { get { return latitude.Value; } set { latitude.Value = value; } }
        public string Title { get { return title.Value; } set { title.Value = value; } }
        public string Description { get { return description.Value; } set { description.Value = value; } }
        public string ImageUrl { get { return image_url.Value; } set { image_url.Value = value; } }
        
        public qGis_Object ()
            : this(new DbRow())
        {
        }

        protected qGis_Object(DbRow c)
        {
            container = c;
            container.SetContainerName("qGIS_Objects");
            gis_object_id = container.NewColumn <int>("GISObjectID", true);
            spatial_type = container.NewColumn<string>("SpatialType");
            data_group_id = container.NewColumn<int>("DataGroupID");
            content_type_id = container.NewColumn<int>("ContentTypeID");
            reference_id = container.NewColumn<int>("ReferenceID");
            longitude = container.NewColumn <double>("Longitude");
            latitude = container.NewColumn <double>("Latitude");
            title = container.NewColumn<string>("Title");
            description = container.NewColumn<string>("Description");
            image_url = container.NewColumn<string>("ImageURL");
        }

        public qGis_Object(int gis_object_id)
            : this ()
        {
            container.Select("GISObjectID = @GISObjectID", new SqlQueryParameter("@GISObjectID", gis_object_id));
        }

        public void Update()
        {
            container.Update("GISObjectID = @GISObjectID");
        }

        public void Insert()
        {
            GISObjectID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qGis_Object> GetAvailableGISObjects()
        {
            return schema.container.Select(
                new DbQuery
                {
                    Where = "GISObjectID > 0",
                    OrderBy = "SpatialType ASC",
                },
                c => new qGis_Object(c));
        }

        public static ICollection<qGis_Object> GetAvailableGISObjectsByContentType(int content_type_id)
        {
            return schema.container.Select(
                new DbQuery
                {
                    Where = "ContentTypeID = @ContentTypeID",
                    OrderBy = "Title ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@ContentTypeID", content_type_id) }
                },
                c => new qGis_Object(c));
        }
        
        public static qGis_Object[] GetObjects()
        {
            DbRow[] objects = DbRow.Select(schema2, string.Concat("SELECT * FROM qGIS_Objects"), null);

            return DbRow.CreateArray<qGis_Object>(objects, current_container => new qGis_Object (current_container));
        }

        public static qGis_Object[] GetObjects(int search_id)
        {
            qDbs_Search search = new qDbs_Search(search_id);

            qDbs_DataGroupConfig data_group_config = new qDbs_DataGroupConfig (search.DataGroupID);

            DbRow[] results = search.GetResults(schema2, string.Format("INNER JOIN qGIS_Objects ON qGIS_Objects.ReferenceID = {0}", data_group_config.KeyColumn), string.Format ("qGIS_Objects.DataGroupID = {0}", search.DataGroupID), null);

            return DbRow.CreateArray<qGis_Object>(results, c => new qGis_Object(c));
        }

        public static qGis_Object[] GetObjects(qGis_Rectangle boundary)
        {
            SqlQueryParameter sql_left = new SqlQueryParameter("@Left", boundary.NorthWest.Longitude);
            SqlQueryParameter sql_right = new SqlQueryParameter("@Right", boundary.NorthEast.Longitude);
            SqlQueryParameter sql_top = new SqlQueryParameter("@Top", boundary.NorthEast.Latitude);
            SqlQueryParameter sql_bottom = new SqlQueryParameter("@Bottom", boundary.SouthWest.Latitude);

            DbRow[] objects = DbRow.Select(schema2, "SELECT * FROM qGIS_Objects WHERE (Longitude BETWEEN @Left AND @Right) AND (Latitude BETWEEN @Bottom AND @Top)", new SqlQueryParameter [] { sql_left, sql_right, sql_top, sql_bottom });

            return DbRow.CreateArray<qGis_Object>(objects, c => new qGis_Object(c));
        }

        public static qGis_Object[] GetObjects(qGis_Rectangle boundary, int search_id)
        {
            SqlQueryParameter sql_left = new SqlQueryParameter("@Left", boundary.NorthWest.Longitude);
            SqlQueryParameter sql_right = new SqlQueryParameter("@Right", boundary.NorthEast.Longitude);
            SqlQueryParameter sql_top = new SqlQueryParameter("@Top", boundary.NorthEast.Latitude);
            SqlQueryParameter sql_bottom = new SqlQueryParameter("@Bottom", boundary.SouthWest.Latitude);
            
            qDbs_Search search = new qDbs_Search(search_id);

            qDbs_DataGroupConfig data_group_config = new qDbs_DataGroupConfig(search.DataGroupID);

            DbRow[] results = search.GetResults(schema2, string.Format("INNER JOIN qGIS_Objects ON qGIS_Objects.ReferenceID = {0}", data_group_config.KeyColumn), string.Format("qGIS_Objects.DataGroupID = {0} AND (Longitude BETWEEN @Left AND @Right) AND (Latitude BETWEEN @Bottom AND @Top)", search.DataGroupID), sql_left, sql_right, sql_top, sql_bottom);

            return DbRow.CreateArray<qGis_Object>(results, c => new qGis_Object(c));
        }

        public static qGis_Object GetGISObjectByDataGroupAndReference(int data_group_id, int reference_id)
        {
            return schema.container.SelectSingle<qGis_Object>(
                new DbQuery
                {
                    Where = "DataGroupID = @DataGroupID AND ReferenceID = @ReferenceID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@DataGroupID", data_group_id), new SqlQueryParameter ("@ReferenceID", reference_id)
                    },
                    OrderBy = "GISObjectID DESC"
                }, c => new qGis_Object(c));
        }

        public static qGis_Object GetGISObjectByContentTypeAndReference(int content_type_id, int reference_id)
        {
            return schema.container.SelectSingle<qGis_Object>(
                new DbQuery
                {
                    Where = "ContentTypeID = @ContentTypeID AND ReferenceID = @ReferenceID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@ContentTypeID", content_type_id), new SqlQueryParameter ("@ReferenceID", reference_id)
                    },
                    OrderBy = "GISObjectID DESC"
                }, c => new qGis_Object(c));
        }
    }
}
