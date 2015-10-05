using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;

using Telerik.Web.UI;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Communication;
using Quartz.Learning;
using Quartz.Core;
using Quartz.Data;
using Quartz.Organization;
using System.Globalization;

public partial class controls_register : System.Web.UI.UserControl
{
    int regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MinAge"]);
    int regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MaxAge"]);
    bool invitation_required = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_InviteRequired"]);
    bool space_code_available = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_CodeAvailable"]);
    bool sms_available = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_SMSAvailable"]);
    bool race_available = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_RaceAvailable"]);
    bool race_required = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_RaceRequired"]);
    bool school_available = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_SchoolAvailable"]);
    bool school_required = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_SchoolRequired"]);
    string process_code_mode = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_CodeMode"]);
    bool space_redirect = false;
    int new_space_id = 0;

    private const int ItemsPerRequest = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateSchoolDistricts();

            if (!String.IsNullOrEmpty(Convert.ToString(Context.Items["UserID"])))
            {
                qPtl_User curr_user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
                if (curr_user.UserID > 0)
                    Response.Redirect("~/utilities/manage-user-access.aspx");
            }

            if (invitation_required == true)
                plhInvitation.Visible = true;
            else
                plhInvitation.Visible = false;

            if (space_code_available == true && invitation_required == false)
                plhSpaceCode.Visible = true;
            else
                plhSpaceCode.Visible = false;

            if (sms_available == true)
                plhMobileAvailable.Visible = false;     // HACK - always set to false
            else
                plhMobileAvailable.Visible = false;

            if (race_available == true)
                plhRace.Visible = true;
            else
                plhRace.Visible = false;

            if (school_available == true)
            {
                plhSchool.Visible = true;
                plhSchoolInfo.Visible = false;
            }
            else
            {
                plhSchool.Visible = false;
            }

            // see if registration code passed
            string reg_code = string.Empty;

            // check to see if matches space
            if (!String.IsNullOrEmpty(Request.QueryString["code"]))
            {
                reg_code = Request.QueryString["code"];
                qSoc_Space space = new qSoc_Space(reg_code);

                if (space.SpaceID > 0)
                {
                    txtSpaceCode.Text = reg_code;
                    lblSpaceDescription.Text = "<br><span class=\"label label-success\"><i class=\"icon-ok\"></i>&nbsp;" + space.SpaceShortName + " group.</span><br>";
                }

                // if no space match, then check to see if it matches a campaign
                if (space.SpaceID == 0)
                {
                    qSoc_Campaign campaign = new qSoc_Campaign(reg_code);

                    if (campaign.CampaignID > 0)
                    {
                        txtSpaceCode.Text = reg_code;
                        lblSpaceDescription.Text = "<br><span class=\"label label-success\"><i class=\"icon-ok\"></i>&nbsp;" + campaign.CampaignName + " channel.</span><br>";
                    }
                }
            }

            // populate the year dropdown
            DateTime currDate = DateTime.Now;
            int currYear = Convert.ToInt32(currDate.Year);
            int startYear = Convert.ToInt32(currDate.Year) - 108;
            DataTable dtYears = new DataTable();
            DataColumn year = new DataColumn("Year");
            year.DataType = System.Type.GetType("System.Int32");
            dtYears.Columns.Add(year);

            for (int i = startYear; i <= currYear; i++)
            {
                DataRow row = dtYears.NewRow();
                row[year] = i;
                dtYears.Rows.Add(row);
            }

            dtYears.DefaultView.Sort = "Year DESC";

            ddlYear.DataSource = dtYears;
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("", string.Empty));

            ddlMonth.SelectedIndex = 0;
            ddlDay.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;

            // configure gender
            string gender_options = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_SupportedGenderValues"]);
            ddlGender.Items.FindByValue("Intersex").Enabled = false;
            ddlGender.Items.FindByValue("Transgender").Enabled = false;
            if (gender_options.Contains("Intersex"))
                ddlGender.Items.FindByValue("Intersex").Enabled = true;
            if (gender_options.Contains("Transgender"))
                ddlGender.Items.FindByValue("Transgender").Enabled = true;

            // look for custom home value
            string hplBack_custom = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_PublicHomePage"]);
            if (!String.IsNullOrEmpty(hplBack_custom))
                hplBack.NavigateUrl = hplBack_custom;
            else
                hplBack.NavigateUrl = "~/default.aspx";
        }
    }

    protected void populateSchoolDistricts()
    {
        ddlSchoolDistrict.DataSource = qOrg_SchoolDistrict.GetSchoolDistricts();
        ddlSchoolDistrict.DataTextField = "DistrictName";
        ddlSchoolDistrict.DataValueField = "SchoolDistrictID";
        ddlSchoolDistrict.DataBind();
        ddlSchoolDistrict.Items.Insert(0, new ListItem("", string.Empty));
        int num_school_districts = ddlSchoolDistrict.Items.Count;
        ddlSchoolDistrict.Items.Insert(num_school_districts, new ListItem("Other", "-1"));
    }

    protected void ValidateEverything(object obj, ServerValidateEventArgs args)
    {
        string error_msg = string.Empty;
        bool error_occurred = false;

        // validate first name
        if (String.IsNullOrEmpty(txtFirstName.Text))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "first name missing";
        }

        // validate last name
        if (String.IsNullOrEmpty(txtLastName.Text))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "last name missing";
        }

        // validate email -- not empty, meets rege+x, not used
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
            var user = qPtl_User.GetUserByEmail(txtEmail.Text);

            if (user != null)
            {
                if (user.UserID > 0)
                {
                    error_occurred = true;
                    if (!String.IsNullOrEmpty(error_msg))
                        error_msg += ", ";
                    error_msg += "email already being used";
                }
            }
        }

        // validate username -- not used and matches standard
        if (String.IsNullOrEmpty(txtUserName.Text))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "username missing";
        }
        string pat_u = @"^[a-zA-Z0-9]+$";
        Regex r_u = new Regex(pat_u, RegexOptions.IgnoreCase);
        Match m_u = r_u.Match(txtUserName.Text);
        if (!m_u.Success)
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "username has characters that are not allowed - use only letters and/or numbers";
        }
        bool username_available = CheckUserName(txtUserName.Text);
        if (username_available == false)
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "username is already taken";
        }

        // validate password -- meets standard and they match
        if (String.IsNullOrEmpty(txtPassword.Text) || String.IsNullOrEmpty(txtPasswordConfirm.Text) || (txtPassword.Text != txtPasswordConfirm.Text))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "passwords must match";
        }
        if (txtPassword.Text.Length < 6)
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "passwords must be more than 6 characters";
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
        if (String.IsNullOrEmpty(ddlMonth.SelectedValue))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "month missing";
        }
        if (String.IsNullOrEmpty(ddlDay.SelectedValue))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "day missing";
        }
        if (String.IsNullOrEmpty(ddlYear.SelectedValue))
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "month missing";
        }

        bool is_age_valid = CheckAge();
        if (is_age_valid == false)
        {
            error_occurred = true;
            if (!String.IsNullOrEmpty(error_msg))
                error_msg += ", ";
            error_msg += "you do not meet the age requirements";
        }

        // check invitation -- ONLY if visible in form; does text exist & is it a valid invitation
        if (plhInvitation.Visible == true)
        {
            if (String.IsNullOrEmpty(txtInvitationCode.Text))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "invitation code missing";
            }

            bool is_invitation_valid = CheckInvitation(txtInvitationCode.Text);
            if (is_invitation_valid == false)
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "invitation code not valid";
            }
        }

        if (plhMobileNumber.Visible == true)
        {
            if (String.IsNullOrEmpty(txtMobileNumber.Text))
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "mobile phone missing";
            }
            string pat_m = @"^[0-9]{10}$";
            Regex r_m = new Regex(pat_m, RegexOptions.IgnoreCase);
            Match m_m = r_m.Match(txtMobileNumber.Text);
            if (!m_m.Success)
            {
                error_occurred = true;
                if (!String.IsNullOrEmpty(error_msg))
                    error_msg += ", ";
                error_msg += "mobile phone number is not in correct format";
            }
        }

        // final analysis
        if (error_occurred == true)
        {
            args.IsValid = false;
            lblMsg.Text = "<br><br>Problems: " + error_msg + " ";
            lblMsg.Visible = true;
            lblMsgTop.Text = "Problems: " + error_msg + " *<br><br>";
            lblMsgTop.Visible = true;

        }

    }

    protected bool CheckInvitation(string invite_code)
    {
        bool invitation_valid = false;

        if (qPtl_Invitation.InvitationValid(invite_code))
            invitation_valid = true;

        return invitation_valid;
    }

    protected bool CheckUserName(string username)
    {
        bool username_available = false;

        qPtl_User user = new qPtl_User(username);

        if (user != null)
        {
            if (user.UserID > 0)
                username_available = false;
            else
                username_available = true;
        }
        else
            username_available = true;

        return username_available;
    }

    protected bool CheckAge()
    {
        bool valid_age = false;

        // if invitation is being used then get associated role; then check age range based on role
        if (plhInvitation.Visible == true && !String.IsNullOrEmpty(txtInvitationCode.Text))
        {
            qPtl_Invitation invite = new qPtl_Invitation(txtInvitationCode.Text);
            if (invite != null)
            {
                if (invite.RoleID > 0)
                {
                    qPtl_Role role = new qPtl_Role(invite.RoleID);

                    if (role != null)
                    {
                        if (role.RoleName.Contains("Teen"))  // use teen age ELSE assume adult user
                        {
                            regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MinAge"]);
                            regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenMaxAge"]);
                        }
                        else
                        {
                            regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenMaxAge"]) + 1;
                            regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MaxAge"]);
                        }
                    }
                }
            }
        }
        else
        {
            regMinAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MinAge"]);
            regMaxAge = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_MaxAge"]);
        }

        try
        {
            string selectedMonth = string.Empty;
            if (!String.IsNullOrEmpty(Convert.ToString(ddlMonth.SelectedValue)))
                selectedMonth = Convert.ToString(ddlMonth.SelectedValue);
            string selectedDay = string.Empty;
            if (!String.IsNullOrEmpty(Convert.ToString(ddlDay.SelectedValue)))
                selectedDay = Convert.ToString(ddlDay.SelectedValue);

            //DateTime DOB = Convert.ToDateTime(selectedMonth + "/" + selectedDay + "/" + ddlYear.SelectedValue);
            DateTime DOB = new DateTime(int.Parse(ddlYear.SelectedValue),int.Parse( ddlMonth.SelectedValue), int.Parse(ddlDay.SelectedValue));
            DateTime currDate = DateTime.Now;
            int numYears = 0;
            try
            {
                /*TimeSpan age = currDate.Subtract(DOB);
                numYears = (age.Days / 365);*/
                numYears = currDate.Year - DOB.Year;
                if (currDate.Month < DOB.Month) numYears--;
                else if (currDate.Month == DOB.Month && currDate.Day < DOB.Day) numYears--;

            }
            catch
            {
                valid_age = false;
            }

            if (numYears >= regMinAge && numYears <= regMaxAge)
            {
                valid_age = true;
            }
            else
            {
                valid_age = false;
            }
        }
        catch
        {
            valid_age = false;
        }

        return valid_age;
    }

    protected void chkAlreadyMobile_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAlreadyMobile.Checked == true)
            plhMobileNumber.Visible = true;
        else
            plhMobileNumber.Visible = false;
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Page.Validate("register");

        if (Page.IsValid)
        {
            string sqlCode = string.Empty;
            string returnMessage = string.Empty;
            qDbs_SQLcode sql = new qDbs_SQLcode();
            string register_mode = "new";
            int existing_user_id = 0;
            int scope_id = 1;   // would have to be changed to support multiple organizations on a single platform
            int role_id = 0;
            int moderator_role_id = 0;
            if (!String.IsNullOrEmpty(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_ModeratorRoleID"])))
                moderator_role_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_ModeratorRoleID"]);


            // ****************************************************
            // STEP 1: DETERMINE IF USER IS ALREADY A LIMITED MOBILE USER
            if (plhMobileNumber.Visible == true)
            {
                //Determine if upgrade of mobile account or new registration
                qPtl_UserProfile mobile_test = qPtl_UserProfile.GetProfileByMobileNumber(txtMobileNumber.Text);

                if (mobile_test != null)
                {
                    if (mobile_test.UserID > 0)
                    {
                        // make sure that the user is eligible for upgrade from mobile only status
                        qPtl_User eval_user = new qPtl_User(mobile_test.UserID);

                        if (eval_user.HighestRole == "Mobile")
                        {
                            register_mode = "update";
                            existing_user_id = mobile_test.UserID;
                        }
                    }
                    else
                    {
                        register_mode = "new";
                    }
                }
                else
                {
                    register_mode = "new";
                }
            }


            // ****************************************************
            // STEP 2a: Mode == new; then add new user
            string currAvailableStatus = string.Empty;
            currAvailableStatus = "Yes";

            if (register_mode == "new")
            {
                qPtl_User new_user = new qPtl_User();
                new_user.Available = "Yes";
                new_user.ScopeID = scope_id;
                new_user.Created = DateTime.Now;
                new_user.CreatedBy = 0;
                new_user.LastModified = DateTime.Now;
                new_user.LastModifiedBy = 0;
                new_user.MarkAsDelete = 0;
                new_user.Status = "";       // used to include a default message for their status, now leave blank
                new_user.FirstName = txtFirstName.Text;
                new_user.LastName = txtLastName.Text;
                new_user.Email = txtEmail.Text;
                new_user.UserName = txtUserName.Text;
                string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                new_user.Password = password_for_storing;
                new_user.AccountStatus = "Active";
                new_user.Insert();
                existing_user_id = new_user.UserID;

                DateTime DOB;
                try
                {
                    DOB = new DateTime(int.Parse(ddlYear.SelectedValue), int.Parse(ddlMonth.SelectedValue), int.Parse(ddlDay.SelectedValue)); ;//Convert.ToDateTime(ddlMonth.SelectedValue + "/" + ddlDay.SelectedValue + "/" + ddlYear.SelectedValue);
                }
                catch
                {
                    // no valid date so use default value
                    DOB = new DateTime(1900, 1, 1); ////Convert.ToDateTime("1/1/1900");
                }
                qPtl_UserProfile new_profile = new qPtl_UserProfile();
                new_profile.UserID = existing_user_id;
                new_profile.ScopeID = scope_id;
                new_profile.Available = "Yes";
                new_profile.Created = DateTime.Now;
                new_profile.CreatedBy = existing_user_id;
                new_profile.LastModified = DateTime.Now;
                new_profile.LastModifiedBy = existing_user_id;
                new_profile.MarkAsDelete = 0;
                new_profile.Style = "default";
                new_profile.Visibility = "all";
                new_profile.Gender = ddlGender.SelectedValue;
                new_profile.DOB = DOB;

                if (plhRace.Visible == true)
                {
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
                    new_profile.Race = race;
                }
                new_profile.Insert();
            }
            qPtl_User user = new qPtl_User(existing_user_id);

            // STEP 2b: Mode == update; then update mobile info
            //mode mobile = update existing account
            if (register_mode == "update")
            {
                user.FirstName = txtFirstName.Text;
                user.LastName = txtLastName.Text;
                user.UserName = txtEmail.Text;
                user.Email = txtEmail.Text;
                string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                user.Password = password_for_storing;
                user.AccountStatus = "Mobile Verification Pending";
                user.Update();
            }


            // ****************************************************
            // STEP 3: Process invitation & perform invitation-specific work
            // OPTION #1 -- an invitation is required as part of the registration process and a collection of additional actions are related to that 
            string code = string.Empty;
            string process_code_mode = string.Empty;
            if (plhInvitation.Visible == true)
            {
                code = txtInvitationCode.Text;
                if (String.IsNullOrEmpty(process_code_mode))
                    process_code_mode = "invitation";                // deal with default/null values
            }
            else if (plhSpaceCode.Visible == true)
            {
                code = txtSpaceCode.Text;
                if (String.IsNullOrEmpty(process_code_mode))
                {
                    qSoc_Space space = new qSoc_Space(code);

                    if (space.SpaceID > 0)
                    {
                        process_code_mode = "space";
                    }
                    else
                    {
                        qSoc_Campaign campaign = new qSoc_Campaign(code);

                        if (campaign.CampaignID > 0)
                        {
                            process_code_mode = "campaign";
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(code) && process_code_mode == "invitation")
            {
                //Process invitation
                int invitationID = 0;
                qPtl_Invitation invite = null;
                invite = new qPtl_Invitation(code);
                invitationID = invite.InvitationID;
                role_id = invite.RoleID;

                // update invitation records as necessary
                if (invite.InvitationAudience == "family")
                {
                    invite.UserID = user.UserID;
                    invite.InvitationStatus = "Redeemed";
                    invite.LastModified = DateTime.Now;
                    invite.LastModifiedBy = user.UserID;
                    invite.CurrRedemptions = invite.CurrRedemptions + 1;
                }
                else if (invite.InvitationAudience == "individual")
                {
                    invite.UserID = user.UserID;
                    invite.InvitationStatus = "Redeemed";
                    invite.LastModified = DateTime.Now;
                    invite.LastModifiedBy = user.UserID;
                    invite.CurrRedemptions = invite.CurrRedemptions + 1;
                }
                else if (invite.InvitationAudience == "group")
                {
                    invite.UserID = user.UserID;
                    invite.LastModified = DateTime.Now;
                    invite.LastModifiedBy = user.UserID;
                    invite.CurrRedemptions = invite.CurrRedemptions + 1;

                    if (invite.MaxRedemptions > 0 && invite.CurrRedemptions >= invite.MaxRedemptions)
                        invite.InvitationStatus = "Redeemed";
                }
                else if (invite.InvitationAudience == "moderated group")
                {
                    invite.UserID = user.UserID;
                    invite.LastModified = DateTime.Now;
                    invite.LastModifiedBy = user.UserID;
                    invite.CurrRedemptions = invite.CurrRedemptions + 1;

                    if (invite.MaxRedemptions > 0 && invite.CurrRedemptions >= invite.MaxRedemptions)
                        invite.InvitationStatus = "Redeemed";
                }
                invite.Update();

                // add possible space associated with invitation & space-specific elements

                if (invite.SpaceID > 0)
                {
                    qSoc_Space space = new qSoc_Space(invite.SpaceID);
                    if (space != null)
                    {
                        if (space.SpaceID > 0)
                        {
                            qSoc_UserSpace u_space = new qSoc_UserSpace();
                            u_space.ScopeID = scope_id;
                            u_space.Available = "Yes";
                            u_space.Created = DateTime.Now;
                            u_space.CreatedBy = user.UserID;
                            u_space.LastModified = DateTime.Now;
                            u_space.LastModifiedBy = user.UserID;
                            u_space.MarkAsDelete = 0;
                            u_space.UserID = user.UserID;
                            u_space.SpaceID = space.SpaceID;
                            u_space.PrimarySpace = true;
                            if (role_id == moderator_role_id)
                                u_space.SpaceRole = "Moderator";
                            u_space.Insert();
                        }

                        if (space.SchoolID > 0)
                        {
                            qOrg_UserSchool school = new qOrg_UserSchool();
                            school.UserID = user.UserID;
                            school.SchoolID = space.SchoolID;
                            school.Insert();
                        }

                        // set space to visibile in directory it not already visible
                        if (space.VisibleInDirectory == "No")
                        {
                            space.VisibleInDirectory = "Yes";
                            space.Update();
                        }


                        // add campaign -- includes check to insure that campaigns aren't added twice
                        AddSpaceCampaigns(space, user, scope_id);
                    }
                }

                // add possible campaign associated with invitation -- includes check to insure that campaigns aren't added twice
                if (invite.CampaignID > 0)
                {
                    qSoc_UserCampaign checkc = new qSoc_UserCampaign(user.UserID, invite.CampaignID);
                    if (checkc.UserCampaignID == 0)
                    {
                        AddUserCampaign(invite.CampaignID, scope_id, user);
                    }
                }

                if (invite.FunctionalRoleID > 0)
                {
                    sqlCode = "INSERT INTO qLrn_UserFunctionalRoles ([UserID],[FunctionalRoleID]) VALUES(" + user.UserID + "," + invite.FunctionalRoleID + ")";
                    sql.ExecuteSQL(sqlCode);

                    UserFunctions.AddUserTrainingsByFunctionalRole(user.UserID, invite.FunctionalRoleID);
                }
            }
            else
            {
                // else use default role types
                int teen_max_age = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenMaxAge"]);

                qPtl_UserProfile profile = new qPtl_UserProfile(user.UserID);
                if (profile.Age <= teen_max_age)
                    role_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_TeenRoleID"]);
                else
                    role_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Register_ParentRoleID"]);
            }

            // OPTION #2 -- User might have entered a registration code; this will assign them to a space which might have one or more associated campaigns
            if (!String.IsNullOrEmpty(code) && process_code_mode == "space")
            {
                if (!string.IsNullOrEmpty(code))
                {
                    qSoc_Space space = new qSoc_Space(code);

                    if (space != null)
                    {
                        if (space.SpaceID > 0)
                        {
                            qSoc_UserSpace u_space = new qSoc_UserSpace();
                            u_space.ScopeID = scope_id;
                            u_space.Available = "Yes";
                            u_space.Created = DateTime.Now;
                            u_space.CreatedBy = user.UserID;
                            u_space.LastModified = DateTime.Now;
                            u_space.LastModifiedBy = user.UserID;
                            u_space.MarkAsDelete = 0;
                            u_space.UserID = user.UserID;
                            u_space.SpaceID = space.SpaceID;
                            u_space.PrimarySpace = true;
                            if (role_id == moderator_role_id)
                                u_space.SpaceRole = "Moderator";
                            u_space.Insert();
                        }

                        space_redirect = true;
                        new_space_id = space.SpaceID;

                        AddSpaceCampaigns(space, user, scope_id);
                    }
                }
            }
            else if (!String.IsNullOrEmpty(code) && process_code_mode == "campaign")
            {
                qSoc_Campaign campaign = new qSoc_Campaign(code);
                
                AddUserCampaign(campaign.CampaignID, scope_id, user);
            }

            // ****************************************************
            // STEP 4: Add School Information
            if (plhSchool.Visible == true)
            {
                string school_name = string.Empty;
                string school_other_name = string.Empty;

                if (radCBSearch.Visible == true)
                    school_name = radCBSearch.SelectedValue;
                else
                {
                    school_other_name = txtSchoolOther.Text;
                    school_name = "Other";
                }

                var school = qOrg_School.GetSchoolFromAutoPopulateDropdown(school_name);

                if (school != null)
                {
                    if (school.SchoolID > 0)
                    {
                        qOrg_UserSchool user_school = new qOrg_UserSchool();
                        user_school.Available = "Yes";
                        user_school.ScopeID = scope_id;
                        user_school.Created = DateTime.Now;
                        user_school.CreatedBy = user.UserID;
                        user_school.LastModified = DateTime.Now;
                        user_school.LastModifiedBy = user.UserID;
                        user_school.MarkAsDelete = 0;
                        user_school.UserID = user.UserID;
                        user_school.SchoolID = school.SchoolID;
                        user_school.OtherName = school_other_name;
                        user_school.Insert();

                        // see if this school is already a space
                        var school_space = qSoc_Space.GetSpacesBySchool(school.SchoolID);

                        qSoc_Space curr_space = new qSoc_Space();
                        if (school_space == null)
                        {
                            qSoc_Space new_space = new qSoc_Space();
                            new_space.ScopeID = scope_id;
                            new_space.Available = "Yes";
                            new_space.Created = DateTime.Now;
                            new_space.CreatedBy = 0;
                            new_space.LastModified = DateTime.Now;
                            new_space.LastModifiedBy = 0;
                            new_space.MarkAsDelete = 0;
                            new_space.SpaceName = school.School;
                            new_space.SpaceShortName = school.School;
                            new_space.SpaceType = "school";
                            new_space.AccessMode = "open";
                            new_space.VisibleInDirectory = "Yes";
                            new_space.SpaceCategoryID = 1;
                            new_space.SchoolID = school.SchoolID;
                            new_space.Insert();

                            school_space = new_space;
                        }

                        if (school_space != null)
                        {
                            qSoc_UserSpace s_space = new qSoc_UserSpace();
                            s_space.ScopeID = scope_id;
                            s_space.Available = "Yes";
                            s_space.Created = DateTime.Now;
                            s_space.CreatedBy = user.UserID;
                            s_space.LastModified = DateTime.Now;
                            s_space.LastModifiedBy = user.UserID;
                            s_space.MarkAsDelete = 0;
                            s_space.UserID = user.UserID;
                            s_space.SpaceID = school_space.SpaceID;
                            s_space.PrimarySpace = true;
                            if (role_id == moderator_role_id)
                                s_space.SpaceRole = "Moderator";
                            s_space.Insert();
                        }
                    }
                }
            }

            // ****************************************************
            // STEP 5: Add User Role & Supporting Role Structures
            // Add role
            /*
            qPtl_UserRole role = new qPtl_UserRole();
            role.UserID = user.UserID;
            role.RoleID = role_id;
            role.Insert();
             */
            sqlCode = "INSERT INTO qPtl_UserRoles ([UserID],[RoleID]) VALUES(" + user.UserID + "," + role_id + ")";
            sql.ExecuteSQL(sqlCode);

            // Add possible role actions for the new user role
            AddRoleAction(role_id, scope_id, user);

            // Add possible role campaigns
            AddRoleCampaigns(role_id, scope_id, user);

            // Redundancy check -- write Highest Level into qPtl_User table in case DB trigger not working
            qPtl_Role role = new qPtl_Role(role_id);
            user.HighestRank = role.RoleRank;
            user.HighestRole = role.RoleName;
            user.Update();


            // ****************************************************
            // STEP 6: User Utilties to finalize/prep account
            // Add username folder for images
            string rootLocation = Server.MapPath("~/") + "user_data\\";

            if (!Directory.Exists(rootLocation + user.UserName))
                Directory.CreateDirectory(rootLocation + user.UserName);

            // Create default album
            qSoc_Album album = new qSoc_Album();
            album.ScopeID = scope_id;
            album.Available = "Yes";
            album.Created = DateTime.Now;
            album.CreatedBy = user.UserID;
            album.LastModified = DateTime.Now;
            album.LastModifiedBy = user.UserID;
            album.MarkAsDelete = 0;
            album.UserID = user.UserID;
            album.Name = "My Pics";
            album.Insert();

            // Add communications preferences
            if (register_mode == "new")
            {
                qCom_UserPreference connect = new qCom_UserPreference();
                connect.UserID = user.UserID;
                connect.Created = DateTime.Now;
                connect.CreatedBy = user.UserID;
                connect.LastModified = DateTime.Now;
                connect.LastModifiedBy = user.UserID;
                connect.Available = "Yes";
                connect.ScopeID = 1;
                connect.MarkAsDelete = 0;
                connect.OkBulkEmail = "Yes";
                connect.OkEmail = "Yes";
                connect.OkSms = "Yes";
                connect.LanguageID = 1;
                connect.Insert();
            }

            qCom_UserPreference user_connect = qCom_UserPreference.GetUserPreference(user.UserID);
            user_connect.OkBulkEmail = "Yes";
            user_connect.OkEmail = "Yes";
            user_connect.Update();


            // ****************************************************
            // STEP 7: Log user in and redirect to account setup page/
            // initial session created -- will last for 24 hours before timing out
            qPtl_Sessions session = new qPtl_Sessions();
            session.Created = DateTime.Now;
            session.StartTime = DateTime.Now;
            session.LastTimeSeen = DateTime.Now;
            session.ScopeID = user.ScopeID;
            session.UserID = user.UserID;
            session.BrowserType = Request.Browser.Browser;
            session.ComputerType = Request.Browser.Platform;
            session.Insert();
            int sessionID = session.SessionID;
            int scopeID = user.ScopeID;

            var u_roles = qPtl_UserRole_View.GetUserRoles(user.UserID);
            string role_list = string.Empty;

            if (u_roles != null)
            {
                foreach (var u in u_roles)
                {
                    role_list += string.Format("{0},", u.RoleName, ",");
                }
            }

            role_list.TrimEnd(',');

            string userData = string.Format("{0};{1};{2}", sessionID, role_list, scopeID);

            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            tkt = new FormsAuthenticationTicket(1, user.UserID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(3600), false, userData);
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);

            HttpCookie cookie2 = new HttpCookie("UserID", Convert.ToString(user.UserID));
            if (tkt.IsPersistent) { cookie2.Expires = tkt.Expiration.AddMinutes(3605); }
            Response.Cookies.Add(cookie2);

            if (space_redirect == true && new_space_id > 0)
                Response.Redirect("~/utilities/manage-user-access.aspx?mode=registration&spaceID=" + new_space_id);
            else
                Response.Redirect("~/utilities/manage-user-access.aspx?mode=registration");
        }
    }

    protected void AddRoleCampaigns(int role_id, int scope_id, qPtl_User user)
    {
        //Add role campaigns
        string sqlCode = string.Empty;
        string returnMessage = string.Empty;
        qDbs_SQLcode sql = new qDbs_SQLcode();

        sqlCode = string.Format("SELECT * FROM qSoc_RoleCampaigns WHERE RoleID = " + role_id + " AND Available = 'Yes' AND ScopeID = " + scope_id);

        int campaignID = 0;
        string progressMode = string.Empty;

        using (SqlDataReader sReader = sql.GetDataReader(sqlCode))
        {
            if (sReader.HasRows)
            {
                while (sReader.Read())
                {
                    campaignID = Convert.ToInt32(sReader["CampaignID"]);

                    qSoc_UserCampaign checkc = new qSoc_UserCampaign(user.UserID, campaignID);
                    if (checkc.UserCampaignID == 0)
                    {
                        //create campaign for user
                        qSoc_UserCampaign campaign = new qSoc_UserCampaign();
                        qSoc_Campaign cam = new qSoc_Campaign(campaign.CampaignID);

                        campaign.ScopeID = 1;
                        campaign.CampaignID = campaignID;
                        campaign.Available = "Yes";
                        campaign.Created = DateTime.Now;
                        campaign.CreatedBy = user.UserID;
                        campaign.UserID = user.UserID;
                        campaign.CampaignStatus = "In Progress";
                        // following parameters are defaults based on these campaigns being created associated to user roles and not spaces
                        campaign.SpaceID = 0;
                        campaign.EnrollmentType = "role";
                        campaign.Enrolled = DateTime.Now;
                        if (cam.SetupRequired != true)
                        {
                            campaign.CampaignStart = DateTime.Now;
                        }

                        //add campaign actions
                        int j = 0;
                        var cActions = qSoc_CampaignAction.GetCampaignActions(campaignID);
                        foreach (var i in cActions)
                        {
                            // get values
                            qSoc_UserCampaignAction cUserAction = new qSoc_UserCampaignAction();

                            cUserAction.UserID = user.UserID;
                            cUserAction.ScopeID = 1;
                            cUserAction.Available = "Yes";
                            cUserAction.Created = DateTime.Now;
                            cUserAction.CreatedBy = 0;
                            cUserAction.CampaignActionID = i.CampaignActionID;
                            if (j == 0)
                            {
                                cUserAction.Status = "In Progress";
                                campaign.CurrentCampaignActionID = i.CampaignActionID;
                            }
                            else
                            {
                                cUserAction.Status = "Not Started";
                            }

                            cUserAction.Insert();

                            j++;
                        }

                        // create campaign record now that have current campaign actionID
                        campaign.Insert();
                    }
                }
            }
        }
    }

    protected void AddRoleAction(int role_id, int scope_id, qPtl_User user)
    {
        var role_actions = qPtl_RoleAction.GetAvailableRoleActionsByRole(role_id, scope_id);
        DateTime startTime = DateTime.Now;
        DateTime endTime = DateTime.Now;

        foreach (var a in role_actions)
        {
            qPtl_UserAction action = new qPtl_UserAction();
            qPtl_Action act = new qPtl_Action(a.ActionID);
            action.ScopeID = scope_id;
            action.Available = "Yes";
            action.Created = DateTime.Now;
            action.CreatedBy = user.UserID;
            action.LastModified = DateTime.Now;
            action.LastModifiedBy = user.UserID;
            action.MarkAsDelete = 0;
            action.UserID = user.UserID;
            action.ActionID = a.ActionID;
            startTime = startTime.AddDays(a.StartDaysFromNow);
            endTime = endTime.AddDays(a.EndDaysFromNow);
            action.AvailableFrom = startTime;
            action.AvailableTo = endTime;
            action.AfterNumLogins = a.AfterNumLogins;
            action.Priority = a.Priority;
            action.SkipAllowed = a.SkipAllowed;
            action.NumberSkipsAllowed = a.NumberSkipsAllowed;
            action.Required = a.Required;
            action.OptionalOptOut = a.OptionOptOut;
            action.RedirectSkipURL = a.RedirectSkipURL;
            action.RedirectURL = a.RedirectURL;
            action.ReferenceID = act.ReferenceID;
            action.Insert();
        }
    }

    protected void AddSpaceCampaigns(qSoc_Space space, qPtl_User user, int scope_id)
    {
        //Add space campaigns
        string sqlCode = string.Empty;
        string returnMessage = string.Empty;
        qDbs_SQLcode sql = new qDbs_SQLcode();

        sqlCode = string.Format("SELECT * FROM qSoc_SpaceCampaigns WHERE SpaceID = " + space.SpaceID + " AND Available = 'Yes' AND ScopeID = " + scope_id);

        int campaignID = 0;
        string progressMode = string.Empty;

        using (SqlDataReader sReader = sql.GetDataReader(sqlCode))
        {
            if (sReader.HasRows)
            {
                while (sReader.Read())
                {
                    campaignID = Convert.ToInt32(sReader["CampaignID"]);

                    qSoc_UserCampaign checkc = new qSoc_UserCampaign(user.UserID, campaignID);
                    if (checkc.UserCampaignID == 0)
                    {
                        //create campaign for user
                        qSoc_Campaign campaign = new qSoc_Campaign(campaignID);

                        AddUserCampaign(campaign.CampaignID, scope_id, user);
                    }
                }
            }
        }
    }

    protected void AddUserCampaign(int campaign_id, int scope_id, qPtl_User user)
    {
        DateTime start_date = new DateTime();
        start_date = DateTime.Now;

        qSoc_UserCampaign new_campaign = qSoc_UserCampaign.EnrollUserInCampaign(user.UserID, campaign_id, "controlled", start_date, Convert.ToInt32(Context.Items["ScopeID"]), "role", "Yes", "Yes", "Yes", "Yes");
    }

    protected void radCBSearch_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
    {
        DataRow[] dataRows;

        //if (Cache["dtSchools"] == null)
        //{
        var schools = qOrg_School.GetSchoolsWithLongName();
        Cache["dtSchools"] = schools;
        //}

        if (e.Text == string.Empty)
        {
            dataRows = ((DataTable)Cache["dtSchools"]).Select();
        }
        else
        {
            dataRows = ((DataTable)Cache["dtSchools"]).Select(string.Format("School LIKE '%{0}%'", escapeLikeValue(e.Text)));
        }

        int itemOffset = e.NumberOfItems;
        int endOffset = Math.Min(itemOffset + ItemsPerRequest, dataRows.Count());
        e.EndOfItems = endOffset == dataRows.Count();

        for (int i = itemOffset; i < endOffset; i++)
        {
            radCBSearch.Items.Add(new RadComboBoxItem(dataRows[i]["School"].ToString(), dataRows[i]["School"].ToString()));
        }

        e.Message = GetStatusMessage(endOffset, dataRows.Count());
    }

    private static string GetStatusMessage(int offset, int total)
    {
        if (total <= 0)
            return "No matches";

        return String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", offset, total);
    }

    public static string escapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }

    protected void radCBSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        // get the school info that matches the selection

    }

    protected void ddlSchoolDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        plhSchoolInfo.Visible = true;

        txtSchoolOther.Visible = false;
        radCBSearch.Visible = false;
        if (ddlSchoolDistrict.SelectedValue == "-1")
            txtSchoolOther.Visible = true;
        else
            radCBSearch.Visible = true;

    }
}