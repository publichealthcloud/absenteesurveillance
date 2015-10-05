using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;

public partial class manage_health_controls_HealthKitsList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        siteUserHealthKits.SelectCommand = "SELECT * FROM qHtl_UserHealthKits_View WHERE MarkAsDelete = 0 ORDER BY OrderTimestamp ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "UserHealthKits_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    } 
}