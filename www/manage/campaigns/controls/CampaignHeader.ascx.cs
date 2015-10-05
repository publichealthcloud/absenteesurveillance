using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;

public partial class manage_campaigns_controls_CampaignHeader : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected qSoc_Campaign campaign;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
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
            CampaignTopNav.CampaignID = campaign_id;

            litCampaignName.Text = campaign.CampaignName;

            CampaignSelector.CampaignName = campaign.CampaignName;

            litDateReportGenerated.Text = "<br /><strong>Information last updated:</strong> " + DateTime.Now;
        }
    }
}