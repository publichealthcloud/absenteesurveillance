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

public partial class edit_keyword : System.Web.UI.Page
{
    public int keyword_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateThemes();
            
            if (!String.IsNullOrEmpty(Request.QueryString["keywordID"]))
            {
                keyword_id = Convert.ToInt32(Request.QueryString["keywordID"]);

                qPtl_Keyword keyword = new qPtl_Keyword(keyword_id);

                lblTitle.Text = "Edit Keyword (ID: " + keyword.KeywordID + ")";
                txtKeyword.Text = keyword.Keyword;
                txtAssociatedKeywords.Text = keyword.AssociatedKeywords;
                txtDefinition.Text = keyword.Definition;
                rblAvailable.SelectedValue = keyword.Available;

                if (!String.IsNullOrEmpty(Convert.ToString(keyword.ThemeID)))
                    ddlTheme.SelectedValue = Convert.ToString(keyword.ThemeID);
            }
            else
            {
                lblTitle.Text = "New keyword";
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

            if (!String.IsNullOrEmpty(Request.QueryString["keywordID"]))
            {
                keyword_id = Convert.ToInt32(Request.QueryString["keywordID"]);
                qPtl_Keyword keyword = new qPtl_Keyword(keyword_id);

                keyword.Keyword = txtKeyword.Text;

                string keyword_val = txtKeyword.Text;
                string keyword_assoc_val = txtAssociatedKeywords.Text;
                keyword_assoc_val = keyword_assoc_val.Replace(", ", ",");
                var result = (keyword_assoc_val.Contains(keyword_val + ",") || keyword_assoc_val.Contains(keyword_val));
                if (result == false)
                {
                    if (!String.IsNullOrEmpty(keyword_assoc_val))
                        keyword.AssociatedKeywords = keyword.Keyword + "," + keyword_assoc_val;
                    else
                        keyword.AssociatedKeywords = keyword.Keyword;
                    txtAssociatedKeywords.Text = keyword.AssociatedKeywords;
                }
                else
                    keyword.AssociatedKeywords = keyword_assoc_val;

                keyword.Definition = txtDefinition.Text;
                keyword.Available = rblAvailable.SelectedValue;
                keyword.LastModified = DateTime.Now;
                keyword.LastModifiedBy = user_id;
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    keyword.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);

                keyword.Update();
            }
            else
            {
                qPtl_Keyword keyword = new qPtl_Keyword();
                keyword.ScopeID = 1;
                keyword.Created = DateTime.Now;
                keyword.CreatedBy = user_id;
                keyword.LastModified = DateTime.Now;
                keyword.LastModifiedBy = user_id;
                keyword.Available = "Yes";
                keyword.MarkAsDelete = 0;
                keyword.Keyword = txtKeyword.Text;

                string keyword_val = txtKeyword.Text;
                string keyword_assoc_val = txtAssociatedKeywords.Text;
                keyword_assoc_val = keyword_assoc_val.Replace(", ", ",");
                var result = (keyword_assoc_val.Contains(keyword_val + ",") || keyword_assoc_val.Contains(keyword_val));
                if (result == false)
                {
                    if (!String.IsNullOrEmpty(keyword_assoc_val))
                        keyword.AssociatedKeywords = keyword.Keyword + "," + keyword_assoc_val;
                    else
                        keyword.AssociatedKeywords = keyword.Keyword;
                    txtAssociatedKeywords.Text = keyword.AssociatedKeywords;
                }
                else
                    keyword.AssociatedKeywords = keyword_assoc_val;

                keyword.Definition = txtDefinition.Text;
                keyword.Available = rblAvailable.SelectedValue;
                if (!String.IsNullOrEmpty(ddlTheme.SelectedValue))
                    keyword.ThemeID = Convert.ToInt32(ddlTheme.SelectedValue);
                keyword.Insert();

                keyword_id = keyword.KeywordID;
            }

            // redirect to page to add keyword + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["keywordID"]))
            {
                //lblMessage.Text = "*** Record Successfully Updated ***";
                //lblMessageBottom.Text = "*** Record Successfully Updated ***";
                Response.Redirect("keywords-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&keywordID=" + keyword_id);
                //Response.Redirect("keywords-list.aspx");
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
        keyword_id = Convert.ToInt32(Request.QueryString["keywordID"]);

        qPtl_Keyword keyword = new qPtl_Keyword(keyword_id);
        keyword.Available = "No";
        keyword.MarkAsDelete = 1;
        keyword.Update();

        Response.Redirect("keywords-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("keywords-list.aspx");
    }
}
