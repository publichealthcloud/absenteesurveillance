using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;

public partial class manage_default : System.Web.UI.Page
{
    public static string resources_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ResourcesUrl"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {   
        int curr_space_id = 0;
        if (Page.IsPostBack)
        {
            DropDownList space_list = (DropDownList) Master.FindControl("ddlSpaces");
            curr_space_id = Convert.ToInt32(space_list.SelectedValue);
        }
        else if (!String.IsNullOrEmpty(Convert.ToString(Session["manage_space_id"])) && Convert.ToString(Session["manage_space_id"]) != "0")
        {
            curr_space_id = Convert.ToInt32(Session["manage_space_id"]);
        }
        else
        {
            // get first space associated with this user
            var spaces = qPtl_SpaceAdmin_View.GetSpaceAdminsByUser(Convert.ToInt32(Context.Items["UserID"]));
            int i = 0;
            foreach (var s in spaces)
            {
                if (i == 0)
                {
                    curr_space_id = s.SpaceID;
                }
                i++;
            }
        }
        loadPageInfo(curr_space_id);

    }

    protected void loadPageInfo(int space_id)
    {
        qSoc_Space space = new qSoc_Space(space_id);

        //litDashboardImage.Text = "<h3>" + space.SpaceShortName  + "</h3><img src=\"" + resources_url + "/spaces/" + space_id + "/reports/report.png\">";
    }
}