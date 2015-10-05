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
        if (s_report != null)
        {
            litSummaryTotalEnrolled.Text = Convert.ToString(s_report[0].num_enrolled);
            litSummaryWaitingToStart.Text = Convert.ToString(s_report[0].num_waiting);
            litSummaryInProgress.Text = Convert.ToString(s_report[0].num_in_progress);
            litSummaryFinished.Text = Convert.ToString(s_report[0].num_finished);
            string total_days_text = "open";
            if (!String.IsNullOrEmpty(Convert.ToString(campaign.TotalDays)))
            {
                if (campaign.TotalDays > 0)
                    total_days_text = campaign.TotalDays + " days";
            }
            litSummaryTotalDays.Text = total_days_text;
            if (campaign.Available == "Yes")
                litSummaryIsAvailable.Text = "<strong>Currently Available</strong>";
            else
                litSummaryIsAvailable.Text = "<strong>Not Available</strong>";
        }
    }
}