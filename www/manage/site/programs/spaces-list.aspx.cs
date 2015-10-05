using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;

public partial class manage_manage_contests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        loadPageInfo();
    }
    
    protected void loadPageInfo()
    {
        string list_html = string.Empty;

        var list = qSoc_Space.GetSpaces();
        if (list != null)
        {
            foreach (var l in list)
            {
                qSoc_Space item = new qSoc_Space(l.SpaceID);
                list_html += "<li><a href=\"/manage/spaces/default.aspx?spaceID=" + item.SpaceID + "\">" + item.SpaceShortName + "</a></li>";
            }

            litList.Text = list_html;
        }
    }
}