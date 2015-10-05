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
using Quartz.Help;

public partial class qHlp_help_add_topic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateCategories();
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int parent_topic_id = Convert.ToInt32(ddlHelpCategories.SelectedValue);
        double last_num_order = Convert.ToDouble(qHlp_HelpTopic.GetLastTopicOrderInCategory(parent_topic_id));
        double next_num_order;
        if (last_num_order > 0)
            next_num_order = last_num_order + .1;
        else
        {
            qHlp_HelpTopic category = new qHlp_HelpTopic(parent_topic_id);
            next_num_order = category.TopicOrder + .1;
        }

        qHlp_HelpTopic new_topic = new qHlp_HelpTopic();
        new_topic.Title = txtTitle.Text;
        new_topic.ParentTopicID = parent_topic_id;
        new_topic.TopicOrder = next_num_order;
        new_topic.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
        new_topic.Available = "Yes";
        new_topic.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
        new_topic.Created = DateTime.Now;
        new_topic.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        new_topic.LastModified = DateTime.Now;
        new_topic.MarkAsDelete = 0;
        if (chkIsSystem.Checked)
            new_topic.IsSystemHelp = true;
        new_topic.Insert();

        Response.Redirect("~/qHlp/help-viewer.aspx?topic=" + new_topic.Title + "&mode=edit");
    }

    protected void populateCategories()
    {
        ddlHelpCategories.DataSource = qHlp_HelpTopic.GetHelpCategories();
        ddlHelpCategories.DataTextField = "Title";
        ddlHelpCategories.DataValueField = "HelpTopicID";
        ddlHelpCategories.DataBind();
        ddlHelpCategories.Items.Insert(0, new ListItem("", string.Empty));
    }    
}
