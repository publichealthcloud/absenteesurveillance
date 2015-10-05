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

public partial class qSoc_theme_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        siteThemes.SelectCommand = "SELECT * FROM qSoc_Themes WHERE MarkAsDelete = 0 ORDER BY Name ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Themes_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}
