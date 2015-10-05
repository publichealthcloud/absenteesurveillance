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
using Quartz.Data;
using Quartz.Core;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;
using Quartz.CMS;

public partial class edit_poll : System.Web.UI.Page
{
    public int poll_id;
    public string experience_type;
    public string poll_type;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["site_imageLocation"]);
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        plhAvailableTimes.Visible = false;
        
        if (Page.IsPostBack)
        {
            // re-bind choices        if (String.IsNullOrEmpty(Convert.ToString(poll_id)))
            if (!String.IsNullOrEmpty(Request.QueryString["pollID"]))
                poll_id = (Int32)ViewState["vsPollID"];
        }
        
        if (!Page.IsPostBack)
        {

            populateThemes();
            
            if (!String.IsNullOrEmpty(Request.QueryString["pollID"]))
            {
                poll_id = Convert.ToInt32(Request.QueryString["pollID"]);

                ViewState.Add("vsPollID", poll_id);

                populateKeywords(poll_id, (int)qSoc_ContentType.Types.Poll);
                populateTopics(poll_id, (int)qSoc_ContentType.Types.Poll);

                qSoc_Poll2 poll = new qSoc_Poll2(poll_id);

                lblTitle.Text = "Edit poll (ID: " + poll.PollID + ")";
                txtQuestion.Text = poll.Question;
                rblAvailable.SelectedValue = poll.Available;

                if (!String.IsNullOrEmpty(Convert.ToString(poll.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(poll.ThemeID);

                if (poll.PollType == "Fact")
                {
                    ddlPollType.SelectedValue = "Fact";
                }
                else if (poll.PollType == "Opinion")
                {
                    if (poll.ExperienceType == "Poll")
                    {
                        ddlPollType.SelectedValue = "Opinion";
                    } 
                    else 
                    {
                        ddlPollType.SelectedValue = "Think";
                    }
                }

                rdtStartTime.SelectedDate = poll.StartDate;
                rdtEndTime.SelectedDate = poll.EndDate;

                poll_type = poll.ExperienceType;
                BindChoices();

                if (poll.ExperienceType == "Think")
                {
                    plhAddResponse.Visible = false;
                    repChoices.Visible = false;
                    repNoEditChoices.Visible = true;

                }
                else
                {
                    repChoices.Visible = true;
                    repNoEditChoices.Visible = false;
                }

                ddlPollType.Enabled = false;
                lblPollTypeMessage.Text = "*** You cannot change the poll type once the poll has been created ***";
                lblPollTypeMessage.Visible = true;
                lblMessage.Visible = true;

                if (rblAvailable.SelectedValue == "Yes")
                {
                    // disabled since no longer using start dates
                    //plhAvailableTimes.Visible = true;
                    //rfvStartTime.Enabled = true;
                    //rfvEndTime.Enabled = true;
                    plhHighlightedPoll.Visible = true;
                }
                else
                {
                    plhAvailableTimes.Visible = false;
                    rfvStartTime.Enabled = false;
                    rfvEndTime.Enabled = false;
                    plhHighlightedPoll.Visible = false;
                }

                // see if in feed
                qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Poll, poll_id);
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
                lblTitle.Text = "New poll";
                plhTools.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                populateKeywords(0, (int)qSoc_ContentType.Types.Poll);
                plhChoices.Visible = false;
                plhAdditionalInfo.Visible = false;
                lblMessage.Text = "*** You must first enter and save the basic poll info and then you can add response choices";
                rfvAvailable.Enabled = false;
                rfvEndTime.Enabled = false;
                rfvStartTime.Enabled = false;
                plhDisplayInSiteSettings.Visible = false;
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added - you can now configure additional poll info ***";
                lblMessageBottom.Text = "*** Record Successfully Added - you can now configure additional poll info ***";
            }

            if (Request.QueryString["mode"] == "edit-choice")
            {
                int poll_choice_id = Convert.ToInt32(Request.QueryString["pollChoiceID"]);
                ViewState.Add("vsPollChoiceID", poll_choice_id);

                qSoc_PollChoice2 choice = new qSoc_PollChoice2(poll_choice_id);
                txtChoice.Text = choice.Choice;
                txtMediaChoiceHTML.Text = choice.ChoiceMediaHTML;
                rblIsCorrect.SelectedValue = choice.Correct;
                if (ddlPollType.SelectedValue == "Fact")
                    plhChoiceCorrect.Visible = true;
                else
                    plhChoiceCorrect.Visible = false;
                txtFeedbackTitle.Text = choice.FeedbackTitle;
                txtFeedbackText.Text = choice.FeedbackDescription;
                txtFeedbackLink.Text = choice.FeedbackUrl;

                plhEditChoice.Visible = true;
            }

            var highlighted_poll = qSoc_Poll2.GetHighlightedPoll();

            if (highlighted_poll != null)
            {
                if (highlighted_poll.PollID == poll_id)
                {
                    chkHighlightedPoll.Checked = true;
                    lblHighlightedMessage.Text = "This is the current highlighted poll ID: " + highlighted_poll.PollID;
                }
                else
                    lblHighlightedMessage.Text = "Check to set as the highlighted poll";
            }
            else
                lblHighlightedMessage.Text = "Check to set as the highlighted poll";
        }
    }

    protected void BindChoices()
    {
        int curr_poll_id = (Int32)ViewState["vsPollID"];
        repChoices.DataSource = qSoc_PollChoice.get_choices(curr_poll_id);
        repChoices.DataBind();
        repNoEditChoices.DataSource = qSoc_PollChoice.get_choices(curr_poll_id);
        repNoEditChoices.DataBind();
        repChoices.DataBind();
        txtFeedbackLink.Text = "";
        txtChoice.Text = "";
        txtMediaChoiceHTML.Text = "";
        txtFeedbackTitle.Text = "";
        txtFeedbackText.Text = "";
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["pollID"]))
            {
                poll_id = Convert.ToInt32(Request.QueryString["pollID"]);
                qSoc_Poll2 poll = new qSoc_Poll2(poll_id);

                poll.Question = txtQuestion.Text;
                poll.LastModified = DateTime.Now;
                poll.LastModifiedBy = user_id;
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    poll.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                else
                    poll.ThemeID = 0;

                DateTime start_time = new DateTime();
                DateTime end_time = new DateTime();
                if (rblAvailable.SelectedValue == "Yes")
                {
                    poll.Available = "Yes";
                    start_time = DateTime.Now;
                    end_time = DateTime.Now.AddYears(10);
                    poll.StartDate = start_time;
                    poll.EndDate = end_time;

                    /*
                    // code no longer used since use defaults for setting these values
                    if (!String.IsNullOrEmpty(Convert.ToString(rdtStartTime.SelectedDate)))
                        poll.StartDate = Convert.ToDateTime(rdtStartTime.SelectedDate);
                    if (!String.IsNullOrEmpty(Convert.ToString(rdtEndTime.SelectedDate)))
                        poll.EndDate = Convert.ToDateTime(rdtEndTime.SelectedDate);
                     */
                }
                else
                {
                    poll.Available = "No";
                    start_time = DateTime.Now.AddDays(-2);
                    end_time = DateTime.Now.AddDays(-1);
                    poll.StartDate = start_time;
                    poll.EndDate = end_time;

                    /*
                    // code no longer used since use defaults for setting these values
                    if (!String.IsNullOrEmpty(Convert.ToString(rdtStartTime.SelectedDate)))
                        poll.StartDate = null;
                    if (!String.IsNullOrEmpty(Convert.ToString(rdtEndTime.SelectedDate)))
                        poll.EndDate = null;
                    */
                }

                poll.Update();
            }
            else
            {
                qSoc_Poll2 poll = new qSoc_Poll2();
                poll.ScopeID = 1;
                poll.Created = DateTime.Now;
                poll.CreatedBy = user_id;
                poll.LastModified = DateTime.Now;
                poll.LastModifiedBy = user_id;
                poll.Available = "Yes";
                poll.MarkAsDelete = 0;
                poll.Question = txtQuestion.Text;
                if (ddlPollType.SelectedValue == "Think")
                {
                    poll.PollType = "Opinion";
                    poll.ExperienceType = "Think";
                }
                else if (ddlPollType.SelectedValue == "Fact")
                {
                    poll.PollType = "Fact";
                    poll.ExperienceType = "Poll";
                }
                else if (ddlPollType.SelectedValue == "Opinion")
                {
                    poll.PollType = "Opinion";
                    poll.ExperienceType = "Poll";
                }
                poll.Available = "No";

                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    poll.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);

                // default to start time is today and end time is 20 years from now
                poll.StartDate = DateTime.Now;
                poll.EndDate = DateTime.Now.AddYears(20);

                poll.Insert();
                /*
                if (!String.IsNullOrEmpty(Convert.ToString(rdtStartTime.SelectedDate)))
                    poll.StartDate = Convert.ToDateTime(rdtStartTime.SelectedDate);
                if (!String.IsNullOrEmpty(Convert.ToString(rdtEndTime.SelectedDate)))
                    poll.EndDate = Convert.ToDateTime(rdtEndTime.SelectedDate);
                 */

                poll_id = poll.PollID;

                if (ddlPollType.SelectedValue == "Think")
                {
                    int curr_choice_id = createNewChoice(poll_id, "Agree", false);
                    curr_choice_id = createNewChoice(poll_id, "Disagree", false);
                }
            }

            // process if highlighted poll
            qDbs_SQLcode sql = new qDbs_SQLcode();
            string sqlCode = string.Empty;
            sqlCode = "UPDATE qSoc_Polls SET Highlighted = null";

            if (lblHighlightedMessage.Text.Contains("current") && chkHighlightedPoll.Checked == false)
            {
                sql.ExecuteSQL(sqlCode);        // // clear all highlighted options
            }
            else if (chkHighlightedPoll.Checked && lblHighlightedMessage.Text.Contains("current"))
            {
                sql.ExecuteSQL(sqlCode);        // clear all highlighted options
                qSoc_Poll2 poll = new qSoc_Poll2(poll_id);
                poll.Highlighted = true;
                poll.Update();
            }
            else if (chkHighlightedPoll.Checked && lblHighlightedMessage.Text.Contains("Check"))
            {
                sql.ExecuteSQL(sqlCode);        // clear all highlighted options
                qSoc_Poll2 poll = new qSoc_Poll2(poll_id);
                poll.Highlighted = true;
                poll.Update();
            }

            // add keywords
            string owner_keywords = string.Empty;
            qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Poll, poll_id);
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
                    keyword.ContentTypeID = (int)qSoc_ContentType.Types.Poll;
                    keyword.ReferenceID = poll_id;
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

            qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Poll, poll_id);

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
                qSoc_Poll2 poll = new qSoc_Poll2(poll_id);

                int p_user_id = user_id;
                if (post_as_user_id > 0)
                    p_user_id = post_as_user_id;

                qPtl_User user = new qPtl_User(p_user_id);
                var u_space = qSoc_UserSpace_View.GetUserSpaces(poll.CreatedBy);

                // evaluate title and description
                string p_title = q_Helper.replaceSpecialCharacters(poll.Question);
                string p_description = string.Empty;
                // add choices to description field
                var choices = qSoc_PollChoice2.GetAvailablePollChoices(poll_id);

                string choices_html = string.Empty;
                int i = 1;
                if (choices != null)
                {
                    foreach (var c in choices)
                    {
                        if (!String.IsNullOrEmpty(choices_html))
                            choices_html += "<br>";
                        choices_html += i + ") " + c.Choice;
                        i++;
                    }
                }

                if (!String.IsNullOrEmpty(choices_html))
                {
                    p_description = choices_html;
                }

                if (feed != null)
                {
                    if (feed.FeedID > 0)
                    {
                        feed.CreatedBy = user.UserID;
                        feed.Available = poll.Available;
                        if (chkMoveToTop.Checked)
                            feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = poll.MarkAsDelete;
                        feed.OwnerMarkAsDelete = poll.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Poll;
                        feed.ReferenceID = poll_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "poll";
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
                        feed.Available = poll.Available;
                        feed.Created = DateTime.Now;
                        feed.CreatedBy = user.UserID;
                        feed.LastModified = DateTime.Now;
                        feed.LastModifiedBy = user.UserID;
                        feed.MarkAsDelete = poll.MarkAsDelete;
                        feed.OwnerMarkAsDelete = poll.MarkAsDelete;
                        feed.ContentTypeID = (int)qSoc_ContentType.Types.Poll;
                        feed.ReferenceID = poll_id;
                        feed.OwnerID = user.UserID;
                        feed.OwnerName = user.UserName;
                        feed.OwnerProfilePic = user.ProfilePict;
                        feed.Type = "poll";
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


            // redirect to page to add poll + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["pollID"]))
            {
                Response.Redirect("poll-edit.aspx?pollID=" + poll_id);
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&pollID=" + poll_id);
            }
        }
        else
        {
            lblMessage.Text = "*** A problem has occurred -- make sure all the required information has been entered ***";
            lblMessageBottom.Text = "*** A problem has occurred -- make sure all the required information has been entered ***";
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

    protected void populateTopics(int video_id, int content_type_id)
    {
        var topics = qSoc_Topic.GetTopics();
        qSoc_Feed feed = new qSoc_Feed((int)qSoc_ContentType.Types.Poll, poll_id);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        poll_id = Convert.ToInt32(Request.QueryString["pollID"]);

        qSoc_Poll2 poll = new qSoc_Poll2(poll_id);
        poll.DeleteAllPollData(poll_id);

        Response.Redirect("polls-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("polls-list.aspx");
    }

    protected void btnSaveChoice_Click(object sender, EventArgs e)
    {
        Page.Validate("choice");

        if (Page.IsValid)
        {
            if (String.IsNullOrEmpty(Request.QueryString["pollChoiceID"]))
            {
                int curr_poll_id = (Int32)ViewState["vsPollID"];
                qSoc_PollChoice2 choice = new qSoc_PollChoice2();
                choice.PollID = curr_poll_id;
                choice.Choice = txtChoice.Text;
                choice.ChoiceMediaHTML = txtMediaChoiceHTML.Text;
                choice.FeedbackTitle = txtFeedbackTitle.Text;
                choice.FeedbackDescription = txtFeedbackText.Text;
                choice.FeedbackUrl = txtFeedbackLink.Text;
                if (rblIsCorrect.SelectedValue == "Yes")
                    choice.Correct = "Yes";
                choice.Insert();

                int new_poll_choice_id = choice.PollChoiceID;
                BindChoices();
                plhEditChoice.Visible = false;
            }
            else
            {
                int curr_poll_id = (Int32)ViewState["vsPollID"];
                int curr_poll_choice_id = Convert.ToInt32(Request.QueryString["pollChoiceID"]);
                qSoc_PollChoice2 choice = new qSoc_PollChoice2(curr_poll_choice_id);
                choice.Choice = txtChoice.Text;
                choice.ChoiceMediaHTML = txtMediaChoiceHTML.Text;
                choice.FeedbackTitle = txtFeedbackTitle.Text;
                choice.FeedbackDescription = txtFeedbackText.Text;
                choice.FeedbackUrl = txtFeedbackLink.Text;
                if (rblIsCorrect.SelectedValue == "Yes")
                    choice.Correct = "Yes";
                choice.Update();
                BindChoices();
                plhEditChoice.Visible = false;
            }
        }
    }

    protected int createNewChoice(int curr_poll_id, string choice, bool is_correct)
    {
        int new_poll_id = 0;
        qSoc_PollChoice2 new_choice = new qSoc_PollChoice2();
        new_choice.PollID = curr_poll_id;
        new_choice.Choice = choice;
        if (is_correct == true)
            new_choice.Correct = "Yes";
        new_choice.ChoiceMediaHTML = txtMediaChoiceHTML.Text;
        new_choice.FeedbackTitle = txtFeedbackTitle.Text;
        new_choice.FeedbackDescription = txtFeedbackText.Text;
        new_choice.FeedbackUrl = txtFeedbackLink.Text;

        new_choice.Insert();
        new_poll_id = new_choice.PollChoiceID;
        return new_poll_id;
    }

    protected void btnCancelChoice_Click(object sender, EventArgs e)
    {
        plhEditChoice.Visible = false;
        txtFeedbackLink.Text = "";
        txtChoice.Text = "";
        txtFeedbackTitle.Text = "";
        txtFeedbackText.Text = "";
    }

    protected void btnDeleteChoice_Click(object sender, EventArgs e)
    {
        int curr_poll_choice_id = Convert.ToInt32(Request.QueryString["pollChoiceID"]);
        qSoc_PollChoice2 choice = new qSoc_PollChoice2();
        choice.DeletePollChoice(curr_poll_choice_id);

        qSoc_PollAnswer2 answers = new qSoc_PollAnswer2();
        answers.DeleteAllPollChoiceAnswers(curr_poll_choice_id);

        BindChoices();
        plhEditChoice.Visible = false;
    }

    protected void rblAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAvailable.SelectedValue == "Yes")
        {
            plhAvailableTimes.Visible = true;
            rfvStartTime.Enabled = true;
            rfvEndTime.Enabled = true;
        }
        else
        {
            plhAvailableTimes.Visible = false;
            rfvStartTime.Enabled = false;
            rfvEndTime.Enabled = false;
        }
    }
    protected void lnkAddresponse_Click(object sender, EventArgs e)
    {
        plhEditChoice.Visible = true;
        btnDeleteChoice.Visible = false;
        rblIsCorrect.SelectedIndex = -1;
        txtFeedbackLink.Text = "";
        txtChoice.Text = "";
        txtFeedbackTitle.Text = "";
        txtFeedbackText.Text = "";
    }
}
