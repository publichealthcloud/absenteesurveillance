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

namespace Quartz.Report
{
    public class qRpt_SiteReport
    {
        public static List<SiteActivityReport> GenerateSiteReport(DateTime start_time, DateTime end_time)
        {
            List<SiteActivityReport> report = new List<SiteActivityReport>();

            TimeSpan t = end_time - start_time;
            double num_days = t.TotalDays;

            for (int i = 0; i < num_days; i++)
            {
                DateTime curr_date = new DateTime();
                curr_date = start_time.AddDays(i);

                int curr_num_sessions = 0;
                int curr_num_unique_sessions = 0;
                int curr_num_logins = 0;
                int curr_num_new_members = 0;
                int curr_num_logs = 0;

                curr_num_sessions = GetNumberSessions(curr_date);
                curr_num_unique_sessions = GetNumberUniqueSessions(curr_date);
                curr_num_logins = GetNumberLogins(curr_date);
                curr_num_new_members = GetNumberNewMembers(curr_date);

                report.Add(new SiteActivityReport()
                {
                    data_date = curr_date,
                    num_sessions = curr_num_sessions,
                    num_unique_sessions = curr_num_unique_sessions,
                    num_logins = curr_num_logins,
                    num_new_members = curr_num_new_members,
                    num_logs = curr_num_logs
                });
            }

            return report;
        }

        public static int GetNumberUniqueSessions(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT(DISTINCT IPAddress) As NumUniqueSessions FROM qPtl_TempSessions WHERE CAST(StartTime AS DATE) = '" + curr_date.Date + "'", CommandType.Text));
        }

        public static int GetNumberSessions(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT(IPAddress) As NumSessions FROM qPtl_TempSessions WHERE CAST(StartTime AS DATE) = '" + curr_date.Date + "'", CommandType.Text));
        }

        public static int GetNumberLogins(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT(UserID) As NumSessions FROM qPtl_Sessions WHERE CAST(StartTime AS DATE) = '" + curr_date.Date + "'", CommandType.Text));
        }

        public static int GetNumberNewMembers(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT(UserID) As NumMembers FROM qPtl_Users WHERE CAST(CREATED AS DATE) = '" + curr_date.Date + "'", CommandType.Text));
        }

        public static int GetNumberLogs(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar("SELECT COUNT(UserID) As NumLogs FROM qPtl_Logs WHERE CAST(CREATED AS DATE) = '" + curr_date.Date + "'", CommandType.Text));
        }
    }

    public class SiteActivityReport
    {
        public DateTime data_date { get; set; }
        public int num_sessions { get; set; }
        public int num_unique_sessions { get; set; }
        public int num_logins { get; set; }
        public int num_new_members { get; set; }
        public int num_logs { get; set; }
    }
}
