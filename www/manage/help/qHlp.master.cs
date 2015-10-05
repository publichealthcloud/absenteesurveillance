using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Telerik.Web.UI;

using Quartz;
using Quartz.Portal;
using Quartz.Help;

public partial class qHlp_qHlp : System.Web.UI.MasterPage
{
    private const int ItemsPerRequest = 10;
    public int scopeID;

    protected override void OnLoad(EventArgs e)
    {
        explorer.Target = rightPane;

        if (!Page.IsPostBack)
        {
            explorer.ContentPaneClientID = rightPane.ClientID;
            
            //menu_top.ContentPaneClientID = rightPane.ClientID;
            //personalmenu.ContentPaneClientID = rightPane.ClientID;
            //set_targets(rad_left_panel_bar.Items);

            // get user permissions
            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            string highest_role = Convert.ToString(user.HighestRole);
            qPtl_ManagerPermission_View permission = new qPtl_ManagerPermission_View(highest_role);

            // see if need to advance to pre-loaded page
            string action = Convert.ToString(Request.QueryString["topic"]);
            if (!String.IsNullOrEmpty(action))
                rightPane.ContentUrl = "~/manage/help/help-viewer.aspx?topic=" + action;

        }
    }

    protected void set_targets(RadPanelItemCollection items)
    {
        foreach (RadPanelItem item in items)
        {
            item.Target = rightPane.ClientID;

            set_targets(item.Items);
        }
    }

    protected void btnReloadExplorer_OnClick(object sender, EventArgs args)
    {
        explorer.refreshExplorer();
    }

    private static string GetStatusMessage(int offset, int total)
    {
        if (total <= 0)
            return "No matches";

        return String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", offset, total);
    }

    public static string escapeLikeValue(string valueWithoutWildcards)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < valueWithoutWildcards.Length; i++)
        {
            char c = valueWithoutWildcards[i];
            if (c == '*' || c == '%' || c == '[' || c == ']')
                sb.Append("[").Append(c).Append("]");
            else if (c == '\'')
                sb.Append("''");
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
}
