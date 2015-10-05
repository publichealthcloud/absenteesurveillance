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
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.Communication;

public partial class warning_edit : System.Web.UI.Page
{
    public int warning_id;
    public int actor_id;
    public string actor;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["source"] == "tasks")
            {
                hplBackTop.NavigateUrl = "~/qPtl/task-list.aspx?searchType=open";
                hplBackTop.Text = "<img src=\"../images/PagingPrev.gif\" border=\"0\">&nbsp;&nbsp;Back to Task List";
                hplBackBottom.NavigateUrl = "~/qPtl/task-list.aspx?searchType=open";
                hplBackBottom.Text = "<img src=\"../images/PagingPrev.gif\" border=\"0\">&nbsp;&nbsp;Back to Task List";
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["status"]))
            {
                hplBackTop.NavigateUrl = "warnings-list.aspx?status=" + Request.QueryString["status"];
                hplBackBottom.NavigateUrl = "warnings-list.aspx?status=" + Request.QueryString["status"];
            }
            else
            {
                hplBackTop.NavigateUrl = "warnings-list.aspx";
                hplBackBottom.NavigateUrl = "warnings-list.aspx";
            }

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();
            
            if (!String.IsNullOrEmpty(Request.QueryString["warningID"]))
            {
                warning_id = Convert.ToInt32(Request.QueryString["warningID"]);
                qSoc_Warning warning = new qSoc_Warning(warning_id);
                if (!String.IsNullOrEmpty(warning.BannedWords))
                {
                    plhBannedWord.Visible = true;
                    lblBannedWords.Text = warning.BannedWords;
                    qSoc_ContentType content = new qSoc_ContentType(warning.ContentTypeID);
                    lblWarningType.Text = warning.Message;
                    ddlStatus.SelectedValue = warning.Status;

                    lblContent.Text = content.Name;
                    string content_info = content.Name;
                    if (content.ContentTypeID == 6 && warning.ReferenceID > 0)
                    {
                        qSoc_Blog2 blog = new qSoc_Blog2(warning.ReferenceID);
                        lblFullText.Text = blog.Text;
                    }
                    else if (content.ContentTypeID == 8 && warning.ReferenceID > 0)
                    {
                        // message reply
                        //qCom_MessageReplies m_reply = new qCom_MessageReplies(warning.ReferenceID);
                        //lblFullText.Text = m_reply.post;
                    }
                    else if (content.ContentTypeID == 9 && warning.ReferenceID > 0)
                    {
                        // message thread
                        qCom_MessageThreads m_thread = new qCom_MessageThreads(warning.ReferenceID);
                        lblFullText.Text = m_thread.subject;
                    }
                    else if (content.ContentTypeID == 10 && warning.ReferenceID > 0)
                    {
                        // forum thread
                        qCom_ForumThread f_thread = new qCom_ForumThread(warning.ReferenceID);
                        lblFullText.Text = f_thread.Subject;
                    }
                    else if (content.ContentTypeID == 11 && warning.ReferenceID > 0)
                    {
                        // forum reply
                        qCom_ForumPost f_post = new qCom_ForumPost(warning.ReferenceID);
                        lblFullText.Text = f_post.Post;
                    }
                    else if (content.ContentTypeID == 31 && warning.ReferenceID > 0)
                    {
                        // shout
                        qSoc_Comment shout = new qSoc_Comment(warning.ReferenceID);
                        lblFullText.Text = shout.Comment;
                    }
                    else if (content.ContentTypeID == 33 && warning.ReferenceID > 0)
                    {
                        // comment
                        qSoc_Comment shout = new qSoc_Comment(warning.ReferenceID);
                        lblFullText.Text = shout.Comment;
                    }
                    else if (content.ContentTypeID == 40 && warning.ReferenceID > 0)
                    {
                        // status update
                        qSoc_Comment shout = new qSoc_Comment(warning.ReferenceID);
                        lblFullText.Text = shout.Comment;
                    }
                }
                else
                    plhBannedWord.Visible = false;

                lblPostedTime.Text = Convert.ToString(warning.Created);
                lblTitle.Text = "Edit Warning (ID: " + warning.WarningID + ")";

                qPtl_User user = new qPtl_User(warning.ActorID);
                actor_id = user.UserID;
                actor = user.UserName;

                if (actor_id != warning.LastModifiedBy && warning.LastModifiedBy > 0)
                {
                    qPtl_User reviewed_by = new qPtl_User(warning.LastModifiedBy);
                    lblReviewedBy.Text = "Reviewed by " + reviewed_by.UserName + " at " + warning.LastModified;
                }
                else
                    lblReviewedBy.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["warningID"]))
        {  
            warning_id = Convert.ToInt32(Request.QueryString["warningID"]);
            qSoc_Warning warning = new qSoc_Warning(warning_id);

            warning.Status = ddlStatus.SelectedValue;
            warning.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            warning.LastModified = DateTime.Now;
            warning.Update();

            // if being processed as a task then close task
            if (Request.QueryString["source"] == "tasks")
            {
                int task_id = Convert.ToInt32(Request.QueryString["taskID"]);
                qPtl_Task task = new qPtl_Task(task_id);

                if (ddlStatus.SelectedValue == "Reviewed")
                {
                    task.PercentCompleted = 100;
                    task.Status = "Completed";
                    task.Update();
                }
            }
            else
            {
                // check to see if there there a pending task
                qPtl_Task task = new qPtl_Task(warning.ContentTypeID, warning_id);

                if (task != null)
                {
                    if (task.TaskID > 0)
                    {
                        task.PercentCompleted = 100;
                        task.Status = "Completed";
                        task.Update();
                    }
                }
            }
        }

        if (Request.QueryString["source"] == "tasks")
            Response.Redirect("~/manage/members/task-list.aspx?searchType=open");
        else if (!String.IsNullOrEmpty(Request.QueryString["status"]))
            Response.Redirect("~/manage/members/warnings-list.aspx?status=" + Request.QueryString["status"]);
        else
            Response.Redirect("~/qSoc/warnings-list.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        warning_id = Convert.ToInt32(Request.QueryString["warningID"]);
        qSoc_Warning warning = new qSoc_Warning(warning_id);

        warning.Available = "No";
        warning.Update();

        // if being processed as a task then close task
        if (Request.QueryString["source"] == "tasks")
        {
            int task_id = Convert.ToInt32(Request.QueryString["taskID"]);
            qPtl_Task task = new qPtl_Task(task_id);

            task.PercentCompleted = 100;
            task.Status = "Completed";
            task.Update();
        }
        else
        {
            // check to see if there there a pending task
            qPtl_Task task = new qPtl_Task(warning.ContentTypeID, warning_id);

            if (task != null)
            {
                if (task.TaskID > 0)
                {
                    task.PercentCompleted = 100;
                    task.Status = "Completed";
                    task.Update();
                }
            }
        }

        if (Request.QueryString["source"] == "tasks")
            Response.Redirect("~/manage/members/task-list.aspx?searchType=open");
        else if (!String.IsNullOrEmpty(Request.QueryString["status"]))
            Response.Redirect("~/manage/members/warnings-list.aspx?status=" + Request.QueryString["status"]);
        else
            Response.Redirect("~/manage/members/warnings-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["source"] == "tasks")
            Response.Redirect("~/manage/members/task-list.aspx?searchType=open");
        else if (!String.IsNullOrEmpty(Request.QueryString["status"]))
            Response.Redirect("~/manage/members/warnings-list.aspx?status=" + Request.QueryString["status"]);
        else
            Response.Redirect("~/manage/members/warnings-list.aspx");
    }
}
