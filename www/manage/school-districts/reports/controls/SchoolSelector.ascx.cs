using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Organization;

public partial class manage_school_districts_reports_controls_SchoolSelector : System.Web.UI.UserControl
{
    protected int school_id;
    protected string school_name;

    public int SchoolID
    {
        get { return school_id; }
        set { school_id = value; }
    }

    public string SchoolName
    {
        get { return school_name; }
        set { school_name = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (school_id == 0)
                school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

            int school_district_id = 1;

            string list_html = string.Empty;
            list_html += "<option value=\"#\">Pick a School</option>";

            if (school_district_id > 0)
            {
                var list = qOrg_School.GetSchoolsByDistrictID(school_district_id);
                if (list != null)
                {
                    foreach (var l in list)
                    {
                        qOrg_School item = new qOrg_School(l.SchoolID);
                        string curr_date = Convert.ToString(Session["CurrDate"]);
                        if (!String.IsNullOrEmpty(Request.QueryString["currDate"]))
                            curr_date = Request.QueryString["currDate"];
                        //list_html += "<option value=\"javascript:showManageElementModal(0," + item.SchoolID + ",0,'school','" + item.School + "')\">" + item.School + "</option>";
                        list_html += "<option value=\"javascript:openSchoolWindow('/manage/school-districts/school-default.aspx?schoolID=" + item.SchoolID + "&currDate=" + curr_date + "')\">" + item.School + "</option>";
                    }
                }
            }

            litSelector.Text = list_html;
        }
    }
}