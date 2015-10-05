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

public partial class manage_communications_email_edit : System.Web.UI.Page
{
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_EmailFolder"]);
    public int email_id;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int language_id = 1;            
        int emailID = (string.IsNullOrEmpty(Request.QueryString["emailID"])) ? 0 : Convert.ToInt32(Request.QueryString["emailID"]);
        int curr_email_id;

        var versions = qPtl_HTMLRevision.GetRevisions(emailID, "email", language_id);

        cmbRevisions.DataSource = versions;
        cmbRevisions.DataTextField = "VersionInfo";
        cmbRevisions.DataValueField = "HTMLRevisionID";
        cmbRevisions.DataBind();
        
        if (!Page.IsPostBack)
        {
            populateCampaigns();
            populateLanguages();
            
            hplDuplicate.NavigateUrl = "email-edit.aspx?action=duplicate&emailID=" + emailID;
            hplTestSend.NavigateUrl = "test-send.aspx?emailID=" + emailID;

            //duplicate information and then 
            if (emailID > 0 && Convert.ToString(Request.QueryString["action"]) == "duplicate")
            {
                curr_email_id = CreateNewEmail("duplicate", emailID);
                Response.Redirect("email-edit.aspx?emailID=" + curr_email_id);
            }
            else
            {
                curr_email_id = emailID;
            }

            if (Convert.ToString(Request.QueryString["message"]) == "duplicate-successful")
            {
                lblMessage.Text = "*** Email Successfully Duplicated ***";
            }

            if (curr_email_id > 0)
            {
                qCom_EmailItem email = new qCom_EmailItem(Convert.ToInt32(Request.QueryString["emailID"]));
                txtURI.Text = email.URI;
                ddlLanguages.SelectedValue = Convert.ToString(email.LanguageID);
                ddlCampaigns.SelectedValue = Convert.ToString(email.CampaignID);
                ddlEmailType.SelectedValue = Convert.ToString(email.Type);
                txtSubject.Text = email.Subject;
                reContent.Content = email.Draft;

                lblTitle.Text = "Edit Email [ID: " + email.EmailID + " ]";
            }
            else
                lblTitle.Text = "Create Email";
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
            int language_id = 1;
            int emailID = (string.IsNullOrEmpty(Request.QueryString["emailID"])) ? 0 : Convert.ToInt32(Request.QueryString["emailID"]);
            int curr_email_id = 0;
            int scopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            int user_id = Convert.ToInt32(Context.Items["UserID"]);
            var last_version_number = qPtl_HTMLRevision.GetLastVersionNumber(emailID, "email", language_id);
            qPtl_User user = new qPtl_User(user_id);

            if (emailID > 0)
            {
                lblTitle.Text = "Edit Email";
                qCom_EmailItem email = new qCom_EmailItem(emailID);
                email.URI = txtURI.Text;
                email.Subject = txtSubject.Text;
                if (!String.IsNullOrEmpty(Convert.ToString(ddlEmailType.SelectedValue)))
                    email.Type = Convert.ToString(ddlEmailType.SelectedValue);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlLanguages.SelectedValue)))
                    email.LanguageID = Convert.ToInt32(ddlLanguages.SelectedValue);
                if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaigns.SelectedValue)))
                    email.CampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
                email.Draft = reContent.Content;
                email.Update();

                lblMessage.Text = "*** Email Successfully Saved at " + DateTime.Now + " ***";

                curr_email_id = emailID;

                //Response.Redirect("~/qCom/email-list.aspx");
            }
            else
            {
                int new_email_id = CreateNewEmail("new", -1);

                lblMessage.Text = "*** Email Successfully Created at " + DateTime.Now + " ***";

                curr_email_id = new_email_id;
                //Response.Redirect("~/qCom/email-list.aspx");
            }

            qPtl_HTMLRevision revision = new qPtl_HTMLRevision();
            revision.Available = "Yes";
            revision.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            revision.Created = DateTime.Now;
            revision.CreatedBy = user_id;
            revision.LastModified = DateTime.Now;
            revision.LastModifiedBy = user_id;
            revision.MarkAsDelete = 0;
            revision.ModuleInstanceID = curr_email_id;
            revision.SourceType = "email";
            revision.HTML = reContent.Content;
            revision.VersionNumber = last_version_number + 1;
            revision.VersionInfo = "Version: " + revision.VersionNumber + " saved by " + user.FirstName + " " + user.LastName + " [" + user.UserName + "] at " + DateTime.Now;
            revision.LanguageID = language_id;

            revision.Insert();
        }
    }

    protected int CreateNewEmail(string action, int existing_email_id)
    {
        string body = string.Empty;
        string subject = string.Empty;
        int language_id = 0;
        string type = string.Empty;
        string email_uri = string.Empty;

        if (action == "duplicate")
        {
            qCom_EmailItem email = new qCom_EmailItem(Convert.ToInt32(Request.QueryString["EmailID"]));
            
            if (email != null)
            {
                body = email.Draft;
                subject = "DUPLICATE - " + email.Subject;
                language_id = email.LanguageID;
                type = email.Type;
                email_uri = "DUPLICATE - " + email.URI;
            }
        }
        else
        {
            body = reContent.Content;
            subject = txtSubject.Text;
            email_uri = txtURI.Text;
            if (!String.IsNullOrEmpty(Convert.ToString(ddlEmailType.SelectedValue)))
                type = Convert.ToString(ddlEmailType.SelectedValue);
            if (!String.IsNullOrEmpty(Convert.ToString(ddlLanguages.SelectedValue)))
                language_id = Convert.ToInt32(ddlLanguages.SelectedValue);
        }

        qCom_EmailItem new_email = new qCom_EmailItem();
        new_email.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
        new_email.Available = "Yes";
        new_email.Created = DateTime.Now;
        new_email.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
        new_email.LastModified = DateTime.Now;
        new_email.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        new_email.MarkAsDelete = 0;
        new_email.Subject = subject;
        new_email.Draft = body;
        new_email.URI = email_uri;
        new_email.Type = type;
        new_email.LanguageID = language_id;
        if (!String.IsNullOrEmpty(Convert.ToString(ddlCampaigns.SelectedValue)))
            new_email.CampaignID = Convert.ToInt32(ddlCampaigns.SelectedValue);
        new_email.Insert();

        return new_email.EmailID;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        qCom_EmailItem email = new qCom_EmailItem(Convert.ToInt32(Request.QueryString["emailID"]));
        email.Available = "No";
        email.MarkAsDelete = 1;
        email.Update();

        Response.Redirect("emails-list.aspx");
    }

    protected void btnLoadRevision_Click(object sender, EventArgs args)
    {
        int html_revision_id;

        if (int.TryParse(cmbRevisions.SelectedValue, out html_revision_id))
        {
            var html_revision = new qPtl_HTMLRevision(html_revision_id);

            reContent.Content = html_revision.HTML;

            cmbRevisions.ClearSelection();
            cmbRevisions.Text = string.Empty;

            lblMessage.Text = string.Empty;
        }
    }
}