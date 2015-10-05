using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Quartz.Data;
using Quartz.Communication;
using Quartz.Portal;
using Quartz.Social;

public partial class manage_communications_sms_message_edit : System.Web.UI.Page
{
    public int sms_message_id;
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {          
        int sms_message_id = (string.IsNullOrEmpty(Request.QueryString["smsMessageID"])) ? 0 : Convert.ToInt32(Request.QueryString["smsMessageID"]);
        
        if (!Page.IsPostBack)
        {
            populateCampaigns();
            populateLanguages();

            hplBackToMessages.NavigateUrl = "sms-messages-list.aspx";
            hplBackBottom.NavigateUrl = "sms-messages-list.aspx";
            if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["campaignID"])))
            {
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                if (campaign_id > 0)
                {
                    hplBackToMessages.NavigateUrl = "sms-messages-list.aspx?campaignID=" + campaign_id;
                    hplBackBottom.NavigateUrl = "sms-messages-list.aspx?campaignID=" + campaign_id;
                }
            }

            if (sms_message_id > 0)
            {
                qCom_SMSMessage message = new qCom_SMSMessage(sms_message_id);
                txtURI.Text = message.MessageURI;
                ddlLanguages.SelectedValue = Convert.ToString(message.LanguageID);
                ddlCampaigns.SelectedValue = Convert.ToString(message.CampaignID);
                txtMessage.Text = message.MessageText;
                txtDayInCampaign.Text = Convert.ToString(message.DayInCampaign);

                lblTitle.Text = "Edit Text Message [ID: " + message.SMSMessageID + " ]";
            }
            else
            {
                lblTitle.Text = "Create Text Message";
                plhTools.Visible = false;
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

    protected void populateLanguages()
    {
        ddlLanguages.DataSource = qPtl_Language.GetLanguages();
        ddlLanguages.DataTextField = "DisplayName";
        ddlLanguages.DataValueField = "LanguageID";
        ddlLanguages.DataBind();
        ddlLanguages.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int sms_message_id = (string.IsNullOrEmpty(Request.QueryString["smsMessageID"])) ? 0 : Convert.ToInt32(Request.QueryString["smsMessageID"]);
            int curr_message_id = 0;
            int scopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (sms_message_id > 0)
            {
                lblTitle.Text = "Edit Message";

                qCom_SMSMessage message = new qCom_SMSMessage(sms_message_id);
                message.LastModified = DateTime.Now;
                message.LastModifiedby = user_id;
                message.MarkAsDelete = 0;
                message.MessageURI = txtURI.Text;
                message.MessageText = txtMessage.Text;
                if (!String.IsNullOrEmpty(ddlCampaigns.SelectedValue))
                    message.CampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
                if (!String.IsNullOrEmpty(ddlLanguages.SelectedValue))
                    message.LanguageID = Convert.ToInt32(ddlLanguages.SelectedValue);
                message.DayInCampaign = Convert.ToInt32(txtDayInCampaign.Text);
                message.Update();  

                lblMessage.Text = "*** Message Successfully Saved at " + DateTime.Now + " ***";
            }
            else
            {
                qCom_SMSMessage message = new qCom_SMSMessage();
                message.ScopeID = scopeID;
                message.Available = "Yes";
                message.Created = DateTime.Now;
                message.CreatedBy = user_id;
                message.LastModified = DateTime.Now;
                message.LastModifiedby = user_id;
                message.MarkAsDelete = 0;
                message.MessageURI = txtURI.Text;
                message.MessageText = txtMessage.Text;
                if (!String.IsNullOrEmpty(ddlCampaigns.SelectedValue))
                    message.CampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
                if (!String.IsNullOrEmpty(ddlLanguages.SelectedValue))
                    message.LanguageID = Convert.ToInt32(ddlLanguages.SelectedValue);
                message.DayInCampaign = Convert.ToInt32(txtDayInCampaign.Text);
                message.Insert();

                curr_message_id = message.SMSMessageID;

                if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
                    Response.Redirect("sms-message-edit.aspx?smsMessageID=" + curr_message_id + "&campaignID=" + Request.QueryString["campaignID"]);
                else
                    Response.Redirect("sms-message-edit.aspx?smsMessageID=" + curr_message_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // make sure that the message is not used in any active rules
        qCom_SMSMessage message = new qCom_SMSMessage(Convert.ToInt32(Request.QueryString["smsMessageID"]));

        var rules = qCom_SMSMessageRule.GetMessageRuleByAnySMSMessageURI(message.MessageURI);

        if (rules.SMSMessageRuleID > 0)
        {
            lblMessageBottom.Text = "*** WARNING: This message cannot be deleted since it is being used in active rules. ***";
        }
        else
        {
            message.Available = "No";
            message.MarkAsDelete = 1;
            message.Update();

            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
                Response.Redirect("sms-messages-list.aspx?campaignID=" + Request.QueryString["campaignID"]);
            else
                Response.Redirect("sms-messages-list.aspx");
        }
    }
}