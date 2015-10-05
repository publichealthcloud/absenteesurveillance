using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_Permissions
    {
        protected static qPtl_Permissions schema = new qPtl_Permissions();

        protected DbRow container;
        protected readonly DbColumn<Int32> permission_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> data_group_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<String> applies_to;
        protected readonly DbColumn<Int32> applies_to_id;
        protected readonly DbColumn<String> permissions;

        public Int32 PermissionID { get { return permission_id.Value; } set { permission_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 DataGroupID { get { return data_group_id.Value; } set { data_group_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String AppliesTo { get { return applies_to.Value; } set { applies_to.Value = value; } }
        public Int32 AppliesToID { get { return applies_to_id.Value; } set { applies_to_id.Value = value; } }
        public String Permissions { get { return permissions.Value; } set { permissions.Value = value; } }

        public qPtl_Permissions()
            : this(new DbRow())
        {
        }

        protected qPtl_Permissions(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Permissions");
            permission_id = container.NewColumn<Int32>("PermissionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            data_group_id = container.NewColumn<Int32>("DataGroupID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            applies_to = container.NewColumn<String>("AppliesTo");
            applies_to_id = container.NewColumn<Int32>("AppliesToID");
            permissions = container.NewColumn<String>("Permissions");
        }

        public qPtl_Permissions(Int32 permission_id)
            : this()
        {
            container.Select("PermissionID = @PermissionID", new SqlQueryParameter("@PermissionID", permission_id));
        }

        public bool CanEditAll() { return Permissions[9] == '1'; }
        public bool CanAddAll() { return Permissions[13] == '1'; }

        public static qPtl_Permissions GetUserPermissions (int data_group_id, int reference_id, int user_id)
        {
            qPtl_Permissions permissions = new qPtl_Permissions();

            permissions.container.Select(new DbQuery
            {
                Where = string.Format("DataGroupID = {0} AND ReferenceID = {1} AND AppliesTo = 'User' AND AppliesToID = {2}", data_group_id, reference_id, user_id)
            });

            if (permissions.PermissionID > 0) return permissions;
            else return null;
        }
    }
}
