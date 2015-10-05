using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;

public partial class manage_tools_reset_entire_campaign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateCampaigns();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        int campaign_id = 0;
        DateTime start_date = new DateTime();

        if (!String.IsNullOrEmpty(Convert.ToString(datStart.SelectedDate)))
            start_date = Convert.ToDateTime(datStart.SelectedDate);

        if (!String.IsNullOrEmpty(ddlCampaigns.SelectedValue))
            campaign_id = Convert.ToInt32(ddlCampaigns.SelectedValue);
        
        // get all users in campaign
        var campaign_users = qSoc_UserCampaign_View.GetCampaignUsers(campaign_id);

        if (campaign_users != null)
        {
            foreach (var u in campaign_users)
            {
                qSoc_UserCampaign.ResetCampaign(campaign_id, u.UserID, u.UserCampaignID, start_date);
            }
        }
    }

    protected void populateCampaigns()
    {
        ddlCampaigns.DataSource = qSoc_Campaign.GetCampaigns();
        ddlCampaigns.DataTextField = "CampaignName";
        ddlCampaigns.DataValueField = "CampaignID";
        ddlCampaigns.DataBind();
        ddlCampaigns.Items.Insert(0, new ListItem("", string.Empty));
    }
}