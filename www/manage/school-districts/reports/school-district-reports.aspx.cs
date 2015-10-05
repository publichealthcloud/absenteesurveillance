using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;
using Quartz.Organization;

public partial class manage_school_districts_reports : System.Web.UI.Page
{
    public static string resources_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ResourcesUrl"]);

    protected int school_district_id;

    public int SchoolDistrictID
    {
        get { return school_district_id; }
        set { school_district_id = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_school_district_id = 0;
        if (Page.IsPostBack)
        {
            // get most recent day of data
            bool day_prior_exists = false;
            bool day_after_exists = false;


        }

        if (!String.IsNullOrEmpty(Request.QueryString["schoolDistrictID"]))
        {
            curr_school_district_id = Convert.ToInt32(Request.QueryString["spaceID"]);
        }
        else
        {
            // get first space associated with this user
            var districts = qPtl_SchoolDistrictAdmin_View.GetSchoolDistrictAdminsByUser(Convert.ToInt32(Context.Items["UserID"]));
            int i = 0;
            foreach (var d in districts)
            {
                if (i == 0)
                {
                    curr_school_district_id = d.SchoolDistrictID;

                    // set session variable

                }
                i++;
            }
        }
        school_district_id = curr_school_district_id;
        loadPageInfo(curr_school_district_id);
        schooldistrictsidebar.SchoolDistrictID = curr_school_district_id;
    }

    protected void loadPageInfo(int school_district_id)
    {
        qOrg_SchoolDistrict district = new qOrg_SchoolDistrict(school_district_id);

        //litTitle.Text = "<h3>" + district.DistrictName + "</h3>";
    }
}