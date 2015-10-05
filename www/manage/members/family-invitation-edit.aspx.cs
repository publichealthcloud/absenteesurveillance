using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;
using System.Text;

using Telerik.Web.UI;

using Quartz;
using Quartz.Core;
using Quartz.Data;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.CMS;
using Quartz.Communication;
using Quartz.Organization;

public partial class edit_family_invitation : System.Web.UI.Page
{
    public int invitation_id;
    private const int ItemsPerRequest = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            // configure type pull down menu based on site preferences
            ddlInvitationAudience.Items.FindByValue("family").Enabled = false;
            ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
            string invitation_types = Convert.ToString(ConfigurationManager.AppSettings["Members_InvitationTypes"]);
            if (invitation_types.ToLower().Contains("family"))
            {
                ddlInvitationAudience.Items.FindByValue("family").Enabled = true;
                ddlInvitationAudience.Items.FindByValue("new family").Enabled = true;
            }

            invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);
            string invite_audience = Convert.ToString(Request.QueryString["audience"]);

            qPtl_Invitation invite = new qPtl_Invitation(invitation_id);

            txtAudienceName.Text = invite.InvitationAudienceName;
            lblAudienceName.Text = invite.InvitationAudienceName;
            lblAudienceName.Visible = false;
            txtModeratedGroupNameShort.Text = invite.InvitationAudienceName;
            txtInvitationCode.Text = invite.InviteCode;
            rblAvailable.SelectedValue = invite.Available;
            ddlInvitationAudience.SelectedValue = invite.InvitationAudience;
            if (invite.MaxRedemptions > 0)
                txtMaxRedemptions.Text = Convert.ToString(invite.MaxRedemptions);
            else
                txtMaxRedemptions.Text = "";
            lblCurrRedemptions.Text = Convert.ToString(invite.CurrRedemptions);
            rdtStartTime.SelectedDate = invite.StartDate;
            rdtEndTime.SelectedDate = invite.EndDate;

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
            }

            // configure print link
            string baseURL = ConfigurationManager.AppSettings["returnURL"];
            string invitation_audience = Request.QueryString["audience"];
            string rawURL = string.Empty;

            plhNumRedemptions.Visible = true;
            plhCurrRedemptions.Visible = true;

            populateFamilies();
            populateGroups();
            rawURL = baseURL + "/qDbs/print/print-family-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
            lblTitle.Text = "Edit Family Invitation (ID: " + invite.InvitationID + ")";
            // load family information
            qPtl_Family family = new qPtl_Family(invite.FamilyID);
            txtFirstName.Text = family.ContactFirstName;
            txtLastName.Text = family.ContactLastName;
            txtAddress1.Text = family.ContactAddress1;
            txtAddress2.Text = family.ContactAddress2;
            txtCity.Text = family.ContactCity;
            txtStateProvince.Text = family.ContactStateProvince;
            txtPostalCode.Text = family.ContactPostalCode;
            txtCountry.Text = family.ContactCountry;
            txtPhone1.Text = family.ContactPhone1;
            if (!String.IsNullOrEmpty(family.ContactPhone1Type))
                ddlPhone1Type.SelectedValue = family.ContactPhone1Type;
            txtPhone2.Text = family.ContactPhone2;
            if (!String.IsNullOrEmpty(family.ContactPhone2Type))
                ddlPhone2Type.SelectedValue = family.ContactPhone2Type;
            txtEmail.Text = family.ContactEmail;
            txtRelationship.Text = family.ContactRelationship;
            //ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
            ddlInvitationAudience.Items.FindByValue("").Enabled = false;
            plhExistingFamily.Visible = false;
            lblAudience.Text = "Family Name *";
            hplBackTop.NavigateUrl = "invitations-list-families.aspx";
            hplBackBottom.NavigateUrl = "invitations-list-families.aspx";
            hplBackTop.Text = "<i class=\"icon-circle-arrow-left\"></i>&nbsp;&nbsp;Back to Family Invitations";
            hplBackBottom.Text = "<i class=\"icon-circle-arrow-left\"></i>&nbsp;&nbsp;Back to Family Invitations";

            string passURL = Server.UrlEncode(rawURL);
            string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);

            hplPrint.NavigateUrl = "~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=" + invitation_audience + " invitation_id_" + invitation_id + "_" + timeStamp + ".pdf";
            hplPrint.Target = "_blank";

            plhInvitationCode.Visible = true;
            plhTools.Visible = true;
        }
        /*
        else
        {
            rblAvailable.SelectedValue = "Yes";
            if (!String.IsNullOrEmpty(Request.QueryString["audience"]))
                ddlInvitationAudience.SelectedValue = Convert.ToString(Request.QueryString["audience"]);
            plhInvitationCode.Visible = false;
            plhTools.Visible = false;
            plhCurrRedemptions.Visible = false;

            string invitation_audience = Request.QueryString["audience"];

            lblTitle.Text = "New Invitation";
            hplBackTop.NavigateUrl = "invitations-list-families.aspx";
            hplBackBottom.NavigateUrl = "invitations-list-families.aspx";
            lblAudience.Text = "Family Name *";
            plhCreateFamilyInvitationSettings.Visible = true;
            txtNumParents.Text = Convert.ToString(2);
            txtNumTeens.Text = Convert.ToString(1);
            plhNumRedemptions.Visible = false;
            populateFamilies();
            plhFamilyContactInfo.Visible = true;
            plhExistingFamily.Visible = false;
            ddlInvitationAudience.SelectedValue = "new family";
            ddlExistingGroups.Visible = false;
        }
        */
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["invitationID"]))
            {
                invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);

                qPtl_Invitation invite = new qPtl_Invitation(invitation_id);
                invite.InvitationAudience = ddlInvitationAudience.SelectedValue;
                invite.InvitationAudienceName = txtAudienceName.Text;
                invite.Available = rblAvailable.SelectedValue;
                invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                invite.LastModified = DateTime.Now;
                if (!String.IsNullOrEmpty(Convert.ToString(rdtStartTime.SelectedDate)))
                    invite.StartDate = rdtStartTime.SelectedDate;
                if (!String.IsNullOrEmpty(Convert.ToString(rdtEndTime.SelectedDate)))
                    invite.EndDate = rdtEndTime.SelectedDate;
                
                invite.Update();
            }
            else
            {
                int functional_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["register_InvitationFunctionalRoleID"]);
                int member_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["register_DefaultRoleID"]);
                int parent_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["register_ParentRoleID"]);
                int moderator_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["register_ModeratorRoleID"]);
                string invitation_type = Convert.ToString(ConfigurationManager.AppSettings["register_InvitationType"]);
                int invite_length = Convert.ToInt32(ConfigurationManager.AppSettings["register_InvitationLength"]);

                DateTime start_time = new DateTime();
                DateTime end_time = new DateTime();

                start_time = Convert.ToDateTime(rdtStartTime.SelectedDate);
                end_time = Convert.ToDateTime(rdtEndTime.SelectedDate);


                int family_id = 0;
                string audience_name = string.Empty;

                if (ddlInvitationAudience.SelectedValue == "new family")
                {
                    // create family and add information
                    qPtl_Family family = new qPtl_Family();

                    family.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    family.Available = rblAvailable.SelectedValue;
                    family.Created = DateTime.Now;
                    family.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                    family.LastModified = DateTime.Now;
                    family.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    family.MarkAsDelete = 0;
                    family.FamilyName = txtAudienceName.Text;
                    family.ContactFirstName = txtFirstName.Text;
                    family.ContactLastName = txtLastName.Text;
                    family.ContactAddress1 = txtAddress1.Text;
                    family.ContactAddress2 = txtAddress2.Text;
                    family.ContactCity = txtCity.Text;
                    family.ContactStateProvince = txtStateProvince.Text;
                    family.ContactPostalCode = txtPostalCode.Text;
                    family.ContactCountry = txtCountry.Text;
                    family.ContactPhone1 = txtPhone1.Text;
                    if (!String.IsNullOrEmpty(ddlPhone1Type.SelectedValue))
                        family.ContactPhone1Type = ddlPhone1Type.SelectedValue;
                    family.ContactPhone2 = txtPhone2.Text;
                    if (!String.IsNullOrEmpty(ddlPhone2Type.SelectedValue))
                        family.ContactPhone2Type = ddlPhone2Type.SelectedValue;
                    family.ContactEmail = txtEmail.Text;
                    family.ContactRelationship = txtRelationship.Text;

                    family.Insert();

                    family_id = family.FamilyID;
                    audience_name = txtAudienceName.Text;
                }
            }

            if (!String.IsNullOrEmpty(Request.QueryString["invitationID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                plhInvitationCode.Visible = true;
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "&mode=add-successful&invitationID=" + invitation_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);

        qPtl_Invitation invite = new qPtl_Invitation(invitation_id);
        invite.LastModified = DateTime.Now;
        invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        invite.Available = "No";
        invite.MarkAsDelete = 1;
        invite.Update();

        Response.Redirect("invitations-list-families.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string invitation_audience = Request.QueryString["audience"];

        Response.Redirect("invitations-list-families.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        string invitation_audience = Request.QueryString["audience"];

        Response.Redirect("invitations-list-families.aspx");
    }

    protected void btnPrintCustomForm_Click(object sender, EventArgs e)
    {
        string baseURL = ConfigurationManager.AppSettings["returnURL"];
        string invitation_audience = Request.QueryString["audience"];
        string rawURL = string.Empty;

        if (invitation_audience == "individual")
            rawURL = baseURL + "/qDbs/print/print-individual-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
        else if (invitation_audience == "family")
            rawURL = baseURL + "/qDbs/print/print-family-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
        else if (invitation_audience == "group")
            rawURL = baseURL + "/qDbs/print/print-group-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];       

        string passURL = Server.UrlEncode(rawURL);
        string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);
        Response.Redirect("~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput="+ invitation_audience + "_invitation_UserID_" + Context.Items["UserID"] + "_" + timeStamp + ".pdf");
    }

    protected void loadInviteSettings(object sender, EventArgs e)
    {
        if (ddlInvitationAudience.SelectedValue == "family")
        {
            plhFamilyContactInfo.Visible = false;
            plhExistingFamily.Visible = true;
            plhEditAudienceName.Visible = false;
            plhExistingGroup.Visible = true;
        }
        else if (ddlInvitationAudience.SelectedValue == "new family")
        {
            plhFamilyContactInfo.Visible = true;
            plhExistingFamily.Visible = false;
            plhEditAudienceName.Visible = true;
            plhExistingGroup.Visible = true;
        }
    }

    protected void populateFamilies()
    {
        ddlExistingFamilies.DataSource = qPtl_Family.GetFamilies();
        ddlExistingFamilies.DataTextField = "FamilyName";
        ddlExistingFamilies.DataValueField = "FamilyID";
        ddlExistingFamilies.DataBind();
        ddlExistingFamilies.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateGroups()
    {
        ddlExistingGroups.DataSource = qSoc_Space.GetSpaces();
        ddlExistingGroups.DataTextField = "SpaceShortName";
        ddlExistingGroups.DataValueField = "SpaceID";
        ddlExistingGroups.DataBind();
        ddlExistingGroups.Items.Insert(0, new ListItem("", string.Empty));
    }
}
