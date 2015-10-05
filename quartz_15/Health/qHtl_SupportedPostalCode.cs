using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Health
{
    public class qHtl_SupportedPostalCode
    {
        protected static qHtl_SupportedPostalCode schema = new qHtl_SupportedPostalCode();

        protected DbRow container;
        protected readonly DbColumn<Int32> supported_postal_code_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> postal_code;

        public Int32 SupportedPostalCodeID { get { return supported_postal_code_id.Value; } set { supported_postal_code_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String PostalCode { get { return postal_code.Value; } set { postal_code.Value = value; } }

        public qHtl_SupportedPostalCode()
            : this(new DbRow())
        {
        }

        protected qHtl_SupportedPostalCode(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_SupportedPostalCodes");
            supported_postal_code_id = container.NewColumn<Int32>("SupportedPostalCodeID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            postal_code = container.NewColumn<String>("PostalCode");
        }

        public qHtl_SupportedPostalCode(Int32 supported_postal_code_id)
            : this()
        {
            container.Select("SupportedPostalCodeID = @SupportedPostalCodeID", new SqlQueryParameter("@SupportedPostalCodeID", supported_postal_code_id));
        }

        public qHtl_SupportedPostalCode(String postal_code)
            : this()
        {
            container.Select("PostalCode = @PostalCode", new SqlQueryParameter("@PostalCode", postal_code));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SupportedPostalCodeID = @SupportedPostalCodeID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            LastModified = DateTime.Now;

            SupportedPostalCodeID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qHtl_SupportedPostalCode> GetAllSupportedPostalCodes()
        {
            return schema.container.Select<qHtl_SupportedPostalCode>(
                new DbQuery
                {
                    Where = "MarkAsDelete = 0 AND Available = 'Yes'",
                    OrderBy = "PostalCode ASC",
                }, c => new qHtl_SupportedPostalCode(c));
        }

        public static void DeleteByPostalCode(string postal_code)
        {
            schema.container.Delete(string.Concat("PostalCode = ", postal_code));
        }

        public static void DeleteSupportedPostalCode(int supported_postal_code_id)
        {
            schema.container.Delete(string.Concat("SupportedPostalCodeID = ", supported_postal_code_id));
        }
    }
}
