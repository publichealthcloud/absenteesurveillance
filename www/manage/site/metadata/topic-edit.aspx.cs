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

public partial class edit_topic : System.Web.UI.Page
{
    public int topic_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["author_imageLocation"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // load styles for this project
            string css_text_file = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CmsTextCSS"]))
                css_text_file = Convert.ToString(ConfigurationManager.AppSettings["CmsTextCSS"]);

            reContent.CssFiles.Add(css_text_file);
            
            if (!String.IsNullOrEmpty(Request.QueryString["topicID"]))
            {
                topic_id = Convert.ToInt32(Request.QueryString["topicID"]);

                populateKeywords(topic_id, (int)qSoc_ContentType.Types.Topic);

                qSoc_Topic topic = new qSoc_Topic(topic_id);

                lblTitle.Text = "Edit Topic (ID: " + topic.TopicID + ")";
                txtName.Text = topic.Name;
                txtURL.Text = topic.URL;
                txtURL.Enabled = false;
                txtSummary.Text = topic.Summary;
                reContent.Content = topic.Description;
                rblAvailable.SelectedValue = topic.Available;
                txtImageURL.Text = topic.ImageURL;
                txtIconURL.Text = topic.IconURL;
                lblSiteNavInstructions.Text = "* This MUST be an active page on your site and be of the format: page-name.aspx?topicID=" + topic_id;
            }
            else
            {
                lblTitle.Text = "New Topic";
                btnDelete.Visible = false;
                populateKeywords(0, (int)qSoc_ContentType.Types.Topic);
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["topicID"]))
        {
            topic_id = Convert.ToInt32(Request.QueryString["topicID"]);
            qSoc_Topic topic = new qSoc_Topic(topic_id);
            
            topic.Name = txtName.Text;
            topic.URL = txtURL.Text;
            topic.Summary = txtSummary.Text;
            topic.Description = reContent.Content;
            topic.Available = rblAvailable.SelectedValue;
            topic.LastModified = DateTime.Now;
            topic.LastModifiedBy = user_id;
            topic.ImageURL = txtImageURL.Text;
            topic.IconURL = txtIconURL.Text;
            topic.Update();
        }
        else
        {
            qSoc_Topic topic = new qSoc_Topic();
            topic.ScopeID = 1;
            topic.Created = DateTime.Now;
            topic.CreatedBy = user_id;
            topic.LastModified = DateTime.Now;
            topic.LastModifiedBy = user_id;
            topic.Available = "Yes";
            topic.MarkAsDelete = 0;
            topic.Name = txtName.Text;
            topic.URL = txtURL.Text;
            topic.Summary = txtSummary.Text;
            topic.Description = reContent.Content;
            topic.Available = rblAvailable.SelectedValue;
            topic.ImageURL = txtImageURL.Text;
            topic.IconURL = txtIconURL.Text;
            topic.Insert();

            topic_id = topic.TopicID;
        }

        // add keywords
        qPtl_KeywordReference.DeleteKeywordReferencesByContent((int)qSoc_ContentType.Types.Topic, topic_id);
        foreach (ListItem item in cblKeywords.Items)
        {
            if (item.Selected)
            {
                qPtl_KeywordReference keyword = new qPtl_KeywordReference();
                keyword.Available = "Yes";
                keyword.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                keyword.KeywordID = Convert.ToInt32(item.Value);
                keyword.ContentTypeID = (int)qSoc_ContentType.Types.Topic;
                keyword.ReferenceID = topic_id;
                keyword.Created = DateTime.Now;
                keyword.LastModified = DateTime.Now;
                keyword.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                keyword.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                keyword.MarkAsDelete = 0;
                keyword.Insert();
            }
        }

        Response.Redirect("~/manage/site/metadata/topics-list.aspx");
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
        topic_id = Convert.ToInt32(Request.QueryString["topicID"]);
        
        qSoc_Topic topic = new qSoc_Topic(topic_id);
        topic.Available = "No";
        topic.MarkAsDelete = 1;
        topic.Update();

        Response.Redirect("topics-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("topics-list.aspx");
    }

    protected void btnEnableSiteNav_Click(object sender, EventArgs e)
    {
        txtURL.Enabled = true;
        btnEnableSiteNav.Visible = false;
    }
}
