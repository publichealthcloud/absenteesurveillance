using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Quartz.Portal;
using Quartz.Social;

public partial class t2x_takecontrol_main_master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();   
        
        int user_id = 0;
        bool logged_in = false;
        if (!String.IsNullOrEmpty(Convert.ToString(Context.Items["UserID"])))
        {
            user_id = Convert.ToInt32(Context.Items["UserID"]);
            logged_in = true;
        }
        else
        {

        }

        if (!Page.IsPostBack)
        {
            // load site footer
            qSoc_Element html = new qSoc_Element("footer-text");

            if (html != null)
            {
                if (html.ElementID > 0)
                    litFooter.Text = html.HTML;
            }

            // load site preferences
            string site_title = System.Configuration.ConfigurationManager.AppSettings["Site_Title"];
            string site_name = System.Configuration.ConfigurationManager.AppSettings["Site_ShortName"];
            string logo_url = System.Configuration.ConfigurationManager.AppSettings["Site_LogoUrl"];

            litPageTitle.Text = site_title;
            litTitle.Text = site_name;

            bool register_disabled = false;
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Register_RegisterDisabled"]))
                register_disabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_RegisterDisabled"]);
 
        }
    }
}
