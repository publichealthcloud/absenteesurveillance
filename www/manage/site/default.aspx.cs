using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_site_default : System.Web.UI.Page
{
    public static string join_group_type = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_JoinGroupType"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GroupRequestsList.Visible = false;
            HealthProviderGroupRequestsList.Visible = false;

            if (!String.IsNullOrEmpty(join_group_type))
            {
                if (join_group_type == "health")
                {
                    GroupRequestsList.Visible = false;
                    HealthProviderGroupRequestsList.Visible = true;
                    HealthProviderGroupRequestsList.StatusFilter = "Pending";
                }
            }
        }
    }
}