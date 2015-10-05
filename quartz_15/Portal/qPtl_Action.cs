using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_Action
    {
        protected static qPtl_Action schema = new qPtl_Action();

        protected DbRow container;
        protected readonly DbColumn<Int32> action_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> action_name;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> more_info;
        protected readonly DbColumn<String> url;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Int32> campaign_id;

        public Int32 ActionID { get { return action_id.Value; } set { action_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String ActionName { get { return action_name.Value; } set { action_name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String MoreInfo { get { return more_info.Value; } set { more_info.Value = value; } }
        public String URL { get { return url.Value; } set { url.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }

        public qPtl_Action()
            : this(new DbRow())
        {
        }

        protected qPtl_Action(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Actions");
            action_id = container.NewColumn<Int32>("ActionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            action_name = container.NewColumn<String>("ActionName");
            description = container.NewColumn<String>("Description");
            more_info = container.NewColumn<String>("MoreInfo");
            url = container.NewColumn<String>("URL");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            campaign_id = container.NewColumn<Int32>("CampaignID");
        }

        public qPtl_Action(Int32 action_id)
            : this()
        {
            container.Select("ActionID = @ActionID", new SqlQueryParameter("@ActionID", action_id));
        }

        public void Update()
        {
            container.Update("ActionID = @ActionID");
        }

        public void Insert()
        {
            ActionID = Convert.ToInt32(container.Insert());
        }

        public static qPtl_Action GetActionByReferenceInfo(int content_type_id, int reference_id)
        {
            qPtl_Action action_item = new qPtl_Action();

            action_item.container.Select(new DbQuery
            {
                Where = string.Format("ContentTypeID = {0} AND ReferenceID = {1} AND Available = 'Yes' AND  MarkAsDelete = 0", content_type_id, reference_id)
            });

            if (action_item.ActionID > 0) return action_item;
            else return null;
        }

        public static ICollection<qPtl_Action> GetAvailableActions()
        {
            return schema.container.Select<qPtl_Action>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "ActionName ASC"
                },
                c => new qPtl_Action(c));
        }
    }
}
