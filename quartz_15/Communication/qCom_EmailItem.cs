using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Communication
{
    public class qCom_EmailItem
    {
        protected static qCom_EmailItem schema = new qCom_EmailItem();

        protected DbRow container;
        protected readonly DbColumn<Int32> email_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> type;
        protected readonly DbColumn<String> uri;
        protected readonly DbColumn<String> subject;
        protected readonly DbColumn<String> draft;
        protected readonly DbColumn<Int32> campaign_id;
        protected readonly DbColumn<Int32> language_id;

        public Int32 EmailID { get { return email_id.Value; } set { email_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Type { get { return type.Value; } set { type.Value = value; } }
        public String URI { get { return uri.Value; } set { uri.Value = value; } }
        public String Subject { get { return subject.Value; } set { subject.Value = value; } }
        public String Draft { get { return draft.Value; } set { draft.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }
        public Int32 LanguageID { get { return language_id.Value; } set { language_id.Value = value; } }

        public qCom_EmailItem()
            : this(new DbRow())
        {
        }

        protected qCom_EmailItem(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_EmailItem");
            email_id = container.NewColumn<Int32>("EmailID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            type = container.NewColumn<String>("Type");
            uri = container.NewColumn<String>("URI");
            subject = container.NewColumn<String>("Subject");
            draft = container.NewColumn<String>("Draft");
            campaign_id = container.NewColumn<Int32>("CampaignID");
            language_id = container.NewColumn<Int32>("LanguageID");
        }

        public qCom_EmailItem(Int32 email_id)
            : this()
        {
            container.Select("EmailID = @EmailID", new SqlQueryParameter("@EmailID", email_id));
        }

        public void Update()
        {
            container.Update("EmailID = @EmailID");
        }

        public void Insert()
        {
            EmailID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qCom_EmailItem> GetEmailItems()
        {
            return schema.container.Select<qCom_EmailItem>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qCom_EmailItem(c));
        }

        public static ICollection<qCom_EmailItem> GetEmailItemsByCampaign(int campaign_id)
        {
            return schema.container.Select<qCom_EmailItem>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND CampaignID = @CampaignID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@CampaignID", campaign_id) },
                    OrderBy = "Subject"
                }, c => new qCom_EmailItem(c));
        }
    }
}
