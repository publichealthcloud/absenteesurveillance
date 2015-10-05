using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class www_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // if active session, then pass to manage nav page
        int curr_user_id = 0;
        bool active_session = false;

        if (!String.IsNullOrEmpty(Convert.ToString(Context.Items["UserID"])))
        {
            curr_user_id = Convert.ToInt32(Context.Items["UserID"]);
            if (curr_user_id > 0)
                active_session = true;
        }

        if (active_session == true)
            Response.Redirect("~/utilities/manage-user-access.aspx");
        else
        {
            string custom_start_page = System.Configuration.ConfigurationManager.AppSettings["Site_PublicStartPage"];
            if (!String.IsNullOrEmpty(custom_start_page))
                Response.Redirect(custom_start_page);
            else
                Response.Redirect("~/logon.aspx");
        }
    }
}