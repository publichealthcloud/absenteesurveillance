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
    public int campaign_id, campaign_action_id;
    public int user_id;
    public string username;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            campaign_action_id = Convert.ToInt32(Request.QueryString["campaignActionID"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateEmails(campaign_id);
            populateNotifications(campaign_id);
            populateSMSMessages(campaign_id);
            
            hplBackTop.NavigateUrl = "campaign-manage-activities.aspx?campaignID=" + campaign_id;
            hplBackBottom.NavigateUrl = "campaign-manage-activities.aspx?campaignID=" + campaign_id;

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();

            qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
            if (campaign.Mobile == "Yes")
            {
                plhNotifications.Visible = true;
            }
            else
                plhNotifications.Visible = false;
            if (campaign.SMS == "Yes")
            {
                plhTextMessages.Visible = true;
            }
            else
                plhTextMessages.Visible = false;
            if (campaign.Email == "Yes")
            {
                plhEmails.Visible = true;
            }
            else
                plhEmails.Visible = false;

            if (!String.IsNullOrEmpty(Request.QueryString["campaignActionID"]))
            {
                campaign_action_id = Convert.ToInt32(Request.QueryString["campaignActionID"]);
                qSoc_CampaignAction action = new qSoc_CampaignAction(campaign_action_id);
                qSoc_ContentType type = new qSoc_ContentType(action.ContentTypeID);
                lblTypeInformation.Text = type.Name;
                lblTitle.Text = action.ActionName + " [" + type.Name + "] - Edit";
                txtActivityName.Text = action.ActionName;
                txtPoints.Text = Convert.ToString(action.Points);
                plhSelectType.Visible = false;
                plhDisplayType.Visible = true;
                btnSave.Visible = true;
                btnSave_top.Visible = true;
                ddlEmails.SelectedValue = Convert.ToString(action.EmailID);
                ddlTextMessages.SelectedValue = Convert.ToString(action.SMSMessageID);
                ddlNotifications.SelectedValue = Convert.ToString(action.NotificationID);
                ddlTimeOfDay.SelectedValue = Convert.ToString(action.TimeAvailableInCampaign);
                txtDayInCampaign.Text = Convert.ToString(action.DayAvailableInCampaign);
            }
            else
            {
                lblTitle.Text = "Create New Campaign Activity";
                plhSelectType.Visible = true;
                plhDisplayType.Visible = false;
                btnSave.Visible = false;
                btnSave_top.Visible = false;
            }
        }
    }

    protected void populateEmails(int campaign_id)
    {
        ddlEmails.DataSource = qCom_EmailItem.GetEmailItemsByCampaign(campaign_id);
        ddlEmails.DataTextField = "Subject";
        ddlEmails.DataValueField = "EmailID";
        ddlEmails.DataBind();
        ddlEmails.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateNotifications(int campaign_id)
    {
        ddlNotifications.DataSource = qPtl_Notification.GetNotificationsByCampaign(campaign_id);
        ddlNotifications.DataTextField = "Title";
        ddlNotifications.DataValueField = "NotificationID";
        ddlNotifications.DataBind();
        ddlNotifications.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateSMSMessages(int campaign_id)
    {
        ddlTextMessages.DataSource = qCom_SMSMessage.GetSMSMessagesByCampaignID(campaign_id);
        ddlTextMessages.DataTextField = "MessageURI";
        ddlTextMessages.DataValueField = "SMSMessageID";
        ddlTextMessages.DataBind();
        ddlTextMessages.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"]);
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["campaignActionID"]))
            {
                int campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);

                // update activity information -- name, points
                qSoc_CampaignAction action = new qSoc_CampaignAction(Convert.ToInt32(Request.QueryString["campaignActionID"]));
                action.ActionName = txtActivityName.Text;
                action.Points = Convert.ToInt32(txtPoints.Text);
                action.Available = "Yes";
                if (!String.IsNullOrEmpty(txtDayInCampaign.Text))
                    action.DayAvailableInCampaign = Convert.ToInt32(txtDayInCampaign.Text);
                if (!String.IsNullOrEmpty(ddlTimeOfDay.SelectedValue))
                    action.TimeAvailableInCampaign = Convert.ToString(ddlTimeOfDay.SelectedValue);
                if (!String.IsNullOrEmpty(ddlEmails.SelectedValue))
                    action.EmailID = Convert.ToInt32(ddlEmails.SelectedValue);
                if (!String.IsNullOrEmpty(ddlTextMessages.SelectedValue))
                    action.SMSMessageID = Convert.ToInt32(ddlTextMessages.SelectedValue);
                if (!String.IsNullOrEmpty(ddlNotifications.SelectedValue))
                    action.NotificationID = Convert.ToInt32(ddlNotifications.SelectedValue);
                action.Update();

                // check to see if there are any existing campaign enrollments, if so -- must add this action for all existing enrolled members
                // get values
                var members = qSoc_UserCampaign_View.GetCampaignUsers(campaign_id);
                var all_actions = qSoc_CampaignAction.GetCampaignActions(campaign_id);
                int actions_count = 1;

                if (all_actions != null)
                {
                    actions_count = all_actions.Count;
                }

                if (members != null)
                {
                    foreach (var m in members)
                    {
                        // see if this member already has this action
                        qSoc_UserCampaignAction u_action = new qSoc_UserCampaignAction(m.UserID, action.CampaignActionID);

                        if (u_action.UserCampaignActionID > 0)
                        {
                            // update existing action
                            u_action.DayAvailable = action.DayAvailableInCampaign;
                            u_action.Update();
                        }
                        else
                        {
                            // create new action
                            qSoc_UserCampaignAction cUserAction = new qSoc_UserCampaignAction();

                            cUserAction.UserID = user_id;
                            cUserAction.ScopeID = 1;
                            cUserAction.Available = "Yes";
                            cUserAction.Created = DateTime.Now;
                            cUserAction.CreatedBy = 0;
                            cUserAction.CampaignActionID = action.CampaignActionID;
                            cUserAction.DayAvailable = action.DayAvailableInCampaign;
                            if (m.CurrentCampaignActionID == 0)
                            {
                                cUserAction.Status = "In Progress";
                                cUserAction.LastModified = DateTime.Now;
                                m.CurrentCampaignActionID = action.CampaignActionID;
                            }
                            else
                            {
                                cUserAction.Status = "Not Started";
                            }

                            cUserAction.Insert();
                        }
                    }
                }

                Response.Redirect("~/manage/site/learning/campaign-manage-activities.aspx?campaignID=" + campaign_id);            
            }
        }
    }

    protected void btnSelectActivity_Click(object sender, EventArgs e)
    {
        // create new campaign action and write item information into the record
        int feed_id = Convert.ToInt32(ddlFeedItems.SelectedValue);
        if (feed_id > 0)
        {
            qSoc_CampaignAction action = new qSoc_CampaignAction();
            action.CampaignID = Convert.ToInt32(Convert.ToInt32(Request.QueryString["campaignID"]));
            qSoc_Feed feed = new qSoc_Feed(feed_id);
            qSoc_ContentType type = new qSoc_ContentType(feed.ContentTypeID);
            var existing_actions = qSoc_CampaignAction.GetCampaignActions(Convert.ToInt32(Request.QueryString["campaignID"]));
            int action_order = 1;
            if (existing_actions != null)
                action_order = existing_actions.Count + 1;
            action.ActionOrder = action_order;
            action.CampaignID = Convert.ToInt32(Request.QueryString["campaignID"]);
            action.ContentTypeID = feed.ContentTypeID;
            action.ReferenceID = feed.ReferenceID;
            action.FeedID = feed.FeedID;
            action.LogActionID = type.LogActionID;
            action.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            action.Created = DateTime.Now;
            action.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
            action.LastModified = DateTime.Now;
            action.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            action.Available = "No";
            action.MarkAsDelete = 0;
            action.ActionName = feed.Title;
            action.ActionDescription = feed.Title;
            action.Required = "Yes";
            action.ActionType = type.ActionDescription;
            action.Insert();

            qSoc_Campaign campaign = new qSoc_Campaign(Convert.ToInt32(Request.QueryString["campaignID"]));
            campaign.NumUserActivities = existing_actions.Count + 1;
            campaign.Update();

            Response.Redirect("~/manage/site/learning/campaign-activity-edit.aspx?campaignID=" + Request.QueryString["campaignID"] + "&campaignActionID=" + action.CampaignActionID);
        }
    }

    protected void ddlContentTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        // get all feed available feed items which match this content type - must be either posted by main site or host or be visibe in the explore or topics
        // avoid loading items which are only from users
        if (Convert.ToInt32(ddlContentTypes.SelectedValue) > 0)
        {
            // get all appropriate feed items
            populateFeedItems(Convert.ToInt32(ddlContentTypes.SelectedValue));
        }
        else
        {
            lblMessage.Text = "*** You must select a type of activity ***";
        }
    }

    protected void populateFeedItems(int content_type_id)
    {
        if (content_type_id == (int)qSoc_ContentType.Types.Shout)
            ddlFeedItems.DataSource = qSoc_Feed.GetFeedItemsForCampaigns(content_type_id, true);
        else
            ddlFeedItems.DataSource = qSoc_Feed.GetFeedItemsForCampaigns(content_type_id, false);
        ddlFeedItems.DataTextField = "Title";
        ddlFeedItems.DataValueField = "FeedID";
        ddlFeedItems.DataBind();
        ddlFeedItems.Items.Insert(0, new ListItem("", string.Empty));

        ddlFeedItems.Enabled = true;
    }

    protected void ddlFeedItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        // display the select button
        btnSelectActivity.Visible = true;
    }
}
