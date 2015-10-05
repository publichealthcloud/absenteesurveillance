using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;

public partial class manage_school_districts_reports_controls_DailySchoolLevelDetails : System.Web.UI.UserControl
{
    protected qHtl_DailySchoolDistrictAbsenteeSummary summary;
    protected qHtl_AbsenteeAnalysisVariable variables;
    protected DateTime curr_date;

    public DateTime CurrDate
    {
        get { return curr_date; }
        set { curr_date = value; }
    }

    public qHtl_DailySchoolDistrictAbsenteeSummary Summary
    {
        get { return summary; }
        set { summary = value; }
    }

    public qHtl_AbsenteeAnalysisVariable Variables
    {
        get { return variables; }
        set { variables = value; }
    }  
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (summary != null)
            {
                if (summary.DailySchoolDistrictAbsenteeSummaryID > 0)
                {
                    DateTime curr_date = new DateTime();
                    curr_date = Convert.ToDateTime(summary.DataDate);

                    litElementarySchools.Text = Convert.ToString(summary.NumElementarySchools);
                    litElementarySchoolsStudentsEnrolled.Text = Convert.ToString(summary.TotalElementaryStudents);
                    litElementarySchoolStudentsAbsent.Text = Convert.ToString(summary.TotalElementaryStudentsAbsent);
                    litElementarySchoolAbsenteeRate.Text = Convert.ToString(Math.Round(summary.ElementarySchoolAbsenteeRate, 2));
                    litElementarySchoolWarningLevel.Text = Convert.ToString(Math.Round((summary.ElementarySchoolAbsenteeSTD * variables.RedRateSTDMultiplier), 2));

                    litJuniorHighSchools.Text = Convert.ToString(summary.NumJuniorHighs);
                    litJuniorHighStudents.Text = Convert.ToString(summary.TotalJuniorHighStudents);
                    litElementarySchoolStudentsAbsent.Text = Convert.ToString(summary.TotalJuniorHighStudentsAbsent);
                    litJunionHighAbsenteeRate.Text = Convert.ToString(Math.Round(summary.JuniorHighSchoolAbsenteeRate, 2));
                    litJuniorHighWarningLevel.Text = Convert.ToString(Math.Round((summary.JuniorHighSchoolAbsenteeSTD * variables.RedRateSTDMultiplier), 2));

                    litHighSchools.Text = Convert.ToString(summary.NumHighSchools);
                    litHighSchoolStudentsEnrolled.Text = Convert.ToString(summary.TotalHighSchoolStudents);
                    litHighSchoolStudentsAbsent.Text = Convert.ToString(summary.TotalHighSchoolStudentsAbsent);
                    litHighSchoolAbsenteeRates.Text = Convert.ToString(Math.Round(summary.HighSchoolAbsenteeRate, 2));
                    litHighSchoolWarningLevel.Text = Convert.ToString(Math.Round((summary.HighSchoolAbsenteeSTD * variables.RedRateSTDMultiplier), 2));
                }
            }
        }
    }
}