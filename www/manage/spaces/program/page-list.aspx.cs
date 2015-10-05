using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class manage_spaces_pages : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int curr_space_id = 0;
            if (Page.IsPostBack)
            {
                DropDownList space_list = (DropDownList)Master.FindControl("ddlSpaces");
                curr_space_id = Convert.ToInt32(space_list.SelectedValue);
            }
            else
            {
                curr_space_id = Convert.ToInt32(Session["manage_space_id"]);
            }
            spacesidebar.SpaceID = curr_space_id;
        }
    }

}