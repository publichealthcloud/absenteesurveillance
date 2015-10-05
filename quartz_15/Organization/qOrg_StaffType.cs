using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Organization
{
    public class qOrg_StaffType
    {
        protected static qOrg_StaffType schema = new qOrg_StaffType();

        protected DbRow container;
        protected readonly DbColumn<Int32> staff_type_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> staff_type_name;
        protected readonly DbColumn<String> alt_name;
        protected readonly DbColumn<String> code;

        public Int32 StaffTypeID { get { return staff_type_id.Value; } set { staff_type_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String StaffTypeName { get { return staff_type_name.Value; } set { staff_type_name.Value = value; } }
        public String AltName { get { return alt_name.Value; } set { alt_name.Value = value; } }
        public String Code { get { return code.Value; } set { code.Value = value; } }

        public qOrg_StaffType()
            : this(new DbRow())
        {
        }

        protected qOrg_StaffType(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_StaffTypes");
            staff_type_id = container.NewColumn<Int32>("StaffTypeID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            staff_type_name = container.NewColumn<String>("StaffTypeName");
            alt_name = container.NewColumn<String>("AltName");
            code = container.NewColumn<String>("Code");
        }

        public qOrg_StaffType(Int32 staff_type_id)
            : this()
        {
            container.Select("StaffTypeID = @StaffTypeID", new SqlQueryParameter("@StaffTypeID", staff_type_id));
        }

        public void Update()
        {
            container.Update("StaffTypeID = @StaffTypeID");
        }

        public void Insert()
        {
            StaffTypeID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_StaffType> GetStaffTypes()
        {
            return schema.container.Select<qOrg_StaffType>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qOrg_StaffType(c));
        }
    }
}
