using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Text;

namespace Quartz.GIS
{
    public class qGis_ClientRule
    {
        private static DbRow schema = (new qGis_ClientRule()).container;

        private DbRow container;

        private readonly DbColumn<Int32> rule_id;
        private readonly DbColumn<Int32> zoom;
        private readonly DbColumn<Int32> data_group_id;
        private readonly DbColumn<Int32> cluster_minimum_size;
        private readonly DbColumn<Int32> cluster_maximum_size;
        private readonly DbColumn<Int32> grid_horizontal_divisions;
        private readonly DbColumn<Int32> grid_vertical_divisions;

        public Int32 RuleID { get { return rule_id.Value; } set { rule_id.Value = value; } }
        public Int32 Zoom { get { return zoom.Value; } set { zoom.Value = value; } }
        public Int32 DataGroupID { get { return data_group_id.Value; } set { data_group_id.Value = value; } }
        public Int32 ClusterMinimumSize { get { return cluster_minimum_size.Value; } set { cluster_minimum_size.Value = value; } }
        public Int32 ClusterMaximumSize { get { return cluster_maximum_size.Value; } set { cluster_maximum_size.Value = value; } }
        public Int32 GridHorizontalDivisions { get { return grid_horizontal_divisions.Value; } set { grid_horizontal_divisions.Value = value; } }
        public Int32 GridVerticalDivisions { get { return grid_vertical_divisions.Value; } set { grid_vertical_divisions.Value = value; } }

        public qGis_ClientRule()
            : this(new DbRow())
        {
        }

        public qGis_ClientRule(DbRow existing_container)
        {
            container = existing_container;
            
            container.SetContainerName ("qGis_ClientRules");

            rule_id = container.NewColumn<Int32>("RuleID", true);
            zoom = container.NewColumn<Int32>("Zoom");
            data_group_id = container.NewColumn<Int32>("DataGroupID");
            cluster_minimum_size = container.NewColumn<Int32>("ClusterMinimumSize");
            cluster_maximum_size = container.NewColumn<Int32>("ClusterMaximumSize");
            grid_horizontal_divisions = container.NewColumn<Int32>("GridHorizontalDivisions");
            grid_vertical_divisions = container.NewColumn<Int32>("GridVerticalDivisions");
        }

        public qGis_ClientRule(Int32 rule_id)
            : this ()
        {
            container.Select("RuleID = @RuleID", new SqlQueryParameter("@RuleID", rule_id));
        }

        public static qGis_ClientRule [] GetClientRules (int data_group_id)
        {
            return schema.Select <qGis_ClientRule> (string.Format("DataGroupID = {0} ORDER BY Zoom", data_group_id), c => new qGis_ClientRule(c));
        }
    }
}
