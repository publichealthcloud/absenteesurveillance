using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Learning;
using Quartz.Social;

public partial class manage_site_learning_controls_AssessmentEditor : System.Web.UI.UserControl
{
    protected int TrainingID { get; set; }
    protected int SlideID { get; set; }

    protected string AssessmentType
    {
        get { return Convert.ToString(ViewState["assessment_type"]); }
        set { ViewState["assessment_type"] = value; }
    }

    protected int AssessmentID
    {
        get { return Convert.ToInt32(ViewState["assessment_id"]); }
        set { ViewState["assessment_id"] = value; }
    }

    protected int AssessmentQuestionCount
    {
        get { return Convert.ToInt32(ViewState["assessment_question_count"]); }
        set { ViewState["assessment_question_count"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        TrainingID = Convert.ToInt32(Request.QueryString["TrainingID"]);
        SlideID = Convert.ToInt32(Request.QueryString["SlideID"]);

        //ValidateAssessmentType();
         hplRefreshTop.NavigateUrl = Request.Url.ToString();

        if (!Page.IsPostBack)
        {
            AssessmentID = Convert.ToInt32(Request.QueryString["AssessmentID"]);
            hplBackTop.NavigateUrl = "/manage/site/learning/assessments-list.aspx";

            if (AssessmentID > 0)
            {
                AssessmentType = Request.QueryString["AssessmentType"];

                qLrn_Assessment assessment = new qLrn_Assessment(AssessmentID);
                txtAssessmentName.Text = assessment.Name;

                GetAssessmentID(false);

                UpdateCategoriesList();
                RefreshAssessmentQuestions();
                UpdateQuestionsList();
                RefreshTrainingQuestionsDropDown();

                RefreshReviewSlidesDropDown();

                lit_assessment_type.Text = AssessmentType;
                if (AssessmentID > 0)
                    lblAssessmentID.Text = "(ID: " + AssessmentID + ")";

                btn_add_existing_question.Visible = false;
                btn_update_existing_assessmentquestion.Visible = false;
                btn_cancel.Visible = false;
                plhAdditionalQuestionInfo.Visible = false;
                lblAssessmentInfoUpdateTitle.Text = "Update Assessment Info";

                // see if there is a feed item for this assessment
                var feed = qSoc_Feed.GetFeedItemByReferenceInfo(Convert.ToInt32(qSoc_ContentType.Types.GeneralAssessment), AssessmentID);

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        btnMakeAvailableCampaigns.Visible = false;
                        lblExistsFeed.Text = "<i class=\"icon-check\"></i> This assessment is available for use in campaigns";
                    }
                }
            }
            else
            {
                plhManageQuestion.Visible = false;
                plhManageQuestionsTitle.Visible = false;
                plhFilter.Visible = false;
                plhTools.Visible = false;
                btn_save_question_order.Visible = false;
                btnSaveAssessmentInfo.Text = "SAVE ASSESSMENT";
                btnSaveAssessmentInfo.CssClass = "btn btn-primary";
                lblAssessmentInfoUpdateTitle.Text = "New Assessment Info";
                btnMakeAvailableCampaigns.Visible = false;
            }
        }
    }

    private void UpdateCategoriesList()
    {
        ddl_question_categories.DataSource = qLrn_QuestionCategory.GetCategories(null);
        ddl_question_categories.DataValueField = "QuestionCategoryID";
        ddl_question_categories.DataTextField = "Name";
        ddl_question_categories.DataBind();

        ddl_question_categories.Items.Add(new ListItem("Uncategorized", "0"));

        if (ddl_question_categories.Items.Count == 1)
            plhFilter.Visible = false;
    }

    protected void ddl_question_categories_OnSelectedIndexChanged(object sender, EventArgs args)
    {
        UpdateQuestionsList();

        //DisableQuestionRemoval();
    }

    private void UpdateQuestionsList()
    {
        ddl_questions.Items.Clear();

        int category_id;

        if (int.TryParse(ddl_question_categories.SelectedValue, out category_id))
        {
            int? chosen_category_id = category_id > 0 ? (int?)category_id : null;

            ddl_questions.DataSource = qLrn_Question.GetQuestions(chosen_category_id);
            ddl_questions.DataTextField = "Text";
            ddl_questions.DataValueField = "QuestionID";
            ddl_questions.DataBind();
        }

        ddl_questions.Items.Insert(0, new ListItem("-", string.Empty));
    }

    private void RefreshReviewSlidesDropDown()
    {
        ph_review_slides.Visible = TrainingID > 0;

        if (TrainingID > 0)
        {
            ddl_review_slides.Items.Clear();

            var slides = qLrn_TrainingSlide_NavigationView.GetSlides(TrainingID);

            foreach (var s in slides)
            {
                var i = new ListItem(string.Format("{0} - {1} (SlideID={2})", s.SlideOrder, s.Title, s.SlideID), Convert.ToString(s.SlideID));

                ddl_review_slides.Items.Add(i);
            }

            ddl_review_slides.Items.Insert(0, new ListItem("", string.Empty));
        }
    }

    private void GetAssessmentID(bool create_if_necessary)
    {
        qLrn_Assessment assessment = null;

        if (AssessmentID > 0)
        {
            assessment = new qLrn_Assessment(AssessmentID);
        }
        else if (SlideID > 0)
        {
            assessment = qLrn_Assessment.GetAssessmentForSlide(SlideID);
        }
        else if (TrainingID > 0)
        {
            assessment = qLrn_Assessment.GetTrainingAssessment(TrainingID, AssessmentType);
        }

        if (assessment != null && assessment.AssessmentID > 0)
        {
            AssessmentID = assessment.AssessmentID;
            AssessmentType = assessment.AssessmentType;
        }
        else if (create_if_necessary)
        {
            var new_assessment = new qLrn_Assessment();

            new_assessment.AssessmentType = AssessmentType;
            if (TrainingID > 0) new_assessment.TrainingID = TrainingID;
            if (SlideID > 0) new_assessment.SlideID = SlideID;

            new_assessment.Insert();

            AssessmentID = new_assessment.AssessmentID;
        }

        btn_remove_assessment.Visible = AssessmentID > 0;
    }

    private void ValidateAssessmentType()
    {
        if (AssessmentType != "pre" && AssessmentType != "post" && AssessmentType != "embedded")
            throw new Exception("Invalid assessment type.");
    }

    private void RefreshTrainingQuestionsDropDown()
    {
        ICollection<qLrn_Question> training_questions;

        //if (TrainingID > 0) training_questions = qLrn_Question.GetQuestionsByTrainingID(TrainingID);
        //else training_questions = qLrn_Question.GetNonTrainingQuestions();

        training_questions = qLrn_Question.GetAllQuestions();

        ddl_questions.DataSource = training_questions;
        ddl_questions.DataTextField = "Text";
        ddl_questions.DataValueField = "QuestionID";
        ddl_questions.DataBind();

        ddl_questions.Items.Insert(0, new ListItem("-", string.Empty));
    }

    private void RefreshAssessmentQuestions()
    {
        if (AssessmentID > 0)
        {
            var assessment_questions = qLrn_AssessmentQuestion_View.GetAssessmentQuestions(AssessmentID);

            AssessmentQuestionCount = assessment_questions.Count;

            rpt_assessment_questions.DataSource = assessment_questions;
        }
        else
        {
            AssessmentQuestionCount = 0;

            rpt_assessment_questions.DataSource = null;
        }

        rpt_assessment_questions.DataBind();

        btn_save_question_order.Visible = AssessmentQuestionCount > 0;
        btn_remove_assessment.Visible = AssessmentID > 0;
    }

    protected void ddl_questions_OnSelectedIndexChanged(object sender, EventArgs args)
    {
        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            rbl_question_elements.Visible = true;
            plhAdditionalQuestionInfo.Visible = true;

            qLrn_Assessment assess = new qLrn_Assessment(AssessmentID);
            if (assess.AssessmentType == "pre")
            {
                ddl_number_attempts.Enabled = false;
                ddl_number_attempts.SelectedValue = Convert.ToString(1);
                ddl_review_slides.Enabled = false;
                rfv_existing_question_attempts.Enabled = false;
                rfv_review_slides.Enabled = false;
                lblPreWarning.Text = "*** Number of Attempts and Review Slide cannot be set for Pre-Assessments *** <br>";
            }

            var question = new qLrn_Question(question_id);

            var question_elements = qLrn_QuestionElement.GetQuestionElements(question_id);

            rbl_question_elements.DataSource = question_elements;
            rbl_question_elements.DataTextField = "Details";
            rbl_question_elements.DataValueField = "QuestionElementID";
            rbl_question_elements.DataBind();

            var correct_question_element = (from q in question_elements where q.Correct select q).FirstOrDefault();

            if (correct_question_element != null) rbl_question_elements.SelectedValue = Convert.ToString(correct_question_element.QuestionElementID);

            btn_add_existing_question.Visible = true;
            btn_add_existing_question.Enabled = true;

            var assessment_question = qLrn_AssessmentQuestion.GetAssessmentQuestion(AssessmentID, question_id);

            if (assessment_question != null)
            {
                if (assessment_question.AssessmentID > 0)
                {
                    // question has already been added -- change to Edit mode
                    ddl_number_attempts.SelectedValue = Convert.ToString(assessment_question.NumAttemptsAllowed);
                    btn_update_existing_assessmentquestion.Visible = true;
                    btn_add_existing_question.Visible = false;
                    lblQuestionMessage.Text = "*** This question has already been added. You can only update the existing question. ***";
                }
            }
            else
            {
                // question has not been added before
                ddl_number_attempts.SelectedValue = "";
                btn_add_existing_question.Visible = true;
                btn_update_existing_assessmentquestion.Visible = false;
                lblQuestionMessage.Text = "";
            }
        }
        else
        {
            rbl_question_elements.Items.Clear();
            btn_add_existing_question.Visible = false;
            lblQuestionMessage.Text = "";
        }
    }

    protected void rpt_assessment_questions_OnItemDataBound(object sender, RepeaterItemEventArgs args)
    {
        if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
        {
            var rbl_rpt_question_elements = args.Item.FindControl("rbl_question_elements") as RadioButtonList;

            if (rbl_rpt_question_elements != null)
            {
                var assessment_question = args.Item.DataItem as qLrn_AssessmentQuestion_View;

                var question_elements = qLrn_QuestionElement.GetQuestionElements(assessment_question.QuestionID);

                rbl_rpt_question_elements.DataSource = question_elements;
                rbl_rpt_question_elements.DataTextField = "Details";
                rbl_rpt_question_elements.DataValueField = "QuestionElementID";
                rbl_rpt_question_elements.DataBind();

                var correct_question_element = (from e in question_elements where e.Correct select e).FirstOrDefault();

                if (correct_question_element != null) rbl_rpt_question_elements.SelectedValue = Convert.ToString(correct_question_element.QuestionElementID);
            }
        }
    }

    protected void btn_add_existing_question_OnClick(object sender, EventArgs args)
    {
        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            GetAssessmentID(true);

            var new_assessment_question = new qLrn_AssessmentQuestion();

            new_assessment_question.AssessmentID = AssessmentID;
            new_assessment_question.QuestionID = question_id;
            new_assessment_question.QuestionOrder = AssessmentQuestionCount;
            int review_slide_id;
            // change behaviors based on assessment type
            qLrn_Assessment assess = new qLrn_Assessment(AssessmentID);
            if (assess.AssessmentType == "pre")
            {
                new_assessment_question.NumAttemptsAllowed = 1;
                new_assessment_question.ReviewSlideID = 0;
            }
            else
            {
                if (TrainingID > 0 && int.TryParse(ddl_review_slides.SelectedValue, out review_slide_id))
                {
                    new_assessment_question.ReviewSlideID = review_slide_id;
                }
                new_assessment_question.NumAttemptsAllowed = Convert.ToInt32(ddl_number_attempts.SelectedValue);
            }

            new_assessment_question.Insert();

            RefreshAssessmentQuestions();

            btn_add_existing_question.Visible = false;
            rpt_assessment_questions.Visible = true;

            ResetQuestions();
            lbl_save_question_order_message.Text = "*** Question successfully added. ***";
        }
    }

    protected void ResetQuestions()
    {
        ddl_questions.SelectedValue = "";
        rbl_question_elements.Visible = false;
        ddl_number_attempts.SelectedValue = "1";
        lblQuestionMessage.Text = "";
        plhAdditionalQuestionInfo.Visible = false;
    }

    protected void btn_update_existing_assessmentquestion_OnClick(object sender, EventArgs args)
    {
        int question_id;

        if (int.TryParse(ddl_questions.SelectedValue, out question_id))
        {
            GetAssessmentID(true);

            var new_assessment_question = new qLrn_AssessmentQuestion(question_id, AssessmentID);

            new_assessment_question.NumAttemptsAllowed = Convert.ToInt32(ddl_number_attempts.SelectedValue);
            if (!String.IsNullOrEmpty(ddl_review_slides.SelectedValue))
                new_assessment_question.ReviewSlideID = Convert.ToInt32(ddl_review_slides.SelectedValue);
            new_assessment_question.Update();
            lbl_save_question_order_message.Text = "*** Question successfully updated. ***";

            RefreshAssessmentQuestions();

            btn_remove_assessment.Visible = true;
            btn_save_question_order.Visible = true;
            rpt_assessment_questions.Visible = true;

            ddl_number_attempts.SelectedValue = "1";
            ddl_questions.SelectedValue = "";
            btn_update_existing_assessmentquestion.Visible = false;
            btn_cancel.Visible = false;
            lblLegend.Text = "Add New Question";
            ddl_questions.Enabled = true;
            ResetQuestions();
        }
    }

    protected void pbh_save_question_order_OnPostBack(object sender, string args)
    {
        if (!string.IsNullOrEmpty(args))
        {
            GetAssessmentID(true);

            var assessment_question_id_list = args.Split(',');

            for (int i = 0; i < assessment_question_id_list.Length; i++)
            {
                int assessment_question_id = Convert.ToInt32(assessment_question_id_list[i].Split('_').Last());

                var question = new qLrn_AssessmentQuestion(assessment_question_id);

                question.QuestionOrder = i;

                question.Update();
            }

            RefreshAssessmentQuestions();

            lbl_save_question_order_message.Text = "*** Question Order saved. ***";
        }
    }

    protected void btn_remove_assessment_OnClick(object sender, EventArgs args)
    {
        GetAssessmentID(false);

        if (AssessmentID > 0)
        {
            var assessment_questions = qLrn_AssessmentQuestion.GetAssessmentQuestionsByAssessmentID(AssessmentID);

            foreach (var q in assessment_questions)
            {
                q.DeleteAssessmentQuestion(q.AssessmentQuestionID);
            }

            RemoveCurrentAssessment();

            RefreshAssessmentQuestions();

            lbl_save_question_order_message.Text = "*** Assessment removed. ***";
        }
    }

    protected void pbh_remove_assessment_question_OnPostBack(object sender, string args)
    {
        ResetQuestions();

        if (!string.IsNullOrEmpty(args))
        {
            GetAssessmentID(false);

            if (AssessmentID > 0)
            {
                int assessment_question_id = Convert.ToInt32(args);

                var assessment_question = new qLrn_AssessmentQuestion(assessment_question_id);

                assessment_question.DeleteAssessmentQuestion(assessment_question_id);

                if (qLrn_AssessmentQuestion_View.GetAssessmentQuestions(AssessmentID).Count < 1) RemoveCurrentAssessment();

                RefreshAssessmentQuestions();

                lbl_save_question_order_message.Text = "*** Question removed. ***";
            }
        }
    }

    protected void pbh_edit_assessment_question_OnPostBack(object sender, string args)
    {
        lblLegend.Text = "Edit Existing Question";
        ddl_questions.Enabled = false;
        plhFilter.Visible = false;
        plhAdditionalQuestionInfo.Visible = true;

        if (!string.IsNullOrEmpty(args))
        {
            GetAssessmentID(false);

            if (AssessmentID > 0)
            {
                int assessment_question_id = Convert.ToInt32(args);

                var assessment_question = new qLrn_AssessmentQuestion(assessment_question_id);

                ddl_questions.SelectedValue = Convert.ToString(assessment_question.QuestionID);
                ddl_review_slides.SelectedValue = Convert.ToString(assessment_question.ReviewSlideID);
                ddl_number_attempts.SelectedValue = Convert.ToString(assessment_question.NumAttemptsAllowed);
                btn_add_existing_question.Visible = false;
                btn_save_question_order.Visible = false;
                btn_update_existing_assessmentquestion.Visible = true;
                btn_remove_assessment.Visible = false;
                rpt_assessment_questions.Visible = false;
                btn_cancel.Visible = true;
            }
        }
    }

    private void RemoveCurrentAssessment()
    {
        var assessment = new qLrn_Assessment(AssessmentID);
        assessment.Available = "No";
        assessment.MarkAsDelete = 1;
        assessment.Update();

        AssessmentID = 0;

        Response.Redirect("/manage/site/learning/assessments-list.aspx");
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/assessment-editor.aspx?assessmentID=" + Request.QueryString["assessmentID"]);
    }

    protected void btnSaveAssessmentInfo_Click(object sender, EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            AssessmentID = Convert.ToInt32(Request.QueryString["AssessmentID"]);

            if (AssessmentID > 0)
            {
                qLrn_Assessment assessment = new qLrn_Assessment(Convert.ToInt32(Request.QueryString["assessmentID"]));
                assessment.Name = txtAssessmentName.Text;
                assessment.Update();

                Response.Redirect("assessment-editor.aspx?assessmentID=" + Request.QueryString["assessmentID"]);
            }
            else
            {
                qLrn_Assessment assessment = new qLrn_Assessment();
                assessment.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                assessment.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                assessment.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                assessment.Available = "Yes";
                assessment.MarkAsDelete = 0;
                assessment.LastModified = DateTime.Now;
                assessment.Created = DateTime.Now;
                assessment.Name = txtAssessmentName.Text;
                assessment.AssessmentType = "general";
                assessment.Insert();

                Response.Redirect("assessment-editor.aspx?assessmentID=" + assessment.AssessmentID);
            }
        }
    }

    protected void btnMakeAvailableCampaigns_Click(object sender, EventArgs e)
    {
        AssessmentID = Convert.ToInt32(Request.QueryString["AssessmentID"]);
        
        qSoc_Feed feed = new qSoc_Feed();
        feed.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
        feed.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
        feed.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        feed.Available = "Yes";
        feed.MarkAsDelete = 0;
        feed.LastModified = DateTime.Now;
        feed.Created = DateTime.Now;
        feed.OwnerID = Convert.ToInt32(Context.Items["UserID"]);
        feed.ReferenceID = AssessmentID;
        feed.ContentTypeID = Convert.ToInt32(qSoc_ContentType.Types.GeneralAssessment);
        feed.Type = "assessment";
        feed.Title = txtAssessmentName.Text;
        feed.Description = txtAssessmentName.Text;
        feed.VisibleFeed = false;
        feed.VisibleOwnerFeed = false;
        feed.VisibleOwnerProfile = false;
        feed.VisibleExplore = false;
        feed.VisibleCampaign = false;
        feed.UploadedFrom = "manager";
        feed.Insert();

        Response.Redirect("~/manage/site/learning/assessment-editor.aspx?assessmentID=" + AssessmentID);
    }
}