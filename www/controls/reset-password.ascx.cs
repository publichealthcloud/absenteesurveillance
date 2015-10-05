using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Quartz.Portal;

public partial class controls_reset_password : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

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

            user.PasswordResetCode = "";
            string password_for_storing = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "sha1");
            user.Password = password_for_storing;
            user.Update();

            lblMsgReset.Text = "Your password has been successfully reset.<br><br>";
        }
        else
        {
            lblMsgReset.Text = "<br><br>Passwords did not match";
        }
    }
}