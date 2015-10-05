using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Quartz.Portal;
using Quartz.Social;

public partial class utilities_manage_admin_access : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
        string roles = Convert.ToString(Context.Items["UserRoles"]);

        // currently support three types of users for main redirection (manage everything, manage insurance, manage space, manage campaign
        // once the user is at the current pages there will be additional filtering of features by -- 
        if (user.HighestRole == "Site Admin" || user.HighestRole == "Site Host" || user.HighestRole == "Site Reports")
            Response.Redirect("/manage/site/default.aspx");
        else if (user.HighestRole == "Insurer Admin" || user.HighestRole == "Insurer Reports")
            Response.Redirect("/manage/insurer/default.aspx");
        else if (user.HighestRole == "Advisor" || user.HighestRole == "Space Admin" || user.HighestRole == "Space Reports" || roles.Contains("Space Admin") || roles.Contains("Space Reports"))
            Response.Redirect("/manage/spaces/default.aspx");
        else if (user.HighestRole == "Campaign Admin" || user.HighestRole == "Campaign Reports")
            Response.Redirect("/manage/campaigns/default.aspx");
    }
}