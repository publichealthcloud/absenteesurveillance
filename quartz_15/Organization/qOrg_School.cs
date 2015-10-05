using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Quartz.Portal;

namespace Quartz.Organization
{
    public class qOrg_School
    {
        protected static qOrg_School schema = new qOrg_School();

        protected DbRow container;
        protected readonly DbColumn<Int32> school_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> school_selector;
        protected readonly DbColumn<String> school;
        protected readonly DbColumn<String> address1;
        protected readonly DbColumn<String> address2;
        protected readonly DbColumn<String> city;
        protected readonly DbColumn<String> state_province;
        protected readonly DbColumn<String> postal_code;
        protected readonly DbColumn<String> country;
        protected readonly DbColumn<String> district;
        protected readonly DbColumn<String> school_type;
        protected readonly DbColumn<String> school_level;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<String> school_phone;
        protected readonly DbColumn<String> school_fax;
        protected readonly DbColumn<Decimal> latitude;
        protected readonly DbColumn<Decimal> longitude;

        public Int32 SchoolID { get { return school_id.Value; } set { school_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String SchoolSelector { get { return school_selector.Value; } set { school_selector.Value = value; } }
        public String School { get { return school.Value; } set { school.Value = value; } }
        public String Address1 { get { return address1.Value; } set { address1.Value = value; } }
        public String Address2 { get { return address2.Value; } set { address2.Value = value; } }
        public String City { get { return city.Value; } set { city.Value = value; } }
        public String StateProvince { get { return state_province.Value; } set { state_province.Value = value; } }
        public String PostalCode { get { return postal_code.Value; } set { postal_code.Value = value; } }
        public String Country { get { return country.Value; } set { country.Value = value; } }
        public String District { get { return district.Value; } set { district.Value = value; } }
        public String SchoolType { get { return school_type.Value; } set { school_type.Value = value; } }
        public String SchoolLevel { get { return school_level.Value; } set { school_level.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public String SchoolPhone { get { return school_phone.Value; } set { school_phone.Value = value; } }
        public String SchoolFax { get { return school_fax.Value; } set { school_fax.Value = value; } }
        public Decimal Latitude { get { return latitude.Value; } set { latitude.Value = value; } }
        public Decimal Longitude { get { return longitude.Value; } set { longitude.Value = value; } }

        public qOrg_School()
            : this(new DbRow())
        {
        }

        protected qOrg_School(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_Schools");
            school_id = container.NewColumn<Int32>("SchoolID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            school_selector = container.NewColumn<String>("SchoolSelector");
            school = container.NewColumn<String>("School");
            address1 = container.NewColumn<String>("Address1");
            address2 = container.NewColumn<String>("Address2"); 
            city = container.NewColumn<String>("City");
            state_province = container.NewColumn<String>("StateProvince");
            postal_code = container.NewColumn<String>("PostalCode");
            country = container.NewColumn<String>("Country");
            district = container.NewColumn<String>("District");
            school_type = container.NewColumn<String>("SchoolType");
            school_level = container.NewColumn<String>("SchoolLevel");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            school_phone = container.NewColumn<String>("SchoolPhone");
            school_fax = container.NewColumn<String>("SchoolFax");
            longitude = container.NewColumn<Decimal>("Longitude");
            latitude = container.NewColumn<Decimal>("Latitude");
        }

        public qOrg_School(Int32 school_id)
            : this()
        {
            container.Select("SchoolID = @SchoolID", new SqlQueryParameter("@SchoolID", school_id));
        }

        public void Update()
        {
            container.Update("SchoolID = @SchoolID");
        }

        public void Insert()
        {
            SchoolID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_School> GetSchools()
        {
            return schema.container.Select<qOrg_School>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "School ASC"
                }, c => new qOrg_School(c));
        }

        public static ICollection<qOrg_School> GetSchoolsByState(string state_province)
        {
            return schema.container.Select<qOrg_School>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND StateProvince = @StateProvince",
                    OrderBy = "School ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@StateProvince", state_province) }
                }, c => new qOrg_School(c));
        }

        public static ICollection<qOrg_School> GetSchoolsByPostalCode(string postal_code)
        {
            return schema.container.Select<qOrg_School>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND PostalCode = @PostalCode",
                    OrderBy = "School ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@PostalCode", postal_code) }
                }, c => new qOrg_School(c));
        }

        public static ICollection<qOrg_School> GetSchoolsByDistrictName(string district)
        {
            return schema.container.Select<qOrg_School>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND District = @District",
                    OrderBy = "School ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@District", district) }
                }, c => new qOrg_School(c));
        }

        public static ICollection<qOrg_School> GetSchoolsByDistrictID(int district_id)
        {
            return schema.container.Select<qOrg_School>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND SchoolDistrictID = @SchoolDistrictID",
                    OrderBy = "School ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@SchoolDistrictID", district_id) }
                }, c => new qOrg_School(c));
        }

        public static DataTable GetSchoolsByDistrictIDService(int school_district_id)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM qOrg_Schools WHERE Available = 'Yes' AND MarkAsDelete = 0 AND SchoolDistrictID = " + school_district_id;
            sql += " ORDER BY School ASC";

            return SqlQuery.execute_sql(sql);
        }

        public static DataTable GetSchoolsWithLongName()
        {
            return SqlQuery.execute_sql(string.Format("SELECT SchoolID, School, City, StateProvince, School + ' - ' + City + ', ' + StateProvince AS SchoolLong FROM qOrg_Schools WHERE Available = 'Yes' AND MarkAsDelete = 0 ORDER by SchoolLong ASC"));
        }

        public static qOrg_School GetSchoolFromAutoPopulateDropdown(string school_name)
        {
            var school = new qOrg_School();

            string eval = school_name;
            if (school_name.Contains(" - "))
            {
                int get_dash = school_name.IndexOf(" - ");
                eval = school_name.Substring(0, get_dash);
            }

            school.container.Select(
                new DbQuery
                {
                    Where = string.Format("School LIKE '%{0}%'", eval)
                });

            return school;
        }

        public static qOrg_School GetSchoolBySelector(string selector)
        {
            var school = new qOrg_School();
            string sql = string.Format("SchoolSelector = '{0}' AND Available = 'Yes' AND MarkAsDelete = 0", selector);

            school.container.Select(
                new DbQuery
                {
                    Where = sql
                });

            return school;
        }
    }
}
