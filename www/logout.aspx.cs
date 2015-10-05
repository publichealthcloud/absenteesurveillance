using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Quartz.Portal;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Convert.ToString(Context.Items["SessionID"])))
        {
            qPtl_Sessions session = new qPtl_Sessions(Convert.ToInt32(Context.Items["SessionID"]));
            if (session.SessionID > 0)
            {
                session.StopTime = DateTime.Now;
                session.Update();
            }

            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            if (user.UserID > 0)
            {
                DateTime last_time = new DateTime();
                last_time = Convert.ToDateTime(user.LastTimeSeen);
                if (!String.IsNullOrEmpty(Convert.ToString(user.LastTimeSeen)))
                    user.LastTimeSeen = last_time.AddMinutes(-16);
                user.Update();
            }
        }
        
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("~/default.aspx", true);
    }
}