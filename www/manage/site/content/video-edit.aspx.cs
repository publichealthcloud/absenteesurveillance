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
using Quartz.Data;

public partial class video_edit : System.Web.UI.Page
{
    public int video_id;
    public string viddler_id;
    public int owner_id;
    public string owner;
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        video_id = Convert.ToInt32(Request.QueryString["videoID"]);
        
        if (!Page.IsPostBack)
        {
            
            populateThemes();
            populateAuthors();
            populateKeywords(video_id, (int)qSoc_ContentType.Types.Video);
            populateTopics(video_id, (int)qSoc_ContentType.Types.Video);

            qSoc_Video video = new qSoc_Video(video_id);
            txtTitle.Text = video.Title;
            txtDescription.Text = video.Description;
            ddlStatus.SelectedValue = video.ApprovedStatus;
            ddlTheme.SelectedValue = Convert.ToString(video.ThemeID);
            ddlAuthor.SelectedValue = Convert.ToString(video.AuthorID);
            owner_id = video.UserID;
            ViewState.Add("vsOwnerID", owner_id);
            rblAvailable.SelectedValue = video.Available;
            txtPreviewImage.Visible = true;
            txtPreviewImage.Text = video.ThumbnailURL;
            txtPreviewImage.Enabled = false;

            qPtl_User posted_by = new qPtl_User(video.UserID);
            owner = posted_by.UserName;
            ViewState.Add("vsOwner", owner);
            qPtl_User approved_by = new qPtl_User(video.ApprovedBy);

            lblApprovedBy.Text = "Approved by " + approved_by.UserName + " at " + video.Approved;
            lblPostedTime.Text = " at " + video.Created;

            lblVideoType.Text = video.Source;

            lblTitle.Text = "Edit Video (ID: " + video.VideoID + ")";

            // see if in feed
            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Video, video.VideoID);

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

            if (!String.IsNullOrEmpty(video.ThumbnailURL))
            {
                imgThumbnail.Visible = true;
                imgThumbnail.ImageUrl = video.ThumbnailURL;
                imgThumbnail.Width = 100;
            }

            if (!String.IsNullOrEmpty(video.ViddlerID) && video.Source == "internal")
            {
                litVideo.Text = "video successfully uploaded -- embed tags here";
                plhVideo.Visible = true;
                plhExternalVideo.Visible = false;
                plhVideoProcessing.Visible = false;
                viddler_id = video.ViddlerID;
                ViewState.Add("vsViddlerID", viddler_id);
                txtEmbed.Visible = false;
                rfvExternalSource.Enabled = false;
                plhAdditionalExternalInfo.Visible = false;
                btnEnableEmbed.Visible = false;
                rfvEmbedCode.Enabled = false;
                rfvExternalSource.Enabled = false;

                string viddler_embed = "<iframe id=\"Iframe1\" src=\"//www.viddler.com/embed/" + viddler_id + "/?f=1&player=full&secret=90387241\" width=\"347\" height=\"288\" frameborder=\"0\" mozallowfullscreen=\"true\" webkitallowfullscreen=\"true\"></iframe>";

                litViddlerEmbed.Text = viddler_embed;
                txtEmbedTag.Text = viddler_embed;
            }
            else if (video.Source == "external")
            {
                plhVideo.Visible = false;
                plhVideoProcessing.Visible = false;
                plhExternalVideo.Visible = true;
                litExternalEmbed.Text = video.EmbedCode;
                txtEmbed.Text = video.EmbedCode;
                txtEmbed.Enabled = false;
                plhAdditionalExternalInfo.Visible = true;
                txtSourceVideoID.Text = video.SourceVideoID;
                ddlExternalSource.SelectedValue = video.ExternalSourceName;
                btnEnableEmbed.Visible = true;
                if (video.ExternalSourceName == "YouTube")
                {
                    string image_url = "http://img.youtube.com/vi/" + video.SourceVideoID + "/1.jpg";
                    txtPreviewImage.Text = image_url;
                    imgThumbnail.ImageUrl = image_url;
                    txtPreviewImage.Visible = true;
                    imgThumbnail.Visible = true;
                    txtPreviewImage.Enabled = false;
                }
                else
                    imgThumbnail.ImageUrl = video.ThumbnailURL;
            }
            else
            {
                // viddler video before processing is complete -- initial view
                litVideo.Text = "This video is still being processed. Check back again soon...";
                plhVideo.Visible = false;
                plhExternalVideo.Visible = true;
                plhVideoProcessing.Visible = true;
                txtEmbed.Visible = false;
                btnEnableEmbed.Visible = false;
                btnEnableThumbnail.Visible = false;
                txtPreviewImage.Visible = false;
                txtPreviewImage.Enabled = false;
                plhAdditionalExternalInfo.Visible = false;
                rfvExternalSource.Enabled = false;
                txtSourceVideoID.Enabled = false;
                rfvEmbedCode.Enabled = false;
                rfvExternalSource.Enabled = false;
            }

            qSoc_HighlightedVideo high_video = new qSoc_HighlightedVideo();
            high_video = qSoc_HighlightedVideo.GetLatest();

            if (high_video.VideoID == video_id)
            {
                chkHighlightedVideo.Checked = true;
                lblHighlightedMessage.Text = "This is the current highlighted video ID: " + high_video.HighlightedVideoID;
            }
            else
                lblHighlightedMessage.Text = "Check to set as the highlighted video";
        }


        if (String.IsNullOrEmpty(viddler_id))
            viddler_id = (String)ViewState["vsViddlerID"];
        if (String.IsNullOrEmpty(Convert.ToString(owner_id)))
            owner_id = (Int32)ViewState["vsOwnerID"];
        if (String.IsNullOrEmpty(owner))
            owner = (String)ViewState["vsOwner"];

    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);
            video_id = Convert.ToInt32(Request.QueryString["videoID"]);

            string user_name = (new qPtl_User(user_id)).UserName;

            qSoc_Video video = new qSoc_Video(video_id);
            video.Title = txtTitle.Text;
            video.Description = txtDescription.Text;
            if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                video.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
            video.Status = "Approved";
            video.Created = video.Created;
            video.CreatedBy = video.CreatedBy;
            video.Approved = DateTime.Now;
            video.Source = lblVideoType.Text;
            video.LastModified = DateTime.Now;
            video.LastModifiedBy = user_id;
            video.Length = video.Length;
            video.EmbedCode = txtEmbed.Text;
            if (txtPreviewImage.Visible == true)
                video.ThumbnailURL = txtPreviewImage.Text;
            video.SourceVideoID = txtSourceVideoID.Text;
            if (!String.IsNullOrEmpty(ddlExternalSource.SelectedValue))
                video.ExternalSourceName = ddlExternalSource.SelectedValue;
            string author = string.Empty;
            author = ddlAuthor.SelectedValue;
            if (!String.IsNullOrEmpty(author))
                video.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
            video.Available = rblAvailable.SelectedValue;

            // process if highlighted video
            qDbs_SQLcode sql = new qDbs_SQLcode();
            string sqlCode = string.Empty;
            sqlCode = "UPDATE qSoc_Videos SET HighlightedWF = null";

            qSoc_HighlightedVideo.Delete(video_id);

            if (lblHighlightedMessage.Text.Contains("current") && chkHighlightedVideo.Checked == false)
            {
                sql.ExecuteSQL(sqlCode);
                video.HighlightedWF = null;
            }
            else if (chkHighlightedVideo.Checked && lblHighlightedMessage.Text.Contains("current"))
            {
                sql.ExecuteSQL(sqlCode);
                qSoc_HighlightedVideo high_video = new qSoc_HighlightedVideo();
                high_video = qSoc_HighlightedVideo.GetLatest();
                high_video.Available = "No";
                high_video.Created = DateTime.Now;
                high_video.CreatedBy = user_id;
                high_video.LastModified = DateTime.Now;
                high_video.LastModifiedBy = user_id;
                high_video.MarkAsDelete = 1;
                high_video.Status = "Hidden";
                high_video.Update();
            }
            else if (chkHighlightedVideo.Checked && lblHighlightedMessage.Text.Contains("Check"))
            {
                sql.ExecuteSQL(sqlCode);
                qSoc_HighlightedVideo new_highlighted = new qSoc_HighlightedVideo();
                new_highlighted.VideoID = video_id;
                new_highlighted.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                new_highlighted.Available = "Yes";
                new_highlighted.Created = DateTime.Now;
                new_highlighted.CreatedBy = user_id;
                new_highlighted.LastModified = DateTime.Now;
                new_highlighted.LastModifiedBy = user_id;
                new_highlighted.MarkAsDelete = 0;
                new_highlighted.Status = "Visible";
                new_highlighted.Insert();
                video.HighlightedWF = "Yes";
            }
            video.Update();

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Video, video_id);
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
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Video;
                    keyword.ReferenceID = video_id;
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

            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Video, video.VideoID);

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
                int p_user_id = user_id;
                if (post_as_user_id > 0)
                    p_user_id = post_as_user_id;

                qPtl_User user = new qPtl_User(p_user_id);
                var u_space = qSoc_UserSpace_View.GetUserSpaces(video.CreatedBy);

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        feed.Available = video.Available;
                        feed.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                        feed.CreatedBy = user.UserID;
                        if (chkMoveToTop.Checked)
                            feed.LastModified = DateTime.Now;
                        feed.LastModified = video.LastModified;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = video.MarkAsDelete;
                        feed.OwnerMarkAsDelete = video.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Video;
                        feed.ReferenceID = video.VideoID;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "video";
                        feed.Title = video.Title;
                        feed.Description = video.Description;
                        feed.OwnerRole = user.HighestRole;
                        feed.OwnerRoleID = user.HighestRank;
                        feed.OwnerKeywords = owner_keywords;
                        feed.UploadedFrom = "manager";
                        feed.ReservedKeywords = reserved_keywords;
                        feed.Thumbnail = video.ThumbnailURL;
                        if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                        {
                            feed.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                            feed.ThemeName = Convert.ToString(ddlTheme.SelectedItem);
                        }
                        if (video.Source == "internal")
                        {
                            feed.VideoKey = video.ViddlerID;
                        }
                        else
                        {
                            string embed_code = Convert.ToString(video.EmbedCode);
                            feed.Embed = embed_code;
                            feed.VideoKey = null;
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
                        feed.Available = video.Available;
                        feed.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                        feed.Created = DateTime.Now;    //video.Created;
                        feed.CreatedBy = user.UserID;
                        feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = video.MarkAsDelete;
                        feed.OwnerMarkAsDelete = video.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Video;
                        feed.ReferenceID = video.VideoID;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "video";
                        feed.Title = video.Title;
                        feed.Description = video.Description;
                        feed.OwnerRole = user.HighestRole;
                        feed.OwnerRoleID = user.HighestRank;
                        feed.OwnerKeywords = owner_keywords;
                        feed.UploadedFrom = "manager";
                        feed.ReservedKeywords = reserved_keywords;
                        feed.Thumbnail = video.ThumbnailURL;
                        if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                        {
                            feed.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                            feed.ThemeName = Convert.ToString(ddlTheme.SelectedItem);
                        }
                        if (video.Source == "internal")
                        {
                            feed.VideoKey = video.ViddlerID;
                        }
                        else
                        {
                            string embed_code = Convert.ToString(video.EmbedCode);
                            feed.Embed = embed_code;
                        }

                        feed.VisibleAll = true;
                        feed.VisibleFriends = true;
                        feed.VisibleFollowers = true;
                        feed.VisibleSpace = true;
                        feed.VisiblePrivate = true;
                        feed.VisibleFeed = true;
                        feed.VisibleOwnerFeed = true;
                        feed.VisibleOwnerProfile = true;
                        if (!String.IsNullOrEmpty(owner_keywords))
                        {
                            feed.OwnerKeywords = owner_keywords;
                        }
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

            // see if there is a pending task for this video
            if (video.Status == "Approved")
            {
                qPtl_Task task = new qPtl_Task((int)qSoc_ContentType.Types.Video, video_id);

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

            // redirect to page to add theme + keywords
            //  Response.Redirect("~/qSoc/videos-list.aspx");
            lblMessage.Text = "*** Record Successfully Updated ***";
            lblMessageBottom.Text = "*** Record Successfully Updated ***";
        }
    }

    protected void populateKeywords(int video_id, int content_type_id)
    {
        var keywords = qPtl_Keyword_MinimalView.GetKeywords();

        qPtl_KeywordReference[] references = qPtl_KeywordReference.GetKeywordReferencesArrayByContent(content_type_id, video_id);
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

    protected void populateTopics(int video_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Video, video_id);
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
        qSoc_Video video = new qSoc_Video(video_id);
        video.Available = "No";
        video.MarkAsDelete = 1;
        video.Update();

        Response.Redirect("videos-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("videos-list.aspx");
    }
    protected void btnEnableEmbed_Click(object sender, EventArgs e)
    {
        txtEmbed.Enabled = true;
        btnEnableEmbed.Visible = false;
    }
    protected void btnEnableThumbnail_Click(object sender, EventArgs e)
    {
        txtPreviewImage.Enabled = true;
        btnEnableThumbnail.Visible = false;
    }
}
