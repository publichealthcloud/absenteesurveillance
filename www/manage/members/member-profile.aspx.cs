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
using System.Text.RegularExpressions;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Data;
using Quartz.Help;
using Quartz.Core;
using Quartz.Organization;
using Quartz.Controls;
using Quartz.Communication;

public partial class manage_members_member_profile : System.Web.UI.Page
{
    protected int profile_id;
    protected string required_indicator;
    protected string race_required_indicator;
    bool race_required = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_RaceRequired"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            profile_id = Convert.ToInt32(Request.QueryString["userID"]);

            qPtl_User profile = new qPtl_User(profile_id);

            hplManageTrainings.NavigateUrl = "member-learning.aspx?userID=" + profile_id;
            string nav_mode = System.Configuration.ConfigurationManager.AppSettings["Site_NavMode"];
            if (nav_mode == "lms")
                plhSchoolDisplay.Visible = false;

            string img_url = string.Empty;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if (!String.IsNullOrEmpty(profile.ProfilePict))
                img_url = baseUrl + "user_data/" + profile.UserName + "/" + profile.ProfilePict + ".ashx?width=84&height=84&mode=crop";
            else
                img_url = baseUrl + "images/mylife_portrait_default.jpg.ashx?width=84&height=84&mode=crop";

            litProfilePict.Text = "<img src=\"" + img_url + "\" />";

            lblUsername.Text = profile.UserName;
            lblFullName.Text = profile.FirstName + " " + profile.LastName;
            lblEmail.Text = profile.Email;
            lblUserID.Text = Convert.ToString(profile.UserID);
            lblCreated.Text = Convert.ToString(profile.Created);
            lblMostRecentLogin.Text = Convert.ToString(profile.LastTimeSeen);
            lblMostRecentIPAddress.Text = profile.LastIPAddress;
            lblRoleName.Text = profile.HighestRole;

            txtFirstName.Text = profile.FirstName;
            txtLastName.Text = profile.LastName;
            txtEmail.Text = profile.Email;
            lblUsernameProfile.Text = profile.UserName;
            if (profile.HighestRole == "Mobile")
            {
                lblMemberTypeProfile.Text = "Mobile Only";
            }
            else
            {
                lblMemberTypeProfile.Text = "Social";
                required_indicator = " *";
                if (race_required == true)
                    race_required_indicator = " *";
            }

            qPtl_UserProfile full_profile = new qPtl_UserProfile(profile_id);
            DateTime check = new DateTime();
            check = Convert.ToDateTime("1/1/1900");
            if (full_profile.DOB != check)
            {
                try
                {
                    rdtDOB.SelectedDate = Convert.ToDateTime(full_profile.DOB).Date;
                }
                catch
                {
                    // do nothing
                }
            }

            var user_space = qSoc_UserSpace_View.GetMostRecentUserspace(profile_id);

            if (user_space != null)
            {
                if (user_space.UserSpaceID > 0)
                {
                    lblSchool.Text = user_space.School;
                    string full_info = string.Empty;
                    full_info = user_space.SpaceShortName;
                    if (!String.IsNullOrEmpty(user_space.CategoryName))
                        full_info += " [" + user_space.CategoryName + "]";
                    if (!String.IsNullOrEmpty(user_space.School))
                        full_info += " at " + user_space.School;
                    lblGroupTabGroupName.Text = "<a href=\"/manage/members/space-edit.aspx?spaceID=" + user_space.SpaceID + "\">" + full_info + " <i class=\"icon-circle-arrow-right\"></i></a>";
                    string full_group_name = user_space.SpaceShortName;
                    if (!String.IsNullOrEmpty(user_space.CategoryName))
                        full_group_name += " [" + user_space.CategoryName + "]";
                    lblGroupName.Text = "<a href=\"/manage/members/space-edit.aspx?spaceID=" + user_space.SpaceID + "\">" + full_group_name + " <i class=\"icon-circle-arrow-right\"></i></a>";
                }
                else
                {
                    btnChangeGroup.Text = "Add To Group";
                }
            }

            // profile gender
            string gender_options = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_SupportedGenderValues"]);
            ddlGender.Items.FindByValue("Intersex").Enabled = false;
            ddlGender.Items.FindByValue("Transgender").Enabled = false;
            if (gender_options.Contains("Intersex"))
                ddlGender.Items.FindByValue("Intersex").Enabled = true;
            if (gender_options.Contains("Transgender"))
                ddlGender.Items.FindByValue("Transgender").Enabled = true;
            ddlGender.SelectedValue = full_profile.Gender;

            // evaluate race
            if (!String.IsNullOrEmpty(full_profile.Race))
            {
                if (full_profile.Race.Contains("Asian/SouthAsian"))
                    cblRace.Items.FindByValue("Asian/SouthAsian").Selected = true;
                if (full_profile.Race.Contains("Biracial"))
                    cblRace.Items.FindByValue("Biracial").Selected = true;
                if (full_profile.Race.Contains("Black/African American"))
                    cblRace.Items.FindByValue("Black/African American").Selected = true;
                if (full_profile.Race.Contains("Latino/a"))
                    cblRace.Items.FindByValue("Latino/a").Selected = true;
                if (full_profile.Race.Contains("Middle Eastern"))
                    cblRace.Items.FindByValue("Middle Eastern").Selected = true;
                if (full_profile.Race.Contains("Multiracial"))
                    cblRace.Items.FindByValue("Multiracial").Selected = true;
                if (full_profile.Race.Contains("Pacific Islander"))
                    cblRace.Items.FindByValue("Pacific Islander").Selected = true;
                if (full_profile.Race.Contains("White/European-American"))
                    cblRace.Items.FindByValue("White/European-American").Selected = true;
            }

            // **** PERMISSIONS ****
            string sqlCode = string.Empty;
            qDbs_SQLcode sql = new qDbs_SQLcode();
            q_Helper helper = new q_Helper();

            // get roles
            sqlCode = "SELECT RoleID, RoleName FROM qPtl_Roles WHERE Available = 'Yes' ORDER BY RoleRank";

            DataTable dtRoles;

            using (dtRoles = sql.GetDataTable(sqlCode))
            {

                cblRoles.DataSource = dtRoles;
                cblRoles.DataValueField = "RoleID";
                cblRoles.DataTextField = "RoleName";
                cblRoles.DataBind();
            }

            // mark current permissions
            sqlCode = "SELECT RoleID FROM qPtl_UserRoles WHERE UserID = " + Request.QueryString["userID"];

            SqlDataReader rReader;

            using (rReader = sql.GetDataReader(sqlCode))
            {
                while (rReader.Read())
                {
                    ListItem currentCheckBox = cblRoles.Items.FindByValue(rReader["RoleID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                }
            }


            // *** GROUP ***
            // load group information and all other available groups
            ddlSpaces.DataSource = qSoc_Space.GetSpaces();
            ddlSpaces.DataTextField = "SpaceShortName";
            ddlSpaces.DataValueField = "SpaceID";
            ddlSpaces.DataBind();
            ddlSpaces.Items.Insert(0, new ListItem("", "0"));

            
            /* // no longer used since we now want to automatically remove any existing user spaces from the list of options for adding
            if (user_space != null)
            {
                if (user_space.UserSpaceID > 0)
                {
                    ddlSpaces.SelectedValue = Convert.ToString(user_space.SpaceID);
                }
            }
            */

            // get other groups enrolled
            var u_groups = qSoc_UserSpace_View.GetAllAvailableUserSpacesOrderMostRecent(profile_id);
            if (u_groups != null)
            {
                int j = 0;
                string group_list = string.Empty;
                foreach (var g in u_groups)
                {
                    Quartz.Controls.MemberEnrolledGroup curr_group = (Quartz.Controls.MemberEnrolledGroup)LoadControl("~/manage/members/controls/MemberEnrolledGroup.ascx");
                    curr_group.UserSpaceID = g.UserSpaceID;
                    curr_group.UserID = g.UserID;
                    curr_group.SpaceID = g.SpaceID;
                    if (g.PrimarySpace == true)
                        curr_group.IsPrimary = true;
                    pnlUserGroups.Controls.Add(curr_group);

                    // remove each of these spaces from the spaces pull down
                    if (ddlSpaces.Items.Count > 0)
                    {
                        try
                        {
                            ddlSpaces.Items.FindByValue(Convert.ToString(g.SpaceID)).Enabled = false;
                        }
                        catch
                        {
                            // do nothing
                        }
                    }
                    j++;
                }
            }


            // *** TAB MANAGEMENT ***
            string curr_tab = string.Empty;
            curr_tab = Request.QueryString["currTab"];
            litOverviewClass.Text = "";
            litProfileClass.Text = "";
            litPermissionClass.Text = "";
            litGroupClass.Text = "";
            litWarningsClass.Text = "";
            litTabOverviewClass.Text = "class=\"tab-pane\"";
            litTabProfileClass.Text = "class=\"tab-pane\"";
            litTabPermissionsClass.Text = "class=\"tab-pane\"";
            litTabWarningsClass.Text = "class=\"tab-pane\"";
            litTabGroupClass.Text = "class=\"tab-pane\"";
            if (curr_tab == "profile")
            {
                litProfileClass.Text = "class='active'";
                litTabProfileClass.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblProfileMessage.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "permissions")
            {
                litPermissionClass.Text = "class='active'";
                litTabPermissionsClass.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblPermissionsMessage.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "group")
            {
                litGroupClass.Text = "class='active'";
                litTabGroupClass.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblGroupMessage.Text = " *** " + Request.QueryString["message"] + "***<br><br>";
            }
            else if (curr_tab == "warnings")
            {
                litWarningsClass.Text = "class='active'";
                litTabWarningsClass.Text = "class=\"tab-pane active\"";
            }
            else
            {
                litOverviewClass.Text = "class='active'";
                litTabOverviewClass.Text = "class=\"tab-pane active\"";
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // first delete all existing roles
        int user_id = Convert.ToInt32(Request.QueryString["userID"]);
        string sqlCode = "DELETE FROM qPtl_UserRoles WHERE UserID = " + user_id;
        qDbs_SQLcode sql = new qDbs_SQLcode();
        sql.ExecuteSQL(sqlCode);

        // delete from usermember group -- > cms permissions
        //qPtl_UserGroupMembers.DeleteUserGroupMember(user_id);

        // create records for all new roles
        int n;
        string selectedItems = string.Empty;

        n = 0;
        foreach (ListItem item in cblRoles.Items)
        {
            if (item.Selected)
            {
                sqlCode = "INSERT INTO qPtl_UserRoles (UserID, RoleID)";
                sqlCode += " VALUES (" + Request.QueryString["userID"] + "," + item.Value + ")";
                sql.ExecuteSQL(sqlCode);

                if (n > 0)
                {
                    selectedItems += "," + item.Value;
                }
                else
                {
                    selectedItems += item.Value;
                }
                n++;
            }
        }

        Response.Redirect("/manage/members/member-profile.aspx?userID=" + user_id + "&currTab=permissions&message=successfully updated");
    }

    protected void btnChangeGroup_Click(object sender, EventArgs e)
    {
        // delete existing group
        if (!String.IsNullOrEmpty(ddlSpaces.SelectedValue))
        {
            int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
            int profile_id = Convert.ToInt32(Request.QueryString["userID"]);
            var user_space = qSoc_UserSpace_View.GetMostRecentUserspace(profile_id);

            if (user_space != null)
            {
                //DeleteSpaceItems(user_space);     // no longer do this so we can support multiple groups
            }

            // create new group based on selected items
            qSoc_Space space = new qSoc_Space(Convert.ToInt32(ddlSpaces.SelectedValue));

            qPtl_User user = new qPtl_User(profile_id);

            qSoc_UserSpace u_space = new qSoc_UserSpace();
            u_space.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            u_space.Available = "Yes";
            u_space.Created = DateTime.Now;
            u_space.CreatedBy = curr_user_id;
            u_space.LastModified = DateTime.Now;
            u_space.LastModifiedBy = curr_user_id;
            u_space.MarkAsDelete = 0;
            u_space.UserID = profile_id;
            u_space.SpaceID = space.SpaceID;
            if (user.HighestRole == "Advisor")
                u_space.SpaceRole = "Moderator";
            u_space.Insert();

            // create new school
            if (space.SchoolID > 0)
            {
                qOrg_UserSchool u_school = new qOrg_UserSchool();
                u_school.Available = "Yes";
                u_school.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                u_school.MarkAsDelete = 0;
                u_school.Created = DateTime.Now;
                u_school.CreatedBy = curr_user_id;
                u_school.LastModified = DateTime.Now;
                u_school.LastModifiedBy = curr_user_id;
                u_school.MarkAsDelete = 0;
                u_school.UserID = profile_id;
                u_school.SchoolID = space.SchoolID;
                u_school.Insert();
            }

            Response.Redirect("/manage/members/member-profile.aspx?userID=" + profile_id + "&currTab=group&message=successfully updated user group");
        }
        else
            lblGroupMessage.Text = "*** You must first select a group ***";
    }

    protected void DeleteSpaceItems(qSoc_UserSpace_View u_space)
    {
        qSoc_UserSpace.DeleteUserSpace(u_space.UserSpaceID);

        // delete existing school
        qOrg_UserSchool.DeleteUserSchool(u_space.UserID, u_space.SchoolID);
    }

    protected void btnDeleteGroup_Click(object sender, EventArgs e)
    {
        int profile_id = Convert.ToInt32(Request.QueryString["userID"]);
        var user_space = qSoc_UserSpace_View.GetMostRecentUserspace(profile_id);

        if (user_space != null)
        {
            DeleteSpaceItems(user_space);

            Response.Redirect("/manage/members/member-profile.aspx?userID=" + profile_id + "&currTab=group&message=successfully removed from user group");
        }
    }

    protected void btnDeleteAllGroups_Click(object sender, EventArgs e)
    {
        int profile_id = Convert.ToInt32(Request.QueryString["userID"]);
        var spaces = qSoc_UserSpace_View.GetAllAvailableUserSpacesOrderMostRecent(profile_id);

        if (spaces != null)
        {
            foreach (var s in spaces)
            {
                qSoc_UserSpace_View user_space = new qSoc_UserSpace_View(s.UserSpaceID);
                DeleteSpaceItems(user_space);
            }
        }
        Response.Redirect("/manage/members/member-profile.aspx?userID=" + profile_id + "&currTab=group&message=successfully removed from user group");
    }

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        profile_id = Convert.ToInt32(Request.QueryString["userID"]);
        Page.Validate("profile");

        if (Page.IsValid)
        {
            qPtl_User user = new qPtl_User(profile_id);
            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Email = txtEmail.Text;
            user.Update();

            qPtl_UserProfile profile = new qPtl_UserProfile(profile_id);
            profile.Gender = ddlGender.SelectedValue;
            if (!String.IsNullOrEmpty(Convert.ToString(rdtDOB)))
                profile.DOB = rdtDOB.SelectedDate;
            string race = string.Empty;
            int n = 0;
            foreach (ListItem item in cblRace.Items)
            {
                if (item.Selected)
                {
                    if (n > 0)
                    {
                        race += "," + item.Value;
                    }
                    else
                    {
                        race += item.Value;
                    }
                    n++;
                }
            }
            n = 0;
            profile.Race = race;
            profile.Update();
            
            Response.Redirect("/manage/members/member-profile.aspx?userID=" + profile_id + "&currTab=profile&message=successfully updated member profile");
        }
    }

    protected void ValidateEverything(object obj, ServerValidateEventArgs args)
    {     
        string error_msg = string.Empty;
        bool error_occurred = false;

        qPtl_User curr_profile = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));

        if (curr_profile.HighestRole != "Mobile")
        {
            // validate email -- not empty, meets regex, not used
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "email missing";
            }
            string pat = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
            Match m = r.Match(txtEmail.Text);
            if (!m.Success)
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "email is not the correct format";
            }
            string email = txtEmail.Text.Trim();

            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                if (curr_profile.Email != txtEmail.Text)
                {
                    var user = qPtl_User.GetUserByEmail(txtEmail.Text);

                    if (user != null)
                    {
                        if (user.UserID > 0)
                        {
                            error_occurred = true;
                            if (!String.IsNullOrEmpty(error_msg))
                                error_msg += ", ";
                            error_msg += "email already being used by another user";
                        }
                    }
                }
            }

            // validate gender -- must be selected
            if (String.IsNullOrEmpty(ddlGender.SelectedValue))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "gender missing";
            }

            // validate race -- something must be selected
            string race = string.Empty;
            int n = 0;
            foreach (ListItem item in cblRace.Items)
            {
                if (item.Selected)
                {
                    if (n > 0)
                    {
                        race += "," + item.Value;
                    }
                    else
                    {
                        race += item.Value;
                    }
                    n++;
                }
            }
            n = 0;

            if (race_required == true && String.IsNullOrEmpty(race))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "race required";
            }

            // validate age -- age exists & user is within age range
            if (String.IsNullOrEmpty(Convert.ToString(rdtDOB.SelectedDate)))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "date of birth missing";
            }
        }

        // final analysis
        if (error_occurred == true)
        {
            args.IsValid = false;
            lblMsg.Text = "<br><br><strong>Problems:</strong> " + error_msg;
            lblMsg.Visible = true;
            cvValidateAllData.ErrorMessage = "<strong>Problems: </strong> " + error_msg + " *";
        }

        // display correct tab
        litTabOverviewClass.Text = "class=\"tab-pane\"";
        litTabProfileClass.Text = "class=\"tab-pane\"";
        litTabPermissionsClass.Text = "class=\"tab-pane\"";
        litTabWarningsClass.Text = "class=\"tab-pane\"";
        litTabGroupClass.Text = "class=\"tab-pane\"";
        litProfileClass.Text = "class='active'";
        litOverviewClass.Text = "";
        litTabProfileClass.Text = "class=\"tab-pane active\"";
    }

    protected void btnSendWelcomeEmail_Click(object sender, EventArgs e)
    {
        string strMessage = string.Empty;
        lblWelcomeEmailMessage.Text = string.Empty;

        int userID = Convert.ToInt32(Request.QueryString["userID"]);
        qPtl_User user = new qPtl_User(userID);

        if (user.UserID > 0)
        {
            if (!String.IsNullOrEmpty(user.Email))
            {
                if (user.SetPasswordResetCode(user.UserID))
                {
                    qPtl_User user2 = new qPtl_User(userID);
                    int welcomeEmailID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Email_WelcomeEmailID"]);
                    string publicURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CMS_URL"]);
                    string siteName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ShortName"]);
                    qCom_EmailTool etool = new qCom_EmailTool(welcomeEmailID, user.UserID);
                    Regex regParseable = new Regex("{FirstName}");
                    string emailContent = regParseable.Replace(etool.emailDraft, user.FirstName);
                    regParseable = new Regex("{UserID}");
                    emailContent = regParseable.Replace(emailContent, user.UserID.ToString());
                    regParseable = new Regex("{ResetCode}");
                    emailContent = regParseable.Replace(emailContent, user2.PasswordResetCode);
                    regParseable = new Regex("{SiteURL}");
                    emailContent = regParseable.Replace(emailContent, Request.Url.Authority + HttpRuntime.AppDomainAppVirtualPath);
                    int email_log_id = 0;
                    email_log_id = etool.SendDatabaseMail(user.Email, welcomeEmailID, user.UserID, user.UserName, user2.PasswordResetCode, Request.Url.Authority + HttpRuntime.AppDomainAppVirtualPath.TrimEnd('/'), string.Empty, string.Empty, false);
                    if (email_log_id > 0)
                        lblWelcomeEmailMessage.Text += "&nbsp;&nbsp;<i class=\"icon-check\"></i> Sent welcome message to: " + user.Email + " at " + DateTime.Now + "<br>";
                    else
                        lblWelcomeEmailMessage.Text += "&nbsp;&nbsp;<i class=\"icon-warning-sign\"></i> An error occured sending message for: " + user.Email + "<br>";
                }
            }
            else
            {
                lblWelcomeEmailMessage.Text += "** This user does not have an email on record -- welcome email cannot be sent ***<br><br>";
            }
        }

        lblWelcomeEmailMessage.Visible = true;
        //btnSendInvitation.Enabled = false;
    }
}