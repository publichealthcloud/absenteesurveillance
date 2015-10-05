using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Communication;
using Quartz.Portal;

public partial class manage_communications_messaging_send_campaign_sms : System.Web.UI.Page
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (campaign_id > 0)
            {
                CampaignAllEnrolledScrolling.CampaignID = campaign_id;
                CampaignAllEnrolledScrolling.SMSOk = true;

                qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);

                if (!String.IsNullOrEmpty(Request.QueryString["userID"]))
                {
                    int user_id = Convert.ToInt32(Request.QueryString["userID"]);
                    qPtl_User user = new qPtl_User(user_id);
                    litRecipientList.Text = "This message will be sent to a single enrolled member of: <strong>" + campaign.CampaignName + "</strong><br><br><strong>Campaign Member</strong><br>Username = " + user.UserName;
                    CampaignAllEnrolledScrolling.Visible = false;
                    plhLimitByDays.Visible = false;
                }
                else
                {
                    litRecipientList.Text = "This message will be sent to all enrolled members of: <strong>" + campaign.CampaignName + "</strong><br><strong>Campaign Members</strong>";
                    plhLimitByDays.Visible = true;
                }

                populateCampaignMessages(campaign_id);
            }
        }
    }

    protected void populateCampaignMessages(int campaign_id)
    {
        var messages = qCom_SMSMessage.GetSMSMessagesByCampaignID(campaign_id);

        if (messages != null)
        {
            foreach (var m in messages)
            {
                if (m.LanguageID == 1)
                    m.MessageURI = m.MessageURI + " (English):" + " " + m.MessageText;
                else
                    m.MessageURI = m.MessageURI + " (Spanish):" + " " + m.MessageText;
            }
        }
        
        ddlCampaignMessages.DataSource = messages;
        ddlCampaignMessages.DataTextField = "MessageURI";
        ddlCampaignMessages.DataValueField = "SMSMessageID";
        ddlCampaignMessages.DataBind();
        ddlCampaignMessages.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void ddlCampaignMessages_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlCampaignMessages.SelectedValue));
        {
            int sms_message_id = Convert.ToInt32(ddlCampaignMessages.SelectedValue);

            qCom_SMSMessage message = new qCom_SMSMessage(sms_message_id);

            if (message.SMSMessageID > 0)
            {
                litMessageText.Text = "<br><strong>This message text is:</strong> <pre>" + message.MessageText + "</pre>";
                btnSendConfirm.Visible = true;
            }
            else
                btnSendConfirm.Visible = false;
        }
    }

    protected void btnSendConfirm_Click(object sender, EventArgs e)
    {
        // see if there are alternate language versions of this same message URI
        if (!string.IsNullOrEmpty(ddlCampaignMessages.SelectedValue))
        {
            // output variables
            string output_message = string.Empty;
            int num_messages_sent = 0;
            int limit_day_in_campaign_min = 0;
            int limit_day_in_campaign_max = 0;

            limit_day_in_campaign_min = Convert.ToInt32(txtMinDays.Text);
            limit_day_in_campaign_max = Convert.ToInt32(txtMaxDays.Text);
            
            int sms_message_id = Convert.ToInt32(ddlCampaignMessages.SelectedValue);

            qCom_SMSMessage init_message = new qCom_SMSMessage(sms_message_id);

            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);

            bool adjust_times = false;
            if (chkAlignSendTimes.Checked == true)
                adjust_times = true;

            // SEND TO INDIVIDUAL OR ALL ENROLLED MEMBERS
            if (!String.IsNullOrEmpty(Request.QueryString["userID"]))
            {
                // send to single person in the campaign
                int user_id = Convert.ToInt32(Request.QueryString["userID"]);
                qPtl_User user = new qPtl_User(user_id);
                
                if (user.UserID > 0)
                {
                    var log = SMSMessageFunctions.SendTextMessageToUser(user_id, init_message, campaign_id, adjust_times);

                    if (log != null)
                    {
                        if (!String.IsNullOrEmpty(log.ReferenceMessageID))
                        {
                            num_messages_sent++;
                            output_message += "<br>Member mobile number: " + log.MobilePhoneNumber + " successfully sent message";
                        }
                    }
                }
            }
            else if (limit_day_in_campaign_min != limit_day_in_campaign_max)
            {
                // send only to members between specific dates
                var users = qSoc_UserCampaign_View.GetCampaignUsersBetweenDaysInCampaign(campaign_id, limit_day_in_campaign_min, limit_day_in_campaign_max);

                if (users != null)
                {
                    foreach (var u in users)
                    {
                        var log = SMSMessageFunctions.SendTextMessageToUser(u.UserID, init_message, campaign_id, adjust_times);

                        if (log != null)
                        {
                            if (!String.IsNullOrEmpty(log.ReferenceMessageID))
                            {
                                num_messages_sent++;
                                output_message += "<br>Member mobile number: " + log.MobilePhoneNumber + " successfully sent message";
                            }
                        }
                    }
                }
            }
            else
            {
                // send to everyone in the campaign
                var users = qSoc_UserCampaign_View.GetCampaignUsers(campaign_id);

                if (users != null)
                {
                    foreach (var u in users)
                    {
                        var log = SMSMessageFunctions.SendTextMessageToUser(u.UserID, init_message, campaign_id, adjust_times);

                        if (log != null)
                        {
                            if (!String.IsNullOrEmpty(log.ReferenceMessageID))
                            {
                                num_messages_sent++;
                                output_message += "<br>Member mobile number: " + log.MobilePhoneNumber + " successfully sent message";
                            }
                        }
                    }
                }
            }

            btnSendConfirm.Visible = false;
            hlpRefresh.Visible = true;
            hlpRefresh.NavigateUrl = Request.Url.ToString();
            litOutputMessage.Text = "<br><br><div style=\"height:300px; width:500px; overflow-y:auto\"><span class=\"validation2\"><strong>" + num_messages_sent + "</strong> messages successfully sent.<br><br><strong>SEND LOG</strong><br>" + output_message + "</span></div>";
        }
    }
}