using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_UserAction
    {
        protected static qPtl_UserAction schema = new qPtl_UserAction();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_action_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> action_id;
        protected readonly DbColumn<String> assign_type;
        protected readonly DbColumn<DateTime?> available_from;
        protected readonly DbColumn<DateTime?> available_to;
        protected readonly DbColumn<Int32> after_num_logins;
        protected readonly DbColumn<Int32> priority;
        protected readonly DbColumn<String> skip_allowed;
        protected readonly DbColumn<Int32> number_skips_allowed;
        protected readonly DbColumn<String> required;
        protected readonly DbColumn<String> optional_opt_out;
        protected readonly DbColumn<DateTime?> user_completed; 
        protected readonly DbColumn<DateTime?> user_opt_out;
        protected readonly DbColumn<Int32> user_num_skips;
        protected readonly DbColumn<DateTime?> user_last_skip;
        protected readonly DbColumn<Int32> last_user_action_session_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<String> redirect_url;
        protected readonly DbColumn<String> redirect_skip_url;

        public Int32 UserActionID { get { return user_action_id.Value; } set { user_action_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ActionID { get { return action_id.Value; } set { action_id.Value = value; } }
        public String AssignType { get { return assign_type.Value; } set { assign_type.Value = value; } }
        public DateTime? AvailableFrom { get { return available_from.Value; } set { available_from.Value = value; } }
        public DateTime? AvailableTo { get { return available_to.Value; } set { available_to.Value = value; } }
        public Int32 AfterNumLogins { get { return after_num_logins.Value; } set { after_num_logins.Value = value; } }
        public Int32 Priority { get { return priority.Value; } set { priority.Value = value; } }
        public String SkipAllowed { get { return skip_allowed.Value; } set { skip_allowed.Value = value; } }
        public Int32 NumberSkipsAllowed { get { return number_skips_allowed.Value; } set { number_skips_allowed.Value = value; } }
        public String Required { get { return required.Value; } set { required.Value = value; } }
        public String OptionalOptOut { get { return optional_opt_out.Value; } set { optional_opt_out.Value = value; } }
        public DateTime? UserCompleted { get { return user_completed.Value; } set { user_completed.Value = value; } }
        public DateTime? UserOptOut { get { return user_opt_out.Value; } set { user_completed.Value = value; } }
        public Int32 UserNumSkips { get { return user_num_skips.Value; } set { user_num_skips.Value = value; } }
        public DateTime? UserLastSkip { get { return user_last_skip.Value; } set { user_last_skip.Value = value; } }
        public Int32 LastUserActionSessionID { get { return last_user_action_session_id.Value; } set { last_user_action_session_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String RedirectURL { get { return redirect_url.Value; } set { redirect_url.Value = value; } }
        public String RedirectSkipURL { get { return redirect_skip_url.Value; } set { redirect_skip_url.Value = value; } }

        public qPtl_UserAction()
            : this(new DbRow())
        {
        }

        protected qPtl_UserAction(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserActions");
            user_action_id = container.NewColumn<Int32>("UserActionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            action_id = container.NewColumn<Int32>("ActionID");
            assign_type = container.NewColumn<String>("AssignType");
            available_from = container.NewColumn<DateTime?>("AvailableFrom");
            available_to = container.NewColumn<DateTime?>("AvailableTo");
            after_num_logins = container.NewColumn<Int32>("AfterNumLogins");
            priority = container.NewColumn<Int32>("Priority");
            skip_allowed = container.NewColumn<String>("SkipAllowed");
            number_skips_allowed = container.NewColumn<Int32>("NumberSkipsAllowed");
            required = container.NewColumn<String>("Required");
            optional_opt_out = container.NewColumn<String>("OptionalOptOut");
            user_completed = container.NewColumn<DateTime?>("UserCompleted");
            user_opt_out = container.NewColumn<DateTime?>("UserOptOut");
            user_num_skips = container.NewColumn<Int32>("UserNumSkips");
            user_last_skip = container.NewColumn<DateTime?>("UserLastSkip");
            last_user_action_session_id = container.NewColumn<Int32>("LastUserActionSessionID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            redirect_url = container.NewColumn<String>("RedirectURL");
            redirect_skip_url = container.NewColumn<String>("RedirectSkipURL"); 
        }

        public qPtl_UserAction(Int32 user_action_id)
            : this()
        {
            container.Select("UserActionID = @UserActionID", new SqlQueryParameter("@UserActionID", user_action_id));
        }

        public qPtl_UserAction(Int32 user_id, Int32 action_id)
            : this()
        {
            container.Select("UserID = @UserID AND ActionID = @ActionID", new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@ActionID", action_id));
        }

        public void Update()
        {
            container.Update("UserActionID = @UserActionID");
        }

        public void Insert()
        {
            UserActionID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteAllUserAction(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteUserAction(int user_id, int action_id)
        {
            schema.container.Delete(string.Concat("ActionID = ", action_id, " AND UserID = ", user_id));
        }

        public static qPtl_UserAction GetActionByUserAndAction(Int32 user_id, int action_id)
        {
            var action = new qPtl_UserAction();

            action.container.Select(
                new DbQuery
                {
                    Where = "UserID = @UserID AND ActionID = @ActionID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@ActionID", action_id), new SqlQueryParameter("@UserID", user_id) }
                });

            return action.UserActionID > 0 ? action : null;
        }

        public static ICollection<qPtl_UserAction> GetAvailableUserActionsByUser(int user_id)
        {
            return schema.container.Select<qPtl_UserAction>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                },
                c => new qPtl_UserAction(c));
        }
    }

    public class qPtl_UserAction_View
    {
        protected static qPtl_UserAction_View schema = new qPtl_UserAction_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_action_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> action_id;
        protected readonly DbColumn<String> assign_type;
        protected readonly DbColumn<DateTime?> available_from;
        protected readonly DbColumn<DateTime?> available_to;
        protected readonly DbColumn<Int32> after_num_logins;
        protected readonly DbColumn<Int32> priority;
        protected readonly DbColumn<String> skip_allowed;
        protected readonly DbColumn<Int32> number_skips_allowed;
        protected readonly DbColumn<String> required;
        protected readonly DbColumn<String> optional_opt_out;
        protected readonly DbColumn<DateTime?> user_completed;
        protected readonly DbColumn<DateTime?> user_opt_out;
        protected readonly DbColumn<Int32> user_num_skips;
        protected readonly DbColumn<DateTime?> user_last_skip;
        protected readonly DbColumn<Int32> last_user_action_session_id;
        protected readonly DbColumn<String> redirect_url;
        protected readonly DbColumn<String> redirect_skip_url;
        protected readonly DbColumn<String> action_name;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<String> more_info;
        protected readonly DbColumn<String> url;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Int32> campaign_id;

        public Int32 UserActionID { get { return user_action_id.Value; } set { user_action_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ActionID { get { return action_id.Value; } set { action_id.Value = value; } }
        public String AssignType { get { return assign_type.Value; } set { assign_type.Value = value; } }
        public DateTime? AvailableFrom { get { return available_from.Value; } set { available_from.Value = value; } }
        public DateTime? AvailableTo { get { return available_to.Value; } set { available_to.Value = value; } }
        public Int32 AfterNumLogins { get { return after_num_logins.Value; } set { after_num_logins.Value = value; } }
        public Int32 Priority { get { return priority.Value; } set { priority.Value = value; } }
        public String SkipAllowed { get { return skip_allowed.Value; } set { skip_allowed.Value = value; } }
        public Int32 NumberSkipsAllowed { get { return number_skips_allowed.Value; } set { number_skips_allowed.Value = value; } }
        public String Required { get { return required.Value; } set { required.Value = value; } }
        public String OptionalOptOut { get { return optional_opt_out.Value; } set { optional_opt_out.Value = value; } }
        public DateTime? UserCompleted { get { return user_completed.Value; } set { user_completed.Value = value; } }
        public DateTime? UserOptOut { get { return user_opt_out.Value; } set { user_completed.Value = value; } }
        public Int32 UserNumSkips { get { return user_num_skips.Value; } set { user_num_skips.Value = value; } }
        public DateTime? UserLastSkip { get { return user_last_skip.Value; } set { user_last_skip.Value = value; } }
        public Int32 LastUserActionSessionID { get { return last_user_action_session_id.Value; } set { last_user_action_session_id.Value = value; } }
        public String RedirectURL { get { return redirect_url.Value; } set { redirect_url.Value = value; } }
        public String RedirectSkipURL { get { return redirect_skip_url.Value; } set { redirect_skip_url.Value = value; } }
        public String ActionName { get { return action_name.Value; } set { action_name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public String MoreInfo { get { return more_info.Value; } set { more_info.Value = value; } }
        public String URL { get { return url.Value; } set { url.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }

        public qPtl_UserAction_View()
            : this(new DbRow())
        {
        }

        protected qPtl_UserAction_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserActions_View");
            user_action_id = container.NewColumn<Int32>("UserActionID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            action_id = container.NewColumn<Int32>("ActionID");
            assign_type = container.NewColumn<String>("AssignType");
            available_from = container.NewColumn<DateTime?>("AvailableFrom");
            available_to = container.NewColumn<DateTime?>("AvailableTo");
            after_num_logins = container.NewColumn<Int32>("AfterNumLogins");
            priority = container.NewColumn<Int32>("Priority");
            skip_allowed = container.NewColumn<String>("SkipAllowed");
            number_skips_allowed = container.NewColumn<Int32>("NumberSkipsAllowed");
            required = container.NewColumn<String>("Required");
            optional_opt_out = container.NewColumn<String>("OptionalOptOut");
            user_completed = container.NewColumn<DateTime?>("UserCompleted");
            user_opt_out = container.NewColumn<DateTime?>("UserOptOut");
            user_num_skips = container.NewColumn<Int32>("UserNumSkips");
            user_last_skip = container.NewColumn<DateTime?>("UserLastSkip");
            last_user_action_session_id = container.NewColumn<Int32>("LastUserActionSessionID");
            redirect_url = container.NewColumn<String>("RedirectURL");
            redirect_skip_url = container.NewColumn<String>("RedirectSkipURL");
            action_name = container.NewColumn<String>("ActionName");
            description = container.NewColumn<String>("Description");
            more_info = container.NewColumn<String>("MoreInfo");
            url = container.NewColumn<String>("URL");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            campaign_id = container.NewColumn<Int32>("CampaignID");
        }

        public qPtl_UserAction_View(Int32 user_action_id)
            : this()
        {
            container.Select("UserActionID = @UserActionID", new SqlQueryParameter("@UserActionID", user_action_id));
        }

        public qPtl_UserAction_View(Int32 user_id, Int32 action_id)
            : this()
        {
            container.Select("UserID = @UserID AND ActionID = @ActionID", new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@ActionID", action_id));
        }

        public static ICollection<qPtl_UserAction_View> GetUserActionsByUserID(int user_id, int mark_as_delete, string available)
        {
            return schema.container.Select<qPtl_UserAction_View>(
                new DbQuery
                {
                    Where = "Available = @Available AND UserID = @UserID AND MarkAsDelete = @MarkAsDelete",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@MarkAsDelete", mark_as_delete), new SqlQueryParameter("@Available", available) }
                },
                c => new qPtl_UserAction_View(c));
        }

        public static DataTable GetUserActionsByFilter(int num_records, int user_id, string order_by, string available, int mark_as_delete, string available_from, string available_to, bool user_completed, bool user_opt_out, int last_user_session)
        {
            string sql = string.Empty;

            if (num_records > 0)
                sql = "SELECT TOP(" + num_records + ")* FROM qPtl_UserActions_View WHERE Available = '" + available + "' AND MarkAsDelete = " + mark_as_delete;
            else
                sql = "SELECT * FROM qPtl_UserActions_View WHERE Available = '" + available + "' AND MarkAsDelete = " + mark_as_delete;

            if (user_id > 0)
                sql += " AND UserID = " + user_id;

            if (user_completed == false)
                sql += " AND UserCompleted Is null";

            if (user_opt_out == false)
                sql += " AND UserOptOut Is null";

           if (!String.IsNullOrEmpty(order_by))
               sql += " ORDER BY " + order_by;

            DataTable initial_results = new DataTable();
            initial_results = SqlQuery.execute_sql(sql);

            return initial_results;
        }
    }
}
