using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Communication;

public partial class logon : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }

        hplRegister.NavigateUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_RegisterPage"]);
        string hplBack_custom = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_PublicHomePage"]);
        string login_nav = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_LoginExtraNav"]);
        if (!String.IsNullOrEmpty(login_nav))
        {
            if (login_nav == "false")
                plhLoginExtraNav.Visible = false;
        }
        if (!String.IsNullOrEmpty(hplBack_custom))
            hplBack.NavigateUrl = hplBack_custom;
        else
            hplBack.NavigateUrl = "~/default.aspx";

        this.cmdLogin.ServerClick += new System.EventHandler(this.cmdLogin_ServerClick);

        // check to see if matches space
        string reg_code = string.Empty;
        if (!String.IsNullOrEmpty(Request.QueryString["code"]))
        {
            reg_code = Request.QueryString["code"];

            qSoc_Campaign campaign = new qSoc_Campaign(reg_code);

            if (campaign.CampaignID > 0)
            {
                plhSpaceCode.Visible = true;
                txtSpaceCode.Text = reg_code;
                lblSpaceDescription.Text = "<br><span class=\"label label-success\"><i class=\"icon-ok\"></i>&nbsp;" + campaign.CampaignName + " channel.</span><br>";
            }
        }

        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        if (user_id > 0)
            Response.Redirect("~/utilities/manage-user-access.aspx");
    }


    private bool ValidateUser(string userName, string passWord)
    {
        bool user_exists = false;

        // Check for invalid userName.
        // userName must not be null and must be between 1 and 15 characters.
        if ((null == userName) || (0 == userName.Length))
        {
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
            return false;
        }

        // Check for invalid passWord.
        // passWord must not be null and must be between 1 and 25 characters.
        if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
        {
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");
            return false;
        }

       try
       {
        var user = qPtl_User.UserLogon(txtUserName.Value, txtUserPass.Value);

            if (user != null)
                if (user.UserID > 0)
                    user_exists = true;
       }
        catch (Exception ex)
        {
            // Add error handling here for debugging.
            // This error message should not be sent back to the caller.
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

        // Compare lookupPassword and input passWord, using a case-sensitive comparison.
        return user_exists;
    }

    private void cmdLogin_ServerClick(object sender, System.EventArgs e)
    {
        if (ValidateUser(txtUserName.Value, txtUserPass.Value))
        {
            qPtl_User user = new qPtl_User(txtUserName.Value);

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

            // see if there is a campaign code
            string reg_code = string.Empty;
            string custom_redirect = string.Empty;
            if (!String.IsNullOrEmpty(txtSpaceCode.Text))
            {
                reg_code = txtSpaceCode.Text;

                qSoc_Campaign campaign = new qSoc_Campaign(reg_code);

                if (campaign.CampaignID > 0)
                {
                    // see if already enrolled
                    var exist_campaign = qSoc_UserCampaign.GetUserCampaign(user.UserID, campaign.CampaignID);

                    if (exist_campaign != null)
                    {
                        if (exist_campaign.UserCampaignID > 0)
                        {
                            exist_campaign.DeleteUserCampaign(campaign.CampaignID, user.UserID);
                            exist_campaign.DeleteUserCampaignActions(campaign.CampaignID, user.UserID);
                        }
                    }

                    AddUserCampaign(campaign.CampaignID, user.ScopeID, user);
                    custom_redirect = "~/social/learning/campaigns/campaign-details.aspx?campaignID=" + campaign.CampaignID;
                }
            }

            string userData = string.Format("{0};{1};{2}", sessionID, role_list, scopeID);

            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            int timeout_minutes = 480;                  // default shorter timeout (8 hours)
            int timeout_extended_minutes = 1440;        // default extended timeout (1 day)
            int logout_minutes = 0;
            if (!String.IsNullOrEmpty(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_LoginTimeOut"])))
                timeout_minutes = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Site_LoginTimeOut"]);
            else if (!String.IsNullOrEmpty(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ExtendedLoginTimeOut"])))
                timeout_extended_minutes = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Site_ExtendedLoginTimeOut"]);

            if (chkLeaveLoggedIn.Checked)
                logout_minutes = timeout_extended_minutes;
            else
                logout_minutes = timeout_minutes;

            tkt = new FormsAuthenticationTicket(1, user.UserID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(logout_minutes), true, userData);
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            //if (chkPersistCookie.Checked)
            //ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);

            HttpCookie cookie2 = new HttpCookie("UserID", Convert.ToString(user.UserID));
            if (tkt.IsPersistent) { cookie2.Expires = tkt.Expiration.AddMinutes(logout_minutes+5); }
            Response.Cookies.Add(cookie2);

            string strRedirect;
            strRedirect = Request["ReturnUrl"];
            if (strRedirect == null)
                strRedirect = "~/utilities/manage-user-access.aspx";
            if (!String.IsNullOrEmpty(custom_redirect))
                strRedirect = custom_redirect;
            Response.Redirect(strRedirect, true);
        }
        else
            lblMsg.Text = "<br><br>Your username or password is not correct. Please try again.";
    }

    protected void AddUserCampaign(int campaign_id, int scope_id, qPtl_User user)
    {
        DateTime start_date = new DateTime();
        start_date = DateTime.Now;

        qSoc_UserCampaign new_campaign = qSoc_UserCampaign.EnrollUserInCampaign(user.UserID, campaign_id, "controlled", start_date, Convert.ToInt32(Context.Items["ScopeID"]), "role", "Yes", "Yes", "Yes", "Yes");
    }
}