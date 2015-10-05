using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;
using Telerik.Web;

public partial class qHtl_condom_orders_list : System.Web.UI.Page
{
    public static string join_group_type = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Register_JoinGroupType"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GroupRequestsList.Visible = true;
            HealthProviderGroupRequestsList.Visible = false;

            if (!String.IsNullOrEmpty(join_group_type))
            {
                if (join_group_type == "health")
                {
                    GroupRequestsList.Visible = false;
                    HealthProviderGroupRequestsList.Visible = true;
                }
            }
        }
    }
}
