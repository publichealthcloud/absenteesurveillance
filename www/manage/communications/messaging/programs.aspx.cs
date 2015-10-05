using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Telerik.Web;

public partial class text_messages_programs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        programs.SelectCommand = "SELECT * FROM qSoc_Campaigns WHERE Available = 'Yes' AND MarkAsDelete = 0 ORDER BY CampaignName ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Programs_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}