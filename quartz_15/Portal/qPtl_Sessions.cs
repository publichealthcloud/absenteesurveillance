using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_Sessions
    {
        protected static qPtl_Sessions schema = new qPtl_Sessions();

        protected DbRow container;
        protected readonly DbColumn<Int32> session_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> temp_session_id;
        protected readonly DbColumn<String> ipaddress;
        protected readonly DbColumn<String> computer_type;
        protected readonly DbColumn<String> browser_type;
        protected readonly DbColumn<DateTime?> start_time;
        protected readonly DbColumn<DateTime?> stop_time;
        protected readonly DbColumn<DateTime?> last_time_seen;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<DateTime?> session_length;

        public Int32 SessionID { get { return session_id.Value; } set { session_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 TempSessionID { get { return temp_session_id.Value; } set { temp_session_id.Value = value; } }
        public String IPAddress { get { return ipaddress.Value; } set { ipaddress.Value = value; } }
        public String ComputerType { get { return computer_type.Value; } set { computer_type.Value = value; } }
        public String BrowserType { get { return browser_type.Value; } set { browser_type.Value = value; } }
        public DateTime? StartTime { get { return start_time.Value; } set { start_time.Value = value; } }
        public DateTime? StopTime { get { return stop_time.Value; } set { stop_time.Value = value; } }
        public DateTime? LastTimeSeen { get { return last_time_seen.Value; } set { last_time_seen.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public DateTime? SessionLength { get { return session_length.Value; } set { session_length.Value = value; } }

        public qPtl_Sessions()
            : this(new DbRow())
        {
        }

        protected qPtl_Sessions(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Sessions");
            session_id = container.NewColumn<Int32>("SessionID", true);
            user_id = container.NewColumn<Int32>("UserID");
            temp_session_id = container.NewColumn<Int32>("TempSessionID");
            ipaddress = container.NewColumn<String>("IPAddress");
            computer_type = container.NewColumn<String>("ComputerType");
            browser_type = container.NewColumn<String>("BrowserType");
            start_time = container.NewColumn<DateTime?>("StartTime");
            stop_time = container.NewColumn<DateTime?>("StopTime");
            last_time_seen = container.NewColumn<DateTime?>("LastTimeSeen");
            scope_id = container.NewColumn<Int32>("ScopeID");
            created = container.NewColumn<DateTime>("Created");
            session_length = container.NewColumn<DateTime?>("SessionLength", true);
        }

        public qPtl_Sessions(Int32 session_id)
            : this()
        {
            container.Select("SessionID = @SessionID", new SqlQueryParameter("@SessionID", session_id));
        }

        public void Update()
        {
            container.Update("SessionID = @SessionID");
        }

        public void Insert()
        {
            SessionID = Convert.ToInt32(container.Insert());
        }

        public static int GetSessionsLast24Hours()
        {
            DateTime yesterday = new DateTime();
            yesterday = DateTime.Now;
            yesterday = yesterday.AddDays(-1);
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(SessionID) FROM qPtl_Sessions WHERE Created > '" + yesterday + "'"), CommandType.Text, null));
        }

        public static int GetAllSessions(int num_days)
        {
            DateTime boundary_date = new DateTime();
            boundary_date = DateTime.Now;
            boundary_date = boundary_date.AddDays(-num_days);

            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(SessionID) FROM qPtl_Sessions WHERE Created > '" + boundary_date + "'"), CommandType.Text, null));
        }

        public static int GetCurrentSessionID(int user_id)
        {
            int session_id = 0;

            string returned_session_id = Convert.ToString(SqlQuery.execute_sql_scalar(
                "SELECT TOP(1) SessionID FROM qPtl_Sessions WHERE UserID = @UserID ORDER BY LastTimeSeen DESC ",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }));

            if (!String.IsNullOrEmpty(returned_session_id))
                session_id = Convert.ToInt32(returned_session_id);

            return session_id;
        }

        public static void UpdateMobileSession(int user_id)
        {
            int curr_session_id = 0;

            curr_session_id = GetCurrentSessionID(user_id);

            if (curr_session_id > 0)
            {
                qPtl_Sessions session = new qPtl_Sessions(curr_session_id);
                session.LastTimeSeen = DateTime.Now;
                session.StopTime = null;
                session.Update();
            }
        }

        public static bool ValidateMobileSession(int session_id, int user_id)
        {
            bool is_valid = false;

            qPtl_Sessions session = new qPtl_Sessions(session_id);

            if (session.SessionID > 0)
            {
                if (session.UserID == user_id && String.IsNullOrEmpty(Convert.ToString(session.StopTime)))
                {
                    is_valid = true;
                }
            }

            return is_valid;
        }
    }

}
