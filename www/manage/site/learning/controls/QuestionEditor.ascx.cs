using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;
using Quartz.Learning;

public partial class manage_site_learning_controls_QuestionEditor : System.Web.UI.UserControl
{
    protected int QuestionID
    {
        get { return Convert.ToInt32(ViewState["question_id"]); }
        set { ViewState["question_id"] = value; }
    }

    protected int UsageCount { get; set; }
    protected ICollection<qLrn_Assessment> AssessmentsContainingQuestion { get; set; }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnNewQuestion.Text = "<i class=\"glyphicon-circle_plus\"></i> ADD QUESTION";
            
            if (Convert.ToString(Request.QueryString["form"]) == "new")
            {
                updateQuestionForm("new");
            }
            else
                updateQuestionForm("edit");
        }
    }

    protected void updateQuestionForm(string mode)
    {
        UpdateCategoriesList();

        if (mode == "new")
        {
            btnReloadQuestions.Visible = false;
            lblModeNewTitle.Text = "CREATE A NEW QUESTION";
            btnNewQuestion.Visible = false;
            plhNewQuestions.Visible = true;
            plhEditQuestion.Visible = false;
            btnReloadQuestions.Visible = false;
            plhQuestionDetails.Visible = true;
            btnDisplayQuestions.Text = "<i class=\"icon-circle-arrow-left\"></i> Back to Existing Questions";
            btnDisplayQuestions.Visible = true;
            btnNewQuestion.Visible = false;
            litQuestionEditTitle.Text = "Create New Question";
        }
        else
        {
            btnReloadQuestions.Visible = false;
            btnDisplayQuestions.Visible = false;
            lblMode.Text = "FIND A QUESTION";
            plhNewQuestions.Visible = false;
            plhQuestionDetails.Visible = true;
            plhEditQuestion.Visible = true;
            btnNewQuestion.Visible = true;
            int question_category_id = 0;
            litQuestionEditTitle.Text = "Edit Existing Question";
            if (!String.IsNullOrEmpty(Request.QueryString["questionID"]))
            {
                int question_id = Convert.ToInt32(Request.QueryString["questionID"]);
                qLrn_Question question = new qLrn_Question(question_id);
                lblTitle.Text = "Question Editor [ID:" + question.QuestionID + "]";
                if (question.QuestionCategoryID != null)
                    question_category_id = (int)question.QuestionCategoryID;
                QuestionID = question_id;

                RefreshEditFields();

                UpdateAssessmentView();

                EnableQuestionRemoval();

            }
            if (question_category_id > 0)
            {
                ddl_question_categories.SelectedValue = Convert.ToString(question_category_id);
                ddl_question_categories_inactive.SelectedValue = Convert.ToString(question_category_id);
            }
            UpdateQuestionsList();
            ddl_questions.SelectedValue = Convert.ToString(QuestionID);
        }

    }

    private void UpdateCategoriesList()
    {
        ddl_question_categories.DataSource = qLrn_QuestionCategory.GetCategories(null);
        ddl_question_categories.DataValueField = "QuestionCategoryID";
        ddl_question_categories.DataTextField = "Name";
        ddl_question_categories.DataBind();
        ddl_question_categories.Items.Insert(0, new ListItem("All Categories", "0"));

        ddl_question_categories_inactive.DataSource = qLrn_QuestionCategory.GetCategories(null);
        ddl_question_categories_inactive.DataValueField = "QuestionCategoryID";
        ddl_question_categories_inactive.DataTextField = "Name";
        ddl_question_categories_inactive.DataBind();
        ddl_question_categories_inactive.Items.Insert(0, new ListItem("All Categories", "0"));
    }

    private void UpdateQuestionsList ()
    {
        ddl_questions.Items.Clear();

        int category_id;

        if (int.TryParse(ddl_question_categories.SelectedValue, out category_id))
        {
            int? chosen_category_id = category_id > 0 ? (int?)category_id : null;

            ddl_questions.DataSource = qLrn_Question.GetQuestions (chosen_category_id);
            ddl_questions.DataTextField = "Text";
            ddl_questions.DataValueField = "QuestionID";
            ddl_questions.DataBind();
        }

        ddl_questions.Items.Insert(0, new ListItem("-", string.Empty));

        DisableQuestionRemoval();
    }

    protected void btn_save_question_OnClick(object sender, EventArgs args)
    {
        if (QuestionID > 0)
        {
            var question = new qLrn_Question(QuestionID);

            question.Text = txt_new_question.Text;
            question.AnswerText = txt_answer.Text;
            question.Created = DateTime.Now;
            question.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
            question.LastModified = DateTime.Now;
            question.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);

            int question_category_id = GetNewQuestionCategoryID();
            if (question_category_id > 0) question.QuestionCategoryID = question_category_id;

            question.Update();

            foreach (var c in ph_question_elements.Controls)
            {
                var element_control = c as IQuestionElementEditControl;

                if (element_control != null)
                {
                    int question_element_id = element_control.GetQuestionElementId ();

                    if (question_element_id > 0)
                    {
                        var e = new qLrn_QuestionElement(question_element_id);

                        if (string.IsNullOrEmpty (element_control.GetDetails ()))
                        {                            
                            e.MarkAsDelete = 1;
                        }
                        else 
                        {
                            e.Details = element_control.GetDetails ();
                            e.Correct = element_control.IsCorrect ();
                            e.AnswerOrder = element_control.GetOrderNumber();
                        }

                        e.Update ();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty (element_control.GetDetails ()))
                        {
                            var e = AddNewQuestionElement (question.QuestionID, element_control.GetDetails (), element_control.IsCorrect ());
                            element_control.SetQuestionElementId(e.QuestionElementID);
                        }
                    }
                }
            }
            Response.Redirect("question-editor.aspx?mode=edit&questionID=" + question.QuestionID);
        }
        else
        {
            var question = new qLrn_Question();

            question.Text = txt_new_question.Text;
            question.AnswerText = txt_answer.Text;
            question.Available = "Yes";
            question.Created = DateTime.Now;
            question.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);

            int question_category_id = GetNewQuestionCategoryID();
            if (question_category_id > 0) question.QuestionCategoryID = question_category_id;

            QuestionID = question.AddQuestion();
            
            foreach (var c in ph_question_elements.Controls)
            {
                var element_control = c as IQuestionElementEditControl;

                if (element_control != null)
                {
                    string element_details = element_control.GetDetails();

                    if (!string.IsNullOrEmpty(element_details))
                    {
                        var e = AddNewQuestionElement(question.QuestionID, element_details, element_control.IsCorrect());
                        element_control.SetQuestionElementId(e.QuestionElementID);
                    }
                }
            }
            
            /*
            updateQuestionForm("edit");
            UpdateQuestionsList();
            ddl_questions.SelectedValue = Convert.ToString(question.QuestionID);
            EnableQuestionRemoval();
             */
            Response.Redirect("question-editor.aspx?mode=edit&questionID=" + question.QuestionID);
        }

        MessageLog("SUCCESS: Question saved.");
    }

    protected int GetCurrentQuestionCategoryID()
    {
        int category_id;

        if (int.TryParse(ddl_question_categories.SelectedValue, out category_id))
        {
            return category_id;
        }
        else return -1;
    }

    protected int GetNewQuestionCategoryID()
    {
        int category_id;

        if (int.TryParse(ddl_question_categories_inactive.SelectedValue, out category_id))
        {
            return category_id;
        }
        else return -1;
    }

    protected void ddl_questions_OnSelectedIndexChanged(object sender, EventArgs args)
    {
        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            QuestionID = question_id;
            qLrn_Question question = new qLrn_Question(QuestionID);

            RefreshEditFields();

            UpdateAssessmentView();

            EnableQuestionRemoval ();

            plhQuestionDetails.Visible = true;

            if (question.QuestionCategoryID != null)
                ddl_question_categories_inactive.SelectedValue = Convert.ToString(question.QuestionCategoryID);
        }
    }

    private void EnableQuestionRemoval()
    {
        btn_remove_question.Enabled = true;
    }

    private void DisableQuestionRemoval()
    {
        btn_remove_question.Enabled = false;
    }

    protected void ddl_question_categories_OnSelectedIndexChanged(object sender, EventArgs args)
    {
        UpdateQuestionsList();

        DisableQuestionRemoval();

        plhQuestionDetails.Visible = false;
    }

    protected void RefreshEditFields ()
    {
        var question = new qLrn_Question(QuestionID);

        if (question.QuestionID > 0)
        {
            txt_new_question.Text = question.Text;
            txt_answer.Text = question.AnswerText;

            var question_elements = qLrn_QuestionElement.GetQuestionElements(QuestionID);

            for (int i = 0; i < question_elements.Count && i < 6; i++)
            {
                var c = ph_question_elements.Controls[i] as IQuestionElementEditControl;

                if (c != null && !(c is Literal))
                {
                    var q = question_elements.ElementAt (i);

                    c.SetDetails(q.Details);
                    c.SetCorrect(q.Correct);
                    c.SetQuestionElementId(q.QuestionElementID);
                }
            }
        }
    }

    protected qLrn_QuestionElement AddNewQuestionElement(int question_id, string details, bool is_correct)
    {
        var new_question_element = new qLrn_QuestionElement();

        new_question_element.QuestionID = question_id;
        new_question_element.Details = details;
        new_question_element.Correct = is_correct;

        new_question_element.AddQuestionElement();

        return new_question_element;
    }

    protected void btn_select_question_by_id_OnClick(object sender, EventArgs args)
    {
        int question_id;

        if (int.TryParse(txt_question_id.Text, out question_id))
        {
            QuestionID = question_id;

            RefreshEditFields();

            try
            {
                ddl_questions.SelectedValue = Convert.ToString(QuestionID);
            }
            catch
            {
            }
        }
    }

    private void UpdateAssessmentView()
    {
        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            AssessmentsContainingQuestion = qLrn_Assessment.GetAssessmentsByQuestion (question_id);

            UsageCount = AssessmentsContainingQuestion.Count;
        }
    }

    private void MessageLog (string message)
    {
        lbl_message_log.Text = message;
    }

    protected void btn_remove_question_OnClick(object sender, EventArgs args)
    {
        QuestionID = 0;

        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            var question_elements = qLrn_QuestionElement.GetQuestionElements(question_id);

            foreach (var e in question_elements)
            {
                e.MarkAsDelete = 1;
                e.Update();
            }

            var assessment_questions = qLrn_AssessmentQuestion.GetQuestionUsage(question_id);

            foreach (var q in assessment_questions)
            {
                q.MarkAsDelete = 1;
                q.LastModified = DateTime.Now;
                q.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);

                q.Update();
            }

            var assessment_id_collection = assessment_questions.Select(aq => aq.AssessmentID).Distinct();

            foreach (var a in assessment_id_collection)
            {
                if (qLrn_AssessmentQuestion_View.GetAssessmentQuestions(a).Count < 1)
                {
                    var assessment = new qLrn_Assessment(a);

                    assessment.MarkAsDelete = 1;

                    assessment.Update();
                }
            }

            var question = new qLrn_Question(question_id);

            question.MarkAsDelete = 1;
            question.LastModified = DateTime.Now;
            question.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);

            question.Update();

            UpdateQuestionsList();

            MessageLog("SUCCESS: Question removed.");

            Response.Redirect("question-editor.aspx");
        }
        else MessageLog("ERROR: You must select a question.");
    }

    protected void btn_new_question_OnClick(object sender, EventArgs args)
    {
        Response.Redirect("question-editor.aspx?mode=all&form=new");
    }

    protected void btnReloadQuestions_Click(object sender, EventArgs e)
    {
        Response.Redirect("question-editor.aspx?mode=all");
    }
}