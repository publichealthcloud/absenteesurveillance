using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_SpaceAdmin
    {
        protected static qPtl_SpaceAdmin schema = new qPtl_SpaceAdmin();

        protected DbRow container;
        protected readonly DbColumn<Int32> space_admin_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> space_id;
        protected readonly DbColumn<Boolean> primary_admin;

        public Int32 SpaceAdminID { get { return space_admin_id.Value; } set { space_admin_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 SpaceID { get { return space_id.Value; } set { space_id.Value = value; } }
        public Boolean PrimaryAdmin { get { return primary_admin.Value; } set { primary_admin.Value = value; } }

        public qPtl_SpaceAdmin()
            : this(new DbRow())
        {
        }

        protected qPtl_SpaceAdmin(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_SpaceAdmins");
            space_admin_id = container.NewColumn<Int32>("SpaceAdminID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            space_id = container.NewColumn<Int32>("SpaceID");
            primary_admin = container.NewColumn<Boolean>("PrimaryAdmin");
        }

        public qPtl_SpaceAdmin(Int32 space_admin_id)
            : this()
        {
            container.Select("SpaceAdminID = @SpaceAdminID", new SqlQueryParameter("@SpaceAdminID", space_admin_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SpaceAdminID = @SpaceAdminID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;

            SpaceAdminID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_SpaceAdmin> GetSpaceAdminsByUser (int user_id)
        {
            return schema.container.Select<qPtl_SpaceAdmin>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("UserID", user_id) }
                }, c => new qPtl_SpaceAdmin(c));
        }

        public static ICollection<qPtl_SpaceAdmin> GetSpaceAdminsBySpace(int space_id)
        {
            return schema.container.Select<qPtl_SpaceAdmin>(
                new DbQuery
                {
                    Where = "SpaceID = @SpaceID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("SpaceID", space_id) }
                }, c => new qPtl_SpaceAdmin(c));
        }

        public static void DeleteAllSpaceAdmins(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteSpaeAdmin(int user_id, int space_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id, " AND SpaceID = ", space_id));
        }
    }

    public class qPtl_SpaceAdmin_View
    {
        protected static qPtl_SpaceAdmin_View schema = new qPtl_SpaceAdmin_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> space_admin_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> space_id;
        protected readonly DbColumn<Boolean> primary_admin;
        protected readonly DbColumn<String> space_short_name;

        public Int32 SpaceAdminID { get { return space_admin_id.Value; } set { space_admin_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 SpaceID { get { return space_id.Value; } set { space_id.Value = value; } }
        public Boolean PrimaryAdmin { get { return primary_admin.Value; } set { primary_admin.Value = value; } }
        public String SpaceShortName { get { return space_short_name.Value; } set { space_short_name.Value = value; } }

        public qPtl_SpaceAdmin_View()
            : this(new DbRow())
        {
        }

        protected qPtl_SpaceAdmin_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_SpaceAdmins_View");
            space_admin_id = container.NewColumn<Int32>("SpaceAdminID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            space_id = container.NewColumn<Int32>("SpaceID");
            primary_admin = container.NewColumn<Boolean>("PrimaryAdmin");
            space_short_name = container.NewColumn<String>("SpaceShortName");
        }

        public qPtl_SpaceAdmin_View(Int32 space_admin_id)
            : this()
        {
            container.Select("SpaceAdminID = @SpaceAdminID", new SqlQueryParameter("@SpaceAdminID", space_admin_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SpaceAdminID = @SpaceAdminID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;

            SpaceAdminID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_SpaceAdmin_View> GetSpaceAdminsByUser(int user_id)
        {
            return schema.container.Select<qPtl_SpaceAdmin_View>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("UserID", user_id) }
                }, c => new qPtl_SpaceAdmin_View(c));
        }

        public static ICollection<qPtl_SpaceAdmin_View> GetSpaceAdminsBySpace(int space_id)
        {
            return schema.container.Select<qPtl_SpaceAdmin_View>(
                new DbQuery
                {
                    Where = "SpaceID = @SpaceID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("SpaceID", space_id) }
                }, c => new qPtl_SpaceAdmin_View(c));
        }
    }
}
