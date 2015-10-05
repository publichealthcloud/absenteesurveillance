using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_HealthProvider
    {
        protected static qHtl_HealthProvider schema = new qHtl_HealthProvider();

        protected DbRow container;
        protected readonly DbColumn<Int32> health_provider_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> health_provider_type;
        protected readonly DbColumn<String> name;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> address1;
        protected readonly DbColumn<String> address2;
        protected readonly DbColumn<String> city;
        protected readonly DbColumn<String> state_province;
        protected readonly DbColumn<String> postal_code;
        protected readonly DbColumn<String> country;
        protected readonly DbColumn<String> phone;
        protected readonly DbColumn<String> website;
        protected readonly DbColumn<String> service_type;
        protected readonly DbColumn<String> service_categories;

        public Int32 HealthProviderID { get { return health_provider_id.Value; } set { health_provider_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String HealthProviderType { get { return health_provider_type.Value; } set { health_provider_type.Value = value; } }
        public String Name { get { return name.Value; } set { name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String Address1 { get { return address1.Value; } set { address1.Value = value; } }
        public String Address2 { get { return address2.Value; } set { address2.Value = value; } }
        public String City { get { return city.Value; } set { city.Value = value; } }
        public String StateProvince { get { return state_province.Value; } set { state_province.Value = value; } }
        public String PostalCode { get { return postal_code.Value; } set { postal_code.Value = value; } }
        public String Country { get { return country.Value; } set { country.Value = value; } }
        public String Phone { get { return phone.Value; } set { phone.Value = value; } }
        public String Website { get { return website.Value; } set { website.Value = value; } }
        public String ServiceType { get { return service_type.Value; } set { service_type.Value = value; } }
        public String ServiceCategories { get { return service_categories.Value; } set { service_categories.Value = value; } }

        public qHtl_HealthProvider()
            : this(new DbRow())
        {
        }

        protected qHtl_HealthProvider(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_HealthProviders");
            health_provider_id = container.NewColumn<Int32>("HealthProviderID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            health_provider_type = container.NewColumn<String>("HealthProviderType");
            name = container.NewColumn<String>("name");
            description = container.NewColumn<String>("description");
            address1 = container.NewColumn<String>("Address1");
            address2 = container.NewColumn<String>("Address2");
            city = container.NewColumn<String>("City");
            state_province = container.NewColumn<String>("StateProvince");
            postal_code = container.NewColumn<String>("PostalCode");
            country = container.NewColumn<String>("Country");
            phone = container.NewColumn<String>("Phone");
            website = container.NewColumn<String>("Website");
            service_type = container.NewColumn<String>("ServiceType");
            service_categories = container.NewColumn<String>("ServiceCategories");
        }

        public qHtl_HealthProvider(Int32 health_provider_id)
            : this()
        {
            container.Select("HealthProviderID = @HealthProviderID", new SqlQueryParameter("@HealthProviderID", health_provider_id));
        }

        public qHtl_HealthProvider(String name)
            : this()
        {
            container.Select("Name LIKE @Name", new SqlQueryParameter("@Name", name));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("HealthProviderID = @HealthProviderID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            LastModified = DateTime.Now;

            HealthProviderID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qHtl_HealthProvider> GetHealthProviders()
        {
            return schema.container.Select<qHtl_HealthProvider>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                    OrderBy = "Name ASC"
                }, c => new qHtl_HealthProvider(c));
        }

        public static ICollection<qHtl_HealthProvider> GetAllSupportedPostalCodes()
        {
            return schema.container.Select<qHtl_HealthProvider>(
                new DbQuery
                {
                    Where = "MarkAsDelete = 0 AND Available = 'Yes'",
                    OrderBy = "PostalCode ASC",
                }, c => new qHtl_HealthProvider(c));
        }

        public static void DeleteHealthProvider(int health_provider_id)
        {
            schema.container.Delete(string.Concat("HealthProviderID = ", health_provider_id));
        }

        public static DataSet GetNearestHealthProvidersByPostalCode(string postal_code)
        {
            string sqlGIS = String.Empty;
            string distance = "3500";
            string sqlWHERE = String.Empty;
            string criteria = String.Empty;

            sqlGIS = "DECLARE @Zip nvarchar(5); ";
            sqlGIS += "DECLARE @Distance int; ";
            sqlGIS += "SET @Zip = '" + postal_code + "'; ";
            sqlGIS += "SET @Distance = " + distance + "; ";
            sqlGIS += "DECLARE @GeomZip geography ";
            sqlGIS += "SET @GeomZip = (SELECT TOP(1) g.geom FROM qGis_ReferenceObjects g WHERE g.Name = @Zip ORDER BY AREA DESC) ";
            sqlGIS += "SELECT h.*, (SELECT h.Geography.STDistance(@GeomZip)/1609.344) AS Distance ";
            sqlGIS += "FROM qHtl_HealthProviders_Spatial_View h ";
            sqlGIS += "WHERE (h.Geography.STIntersects(@GeomZip.STBuffer(@Distance * 1609.344))=1) ";
            sqlGIS += "ORDER BY Distance ASC ";
            criteria += " " + distance + " miles of " + postal_code;

            qDbs_SQLcode sql = new qDbs_SQLcode();

            DataSet dsHealthProviders = new DataSet();
            dsHealthProviders = sql.GetDataSet(sqlGIS);
            return dsHealthProviders;
        }
    }
}
