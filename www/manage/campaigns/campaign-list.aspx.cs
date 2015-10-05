using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;

public partial class manage_manage_contests : System.Web.UI.Page
{
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
        loadPageInfo(curr_space_id);
    }
    
    protected void loadPageInfo(int space_id)
    {
        string list_html = string.Empty;

        var list = qSoc_Campaign.GetGenerallyAvailableCampaigns();
        if (list != null)
        {
            foreach (var l in list)
            {
                qSoc_Campaign item = new qSoc_Campaign(l.CampaignID);
                list_html += "<li><a href=\"/manage/campaigns/campaign-details.aspx?campaignID=" + item.CampaignID + "\">" + item.CampaignName + "</a></li>";
            }

            litList.Text = list_html;
        }
    }
}