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

public partial class edit_page_element : System.Web.UI.Page
{
    public int element_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["site_imageLocation"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            // load styles for this project
            string css_text_file = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CmsTextCSS"]))
                css_text_file = Convert.ToString(ConfigurationManager.AppSettings["CmsTextCSS"]);

            reContent.CssFiles.Add(css_text_file);
            
            if (!String.IsNullOrEmpty(Request.QueryString["elementID"]))
            {
                element_id = Convert.ToInt32(Request.QueryString["elementID"]);

                qSoc_Element element = new qSoc_Element(element_id);

                lblTitle.Text = "Edit page zone (ID: " + element.ElementID + ")";
                txtName.Text = element.ElementType;
                txtSummary.Text = element.Title;
                reContent.Content = element.HTML;
                rblAvailable.SelectedValue = element.Available;
            }
            else
            {
                lblTitle.Text = "New Page Zone";
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["elementID"]))
            {
                element_id = Convert.ToInt32(Request.QueryString["elementID"]);
                qSoc_Element element = new qSoc_Element(element_id);

                element.ElementType = txtName.Text;
                element.Title = txtSummary.Text;
                element.HTML = reContent.Content;
                element.Available = rblAvailable.SelectedValue;
                element.LastModified = DateTime.Now;
                element.LastModifiedBy = user_id;
                element.Update();
            }
            else
            {
                qSoc_Element element = new qSoc_Element();
                element.ScopeID = 1;
                element.Created = DateTime.Now;
                element.CreatedBy = user_id;
                element.LastModified = DateTime.Now;
                element.LastModifiedBy = user_id;
                element.Available = "Yes";
                element.MarkAsDelete = 0;
                element.Title = txtSummary.Text;
                element.ElementType = txtName.Text;
                element.HTML = reContent.Content;
                element.Available = rblAvailable.SelectedValue;
                element.Highlighted = "Yes";
                element.Insert();

                element_id = element.ElementID;
            }

            // redirect to page to add tip + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["elementID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&elementID=" + element_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        element_id = Convert.ToInt32(Request.QueryString["elementID"]);

        qSoc_Element element = new qSoc_Element(element_id);
        element.Available = "No";
        element.MarkAsDelete = 1;
        element.Update();

        Response.Redirect("page-elements-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("page-elements-list.aspx");
    }
}
