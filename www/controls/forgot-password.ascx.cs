using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Quartz.Portal;
using Quartz.Communication;

public partial class controls_forgot_password : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["resetCode"]))
        {
            plhRequest.Visible = false;
            plhReset.Visible = true;
            int user_id = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["userID"]))
            {
                user_id = Convert.ToInt32(Request.QueryString["userID"]);
                qPtl_User user = new qPtl_User(user_id);
                lblResetInstructions.Text = "Welcome back, " + user.UserName + ". Enter a new password below<br><br />";
            }
        }
        else
        {
            plhRequest.Visible = true;
            plhReset.Visible = false;
            lblRequestInstructions.Text = "What email address did you use when you created your account?<br /><br />";
        }

    }

    protected void btnRequestPasswordReset_Click(object sender, EventArgs e)
    {
        var user = qPtl_User.GetUserByEmail(txtOrigEmail.Text);

        if (user != null)
        {
            if (user.UserID > 0)
            {
                if (user.SetPasswordResetCode(user.UserID))
                {
                    // create object
                    int emailID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Email_ForgotPasswordID"]);
                    qCom_EmailTool email = new qCom_EmailTool(emailID);

                    qPtl_User updated_user = new qPtl_User(user.UserID);

                    int sent_email_log_id = email.SendDatabaseMail(updated_user.Email, emailID, updated_user.UserID, updated_user.UserName, updated_user.PasswordResetCode, Request.Url.Authority + HttpRuntime.AppDomainAppVirtualPath.TrimEnd('/'), string.Empty, string.Empty, false);

                    if (sent_email_log_id > 0)
                    {
                        lblMsgRequest.Text = "<br>Please check your email for further instructions";
                        txtOrigEmail.Visible = false;
                        btnRequestPasswordReset.Visible = false;
                        hplCancelRequest.Visible = false;
                    }
                    else
                    {
                        lblMsgRequest.Text = "<br><br>An error occured sending an email, please contact support";
                    }
                }
                else
                {
                    lblMsgRequest.Text = "<br><br>An error occured setting the reset code, please contact support";
                }
            }
        }
        else
            lblMsgRequest.Text = "<br><br>Email address not found";
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        int user_id = 0;
        if (!String.IsNullOrEmpty(Request.QueryString["userID"]))
        {
            user_id = Convert.ToInt32(Request.QueryString["userID"]);
            if (txtPassword.Text == "")
            {
                lblMsgReset.Text = "<br><br>New password cannot be blank";
            }
            else if (txtPassword.Text.Length < 6)
            {
                lblMsgReset.Text = "<br><br>New password cannot be less than 6 characters";
            }
            else if (txtPassword.Text.Length > 15)
            {
                lblMsgReset.Text = "<br><br>New password cannot be longer than 15 characters";
            }
            else if (txtPassword.Text == txtPasswordConfirm.Text)
            {
                lblMsgReset.Text = "";
                qPtl_User user = new qPtl_User(user_id);

                if (user.PasswordResetCode == Request.QueryString["resetCode"])
                {

                    user.PasswordResetCode = "";
                    string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
                    user.Password = password_for_storing;
                    user.Update();
                    txtPassword.Visible = false;
                    txtPasswordConfirm.Visible = false;
                    btnResetPassword.Visible = false;
                    hplCancelReset.Visible = false;

                    lblMsgReset.Text = "Your password has been successfully reset.<br><br> <a href=\"/logon.aspx\" class=\"btn\">Sign in now</a>";
                }
                else
                    lblMsgReset.Text = "<br><br>This password reset code is no longer valid. Please request another one or contact support";
            }
            else
            {
                lblMsgReset.Text = "<br><br>Passwords did not match";
            }
        }
        else
        {
            Response.Redirect("/default.aspx");
        }
    }
}