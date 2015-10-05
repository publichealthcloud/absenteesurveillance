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
using Quartz.Organization;

public partial class manage_daily_school_absentee_data : System.Web.UI.Page
{
    protected int school_district_id;

    public int SchoolDistrictID
    {
        get { return school_district_id; }
        set { school_district_id = value; }
    } 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        school_district_id = Convert.ToInt32(Session["manage_school_district_id"]);
        
        if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
        {
            int school_id = Convert.ToInt32(Request.QueryString["schoolID"]);
            siteSchoolWeeklyAbsenteeData.SelectCommand = "SELECT * FROM qHtl_DailyClassroomAbsenteeData WHERE MarkAsDelete = 0 AND SchoolID = " + school_id + " ORDER BY DataDate, School DESC";

            qOrg_School school = new qOrg_School(school_id);
            lblTitle.Text = school.School + " Daily Absentee Data";
        }
        else
        {
            siteSchoolWeeklyAbsenteeData.SelectCommand = "SELECT * FROM qHtl_DailyClassroomAbsenteeData WHERE MarkAsDelete = 0 ORDER BY DataDate, School DESC";
            lblTitle.Text = "All Classrooms Daily Absentee Data";
        }

        if (!Page.IsPostBack)
        {
            startDate = null;
            endDate = null;

            var schools = qOrg_School.GetSchoolsByDistrictID(school_district_id);

            string school_options_html = string.Empty;
            school_options_html += "<li><a href=\"daily-school-absentee-data.aspx\">All Schools</a></li>";

            if (schools != null)
            {
                foreach (var s in schools)
                {
                    school_options_html += "<li><a href=\"daily-school-absentee-data.aspx?schoolID=" + s.SchoolID + "\">" + s.School + "</a></li>";
                }
            }

            litGroupTypeOptions.Text = school_options_html;
        }
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.FilterCommandName)
        {
            Pair filterPair = (Pair)e.CommandArgument;

            switch (filterPair.Second.ToString())
            {
                case "DataDate":
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
        RadGrid1.ExportSettings.FileName = "School_Absentee_Data_run=" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}
