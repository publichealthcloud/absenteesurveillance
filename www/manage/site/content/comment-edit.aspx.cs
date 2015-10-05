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

public partial class edit_comment : System.Web.UI.Page
{
    public int comment_id;
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["commentID"]))
            {
                comment_id = Convert.ToInt32(Request.QueryString["commentID"]);
                ViewState.Add("vsCommentID", comment_id);

                populateKeywords(comment_id, (int)qSoc_ContentType.Types.Tip);

                qSoc_Comment2 comment = new qSoc_Comment2(comment_id);
                qSoc_ContentType content = new qSoc_ContentType((int)qSoc_ContentType.Types.Shout);
                qPtl_User posted_by = new qPtl_User(comment.ActorID);

                lblTitle.Text = "Edit Thought (ID: " + comment.CommentID + ")";
                lblPostedBy.Text = posted_by.UserName;
                txtComment.Text = comment.Comment;

                rblAvailable.SelectedValue = comment.Available;

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Shout, comment_id);

                if (feed != null)
                {
                    if (feed.FeedID > 0 && feed.Available == "Yes")
                    {
                        btnMakeAvailableCampaigns.Visible = false;
                        lblExistsFeed.Text = "<i class=\"icon-check\"></i> This training is available for use in campaigns";
                    }
                }
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(comment_id)))
            comment_id = (Int32)ViewState["vsCommentID"];
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            comment_id = Convert.ToInt32(Request.QueryString["commentID"]);
            qSoc_Comment2 comment = new qSoc_Comment2(comment_id);
            comment.LastModified = DateTime.Now;
            comment.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
            comment.Comment = txtComment.Text;
            comment.Available = rblAvailable.SelectedValue;
            comment.Update();

            string user_name = (new qPtl_User(user_id)).UserName;

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Shout, comment_id);
            foreach (ListItem item in cblKeywords.Items)
            {
                if (item.Selected)
                {
                    if (!String.IsNullOrEmpty(owner_keywords))
                        owner_keywords += "," + item.Text;
                    else
                        owner_keywords += item.Text;
                    qPtl_KeywordReference keyword = new qPtl_KeywordReference();
                    keyword.Available = "Yes";
                    keyword.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    keyword.KeywordID = Convert.ToInt32(item.Value);
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Shout;
                    keyword.ReferenceID = comment_id;
                    keyword.Created = DateTime.Now;
                    keyword.LastModified = DateTime.Now;
                    keyword.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                    keyword.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    keyword.MarkAsDelete = 0;
                    keyword.Insert();
                }
            }

            // update feed information
            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Shout, comment_id);

            if (feed != null)
            {
                if (feed.FeedID > 0)
                {
                    feed.Body = txtComment.Text;
                    int length_comment = comment.Comment.Length;
                    int max_length = 250;
                    if (length_comment < max_length)
                        max_length = length_comment;
                    feed.Title = feed.Body.Substring(0, max_length);
                    feed.Available = rblAvailable.SelectedValue;
                    feed.Update();
                }
            }

            lblMessage.Text = "*** Record Successfully Updated ***";
            lblMessageBottom.Text = "*** Record Successfully Updated ***";
            if (Request.QueryString["edit-mode"] == "in-place")
                Response.Redirect(Request.QueryString["returnURL"]);
            else
                Response.Redirect("comments-list.aspx");
        }
    }

    protected void populateKeywords(int reference_id, int content_type_id)
    {
        var keywords = qPtl_Keyword_MinimalView.GetKeywords();

        if (reference_id > 0)
        {
            qPtl_KeywordReference[] references = qPtl_KeywordReference.GetKeywordReferencesArrayByContent(content_type_id, reference_id);
            if (keywords != null)
            {
                foreach (qPtl_Keyword_MinimalView keyword in keywords)
                {
                    bool selected = false;
                    if (references != null && references.Length > 0)
                    {
                        foreach (qPtl_KeywordReference k_ref in references)
                        {
                            if (k_ref.KeywordID == keyword.KeywordID)
                                selected = true;
                        }
                    }
                    ListItem kr_item = new ListItem(keyword.Keyword, keyword.KeywordID.ToString());
                    kr_item.Selected = selected;
                    cblKeywords.Items.Add(kr_item);
                }
            }
        }
        else
        {
            if (keywords != null)
            {
                foreach (qPtl_Keyword_MinimalView keyword in keywords)
                {
                    ListItem kr_item = new ListItem(keyword.Keyword, keyword.KeywordID.ToString());
                    cblKeywords.Items.Add(kr_item);
                }
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        comment_id = Convert.ToInt32(Request.QueryString["commentID"]);
        qSoc_Comment2 comment = new qSoc_Comment2(comment_id);
        comment.Available = "No";
        comment.MarkAsDelete = 1;
        comment.Update();

        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Shout, comment_id);

        if (feed != null)
        {
            if (feed.FeedID > 0)
            {
                feed.Available = "No";
                feed.MarkAsDelete = 1;
                feed.LastModified = DateTime.Now;
                feed.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                feed.Update();
            }
        }

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("comments-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("comments-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("comments-list.aspx");
    }

    protected void btnMakeAvailableCampaigns_Click(object sender, EventArgs e)
    {
        int comment_id = Convert.ToInt32(Request.QueryString["commentID"]);

        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Shout, comment_id);

        if (feed != null)
        {
            if (feed.FeedID > 0)
            {
                feed.Available = "Yes";
                feed.Update();
            }
        }

        qSoc_Comment2 comment = new qSoc_Comment2(comment_id);
        comment.Available = "Yes";
        comment.Update();

        Response.Redirect("~/manage/site/content/comment-edit.aspx?commentID=" + comment_id);
    }
}
