using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;
using Quartz.Portal;

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
        CampaignTopNav.CampaignID = reference_id;
        loadControls(curr_space_id, reference_id);
    }
    
    protected void loadControls(int space_id, int reference_id)
    {
        string list_html = string.Empty;
        qSoc_Campaign campaign = new qSoc_Campaign(reference_id);

        CampaignSelector.CampaignName = campaign.CampaignName;

        if (!Page.IsPostBack)
        {
            qSoc_Element html = new qSoc_Element("campaign-pages");

            if (html != null)
            {
                if (html.ElementID > 0)
                    litMainContent.Text = html.HTML;
            }
        }
    }
}