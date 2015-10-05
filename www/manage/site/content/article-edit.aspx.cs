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
using Quartz.CMS;

public partial class edit_article : System.Web.UI.Page
{
    public int article_id;
    public int owner_id;
    public string owner;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ArticleFolder"]);
    public static string health_active = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_HealthActive"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateThemes();
            populateAuthors();

            if (!String.IsNullOrEmpty(Request.QueryString["articleID"]))
            {
                article_id = Convert.ToInt32(Request.QueryString["articleID"]);
                ViewState.Add("vsArticleID", article_id);
                reContent.ImageManager.MaxUploadFileSize = 4194304;

                populateKeywords(article_id, (int)qSoc_ContentType.Types.Article);
                populateTopics(article_id, (int)qSoc_ContentType.Types.Article);

                qLrn_Article article = new qLrn_Article(article_id);
                qSoc_ContentType content = new qSoc_ContentType((int)qSoc_ContentType.Types.Article);

                qPtl_User posted_by = new qPtl_User(article.CreatedBy);
                owner = posted_by.UserName;
                ViewState.Add("vsOwner", owner);
                owner_id = article.CreatedBy;
                ViewState.Add("vsOwnerID", owner_id);
                lblPostedTime.Text = " at " + article.Created;

                lblTitle.Text = "Edit Article (ID: " + article.ArticleID + ")";
                txtTitle.Text = article.Title;
                txtSummary.Text = article.Description;
                reContent.Content = article.Body;
                if (!String.IsNullOrEmpty(Convert.ToString(article.AuthorID)))
                    ddlAuthor.SelectedValue = Convert.ToString(article.AuthorID);
                if (!String.IsNullOrEmpty(Convert.ToString(article.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(article.ThemeID);
                ddlArticleType.SelectedValue = article.ArticleType;
                if (!String.IsNullOrEmpty(article.Language))
                    ddlLanguage.SelectedValue = article.Language;
                else
                    ddlLanguage.SelectedValue = "en";

                rblAvailable.SelectedValue = article.Available;
                plhPostedBy.Visible = true;
                hplPreviewArticle.Visible = true;
                hplPreviewArticle.NavigateUrl = "/social/learning/article-details.aspx?articleID=" + article.ArticleID;
                hplPreviewArticle.Target = "_blank";

                if (!String.IsNullOrEmpty(Request.QueryString["location"]))
                {
                    if (Request.QueryString["location"] == "my-health-care")
                        plhArticleType.Visible = true;
                }

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Article, article_id);
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
                lblTitle.Text = "New Article";
                btnDelete.Visible = false;
                populateKeywords(0, (int)qSoc_ContentType.Types.Article);
                plhPostedBy.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }

        if (!String.IsNullOrEmpty(health_active))
            if (health_active == "true")
                plhArticleType.Visible = true;

        if (String.IsNullOrEmpty(Convert.ToString(article_id)))
            article_id = (Int32)ViewState["vsArticleID"];
        if (String.IsNullOrEmpty(Convert.ToString(owner_id)))
            owner_id = (Int32)ViewState["vsOwnerID"];
        if (String.IsNullOrEmpty(owner))
            owner = (String)ViewState["vsOwner"];

    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["articleID"]))
        {
            article_id = Convert.ToInt32(Request.QueryString["articleID"]);
            qLrn_Article article = new qLrn_Article(article_id);
            article.Title = txtTitle.Text;
            article.Description = txtSummary.Text;
            article.Body = reContent.Content;
            article.LastModified = DateTime.Now;
            article.LastModifiedBy = user_id;
            if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
            {
                article.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
                article.OrganizationAuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlLanguage.SelectedValue))
                article.Language = ddlLanguage.SelectedValue;
            else
                article.Language = "en";
            if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                article.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
            article.Available = rblAvailable.SelectedValue;
            if (plhArticleType.Visible == true)
                article.ArticleType = ddlArticleType.SelectedValue;
            article.Update();
        }
        else
        {
            qLrn_Article article = new qLrn_Article();
            article.ScopeID = 1;
            article.Created = DateTime.Now;
            article.CreatedBy = user_id;
            article.LastModified = DateTime.Now;
            article.LastModifiedBy = user_id;
            article.Available = "Yes";
            article.MarkAsDelete = 0;
            article.Title = txtTitle.Text;
            article.Description = txtSummary.Text;
            article.Body = reContent.Content;
            article.LastModified = DateTime.Now;
            article.LastModifiedBy = user_id;
            if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
            {
                article.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
                article.OrganizationAuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
            }
            if (!String.IsNullOrEmpty(ddlLanguage.SelectedValue))
                article.Language = ddlLanguage.SelectedValue;
            else
                article.Language = "en";
            if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                article.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
            if (plhArticleType.Visible == true)
                article.ArticleType = ddlArticleType.SelectedValue;
            article.Available = rblAvailable.SelectedValue;
            article.Insert();

            article_id = article.ArticleID;
        }

        string user_name = (new qPtl_User(user_id)).UserName;

        // add keywords
        string owner_keywords = string.Empty;
        qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Article, article_id);
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
                keyword.ContentTypeID = (int)qSoc_ContentType.Types.Article;
                keyword.ReferenceID = article_id;
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

        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Article, article_id);

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
            qLrn_Article article = new qLrn_Article(article_id);

            int p_user_id = user_id;
            if (post_as_user_id > 0)
                p_user_id = post_as_user_id;

            qPtl_User user = new qPtl_User(p_user_id);
            var u_space = qSoc_UserSpace_View.GetUserSpaces(article.CreatedBy);

            // evaluate title and description
            string p_title = q_Helper.replaceSpecialCharacters(article.Title);
            string p_description = q_Helper.replaceSpecialCharacters(article.Description);

            if (feed != null)
            {
                if (feed.FeedID > 0)
                {
                    feed.CreatedBy = user.UserID;
                    feed.Available = article.Available;
                    if (chkMoveToTop.Checked)
                        feed.LastModified = DateTime.Now;
                    feed.LastModifiedBy = user.UserID;
                    feed.MarkAsDelete = article.MarkAsDelete;
                    feed.OwnerMarkAsDelete = article.MarkAsDelete;
                    feed.ContentTypeID = (int)qSoc_ContentType.Types.Article;
                    feed.ReferenceID = article_id;
                    feed.OwnerID = user.UserID;
                    feed.OwnerName = user.UserName;
                    feed.OwnerProfilePic = user.ProfilePict;
                    feed.Type = "article";
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
                    feed.Available = article.Available;
                    feed.Created = DateTime.Now;
                    feed.CreatedBy = user.UserID;
                    feed.LastModified = DateTime.Now;
                    feed.LastModifiedBy = user.UserID;
                    feed.MarkAsDelete = article.MarkAsDelete;
                    feed.OwnerMarkAsDelete = article.MarkAsDelete;
                    feed.ContentTypeID = (int)qSoc_ContentType.Types.Article;
                    feed.ReferenceID = article_id;
                    feed.OwnerID = user.UserID;
                    feed.OwnerName = user.UserName;
                    feed.OwnerProfilePic = user.ProfilePict;
                    feed.Type = "article";
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

        // redirect to page to add theme + keywords
        //Response.Redirect("~/qLrn/articles-list.aspx");
        if (!String.IsNullOrEmpty(Request.QueryString["articleID"]))
        {
            lblMessage.Text = "*** Record Successfully Updated ***";
            lblMessageBottom.Text = "*** Record Successfully Updated ***";
            if (Request.QueryString["edit-mode"] == "in-place")
            {
                string return_redirect = Request.QueryString["returnURL"];
                if (!String.IsNullOrEmpty(Request.QueryString["location"]))
                    return_redirect += "&location=" + Request.QueryString["location"];
                Response.Redirect(return_redirect);
            }
            else
                Response.Redirect("articles-list.aspx");
        }
        else
        {
            Response.Redirect(Request.Url.ToString() + "?mode=add-successful&articleID=" + article_id);
        }
    }

    protected void populateTopics(int video_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Article, article_id);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        article_id = Convert.ToInt32(Request.QueryString["articleID"]);
        qLrn_Article article = new qLrn_Article(article_id);
        article.Available = "No";
        article.MarkAsDelete = 1;
        article.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("articles-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("articles-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("articles-list.aspx");
    }
}
