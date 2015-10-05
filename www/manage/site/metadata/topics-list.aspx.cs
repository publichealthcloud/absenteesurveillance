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

public partial class qSoc_topic_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        siteTopics.SelectCommand = "SELECT * FROM qSoc_Topics WHERE MarkAsDelete = 0 ORDER BY Name ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Topics_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    } 
}
