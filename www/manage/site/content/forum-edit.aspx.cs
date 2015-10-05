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
using Quartz.Communication;

public partial class edit_forum : System.Web.UI.Page
{
    public int forum_topic_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ArticleFolder"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["forumTopicID"]))
            {
                forum_topic_id = Convert.ToInt32(Request.QueryString["forumTopicID"]);

                ViewState.Add("vsForumTopicID", forum_topic_id);

                qCom_ForumTopic topic = new qCom_ForumTopic(forum_topic_id);

                lblTitle.Text = "Edit Forum (ID: " + topic.ForumTopicID + ")";
                txtTitle.Text = topic.Name;
                txtSummary.Text = topic.Description;

                rblAvailable.SelectedValue = topic.Available;

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }
            }

            else
            {
                lblTitle.Text = "New Forum";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(forum_topic_id)))
            forum_topic_id = (Int32)ViewState["vsForumTopicID"];

    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["forumTopicID"]))
            {
                forum_topic_id = Convert.ToInt32(Request.QueryString["forumTopicID"]);

                qCom_ForumTopic topic = new qCom_ForumTopic(forum_topic_id);
                topic.Name = txtTitle.Text;
                topic.Description = txtSummary.Text;
                topic.LastModified = DateTime.Now;
                topic.LastModifiedBy = user_id;
                topic.Available = rblAvailable.SelectedValue;
                topic.Update();
            }
            else
            {
                qCom_ForumTopic topic = new qCom_ForumTopic();
                topic.ScopeID = 1;
                topic.Created = DateTime.Now;
                topic.CreatedBy = user_id;
                topic.LastModified = DateTime.Now;
                topic.LastModifiedBy = user_id;
                topic.Available = "Yes";
                topic.MarkAsDelete = 0;
                topic.Name = txtTitle.Text;
                topic.Description = txtSummary.Text;
                topic.LastModified = DateTime.Now;
                topic.LastModifiedBy = user_id;
                topic.Available = rblAvailable.SelectedValue;
                topic.Insert();

                forum_topic_id = topic.ForumTopicID;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["forumTopicID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("forums-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&forumTopicID=" + forum_topic_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        forum_topic_id = Convert.ToInt32(Request.QueryString["forumTopicID"]);
        qCom_ForumTopic topic = new qCom_ForumTopic(forum_topic_id);
        topic.Available = "No";
        topic.MarkAsDelete = 1;
        topic.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("forums-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("forums-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("forums-list.aspx");
    }
}
