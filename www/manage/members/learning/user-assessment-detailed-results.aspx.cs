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

using Quartz.Learning;

public partial class manage_members_learning_user_assessment_defailed_results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int assessment_id = Convert.ToInt32(Request.QueryString["assessmentID"]);

        var results = qLrn_UserAssessment2.BuildUserAssessmentResultsList(assessment_id, 0, "assessmentID ASC");

        DataTable dt_results = new DataTable();
        dt_results.Columns.Add("UserAssessmentID", typeof(int));
        dt_results.Columns.Add("AssessmentID", typeof(int));
        dt_results.Columns.Add("UserID", typeof(int));
        dt_results.Columns.Add("Username", typeof(string));
        dt_results.Columns.Add("FirstName", typeof(string));
        dt_results.Columns.Add("LastName", typeof(string));
        dt_results.Columns.Add("Email", typeof(string));
        dt_results.Columns.Add("HighestRole", typeof(string));
        dt_results.Columns.Add("Profession", typeof(string)); 
        dt_results.Columns.Add("EmploymentLocation", typeof(string));
        dt_results.Columns.Add("EmploymentSetting", typeof(string));
        dt_results.Columns.Add("WorkSites", typeof(string));
        dt_results.Columns.Add("Degrees", typeof(string));
        dt_results.Columns.Add("Position", typeof(string));
        dt_results.Columns.Add("Agency", typeof(string));
        dt_results.Columns.Add("Division", typeof(string));
        dt_results.Columns.Add("Gender", typeof(string));
        dt_results.Columns.Add("Race", typeof(string));
        dt_results.Columns.Add("AssessmentType", typeof(string));
        dt_results.Columns.Add("AssessmentName", typeof(string));
        dt_results.Columns.Add("Created", typeof(DateTime));
        dt_results.Columns.Add("StartTime", typeof(DateTime));
        dt_results.Columns.Add("EndTime", typeof(DateTime));
        dt_results.Columns.Add("TotalNumberQuestions", typeof(int));
        dt_results.Columns.Add("TotalNumberCorrect", typeof(int));
        dt_results.Columns.Add("MemberScore", typeof(decimal));
        dt_results.Columns.Add("MinimumPassingScore", typeof(decimal));
        dt_results.Columns.Add("MemberOutcome", typeof(string));
        dt_results.Columns.Add("NeedsAssessmentID", typeof(int));
        dt_results.Columns.Add("NeedsAssessmentName", typeof(string));
        dt_results.Columns.Add("Cat1_Name", typeof(string));
        dt_results.Columns.Add("Cat1_Outcome", typeof(string));
        dt_results.Columns.Add("Cat1_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat1_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat1_Score", typeof(decimal));
        dt_results.Columns.Add("Cat2_Name", typeof(string));
        dt_results.Columns.Add("Cat2_Outcome", typeof(string));
        dt_results.Columns.Add("Cat2_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat2_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat2_Score", typeof(decimal));
        dt_results.Columns.Add("Cat3_Name", typeof(string));
        dt_results.Columns.Add("Cat3_Outcome", typeof(string));
        dt_results.Columns.Add("Cat3_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat3_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat3_Score", typeof(decimal));
        dt_results.Columns.Add("Cat4_Name", typeof(string));
        dt_results.Columns.Add("Cat4_Outcome", typeof(string));
        dt_results.Columns.Add("Cat4_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat4_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat4_Score", typeof(decimal));
        dt_results.Columns.Add("Cat5_Name", typeof(string));
        dt_results.Columns.Add("Cat5_Outcome", typeof(string));
        dt_results.Columns.Add("Cat5_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat5_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat5_Score", typeof(decimal));
        dt_results.Columns.Add("Cat6_Name", typeof(string));
        dt_results.Columns.Add("Cat6_Outcome", typeof(string));
        dt_results.Columns.Add("Cat6_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat6_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat6_Score", typeof(decimal));
        dt_results.Columns.Add("Cat7_Name", typeof(string));
        dt_results.Columns.Add("Cat7_Outcome", typeof(string));
        dt_results.Columns.Add("Cat7_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat7_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat7_Score", typeof(decimal));
        dt_results.Columns.Add("Cat8_Name", typeof(string));
        dt_results.Columns.Add("Cat8_Outcome", typeof(string));
        dt_results.Columns.Add("Cat8_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat8_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat8_Score", typeof(decimal));
        dt_results.Columns.Add("Cat9_Name", typeof(string));
        dt_results.Columns.Add("Cat9_Outcome", typeof(string));
        dt_results.Columns.Add("Cat9_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat9_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat9_Score", typeof(decimal));
        dt_results.Columns.Add("Cat10_Name", typeof(string));
        dt_results.Columns.Add("Cat10_Outcome", typeof(string));
        dt_results.Columns.Add("Cat10_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat10_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat10_Score", typeof(decimal));
        dt_results.Columns.Add("Cat11_Name", typeof(string));
        dt_results.Columns.Add("Cat11_Outcome", typeof(string));
        dt_results.Columns.Add("Cat11_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat11_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat11_Score", typeof(decimal));
        dt_results.Columns.Add("Cat12_Name", typeof(string));
        dt_results.Columns.Add("Cat12_Outcome", typeof(string));
        dt_results.Columns.Add("Cat12_NumQuestions", typeof(int));
        dt_results.Columns.Add("Cat12_NumCorrect", typeof(int));
        dt_results.Columns.Add("Cat12_Score", typeof(decimal));

        if (results != null)
        {
            foreach (var r in results)
            {
                string cat1_name = "na";
                string cat1_outcome = "na";
                int cat1_questions = 0;
                int cat1_correct = 0;
                decimal cat1_score = 0;
                string cat2_name = "na";
                string cat2_outcome = "na";
                int cat2_questions = 0;
                int cat2_correct = 0;
                decimal cat2_score = 0;
                string cat3_name = "na";
                string cat3_outcome = "na";
                int cat3_questions = 0;
                int cat3_correct = 0;
                decimal cat3_score = 0;
                string cat4_name = "na";
                string cat4_outcome = "na";
                int cat4_questions = 0;
                int cat4_correct = 0;
                decimal cat4_score = 0;
                string cat5_name = "na";
                string cat5_outcome = "na";
                int cat5_questions = 0;
                int cat5_correct = 0;
                decimal cat5_score = 0;
                string cat6_name = "na";
                string cat6_outcome = "na";
                int cat6_questions = 0;
                int cat6_correct = 0;
                decimal cat6_score = 0;
                string cat7_name = "na";
                string cat7_outcome = "na";
                int cat7_questions = 0;
                int cat7_correct = 0;
                decimal cat7_score = 0;
                string cat8_name = "na";
                string cat8_outcome = "na";
                int cat8_questions = 0;
                int cat8_correct = 0;
                decimal cat8_score = 0;
                string cat9_name = "na";
                string cat9_outcome = "na";
                int cat9_questions = 0;
                int cat9_correct = 0;
                decimal cat9_score = 0;
                string cat10_name = "na";
                string cat10_outcome = "na";
                int cat10_questions = 0;
                int cat10_correct = 0;
                decimal cat10_score = 0;
                string cat11_name = "na";
                string cat11_outcome = "na";
                int cat11_questions = 0;
                int cat11_correct = 0;
                decimal cat11_score = 0;
                string cat12_name = "na";
                string cat12_outcome = "na";
                int cat12_questions = 0;
                int cat12_correct = 0;
                decimal cat12_score = 0;

                int i = 0;

                foreach (var l in r.learning_objectives)
                {
                    i++;
                    if (i == 1)
                    {
                        cat1_name = l.category_name;
                        cat1_outcome = l.member_outcome;
                        cat1_questions = l.num_questions;
                        cat1_correct = l.num_correct;
                        cat1_score = l.member_score;
                    }
                    else if (i == 2)
                    {
                        cat2_name = l.category_name;
                        cat2_outcome = l.member_outcome;
                        cat2_questions = l.num_questions;
                        cat2_correct = l.num_correct;
                        cat2_score = l.member_score;
                    }
                    else if (i == 3)
                    {
                        cat3_name = l.category_name;
                        cat3_outcome = l.member_outcome;
                        cat3_questions = l.num_questions;
                        cat3_correct = l.num_correct;
                        cat3_score = l.member_score;
                    }
                    else if (i == 4)
                    {
                        cat4_name = l.category_name;
                        cat4_outcome = l.member_outcome;
                        cat4_questions = l.num_questions;
                        cat4_correct = l.num_correct;
                        cat4_score = l.member_score;
                    }
                    else if (i == 5)
                    {
                        cat5_name = l.category_name;
                        cat5_outcome = l.member_outcome;
                        cat5_questions = l.num_questions;
                        cat5_correct = l.num_correct;
                        cat5_score = l.member_score;
                    }
                    else if (i == 6)
                    {
                        cat6_name = l.category_name;
                        cat6_outcome = l.member_outcome;
                        cat6_questions = l.num_questions;
                        cat6_correct = l.num_correct;
                        cat6_score = l.member_score;
                    }
                    else if (i == 7)
                    {
                        cat7_name = l.category_name;
                        cat7_outcome = l.member_outcome;
                        cat7_questions = l.num_questions;
                        cat7_correct = l.num_correct;
                        cat7_score = l.member_score;
                    }
                    else if (i == 8)
                    {
                        cat8_name = l.category_name;
                        cat8_outcome = l.member_outcome;
                        cat8_questions = l.num_questions;
                        cat8_correct = l.num_correct;
                        cat8_score = l.member_score;
                    }
                    else if (i == 9)
                    {
                        cat9_name = l.category_name;
                        cat9_outcome = l.member_outcome;
                        cat9_questions = l.num_questions;
                        cat9_correct = l.num_correct;
                        cat9_score = l.member_score;
                    }
                    else if (i == 10)
                    {
                        cat10_name = l.category_name;
                        cat10_outcome = l.member_outcome;
                        cat10_questions = l.num_questions;
                        cat10_correct = l.num_correct;
                        cat10_score = l.member_score;
                    }
                    else if (i == 11)
                    {
                        cat11_name = l.category_name;
                        cat11_outcome = l.member_outcome;
                        cat11_questions = l.num_questions;
                        cat11_correct = l.num_correct;
                        cat11_score = l.member_score;
                    }
                    else if (i == 12)
                    {
                        cat12_name = l.category_name;
                        cat12_outcome = l.member_outcome;
                        cat12_questions = l.num_questions;
                        cat12_correct = l.num_correct;
                        cat12_score = l.member_score;
                    }
                }
                // add row
                dt_results.Rows.Add(r.user_assessment_id, r.assessment_id, r.user_id, r.username, r.first_name, r.last_name, r.email, r.highest_role, r.profession, r.employment_location, r.employment_setting, r.work_sites, r.degrees, r.position, r.agency, r.division, r.gender, r.race, r.assessment_type, r.assessment_name, r.created, r.start_time, r.end_time, r.total_number_questions, r.total_number_correct, r.member_score, Math.Round(r.min_passing_score, 2), r.member_outcome, r.needs_assessment_id, r.needs_assessment_name, cat1_name, cat1_outcome, cat1_questions, cat1_correct, cat1_score, cat2_name, cat2_outcome, cat2_questions, cat2_correct, cat2_score, cat3_name, cat3_outcome, cat3_questions, cat3_correct, cat3_score, cat4_name, cat4_outcome, cat4_questions, cat4_correct, cat4_score, cat5_name, cat5_outcome, cat5_questions, cat5_correct, cat5_score, cat6_name, cat6_outcome, cat6_questions, cat6_correct, cat6_score, cat7_name, cat7_outcome, cat7_questions, cat7_correct, cat7_score, cat8_name, cat8_outcome, cat8_questions, cat8_correct, cat8_score, cat9_name, cat9_outcome, cat9_questions, cat9_correct, cat9_score, cat10_name, cat10_outcome, cat10_questions, cat10_correct, cat10_score, cat11_name, cat11_outcome, cat11_questions, cat11_correct, cat11_score, cat12_name, cat12_outcome, cat12_questions, cat12_correct, cat12_score);
            }
        }

        radAssessmentResults.DataSource = dt_results;
        radAssessmentResults.DataBind();

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        radAssessmentResults.ExportSettings.ExportOnlyData = true;
        radAssessmentResults.ExportSettings.IgnorePaging = true;
        radAssessmentResults.ExportSettings.OpenInNewWindow = true;
        radAssessmentResults.ExportSettings.FileName = "AssessmentDetails_" + DateTime.Now;
        radAssessmentResults.MasterTableView.ExportToExcel();
    }
}