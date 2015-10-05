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
    protected string viewPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        int scopeID = Convert.ToInt32(Context.Items["ScopeID"]);

        RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
        RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);

        RadGrid1.Width = Unit.Percentage(100);
        RadGrid1.PageSize = 20;
        RadGrid1.AllowPaging = true;
        RadGrid1.AllowSorting = true;
        RadGrid1.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
        RadGrid1.AutoGenerateColumns = false;
        RadGrid1.ShowGroupPanel = true;
        RadGrid1.ShowStatusBar = true;
        RadGrid1.ClientSettings.AllowDragToGroup = true;

        RadGrid1.MasterTableView.PageSize = 25;

        viewPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CmsBasePath"]);

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

            switch (searchType)
            {
                case "all":
                    string sql = "SELECT * FROM qLrn_UserTrainingCertificates_View WHERE MarkAsDelete = 0 AND UserID = " + Request.QueryString["userID"];
                    RadGrid1.DataSource = GetDataTable(sql);
                    lblTitle.Text = "All User Training Certificates";
                    break;
                default:
                    RadGrid1.DataSource = GetDataTable("SELECT * FROM qLrn_UserTrainingCertificates_View WHERE MarkAsDelete = 0 AND UserID = " + Request.QueryString["userID"]);
                    lblTitle.Text = "All User Training Certificates";
                    break;
            }
        }
    }

    private void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {
            foreach (GridItem item in e.Item.OwnerTableView.Items)
            {
                if (item.Expanded && item != e.Item)
                {
                    item.Expanded = false;
                }
            }
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

    protected void RadGrid1_ExcelMLExportRowCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowCreatedArgs e)
    {
        if (e.RowType == Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowType.DataRow)
        {
            if (e.Row.Cells[0] != null && ((string)e.Row.Cells[0].Data.DataItem).Contains("U"))
            {
                e.Row.Cells[0].StyleValue = "MyCustomStyle";
            }
        }
    }

    protected void RadGrid1_ExcelMLExportStylesCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLStyleCreatedArgs e)
    {
        foreach (Telerik.Web.UI.GridExcelBuilder.StyleElement style in e.Styles)
        {
            if (style.Id == "headerStyle")
            {
                style.FontStyle.Bold = true;
                style.FontStyle.Color = System.Drawing.Color.Gainsboro;
                style.InteriorStyle.Color = System.Drawing.Color.Wheat;
                style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            }
            else if (style.Id == "itemStyle")
            {
                style.InteriorStyle.Color = System.Drawing.Color.WhiteSmoke;
                style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            }
            else if (style.Id == "alternatingItemStyle")
            {
                style.InteriorStyle.Color = System.Drawing.Color.LightGray;
                style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            }
        }

        Telerik.Web.UI.GridExcelBuilder.StyleElement myStyle = new Telerik.Web.UI.GridExcelBuilder.StyleElement("MyCustomStyle");
        myStyle.FontStyle.Bold = true;
        myStyle.FontStyle.Italic = true;
        myStyle.InteriorStyle.Color = System.Drawing.Color.Gray;
        myStyle.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
        e.Styles.Add(myStyle);
    }

    protected void gridMenu_ItemClick(object sender, RadMenuEventArgs e)
    {
        Telerik.Web.UI.RadMenuItem ItemClicked = e.Item;
        string clickedItem = Convert.ToString(ItemClicked.Text);

        if (clickedItem == "New Member")
        {
            Response.Redirect("~/qDbs/wizard.aspx?wizardID=1&stepNumber=1&groupID=2&formID=1&recordID=0");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("member-list.aspx");
    }

}
