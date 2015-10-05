using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Organization
{
    public class qOrg_SchoolDistrict
    {
        protected static qOrg_SchoolDistrict schema = new qOrg_SchoolDistrict();

        protected DbRow container;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> district_name;
        protected readonly DbColumn<String> district_short_name;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> city;
        protected readonly DbColumn<String> state_province;

        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String DistrictName { get { return district_name.Value; } set { district_name.Value = value; } }
        public String DistrictShortName { get { return district_short_name.Value; } set { district_short_name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String City { get { return city.Value; } set { city.Value = value; } }
        public String StateProvince { get { return state_province.Value; } set { state_province.Value = value; } }

        public qOrg_SchoolDistrict()
            : this(new DbRow())
        {
        }

        protected qOrg_SchoolDistrict(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_SchoolDistricts");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            district_name = container.NewColumn<String>("DistrictName");
            district_short_name = container.NewColumn<String>("DistrictShortName");
            description = container.NewColumn<String>("Description");
            city = container.NewColumn<String>("City");
            state_province = container.NewColumn<String>("StateProvince");
        }

        public qOrg_SchoolDistrict(Int32 school_district_id)
            : this()
        {
            container.Select("SchoolDistrictID = @SchoolDistrictID", new SqlQueryParameter("@SchoolDistrictID", school_district_id));
        }

        public void Update()
        {
            container.Update("SchoolDistrictID = @SchoolDistrictID");
        }

        public void Insert()
        {
            SchoolDistrictID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_SchoolDistrict> GetSchoolDistricts()
        {
            return schema.container.Select<qOrg_SchoolDistrict>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "DistrictName ASC"
                }, c => new qOrg_SchoolDistrict(c));
        }
    }
}
