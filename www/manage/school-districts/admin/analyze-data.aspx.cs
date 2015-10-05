using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

using Quartz.Health;
using Quartz.Social;

public partial class school_district_analyze_data : System.Web.UI.Page
{
    protected int school_district_id;

    public int SchoolDistrictID
    {
        get { return school_district_id; }
        set { school_district_id = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (school_district_id == 0)
                school_district_id = 1;

            qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);

            string analysis_html = string.Empty;

            analysis_html +=    "<strong>ABSENTEE RATE MULTIPLIERS (x) </strong><br>";
            analysis_html +=    "Green Absentee Rate STD Multiplier = <strong>" + variables.GreenRateSTDMultiplier + "</strong><br>";
            analysis_html +=    "Yellow Absentee Rate STD Multiplier = <strong>" + variables.YellowRateSTDMultiplier + "</strong><br>";
            analysis_html +=    "Red Absentee Rate STD Multiplier = <strong>" + variables.RedRateSTDMultiplier + "</strong><br>";
            analysis_html +=    "<br>";
            analysis_html +=    "<strong>ILLNESS CALCULATIONS (p)</strong><br>";
            analysis_html +=    "Green Status => p < <strong>" + variables.GreenIllnessBoundary + "%</strong> of school's current enrollment<br>";
            analysis_html +=    "Yellow Status => p >= <strong>" + variables.GreenIllnessBoundary + "%</strong> AND p <= <strong>" + variables.RedIllnessBoundary +"%</strong> of school's current enrollment<br>";
            analysis_html +=    "Red Status => p > <strong>" + variables.RedIllnessBoundary + "%</strong> of school's current enrollment<br>";
            analysis_html +=    "<br>";
            analysis_html +=    "<strong>SYMPTOM CALCUATIONS (s)</strong><br>";
            analysis_html +=    "<strong>Gastrointestinal:</strong> Green Status: s < <strong>" + variables.GreenGastrointestinalBoundary + "%</strong>";
            analysis_html +=    " | Yellow Status: <strong>" + variables.GreenGastrointestinalBoundary + "%</strong> <= s <= <strong>" + variables.RedGastrointestinalBoundary + "%</strong>";
            analysis_html +=    " | Red Status: s > <strong>" + variables.RedGastrointestinalBoundary + "%</strong><br>";
            analysis_html +=    "<strong>Respiratory:</strong> Green Status: s < <strong>" + variables.GreenRespiratoryBoundary + "%</strong>";
            analysis_html +=    " | Yellow Status: <strong>" + variables.GreenRespiratoryBoundary + "%</strong> <= s <= <strong>" + variables.RedRespiratoryBoundary + "%</strong>";
            analysis_html +=    " | Red Status: s > <strong>" + variables.RedRespiratoryBoundary + "%</strong><br>";
            analysis_html +=    "<strong>Rash:</strong> Green Status: s < <strong>" + variables.GreenRashBoundary + "%</strong>";
            analysis_html +=    " | Yellow Status: <strong>" + variables.GreenRashBoundary + "%</strong> <= s <= <strong>" + variables.RedRashBoundary + "%</strong>";
            analysis_html +=    " | Red Status: s > <strong>" + variables.RedRashBoundary + "%</strong><br>";
            analysis_html +=    "<strong>Other Illness:</strong> Green Status: s < <strong>" + variables.GreenOtherIllnessBoundary + "%</strong>";
            analysis_html +=    " | Yellow Status: <strong>" + variables.GreenOtherIllnessBoundary + "%</strong> <= s <= <strong>" + variables.RedOtherIllnessBoundary + "%</strong>";
            analysis_html +=    " | Red Status: s > <strong>" + variables.RedOtherIllnessBoundary + "%</strong><br>";
            analysis_html +=    "<strong>Unknown Illness:</strong> Green Status: s < <strong>" + variables.GreenUnknownIllnessBoundary + "%</strong>";
            analysis_html +=    " | Yellow Status: <strong>" + variables.GreenUnknownIllnessBoundary + "%</strong> <= s <= <strong>" + variables.RedUnknownIllnessBoundary + "%</strong>";
            analysis_html +=    " | Red Status: s > <strong>" + variables.RedUnknownIllnessBoundary + "%</strong><br>";

            litAnalysisVariables.Text = analysis_html;
        }
    }
    
    protected void btnAnalyzeData_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Convert.ToString(rdtStartDate.SelectedDate)) || !String.IsNullOrEmpty(Convert.ToString(rdtStartDate.SelectedDate)))
        {
            DateTime start_date = new DateTime();
            DateTime end_date = new DateTime();
            int school_analyzed_count = 0;
            if (school_district_id == 0)
                school_district_id = 1;

            start_date = Convert.ToDateTime(rdtStartDate.SelectedDate);
            end_date = Convert.ToDateTime(rdtEndDate.SelectedDate);

            school_analyzed_count = qHtl_DailySchoolAbsenteeData.AnalyzeDailySchoolDataByDate(start_date, school_district_id);

            lblMessage.Text = "Data analysis complete for date ranage: " + Convert.ToString(rdtStartDate.SelectedDate) + " to " + Convert.ToString(rdtEndDate.SelectedDate) + ".<br>" + school_analyzed_count + " daily school records updated.";
            lblMessage.Visible = true;
            plhStep4.Visible = true;
            rdtStartDate.Clear();
            rdtEndDate.Clear();
        }
        else
        {
            lblMessage.Text = "* PROBLEM: You must select a start and end date to perform an analysis.";
        }
    }
}