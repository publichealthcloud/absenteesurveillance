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
using System.Data;

public partial class manage_members_controls_GroupRequestsList : System.Web.UI.UserControl
{
    protected string status_filter;

    public string StatusFilter
    {
        get { return status_filter; }
        set { status_filter = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(status_filter))
        {
            siteGroupRequests.SelectCommand = "SELECT * FROM qOrg_GroupRequests_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND Status LIKE '%" + status_filter + "%' ORDER BY Created DESC";
            lblTitle.Text = status_filter + " Group Requests";
        }
        else
            siteGroupRequests.SelectCommand = "SELECT * FROM qOrg_GroupRequests_View WHERE Available = 'Yes' AND MarkAsDelete = 0 ORDER BY Created DESC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.FilterCommandName)
        {
            Pair filterPair = (Pair)e.CommandArgument;

            switch (filterPair.Second.ToString())
            {
                case "Created":
                    this.startDate = ((e.Item as GridFilteringItem)[filterPair.Second.ToString()].FindControl("FromDatePicker") as RadDatePicker).SelectedDate;
                    this.endDate = ((e.Item as GridFilteringItem)[filterPair.Second.ToString()].FindControl("ToDatePicker") as RadDatePicker).SelectedDate;
                    break;
                default:
                    break;
            }
        }
    }

    protected DateTime? startDate
    {
        set
        {
            ViewState["strD"] = value;
        }
        get
        {
            if (ViewState["strD"] != null)
                return (DateTime)ViewState["strD"];
            else
                return new DateTime(2012, 11, 1);
        }
    }
    protected DateTime? endDate
    {
        set
        {
            ViewState["endD"] = value;
        }
        get
        {
            if (ViewState["endD"] != null)
                return (DateTime)ViewState["endD"];
            else
                return DateTime.Now.AddDays(1);
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "GroupRequests_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    } 
}