using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;

using Telerik.Web.UI;

using Quartz;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.Communication;
using Quartz.Organization;

public partial class campaign_manage_activities : System.Web.UI.Page
{
    public int campaign_id;
    public int user_id;
    public string username;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            CampaignActivitiesListEnhanced.CampaignID = campaign_id;   
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hplBackTop.NavigateUrl = "campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"];
            hplBackBottom.NavigateUrl = "campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"];

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();

            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
            {
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
                lblTitle.Text = campaign.CampaignName + " - Activities";
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"]);
    }
}
