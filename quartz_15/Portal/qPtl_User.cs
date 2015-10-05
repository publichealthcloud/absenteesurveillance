using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Security;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Linq;
using System.IO;

using Quartz.Communication;
using Quartz.Portal;
using Quartz.Data;
using Quartz.Organization;

namespace Quartz.Portal
{
    public class qPtl_User
    {
        protected static qPtl_User schema = new qPtl_User();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> org_unit_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> username;
        protected readonly DbColumn<String> password;
        protected readonly DbColumn<String> password_reset_code;
        protected readonly DbColumn<String> first_name;
        protected readonly DbColumn<String> last_name;
        protected readonly DbColumn<String> email;
        protected readonly DbColumn<String> comments;
        protected readonly DbColumn<Int32> dm_user_id;
        protected readonly DbColumn<Int32> dm_status_id;
        protected readonly DbColumn<String> highest_role;
        protected readonly DbColumn<Int32> highest_rank;
        protected readonly DbColumn<DateTime?> last_time_seen;
        protected readonly DbColumn<String> last_ip_address;
        protected readonly DbColumn<String> profile_pict;
        protected readonly DbColumn<String> status;
        protected readonly DbColumn<DateTime?> status_date;
        protected readonly DbColumn<String> account_status;
        protected readonly DbColumn<Int32> last_activity;
        protected readonly DbColumn<Int32> number_logins;
        protected readonly DbColumn<String> registration_type;
        protected readonly DbColumn<String> registration_notes;

        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 OrgUnitID { get { return org_unit_id.Value; } set { org_unit_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String UserName { get { return username.Value; } set { username.Value = value; } }
        public String Password { get { return password.Value; } set { password.Value = value; } }
        public String PasswordResetCode { get { return password_reset_code.Value; } set { password_reset_code.Value = value; } }
        public String FirstName { get { return first_name.Value; } set { first_name.Value = value; } }
        public String LastName { get { return last_name.Value; } set { last_name.Value = value; } }
        public String Email { get { return email.Value; } set { email.Value = value; } }
        public String Comments { get { return comments.Value; } set { comments.Value = value; } }
        public Int32 DM_UserID { get { return dm_user_id.Value; } set { dm_user_id.Value = value; } }
        public Int32 DM_StatusID { get { return dm_status_id.Value; } set { dm_status_id.Value = value; } }
        public String HighestRole { get { return highest_role.Value; } set { highest_role.Value = value; } }
        public Int32 HighestRank { get { return highest_rank.Value; } set { highest_rank.Value = value; } }
        public DateTime? LastTimeSeen { get { return last_time_seen.Value; } set { last_time_seen.Value = value; } }
        public String LastIPAddress { get { return last_ip_address.Value; } set { last_ip_address.Value = value; } }
        public String ProfilePict { get { return profile_pict.Value; } set { profile_pict.Value = value; } }
        public String Status { get { return status.Value; } set { status.Value = value; } }
        public DateTime? StatusDate { get { return status_date.Value; } set { status_date.Value = value; } }
        public String AccountStatus { get { return account_status.Value; } set { account_status.Value = value; } }
        public Int32 LastActivity { get { return last_activity.Value; } set { last_activity.Value = value; } }
        public Int32 NumberLogins { get { return number_logins.Value; } set { number_logins.Value = value; } }
        public String RegistrationType { get { return registration_type.Value; } set { registration_type.Value = value; } }
        public String RegistrationNotes { get { return registration_notes.Value; } set { registration_notes.Value = value; } }

        public qPtl_User()
            : this(new DbRow())
        {
        }

        protected qPtl_User(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Users");
            user_id = container.NewColumn<Int32>("UserID", true);
            org_unit_id = container.NewColumn<Int32>("OrgUnitID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            username = container.NewColumn<String>("UserName");
            password = container.NewColumn<String>("Password");
            password_reset_code = container.NewColumn<String>("PasswordResetCode");
            first_name = container.NewColumn<String>("FirstName");
            last_name = container.NewColumn<String>("LastName");
            email = container.NewColumn<String>("Email");
            comments = container.NewColumn<String>("Comments");
            dm_user_id = container.NewColumn<Int32>("DM_UserID");
            dm_status_id = container.NewColumn<Int32>("DM_StatusID");
            highest_role = container.NewColumn<String>("HighestRole");
            highest_rank = container.NewColumn<Int32>("HighestRank");
            last_time_seen = container.NewColumn<DateTime?>("LastTimeSeen");
            last_ip_address = container.NewColumn<String>("LastIPAddress");
            profile_pict = container.NewColumn<String>("ProfilePict");
            status = container.NewColumn<String>("Status");
            status_date = container.NewColumn<DateTime?>("StatusDate");
            account_status = container.NewColumn<String>("AccountStatus");
            last_activity = container.NewColumn<Int32>("LastActivity");
            number_logins = container.NewColumn<Int32>("NumberLogins");
            registration_type = container.NewColumn<String>("RegistrationType");
            registration_notes = container.NewColumn<String>("RegistrationNotes");
        }

        public qPtl_User(Int32 user_id)
            : this()
        {
            container.Select("UserID = @UserID", new SqlQueryParameter("@UserID", user_id));
        }

        public qPtl_User(String user_name)
            : this()
        {
            container.Select("UserName = @UserName", new SqlQueryParameter("@UserName", user_name));
        }

        public void Update()
        {
            container.Update("UserID = @UserID");
        }

        public void Insert()
        {
            UserID = Convert.ToInt32(container.Insert());
        }

        public static DataTable GetUsersByFilters(int num_returned, int requester_user_id, string order_by)
        {
            // see if solution has a limit by type of user
            int limit_by_space_id = 0;
            qPtl_User user = new qPtl_User(requester_user_id);

            string sql = string.Empty;
            if (limit_by_space_id > 0)
            {
                if (num_returned > 0)
                    sql = "SELECT TOP(" + num_returned + ")* FROM qSoc_UserSpaces_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND SpaceID = " + limit_by_space_id;
                else
                    sql = "SELECT * FROM qSoc_UserSpaces_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active'  AND SpaceID = " + limit_by_space_id;
            }
            else
            {
                if (num_returned > 0)
                    sql = "SELECT TOP(" + num_returned + ")* FROM qPtl_Users_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active'";
                else
                    sql = "SELECT * FROM qPtl_Users_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active'";
            }

            if (!String.IsNullOrEmpty(order_by))
            {
                switch (order_by)
                {
                    case "alpha":
                        sql += " ORDER BY Username ASC";
                        break;
                    case "newest-to-oldest":
                        sql += " ORDER BY Created DESC";
                        break;
                    case "oldest-to-newest":
                        sql += " ORDER BY Created ASC";
                        break;
                    default:
                        sql += " ORDER BY Username ASC";
                        break;
                }
            }

            return SqlQuery.execute_sql(sql);
        }

        public static DataTable FindPeople(string keyword)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM qPtl_Users_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND (Username LIKE '%" + keyword + "%')";
            return SqlQuery.execute_sql(sql);
        }

        public static DataTable GetSingleUserTooltip(int user_id)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM qPtl_Users_Tooltip_View WHERE UserID = " + user_id;

            return SqlQuery.execute_sql(sql);
        }

        public static DataTable GetRandomUser(int scope_id, int exclude_user_id, bool must_have_profile_pic, bool host_only)
        {
            string sql = string.Empty;
            string host_only_sql = string.Empty;

            if (host_only == true)
                host_only_sql = " AND (HighestRole LIKE '%Admin%' OR HighestRole LIKE '%Host%') ";

            if (exclude_user_id > 0 && must_have_profile_pic == true)
                sql = "SELECT TOP 1 ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [RandomNumber],[UserID] FROM [dbo].[qPtl_Users] WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND ScopeID = " + scope_id + " AND UserID <> " + exclude_user_id + " AND ProfilePict Is not null " + host_only_sql + " ORDER BY RandomNumber";
            else if (exclude_user_id > 0)
                sql = "SELECT TOP 1 ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [RandomNumber],[UserID] FROM [dbo].[qPtl_Users] WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND ScopeID = " + scope_id + " AND UserID <> " + exclude_user_id + " " + host_only_sql + " ORDER BY RandomNumber";
            else if (must_have_profile_pic == true)
                sql = "SELECT TOP 1 ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [RandomNumber],[UserID] FROM [dbo].[qPtl_Users] WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND ScopeID = " + scope_id + " AND ProfilePict Is not null " + host_only_sql + " ORDER BY RandomNumber";
            else
                sql = "SELECT TOP 1 ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT)) AS [RandomNumber],[UserID] FROM [dbo].[qPtl_Users] WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND ScopeID = " + scope_id + " " + host_only_sql + " ORDER BY RandomNumber";

            return SqlQuery.execute_sql(sql);
        }

        public static qPtl_User UserLogon(String user_name, String password)
        {
            string hashed_password = string.Join("", SHA1CryptoServiceProvider.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2"))).ToLower();

            var user = new qPtl_User();
            user.container.Select(user.container.GetSelectColumns(),
                "",
                "Available = 'Yes' AND AccountStatus <> 'Deleted' AND MarkAsDelete = 0 AND UserName = @UserName AND Password = @Password",
                new SqlQueryParameter[] { new SqlQueryParameter("@UserName", user_name), new SqlQueryParameter("@Password", hashed_password) });

            return user.UserID > 0 ? user : null;
        }

        public static int UserLogoff(int user_id, int session_id)
        {
            qPtl_Sessions session = new qPtl_Sessions(session_id);
            session.StopTime = DateTime.Now;
            session.Update();

            qPtl_User user = new qPtl_User(Convert.ToInt32(user_id));
            DateTime last_time = new DateTime();
            last_time = Convert.ToDateTime(user.LastTimeSeen);
            user.LastTimeSeen = last_time.AddMinutes(-16);
            user.Update();

            return user_id;
        }

        public static qPtl_User GetUserByEmail(String email)
        {
            var user = new qPtl_User();
            user.container.Select(user.container.GetSelectColumns(),
                "",
                "Email = @Email AND MarkAsDelete = 0",
                new SqlQueryParameter[] { new SqlQueryParameter("@Email", email) });

            return user.UserID > 0 ? user : null;
        }

        public static qPtl_User GetUserByPhone(string mobile_phone_number, int scopeID)
        {
            string sql = string.Format("SELECT * FROM qPtl_Users_View WHERE (Phone1 = '{0}' OR Phone2 = '{0}') AND ScopeID = {1}", mobile_phone_number, scopeID);
            DataTable dt = SqlQuery.execute_sql(sql);

            int curr_user_id = 0;

            if (dt.Rows.Count > 0)
            {
                curr_user_id = Convert.ToInt32(dt.Rows[0]["UserID"]);
            }

            qPtl_User user = new qPtl_User(curr_user_id);

            return user;
        }

        public DataTable get_chat_users_distinct(int start_row, int end_row, string order_by_clause, object user_data)
        {
            return SqlQuery.execute_sql(string.Format("SELECT DISTINCT(UserID), UserID, ProfilePict, UserName, Country, HighestRole FROM (SELECT ROW_NUMBER() OVER (ORDER BY {0}) AS RowNumber, * FROM qSoc_UsersOnlineInChat_View WHERE IsOnline = 1 AND UserID != {1}) AS PagedView WHERE RowNumber BETWEEN {2} AND {3}", string.IsNullOrEmpty(order_by_clause) ? "HighestRole, UserName" : order_by_clause, UserID, start_row, end_row));
        }

        public void UpdateUserPassword(int user_id, string password)
        {
            string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");

            qPtl_User user = new qPtl_User(user_id);
            user.Password = password_for_storing;
            user.Update();
        }

        public bool SetPasswordResetCode(int user_id)
        {
            bool ret = false;

            string password_reset_code = FormsAuthentication.HashPasswordForStoringInConfigFile(DateTime.Now.ToString("s"), "sha1");
            
            qPtl_User user = new qPtl_User(user_id);
            user.PasswordResetCode = password_reset_code;
            user.Update();

            if (user.UserID > 0)
                ret = true;
            else
                ret = false;

            return ret;
        }

        public static ICollection<qPtl_User> SimplePeopleSearch(string keyword)
        {
            return schema.container.Select<qPtl_User>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND AccountStatus = 'Active' AND Username LIKE '%" + keyword + "%'",
                    OrderBy = "Username ASC"
                },
                c => new qPtl_User(c));
        }

        public static int GetNumUsersOnlineNotCurrent(int user_id)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT (*) FROM qSoc_UsersLastOnline_View WHERE IsOnline = 1 AND UserID <> @UserID", CommandType.Text, new SqlQueryParameter("@UserID", user_id)));
        }

        public static int GetNumUsersOnline()
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT (*) FROM qSoc_UsersLastOnline_View WHERE IsOnline = 1", CommandType.Text));
        }

        public static int GetNumActiveUsers(int num_days)
        {
            DateTime boundary_date = new DateTime();
            boundary_date = DateTime.Now;
            boundary_date = boundary_date.AddDays(-num_days);

            string sql = "SELECT COUNT (UserID) FROM qPtl_Users WHERE Available='Yes' AND MarkAsDelete = 0 AND Created > '" + boundary_date + "'";
            
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(sql, CommandType.Text));
        }

        public static int GetNumberUnreadIMNotCurrent(int user_id)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT (*) FROM cometchat WHERE [read] = 0 AND [from] <> @UserID", CommandType.Text, new SqlQueryParameter("@UserID", user_id)));
        }

        public static int[] GetAllUserAlerts(int user_id, bool message_center, bool notifications, bool campaign_activities)
        {
            int[] all_alerts;
            all_alerts = new int[4];
            int num_alerts_total = 0;

            if (notifications == true)
            {
                var unviewed_notifications = qPtl_Notification_View.GetUnviewedAvailableUserNotifications(user_id, 0);
                int num_unviewed_notifications = unviewed_notifications.Count();
                num_alerts_total += num_unviewed_notifications;
                all_alerts[1] = num_unviewed_notifications;
            }

            all_alerts[3] = num_alerts_total;

            return all_alerts;
        }

        public static DataTable GetUsersOnline(int user_id)
        {
            return SqlQuery.execute_sql(string.Format("SELECT UserID FROM qSoc_UsersLastOnline_View WHERE IsOnline = 1 AND UserID <> " + user_id));
        }
    }

    public class UserFunctions
    {
        public static qPtl_User RegisterNewUser(RegistrationData data)
        {
            int existing_user_id = 0;
            int new_space_id = 0;
            string sqlCode = string.Empty;

            // Redundancy check -- write Highest Level into qPtl_User table in case DB trigger not working
            qPtl_Role role = new qPtl_Role(data.default_role_id);

            // add user
            qPtl_User new_user = new qPtl_User();
            new_user.Available = "Yes";
            new_user.OrgUnitID = data.scope_id;
            new_user.ScopeID = data.scope_id;
            new_user.Created = DateTime.Now;
            new_user.CreatedBy = 0;
            new_user.LastModified = DateTime.Now;
            new_user.LastModifiedBy = 0;
            new_user.MarkAsDelete = 0;
            new_user.Status = "";                   // used to include a default message for their status, now leave blank
            new_user.FirstName = data.firstname;
            new_user.LastName = data.lastname;
            new_user.Email = data.email;
            new_user.UserName = data.username;
            string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(data.password, "sha1");
            new_user.Password = password_for_storing;
            new_user.AccountStatus = "Active";
            new_user.HighestRank = role.RoleRank;
            new_user.HighestRole = role.RoleName;
            new_user.Insert();
            existing_user_id = new_user.UserID;

            DateTime DOB;
            try
            {
                DOB = Convert.ToDateTime(data.dob);
            }
            catch
            {
                // no valid date so use default value
                DOB = Convert.ToDateTime("1/1/1900");
            }

            // add user profile
            qPtl_UserProfile new_profile = new qPtl_UserProfile();
            new_profile.UserID = existing_user_id;
            new_profile.ScopeID = data.scope_id;
            new_profile.Available = "Yes";
            new_profile.Created = DateTime.Now;
            new_profile.CreatedBy = existing_user_id;
            new_profile.LastModified = DateTime.Now;
            new_profile.LastModifiedBy = existing_user_id;
            new_profile.MarkAsDelete = 0;
            new_profile.Style = "default";
            new_profile.Visibility = "all";
            new_profile.Division = data.division;
            new_profile.Agency = data.agency;
            new_profile.Position = data.position;
            new_profile.Degrees = data.degrees;
            new_profile.Address1 = data.address;
            new_profile.Address2 = data.address2;
            new_profile.City = data.city;
            new_profile.StateProvince = data.state;
            new_profile.PostalCode = data.postal_code;
            new_profile.Country = data.country;
            new_profile.Gender = data.gender;
            new_profile.DOB = DOB;
            new_profile.Race = data.race;
            new_profile.EmploymentLocation = data.employment_location;
            new_profile.EmploymentSetting = data.employment_setting;
            new_profile.WorkSites = data.employment_sites;
            new_profile.Profession = data.profession;
            new_profile.Phone1 = data.work_phone;
            new_profile.Phone1Type = "work";
            new_profile.Insert();

            qPtl_User user = new qPtl_User(existing_user_id);

            // add user communication preference
            if (!String.IsNullOrEmpty(user.Email))
            {
                qCom_UserPreference connect = new qCom_UserPreference();
                connect.UserID = user.UserID;
                connect.Created = DateTime.Now;
                connect.CreatedBy = user.UserID;
                connect.LastModified = DateTime.Now;
                connect.LastModifiedBy = user.UserID;
                connect.Available = "Yes";
                connect.ScopeID = 1;
                connect.MarkAsDelete = 0;
                connect.OkBulkEmail = "Yes";
                connect.OkEmail = "Yes";
                connect.OkSms = "Yes";
                connect.LanguageID = 1;
                connect.Insert();
            }

            // ****************************************************
            // STEP 5: Add User Role & Supporting Role Structures
            // Add role
            /*
            qPtl_UserRole role = new qPtl_UserRole();
            role.UserID = user.UserID;
            role.RoleID = role_id;
            role.Insert();
             */
            qDbs_SQLcode sql = new qDbs_SQLcode();
            sqlCode = "INSERT INTO qPtl_UserRoles ([UserID],[RoleID]) VALUES(" + user.UserID + "," + data.default_role_id + ")";
            sql.ExecuteSQL(sqlCode);

            // Add possible role actions for the new user role
            AddRoleAction(data.default_role_id, data.scope_id, user);

            // add folder for user_data
            string rootLocation = HttpContext.Current.Server.MapPath("~/") + "user_data\\";

            if (!Directory.Exists(rootLocation + user.UserName))
                Directory.CreateDirectory(rootLocation + user.UserName);

            if (new_user.UserID > 0) return new_user;
            else return null;
        }

        protected static void AddRoleAction(int role_id, int scope_id, qPtl_User user)
        {
            var role_actions = qPtl_RoleAction.GetAvailableRoleActionsByRole(role_id, scope_id);
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;

            foreach (var a in role_actions)
            {
                qPtl_UserAction action = new qPtl_UserAction();
                qPtl_Action act = new qPtl_Action(a.ActionID);
                action.ScopeID = scope_id;
                action.Available = "Yes";
                action.Created = DateTime.Now;
                action.CreatedBy = user.UserID;
                action.LastModified = DateTime.Now;
                action.LastModifiedBy = user.UserID;
                action.MarkAsDelete = 0;
                action.UserID = user.UserID;
                action.ActionID = a.ActionID;
                startTime = startTime.AddDays(a.StartDaysFromNow);
                endTime = endTime.AddDays(a.EndDaysFromNow);
                action.AvailableFrom = startTime;
                action.AvailableTo = endTime;
                action.AfterNumLogins = a.AfterNumLogins;
                action.Priority = a.Priority;
                action.SkipAllowed = a.SkipAllowed;
                action.NumberSkipsAllowed = a.NumberSkipsAllowed;
                action.Required = a.Required;
                action.OptionalOptOut = a.OptionOptOut;
                action.RedirectSkipURL = a.RedirectSkipURL;
                action.RedirectURL = a.RedirectURL;
                action.ReferenceID = act.ReferenceID;
                action.Insert();
            }
        }

        protected static bool CheckUserName(string username)
        {
            bool username_available = false;

            qPtl_User user = new qPtl_User(username);

            if (user != null)
            {
                if (user.UserID > 0)
                    username_available = false;
                else
                    username_available = true;
            }
            else
                username_available = true;

            return username_available;
        }

        protected static bool CheckInvitation(string invite_code)
        {
            bool invitation_valid = false;

            if (qPtl_Invitation.InvitationValid(invite_code))
                invitation_valid = true;

            return invitation_valid;
        }

        protected static bool CheckAge(RegistrationData data)
        {
            bool valid_age = false;
            int regMinAge = 0;
            int regMaxAge = 0;

            // if invitation is being used then get associated role; then check age range based on role
            if (!String.IsNullOrEmpty(data.invite_code))
            {
                qPtl_Invitation invite = new qPtl_Invitation(data.invite_code);
                if (invite != null)
                {
                    if (invite.RoleID > 0)
                    {
                        qPtl_Role role = new qPtl_Role(invite.RoleID);

                        if (role != null)
                        {
                            if (role.RoleName.Contains("Teen"))  // use teen age ELSE assume adult user
                            {
                                regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MinAge"]);
                                regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenMaxAge"]);
                            }
                            else
                            {
                                regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenMaxAge"]) + 1;
                                regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MaxAge"]);
                            }
                        }
                    }
                }
            }
            else
            {
                regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MinAge"]);
                regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MaxAge"]);
            }

            try
            {
                DateTime DOB = Convert.ToDateTime(data.dob);
                DateTime currDate = DateTime.Now;
                int numYears = 0;
                try
                {
                    TimeSpan age = currDate.Subtract(DOB);
                    numYears = (age.Days / 365);
                }
                catch
                {
                    valid_age = false;
                }

                if (numYears >= regMinAge && numYears <= regMaxAge)
                {
                    valid_age = true;
                }
                else
                {
                    valid_age = false;
                }
            }
            catch
            {
                valid_age = false;
            }

            return valid_age;
        }

        public static qPtl_Sessions AddSession(qPtl_User user, int temp_session_id, string host_address, string browser, string platform)
        {
            qPtl_Sessions session = new qPtl_Sessions();
            session.Created = DateTime.Now;
            session.StartTime = DateTime.Now;
            session.LastTimeSeen = DateTime.Now;
            session.TempSessionID = temp_session_id;
            session.ScopeID = user.ScopeID;
            session.UserID = user.UserID;
            session.IPAddress = host_address;
            session.BrowserType = browser;
            session.ComputerType = platform;
            session.Insert();

            return session;
        }
    }

    public class RegistrationData
    {
        public int scope_id { get; set; }
        public string invite_code { get; set; }
        public string space_code { get; set; }
        public string campaign_code { get; set; }
        public string mobile_number { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string degrees { get; set; }
        public string position { get; set; }
        public string agency { get; set; }
        public string division { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string work_phone { get; set; }
        public string first_event { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public string ethnicity { get; set; }
        public string race { get; set; }
        public string profession { get; set; }
        public string employment_type { get; set; }
        public string employment_location { get; set; }
        public string employment_setting { get; set; }
        public string employment_sites { get; set; }
        public string registration_type { get; set; }
        public string registration_notes { get; set; }
        public int default_role_id { get; set; }
        public string browser { get; set; }
        public string platform { get; set; }
    }
}
