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

public partial class qSoc_tip_list : System.Web.UI.Page
{
    public static string documentURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_LinkFolder"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        siteLinks.SelectCommand = "SELECT * FROM qPtl_Links_View WHERE MarkAsDelete = 0 AND UploadedFrom = 'manager' ORDER BY Title ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Links_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}
