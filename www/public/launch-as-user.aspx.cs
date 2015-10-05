using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net;

using Quartz;
using Quartz.Portal;
using Quartz.Data;

public partial class public_launch_as_user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString["key"]) != System.Configuration.ConfigurationManager.AppSettings["Site_AutomationKey"])
        {
            Response.Redirect("access-restricted.aspx");
        }
        else
        {
            qPtl_User user = new qPtl_User(Convert.ToInt32(Request.QueryString["userID"]));

            if (user.UserID > 0)
            {
                qPtl_Sessions sess = new qPtl_Sessions();
                sess.UserID = user.UserID;
                qPtl_Sessions session = new qPtl_Sessions();
                session.Created = DateTime.Now;
                session.StartTime = DateTime.Now;
                session.LastTimeSeen = DateTime.Now;
                session.ScopeID = user.ScopeID;
                session.UserID = user.UserID;
                session.BrowserType = Request.Browser.Browser;
                session.ComputerType = Request.Browser.Platform;
                session.Insert();

                int session_id = session.SessionID;

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

                string userData = string.Format("{0};{1};{2}", session.SessionID, role_list, user.ScopeID);

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

                logout_minutes = timeout_extended_minutes;

                tkt = new FormsAuthenticationTicket(1, user.UserID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(logout_minutes), true, userData);
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);

                HttpCookie cookie2 = new HttpCookie("UserID", Convert.ToString(user.UserID));
                if (tkt.IsPersistent) { cookie2.Expires = tkt.Expiration.AddMinutes(logout_minutes + 5); }
                Response.Cookies.Add(cookie2);

                if (!string.IsNullOrEmpty(Request.QueryString["redirectURL"]))
                {
                    string redirect = Request.QueryString["redirectURL"];
                    Response.Redirect(redirect);
                }
                else
                    Response.Redirect("~/default.aspx");
            }
            else
                Response.Redirect("access-restricted.aspx");
        }
    }
}