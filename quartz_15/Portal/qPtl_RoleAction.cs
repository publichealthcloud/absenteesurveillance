using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_RoleAction
    {
        protected static qPtl_RoleAction schema = new qPtl_RoleAction();

        protected DbRow container;
        protected readonly DbColumn<Int32> role_action_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> role_id;
        protected readonly DbColumn<Int32> action_id;
        protected readonly DbColumn<Int32> start_days_from_now;
        protected readonly DbColumn<Int32> end_days_from_now;
        protected readonly DbColumn<Int32> after_num_logins;
        protected readonly DbColumn<Int32> priority;
        protected readonly DbColumn<String> skip_allowed;
        protected readonly DbColumn<Int32> number_skips_allowed;
        protected readonly DbColumn<String> required;
        protected readonly DbColumn<String> option_opt_out;
        protected readonly DbColumn<String> redirect_url;
        protected readonly DbColumn<String> redirect_skip_url;

        public Int32 RoleActionID { get { return role_action_id.Value; } set { role_action_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 RoleID { get { return role_id.Value; } set { role_id.Value = value; } }
        public Int32 ActionID { get { return action_id.Value; } set { action_id.Value = value; } }
        public Int32 StartDaysFromNow { get { return start_days_from_now.Value; } set { start_days_from_now.Value = value; } }
        public Int32 EndDaysFromNow { get { return end_days_from_now.Value; } set { end_days_from_now.Value = value; } }
        public Int32 AfterNumLogins { get { return after_num_logins.Value; } set { after_num_logins.Value = value; } }
        public Int32 Priority { get { return priority.Value; } set { priority.Value = value; } }
        public String SkipAllowed { get { return skip_allowed.Value; } set { skip_allowed.Value = value; } }
        public Int32 NumberSkipsAllowed { get { return number_skips_allowed.Value; } set { number_skips_allowed.Value = value; } }
        public String Required { get { return required.Value; } set { required.Value = value; } }
        public String OptionOptOut { get { return option_opt_out.Value; } set { option_opt_out.Value = value; } }
        public String RedirectURL { get { return redirect_url.Value; } set { redirect_url.Value = value; } }
        public String RedirectSkipURL { get { return redirect_skip_url.Value; } set { redirect_skip_url.Value = value; } }

        public qPtl_RoleAction()
            : this(new DbRow())
        {
        }

        protected qPtl_RoleAction(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_RoleActions");
            role_action_id = container.NewColumn<Int32>("RoleActionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            role_id = container.NewColumn<Int32>("RoleID");
            action_id = container.NewColumn<Int32>("ActionID");
            start_days_from_now = container.NewColumn<Int32>("StartDaysFromNow");
            end_days_from_now = container.NewColumn<Int32>("EndDaysFromNow");
            after_num_logins = container.NewColumn<Int32>("AfterNumLogins");
            priority = container.NewColumn<Int32>("Priority");
            skip_allowed = container.NewColumn<String>("SkipAllowed");
            number_skips_allowed = container.NewColumn<Int32>("NumberSkipsAllowed");
            required = container.NewColumn<String>("Required");
            option_opt_out = container.NewColumn<String>("OptionOptOut");
            redirect_url = container.NewColumn<String>("RedirectURL");
            redirect_skip_url = container.NewColumn<String>("RedirectSkipURL");
        }

        public qPtl_RoleAction(Int32 role_action_id)
            : this()
        {
            container.Select("RoleActionID = @RoleActionID", new SqlQueryParameter("@RoleActionID", role_action_id));
        }

        public void Update()
        {
            container.Update("RoleActionID = @RoleActionID");
        }

        public void Insert()
        {
            RoleActionID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_RoleAction> GetAvailableRoleActions(int scope_id)
        {
            return schema.container.Select<qPtl_RoleAction>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND ScopeID = @ScopeID",
                    OrderBy = "Priority ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@ScopeID", scope_id) }
                },
                c => new qPtl_RoleAction(c));
        }

        public static ICollection<qPtl_RoleAction> GetAvailableRoleActionsByRole(int role_id, int scope_id)
        {
            return schema.container.Select<qPtl_RoleAction>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND RoleID = @RoleID AND ScopeID = @ScopeID",
                    OrderBy = "Priority ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@RoleID", role_id), new SqlQueryParameter("@ScopeID", scope_id) }
                },
                c => new qPtl_RoleAction(c));
        }
    }
}
