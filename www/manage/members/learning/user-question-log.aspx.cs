using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class qLrn_user_question_log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<QuestionLog> log = new List<QuestionLog>();

        var data = qLrn_UserQuestionLog_View.GetAllUserQuestionLogs();

        if (data != null)
        {
            if (data.Count > 0)
            {
                foreach (var q in data)
                {
                    int curr_user_id, curr_space_id, curr_assessment_id, curr_question_id, curr_user_correct, curr_training_id;
                    string curr_username, curr_first_name, curr_last_name, curr_space_name, curr_assessment_name, curr_assessment_type, curr_question;
                    string curr_choice1 = string.Empty; 
                    string curr_choice2 = string.Empty;
                    string curr_choice3 = string.Empty;
                    string curr_choice4 = string.Empty;
                    string curr_choice5 = string.Empty;
                    string curr_choice6 = string.Empty;
                    string curr_choice7 = string.Empty;
                    string curr_choice8 = string.Empty;
                    string curr_correct_answer = string.Empty;
                    string curr_user_answer = string.Empty;
                    string curr_training_title = string.Empty;
                    DateTime curr_timestamp = new DateTime();

                    curr_assessment_id = q.AssessmentID;
                    curr_training_id = q.TrainingID;
                    curr_training_title = q.Title;
                    curr_user_id = q.UserID;
                    curr_username = q.Username;
                    curr_first_name = q.FirstName;
                    curr_last_name = q.LastName;
                    curr_assessment_name = q.Name;
                    curr_assessment_type = q.AssessmentType;
                    curr_timestamp = q.Created;
                    curr_question_id = q.QuestionID;
                    curr_question = q.QuestionText;
                    curr_user_correct = Convert.ToInt32(q.Correct);

                    // get space information
                    curr_space_id = 0;
                    curr_space_name = string.Empty;

                    // get all question choice information
                    var choices = qLrn_QuestionElement.GetQuestionElements(q.QuestionID);

                    int i = 1;
                    if (choices != null)
                    {
                        foreach (var c in choices)
                        {
                            if (i == 1)
                            {
                                curr_choice1 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 2)
                            {
                                curr_choice2 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 3)
                            {
                                curr_choice3 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 4)
                            {
                                curr_choice4 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 5)
                            {
                                curr_choice5 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 6)
                            {
                                curr_choice6 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 7)
                            {
                                curr_choice7 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }
                            if (i == 8)
                            {
                                curr_choice8 = c.Details;
                                if (c.Correct == true)
                                    curr_correct_answer = c.Details;
                            }

                            if (q.QuestionElementID == c.QuestionElementID)
                                curr_user_answer = c.Details;

                            i++;
                        }
                    }

                    log.Add(new QuestionLog()
                    {
                        user_id = curr_user_id,
                        username = curr_username,
                        first_name = curr_first_name,
                        last_name = curr_last_name,
                        space_id = curr_space_id,
                        space_name = curr_space_name,
                        assessment_id = curr_assessment_id,
                        training_id = curr_training_id,
                        training_title = curr_training_title,
                        assessment_name = curr_assessment_name,
                        assessment_type = curr_assessment_type,
                        question_id = curr_question_id,
                        question = curr_question,
                        choice1 = curr_choice1,
                        choice2 = curr_choice2,
                        choice3 = curr_choice3,
                        choice4 = curr_choice4,
                        choice5 = curr_choice5,
                        choice6 = curr_choice6,
                        choice7 = curr_choice7,
                        choice8 = curr_choice8,
                        correct_answer = curr_correct_answer,
                        user_answer = curr_user_answer,
                        user_correct = curr_user_correct,
                        timestamp = Convert.ToDateTime(curr_timestamp)
                    });
                }
            }
        }

        RadGrid1.DataSource = log;

        if (!Page.IsPostBack)
        {

        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Question_Logs_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }

    public class QuestionLog
    {
        public int question_id { get; set; }
        public string question { get; set; }
        public string choice1 { get; set; }
        public string choice2 { get; set; }
        public string choice3 { get; set; }
        public string choice4 { get; set; }
        public string choice5 { get; set; }
        public string choice6 { get; set; }
        public string choice7 { get; set; }
        public string choice8 { get; set; }
        public string correct_answer { get; set; }
        public string user_answer { get; set; }
        public int user_correct { get; set; }
        public DateTime timestamp { get; set; }
        public int user_id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int space_id { get; set; }
        public string space_name { get; set; }
        public int training_id { get; set; }
        public string training_title { get; set; }
        public int assessment_id { get; set; }
        public string assessment_name { get; set; }
        public string assessment_type { get; set; }
    }
}
