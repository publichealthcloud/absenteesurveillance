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

public partial class manage_site_tasks_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["searchType"]))
        {
            string selected_status = Request.QueryString["searchType"];

            if (selected_status == "closed" || selected_status == "completed")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND PercentCompleted = 100 ORDER BY Created DESC";
                lblTitle.Text = "Completed Tasks";
            }
            else if (selected_status == "pending")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND PercentCompleted < 100 ORDER BY Created DESC";
                lblTitle.Text = "Pending Tasks";
            }
            else if (selected_status == "all")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' ORDER BY Created DESC";
                lblTitle.Text = "All Tasks";
            }
            else if (selected_status == "my-all")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND AssignedTo = " + Context.Items["UserID"] + " ORDER BY Created DESC";
            }
            else if (selected_status == "my-open")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND PercentCompleted < 100 AND AssignedTo = " + Context.Items["UserID"] + " ORDER BY Created DESC";
                lblTitle.Text = "My Open Tasks";
            }
            else if (selected_status == "my-completed")
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND MarkAsDelete = 0 AND PercentCompleted = 100 AND AssignedTo = " + Context.Items["UserID"] + " ORDER BY Created DESC";
                lblTitle.Text = "My Completed Tasks";
            }
            else
            {
                siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' ORDER BY Created DESC";
                lblTitle.Text = "Open Tasks";
            }
        }
        else
        {
            siteTasks.SelectCommand = "SELECT * FROM qPtl_Tasks_View WHERE Available = 'Yes' AND PercentCompleted < 100 ORDER BY Created DESC";
            lblTitle.Text = "Pending Tasks";
        }

        if (!Page.IsPostBack)
        {
            startDate = null;
            endDate = null;
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
            {
                DateTime? beginningDate = new DateTime();
                beginningDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
                ViewState["strD"] = beginningDate;
                return beginningDate;
            }
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
            {
                return DateTime.Now.AddDays(1);
            }
        }
    }
    protected DateTime? minDate
    {
        set
        {
            DateTime? minDate = new DateTime();
            minDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
        }
        get
        {
            DateTime? minDate = new DateTime();
            minDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
            return minDate;
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Site Tasks_run=" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}