using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Organization
{
    public class qOrg_LocationType
    {
        protected static qOrg_LocationType schema = new qOrg_LocationType();

        protected DbRow container;
        protected readonly DbColumn<Int32> location_type_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> location_type_name;

        public Int32 LocationTypeID { get { return location_type_id.Value; } set { location_type_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String LocationTypeName { get { return location_type_name.Value; } set { location_type_name.Value = value; } }

        public qOrg_LocationType()
            : this(new DbRow())
        {
        }

        protected qOrg_LocationType(DbRow c)
        {
            container = c;
            container.SetContainerName("qOrg_LocationTypes");
            location_type_id = container.NewColumn<Int32>("LocationTypeID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            location_type_name = container.NewColumn<String>("LocationTypeName");
        }

        public qOrg_LocationType(Int32 location_type_id)
            : this()
        {
            container.Select("LocationTypeID = @LocationTypeID", new SqlQueryParameter("@LocationTypeID", location_type_id));
        }

        public void Update()
        {
            container.Update("LocationTypeID = @LocationTypeID");
        }

        public void Insert()
        {
            LocationTypeID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qOrg_LocationType> GetLocationTypes()
        {
            return schema.container.Select<qOrg_LocationType>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qOrg_LocationType(c));
        }
    }
}
