using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;

using Telerik.Web.UI;

using Quartz;
using Quartz.Core;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;

public partial class edit_question_category : System.Web.UI.Page
{
    public int question_category_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ArticleFolder"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["questionCategoryID"]))
            {
                question_category_id = Convert.ToInt32(Request.QueryString["questionCategoryID"]);
                ViewState.Add("vsQuestionCategoryID", question_category_id);

                qLrn_QuestionCategory category = new qLrn_QuestionCategory(question_category_id);

                lblTitle.Text = "Edit Question Category (ID: " + category.QuestionCategoryID + ")";
                txtTitle.Text = category.Name;
                rblAvailable.SelectedValue = category.Available;

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }
            }

            else
            {
                lblTitle.Text = "New Question Category";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(question_category_id)))
            question_category_id = (Int32)ViewState["vsQuestionCategoryID"];
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["questionCategoryID"]))
            {
                question_category_id = Convert.ToInt32(Request.QueryString["questionCategoryID"]);
                qLrn_QuestionCategory category = new qLrn_QuestionCategory(question_category_id);
                category.Name = txtTitle.Text;
                category.LastModified = DateTime.Now;
                category.LastModifiedBy = user_id;
                category.Available = rblAvailable.SelectedValue;
                category.Update();
            }
            else
            {
                qLrn_QuestionCategory category = new qLrn_QuestionCategory();
                category.ScopeID = 1;
                category.Created = DateTime.Now;
                category.CreatedBy = user_id;
                category.LastModified = DateTime.Now;
                category.LastModifiedBy = user_id;
                category.Available = "Yes";
                category.MarkAsDelete = 0;
                category.Name = txtTitle.Text;
                category.LastModified = DateTime.Now;
                category.LastModifiedBy = user_id;
                category.Available = rblAvailable.SelectedValue;
                category.Insert();

                question_category_id = category.QuestionCategoryID;
            }

            string user_name = (new qPtl_User(user_id)).UserName;

            if (!String.IsNullOrEmpty(Request.QueryString["questionCategoryID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("question-category-list.aspx");
            }
            else
            {
                //Response.Redirect(Request.Url.ToString() + "?mode=add-successful&questionCategoryID=" + tip_id);
                Response.Redirect("question-category-list.aspx");
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        question_category_id = Convert.ToInt32(Request.QueryString["questionCategoryID"]);
        qLrn_QuestionCategory category = new qLrn_QuestionCategory(question_category_id);
        category.Available = "No";
        category.MarkAsDelete = 1;
        category.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("question-category-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("question-category-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        Response.Redirect("question-category-list.aspx");
    }
}
