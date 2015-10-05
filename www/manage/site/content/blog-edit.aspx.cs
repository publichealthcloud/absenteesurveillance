using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Telerik.Web.UI;

using Quartz;
using Quartz.Core;
using Quartz.Data;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.CMS;

public partial class edit_blog : System.Web.UI.Page
{
    public int blog_id;
    public int owner_id;
    public string owner;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_BlogFolder"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);
    private const int ItemsPerRequest = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rblType.Enabled = false;

            if (Request.QueryString["type"] == "story")
            {
                //litBackTop.Text = "tst";    //<a href="\blogs-list.aspx?type=story\" class=\"btn\"><i class=\"icon-circle-arrow-left\"></i>&nbsp;&nbsp;Back to Stories</a>";
                btnSave_top.Text = "SAVE STORY";
                btnSave.Text = "SAVE STORY";
            }
            else if (Request.QueryString["type"] == "blog")
            {
                //litBackTop.Text = "";
                btnSave_top.Text = "SAVE BLOG";
                btnSave.Text = "SAVE BLOG";
            }
            else
            {
                btnSave_top.Text = "SAVE BLOG";
                btnSave.Text = "SAVE BLOG";
            }
            
            populateThemes();
            populateAuthors();

            // load styles for this project
            string css_text_file = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CmsTextCSS"]))
                css_text_file = Convert.ToString(ConfigurationManager.AppSettings["CmsTextCSS"]);

            reContent.CssFiles.Add(css_text_file);
            
            if (!String.IsNullOrEmpty(Request.QueryString["BlogID"]))
            {
                
                blog_id = Convert.ToInt32(Request.QueryString["BlogID"]);
                ViewState.Add("vsBlogID", blog_id);

                populateKeywords(blog_id, (int)qSoc_ContentType.Types.Story);
                populateTopics(blog_id, (int)qSoc_ContentType.Types.Story);

                qSoc_Blog2 blog = new qSoc_Blog2(blog_id);
                qSoc_ContentType content = new qSoc_ContentType((int)qSoc_ContentType.Types.Story);

                qPtl_User posted_by = new qPtl_User(blog.UserID);
                owner = posted_by.UserName;
                ViewState.Add("vsOwner", owner);
                owner_id = blog.CreatedBy;
                ViewState.Add("vsOwnerID", owner_id);
                lblPostedTime.Text = " at " + blog.Created;
                ddlStatus.SelectedValue = blog.ApprovedStatus;

                qPtl_User approved_by = new qPtl_User(blog.ApprovedBy);
                lblApprovedBy.Text = "Approved by " + approved_by.UserName + " at " + blog.Approved;
                lblPostedTime.Text = " at " + blog.Created;

                if (Request.QueryString["type"] == "story")
                    lblTitle.Text = "Edit Story (ID: " + blog.BlogID + ")";
                else
                    lblTitle.Text = "Edit Blog (ID: " + blog.BlogID + ")";

                txtTitle.Text = blog.Title;
                txtSummary.Text = blog.Summary;
                reContent.Content = blog.Text;
                if (!String.IsNullOrEmpty(Convert.ToString(blog.AuthorID)))
                    ddlAuthor.SelectedValue = Convert.ToString(blog.AuthorID);
                if (!String.IsNullOrEmpty(Convert.ToString(blog.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(blog.ThemeID);

                rblAvailable.SelectedValue = blog.Available;
                rblType.SelectedValue = blog.Type;

                plhPostedBy.Visible = true;
                hplPreviewArticle.Visible = false;
                hplPreviewArticle.Target = "_blank";

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Story, blog_id);
                plhExistingFeedItem.Visible = false;

                if (feed != null)
                {
                    if (feed.FeedID > 0 && feed.MarkAsDelete == 0 && feed.VisibleFeed == true)
                    {
                        chkDisplayInFeed.Checked = true;
                        plhExistingFeedItem.Visible = true;
                    }
                    if (feed.FeedID > 0 && feed.MarkAsDelete == 0 && feed.VisibleExplore == true)
                    {
                        chkDisplayInExplore.Checked = true;
                    }
                    string reserved_keywords = string.Empty;
                    if (!string.IsNullOrEmpty(feed.ReservedKeywords))
                    {
                        reserved_keywords = feed.ReservedKeywords;
                    }
                }
            }
            else
            {
                if (Request.QueryString["type"] == "story")
                {
                    lblTitle.Text = "New Story";
                    rblType.SelectedValue = "story";
                }
                else
                {
                    lblTitle.Text = "New Blog";
                    rblType.SelectedValue = "blog";
                }
                btnDelete.Visible = false;
                populateKeywords(0, (int)qSoc_ContentType.Types.Story);
                plhPostedBy.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                ddlStatus.SelectedValue = "Approved";
                plhTools.Visible = false;
            }
        }

        var highlighted_item = qSoc_Blog2.GetHighlightedBlog();

        if (highlighted_item != null)
        {
            if (highlighted_item.BlogID == blog_id)
            {
                chkHighlightedItem.Checked = true;
                lblHighlightedMessage.Text = "This is the current highlighted blog ID: " + highlighted_item.BlogID;
            }
            else
                lblHighlightedMessage.Text = "Check to set as the highlighted blog";
        }
        else
            lblHighlightedMessage.Text = "Check to set as the highlighted blog";

        if (String.IsNullOrEmpty(Convert.ToString(blog_id)))
            blog_id = (Int32)ViewState["vsBlogID"];
        if (String.IsNullOrEmpty(Convert.ToString(owner_id)))
            owner_id = (Int32)ViewState["vsOwnerID"];
        if (String.IsNullOrEmpty(owner))
            owner = (String)ViewState["vsOwner"];

    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["BlogID"]))
        {
            blog_id = Convert.ToInt32(Request.QueryString["BlogID"]);
            qSoc_Blog2 blog = new qSoc_Blog2(blog_id);
            blog.Title = txtTitle.Text;
            blog.Summary = txtSummary.Text;
            blog.Text = reContent.Content;
            blog.LastModified = DateTime.Now;
            blog.LastModifiedBy = user_id;
            if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
                blog.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
            if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                blog.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
            blog.Available = rblAvailable.SelectedValue;
            blog.Type = rblType.SelectedValue;
            blog.ApprovedStatus = ddlStatus.SelectedValue;
            if (ddlStatus.SelectedValue == "Approved")
            {
                blog.ApprovedBy = user_id;
                blog.Approved = DateTime.Now;
            }
            blog.Update();
        }
        else
        {
            qSoc_Blog2 blog = new qSoc_Blog2();
            blog.ScopeID = 1;
            blog.Created = DateTime.Now;
            blog.CreatedBy = user_id;
            blog.LastModified = DateTime.Now;
            blog.LastModifiedBy = user_id;
            blog.Available = "Yes";
            blog.MarkAsDelete = 0;
            blog.UserID = user_id;
            blog.Title = txtTitle.Text;
            blog.Summary = txtSummary.Text;
            blog.Text = reContent.Content;
            blog.LastModified = DateTime.Now;
            blog.LastModifiedBy = user_id;
            if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
                blog.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
            if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                blog.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
            blog.Available = rblAvailable.SelectedValue;
            blog.Type = rblType.SelectedValue;
            blog.ApprovedStatus = ddlStatus.SelectedValue;
            if (ddlStatus.SelectedValue == "Approved")
            {
                blog.ApprovedBy = user_id;
                blog.Approved = DateTime.Now;
            }
            blog.UploadedFrom = "manager";
            blog.Insert();

            blog_id = blog.BlogID;
        }

        string user_name = (new qPtl_User(user_id)).UserName;

        // add keywords
        string owner_keywords = string.Empty;
        qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Story, blog_id);
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
                keyword.ContentTypeID = (int)qSoc_ContentType.Types.Story;
                keyword.ReferenceID = blog_id;
                keyword.Created = DateTime.Now;
                keyword.LastModified = DateTime.Now;
                keyword.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                keyword.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                keyword.MarkAsDelete = 0;
                keyword.Insert();
            }
        }

        string reserved_keywords = string.Empty;
        foreach (ListItem item in chkTopics.Items)
        {
            if (item.Selected)
            {
                if (!String.IsNullOrEmpty(reserved_keywords))
                    reserved_keywords += "," + item.Text;
                else
                    reserved_keywords += item.Text;
            }
        }

        // process if highlighted blog
        qDbs_SQLcode sql = new qDbs_SQLcode();
        string sqlCode = string.Empty;
        sqlCode = "UPDATE qSoc_Blogs SET Highlighted = null";

        if (lblHighlightedMessage.Text.Contains("current") && chkHighlightedItem.Checked == false)
        {
            sql.ExecuteSQL(sqlCode);        // clear all highlighted options
        }
        else if (chkHighlightedItem.Checked && lblHighlightedMessage.Text.Contains("current"))
        {
            sql.ExecuteSQL(sqlCode);        // clear all highlighted options
            qSoc_Blog2 curr_item = new qSoc_Blog2(blog_id);
            curr_item.Highlighted = true;
            curr_item.Update();
        }
        else if (chkHighlightedItem.Checked && lblHighlightedMessage.Text.Contains("Check"))
        {
            sql.ExecuteSQL(sqlCode);        // clear all highlighted options
            qSoc_Blog2 blog = new qSoc_Blog2(blog_id);
            blog.Highlighted = true;
            blog.Update();
        }

        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Story, blog_id);

        if (!chkDisplayInFeed.Checked && !chkDisplayInExplore.Checked)
        {
            if (feed != null)
            {
                if (feed.FeedID > 0)
                {
                    feed.MarkAsDelete = 1;
                    feed.Available = "No";
                    feed.ReservedKeywords = reserved_keywords;
                    if (!chkDisplayInFeed.Checked)
                        feed.VisibleFeed = false;
                    if (!chkDisplayInExplore.Checked)
                        feed.VisibleExplore = false;
                    feed.Update();
                }
            }
        }
        else if (chkDisplayInFeed.Checked || chkDisplayInExplore.Checked || !string.IsNullOrEmpty(reserved_keywords))
        {
            qSoc_Blog2 blog = new qSoc_Blog2(blog_id);

            int p_user_id = user_id;
            if (post_as_user_id > 0)
                p_user_id = post_as_user_id;

            qPtl_User user = new qPtl_User(p_user_id);
            var u_space = qSoc_UserSpace_View.GetUserSpaces(blog.CreatedBy);

            // evaluate title and description
            string p_title = blog.Title.Replace("'", "\"");
            p_title = q_Helper.replaceSpecialCharacters(p_title);
            string p_description = q_Helper.replaceSpecialCharacters(blog.Summary);

            if (feed != null)
            {
                if (feed.FeedID > 0)
                {
                    feed.CreatedBy = user.UserID;
                    feed.Available = blog.Available;
                    if (chkMoveToTop.Checked)
                        feed.LastModified = DateTime.Now;
                    feed.LastModifiedBy = user.UserID;
                    feed.MarkAsDelete = blog.MarkAsDelete;
                    feed.OwnerMarkAsDelete = blog.MarkAsDelete;
                    feed.ContentTypeID = (int)qSoc_ContentType.Types.Story;
                    feed.ReferenceID = blog_id;
                    feed.OwnerID = user.UserID;
                    feed.OwnerName = user.UserName;
                    feed.OwnerProfilePic = user.ProfilePict;
                    feed.Type = "blog";
                    feed.Title = p_title;
                    feed.Description = p_description;
                    feed.Body = p_description;
                    feed.OwnerRole = user.HighestRole;
                    feed.OwnerRoleID = user.HighestRank;
                    feed.OwnerKeywords = owner_keywords;
                    feed.ReservedKeywords = reserved_keywords;
                    feed.UploadedFrom = "manager";
                    if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    {
                        feed.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                        feed.ThemeName = Convert.ToString(ddlTheme.SelectedItem);
                    }
                    if (chkDisplayInFeed.Checked)
                        feed.VisibleFeed = true;
                    else
                        feed.VisibleFeed = false;
                    if (chkDisplayInExplore.Checked)
                        feed.VisibleExplore = true;
                    else
                        feed.VisibleExplore = false;
                    feed.Update();
                }
                else
                {
                    // create new feed item
                    if (u_space != null)
                    {
                        foreach (var s in u_space)
                        {
                            feed.SpaceID = s.SpaceID;
                            feed.SpaceName = s.SpaceShortName;
                        }
                    }
                    feed.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    feed.Available = blog.Available;
                    feed.Created = DateTime.Now;
                    feed.CreatedBy = user.UserID;
                    feed.LastModified = DateTime.Now;
                    feed.LastModifiedBy = user.UserID;
                    feed.MarkAsDelete = blog.MarkAsDelete;
                    feed.OwnerMarkAsDelete = blog.MarkAsDelete;
                    feed.ContentTypeID = (int)qSoc_ContentType.Types.Story;
                    feed.ReferenceID = blog_id;
                    feed.OwnerID = user.UserID;
                    feed.OwnerName = user.UserName;
                    feed.OwnerProfilePic = user.ProfilePict;
                    feed.Type = "blog";
                    feed.Title = p_title;
                    feed.Description = p_description;
                    feed.Body = p_description;
                    feed.OwnerRole = user.HighestRole;
                    feed.OwnerRoleID = user.HighestRank;

                    feed.VisibleAll = true;
                    feed.VisibleFriends = true;
                    feed.VisibleFollowers = true;
                    feed.VisibleSpace = true;
                    feed.VisiblePrivate = true;
                    feed.VisibleFeed = true;
                    feed.VisibleOwnerFeed = true;
                    feed.VisibleOwnerProfile = true;
                    if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    {
                        feed.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                        feed.ThemeName = Convert.ToString(ddlTheme.SelectedItem);
                    }
                    feed.ReservedKeywords = reserved_keywords;
                    if (!String.IsNullOrEmpty(owner_keywords))
                    {
                        feed.OwnerKeywords = owner_keywords;
                    }
                    feed.UploadedFrom = "manager";
                    if (chkDisplayInFeed.Checked)
                        feed.VisibleFeed = true;
                    else
                        feed.VisibleFeed = false;
                    if (chkDisplayInExplore.Checked)
                        feed.VisibleExplore = true;
                    else
                        feed.VisibleExplore = false;
                    feed.Insert();
                }
            }
        }

        //Response.Redirect("~/qSoc/blogs-list.aspx");
        if (!String.IsNullOrEmpty(Request.QueryString["BlogID"]))
        {
            lblMessage.Text = "*** Record Successfully Updated ***";
            lblMessageBottom.Text = "*** Record Successfully Updated ***"; 
        }
        else
        {
            if (!String.IsNullOrEmpty(Request.QueryString["type"]))      
                Response.Redirect(Request.Url.ToString() + "&mode=add-successful&BlogID=" + blog_id);
            else
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&BlogID=" + blog_id);
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

    protected void populateThemes()
    {
        ddlTheme.DataSource = qSoc_Theme.GetThemes();
        ddlTheme.DataTextField = "Name";
        ddlTheme.DataValueField = "ThemeID";
        ddlTheme.DataBind();
        ddlTheme.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateAuthors()
    {
        ddlAuthor.DataSource = qLrn_Author.GetAuthors();
        ddlAuthor.DataTextField = "AuthorName";
        ddlAuthor.DataValueField = "AuthorID";
        ddlAuthor.DataBind();
        ddlAuthor.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateTopics(int video_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Story, blog_id);
        string reserved_keywords = string.Empty;
        if (!string.IsNullOrEmpty(feed.ReservedKeywords))
        {
            reserved_keywords = feed.ReservedKeywords;
        }

        if (topics != null)
        {
            foreach (qSoc_Topic topic in topics)
            {
                ListItem topic_item = new ListItem(topic.Name, topic.TopicID.ToString());

                if (!String.IsNullOrEmpty(reserved_keywords))
                {
                    if (reserved_keywords.Contains(topic.Name))
                    {
                        topic_item.Selected = true;
                    }
                }
                chkTopics.Items.Add(topic_item);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        blog_id = Convert.ToInt32(Request.QueryString["BlogID"]);
        qSoc_Blog2 blog = new qSoc_Blog2(blog_id);
        blog.Available = "No";
        blog.MarkAsDelete = 1;
        blog.Update();

        if (!String.IsNullOrEmpty(Request.QueryString["type"]))
            Response.Redirect("blogs-list.aspx?type=" + Request.QueryString["type"]);
        else
            Response.Redirect("blogs-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["type"]))
            Response.Redirect("blogs-list.aspx?type=" + Request.QueryString["type"]);
        else
            Response.Redirect("blogs-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["type"]))
            Response.Redirect("blogs-list.aspx?type=" + Request.QueryString["type"]);
        else
            Response.Redirect("blogs-list.aspx");
    }

    protected void btnEnableType_Click(object sender, EventArgs e)
    {
        rblType.Enabled = true;
        btnEnableType.Visible = false;
    }
}
