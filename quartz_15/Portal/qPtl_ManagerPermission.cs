using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_ManagerPermission
    {
       protected static qPtl_ManagerPermission schema = new qPtl_ManagerPermission();

        protected DbRow container;
        protected readonly DbColumn<Int32> manager_permission_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> role_id;
        protected readonly DbColumn<String> dashboard;
        protected readonly DbColumn<String> tasks;
        protected readonly DbColumn<String> site;
        protected readonly DbColumn<String> members;
        protected readonly DbColumn<String> learning;
        protected readonly DbColumn<String> communications;
        protected readonly DbColumn<String> health;
        protected readonly DbColumn<String> searches;
        protected readonly DbColumn<String> reports;
        protected readonly DbColumn<String> help;
        protected readonly DbColumn<String> admin;
        protected readonly DbColumn<String> cms;
        protected readonly DbColumn<String> user_search;
        protected readonly DbColumn<String> user_window;

        public Int32 ManagerPermissionID { get { return manager_permission_id.Value; } set { manager_permission_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }
        public String Dashboard { get { return dashboard.Value; } set { dashboard.Value = value; } }
        public String Tasks { get { return tasks.Value; } set { tasks.Value = value; } }
        public String Site { get { return site.Value; } set { site.Value = value; } }
        public String Members { get { return members.Value; } set { members.Value = value; } }
        public String Learning { get { return learning.Value; } set { learning.Value = value; } }
        public String Communications { get { return communications.Value; } set { communications.Value = value; } }
        public String Health { get { return health.Value; } set { health.Value = value; } }
        public String Searches { get { return searches.Value; } set { searches.Value = value; } }
        public String Reports { get { return reports.Value; } set { reports.Value = value; } }
        public String Help { get { return help.Value; } set { help.Value = value; } }
        public String Admin { get { return admin.Value; } set { admin.Value = value; } }
        public String CMS { get { return cms.Value; } set { cms.Value = value; } }
        public String UserSearch { get { return user_search.Value; } set { user_search.Value = value; } }
        public String UserWindow { get { return user_window.Value; } set { user_window.Value = value; } }

        public qPtl_ManagerPermission()
            : this(new DbRow())
        {
        }

        protected qPtl_ManagerPermission(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_ManagerPermissions");
            manager_permission_id = container.NewColumn<Int32>("ManagerPermissionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            role_id = container.NewColumn<Int32>("RoleID");
            dashboard = container.NewColumn<String>("Dashboard");
            tasks = container.NewColumn<String>("Tasks");
            site = container.NewColumn<String>("Site");
            members = container.NewColumn<String>("Members");
            learning = container.NewColumn<String>("Learning");
            communications = container.NewColumn<String>("Communications");
            health = container.NewColumn<String>("Health");
            searches = container.NewColumn<String>("Searches");
            reports = container.NewColumn<String>("Reports");
            help = container.NewColumn<String>("Help");
            admin = container.NewColumn<String>("Admin");
            cms = container.NewColumn<String>("CMS");
            user_search = container.NewColumn<String>("UserSearch");
            user_window = container.NewColumn<String>("UserWindow");
        }

        public qPtl_ManagerPermission(Int32 manager_permission_id)
            : this()
        {
            container.Select("ManagerPermissionID = @ManagerPermissionID", new SqlQueryParameter("@ManagerPermissionID", manager_permission_id));
        }

        public void Update()
        {
            container.Update("ManagerPermissionID = @ManagerPermissionID");
        }

        public void Insert()
        {
            ManagerPermissionID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_ManagerPermission> GetManagerPermissions()
        {
            return schema.container.Select<qPtl_ManagerPermission>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qPtl_ManagerPermission(c));
        }

        public static ICollection<qPtl_ManagerPermission> GetManagerPermissionsByRole(int role_id)
        {
            return schema.container.Select<qPtl_ManagerPermission>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@RoleID", role_id) }
                }, c => new qPtl_ManagerPermission(c));
        }

        public int GetSupportedRolePermissionTypeByRoleID(int role_id, string permission_type, string permission_value)
        {
            string sql = string.Format("SELECT ManagerPermissionID FROM qPtl_ManagerPermissions WHERE MarkAsDelete = 0 AND Available = 'Yes' AND RoleID = {0} AND {1} LIKE '%{2}%'", role_id, permission_value, permission_value);

            return Convert.ToInt32(SqlQuery.execute_sql_scalar(
                   sql, 
                   CommandType.Text,
                   new SqlQueryParameter[] { }));
        }
    }

    public class qPtl_ManagerPermission_View
    {
        protected static qPtl_ManagerPermission_View schema = new qPtl_ManagerPermission_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> manager_permission_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> role_id;
        protected readonly DbColumn<String> dashboard;
        protected readonly DbColumn<String> tasks;
        protected readonly DbColumn<String> site;
        protected readonly DbColumn<String> members;
        protected readonly DbColumn<String> learning;
        protected readonly DbColumn<String> communications;
        protected readonly DbColumn<String> health;
        protected readonly DbColumn<String> searches;
        protected readonly DbColumn<String> reports;
        protected readonly DbColumn<String> help;
        protected readonly DbColumn<String> admin;
        protected readonly DbColumn<String> cms;
        protected readonly DbColumn<String> user_search;
        protected readonly DbColumn<String> user_window;
        protected readonly DbColumn<String> role_name;
        protected readonly DbColumn<Int32> role_rank;

        public Int32 ManagerPermissionID { get { return manager_permission_id.Value; } set { manager_permission_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }
        public String Dashboard { get { return dashboard.Value; } set { dashboard.Value = value; } }
        public String Tasks { get { return tasks.Value; } set { tasks.Value = value; } }
        public String Site { get { return site.Value; } set { site.Value = value; } }
        public String Members { get { return members.Value; } set { members.Value = value; } }
        public String Learning { get { return learning.Value; } set { learning.Value = value; } }
        public String Communications { get { return communications.Value; } set { communications.Value = value; } }
        public String Health { get { return health.Value; } set { health.Value = value; } }
        public String Searches { get { return searches.Value; } set { searches.Value = value; } }
        public String Reports { get { return reports.Value; } set { reports.Value = value; } }
        public String Help { get { return help.Value; } set { help.Value = value; } }
        public String Admin { get { return admin.Value; } set { admin.Value = value; } }
        public String CMS { get { return cms.Value; } set { cms.Value = value; } }
        public String UserSearch { get { return user_search.Value; } set { user_search.Value = value; } }
        public String UserWindow { get { return user_window.Value; } set { user_window.Value = value; } }
        public String RoleName { get { return role_name.Value; } set { role_name.Value = value; } }
        public Int32 RoleRank { get { return role_rank.Value; } set { role_rank.Value = value; } }

        public qPtl_ManagerPermission_View()
            : this(new DbRow())
        {
        }

        protected qPtl_ManagerPermission_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_ManagerPermissions_View");
            manager_permission_id = container.NewColumn<Int32>("ManagerPermissionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            role_id = container.NewColumn<Int32>("RoleID");
            dashboard = container.NewColumn<String>("Dashboard");
            tasks = container.NewColumn<String>("Tasks");
            site = container.NewColumn<String>("Site");
            members = container.NewColumn<String>("Members");
            learning = container.NewColumn<String>("Learning");
            communications = container.NewColumn<String>("Communications");
            health = container.NewColumn<String>("Health");
            searches = container.NewColumn<String>("Searches");
            reports = container.NewColumn<String>("Reports");
            help = container.NewColumn<String>("Help");
            admin = container.NewColumn<String>("Admin");
            cms = container.NewColumn<String>("CMS");
            user_search = container.NewColumn<String>("UserSearch");
            user_window = container.NewColumn<String>("UserWindow");
            role_name = container.NewColumn<String>("RoleName");
            role_rank = container.NewColumn<Int32>("RoleRank");
        }

        public qPtl_ManagerPermission_View(Int32 manager_permission_id)
            : this()
        {
            container.Select("ManagerPermissionID = @ManagerPermissionID", new SqlQueryParameter("@ManagerPermissionID", manager_permission_id));
        }

        public qPtl_ManagerPermission_View(string role_name)
            : this()
        {
            container.Select("RoleName = @RoleName", new SqlQueryParameter("@RoleName", role_name));
        }

        public void Update()
        {
            container.Update("ManagerPermissionID = @ManagerPermissionID");
        }

        public void Insert()
        {
            ManagerPermissionID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_ManagerPermission_View> GetManagerPermissions()
        {
            return schema.container.Select<qPtl_ManagerPermission_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "RoleName ASC"
                }, c => new qPtl_ManagerPermission_View(c));
        }

        public static ICollection<qPtl_ManagerPermission_View> GetManagerPermissionsByRoleID(int role_id)
        {
            return schema.container.Select<qPtl_ManagerPermission_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND RoleID = @RoleID",
                    OrderBy = "RoleName ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@RoleID", role_id) }
                }, c => new qPtl_ManagerPermission_View(c));
        }

        public int GetSupportedRolePermissionTypeByHighestRole(string role_name, string permission_type, string permission_value)
        {
            string sql = string.Format("SELECT ManagerPermissionID FROM qPtl_ManagerPermissions_View WHERE MarkAsDelete = 0 AND Available = 'Yes' AND RoleName = '{0}' AND {1} LIKE '%{2}%'", role_id, permission_value, permission_value);

            return Convert.ToInt32(SqlQuery.execute_sql_scalar(
                   sql,
                   CommandType.Text,
                   new SqlQueryParameter[] { }));
        }
    }
}
