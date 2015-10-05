using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Report;

public partial class manage_campaigns_controls_CampaignEnrollmentTrend : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected string campaign_name;
    protected qRpt_CampaignOverviewReport report;
    protected qRpt_CampaignReportPreference pref;
    protected qSoc_Campaign campaign;
    protected List<CampaignReport> s_report;
    protected List<CampaignUserReport> u_report;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    public string CampaignName
    {
        get { return campaign_name; }
        set { campaign_name = value; }
    }
    public qRpt_CampaignOverviewReport Report
    {
        get { return report; }
        set { report = value; }
    }
    public List<CampaignReport> S_Report
    {
        get { return s_report; }
        set { s_report = value; }
    }
    public List<CampaignUserReport> U_Report
    {
        get { return u_report; }
        set { u_report = value; }
    }
    public qRpt_CampaignReportPreference Pref
    {
        get { return pref; }
        set { pref = value; }
    }
    public qSoc_Campaign Campaign
    {
        get { return campaign; }
        set { campaign = value; }
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

            // build the report for the last 28 days
            DateTime end_date = new DateTime();
            end_date = DateTime.Now;
            DateTime start_date = new DateTime();
            string report_title = "Campaign Enrollment Last 28 Days";
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                if (Request.QueryString["mode"] == "since-start")
                {
                    string solution_start = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
                    DateTime solution_start_date = new DateTime();
                    solution_start_date = Convert.ToDateTime(solution_start);
                    start_date = solution_start_date;
                    report_title = "Campaign Enrollment Since Site Launch (" + solution_start + ")";
                }
                else if (Request.QueryString["mode"] == "this-year")
                {
                    start_date = DateTime.Now;
                    start_date = start_date.AddDays(-days_this_year);
                    report_title = "Campaign Enrollment This Year";
                }
                else
                    start_date = DateTime.Now.AddDays(-28);
            }
            else
                start_date = DateTime.Now.AddDays(-28);

            litReport.Text = report_title;

            List<CampaignReport> t_report = new List<CampaignReport>();
            t_report = qRpt_CampaignReport.GenerateCampaignReport(campaign_id, start_date, end_date);

            if (t_report != null)
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

                strJSChart += "['Date', 'Members Enrolling', { role: 'style' }],\n";
                int j = 0;
                foreach (var s in t_report)
                {
                    if (j < t_report.Count && j > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = s.data_date.ToString("M/d/yyyy");
                    strJSChart += "['" + curr_chart_date + "', " + s.num_enrolled + ", 'lightblue']";
                    j++;
                }
                strJSChart += "]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: '" + report_title + "',\n";
                strJSChart += "legend: 'none',\n";
                strJSChart += "hAxis: {title: 'Days', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Enrollments'},\n";
                strJSChart += "seriesType: \"bars\",\n";
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