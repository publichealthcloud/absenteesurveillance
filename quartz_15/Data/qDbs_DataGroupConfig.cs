using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;

namespace Quartz.Data
{
    public class qDbs_DataGroupConfig
    {
        private DbRow container;

        public readonly static qDbs_DataGroupConfig Schema = new qDbs_DataGroupConfig();

        private readonly DbColumn<Int32> data_group_id;
        private readonly DbColumn<String> name;
        private readonly DbColumn<String> search_results_url;
        private readonly DbColumn<String> select_source;
        private readonly DbColumn<String> key_column;
        private readonly DbColumn<String> identifier_name;
        private readonly DbColumn<String> identifier_value;
        private readonly DbColumn<String> table_name;
        private readonly DbColumn<String> manage_behavior;
        private readonly DbColumn<String> manage_parameter;
        private readonly DbColumn<Int32> default_form;
        private readonly DbColumn<String> secondary_key;
        private readonly DbColumn<String> secondary_manage_behavior;
        private readonly DbColumn<String> secondary_manage_behavior_name;
        private readonly DbColumn<Int32> secondary_manage_behavior_data_group_id;
        private readonly DbColumn<String> supports_email;
        private readonly DbColumn<String> supports_message;
        private readonly DbColumn<String> supports_spatial;

        public Int32 DataGroupID { get { return data_group_id.Value; } set { data_group_id.Value = value; } }
        public String Name { get { return name.Value; } set { name.Value = value; } }
        public String SearchResultsURL { get { return search_results_url.Value; } set { search_results_url.Value = value; } }
        public String SelectSource { get { return select_source.Value; } set { select_source.Value = value; } }
        public String KeyColumn { get { return key_column.Value; } set { key_column.Value = value; } }
        public String IdentifierName { get { return identifier_name.Value; } set { identifier_name.Value = value; } }
        public String IdentifierValue { get { return identifier_value.Value; } set { identifier_value.Value = value; } }
        public String TableName { get { return table_name.Value; } set { table_name.Value = value; } }
        public String ManageBehavior { get { return manage_behavior.Value; } set { manage_behavior.Value = value; } }
        public String ManageParameter { get { return manage_parameter.Value; } set { manage_parameter.Value = value; } }
        public Int32 DefaultForm { get { return default_form.Value; } set { default_form.Value = value; } }
        public String SecondaryKey { get { return secondary_key.Value; } set { secondary_key.Value = value; } }
        public String SecondaryManageBehavior { get { return secondary_manage_behavior.Value; } set { secondary_manage_behavior.Value = value; } }
        public String SecondaryManageBehaviorName { get { return secondary_manage_behavior_name.Value; } set { secondary_manage_behavior_name.Value = value; } }
        public Int32 SecondaryManageBehaviorDataGroupID { get { return secondary_manage_behavior_data_group_id.Value; } set { secondary_manage_behavior_data_group_id.Value = value; } }
        public String SupportsEmail { get { return supports_email.Value; } set { supports_email.Value = value; } }
        public String SupportsMessage { get { return supports_message.Value; } set { supports_message.Value = value; } }
        public String SupportsSpatial { get { return supports_spatial.Value; } set { supports_spatial.Value = value; } }

        public qDbs_DataGroupConfig()
            : this(new DbRow())
        {
        }

        public qDbs_DataGroupConfig(DbRow existing_container)
        {
            container = existing_container;
            
            container.SetContainerName ("qDbs_DataGroupConfig");

            data_group_id = container.NewColumn<Int32>("DataGroupID", true);
            name = container.NewColumn<String>("Name");
            search_results_url = container.NewColumn<String>("SearchResultsURL");
            select_source = container.NewColumn<String>("SelectSource");
            key_column = container.NewColumn<String>("KeyColumn");
            identifier_name = container.NewColumn<String>("IdentifierName");
            identifier_value = container.NewColumn<String>("IdentifierValue");
            table_name = container.NewColumn<String>("TableName");
            manage_behavior = container.NewColumn<String>("ManageBehavior");
            manage_parameter = container.NewColumn<String>("ManageParameter");
            default_form = container.NewColumn<Int32>("DefaultForm");
            secondary_key = container.NewColumn<String>("SecondaryKey");
            secondary_manage_behavior = container.NewColumn<String>("SecondaryManageBehavior");
            secondary_manage_behavior_name = container.NewColumn<String>("SecondaryManageBehaviorName");
            secondary_manage_behavior_data_group_id = container.NewColumn<Int32>("SecondaryManageBehaviorDataGroupID");
            supports_email = container.NewColumn<String>("SupportsEmail");
            supports_message = container.NewColumn<String>("SupportsMessage");
            supports_spatial = container.NewColumn<String>("SupportsSpatial");
        }

        public qDbs_DataGroupConfig(Int32 data_group_id)
            : this ()
        {
            container.Select("DataGroupID = @DataGroupID", new SqlQueryParameter("@DataGroupID", data_group_id));

            if (DataGroupID < 1) throw new Exception("Data group not found");
        }

        public qDbs_DataGroupConfig(string name)
            : this ()
        {
            container.Select("Name = @Name", new SqlQueryParameter("@Name", name));

            if (DataGroupID < 1) throw new Exception("Data group not found");
        }

        public static qDbs_DataGroupConfig[] GetSpatialDataGroups()
        {
            return Schema.container.Select <qDbs_DataGroupConfig> ("SupportsSpatial = 'Yes'", c => new qDbs_DataGroupConfig (c), null);
        }
    }
}