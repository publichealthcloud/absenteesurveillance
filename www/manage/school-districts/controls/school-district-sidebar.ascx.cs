using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Report;
using Quartz.Social;
using Quartz.Portal;
using Quartz.Health;

public partial class manage_school_districts_controls_sidebar : System.Web.UI.UserControl
{
    protected int school_district_id, num_warnings;

    protected qHtl_DailySchoolDistrictAbsenteeSummary summary;

    public int SchoolDistrictID
    {
        get { return school_district_id; }
        set { school_district_id = value; }
    }

    public int NumWarnings
    {
        get { return num_warnings; }
        set { num_warnings = value; }
    }

    public qHtl_DailySchoolDistrictAbsenteeSummary Summary
    {
        get { return summary; }
        set { summary = value; }
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (school_district_id == 0)
                school_district_id = 1;

            qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);
            
            if (summary == null)
            {
                if (String.IsNullOrEmpty(Request.QueryString["currDate"]))
                {
                    summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetMostRecentDailySummary();
                }
                else
                {
                    string eval_curr_date = Convert.ToString(Request.QueryString["currDate"]);
                    summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetDailySummaryByDate(eval_curr_date);
                }               
            }

            if (summary != null)
            {
                if (summary.DailySchoolDistrictAbsenteeSummaryID > 0)
                {
                    DateTime curr_date = new DateTime();
                    curr_date = Convert.ToDateTime(summary.DataDate);
                    litMostRecentDate.Text = String.Format("{0:MMMM d, yyyy}", curr_date);
                    litEnrolled.Text = String.Format("{0:0,0}", summary.TotalEnrolled);
                    litAbsences.Text = String.Format("{0:0,0}", summary.TotalAbsent);

                    int total_illness = summary.Gastrointestinal + summary.Rash + summary.Respiratory + summary.Rash + summary.OtherIllness;
                    int total_unknown = summary.TotalAbsent - total_illness;
                    litIllness.Text = String.Format("{0:0,0}", total_illness);
                    litUnknown.Text = String.Format("{0:0,0}", total_unknown);
                    litRate.Text = Convert.ToString(Math.Round(summary.OverallAbsenteeRate, 2));
                    litMovingAverage.Text = Convert.ToString(Math.Round(summary.HistoricAbsenteeRate, 2));

                    decimal num_gast = Convert.ToDecimal(summary.Gastrointestinal);
                    decimal num_resp = Convert.ToDecimal(summary.Respiratory);
                    decimal num_rash = Convert.ToDecimal(summary.Rash);
                    decimal num_othr = Convert.ToDecimal(summary.OtherIllness);
                    decimal num_illness_total = num_gast + num_resp + num_rash + num_othr;

                    decimal percent_gast = 0;
                    decimal percent_resp = 0;
                    decimal percent_rash = 0;
                    decimal percent_othr = 0;

                    if (num_gast > 0)
                    {
                        percent_gast = num_gast / num_illness_total;
                        percent_gast = percent_gast * 100;
                    }
                    if (num_resp > 0)
                    {
                        percent_resp = num_resp / num_illness_total;
                        percent_resp = percent_resp * 100;
                    }
                    if (num_rash > 0)
                    {
                        percent_rash = num_rash / num_illness_total;
                        percent_rash = percent_rash * 100;
                    }
                    if (num_othr > 0)
                    {
                        percent_othr = num_othr / num_illness_total;
                        percent_othr = percent_othr * 100;
                    }
                    litGastBar.Text = "style=\"width:" + percent_gast + "%\"";
                    litRespBar.Text = "style=\"width:" + percent_resp + "%\"";
                    litRashBar.Text = "style=\"width:" + percent_rash + "%\"";
                    litOthrBar.Text = "style=\"width:" + percent_othr + "%\"";

                    litGastrointestinal.Text = "(" + Convert.ToString(summary.Gastrointestinal) + ")";
                    litRespiratory.Text = "(" + Convert.ToString(summary.Respiratory) + ")";
                    litRash.Text = "(" + Convert.ToString(summary.Rash) + ")";
                    litOtherIllness.Text = "(" + Convert.ToString(summary.OtherIllness) + ")";

                    litProcessed.Text = Convert.ToString(summary.Created);

                    if (num_warnings > 0)
                        litNumWarnings.Text = "<a href=\"/manage/school-districts/school-health-warnings.aspx\">" + num_warnings + " A-E Warnings</a>";
                    else
                    {
                        int num_a_e_warnings = 0;

                        num_a_e_warnings = qHtl_DailySchoolAbsenteeData.CountNumberAEWarningsByDate(Convert.ToDateTime(summary.DataDate));

                        if (num_a_e_warnings > 0)
                            litNumWarnings.Text = "<i class=\"icon-warning-sign\"></i> <strong>(" + num_a_e_warnings + ")</strong> Absentee Warnings";
                        else
                            litNumWarnings.Text = "No warnings for this date";
                    }
                }
            }
        }
    }
}