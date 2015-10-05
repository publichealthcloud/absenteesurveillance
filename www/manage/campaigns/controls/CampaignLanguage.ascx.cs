using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Report;

public partial class manage_campaigns_controls_CampaignSummaryRaw : System.Web.UI.UserControl
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
        if (u_report != null)
        {
            int num_english = 0;
            int num_spanish = 0;
            foreach (var u in u_report)
            {
                if (u.language == "Español")
                    num_spanish++;
            }

            num_english = u_report.Count - num_spanish;

            // create opening/closing for chart javascript
            string strJSLoad = string.Empty;

            string strJSOpening = string.Empty;
            strJSOpening += "<script type=\"text/javascript\">\n";
            litJSOpening.Text = strJSOpening;

            string strJSClosing = string.Empty;
            strJSClosing += "</script>\n";
            litJSClosing.Text = strJSClosing;

            string strJSChart = string.Empty;

            strJSChart += "function drawChart() {\n";
            strJSChart += "var data = google.visualization.arrayToDataTable([\n";
            strJSChart += "['Language', 'Members'],\n";
            strJSChart += "['English', " + num_english + "],\n";
            strJSChart += "['Spanish', " + num_spanish + "],\n";
            strJSChart += "]);\n";
            strJSChart += "new google.visualization.PieChart(document.getElementById('visualizationChart')).draw(data, {title:\"Member Language Choice\"});\n";
            strJSChart += "}\n";

            strJSLoad += "drawChart();\n";
            litLanaguagePieChart.Text = "<div id=\"visualizationChart\" style=\"width: 600px; height: 400px;\"></div>";

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