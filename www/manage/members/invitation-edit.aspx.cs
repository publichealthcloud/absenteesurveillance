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

public partial class edit_invitation : System.Web.UI.Page
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
            ddlInvitationAudience.Items.FindByValue("group").Enabled = false; 
            ddlInvitationAudience.Items.FindByValue("new group").Enabled = false;
            ddlInvitationAudience.Items.FindByValue("moderated group").Enabled = false;
            ddlInvitationAudience.Items.FindByValue("new moderated group").Enabled = false;
            ddlInvitationAudience.Items.FindByValue("individual").Enabled = false;
            string invitation_types = Convert.ToString(ConfigurationManager.AppSettings["Members_InvitationTypes"]);
            if (invitation_types.ToLower().Contains("family"))
            {
                ddlInvitationAudience.Items.FindByValue("family").Enabled = true;
                ddlInvitationAudience.Items.FindByValue("new family").Enabled = true;
            }
            if (invitation_types.ToLower().Contains("simple group"))
            {
                ddlInvitationAudience.Items.FindByValue("group").Enabled = true;
                ddlInvitationAudience.Items.FindByValue("new group").Enabled = true;
            }
            if (invitation_types.ToLower().Contains("moderated group"))
            {
                ddlInvitationAudience.Items.FindByValue("moderatedGroup").Enabled = true;
                plhSchools.Visible = true;
            }

            if (invitation_types.ToLower().Contains("individual"))
            {
                ddlInvitationAudience.Items.FindByValue("individual").Enabled = true;
            }
            
            if (!String.IsNullOrEmpty(Request.QueryString["invitationID"]))
            {
                ddlInvitationAudience.Items.FindByValue("individual").Enabled = true;
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

            if (invitation_audience == "individual")
            {
                rawURL = baseURL + "/qDbs/print/print-individual-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
                lblTitle.Text = "Edit Individual Invitation (ID: " + invite.InvitationID + ")";
                ddlInvitationAudience.Items.FindByValue("family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("moderated group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("").Enabled = false; 
                lblAudience.Text = "Individual Name *";
                hplBackTop.NavigateUrl = "invitations-list-individuals.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-individuals.aspx";
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhEditAudienceName.Visible = true;
            }
            else if (invitation_audience == "family")
            {
                plhSchools.Visible = false;
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
                ddlInvitationAudience.Items.FindByValue("individual").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("group").Enabled = false;
                //ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("moderated group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("").Enabled = false;
                plhExistingFamily.Visible = false;
                lblAudience.Text = "Family Name *";
                hplBackTop.NavigateUrl = "invitations-list-families.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-families.aspx";
                hplBackTop.Text = "<i class=\"icon-circle-arrow-left\"></i>&nbsp;&nbsp;Back to Family Invitations";
                hplBackBottom.Text = "<i class=\"icon-circle-arrow-left\"></i>&nbsp;&nbsp;Back to Family Invitations";
            }
            else if (invitation_audience == "moderatedGroup")
            {
                populateSchools();
                rawURL = baseURL + "/qDbs/print/print-moderated-group-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
                lblTitle.Text = "Edit Moderated Group Invitation (ID: " + invite.InvitationID + ")";
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhModeratedGroupInfo.Visible = true;
                qPtl_Role invite_role = new qPtl_Role(invite.RoleID);
                lblInvitationForUserRole.Text = invite_role.RoleName;
                txtAudienceName.Visible = false;
                lblAudienceName.Visible = true;
                plhExistingGroup.Visible = false;
                plhEditAudienceName.Visible = false;
                plhCreateNewModeratedGroup.Visible = true;

                if (invite.SpaceID > 0)
                {
                    qSoc_Space space = new qSoc_Space(invite.SpaceID);
                    if (space.SchoolID > 0)
                    {
                        ddlSchools.SelectedValue = Convert.ToString(space.SchoolID);
                    }
                    txtModeratedGroupNameShort.Text = space.SpaceShortName;
                    txtModeratedGroupName.Text = space.SpaceName;
                    ddlGroupType.SelectedValue = space.SpaceType;
                }
                /*
                // load group information
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
                    */
                ddlInvitationAudience.Items.FindByValue("individual").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("new group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("new moderated group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("").Enabled = false;
                plhExistingFamily.Visible = false;
                lblAudience.Text = "Group Name *";
                hplBackTop.NavigateUrl = "invitations-list-moderated-groups.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-moderated-groups.aspx";
            }
            else if (invitation_audience == "group")
            {
                rawURL = baseURL + "/qDbs/print/print-group-invitations.aspx?invitationID=" + Request.QueryString["invitationID"];
                lblTitle.Text = "Edit Group Invitation (ID: " + invite.InvitationID + ")";
                ddlInvitationAudience.Items.FindByValue("family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("new family").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("individual").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("moderated group").Enabled = false;
                ddlInvitationAudience.Items.FindByValue("").Enabled = false;
                lblAudience.Text = "Group Name *";
                hplBackTop.NavigateUrl = "invitations-list-groups.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-groups.aspx";
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhEditAudienceName.Visible = true;
            }

            //hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            //hplRefreshTop.NavigateUrl = Request.Url.ToString();

            string passURL = Server.UrlEncode(rawURL);
            string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);

            hplPrint.NavigateUrl = "~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=" + invitation_audience + " invitation_id_" + invitation_id + "_" + timeStamp + ".pdf";
            hplPrint.Target = "_blank";

            plhInvitationCode.Visible = true;
            plhTools.Visible = true;
        }
        else
        {
            rblAvailable.SelectedValue = "Yes";
            if (!String.IsNullOrEmpty(Request.QueryString["audience"]))
                ddlInvitationAudience.SelectedValue = Convert.ToString(Request.QueryString["audience"]);
            plhInvitationCode.Visible = false;
            plhTools.Visible = false;
            plhCurrRedemptions.Visible = false;

            string invitation_audience = Request.QueryString["audience"];

            //hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            //hplRefreshTop.NavigateUrl = Request.Url.ToString();
            if (invitation_audience == "individual")
            {
                lblTitle.Text = "New Invitation";
                hplBackTop.NavigateUrl = "invitations-list-individuals.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-individuals.aspx";
                lblAudience.Text = "Individual Name *";
                plhCreateFamilyInvitationSettings.Visible = false;
                plhNumRedemptions.Visible = true;
                txtMaxRedemptions.Text = Convert.ToString(1);
                txtMaxRedemptions.Enabled = false;
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhEditAudienceName.Visible = true;
                ddlExistingGroups.Visible = false;
            }
            else if (invitation_audience == "family")
            {
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
            else if (invitation_audience == "group")
            {
                lblTitle.Text = "New Invitation";
                hplBackTop.NavigateUrl = "invitations-list-groups.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-groups.aspx";
                lblAudience.Text = "Group Name *";
                plhCreateFamilyInvitationSettings.Visible = false;
                plhNumRedemptions.Visible = true;
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhEditAudienceName.Visible = true;
                ddlExistingGroups.Visible = false;
            }
            else if (invitation_audience == "moderatedGroup")
            {
                lblTitle.Text = "New Invitation";
                hplBackTop.NavigateUrl = "invitations-list-moderated-groups.aspx";
                hplBackBottom.NavigateUrl = "invitations-list-moderated-groups.aspx";
                lblAudience.Text = "Group Name *";
                plhCreateFamilyInvitationSettings.Visible = false;
                plhNumRedemptions.Visible = true;
                plhExistingFamily.Visible = false;
                plhFamilyContactInfo.Visible = false;
                plhEditAudienceName.Visible = true;
                ddlInvitationAudience.SelectedValue = "new moderated group";
                plhExistingGroup.Visible = false;
                plhCreateModeratedGroupInvitationSettings.Visible = true;
                lblMaxRedemptions.Text = "Max Number of Member Redemptions";
                plhCreateNewModeratedGroup.Visible = true;
                plhEditAudienceName.Visible = false;
            }
        }
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

                if (!String.IsNullOrEmpty(ddlSchools.SelectedValue))
                {
                    if (invite.SpaceID > 0)
                    {
                        qSoc_Space space = new qSoc_Space(invite.SpaceID);
                        space.SchoolID = Convert.ToInt32(ddlSchools.SelectedValue);
                        space.Update();
                    }
                }
                
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

                if (ddlInvitationAudience.SelectedValue == "individual")
                {
                    var invite = qPtl_Invitation.GenerateInvite(0, start_time, end_time, Convert.ToInt32(Context.Items["UserID"]), 0, invite_length, invitation_type, member_role_id, functional_role_id);

                    invite.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    invite.LastModified = DateTime.Now;
                    invite.MarkAsDelete = 0;
                    invite.InvitationAudience = ddlInvitationAudience.SelectedValue;
                    invite.InvitationAudienceName = txtAudienceName.Text;
                    invite.Available = rblAvailable.SelectedValue;
                    invite.InvitationStatus = "Redeemable";
                    invite.CurrRedemptions = 0;
                    invite.MaxRedemptions = 1;

                    invite.Update();
                    invitation_id = invite.InvitationID;
                }
                else if (ddlInvitationAudience.SelectedValue == "group")
                {
                    var invite = qPtl_Invitation.GenerateInvite(0, start_time, end_time, Convert.ToInt32(Context.Items["UserID"]), 0, invite_length, invitation_type, member_role_id, functional_role_id);

                    invite.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    invite.LastModified = DateTime.Now;
                    invite.MarkAsDelete = 0;
                    invite.InvitationAudience = ddlInvitationAudience.SelectedValue;
                    invite.InvitationAudienceName = txtAudienceName.Text;
                    invite.Available = rblAvailable.SelectedValue;
                    invite.InvitationStatus = "Redeemable";
                    invite.CurrRedemptions = 0;
                    if (!String.IsNullOrEmpty(txtMaxRedemptions.Text))
                        invite.MaxRedemptions = Convert.ToInt32(txtMaxRedemptions.Text);

                    invite.Update();
                    invitation_id = invite.InvitationID;
                }
                else if (ddlInvitationAudience.SelectedValue == "family" || ddlInvitationAudience.SelectedValue == "new family")
                {
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
                else if (ddlInvitationAudience.SelectedValue == "moderated group" || ddlInvitationAudience.SelectedValue == "new moderated group")
                {
                    int space_id = 0;
                    string group_audience_name = string.Empty;

                    if (ddlInvitationAudience.SelectedValue == "new moderated group" || ddlInvitationAudience.SelectedValue == "moderatedGroup")
                    {
                        // create space
                        qSoc_Space space = new qSoc_Space();
                        space.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                        space.Available = rblAvailable.SelectedValue;
                        space.Created = DateTime.Now;
                        space.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                        space.LastModified = DateTime.Now;
                        space.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                        space.MarkAsDelete = 0;
                        space.SpaceShortName = txtModeratedGroupNameShort.Text;
                        space.SpaceName = txtModeratedGroupName.Text;
                        space.AccessMode = "invitation";
                        space.VisibleInDirectory = "No";
                        space.SpaceType = ddlGroupType.SelectedValue;
                        if (!String.IsNullOrEmpty(ddlSchools.SelectedValue))
                        {
                            space.SchoolID = Convert.ToInt32(ddlSchools.SelectedValue);
                        }
                        space.Insert();

                        space_id = space.SpaceID;
                        group_audience_name = txtModeratedGroupName.Text;
                    }
                    else
                    {
                        space_id = Convert.ToInt32(ddlExistingGroups.SelectedValue);
                        qSoc_Space space = new qSoc_Space(space_id);
                        group_audience_name = space.SpaceShortName;
                    }

                    // create necessary invitations -- moderators
                    int num_moderators = Convert.ToInt32(txtNumGroupModerators.Text);
                    for (int i = 0; i < num_moderators; i++)
                    {
                        var invite = qPtl_Invitation.GenerateInvite(0, start_time, end_time, Convert.ToInt32(Context.Items["UserID"]), 0, invite_length, invitation_type, moderator_role_id, functional_role_id);
                        invite.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                        invite.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                        invite.LastModified = DateTime.Now;
                        invite.MarkAsDelete = 0;
                        invite.InvitationAudience = "moderated group";
                        invite.InvitationAudienceName = group_audience_name;
                        invite.Available = rblAvailable.SelectedValue;
                        invite.InvitationStatus = "Redeemable";
                        invite.CurrRedemptions = 0;
                        invite.MaxRedemptions = 1;
                        invite.SpaceID = space_id;
                        invite.Update();
                    }

                    // create single teen invitation
                    var invite_t = qPtl_Invitation.GenerateInvite(0, start_time, end_time, Convert.ToInt32(Context.Items["UserID"]), 0, invite_length, invitation_type, member_role_id, functional_role_id);
                    invite_t.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    invite_t.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    invite_t.LastModified = DateTime.Now;
                    invite_t.MarkAsDelete = 0;
                    invite_t.InvitationAudience = "moderated group";
                    invite_t.InvitationAudienceName = group_audience_name;
                    invite_t.Available = rblAvailable.SelectedValue;
                    invite_t.InvitationStatus = "Redeemable";
                    invite_t.CurrRedemptions = 0;
                    if (!String.IsNullOrEmpty(txtNumTeens.Text))
                        invite_t.MaxRedemptions = Convert.ToInt32(txtNumTeens.Text);
                    else
                        invite_t.MaxRedemptions = -1;
                    invite_t.SpaceID = space_id;
                    invite_t.Update();
                    invitation_id = invite_t.InvitationID;

                    qSoc_Space existing_space = new qSoc_Space(space_id);
                    existing_space.SpaceCode = invite_t.InviteCode;
                    existing_space.Update();
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

        if (invite.InvitationAudience == "individual")
            Response.Redirect("invitations-list-individuals.aspx");
        else if (invite.InvitationAudience == "family")
            Response.Redirect("invitations-list-families.aspx");
        else if (invite.InvitationAudience == "group")
            Response.Redirect("invitations-list-groups.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string invitation_audience = Request.QueryString["audience"];

        if (invitation_audience == "individual")
            Response.Redirect("invitations-list-individuals.aspx");
        else if (invitation_audience == "family")
            Response.Redirect("invitations-list-families.aspx");
        else if (invitation_audience == "group")
            Response.Redirect("invitations-list-groups.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        string invitation_audience = Request.QueryString["audience"];

        if (invitation_audience == "individual")
            Response.Redirect("invitations-list-individuals.aspx");
        else if (invitation_audience == "family")
            Response.Redirect("invitations-list-families.aspx");
        else if (invitation_audience == "group")
            Response.Redirect("invitations-list-groups.aspx");
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
        else if (ddlInvitationAudience.SelectedValue == "new moderated group")
        {
            plhModeratedGroupInfo.Visible = false;
            plhFamilyContactInfo.Visible = false;
            plhExistingFamily.Visible = false;
            plhEditAudienceName.Visible = true;
            plhCreateModeratedGroupInvitationSettings.Visible = true;
            plhCreateNewModeratedGroup.Visible = true;
            plhEditAudienceName.Visible = false;
        }
        else if (ddlInvitationAudience.SelectedValue == "moderated group")
        {
            plhModeratedGroupInfo.Visible = true;
            plhFamilyContactInfo.Visible = false;
            plhExistingFamily.Visible = false;
            plhEditAudienceName.Visible = true;
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

    protected void populateSchools()
    {
        ddlSchools.DataSource = qOrg_School.GetSchools();
        ddlSchools.DataTextField = "School";
        ddlSchools.DataValueField = "SchoolID";
        ddlSchools.DataBind();
        ddlSchools.Items.Insert(0, new ListItem("", string.Empty));
    } 
 
}
