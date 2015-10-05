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

public partial class edit_tip : System.Web.UI.Page
{
    public int tip_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ArticleFolder"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateThemes();

            if (!String.IsNullOrEmpty(Request.QueryString["tipID"]))
            {
                tip_id = Convert.ToInt32(Request.QueryString["tipID"]);
                ViewState.Add("vsTipID", tip_id);

                populateKeywords(tip_id, (int)qSoc_ContentType.Types.Tip);
                populateTopics(tip_id, (int)qSoc_ContentType.Types.Tip);

                qSoc_Tip tip = new qSoc_Tip(tip_id);
                qSoc_ContentType content = new qSoc_ContentType((int)qSoc_ContentType.Types.Tip);

                lblTitle.Text = "Edit Tip (ID: " + tip.TipID + ")";
                txtTitle.Text = tip.Name;
                txtSummary.Text = tip.Summary;
                txtTip.Text = tip.Text;
                txtURL.Text = tip.LearnMoreURL;
                txtAuthor.Text = tip.Author;
                ddlTipType.SelectedValue = tip.Type;
                if (!String.IsNullOrEmpty(Convert.ToString(tip.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(tip.ThemeID);

                rblAvailable.SelectedValue = tip.Available;

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Tip, tip_id);
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
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(tip_id)))
            tip_id = (Int32)ViewState["vsTipID"];
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["tipID"]))
            {
                tip_id = Convert.ToInt32(Request.QueryString["tipID"]);
                qSoc_Tip tip = new qSoc_Tip(tip_id);
                tip.Name = txtTitle.Text;
                tip.Summary = txtSummary.Text;
                tip.Text = txtTip.Text;
                tip.LearnMoreURL = txtURL.Text;
                tip.LastModified = DateTime.Now;
                tip.LastModifiedBy = user_id;
                tip.Type = ddlTipType.SelectedValue;
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    tip.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                tip.Available = rblAvailable.SelectedValue;
                tip.Author = txtAuthor.Text;
                tip.Update();
            }
            else
            {
                qSoc_Tip tip = new qSoc_Tip();
                tip.ScopeID = 1;
                tip.Created = DateTime.Now;
                tip.CreatedBy = user_id;
                tip.LastModified = DateTime.Now;
                tip.LastModifiedBy = user_id;
                tip.Available = "Yes";
                tip.MarkAsDelete = 0;
                tip.Name = txtTitle.Text;
                tip.Summary = txtSummary.Text;
                tip.Text = txtTip.Text;
                tip.LearnMoreURL = txtURL.Text;
                tip.LastModified = DateTime.Now;
                tip.LastModifiedBy = user_id;
                tip.Type = ddlTipType.SelectedValue;
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    tip.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                tip.Available = rblAvailable.SelectedValue;
                tip.Author = txtAuthor.Text;
                tip.Insert();

                tip_id = tip.TipID;
            }

            string user_name = (new qPtl_User(user_id)).UserName;

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Tip, tip_id);
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
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Tip;
                    keyword.ReferenceID = tip_id;
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

            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Tip, tip_id);


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
                qSoc_Tip tip = new qSoc_Tip(tip_id);

                int p_user_id = user_id;
                if (post_as_user_id > 0)
                    p_user_id = post_as_user_id;

                qPtl_User user = new qPtl_User(p_user_id);
                var u_space = qSoc_UserSpace_View.GetUserSpaces(tip.CreatedBy);

                // evaluate title and description
                string p_title = q_Helper.replaceSpecialCharacters(tip.Name);
                string p_description = q_Helper.replaceSpecialCharacters(tip.Text);

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        feed.CreatedBy = user.UserID;
                        feed.Available = tip.Available;
                        if (chkMoveToTop.Checked)
                            feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = tip.MarkAsDelete;
                        feed.OwnerMarkAsDelete = tip.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Tip;
                        feed.ReferenceID = tip_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "tip";
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
                        feed.Available = tip.Available;
                        feed.Created = DateTime.Now;
                        feed.CreatedBy = user.UserID;
                        feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = tip.MarkAsDelete;
                        feed.OwnerMarkAsDelete = tip.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Tip;
                        feed.ReferenceID = tip_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "tip";
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

            if (!String.IsNullOrEmpty(Request.QueryString["tipID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("tips-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&tipID=" + tip_id);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        tip_id = Convert.ToInt32(Request.QueryString["tipID"]);
        qSoc_Tip tip = new qSoc_Tip(tip_id);
        tip.Available = "No";
        tip.MarkAsDelete = 1;
        tip.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("tips-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("tips-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("tips-list.aspx");
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
