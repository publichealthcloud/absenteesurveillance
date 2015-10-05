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

public partial class edit_link : System.Web.UI.Page
{
    public int link_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["site_imageLocation"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            plhURL.Visible = false;
            
            populateThemes();
            populateAuthors();
            
            if (!String.IsNullOrEmpty(Request.QueryString["linkID"]))
            {
                link_id = Convert.ToInt32(Request.QueryString["linkID"]);

                populateKeywords(link_id, (int)qSoc_ContentType.Types.Link);
                populateTopics(link_id, (int)qSoc_ContentType.Types.Link);

                qPtl_Link link = new qPtl_Link(link_id);

                lblTitle.Text = "Edit Link (ID: " + link.LinkID + ")";
                txtName.Text = link.Title;
                txtSummary.Text = link.Description;
                txtURL.Text = link.URL;
                rblAvailable.SelectedValue = link.Available;
                if (!String.IsNullOrEmpty(Convert.ToString(link.AuthorID)))
                    ddlAuthor.SelectedValue = Convert.ToString(link.AuthorID);
                if (!String.IsNullOrEmpty(Convert.ToString(link.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(link.ThemeID);
                ddlLanguage.SelectedValue = link.Language;
                ddlLinkType.SelectedValue = link.LinkType;

                ddlType.SelectedValue = link.Type;
                if (link.Type == "Internal Document")
                {
                    btnEnableDocumentTools.Visible = true;
                    plhURL.Visible = true;
                    txtURL.Enabled = false;
                    plhDocumentTools.Visible = false;
                }
                else
                {
                    btnEnableDocumentTools.Visible = false;
                    plhURL.Visible = true;
                    plhDocumentTools.Visible = false;
                }

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Link, link_id);
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
                lblTitle.Text = "New link";
                plhTools.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                populateKeywords(0, (int)qSoc_ContentType.Types.Article);
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
                lblMessageBottom.Text = "*** Record Successfully Added ***";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["linkID"]))
            {
                link_id = Convert.ToInt32(Request.QueryString["linkID"]);
                qPtl_Link link = new qPtl_Link(link_id);

                link.Title = txtName.Text;
                link.Description = txtSummary.Text;
                link.Available = rblAvailable.SelectedValue;
                link.LastModified = DateTime.Now;
                link.LastModifiedBy = user_id;
                link.URL = txtURL.Text;
                if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
                    link.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    link.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                link.Type = ddlType.SelectedValue;
                if (Convert.ToString(ddlType.SelectedValue).Contains("Internal"))
                    link.Source = "internal";
                else
                    link.Source = "external";
                link.UploadedFrom = "manager";
                link.Language = ddlLanguage.SelectedValue;
                link.LinkType = ddlLinkType.SelectedValue;
                link.Update();
            }
            else
            {
                qPtl_Link link = new qPtl_Link();
                link.ScopeID = 1;
                link.Created = DateTime.Now;
                link.CreatedBy = user_id;
                link.LastModified = DateTime.Now;
                link.LastModifiedBy = user_id;
                link.Available = "Yes";
                link.MarkAsDelete = 0;
                link.Title = txtName.Text;
                link.Description = txtSummary.Text;
                link.URL = txtURL.Text;
                link.Available = rblAvailable.SelectedValue;
                link.Language = ddlLanguage.SelectedValue;
                link.LinkType = ddlLinkType.SelectedValue;
                if (!String.IsNullOrEmpty(ddlAuthor.SelectedValue))
                    link.AuthorID = Convert.ToInt32(ddlAuthor.SelectedValue);
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    link.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                link.Type = ddlType.SelectedValue;

                if (Convert.ToString(ddlType.SelectedValue).Contains("Internal"))
                    link.Source = "internal";
                else
                    link.Source = "external";
                link.UploadedFrom = "manager";
                link.Insert();

                link_id = link.LinkID;
            }

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Link, link_id);
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
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Link;
                    keyword.ReferenceID = link_id;
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

            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Link, link_id);


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
                qPtl_Link link = new qPtl_Link(link_id);

                int p_user_id = user_id;
                if (post_as_user_id > 0)
                    p_user_id = post_as_user_id;

                qPtl_User user = new qPtl_User(p_user_id);
                var u_space = qSoc_UserSpace_View.GetUserSpaces(link.CreatedBy);

                // evaluate title and description
                string p_title = q_Helper.replaceSpecialCharacters(link.Title);
                string p_description = q_Helper.replaceSpecialCharacters(link.Description);

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        feed.CreatedBy = user.UserID;
                        feed.Available = link.Available;
                        if (chkMoveToTop.Checked)
                            feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = link.MarkAsDelete;
                        feed.OwnerMarkAsDelete = link.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Link;
                        feed.ReferenceID = link_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "link";
                        feed.Title = p_title;
                        feed.URL = link.URL;
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
                        feed.Available = link.Available;
                        feed.Created = DateTime.Now;
                        feed.CreatedBy = user.UserID;
                        feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = link.MarkAsDelete;
                        feed.OwnerMarkAsDelete = link.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Link;
                        feed.ReferenceID = link_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "link";
                        feed.Title = p_title;
                        feed.Description = p_description;
                        feed.Body = p_description;
                        feed.OwnerRole = user.HighestRole;
                        feed.OwnerRoleID = user.HighestRank;
                        feed.URL = link.URL;

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

            // redirect to page to add link + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["linkID"]))
            {
                //lblMessage.Text = "*** Record Successfully Updated ***";
                //lblMessageBottom.Text = "*** Record Successfully Updated ***";
                Response.Redirect("links-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&linkID=" + link_id);
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
        link_id = Convert.ToInt32(Request.QueryString["linkID"]);

        qPtl_Link link = new qPtl_Link(link_id);
        link.Available = "No";
        link.MarkAsDelete = 1;
        link.Update();

        Response.Redirect("links-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("links-list.aspx");
    }

    protected void loadUploadOptions(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "Internal Document")
        {
            ManageDocuments();
        }
        else
        {
            lblURLInstructions.Visible = true;
            plhDocumentTools.Visible = false;
            plhURL.Visible = true;
            txtURL.Visible = true;
            txtURL.Enabled = true;
        }
    }

    protected void setDocumentURL(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlDocument.SelectedValue))
        {
            txtURL.Text = ddlDocument.SelectedValue;
            txtURL.Visible = true;
            txtURL.Enabled = false;
            plhDocumentTools.Visible = false;
            btnEnableDocumentTools.Visible = true;
        }
    }

    protected void btnUploadDocument_Click(object sender, EventArgs e)
    {
        fxpLinkDocuments.Visible = true;
        btnUploadDocument.Visible = false;
        btnHideDocuments.Visible = true;
    }

    protected void btnRefreshDocument_Click(object sender, EventArgs e)
    {
        RefreshDocuments();
    }

    protected void RefreshDocuments()
    {
        var link_folder = new DirectoryInfo(Server.MapPath("~/resources/links"));

        try
        {
            ddlDocument.Items.Clear();
            ddlDocument.DataSource = link_folder.GetFiles();
            ddlDocument.DataTextField = "Name";
            ddlDocument.DataBind();
            ddlDocument.Items.Insert(0, new ListItem("Select a document"));
        }
        catch
        {
        }
    }

    protected void btnHideDocument_Click(object sender, EventArgs e)
    {
        fxpLinkDocuments.Visible = false;
        btnUploadDocument.Visible = true;
        btnHideDocuments.Visible = false;
    }

    protected void btnEnableDocumentTools_Click(object sender, EventArgs e)
    {
        ManageDocuments();
    }

    protected void ManageDocuments()
    {
        lblURLInstructions.Visible = false;
        plhDocumentTools.Visible = true;
        txtURL.Visible = false;
        txtURL.Enabled = false;
        plhURL.Visible = true;
        btnHideDocuments.Visible = false;
        btnEnableDocumentTools.Visible = false;
        RefreshDocuments();
    }

    protected void populateTopics(int link_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Link, link_id);
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
}
