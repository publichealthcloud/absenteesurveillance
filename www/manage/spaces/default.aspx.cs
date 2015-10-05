using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;

public partial class manage_default : System.Web.UI.Page
{
    public static string resources_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ResourcesUrl"]);

    protected int space_id;

    public int SpaceID
    {
        get { return space_id; }
        set { space_id = value; }
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_space_id = 0;
        if (Page.IsPostBack)
        {
            DropDownList space_list = (DropDownList) Master.FindControl("ddlSpaces");
            curr_space_id = Convert.ToInt32(space_list.SelectedValue);
        }
        else if (!String.IsNullOrEmpty(Request.QueryString["spaceID"]))
        {
            curr_space_id = Convert.ToInt32(Request.QueryString["spaceID"]);
        }
        else if (!String.IsNullOrEmpty(Convert.ToString(Session["manage_space_id"])) && Convert.ToString(Session["manage_space_id"]) != "0")
        {
            curr_space_id = Convert.ToInt32(Session["manage_space_id"]);
        }
        else
        {
            // get first space associated with this user
            var spaces = qPtl_SpaceAdmin_View.GetSpaceAdminsByUser(Convert.ToInt32(Context.Items["UserID"]));
            int i = 0;
            foreach (var s in spaces)
            {
                if (i == 0)
                {
                    curr_space_id = s.SpaceID;
                }
                i++;
            }
        }
        space_id = curr_space_id;
        loadPageInfo(curr_space_id);
        spacesidebar.SpaceID = curr_space_id;
    }

    protected void loadPageInfo(int space_id)
    {
        qSoc_Space space = new qSoc_Space(space_id);
        string html = string.Empty;

        var active_campaigns = qSoc_SpaceCampaign_View.GetAllSpaceCampaignsBySpaceByAvailability(space_id, "Yes");
        var archived_campaigns = qSoc_SpaceCampaign_View.GetAllSpaceCampaignsBySpaceByAvailability(space_id, "No");

        if (active_campaigns != null)
        {
            foreach (var a in active_campaigns)
            {
                html += "<li><a href=\"/manage/campaigns/campaign-details.aspx?campaignID=" + a.CampaignID + "\">" + a.CampaignName + "</a></li>";
            }

            litActiveCampaigns.Text = html;
        }

        html = string.Empty;
        
        if (archived_campaigns != null)
        {
            foreach (var a in archived_campaigns)
            {
                html += "<li><a href=\"/manage/campaigns/campaign-details.aspx?campaignID=" + a.CampaignID + "\">" + a.CampaignName + "</a></li>";
            }

            litArchivedCampaigns.Text = html;
        }
    }
}