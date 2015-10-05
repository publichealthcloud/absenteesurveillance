using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserFamily
    {
        protected static qPtl_UserFamily schema = new qPtl_UserFamily();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_family_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> family_id;

        public Int32 UserFamilyID { get { return user_family_id.Value; } set { user_family_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 FamilyID { get { return family_id.Value; } set { family_id.Value = value; } }

        public qPtl_UserFamily()
            : this(new DbRow())
        {
        }

        protected qPtl_UserFamily(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserFamilies");
            user_family_id = container.NewColumn<Int32>("UserFamilyID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            family_id = container.NewColumn<Int32>("FamilyID");
        }

        public qPtl_UserFamily(Int32 user_family_id)
            : this()
        {
            container.Select("UserFamilyID = @UserFamilyID", new SqlQueryParameter("@UserFamilyID", user_family_id));
        }

        public void Update()
        {
            container.Update("UserFamilyID = @UserFamilyID");
        }

        public void Insert()
        {
            UserFamilyID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteAllFamilyUsers(int family_id)
        {
            schema.container.Delete(string.Concat("FamilyID = ", family_id));
        }

        public static void DeleteFamilyMember(int user_id, int family_id)
        {
            schema.container.Delete(string.Concat("FamilyID = ", family_id, " AND UserID = ", user_id));
        }

        public static qPtl_UserFamily GetFamilyByUserID(Int32 user_id)
        {
            var family = new qPtl_UserFamily();

            family.container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                });

            return family.FamilyID > 0 ? family : null;
        }

        public static ICollection<qPtl_UserFamily> GetFamilyMembers(int family_id)
        {
            return schema.container.Select<qPtl_UserFamily>(
                new DbQuery
                {
                    Join = new DbQuery.DbJoin("INNER", "qPtl_Families", "qPtl_Families.FamilyID = qPtl_UserFamilies.FamilyID"),
                    Where = "qPtl_UserFamilies.Available = 'Yes' AND qPtl_UserFamilies.FamilyID = @FamilyID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id) }
                },
                c => new qPtl_UserFamily(c));
        }
    }

    public class qPtl_UserFamily_View
    {
        protected static qPtl_UserFamily_View schema = new qPtl_UserFamily_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_family_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> family_id;
        protected readonly DbColumn<String> family_name;
        protected readonly DbColumn<String> user_name;
        protected readonly DbColumn<String> first_name;
        protected readonly DbColumn<String> last_name;
        protected readonly DbColumn<String> email;
        protected readonly DbColumn<String> highest_role;

        public Int32 UserFamilyID { get { return user_family_id.Value; } set { user_family_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 FamilyID { get { return family_id.Value; } set { family_id.Value = value; } }
        public String FamilyName { get { return family_name.Value; } set { family_name.Value = value; } }
        public String UserName { get { return user_name.Value; } set { user_name.Value = value; } }
        public String FirstName { get { return first_name.Value; } set { first_name.Value = value; } }
        public String LastName { get { return last_name.Value; } set { last_name.Value = value; } }
        public String Email { get { return email.Value; } set { email.Value = value; } }

        public qPtl_UserFamily_View()
            : this(new DbRow())
        {
        }

        protected qPtl_UserFamily_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserFamilies_View");
            user_family_id = container.NewColumn<Int32>("UserFamilyID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            family_id = container.NewColumn<Int32>("FamilyID");
            family_name = container.NewColumn<String>("FamilyName");
            user_name = container.NewColumn<String>("UserName");
            first_name = container.NewColumn<String>("FirstName");
            last_name = container.NewColumn<String>("LastName");
            email = container.NewColumn<String>("Email");
        }

        public qPtl_UserFamily_View(Int32 user_family_id)
            : this()
        {
            container.Select("UserFamilyID = @UserFamilyID", new SqlQueryParameter("@UserFamilyID", user_family_id));
        }

        public static ICollection<qPtl_UserFamily_View> GetFamilyMembers(int family_id)
        {
            return schema.container.Select<qPtl_UserFamily_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND FamilyID = @FamilyID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id) }
                },
                c => new qPtl_UserFamily_View(c));
        }

        public static ICollection<qPtl_UserFamily_View> GetFamilyMembersByHighestRole(int family_id, string highest_role)
        {
            return schema.container.Select<qPtl_UserFamily_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND FamilyID = @FamilyID AND HighestRole = @HighestRole",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id), new SqlQueryParameter("@HighestRole", highest_role) }
                },
                c => new qPtl_UserFamily_View(c));
        }

        public static ICollection<qPtl_UserFamily_View> GetFamilyMembersByHighestRoleUserID(int family_id, string highest_role)
        {
            return schema.container.Select<qPtl_UserFamily_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND FamilyID = @FamilyID AND HighestRole = @HighestRole",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@FamilyID", family_id), new SqlQueryParameter("@HighestRole", highest_role) }
                },
                c => new qPtl_UserFamily_View(c));
        }
    }
}
