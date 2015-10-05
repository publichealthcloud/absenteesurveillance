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

public partial class manage_members_member_admin_tools : System.Web.UI.Page
{
    protected int profile_id;
    protected string required_indicator;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            profile_id = Convert.ToInt32(Request.QueryString["userID"]);
           

            qPtl_User profile = new qPtl_User(profile_id);

            ddlAccountStatus.SelectedValue = profile.AccountStatus;

            string curr_tab = string.Empty;
            curr_tab = Request.QueryString["currTab"];
            lit1Class.Text = "";
            lit2Class.Text = "";
            lit3Class.Text = "";
            lit4Class.Text = "";
            litTab1Class.Text = "class=\"tab-pane\"";
            litTab2Class.Text = "class=\"tab-pane\"";
            litTab3Class.Text = "class=\"tab-pane\"";
            litTab4Class.Text = "class=\"tab-pane\"";
            if (curr_tab == "1")
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab1Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "2")
            {
                lit2Class.Text = "class='active'";
                litTab2Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab2Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "3")
            {
                lit3Class.Text = "class='active'";
                litTab3Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab3Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "4")
            {
                lit4Class.Text = "class='active'";
                litTab4Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab4Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
            }            
        }
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int inviteUserID = Convert.ToInt32(Request.QueryString["userID"]);
            qPtl_User user = new qPtl_User();
            user.UpdateUserPassword(inviteUserID, txtPassword.Text);

            lblMessage.Text = "&nbsp;&nbsp;<i class=\"icon-check\"></i> Password successfully updated";

            Response.Redirect("member-admin-tools.aspx?currTab=1&message=Password successfully updated&userID=" + inviteUserID);
        }
    }

    protected void ValidatePassword(object obj, ServerValidateEventArgs args)
    {
        string password1 = txtPassword.Text;
        string password2 = txtPasswordConfirm.Text;
        string failMessage = string.Empty;

        if (password1 != password2)
        {
            failMessage = "<i class=\"icon-warning-sign\"></i> Passwords must match";
        }
        else
        {
            int minPasswordLength = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["register_minPasswordLength"]);
            if (password1.Length < minPasswordLength)
            {
                failMessage += "<i class=\"icon-warning-sign\"></i> Passwords must be at least " + minPasswordLength + " characters";
            }
        }

        if (failMessage != "")
        {
            // the user name does not match -- there is no problem
            args.IsValid = false;
            vldPasswordCompare.Text = failMessage;
        }
        else
            args.IsValid = true;
    }

    protected void btnUpdateUsername_Click(object sender, EventArgs e)
    {
        // validate username
         Page.Validate("username");

         if (Page.IsValid)
         {
             // update user record
             qPtl_User user = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));

             string oldUserName = user.UserName;
             string newUserName = txtUsername.Text;

             user.UserName = newUserName;
             user.Update();
             string message = string.Empty;

             if (oldUserName != newUserName)
             {

                 string rootLocation = Server.MapPath("~/") + "user_data\\";

                 if (Directory.Exists(rootLocation + oldUserName))
                 {
                     Directory.Move(rootLocation + oldUserName, rootLocation + newUserName);
                     message = "UserName successfully updated and images re-linked";
                 }
                 else
                 {
                     message = "UserName successfully updated";
                 }
             }
             else
             {
                 message = "New username is the same as the current username.";
             }

             Response.Redirect("member-admin-tools.aspx?currTab=2&message=" + message + "&userID=" + user.UserID);
         }
    }

    protected void ValidateUserName(object obj, ServerValidateEventArgs args)
    {
        string strUserName = txtUsername.Text.Trim();
        string confirmationUserName = txtUsernameConfirm.Text.Trim();

        if (strUserName != confirmationUserName)
        {
            args.IsValid = false;
            cvUserName.ErrorMessage = "WARNING: Username cannot be changed. The usernames do not match.";
        }
        else
        {
            // try and find a matching username in the database
            qPtl_User user = new qPtl_User(strUserName);

            if (user.UserID > 0)
            {
                // the user name exists
                args.IsValid = false;
                cvUserName.ErrorMessage = "WARNING: Username cannot be changed. This username is already being used.";
            }
            else
                args.IsValid = true;
            }
        }

    protected void btnUpdateAccountStatus_Click(object sender, EventArgs e)
    {
        // update user record
        qPtl_User user = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));

        user.AccountStatus = ddlAccountStatus.SelectedValue;
        user.Update();

        string message = "Member account status updated";

        Response.Redirect("member-admin-tools.aspx?currTab=3&message=" + message + "&userID=" + user.UserID);
    }

    protected void btnDeleteAccount_Click(object sender, EventArgs e)
    {
        // delete user
        qPtl_User user = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));

        user.AccountStatus = "Deleted";
        user.Available = "No";
        user.MarkAsDelete = 1;
        user.Update();

        Response.Redirect("member-list.aspx");
    }
}