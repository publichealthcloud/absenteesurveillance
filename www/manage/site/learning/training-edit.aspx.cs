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

public partial class edit_training : System.Web.UI.Page
{
    public int training_id;
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);
    protected string manage_url = System.Configuration.ConfigurationManager.AppSettings["Site_ManageURL"];
    protected string key = System.Configuration.ConfigurationManager.AppSettings["Site_AutomationKey"];
    protected string final_manage_url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateTrainingTypes();
            populateDesignTemplates();
            populateAuthors();

            int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);

            final_manage_url = manage_url + "/public/launch-as-user.aspx?key=" + key + "&userID=" + curr_user_id;

            training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
            
            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {
                populateKeywords(training_id, (int)qSoc_ContentType.Types.Training);
                populateTopics(training_id, (int)qSoc_ContentType.Types.Training);

                ViewState.Add("vsTrainingID", training_id);

                qLrn_Training_View training = new qLrn_Training_View(training_id);

                lblTitle.Text = "Edit Training (ID: " + training.TrainingID + ")";
                txtTitle.Text = training.Title;
                txtDescription.Text = training.Description;
                rblAvailable.SelectedValue = training.Available;
                if (!String.IsNullOrEmpty(Convert.ToString(training.PersonAuthorID)))
                    ddlAuthors.SelectedValue = Convert.ToString(training.PersonAuthorID);

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }

                ddlTrainingTypes.Enabled = false;
                ddlTrainingTypes.SelectedValue = Convert.ToString(training.TrainingTypeID);
                lblTrainingType.Text = "<i>NOTE: Once set, this cannot be changed</i>";
                if (training.TrainingTypeName == "Internal")
                {
                    plhInternalTraining.Visible = true;
                    ddlDesignTemplates.SelectedValue = Convert.ToString(training.DesignThemeID);
                    rfvTrainingLink.Enabled = false;
                }
                else if (training.TrainingTypeName == "External")
                {
                    plhExternalTraining.Visible = true;
                    rfvDesignTemplate.Enabled = false;
                }
                else if (training.TrainingTypeName == "In Person")
                {
                    plhInPersonTraining.Visible = true;
                }
                plhMetaData.Visible = true;

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Training, training_id);
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
                    if (feed.FeedID > 0)
                    {
                        btnMakeAvailableCampaigns.Visible = false;
                        lblExistsFeed.Text = "<i class=\"icon-check\"></i> This training is available for use in campaigns";
                    }
                }
            }
            else
            {
                lblTitle.Text = "New Training";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "No";
                plhTools.Visible = false;
                populateKeywords(0, (int)qSoc_ContentType.Types.Training);
                plhMetaData.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(training_id)))
            training_id = (Int32)ViewState["vsTrainingID"];
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {
                training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
                qLrn_Training training = new qLrn_Training(training_id);
                training.Title = txtTitle.Text;
                training.Description = txtDescription.Text;
                training.LastModified = DateTime.Now;
                training.LastModifiedBy = user_id;
                training.Available = rblAvailable.SelectedValue;
                if (!String.IsNullOrEmpty(ddlAuthors.SelectedValue))
                    training.PersonAuthorID = Convert.ToInt32(ddlAuthors.SelectedValue);
                if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "Internal")
                {
                    training.DesignThemeID = Convert.ToInt32(ddlDesignTemplates.SelectedValue);
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "External")
                {
                    training.Link = txtLink.Text;
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "In Person")
                {
                    // do something
                }
                training.Update();
            }
            else
            {
                qLrn_Training training = new qLrn_Training();
                training.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                training.Created = DateTime.Now;
                training.CreatedBy = user_id;
                training.LastModified = DateTime.Now;
                training.LastModifiedBy = user_id;
                training.Available = "Yes";
                training.MarkAsDelete = 0;
                training.Title = txtTitle.Text;
                training.Description = txtDescription.Text;
                training.LastModified = DateTime.Now;
                training.LastModifiedBy = user_id;
                training.Available = rblAvailable.SelectedValue;
                training.TrainingTypeID = Convert.ToInt32(ddlTrainingTypes.SelectedValue);
                if (!String.IsNullOrEmpty(ddlAuthors.SelectedValue))
                    training.PersonAuthorID = Convert.ToInt32(ddlAuthors.SelectedValue);
                if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "Internal")
                {
                    training.DesignThemeID = Convert.ToInt32(ddlDesignTemplates.SelectedValue);
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "External")
                {
                    training.Link = txtLink.Text;
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "In Person")
                {
                    // do something
                }
                training.Insert();

                training_id = training.TrainingID;
            }

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Training, training_id);
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
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Training;
                    keyword.ReferenceID = training_id;
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

            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Training, training_id);


            if (!chkDisplayInFeed.Checked && !chkDisplayInExplore.Checked)
            {
                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        if (feed.Type == "training" && feed.Available == "Yes" && feed.VisibleFeed == false && feed.VisibleExplore == false)
                        {
                            // situation when the training needs to be available for campaigns but is not visible otherwise
                            feed.MarkAsDelete = 1;
                            feed.Available = "Yes";
                            feed.VisibleFeed = false;
                            feed.VisibleExplore = false;
                        }
                        else
                        {
                            feed.MarkAsDelete = 1;
                            feed.Available = "No";
                            feed.ReservedKeywords = reserved_keywords;
                            if (!chkDisplayInFeed.Checked)
                                feed.VisibleFeed = false;
                            if (!chkDisplayInExplore.Checked)
                                feed.VisibleExplore = false;
                        }
                    
                        feed.Update();
                    }
                }
            }
            else if (chkDisplayInFeed.Checked || chkDisplayInExplore.Checked || !string.IsNullOrEmpty(reserved_keywords))
            {
                qLrn_Training training = new qLrn_Training(training_id);

                int p_user_id = user_id;
                if (post_as_user_id > 0)
                    p_user_id = post_as_user_id;

                qPtl_User user = new qPtl_User(p_user_id);
                var u_space = qSoc_UserSpace_View.GetUserSpaces(training.CreatedBy);

                // evaluate title and description
                string p_title = q_Helper.replaceSpecialCharacters(training.Title);
                string p_description = q_Helper.replaceSpecialCharacters(training.Description);

                // add first slide image to the description
                var slides = qLrn_TrainingSlide_View.GetAvailableSlidesInOrder(training.TrainingID);
                string slide_icon = string.Empty;
                int s_count = 0;
                foreach (var s in slides)
                {
                    if (s_count == 0)
                    {
                        string slide_image_path = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Learning_SlideIconBasePath"]);
                        if (!String.IsNullOrEmpty(slide_image_path))
                        {
                            slide_icon = "<img src=\"" + slide_image_path + "/" + s.TrainingID + "/" + s.SlideID + ".png?width=200&height=133\">";
                        }

                        s_count++;
                    }
                }
                string body = p_description;
                if (!String.IsNullOrEmpty(slide_icon))
                {
                    body = slide_icon + "<br>" + p_description;
                }

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        feed.CreatedBy = user.UserID;
                        feed.Available = training.Available;
                        if (chkMoveToTop.Checked)
                            feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = training.MarkAsDelete;
                        feed.OwnerMarkAsDelete = training.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Training;
                        feed.ReferenceID = training_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "training";
                        feed.Title = p_title;
                        feed.Description = p_description;
                        feed.Body = body;
                        feed.OwnerRole = user.HighestRole;
                        feed.OwnerRoleID = user.HighestRank;
                        feed.OwnerKeywords = owner_keywords;
                        feed.ReservedKeywords = reserved_keywords;
                        feed.UploadedFrom = "manager";
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
                        feed.Available = training.Available;
                        feed.Created = DateTime.Now;
                        feed.CreatedBy = user.UserID;
                        feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = training.MarkAsDelete;
                        feed.OwnerMarkAsDelete = training.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Training;
                        feed.ReferenceID = training_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "training";
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

            string user_name = (new qPtl_User(user_id)).UserName;

            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("trainings-list.aspx");
            }
            else
            {
                //Response.Redirect(Request.Url.ToString() + "?mode=add-successful&questionCategoryID=" + tip_id);
                Response.Redirect("trainings-list.aspx");
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
        qLrn_Training training = new qLrn_Training(training_id);
        training.Available = "No";
        training.MarkAsDelete = 1;
        training.Update();

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("trainings-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("trainings-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        Response.Redirect("trainings-list.aspx");
    }

    protected void populateTrainingTypes()
    {
        ddlTrainingTypes.DataSource = qLrn_TrainingType.GetTypes();
        ddlTrainingTypes.DataTextField = "Name";
        ddlTrainingTypes.DataValueField = "TrainingTypeID";
        ddlTrainingTypes.DataBind();
        ddlTrainingTypes.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateDesignTemplates()
    {
        ddlDesignTemplates.DataSource = qLrn_TrainingDesignTheme.GetTrainingDesignThemes();
        ddlDesignTemplates.DataTextField = "ThemeName";
        ddlDesignTemplates.DataValueField = "TrainingDesignThemeID";
        ddlDesignTemplates.DataBind();
        ddlDesignTemplates.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateAuthors()
    {
        ddlAuthors.DataSource = qLrn_Author.GetAuthors();
        ddlAuthors.DataTextField = "AuthorName";
        ddlAuthors.DataValueField = "AuthorID";
        ddlAuthors.DataBind();
        ddlAuthors.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void ddlTrainingTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        plhExternalTraining.Visible = false;
        plhInternalTraining.Visible = false;
        plhInPersonTraining.Visible = false;
        plhMetaData.Visible = true;

        if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "Internal")
        {
            plhInternalTraining.Visible = true;
            rfvTrainingLink.Enabled = false;
        }
        else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "External")
        {
            plhExternalTraining.Visible = true;
            rfvDesignTemplate.Enabled = false;
        }
        else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "In Person")
        {
            plhInPersonTraining.Visible = true;
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

    protected void populateTopics(int training_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Training, training_id);
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

    protected void btnMakeAvailableCampaigns_Click(object sender, EventArgs e)
    {
        int training_id = Convert.ToInt32(Request.QueryString["trainingID"]);

        qSoc_Feed feed = new qSoc_Feed();
        feed.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
        feed.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
        feed.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        feed.Available = "Yes";
        feed.MarkAsDelete = 0;
        feed.LastModified = DateTime.Now;
        feed.Created = DateTime.Now;
        feed.OwnerID = Convert.ToInt32(Context.Items["UserID"]);
        feed.ReferenceID = training_id;
        feed.ContentTypeID = Convert.ToInt32(qSoc_ContentType.Types.Training);
        feed.Type = "training";
        feed.Title = txtTitle.Text;
        feed.Description = txtDescription.Text;
        feed.VisibleFeed = false;
        feed.VisibleOwnerFeed = false;
        feed.VisibleOwnerProfile = false;
        feed.VisibleExplore = false;
        feed.VisibleCampaign = false;
        feed.UploadedFrom = "manager";
        feed.Insert();

        Response.Redirect("~/manage/site/learning/training-edit.aspx?trainingID=" + training_id);
    }
}
