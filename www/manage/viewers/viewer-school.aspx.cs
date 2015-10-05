using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;

public partial class manage_viewers_viewer_school : System.Web.UI.Page
{
    protected int school_district_id;
    protected int school_id;

    public int SchoolDistrictID
    {
        get { return school_district_id; }
        set { school_district_id = value; }
    }

    public int SchoolID
    {
        get { return school_id; }
        set { school_id = value; }
    }    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (school_district_id == 0)
            school_district_id = 1;

        if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

        qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);

        if (!Page.IsPostBack)
        {
            DateTime today = new DateTime();
            today = Convert.ToDateTime(Session["curr_date"]);
            DailySchoolAbsenteeDashboard.CurrDate = today;
        }
    }
}