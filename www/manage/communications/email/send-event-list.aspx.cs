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

public partial class manage_communications_send_events_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        siteEmailSendEvents.SelectCommand = "SELECT * FROM qCom_SendEvents_View WHERE MarkAsDelete = 0 ORDER BY Subject ASC";
        lblTitle.Text = "Email Send Events";
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Send_Events_=" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("send-event-list.aspx");
    }

    protected void btnNewEvent_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-email-event.aspx");
    }

    protected void btnSendEvents_Click(object sender, EventArgs e)
    {
        Response.Redirect("send-bulk.aspx");
    }

}
