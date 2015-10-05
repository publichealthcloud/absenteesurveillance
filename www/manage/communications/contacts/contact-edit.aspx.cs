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
using Quartz.Core;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.Communication;

public partial class contact_tip : System.Web.UI.Page
{
    public int contact_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["contactID"]))
            {
                populateCampaigns();
                
                contact_id = Convert.ToInt32(Request.QueryString["contactID"]);
                ViewState.Add("vsContactID", contact_id);

                qCom_Contact contact = new qCom_Contact(contact_id);

                lblTitle.Text = "Edit Contact (ID: " + contact.ContactID + ")";
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtEmail.Text = contact.Email;
                txtKeywords.Text = contact.Keywords;
                txtSource.Text = contact.Source;
                rblOkEmail.SelectedValue = contact.OKEmail;
                txtPartner.Text = contact.Partner;
                txtMainGroup.Text = Convert.ToString(contact.MainGroup);
                txtSubGroup.Text = Convert.ToString(contact.SubGroup);
                rblAvailable.SelectedValue = contact.Available;
                txtCustomHTMLElement.Text = contact.CustomHTMLElement;
                if (!String.IsNullOrEmpty(Convert.ToString(contact.Unsubscribed)))
                    rdtUnsubscribed.SelectedDate = Convert.ToDateTime(contact.Unsubscribed);
                if (!String.IsNullOrEmpty(Convert.ToString(contact.UnsubscribedCampaignID)))
                {
                    qSoc_Campaign campaign = new qSoc_Campaign(contact.UnsubscribedCampaignID);
                    ddlCampaigns.SelectedValue = Convert.ToString(contact.UnsubscribedCampaignID);

                    if (campaign.CampaignID > 0)
                    {
                        litUnsubscribedCampaignInfo.Text = "Unsubscribed from campaign: " + campaign.CampaignName;
                    }
                }
                if (!String.IsNullOrEmpty(Convert.ToString(contact.ReportedAsSpam)))
                    rdtReportedAsSpam.SelectedDate = Convert.ToDateTime(contact.ReportedAsSpam);
                if (!String.IsNullOrEmpty(Convert.ToString(contact.ReportedAsSpam)))
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(contact.ReportedAsSpamCampaignID)))
                    {
                        qSoc_Campaign campaign = new qSoc_Campaign(contact.ReportedAsSpamCampaignID);
                        ddlCampaignsSPAM.SelectedValue = Convert.ToString(contact.ReportedAsSpamCampaignID);

                        if (campaign.CampaignID > 0)
                        {
                            litReportedAsSPAMCampaignInfo.Text = "Reported as SPAM from campaign <strong>" + campaign.CampaignName + "</strong> at <strong>" + contact.ReportedAsSpam + "</strong>";
                        }
                    }
                    else
                    {
                        litReportedAsSPAMCampaignInfo.Text = "Reported as SPAM at <strong>" + contact.ReportedAsSpam + "</strong>";
                    }
                }

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }
            }

            else
            {
                lblTitle.Text = "New Contact";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(contact_id)))
            contact_id = (Int32)ViewState["vsContactID"];
    }

    protected void populateCampaigns()
    {
        var campaigns = qSoc_Campaign.GetCampaigns();

        ddlCampaigns.DataSource = campaigns;
        ddlCampaigns.DataTextField = "CampaignName";
        ddlCampaigns.DataValueField = "CampaignID";
        ddlCampaigns.DataBind();
        ddlCampaigns.Items.Insert(0, new ListItem("", string.Empty));
        ddlCampaigns.Items.Insert(1, new ListItem("<All Campaigns>", string.Empty));

        ddlCampaignsSPAM.DataSource = campaigns;
        ddlCampaignsSPAM.DataTextField = "CampaignName";
        ddlCampaignsSPAM.DataValueField = "CampaignID";
        ddlCampaignsSPAM.DataBind();
        ddlCampaignsSPAM.Items.Insert(0, new ListItem("", string.Empty));
        ddlCampaignsSPAM.Items.Insert(1, new ListItem("<All Campaigns>", string.Empty));
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["contactID"]))
            {
                contact_id = Convert.ToInt32(Request.QueryString["contactID"]);
                qCom_Contact contact = new qCom_Contact(contact_id);
                contact.FirstName = txtFirstName.Text;
                contact.LastName = txtLastName.Text;
                contact.Email = txtEmail.Text;
                contact.Keywords = txtKeywords.Text;
                contact.Source = txtSource.Text;
                contact.MainGroup = Convert.ToInt32(txtMainGroup.Text); 
                contact.SubGroup = Convert.ToInt32(txtSubGroup.Text);
                contact.DID = txtDID.Text;
                contact.Partner = txtPartner.Text;
                contact.CustomHTMLElement = txtCustomHTMLElement.Text;

                if (!String.IsNullOrEmpty(Convert.ToString(rdtUnsubscribed.SelectedDate)))
                    contact.Unsubscribed = Convert.ToDateTime(rdtUnsubscribed.SelectedDate);
                if (!String.IsNullOrEmpty(Convert.ToString(rdtReportedAsSpam.SelectedDate)))
                    contact.ReportedAsSpam = Convert.ToDateTime(rdtReportedAsSpam.SelectedDate);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaigns.SelectedValue)))
                    contact.UnsubscribedCampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaignsSPAM.SelectedValue)))
                    contact.ReportedAsSpamCampaignID = Convert.ToInt32(ddlCampaignsSPAM.SelectedValue);

                contact.OKEmail = rblOkEmail.SelectedValue;
                contact.Available = rblAvailable.SelectedValue;
                contact.Update();
            }
            else
            {
                qCom_Contact contact = new qCom_Contact();
                contact.FirstName = txtFirstName.Text;
                contact.LastName = txtLastName.Text;
                contact.Email = txtEmail.Text;
                contact.Keywords = txtKeywords.Text;
                contact.Source = txtSource.Text;
                contact.MainGroup = Convert.ToInt32(txtMainGroup.Text);
                contact.SubGroup = Convert.ToInt32(txtSubGroup.Text);
                contact.DID = txtDID.Text;
                contact.Partner = txtPartner.Text;
                contact.CustomHTMLElement = txtCustomHTMLElement.Text;


                if (!String.IsNullOrEmpty(Convert.ToString(rdtUnsubscribed.SelectedDate)))
                    contact.Unsubscribed = Convert.ToDateTime(rdtUnsubscribed.SelectedDate);
                if (!String.IsNullOrEmpty(Convert.ToString(rdtReportedAsSpam.SelectedDate)))
                    contact.ReportedAsSpam = Convert.ToDateTime(rdtReportedAsSpam.SelectedDate);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaigns.SelectedValue)))
                    contact.UnsubscribedCampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaignsSPAM.SelectedValue)))
                    contact.ReportedAsSpamCampaignID = Convert.ToInt32(ddlCampaignsSPAM.SelectedValue);

                contact.OKEmail = rblOkEmail.SelectedValue;
                contact.Available = rblAvailable.SelectedValue;
                contact.Insert();

                contact_id = contact.ContactID;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["contactID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("contacts-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&contactID=" + contact_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        contact_id = Convert.ToInt32(Request.QueryString["contactID"]);
        qCom_Contact contact = new qCom_Contact(contact_id);
        contact.Available = "No";
        contact.MarkAsDelete = 1;
        contact.OKEmail = "No";
        contact.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("contacts-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("contacts-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("contacts-list.aspx");
    }
}
