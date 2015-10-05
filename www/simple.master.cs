using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class simple : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            imgLogo.ImageUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_LogoUrl"]);
            litTitle.Text = System.Configuration.ConfigurationManager.AppSettings["Site_Title"];
        }
    }
}
