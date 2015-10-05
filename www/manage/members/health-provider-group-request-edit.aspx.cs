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
using Quartz.Organization;
using Quartz.Social;
using Quartz.Communication;
using Quartz.Health;

public partial class edit_group_request : System.Web.UI.Page
{
    public int group_request_id;
    public int user_id;
    public string order_by;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            group_request_id = Convert.ToInt32(Request.QueryString["groupRequestID"]);

            if (!String.IsNullOrEmpty(Convert.ToString(group_request_id)))
            {
                qOrg_GroupRequests request = new qOrg_GroupRequests(group_request_id);

                populateHealthProviders();

                lblTitle.Text = "Edit Group Request (ID: " + request.GroupRequestID + ")";

                lblRequestTimestamp.Text = Convert.ToString(request.Created);
                
                string advisor_info = string.Empty;
                advisor_info = "<span class=\"NormalBoldDarkGray\">Name:</span> " + request.AdvisorFirstName + " " + request.AdvisorLastName;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Position:</span> " + request.AdvisorPosition;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Other Position:</span> " + request.AdvisorPositionOther;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Email:</span> " + request.AdvisorEmail;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Phone:</span> " + request.AdvisorPhone;
                litAdvisorInfo.Text = advisor_info;

                string provider_info = string.Empty;
                provider_info = "<span class=\"NormalBoldDarkGray\">Name:</span> " + request.HealthProviderName;
                provider_info += "<br><br><span class=\"NormalBoldDarkGray\">Why Join:</span> " + request.WhyJoin;
                provider_info += "<br><br><span class=\"NormalBoldDarkGray\">How Many Members:</span> " + request.NumNumbers;
                litProviderInfo.Text = provider_info;

                txtAdvisorEmail.Text = request.AdvisorEmail;
                txtGroupName.Text = request.GroupShortName;
                txtAdvisorNotes.Text = request.AdvisorNotes;
                txtHealthProviderNotes.Text = request.GroupNotes;
                txtHealthProviderNotes.Text = request.SchoolNotes;
                ddlStatus.SelectedValue = request.Status;

                if (request.SpaceID > 0)
                {
                    btnApproveRequest.Visible = false;
                    btnResendEmail.Visible = true;
                    lblApprovalInfo.Text = "Approved by " + request.ApprovedBy + " and email last sent at " + request.WhenApproved;
                }
                if (!String.IsNullOrEmpty(Convert.ToString(request.HealthProviderID)))
                    if (request.HealthProviderID > 0)
                    {
                        ddlHealthProviders.SelectedValue = Convert.ToString(request.HealthProviderID);
                    }
                    else
                    {
                        btnApproveRequest.Enabled = false;
                        lblApprovalInfo.Text = "<br><br><strong>*** WARNING: This request does NOT have a health provider associated with it yet. A health provider must be selected from the pull down in the Health Provider Info section below. ***</strong>";
                    }

                else
                {
                    btnApproveRequest.Enabled = false;
                    lblApprovalInfo.Text = "<br><br><strong>*** WARNING: This request does NOT have a health provider associated with it yet. A health provider must be selected from the pull down in the Health Provider Info section below. ***</strong>";
                }
            }
            else
            {
                lblTitle.Text = "A Problem Has Occurred: Please go back and try again";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        request.Status = ddlStatus.SelectedValue;
        request.AdvisorEmail = txtAdvisorEmail.Text;
        request.GroupShortName = txtGroupName.Text;

        if (!String.IsNullOrEmpty(ddlHealthProviders.SelectedValue))
            request.HealthProviderID = Convert.ToInt32(ddlHealthProviders.SelectedValue);
        request.Update();

        Response.Redirect("/manage/members/health-provider-group-request-edit.aspx?groupRequestID=" + Request.QueryString["groupRequestID"]);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        request.Available = "No";
        request.MarkAsDelete = 0;
        request.Update();

        Response.Redirect("/manage/members/health-provider-group-requests-list.aspx");
    }

    protected void populateHealthProviders()
    {
        ddlHealthProviders.DataSource = qHtl_HealthProvider.GetHealthProviders();
        ddlHealthProviders.DataTextField = "Name";
        ddlHealthProviders.DataValueField = "HealthProviderID";
        ddlHealthProviders.DataBind();
        ddlHealthProviders.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        Page.Validate("register");

        if (Page.IsValid)
        {
            int group_request_id = Convert.ToInt32(Request.QueryString["groupRequestID"]);
            qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));

            // create space ***************************************************
            var space = qSoc_Space.GenerateSpace(4);
            space.Available = "Yes";
            space.Created = DateTime.Now;
            space.CreatedBy = 0;
            space.LastModified = DateTime.Now;
            space.LastModifiedBy = 0;
            space.MarkAsDelete = 0;
            space.ScopeID = 1;
            if (!String.IsNullOrEmpty(ddlHealthProviders.SelectedValue))
                space.HospitalID = Convert.ToInt32(ddlHealthProviders.SelectedValue);
            space.SpaceShortName = request.GroupShortName;
            if (!String.IsNullOrEmpty(txtGroupName.Text))
                space.SpaceName = request.GroupFullName;
            else
                space.SpaceName = request.GroupShortName;
            space.VisibleInDirectory = "No";
            space.SpaceType = "organization";
            space.AccessMode = "invitation";
            space.SpaceCategoryID = request.SpaceCategoryID;
            space.Update();

            // create invitation ************************************************
            int functional_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["Register_InvitationFunctionalRoleID"]);
            int member_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["Register_DefaultRoleID"]);
            int moderator_role_id = Convert.ToInt32(ConfigurationManager.AppSettings["Register_ModeratorRoleID"]);
            string invitation_type = Convert.ToString(ConfigurationManager.AppSettings["Register_InvitationType"]);
            int invite_length = Convert.ToInt32(ConfigurationManager.AppSettings["Register_InvitationLength"]);

            DateTime start_time = new DateTime();
            DateTime end_time = new DateTime();

            start_time = DateTime.Now;
            end_time = start_time.AddYears(10);

            int space_id = 0;
            string group_audience_name = string.Empty;

            space_id = space.SpaceID;
            group_audience_name = space.SpaceShortName;

            // create advisor invitation code
            var invite = qPtl_Invitation.GenerateInvite(0, start_time, end_time, 0, 0, invite_length, invitation_type, moderator_role_id, functional_role_id);
            invite.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            invite.LastModified = DateTime.Now;
            invite.MarkAsDelete = 0;
            invite.InvitationAudience = "moderated group";
            invite.InvitationAudienceName = group_audience_name;
            invite.Available = "Yes";
            invite.InvitationStatus = "Redeemable";
            invite.CurrRedemptions = 0;
            invite.MaxRedemptions = -1;
            invite.SpaceID = space_id;
            invite.Update();

            // update request
            request.ApprovedBy = user.UserName;
            request.WhenApproved = DateTime.Now;
            request.Status = "Completed";
            request.SpaceID = space.SpaceID;
            request.AdvisorInviteID = invite.InvitationID;
            request.Update();

            SendInvitationEmail(request.AdvisorEmail, request.AdvisorFirstName, txtGroupName.Text, invite.InviteCode);

            // reload page and indicate that the email was already sent
            Response.Redirect(Request.Url.ToString());
        }
    }

    public static int SendInvitationEmail(string email, string first_name, string group_name, string invite_code)
    {
        int sent_email_log_id = 0;
        
        // send email to advisor with approval and code for signing up --> single email without attached PDF file
        if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Email_AdvisorInvitationEmailID"]))
        {
            int group_advisor_invitation_email_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Email_AdvisorInvitationEmailID"]);
            qCom_EmailTool etool = new qCom_EmailTool(group_advisor_invitation_email_id);
            string value1 = first_name;                                                                     // advisor name
            string value2 = group_name;                                                                     // advisor group
            string value3 = invite_code;                                                                    // invitation code
            string value4 = System.Configuration.ConfigurationManager.AppSettings["Site_RegisterUrl"];      // register-link
            sent_email_log_id = etool.SendDatabaseMail(email, group_advisor_invitation_email_id, 0, "", value1, value2, value3, value4, false);
        }

        return sent_email_log_id;
    }

    protected void btnResendEmail_Click(object sender, EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        qPtl_Invitation invite = new qPtl_Invitation(request.AdvisorInviteID);

        int sent_email_id = SendInvitationEmail(request.AdvisorEmail, request.AdvisorFirstName, txtGroupName.Text, invite.InviteCode);
        lblMessage.Text = "*** Email resent ***";
    }
}
