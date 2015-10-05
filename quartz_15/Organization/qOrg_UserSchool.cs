using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Organization
{
    public class qOrg_UserSchool
    {
        protected static qOrg_UserSchool schema = new qOrg_UserSchool();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_school_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> school_id;
        protected readonly DbColumn<String> other_name;

        public Int32 UserSchoolID { get { return user_school_id.Value; } set { user_school_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 SchoolID { get { return school_id.Value; } set { school_id.Value = value; } }
        public String OtherName { get { return other_name.Value; } set { other_name.Value = value; } }

        public qOrg_UserSchool()
            : this(new DbRow())
        {
        }

        protected qOrg_UserSchool(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_UserSchools");
            user_school_id = container.NewColumn<Int32>("UserSchoolID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            school_id = container.NewColumn<Int32>("SchoolID");
            other_name = container.NewColumn<String>("OtherName");
        }

        public qOrg_UserSchool(Int32 user_school_id)
            : this()
        {
            container.Select("UserSchoolID = @UserSchoolID", new SqlQueryParameter("@UserSchoolID", user_school_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("UserSchoolID = @UserSchoolID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;

            UserSchoolID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_UserSchool> GetUserSchools (int user_id)
        {
            return schema.container.Select<qOrg_UserSchool>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("UserID", user_id) }
                }, c => new qOrg_UserSchool(c));
        }

        public static DataTable GetTopSchools(int num_returned)
        {
            string sql = string.Empty;
            sql = "SELECT TOP(" + num_returned + ")* FROM qOrg_UserSchools_MostPopular_View WHERE School <> 'Other' ORDER BY NumRegistrations DESC";

            return SqlQuery.execute_sql(sql);
        }

        public static void DeleteAllUserSchools(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteUserSchool(int user_id, int school_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id, " AND SchoolID = ", school_id));
        }

        public static qOrg_UserSchool GetUserSchool (int user_id)
        {
            qOrg_UserSchool user_school = new qOrg_UserSchool();

            user_school.container.Select(new DbQuery
            {
                Where = string.Format("UserID = {0} AND MarkAsDelete = 0", user_id)
            });

            if (user_school.UserSchoolID > 0) return user_school;
            else return null;
        }
    }
}
