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

public partial class custom_member_list : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int scopeID = Convert.ToInt32(Context.Items["ScopeID"]);

        RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
        RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);

        RadGrid1.Width = Unit.Percentage(100);
        RadGrid1.PageSize = 20;
        RadGrid1.AllowPaging = true;
        RadGrid1.AllowSorting = true;
        RadGrid1.PagerStyle.Mode = GridPagerMode.NextPrevNumericAndAdvanced;
        RadGrid1.AutoGenerateColumns = false;
        RadGrid1.ShowGroupPanel = false;
        RadGrid1.ShowStatusBar = true;
        RadGrid1.ClientSettings.AllowDragToGroup = true;

        RadGrid1.MasterTableView.PageSize = 50;

    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            string searchType = Convert.ToString(Request.QueryString["searchType"]);

            if (string.IsNullOrEmpty(searchType))
            {
                searchType = "all"; //default style
            }

            string sql = string.Empty;
            DataTable dt = new DataTable();
            string title = string.Empty;
            switch (searchType)
            {
                case "all":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "All Members";
                    break;
                case "active-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE AccountStatus = 'Active' AND ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "All Active Members";
                    break;
                case "inactive-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE AccountStatus = 'Inactive' AND ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "All Inactive Members";
                    break;
                case "teens-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 AND HighestRole = 'Teen' ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "Teens Only";
                    break;
                case "advisors-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 AND HighestRole = 'Moderator' ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "Advisors Only";
                    break;
                case "hosts-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 AND HighestRole = 'Site Host' ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "Hosts Only";
                    break;
                case "admins-only":
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE  AccountStatus = 'Active' AND ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 AND HighestRole = 'Site Admin' ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "Admins Only";
                    break;
                default:
                    sql = "SELECT * FROM qPtl_Users_View_Manage WHERE AccountStatus = 'Active' AND ScopeID= " + Context.Items["ScopeID"] + " And MarkAsDelete = 0 ORDER BY CREATED DESC";
                    dt = GetDataTable(sql);
                    title = "All Members";
                    break;
            }

            DataTable final_dt = RemoveDuplicateRows(dt, "UserID");

            RadGrid1.DataSource = final_dt;
            lblTitle.Text = title + "(" + final_dt.Rows.Count + ")";
        }
    }

    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        DataView dv = dTable.DefaultView;
        dv.Sort = "UserSpaceID DESC";
        DataTable sortedDT = dv.ToTable();
        
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
        //And add duplicate item value in arraylist.
        foreach (DataRow drow in sortedDT.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
                hTable.Add(drow[colName], string.Empty);
        }

        //Removing a list of duplicate items from datatable.
        foreach (DataRow dRow in duplicateList)
            sortedDT.Rows.Remove(dRow);

        //Datatable which contains unique records will be return as output.
        DataView dv2 = sortedDT.DefaultView;
        dv2.Sort = "Created DESC";
        DataTable sortedDT2 = dv2.ToTable();

        return sortedDT2;
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

    public DataTable GetDataTable(string query)
    {
        String connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        conn.Open();
        try
        {
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }

    private void ExecuteSQL(string query)
    {
        String connString = ConfigurationManager.ConnectionStrings["OutreachConnectionString"].ConnectionString;
        SqlConnection sqlConn = new SqlConnection(connString);
        try
        {
            sqlConn.Open();

            SqlCommand sqlCom = new SqlCommand(query);
            sqlCom.Connection = sqlConn;
            sqlCom.ExecuteNonQuery();
        }
        finally
        {
            sqlConn.Close();
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName =  lblTitle.Text + "_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}
