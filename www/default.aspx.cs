using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class _default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/www/default.aspx");

        this.cmdSignOut.ServerClick += new System.EventHandler(this.cmdSignOut_ServerClick);
        {

        }
    }

    private void cmdSignOut_ServerClick(object sender, System.EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("logon.aspx", true);
    }
}