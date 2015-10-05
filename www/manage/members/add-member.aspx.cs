using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;
using Quartz.Learning;
using Quartz.Data;

public partial class manage_members_add_member : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateRoles();

            string nav_mode = System.Configuration.ConfigurationManager.AppSettings["Site_NavMode"];
            if (nav_mode == "lms")
            {
                plhFunctionalRoles.Visible = true;
                imgFunctionalRoles.ImageUrl = "~/resources/learning/functional-roles.jpg";
                populateFunctionalRoles();
            }
            else
            {
                plhFunctionalRoles.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            // step 1: create account using registration model (single user role)
            int curr_default_role_id = Convert.ToInt32(ddlUserRoles.SelectedValue);

            RegistrationData data = new RegistrationData();
            data.scope_id = 1;
            data.invite_code = "";
            data.space_code = "";
            data.campaign_code = "";
            data.mobile_number = "";
            data.email = txtEmail.Text;
            data.username = txtUserName.Text;
            data.password = "";
            data.firstname = txtFirstName.Text;
            data.lastname = txtLastName.Text;
            data.degrees = "";
            data.position = "";
            data.agency = "";
            data.division = "";
            data.address = "";
            data.address2 = "";
            data.city = "";
            data.state = "";
            data.postal_code = "";
            data.work_phone = "";
            data.first_event = "";
            data.dob = "";
            data.gender = "";
            data.ethnicity = "";
            data.race = "";
            data.profession = "";
            data.employment_setting = "";
            data.employment_location = "";
            data.employment_sites = "";
            data.registration_type = "manager";
            data.registration_notes = "";
            data.default_role_id = curr_default_role_id;
            data.browser = "";
            data.platform = "";

            qPtl_User user = new qPtl_User();
            user = UserFunctions.RegisterNewUser(data);

            user.RegistrationNotes = txtRegistrationNotes.Text;
            user.RegistrationType = ddlRegistrationTypes.SelectedValue;
            user.Update();

            // process functional roles
            if (plhFunctionalRoles.Visible == true)
            {
                string sqlCode = string.Empty;
                string returnMessage = string.Empty;
                qDbs_SQLcode sql = new qDbs_SQLcode();

                // first delete all existing roles
                sqlCode = "DELETE FROM qLrn_UserFunctionalRoles WHERE UserID = " + user.UserID;
                sql.ExecuteSQL(sqlCode);

                // create records for all new roles
                int n;
                string selectedItems = string.Empty;

                n = 0;
                foreach (ListItem item in cblFunctionalRoles.Items)
                {
                    if (item.Selected)
                    {
                        sqlCode = "INSERT INTO qLrn_UserFunctionalRoles (UserID, FunctionalRoleID)";
                        sqlCode += " VALUES (" + user.UserID + "," + item.Value + ")";
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

                int daysBetweenTrainings = 0;
                if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Learning_DaysBetweenTrainings"]))
                    daysBetweenTrainings = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Learning_DaysBetweenTrainings"]);
                int daysTillUnavailable = 5000;
                DateTime seedDate = DateTime.Now;
                string trainingMode = "open";
                string surveyRequired = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Learning_SurveyRequired"]);

                qLrn_UserTraining.manageUserTrainings(user.UserID, daysBetweenTrainings, daysTillUnavailable, trainingMode, "add", 0, seedDate, surveyRequired);

                // redirect to new user tools page
                Response.Redirect("member-profile.aspx?userID=" + user.UserID);
            }
        }
    }

    protected void populateRoles()
    {
        ddlUserRoles.DataSource = qPtl_Role.GetRoles();
        ddlUserRoles.DataTextField = "RoleName";
        ddlUserRoles.DataValueField = "RoleID";
        ddlUserRoles.DataBind();
        ddlUserRoles.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateFunctionalRoles()
    {
        cblFunctionalRoles.DataSource = qLrn_FunctionalRole.GetFunctionalRoles();
        cblFunctionalRoles.DataValueField = "FunctionalRoleID";
        cblFunctionalRoles.DataTextField = "FunctionalRoleName";
        cblFunctionalRoles.DataBind();
    }

    protected void ValidateUserName(object obj, ServerValidateEventArgs args)
    {
        // try and find a matching username in the database
        string strUserName = txtUserName.Text.Trim();
        qPtl_User user = new qPtl_User(strUserName);

        if (user.UserID > 0)
        {
            // the user name exists
            args.IsValid = false;
        }
        else
            args.IsValid = true;
    }

    protected void ValidateEmailAddress(object obj, ServerValidateEventArgs args)
    {
        // try and find a matching username in the database
        string email = txtEmail.Text.Trim();
        var user = qPtl_User.GetUserByEmail(email);

        if (user != null)
        {
            if (user.UserID > 0)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}