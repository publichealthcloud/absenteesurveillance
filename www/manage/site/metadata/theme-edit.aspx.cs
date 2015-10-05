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

public partial class edit_theme : System.Web.UI.Page
{
    public int theme_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ThemesFolder"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["themeID"]))
            {
                theme_id = Convert.ToInt32(Request.QueryString["themeID"]);

                qSoc_Theme theme = new qSoc_Theme(theme_id);

                lblTitle.Text = "Edit Theme (ID: " + theme.ThemeID + ")";
                txtName.Text = theme.Name;
                txtURL.Text = theme.URL;
                txtURL.Enabled = false;
                reContent.Content = theme.Description;
                rblAvailable.SelectedValue = theme.Available;
                lblSiteNavInstructions.Text = "* This MUST be an active page on your site and be of the format: page-name.aspx?themeID=" + theme_id;
                hplPreviewTheme.NavigateUrl = "/social/explore/theme-details.aspx?themeID=" + theme_id;
            }
            else
            {
                lblTitle.Text = "New Theme";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["themeID"]))
        {
            theme_id = Convert.ToInt32(Request.QueryString["themeID"]);
            qSoc_Theme theme = new qSoc_Theme(theme_id);
            
            theme.Name = txtName.Text;
            theme.URL = txtURL.Text;
            theme.Description = reContent.Content;
            theme.Available = rblAvailable.SelectedValue;
            theme.LastModified = DateTime.Now;
            theme.LastModifiedBy = user_id;

            theme.Update();
        }
        else
        {
            qSoc_Theme theme = new qSoc_Theme();
            theme.ScopeID = 1;
            theme.Created = DateTime.Now;
            theme.CreatedBy = user_id;
            theme.LastModified = DateTime.Now;
            theme.LastModifiedBy = user_id;
            theme.Available = "Yes";
            theme.MarkAsDelete = 0;
            theme.Name = txtName.Text;
            theme.URL = txtURL.Text;
            theme.Description = reContent.Content;
            theme.Available = rblAvailable.SelectedValue;
            theme.Insert();

            theme_id = theme.ThemeID;
        }

        Response.Redirect("~/manage/site/metadata/themes-list.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        theme_id = Convert.ToInt32(Request.QueryString["themeID"]);
        
        qSoc_Theme theme = new qSoc_Theme(theme_id);
        theme.Available = "No";
        theme.MarkAsDelete = 1;
        theme.Update();

        Response.Redirect("themes-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("themes-list.aspx");
    }

    protected void btnEnableSiteNav_Click(object sender, EventArgs e)
    {
        txtURL.Enabled = true;
        btnEnableSiteNav.Visible = false;
    }
}
