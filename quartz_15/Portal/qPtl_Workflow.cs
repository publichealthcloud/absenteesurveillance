using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Portal
{
    public class qPtl_Workflow
    {
        protected static qPtl_Workflow schema = new qPtl_Workflow();

        protected DbRow container;
        protected readonly DbColumn<Int32> workflow_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> action_order;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<String> reference_key_column;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Int32> log_action_id;
        protected readonly DbColumn<String> workflow_action_type;
        protected readonly DbColumn<Int32> workflow_action_reference_id;
        protected readonly DbColumn<String> code_class;
        protected readonly DbColumn<String> code_method;
        protected readonly DbColumn<String> sql;

        public Int32 WorkflowID { get { return workflow_id.Value; } set { workflow_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 ActionOrder { get { return action_order.Value; } set { action_order.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public String ReferenceKeyColumn { get { return reference_key_column.Value; } set { reference_key_column.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Int32 LogActionID { get { return log_action_id.Value; } set { log_action_id.Value = value; } }
        public String WorkflowActionType { get { return workflow_action_type.Value; } set { workflow_action_type.Value = value; } }
        public Int32 WorkflowActionReferenceID { get { return workflow_action_reference_id.Value; } set { workflow_action_reference_id.Value = value; } }
        public String Class { get { return code_class.Value; } set { code_class.Value = value; } }
        public String Method { get { return code_method.Value; } set { code_method.Value = value; } }
        public String SQL { get { return sql.Value; } set { sql.Value = value; } }

        public qPtl_Workflow()
            : this(new DbRow())
        {
        }

        protected qPtl_Workflow(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Workflows");
            workflow_id = container.NewColumn<Int32>("WorkflowID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            action_order = container.NewColumn<Int32>("ActionOrder");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_key_column = container.NewColumn<String>("ReferenceKeyColumn");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            log_action_id = container.NewColumn<Int32>("LogActionID");
            workflow_action_type = container.NewColumn<String>("WorkflowActionType");
            workflow_action_reference_id = container.NewColumn<Int32>("WorkflowActionReferenceID");
            code_class = container.NewColumn<String>("Class");
            code_method = container.NewColumn<String>("Method");
            sql = container.NewColumn<String>("SQL");
        }

        public qPtl_Workflow(Int32 workflow_id)
            : this()
        {
            container.Select("WorkflowID = @WorkflowID", new SqlQueryParameter("@WorkflowID", workflow_id));
        }

        public void Update()
        {
            container.Update("WorflowID = @WorkflowID");
        }

        public void Insert()
        {
            WorkflowID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_Workflow> GetWorkflows(int content_type_id, int log_action_id, int reference_id)
        {
            return schema.container.Select<qPtl_Workflow>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND ContentTypeID = @ContentTypeID AND ReferenceID = @ReferenceID",
                    OrderBy = "ActionOrder ASC",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@ContentTypeID", content_type_id),
                        new SqlQueryParameter ("@LogActionID", log_action_id),
                        new SqlQueryParameter ("@ReferenceID", reference_id)
                    }
                },
                c => new qPtl_Workflow(c));
        }

        public static string ProcessWorkflow(Int32 workflow_id, Int32 userID)
        {
            qDbs_SQLcode sql = new qDbs_SQLcode();
            qPtl_Workflow c_workflow = new qPtl_Workflow(workflow_id);
            string returnMessage = string.Empty;

            if (c_workflow.WorkflowActionType == "sql")
            {
                // get sql and then replace the parameters 
                string finalSql = c_workflow.SQL;

                // replace values
                finalSql = finalSql.Replace("{UserID}", Convert.ToString(userID));

                if (c_workflow.ReferenceID > 0)
                    finalSql = finalSql.Replace("{ReferenceID}", Convert.ToString(c_workflow.ReferenceID));

                if (c_workflow.WorkflowActionReferenceID > 0)
                    finalSql = finalSql.Replace("{WorkflowActionReferenceID}", Convert.ToString(c_workflow.WorkflowActionReferenceID));

                returnMessage = sql.ExecuteSQL(finalSql);
            }

            return returnMessage;
        }
    }
}
