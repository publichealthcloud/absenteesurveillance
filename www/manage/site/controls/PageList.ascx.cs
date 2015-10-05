using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;

public partial class manage_site_controls_PageList : System.Web.UI.UserControl
{
    protected int user_id;
    public static string key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_AutomationKey"]);
    public static string cms_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CMS_URL"]);

    public int UserID
    {
        get { return user_id; }
        set { user_id = value; }
    }
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            user_id = Convert.ToInt32(Context.Items["UserID"]);

            repPages.DataSource = qCms_SitePage.GetPages();
            repPages.DataBind();
        }
    }

    protected void loadPages(int space_id)
    {
        user_id = Convert.ToInt32(Context.Items["UserID"]);

        repPages.DataSource = qCms_SitePage.GetPages();
        repPages.DataBind();
    }
}


