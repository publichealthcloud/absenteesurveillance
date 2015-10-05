using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_Role
    {
       protected static qPtl_Role schema = new qPtl_Role();

        protected DbRow container;
        protected readonly DbColumn<Int32> role_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<String> role_name;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<Int32> role_rank;
        protected readonly DbColumn<String> login_allowed;
        protected readonly DbColumn<String> global_account;
        protected readonly DbColumn<String> social_allowed;

        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }        
        public String RoleName { get { return role_name.Value; } set { role_name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public Int32 RoleRank { get { return role_rank.Value; } set { role_rank.Value = value; } }
        public String LoginAllowed { get { return login_allowed.Value; } set { login_allowed.Value = value; } }
        public String GlobalAccount { get { return global_account.Value; } set { global_account.Value = value; } }
        public String SocialAllowed { get { return social_allowed.Value; } set { social_allowed.Value = value; } }

        public qPtl_Role()
            : this(new DbRow())
        {
        }

        protected qPtl_Role(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Roles");
            role_id = container.NewColumn<Int32>("RoleID", true);
            available = container.NewColumn<String>("Available");
            role_name = container.NewColumn<String>("RoleName");
            description = container.NewColumn<String>("Description");
            role_rank = container.NewColumn<Int32>("RoleRank");
            login_allowed = container.NewColumn<String>("LoginAllowed");
            global_account = container.NewColumn<String>("GlobalAccount");
            social_allowed = container.NewColumn<String>("SocialAllowed");
        }

        public qPtl_Role(Int32 role_id)
            : this()
        {
            container.Select("RoleID = @RoleID", new SqlQueryParameter("@RoleID", role_id));
        }

        public qPtl_Role(String role_name)
            : this()
        {
            container.Select("RoleName = @RoleName", new SqlQueryParameter("@RoleName", role_name));
        }

        public void Update()
        {
            container.Update("RoleID = @RoleID");
        }

        public void Insert()
        {
            RoleID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_Role> GetRoles()
        {
            return schema.container.Select<qPtl_Role>(
                new DbQuery
                {
                    Where = "Available = 'Yes'"
                }, c => new qPtl_Role(c));
        }
    }
}
