using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;

public partial class manage_campaigns_controls_CampaignSelector : System.Web.UI.UserControl
{
    protected int campaign_id;
    protected string campaign_name;

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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
            {
                if (!Page.IsPostBack)
                {
                    int campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                    int space_id = Convert.ToInt32(Session["manage_space_id"]);

                    int content_type_id = (int)qSoc_ContentType.Types.Campaign;
                    string list_html = string.Empty;

                    list_html = "<div class=\"btn-group\"><a href=\"/manage/campaigns/campaign-details.aspx?campaignID=" + campaign_id + "\" class=\"btn\">Selected Campaign: <strong>" + campaign_name + "</strong></a>";
		            list_html += "<a data-toggle=\"dropdown\" class=\"btn dropdown-toggle\"><span class=\"caret\"></span></a>";
		            list_html += "<ul class=\"dropdown-menu\">";

                    if (space_id > 0)
                    {
                        var list = qSoc_SpaceCampaign_View.GetAllSpaceCampaignsBySpaceByAvailability(space_id, "Yes");
                        var a_list = qSoc_SpaceCampaign_View.GetAllSpaceCampaignsBySpaceByAvailability(space_id, "No");
                        if (list != null)
                        {
                            foreach (var l in list)
                            {
                                list_html += "<li><a href=\"campaign-details.aspx?campaignID=" + l.CampaignID + "\">" + l.CampaignName + "</a></li>";
                            }
                        }
                        if (a_list != null)
                        {
                            foreach (var a in a_list)
                            {
                                list_html += "<li><a href=\"campaign-details.aspx?campaignID=" + a.CampaignID + "\">" + a.CampaignName + " (Archived)</a></li>";
                            }
                        }
                    }
                    list_html += "</ul></div>";

                    litSelector.Text = list_html;

                }
            }
        }
    }
}