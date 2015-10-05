using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Report;

public partial class manage_campaigns_controls_CampaignSummaryAnalyzed : System.Web.UI.UserControl
{
    protected qRpt_CampaignOverviewReport report;
    protected qRpt_CampaignReportPreference pref;
    protected qSoc_Campaign campaign;

    public qRpt_CampaignOverviewReport Report
    {
        get { return report; }
        set { report = value; }
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
        if (report != null)
        {
            litSummaryTotalEnrolled.Text = Convert.ToString(report.Enroll_NumWeb + report.Enroll_NumSMS + report.Enroll_NumApp);
            litSummaryTotalDays.Text = Convert.ToString(campaign.TotalDays) + " days";
            litSummaryCancelled.Text = Convert.ToString(report.Learn_NumCancelled);
            string start_available = string.Empty;
            string end_available = string.Empty;
            if (report.StartAvailable.HasValue)
                start_available = report.StartAvailable.Value.ToString("d");
            else
                start_available = "not set";
            if (report.EndAvailable.HasValue)
                end_available = report.EndAvailable.Value.ToString("d");
            else
                end_available = "not set";
            litSummaryAvailableDates.Text = start_available + " - " + end_available;
            litSummaryWaitingToStart.Text = Convert.ToString(report.Enroll_NumWaiting);
            litSummaryInProgress.Text = Convert.ToString(report.Enroll_NumInProgress);
            litSummaryFinished.Text = Convert.ToString(report.Enroll_NumFinished);
        }
    }
}