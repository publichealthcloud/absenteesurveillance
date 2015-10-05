using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Quartz.Portal
{
    public class qPtl_TempSession
    {
        protected static qPtl_TempSession schema = new qPtl_TempSession();

        protected DbRow container;
        protected readonly DbColumn<Int32>temp_session_id;
        protected readonly DbColumn<String> ipaddress;
        protected readonly DbColumn<String> computer_type;
        protected readonly DbColumn<String> browser_type;
        protected readonly DbColumn<DateTime> start_time;
        protected readonly DbColumn<DateTime?> stop_time;
        protected readonly DbColumn<DateTime?> last_time_seen;
        protected readonly DbColumn<String> entry_page;

        public Int32 SessionID { get { return temp_session_id.Value; } set { temp_session_id.Value = value; } }
        public String IPAddress { get { return ipaddress.Value; } set { ipaddress.Value = value; } }
        public String ComputerType { get { return computer_type.Value; } set { computer_type.Value = value; } }
        public String BrowserType { get { return browser_type.Value; } set { browser_type.Value = value; } }
        public DateTime StartTime { get { return start_time.Value; } set { start_time.Value = value; } }
        public DateTime? StopTime { get { return stop_time.Value; } set { stop_time.Value = value; } }
        public DateTime? LastTimeSeen { get { return last_time_seen.Value; } set { last_time_seen.Value = value; } }
        public String EntryPage { get { return entry_page.Value; } set { entry_page.Value = value; } }

        public qPtl_TempSession()
            : this(new DbRow())
        {
        }

        protected qPtl_TempSession(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_TempSessions");
            temp_session_id = container.NewColumn<Int32>("TempSessionID", true);
            ipaddress = container.NewColumn<String>("IPAddress");
            computer_type = container.NewColumn<String>("ComputerType");
            browser_type = container.NewColumn<String>("BrowserType");
            start_time = container.NewColumn<DateTime>("StartTime");
            stop_time = container.NewColumn<DateTime?>("StopTime");
            last_time_seen = container.NewColumn<DateTime?>("LastTimeSeen");
            entry_page = container.NewColumn<String>("EntryPage");
        }

        public qPtl_TempSession(Int32 temp_session_id)
            : this()
        {
            container.Select("TempSessionID = @TempSessionID", new SqlQueryParameter("@TempSessionID", temp_session_id));
        }

        public void Update()
        {
            container.Update("SessionID = @SessionID");
        }

        public void Insert()
        {
            SessionID = Convert.ToInt32(container.Insert());
        }

        public static int GetTempSessionsLast24Hours()
        {
            DateTime yesterday = new DateTime();
            yesterday = DateTime.Now;
            yesterday = yesterday.AddDays(-1);
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(TEmpSessionID) FROM qPtl_TempSessions WHERE Created > '" + yesterday + "'"), CommandType.Text, null));
        }
    }
}