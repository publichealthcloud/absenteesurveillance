using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserRole
    {
        public enum Types
        {
            Admin = 1,
            Host = 4,
            Teen = 9,
            Expert = 13,
            Mobile = 14,
            Parent = 16,
            Advisor = 17,
            Adult = 18,
            SpaceAdmin = 20
        }        
        
        
        protected static qPtl_UserRole schema = new qPtl_UserRole();
        protected static DbRow schema2 = new DbRow((new qPtl_UserRole()).container);

        protected DbRow container;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> role_id;

        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }

        public qPtl_UserRole()
            : this(new DbRow())
        {
        }

        protected qPtl_UserRole(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserRoles");
            user_id = container.NewColumn<Int32>("UserID", true);
            role_id = container.NewColumn<Int32>("RoleID", true);
        }

        public qPtl_UserRole(Int32 user_id, Int32 role_id)
            : this()
        {
            container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID AND RoleID = @RoleID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@RoleID", role_id) },
                },
                c => new qPtl_UserRole(c));
        }

        public void Insert()
        {
            container.Insert();
        }

        public static void DeleteAllUserRoles(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteUserRole(int user_id, int role_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id, " AND RoleID = ", role_id));
        }

        public static ICollection<qPtl_UserRole> GetUserRoles(int user_id)
        {
            return schema.container.Select<qPtl_UserRole>(
                new DbQuery
                {
                    Where = "qPtl_UserRoles.UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                },
                c => new qPtl_UserRole(c));
        }

        public static qPtl_UserRole[] GetUserRolesArray(int user_id)
        {
            DbRow[] userRoles = DbRow.Select(schema2, string.Concat("SELECT * FROM qPtl_UserRoles WHERE UserID = ", user_id), null);

            return DbRow.CreateArray<qPtl_UserRole>(userRoles, current_container => new qPtl_UserRole(current_container));
        }

        public static ICollection<qPtl_UserRole> GetRoleUsers(int role_id)
        {
            return schema.container.Select<qPtl_UserRole>(
                new DbQuery
                {
                    Where = "qPtl_UserRoles.RoleID = @RoleID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@RoleID", role_id) }
                },
                c => new qPtl_UserRole(c));
        }

        public static qPtl_UserRole[] GetRoleUsersArray(int role_id)
        {
            DbRow[] roleUsers = DbRow.Select(schema2, string.Concat("SELECT * FROM qPtl_UserRoles WHERE RoleID = ", role_id), null);

            return DbRow.CreateArray<qPtl_UserRole>(roleUsers, current_container => new qPtl_UserRole(current_container));
        }
    }

    public class qPtl_UserRole_View
    {
        protected static qPtl_UserRole_View schema = new qPtl_UserRole_View();
        protected static DbRow schema2 = new DbRow((new qPtl_UserRole_View()).container);

        protected DbRow container;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> role_id;
        protected readonly DbColumn<String> username;
        protected readonly DbColumn<String> first_name;
        protected readonly DbColumn<String> last_name;
        protected readonly DbColumn<String> role_name;
        protected readonly DbColumn<Int32> role_rank;
        protected readonly DbColumn<String> full_name;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<Int32> mark_as_delete;

        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }
        public String UserName { get { return username.Value; } set { username.Value = value; } }
        public String FirstName { get { return first_name.Value; } set { first_name.Value = value; } }
        public String LastName { get { return last_name.Value; } set { last_name.Value = value; } }
        public String RoleName { get { return role_name.Value; } set { role_name.Value = value; } }
        public Int32 RoleRank { get { return role_rank.Value; } set { role_rank.Value = value; } }
        public String FullName { get { return full_name.Value; } set { full_name.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }

        public qPtl_UserRole_View()
            : this(new DbRow())
        {
        }

        protected qPtl_UserRole_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserRoles_View");
            user_id = container.NewColumn<Int32>("UserID", true);
            role_id = container.NewColumn<Int32>("RoleID", true);
            username = container.NewColumn<String>("UserName");
            first_name = container.NewColumn<String>("FirstName");
            last_name = container.NewColumn<String>("LastName");
            role_name = container.NewColumn<String>("RoleName");
            role_rank = container.NewColumn<Int32>("RoleRank");
            full_name = container.NewColumn<String>("FullName");
            available = container.NewColumn<String>("Available");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
        }

        public qPtl_UserRole_View(Int32 user_id, Int32 role_id)
            : this()
        {
            container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID AND RoleID = @RoleID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@RoleID", role_id) },
                },
                c => new qPtl_UserRole_View(c));
        }

       public static ICollection<qPtl_UserRole_View> GetUserRoles(int user_id)
        {
            return schema.container.Select<qPtl_UserRole_View>(
                new DbQuery
                {
                    Where = "qPtl_UserRoles_View.UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                },
                c => new qPtl_UserRole_View(c));
        }

        public static qPtl_UserRole_View[] GetUserRolesArray(int user_id)
        {
            DbRow[] userRoles = DbRow.Select(schema2, string.Concat("SELECT * FROM qPtl_UserRoles_View WHERE Available = 'Yes' AND MarkAsDelete = 0 UserID = ", user_id), null);

            return DbRow.CreateArray<qPtl_UserRole_View>(userRoles, current_container => new qPtl_UserRole_View(current_container));
        }

        public static ICollection<qPtl_UserRole_View> GetRoleUsers(int role_id)
        {
            return schema.container.Select<qPtl_UserRole_View>(
                new DbQuery
                {
                    Where = "qPtl_UserRoles_View.Available = 'Yes' AND qPtl_UserRoles_View.MarkAsDelete = 0 AND qPtl_UserRoles_View.RoleID = @RoleID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@RoleID", role_id) }
                },
                c => new qPtl_UserRole_View(c));
        }

        public static qPtl_UserRole_View[] GetRoleUsersArray(int role_id)
        {
            DbRow[] roleUsers = DbRow.Select(schema2, string.Concat("SELECT * FROM qPtl_UserRoles_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND RoleID = ", role_id), null);

            return DbRow.CreateArray<qPtl_UserRole_View>(roleUsers, current_container => new qPtl_UserRole_View(current_container));
        }
    }
}
