using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_HTMLRevision
    {
        protected static qPtl_HTMLRevision schema = new qPtl_HTMLRevision();

        protected DbRow container;
        protected readonly DbColumn<Int32> html_revision_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> module_instance_id;
        protected readonly DbColumn<String> source_type;
        protected readonly DbColumn<String> html;
        protected readonly DbColumn<Double> version_number;
        protected readonly DbColumn<String> version_info;
        protected readonly DbColumn<String> version_description;
        protected readonly DbColumn<Int32> language_id;

        public Int32 HTMLRevisionID { get { return html_revision_id.Value; } set { html_revision_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 ModuleInstanceID { get { return module_instance_id.Value; } set { module_instance_id.Value = value; } }
        public String SourceType { get { return source_type.Value; } set { source_type.Value = value; } }
        public String HTML { get { return html.Value; } set { html.Value = value; } }
        public Double VersionNumber { get { return version_number.Value; } set { version_number.Value = value; } }
        public String VersionInfo { get { return version_info.Value; } set { version_info.Value = value; } }
        public String VersionDescription { get { return version_description.Value; } set { version_description.Value = value; } }
        public Int32 LanguageID { get { return language_id.Value; } set { language_id.Value = value; } }

        public qPtl_HTMLRevision()
            : this(new DbRow())
        {
        }

        protected qPtl_HTMLRevision(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_HTMLRevisions");
            html_revision_id = container.NewColumn<Int32>("HTMLRevisionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            module_instance_id = container.NewColumn<Int32>("ModuleInstanceID");
            source_type = container.NewColumn<String>("SourceType");
            html = container.NewColumn<String>("HTML");
            version_number = container.NewColumn<Double>("VersionNumber");
            version_info = container.NewColumn<String>("VersionInfo");
            version_description = container.NewColumn<String>("VersionDescription");
            language_id = container.NewColumn<Int32>("LanguageID");
        }

        public qPtl_HTMLRevision(Int32 html_revision_id)
            : this()
        {
            container.Select("HTMLRevisionID = @HTMLRevisionID", new SqlQueryParameter("@HTMLRevisionID", html_revision_id));
        }

        public void Update()
        {
            container.Update("HTMLRevisionID = @HTMLRevisionID");
        }

        public void Insert()
        {
            HTMLRevisionID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_HTMLRevision> GetRevisions(int module_instance_id, string source_type, int language_id)
        {
            return schema.container.Select<qPtl_HTMLRevision>(
                new DbQuery
                {
                    Where = "ModuleInstanceID = @ModuleInstanceID AND SourceType = @SourceType AND LanguageID = @LanguageID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@ModuleInstanceID", module_instance_id),
                        new SqlQueryParameter ("@SourceType", source_type),
                        new SqlQueryParameter ("@LanguageID", language_id)
                    },
                    OrderBy = "VersionNumber DESC"
                }, c => new qPtl_HTMLRevision(c));
        }

        public static double GetLastVersionNumber(int module_instance_id, string source_type, int language_id)
        {
            double last_revision_number = 0;

            last_revision_number = Convert.ToDouble(SqlQuery.execute_sql_scalar(
                "SELECT TOP(1) VersionNumber FROM qPtl_HTMLRevisions WHERE ModuleInstanceID = @ModuleInstanceID AND SourceType = @SourceType AND LanguageID = @LanguageID ORDER BY VersionNumber DESC",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@ModuleInstanceID", module_instance_id), new SqlQueryParameter("@SourceType", source_type), new SqlQueryParameter("@LanguageID", language_id) }));

            return last_revision_number == null ? 0 : last_revision_number;
        }

        public static int GetRevisionCount(int module_instance_id, string source_type, int language_id)
        {
            int revision_count = 0;

            revision_count = Convert.ToInt32(SqlQuery.execute_sql_scalar(
                "SELECT COUNT(HTMLRevisionID) FROM qPtl_HTMLRevisions WHERE ModuleInstanceID = @ModuleInstanceID AND SourceType = @SourceType AND LanguageID = @LanguageID",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@ModuleInstanceID", module_instance_id), new SqlQueryParameter("@SourceType", source_type), new SqlQueryParameter("@LanguageID", language_id) }));

            return revision_count == null ? 0 : revision_count;
        }
    }
}
