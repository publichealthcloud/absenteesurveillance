using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Learning;
using Quartz.Portal;
using Quartz.Report;

public partial class manage_site_controls_SiteDashboard : System.Web.UI.UserControl
{
    protected static string google_api_key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_GoogleAPIKey"]);

    public string GoogleAPIKey
    {
        get { return google_api_key; }
        set { google_api_key = value; }
    }    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime this_week = new DateTime();
            this_week = DateTime.Now;
            DateTime today = new DateTime();
            today = DateTime.Now;
            DateTime this_year = new DateTime();
            this_year = DateTime.Now;
            DateTime first_day_in_month = new DateTime(today.Year, today.Month, 1);
            int days_this_week = Convert.ToInt32(today.DayOfWeek);
            int days_this_year = Convert.ToInt32(today.DayOfYear);

            TimeSpan elapsed_month = today.Subtract(first_day_in_month);
            var elapsed = Convert.ToInt32(elapsed_month.TotalDays);

            int total_members = qPtl_User.GetNumActiveUsers(10000);
            int members_this_week = qPtl_User.GetNumActiveUsers(days_this_week);
            int members_this_month = qPtl_User.GetNumActiveUsers(elapsed);
            int members_this_year = qPtl_User.GetNumActiveUsers(days_this_year);

            litSummaryTotalMembers.Text = Convert.ToString(total_members);
            litSummaryMembersThisWeek.Text = Convert.ToString(members_this_week);
            litSummaryMembersThisMonth.Text = Convert.ToString(members_this_month);
            litSummaryMembersThisYear.Text = Convert.ToString(members_this_year);

            int total_sessions = qPtl_Sessions.GetAllSessions(10000);
            int sessions_this_week = qPtl_Sessions.GetAllSessions(days_this_week);
            int sessions_this_month = qPtl_Sessions.GetAllSessions(elapsed);
            int sessions_this_year = qPtl_Sessions.GetAllSessions(days_this_year);

            litSummaryTotalSessions.Text = Convert.ToString(total_sessions);
            litSummarySessionsThisWeek.Text = Convert.ToString(sessions_this_week);
            litSummarySessionsThisMonth.Text = Convert.ToString(sessions_this_month);
            litSummarySessionsThisYear.Text = Convert.ToString(sessions_this_year);

            var trainings = qLrn_Training_View.GetTrainings();

            if (trainings != null)
            {
                string training_list = string.Empty;
                
                foreach (var t in trainings)
                {
                    training_list += "<li><i class=\"icon-external-link\"></i> <a href=\"/manage/site/learning/training-report.aspx?trainingID=" + t.TrainingID + "\">" + t.Title + "</a></li>";
                }

                if (!String.IsNullOrEmpty(training_list))
                    litTrainingList.Text = training_list;
            }

            litDailyDashboardTitle.Text = "Site Traffic";

            // build the report for the last 28 days
            DateTime end_date = new DateTime();
            end_date = DateTime.Now;
            DateTime start_date = new DateTime();
            string report_title = "Site Activity Last 28 Days";
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                if (Request.QueryString["mode"] == "since-start")
                {
                    string solution_start = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
                    DateTime solution_start_date = new DateTime();
                    solution_start_date = Convert.ToDateTime(solution_start);
                    start_date = solution_start_date;
                    report_title = "Site Activity Since Site Launch (" + solution_start + ")";
                }
                else if (Request.QueryString["mode"] == "this-year")
                {
                    start_date = DateTime.Now;
                    start_date = start_date.AddDays(-days_this_year);
                    report_title = "Site Activity This Year";
                }
                else
                    start_date = DateTime.Now.AddDays(-28);
            }
            else
                start_date = DateTime.Now.AddDays(-28);

            litReport.Text = report_title;

            List<SiteActivityReport> report = new List<SiteActivityReport>();
            report = qRpt_SiteReport.GenerateSiteReport(start_date, end_date);

            if (report != null)
            {
                // create opening/closing for chart javascript
                string strJSLoad = string.Empty;

                string strJSOpening = string.Empty;
                strJSOpening += "<script type=\"text/javascript\">\n";
                litJSOpening.Text = strJSOpening;

                string strJSClosing = string.Empty;
                strJSClosing += "</script>\n";
                litJSClosing.Text = strJSClosing;

                string strJSChart = string.Empty;

                // ******************* google chart bar -- site report *******************
                strJSChart += "function drawSiteReport() {\n";
                strJSChart += "var dataReport = google.visualization.arrayToDataTable([\n";

                strJSChart += "['Date', 'Unique Visitors', { role: 'style' }, 'Member Logins', 'New Members'],\n";
                int j = 0;
                foreach (var s in report)
                {
                    if (j < report.Count && j > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = s.data_date.ToString("M/d/yyyy");
                    strJSChart += "['" + curr_chart_date + "', " + s.num_unique_sessions + ", 'lightblue', " + s.num_logins + ", " + s.num_new_members + "]";
                    j++;
                }
                strJSChart += "]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: '" + report_title + "',\n";
                strJSChart += "legend: 'none',\n";
                strJSChart += "hAxis: {title: 'Days', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Activity'},\n";
                strJSChart += "seriesType: \"bars\",\n";
                strJSChart += "series: {1: {color: 'gray', type: \"line\"}, 2: {color: 'black', type: \"line\"}}\n";
                strJSChart += "};\n\n";

                strJSChart += "var chart = new google.visualization.ComboChart(document.getElementById('visualizationSiteReports'))\n";
                strJSChart += "chart.draw(dataReport, options);\n\n";
                strJSChart += "}\n";

                strJSLoad += "drawSiteReport();\n";

                litReport.Text = "<div id=\"visualizationSiteReports\" style=\"height: 600px;\"></div>";
                // *********************************************************************

                // add all finalized JS
                string finalJSLoad = "google.load(\"visualization\", \"1.1\", {packages:[\"corechart\"]});\n";
                finalJSLoad += "google.setOnLoadCallback(init);\n\n";
                finalJSLoad += "function init() {\n";
                finalJSLoad = finalJSLoad + strJSLoad + "\n}\n\n";

                litJSChart.Text = strJSChart;

                // finalize load code
                litJSLoad.Text = "<script type=\"text/javascript\">\n" + finalJSLoad + "</script>";
            }
        }
    }
}