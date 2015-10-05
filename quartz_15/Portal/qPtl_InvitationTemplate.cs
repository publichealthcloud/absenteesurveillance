using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_InvitationTemplate
    {
       protected static qPtl_InvitationTemplate schema = new qPtl_InvitationTemplate();

        protected DbRow container;
        protected readonly DbColumn<Int32> invitation_template_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> header;
        protected readonly DbColumn<String> header_template;
        protected readonly DbColumn<String> footer;
        protected readonly DbColumn<String> footer_template;

        public Int32 InvitationTemplateID { get { return invitation_template_id.Value; } set { invitation_template_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Header { get { return header.Value; } set { header.Value = value; } }
        public String HeaderTemplate { get { return header_template.Value; } set { header_template.Value = value; } }
        public String Footer { get { return footer.Value; } set { footer.Value = value; } }
        public String FooterTemplate { get { return footer_template.Value; } set { footer_template.Value = value; } }

        public qPtl_InvitationTemplate()
            : this(new DbRow())
        {
        }

        protected qPtl_InvitationTemplate(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_InvitationTemplates");
            invitation_template_id = container.NewColumn<Int32>("InvitationTemplateID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            header = container.NewColumn<String>("Header");
            header_template = container.NewColumn<String>("HeaderTemplate");
            footer = container.NewColumn<String>("Footer");
            footer_template = container.NewColumn<String>("FooterTemplate");
        }

        public qPtl_InvitationTemplate(Int32 invitation_template_id)
            : this()
        {
            container.Select("InvitationTemplateID = @InvitationTemplateID", new SqlQueryParameter("@InvitationTemplateID", invitation_template_id));
        }

        public void Update()
        {
            container.Update("InvitationTemplateID = @InvitationTemplateID");
        }

        public void Insert()
        {
            InvitationTemplateID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_InvitationTemplate> GetInvitationTemplates()
        {
            return schema.container.Select<qPtl_InvitationTemplate>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "Name ASC",
                }, c => new qPtl_InvitationTemplate(c));
        }

        public static qPtl_InvitationTemplate GetTemplateByScopeID(int scope_id)
        {
            var template = new qPtl_InvitationTemplate();

            template.container.Select(new DbQuery
            {
                Where = "Available = 'Yes' AND MarkAsDelete = 0 AND ScopeID = @ScopeID",
                Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@ScopeID", scope_id) }
            });

            return template;
        }
    }
}
