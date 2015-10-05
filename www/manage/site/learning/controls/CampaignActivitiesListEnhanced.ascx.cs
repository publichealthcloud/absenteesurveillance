using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;

public partial class manage_site_learning_camapign_activities_enhanced : System.Web.UI.UserControl
{
    protected int campaign_id, user_id;
    protected string display_mode, status;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }

    public int UserID
    {
        get { return user_id; }
        set { user_id = value; }
    }

    public string DisplayMode
    {
        get { return display_mode; }
        set { display_mode = value; }
    }

    public string Status
    {
        get { return status; }
        set { status = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int curr_campaign_id = 0;

            if (campaign_id > 0)
                curr_campaign_id = campaign_id;

            if (campaign_id == 0)
                curr_campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);

            if (curr_campaign_id > 0)
                Session["CampaignID"] = curr_campaign_id;

            if (curr_campaign_id == 0)
                curr_campaign_id = Convert.ToInt32(Session["CampaignID"]);

            int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
            user_id = Convert.ToInt32(Context.Items["UserID"]);

            var actions = qSoc_CampaignAction.GetCampaignActions(campaign_id);

            string curr_url = Request.Url.ToString();
            if (!curr_url.Contains("/manage/"))
            {
                var u_campaign = qSoc_UserCampaign_View.GetUserCampaign(curr_user_id, curr_campaign_id);
                int day_in_campaign = 0;
                if (!String.IsNullOrEmpty(Convert.ToString(u_campaign.DaysInCampaign)))
                    day_in_campaign = u_campaign.DaysInCampaign;
            }

            bool available_actions = false;
            if (actions != null)
            {
                if (actions.Count > 0)
                {
                    available_actions = true;
                    int i = 1;
                    foreach (var a in actions)
                    {
                        Literal action = new Literal();

                        int curr_feed_id = 0;
                        string keyword_title = string.Empty;
                        string activity_type = string.Empty;
                        string delivery_format = string.Empty;
                        string schedule = string.Empty;
                        if (a.FeedID == 0)
                        {
                            keyword_title = a.ActionName;
                            qSoc_ContentType c_type = new qSoc_ContentType(a.ContentTypeID);
                            activity_type = c_type.Name.ToLower();
                        }
                        else
                        {
                            qSoc_Feed feed = new qSoc_Feed(a.FeedID);
                            keyword_title = feed.Title.Replace("&#39;", "");
                            curr_feed_id = feed.FeedID;
                            activity_type = feed.Type;
                        }
                        if (!String.IsNullOrEmpty(keyword_title))
                        {
                            keyword_title = Uri.EscapeDataString(keyword_title).Replace("'", @"\'").Replace(@"""", @"\""");
                            keyword_title = keyword_title.Replace("%2C", "");
                        }
                        else
                        {
                            keyword_title = "Activity";
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(a.EmailID)))
                        {
                            if (a.EmailID > 0)
                                delivery_format += "<button class=\"btn\" rel=\"tooltip\" title=\"Email\"><i class=\"glyphicon-message_flag\"></i></button>&nbsp;";
                        }
                        if (!String.IsNullOrEmpty(Convert.ToString(a.NotificationID)))
                        {
                            if (a.NotificationID > 0)
                                delivery_format += "<button class=\"btn\" rel=\"tooltip\" title=\"Mobile Notification\"><i class=\"glyphicon-iphone\"></i></button>&nbsp;";
                        }
                        if (!String.IsNullOrEmpty(Convert.ToString(a.SMSMessageID)))
                        {
                            if (a.SMSMessageID > 0)
                                delivery_format += "<button class=\"btn\" rel=\"tooltip\" title=\"Text Message (SMS)\"><i class=\"glyphicon-phone\"></i></button>";
                        }
                        if (!String.IsNullOrEmpty(Convert.ToString(a.DayAvailableInCampaign)))
                            schedule = "Day " + a.DayAvailableInCampaign;
                        if (!String.IsNullOrEmpty(Convert.ToString(a.TimeAvailableInCampaign)))
                            schedule += " at " + a.TimeAvailableInCampaign;

                        string action_html = "<tr id=\"tr-" + a.CampaignActionID + "\">";

                        action_html += "<td><font color=\"gray\">" + i + ") " + a.ActionName + "</font></td>";
                        action_html += "<td class=\"hidden-1024\">" + a.ActionType + "</td>";
                        action_html += "<td class=\"hidden-1024\">" + delivery_format + "</td>";
                        action_html += "<td class=\"hidden-480\">" + a.Points + "</td>";
                        action_html += "<td class=\"hidden-480\">" + schedule + "</td>";
                        action_html += "<td class=\"hidden-480\">";
                        action_html += "<a href=\"/manage/site/learning/campaign-activity-edit.aspx?campaignID=" + a.CampaignID + "&campaignActionID=" + a.CampaignActionID + "\" class=\"btn btn-primary\" rel=\"tooltip\" title=\"Edit Activity\"><i class=\"icon-pencil\"></i></a></i>&nbsp;&nbsp;";
                        action_html += "<a href=\"#\" onclick=\"RemoveCampaignActivity('"+a.CampaignActionID+"', '"+ a.CampaignID +"'); return false;\" class=\"btn btn-danger\" rel=\"tooltip\" title=\"Remove Activity\"><i class=\"icon-remove\"></i></a></i>";
                        action_html += "</td>";
                        action_html += "</tr>";

                        action.Text = action_html;

                        pnlActivities.Controls.Add(action);
                        i++;
                    }
                }
            }

            // if no actions then add empty table
            if (available_actions == false)
            {
                Literal no_action = new Literal();
                no_action.Text = "<tr><td>No activities available</td><td></td><td></td><td></td></tr>";
                pnlActivities.Controls.Add(no_action);
            }
        }
    }
}