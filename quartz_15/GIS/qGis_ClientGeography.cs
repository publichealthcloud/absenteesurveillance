using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Text;

namespace Quartz.GIS
{
    public class qGis_ClientGeography
    {
        private static DbRow schema = (new qGis_ClientGeography()).container;

        private DbRow container;

        private readonly DbColumn<Int32> geography_id;
        private readonly DbColumn<Int32> rule_id;
        private readonly DbColumn<String> reference_table;
        private readonly DbColumn<String> reference_filter;
        private readonly DbColumn<String> reference_columns;

        public Int32 GeographyID { get { return geography_id.Value; } set { geography_id.Value = value; } }
        public Int32 RuleID { get { return rule_id.Value; } set { rule_id.Value = value; } }
        public String ReferenceTable { get { return reference_table.Value; } set { reference_table.Value = value; } }
        public String ReferenceFilter { get { return reference_filter.Value; } set { reference_filter.Value = value; } }
        public String ReferenceColumns { get { return reference_columns.Value; } set { reference_columns.Value = value; } }

        public qGis_ClientGeography()
            : this(new DbRow())
        {
        }

        public qGis_ClientGeography(DbRow existing_container)
        {
            container = existing_container;

            container.SetContainerName("qGis_ClientGeography");

            geography_id = container.NewColumn<Int32>("GeographyID", true);
            rule_id = container.NewColumn<Int32>("RuleID");
            reference_table = container.NewColumn<String>("ReferenceTable");
            reference_filter = container.NewColumn<String>("ReferenceFilter");
            reference_columns = container.NewColumn<String>("ReferenceColumns");
        }

        public qGis_ClientGeography(Int32 geography_id)
            : base()
        {
            container.Select("GeographyID = @GeographyID", new SqlQueryParameter("@GeographyID", geography_id));
        }

        public static qGis_ClientGeography[] GetObjects(int rule_id)
        {
            DbRow[] results = DbRow.Select(schema, "SELECT * FROM qGis_ClientGeography WHERE RuleID = @RuleID", new SqlQueryParameter("@RuleID", rule_id));

            return DbRow.CreateArray(results, c => new qGis_ClientGeography(c));
        }
    }
}
