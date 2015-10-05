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

public partial class manage_campaign_details : System.Web.UI.Page
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_space_id = 0;
        if (Page.IsPostBack)
        {
            DropDownList space_list = (DropDownList) Master.FindControl("ddlSpaces");
            curr_space_id = Convert.ToInt32(space_list.SelectedValue);
        }
        else
        {
            curr_space_id = Convert.ToInt32(Session["manage_space_id"]);
        }

        int reference_id = Convert.ToInt32(Request.QueryString["campaignID"]);
        campaign_id = reference_id;
        loadControls(curr_space_id, reference_id);
        campaignsidebar.CampaignID = campaign_id;

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
    
    protected void loadControls(int space_id, int reference_id)
    {
        string list_html = string.Empty;
        qSoc_Campaign campaign = new qSoc_Campaign(reference_id);

        if (!Page.IsPostBack)
        {
            var report = qRpt_CampaignOverviewReport.GetCampaignOverviewByCampaignID(reference_id);

            string roles = Convert.ToString(Context.Items["UserRoles"]);

            if (report != null)
            {
                if (report.CampaignReportOverviewID > 0)
                {
                    
                }
            }

            /*
            if (Request.QueryString["mode"] == "edit")
            {
                plhEdit.Visible = true;
                plhRead.Visible = false;
                btnEdit.Visible = false;
                btnSave.Visible = true;
            }
            else
            {
                if (roles.Contains("Site Admin"))
                    btnEdit.Visible = true;
                else
                    btnEdit.Visible = false;

                plhEdit.Visible = false;
                plhRead.Visible = true;
            }
             */
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
            CampaignSummaryAnalyzed.Visible = true;
            CampaignSummaryRaw.Visible = false;
            CampaignSummaryAnalyzed.Pref = pref;
            CampaignSummaryAnalyzed.Campaign = campaign;
            CampaignSummaryAnalyzed.Report = a_report;
            campaignsidebar.Report = a_report;
        }
        else
        {
            //litDateReportGenerated.Text = "<br /><strong>Information last updated:</strong> " + DateTime.Now;
            campaignsidebar.Visible = false;
            CampaignSidebarRaw.Visible = true;
            CampaignSummaryAnalyzed.Visible = false;
            CampaignSummaryRaw.Visible = true;
            CampaignSummaryRaw.Pref = pref;
            CampaignSummaryRaw.Campaign = campaign;
            CampaignSummaryRaw.S_Report = s_report;
            CampaignSidebarRaw.S_Report = s_report;
            CampaignSidebarRaw.Report = a_report;
        }

        if (pref.Language == true)
        {
            CampaignLanguage.Visible = true;
            CampaignLanguage.U_Report = u_report;
        }
        else
            CampaignLanguage.Visible = false;

        if (pref.MostRecentEnrolled == true)
        {
            CampaignMostRecentEnrolled.Visible = true;
            CampaignMostRecentEnrolled.CampaignID = campaign_id;
        }
        else
            CampaignMostRecentEnrolled.Visible = false;

        if (pref.HTMLReport == true)
        {
            CampaignHTMLReport.Visible = true;
            CampaignHTMLReport.Report = a_report;
        }
        else
            CampaignHTMLReport.Visible = false;

        if (pref.HealthKits == true)
        {
            CampaignMostRecentHealthKits.Visible = true;
            CampaignMostRecentHealthKits.CampaignID = campaign_id;
        }
        else
            CampaignMostRecentHealthKits.Visible = false;

        if (pref.EnrollmentTrend == true)
        {
            CampaignEnrollmentTrend.Visible = true;
            CampaignEnrollmentTrend.CampaignID = campaign_id;
        }
        else
            CampaignEnrollmentTrend.Visible = false;
    }
}