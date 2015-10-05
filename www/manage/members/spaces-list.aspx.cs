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

using Quartz.Social;

public partial class manage_members_spaces_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!String.IsNullOrEmpty(Request.QueryString["categoryID"]))
        {
            string cat_id = Request.QueryString["categoryID"];
            siteSpaces.SelectCommand = "SELECT * FROM qSoc_Spaces_View WHERE SpaceType <> 'family' AND MarkAsDelete = 0 AND SpaceCategoryID = " + cat_id + " ORDER BY SpaceShortName ASC";

            qSoc_SpaceCategory cat = new qSoc_SpaceCategory(Convert.ToInt32(cat_id));
            lblTitle.Text = cat.CatgoryName + " Groups";
        }
        else
        {
            siteSpaces.SelectCommand = "SELECT * FROM qSoc_Spaces_View WHERE SpaceType <> 'family' AND MarkAsDelete = 0 ORDER BY SpaceShortName ASC";
            lblTitle.Text = "All Groups";
        }

        if (!Page.IsPostBack)
        {
            startDate = null;
            endDate = null;

            var cats = qSoc_SpaceCategory.GetSpaceCategories();

            string cat_options_html = string.Empty;
            cat_options_html += "<li><a href=\"spaces-list.aspx\">All Groups</a></li>";

            if (cats != null)
            {
                foreach (var c in cats)
                {
                    cat_options_html += "<li><a href=\"spaces-list.aspx?categoryID=" + c.SpaceCategoryID + "\">" + c.CatgoryName + "</a></li>";
                }
            }

            litGroupTypeOptions.Text = cat_options_html;
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
        RadGrid1.ExportSettings.FileName = "Groups_run=" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}
