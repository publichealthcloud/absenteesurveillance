using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;
using Quartz.Portal;
using Quartz.Report;

public partial class manage_campaigns_campaign_master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            
            if (campaign_id == 0)
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);

            CampaignHeader.CampaignID = campaign_id;
            CampaignHeader.Campaign = campaign;

            // load core objects
            var pref = qRpt_CampaignReportPreference.GetCampaignReportPreferenceByCampaignID(campaign_id);
            var a_report = qRpt_CampaignOverviewReport.GetCampaignOverviewByCampaignID(campaign_id);

            List<CampaignReport> s_report = new List<CampaignReport>();
            s_report = qRpt_CampaignReport.GenerateCampaignReportSummary(campaign_id);

            List<CampaignUserReport> u_report = new List<CampaignUserReport>();
            u_report = qRpt_CampaignReport.GenerateCampaignUserList(campaign_id);

            displayData(pref, campaign, a_report, s_report, u_report);
        }
    }

    protected void displayData(qRpt_CampaignReportPreference pref, qSoc_Campaign campaign, qRpt_CampaignOverviewReport a_report, List<CampaignReport> s_report, List<CampaignUserReport> u_report)
    {
        // summary
        if (pref.AnalyzedOverview == true)
        {
            //litDateReportGenerated.Text = "<br /><strong>Information last updated:</strong> " + a_report.LastTimeCompiled;
            campaignsidebar.Visible = true;
            CampaignSidebarRaw.Visible = false;
            campaignsidebar.Report = a_report;
        }
        else
        {
            //litDateReportGenerated.Text = "<br /><strong>Information last updated:</strong> " + DateTime.Now;
            campaignsidebar.Visible = false;
            CampaignSidebarRaw.Visible = true;
            CampaignSidebarRaw.S_Report = s_report;
            CampaignSidebarRaw.Report = a_report;
        }
    }
}
