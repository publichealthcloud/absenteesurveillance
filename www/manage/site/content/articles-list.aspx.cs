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

using Quartz.Portal;

public partial class qLrn_article_list : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);

        qPtl_User user = new qPtl_User(curr_user_id);

        if (user.HighestRole == "Space Admin")
        {


        }
        
        siteArticles.SelectCommand = "SELECT * FROM qLrn_Articles_View WHERE MarkAsDelete = 0 AND Created > '" + System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"] + "' ORDER BY Created DESC, Title ASC";

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
        RadGrid1.ExportSettings.FileName = "Articles_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    } 
}
