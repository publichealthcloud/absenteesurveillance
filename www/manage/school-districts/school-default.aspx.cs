﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;
using Quartz.Organization;
using Quartz.Health;

public partial class manage_school_districts_default : System.Web.UI.Page
{
    public static string resources_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_ResourcesUrl"]);

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
        int curr_school_district_id = 0;
        if (school_district_id == 0)
            school_district_id = 1;

        if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
            school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

        qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);

        if (!Page.IsPostBack)
        {
            var summary = new qHtl_DailySchoolAbsenteeData();
            
            if (String.IsNullOrEmpty(Request.QueryString["currDate"]))
            {
                summary = qHtl_DailySchoolAbsenteeData.GetMostRecentDailySummary(school_id);
            }
            else
            {
                string eval_curr_date = Convert.ToString(Request.QueryString["currDate"]);
                summary = qHtl_DailySchoolAbsenteeData.GetSchoolDailySummaryByDate(school_id, eval_curr_date);
            }

            if (summary != null)
            {
                if (summary.DailySchoolAbsenteeDataID > 0)
                {
                    DateTime curr_date = new DateTime();
                    curr_date = Convert.ToDateTime(summary.DataDate);

                    litDataDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
                    DailySchoolAbsenteeDashboard.CurrDate = curr_date;

                    // possibly add daily school level details control
                }

                //schooldistrictsidebar.Summary = summary;
                //DailySummaryCharts.Summary = summary;

                var prior_summary = qHtl_DailySchoolAbsenteeData.GetDailySchoolSummaryPriorOrAfter("prior", Convert.ToString(summary.DataDate));
                var after_summary = qHtl_DailySchoolAbsenteeData.GetDailySchoolSummaryPriorOrAfter("after", Convert.ToString(summary.DataDate));
               

                if (prior_summary != null)
                {
                    if (prior_summary.DailySchoolAbsenteeDataID > 0)
                    {
                        string prior_date = String.Format("{0:M/d/yyyy}", prior_summary.DataDate);
                        litDateBefore.Text = "<a href=\"school-default.aspx?schoolID=" + school_id + "&currDate=" + prior_date + "\" class=\"btn btn-large\"><i class=\"glyphicon-step_backward\"></i></a>";
                    }
                }
                if (after_summary != null)
                {
                    if (after_summary.DailySchoolAbsenteeDataID > 0)
                    {
                        string after_date = String.Format("{0:M/d/yyyy}", after_summary.DataDate);
                        litDateAfter.Text = "<a href=\"school-default.aspx?schoolID=" + school_id + "&currDate=" + after_date + "\" class=\"btn btn-large\"><i class=\"glyphicon-step_forward\"></i></a>";
                    }
                }
            }
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
        loadPageInfo(school_id);
        schoolsidebar.SchoolID = school_id;
        schoolsidebar.SchoolDistrictID = curr_school_district_id;
    }

    protected void loadPageInfo(int school_id)
    {
        qOrg_School school = new qOrg_School(school_id);

        litTitle.Text = "<h3>" + school.School + "</h3>";
    }

    protected void Reload_Click(object sender, EventArgs e)
    {
        string eval_date = String.Format("{0:M/d/yyyy}", rdtDataDate.SelectedDate);
        litDatePickWarning.Text = string.Empty;

        if (String.IsNullOrEmpty(eval_date))
        {
            litDatePickWarning.Text = "<br><strong>WARNING: there is no data for the selected date.";
            DailySchoolAbsenteeDashboard.Visible = false;
        } 
        else 
        {
            var summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetDailySummaryByDate(eval_date);

            if (summary == null)
            {
                litDatePickWarning.Text = "<br><strong>WARNING: there is no data for the selected date.";
                DailySchoolAbsenteeDashboard.Visible = false;
            }
            else
            {
                Response.Redirect("school-default.aspx?schoolID=" + Request.QueryString["schoolID"] + "&currDate=" + eval_date);
            }
        }
    }
}