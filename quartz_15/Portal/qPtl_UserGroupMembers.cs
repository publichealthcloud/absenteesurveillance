using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_UserGroupMembers
    {
        protected static qPtl_UserGroupMembers schema = new qPtl_UserGroupMembers();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_group_member_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_group_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Boolean> group_owner;

        public Int32 UserGroupMemberID { get { return user_group_member_id.Value; } set { user_group_member_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserGroupID { get { return user_group_id.Value; } set { user_group_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Boolean GroupOwner { get { return group_owner.Value; } set { group_owner.Value = value; } }

        public qPtl_UserGroupMembers()
            : this(new DbRow())
        {
        }

        protected qPtl_UserGroupMembers(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserGroupMembers");
            user_group_member_id = container.NewColumn<Int32>("UserGroupMemberID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_group_id = container.NewColumn<Int32>("UserGroupID");
            user_id = container.NewColumn<Int32>("UserID");
            group_owner = container.NewColumn<Boolean>("GroupOwner");

        }

        public qPtl_UserGroupMembers(Int32 user_group_member_id)
            : this()
        {
            container.Select("UserGroupMemberID = @UserGroupMemberID", new SqlQueryParameter("@UserGroupMemberID", user_group_member_id));
        }

        public void Update()
        {
            container.Update("UserGroupMemberID = @UserGroupMemberID");
        }

        public void Insert()
        {
            UserGroupMemberID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteUserGroupMember(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static ICollection<qPtl_UserGroupMembers> GetGroupMembersByGroup(int user_group_id)
        {
            return schema.container.Select<qPtl_UserGroupMembers>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserGroupID = @UserGroupID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserGroupID", user_group_id) }
                }, c => new qPtl_UserGroupMembers(c));
        }

        public static ICollection<qPtl_UserGroupMembers> GetAllGroupMembers()
        {
            return schema.container.Select<qPtl_UserGroupMembers>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0"
                }, c => new qPtl_UserGroupMembers(c));
        }
    }
}
