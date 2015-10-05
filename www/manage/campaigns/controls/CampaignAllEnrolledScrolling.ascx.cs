using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Social;
using Quartz.Report;

public partial class manage_campaigns_controls_CampaignAllEnrolledScrolling : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected string campaign_name;
    protected qRpt_CampaignOverviewReport report;
    protected qRpt_CampaignReportPreference pref;
    protected qSoc_Campaign campaign;
    protected List<CampaignReport> s_report;
    protected List<CampaignUserReport> u_report;
    protected bool sms_ok;

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
    public bool SMSOk
    {
        get { return sms_ok; }
        set { sms_ok = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sms_ok == true)
            siteData.SelectCommand = "SELECT * FROM qSoc_UserCampaignLanguage_View WHERE MarkAsDelete = 0 AND SMSOk = 'Yes' AND CampaignID = " + campaign_id + " ORDER BY Username ASC";
        else
            siteData.SelectCommand = "SELECT * FROM qSoc_UserCampaignLanguage_View WHERE MarkAsDelete = 0 AND CampaignID = " + campaign_id + " ORDER BY Username ASC";

        if (!Page.IsPostBack)
        {
        }
    }
}