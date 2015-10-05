using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_SchoolDistrictAdmin
    {
        protected static qPtl_SchoolDistrictAdmin schema = new qPtl_SchoolDistrictAdmin();

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
        protected readonly DbColumn<Int32> school_district_id;
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
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Boolean PrimaryAdmin { get { return primary_admin.Value; } set { primary_admin.Value = value; } }

        public qPtl_SchoolDistrictAdmin()
            : this(new DbRow())
        {
        }

        protected qPtl_SchoolDistrictAdmin(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_SchoolDistrictAdmins");
            space_admin_id = container.NewColumn<Int32>("SpaceAdminID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            primary_admin = container.NewColumn<Boolean>("PrimaryAdmin");
        }

        public qPtl_SchoolDistrictAdmin(Int32 school_district_admin_id)
            : this()
        {
            container.Select("SchoolDistrictAdminID = @SchoolDistrictAdminID", new SqlQueryParameter("@SchoolDistrictAdminID", school_district_admin_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SchoolDistrictAdminID = @SchoolDistrictAdminID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;

            SpaceAdminID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_SchoolDistrictAdmin> GetSchoolDistrictAdminsByUser (int user_id)
        {
            return schema.container.Select<qPtl_SchoolDistrictAdmin>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("UserID", user_id) }
                }, c => new qPtl_SchoolDistrictAdmin(c));
        }

        public static ICollection<qPtl_SchoolDistrictAdmin> GetSchoolDistrictAdminsBySchoolDistrict(int school_district_id)
        {
            return schema.container.Select<qPtl_SchoolDistrictAdmin>(
                new DbQuery
                {
                    Where = "SchoolDistrictID = @SchoolDistrictID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("SchoolDistrictID", school_district_id) }
                }, c => new qPtl_SchoolDistrictAdmin(c));
        }

        public static void DeleteAllSchoolDistrictAdmins(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteSchoolDistrictAdmin(int user_id, int school_district_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id, " AND SchoolDistrictID = ", school_district_id));
        }
    }

    public class qPtl_SchoolDistrictAdmin_View
    {
        protected static qPtl_SchoolDistrictAdmin_View schema = new qPtl_SchoolDistrictAdmin_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> school_district_admin_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<Boolean> primary_admin;
        protected readonly DbColumn<String> district_short_name;

        public Int32 SchoolDistrictAdminID { get { return school_district_admin_id.Value; } set { school_district_admin_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Boolean PrimaryAdmin { get { return primary_admin.Value; } set { primary_admin.Value = value; } }
        public String DistrictShortName { get { return district_short_name.Value; } set { district_short_name.Value = value; } }

        public qPtl_SchoolDistrictAdmin_View()
            : this(new DbRow())
        {
        }

        protected qPtl_SchoolDistrictAdmin_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_SchoolDistrictAdmins_View");
            school_district_admin_id = container.NewColumn<Int32>("SchoolDistrictAdminID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            primary_admin = container.NewColumn<Boolean>("PrimaryAdmin");
            district_short_name = container.NewColumn<String>("DistrictShortName");
        }

        public qPtl_SchoolDistrictAdmin_View(Int32 school_district_admin_id)
            : this()
        {
            container.Select("SchoolDistrictAdminID = @SchoolDistrictAdminID", new SqlQueryParameter("@SchoolDistrictAdminID", school_district_admin_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SchoolDistrictAdminID = @SchoolDistrictAdminID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            Available = "Yes";
            MarkAsDelete = 0;

            SchoolDistrictAdminID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_SchoolDistrictAdmin_View> GetSchoolDistrictAdminsByUser(int user_id)
        {
            return schema.container.Select<qPtl_SchoolDistrictAdmin_View>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("UserID", user_id) }
                }, c => new qPtl_SchoolDistrictAdmin_View(c));
        }

        public static ICollection<qPtl_SchoolDistrictAdmin_View> GetSchoolDistrictAdminsBySchoolDistrict(int school_district_id)
        {
            return schema.container.Select<qPtl_SchoolDistrictAdmin_View>(
                new DbQuery
                {
                    Where = "SchoolDistrictID = @SchoolDistrictID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("SchoolDistrictID", school_district_id) }
                }, c => new qPtl_SchoolDistrictAdmin_View(c));
        }
    }
}
