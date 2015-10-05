using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;
using Quartz.Communication;

public partial class text_messages_message_editor : System.Web.UI.UserControl
{
    public string final_text;    

    protected void Page_Load(object sender, EventArgs e)
    {
        modalPopup.Modal = true;
        
        if (!Page.IsPostBack)
        {
            ddlPrograms.DataSource = qSoc_Campaign.GetGenerallyAvailableCampaigns();
            ddlPrograms.DataTextField = "CampaignName";
            ddlPrograms.DataValueField = "CampaignID";
            ddlPrograms.DataBind();
        }
    }

    protected void btnSaveMessage_Click(object sender, EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            final_text = txtMessage.Text;
            final_text = final_text.Replace("’", "'");
            final_text = final_text.Replace("“", "\"");
            final_text = final_text.Replace("”", "\"");
            plhSavedMessage.Visible = true;
            int char_count = Convert.ToInt32(final_text.Count());
            lblSavedMessage.Text = "<strong>* Message Successfully Saved *</strong><br><br><strong>Program: </strong> " + ddlPrograms.SelectedValue + "<br><strong>Title</strong> " + txtMessageURI.Text + "<br><strong>Saved Message:</strong> " + final_text;
            txtMessage.Text = "";
            txtMessageURI.Text = "";
            //ddlPrograms.DefaultMessage = "Select a program...";
            plhMessage.Visible = false;
            lblMessageToSend.Text = final_text;
        }
    }

    protected void btnNewMessage_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void radSendTestMessage_Click(object sender, EventArgs e)
    {
        int currentUserID = Convert.ToInt32(Context.Items["UserID"]);

        qPtl_User user = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));
        qPtl_UserProfile profile = new qPtl_UserProfile(Convert.ToInt32(Request.QueryString["userID"]));

        string userMobile = string.Empty;
        string smsMessage = string.Empty;
        string smsMessageURI = string.Empty;

        userMobile = txtMobileNumber.Text;

        // create object
        string smsMode = System.Configuration.ConfigurationManager.AppSettings["SMSMode"];
        Guid authToken = new Guid(System.Configuration.ConfigurationManager.AppSettings["SMSLicenseKey"]);
        Quartz.Communication.qCom_SMSMessageLog sendLong = new Quartz.Communication.qCom_SMSMessageLog();

        // send message + add log
        if (smsMode == "did")
        {
            // use custom number + key
            string did = System.Configuration.ConfigurationManager.AppSettings["SMSDid"];
            Quartz.Communication.CDYNE.SMSResponse[] response = Quartz.Communication.qCom_SMSMessage.SendSMSAdvancedSendMessage(userMobile, smsMessage, authToken, did, 0, false);
            sendLong.Insert(1, DateTime.Now, user.UserID, 0, smsMessageURI, userMobile, did, smsMessage, "sent", Convert.ToString(response[0].MessageID), 0, 0, "", false, false, 0, false, false, 0, string.Empty);
        }

        lblMessage.Text += "Finished sending text message to " + userMobile + "<br>";

        lblMessage.Visible = true;
    }
}