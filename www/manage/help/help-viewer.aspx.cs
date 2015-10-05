using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Quartz;
using Quartz.Help;
using Quartz.Portal;

public partial class qHlp_help_viewer : System.Web.UI.Page
{
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["help_imageLocation"]);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnClose.Visible = false;

            string css_text_file = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CmsTextCSS"]))
                css_text_file = Convert.ToString(ConfigurationManager.AppSettings["CmsTextCSS"]);

            reContent.CssFiles.Add(css_text_file);

            if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["topic"])))
            {
                string topic = Convert.ToString(Request.QueryString["topic"]);
                var help_topic = qHlp_HelpTopic.GetHelpTopicByTitle(topic);

                lblTitle.Text = help_topic.Title;
                litSummary.Text = "Summary: " + help_topic.Summary;
                litKeywords.Text = "Keywords: " + help_topic.Keywords;
                litBody.Text = help_topic.Body;

                txtTitle.Text = help_topic.Title;
                txtSummary.Text = help_topic.Summary;
                txtKeywords.Text = help_topic.Keywords;
                reContent.Content = help_topic.Body;
                plhEditHelp.Visible = false;

                string rawURL = Request.Url.ToString();
                string passURL = Server.UrlEncode(rawURL);
                hplPrint.NavigateUrl = "~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=QuartzHelp_" + topic + ".pdf";
                hplPrint.Target = "_blank";
            }

            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            string highest_role = Convert.ToString(user.HighestRole);
            qPtl_ManagerPermission_View permission = new qPtl_ManagerPermission_View(highest_role);

            if (permission != null)
            {
                if (permission.ManagerPermissionID > 0)
                {
                    if (permission.Help.Contains("Admin") || permission.Help.Contains("Author"))
                    {
                        divEdit.Visible = true;

                        if (Convert.ToString(Request.QueryString["mode"]) == "edit")
                        {
                            plhEditHelp.Visible = true;
                            plhViewHelp.Visible = false;
                        }
                    }
                }
            }
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string topic = Convert.ToString(Request.QueryString["topic"]);
        var help_topic = qHlp_HelpTopic.GetHelpTopicByTitle(topic);

        help_topic.Title = txtTitle.Text;
        help_topic.Summary = txtSummary.Text;
        help_topic.Keywords = txtKeywords.Text;
        help_topic.Body = reContent.Content;
        help_topic.LastModified = DateTime.Now;
        help_topic.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
        help_topic.Update();

        plhEditHelp.Visible = false;
        plhViewHelp.Visible = true;
        btnClose.Visible = false;
        btnEdit.Visible = true;

        lblTitle.Text = help_topic.Title;
        litSummary.Text = help_topic.Summary;
        litKeywords.Text = help_topic.Keywords;
        litBody.Text = help_topic.Body;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        plhViewHelp.Visible = false;
        plhEditHelp.Visible = true;
        btnEdit.Visible = false;
        btnClose.Visible = true;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        plhViewHelp.Visible = true;
        plhEditHelp.Visible = false;
        btnEdit.Visible = true;
        btnClose.Visible = false;
    }
}
