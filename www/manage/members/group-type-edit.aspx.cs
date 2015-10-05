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

public partial class edit_group_type : System.Web.UI.Page
{
    public int space_category_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            if (!String.IsNullOrEmpty(Request.QueryString["spaceCategoryID"]))
            {
                space_category_id = Convert.ToInt32(Request.QueryString["spaceCategoryID"]);

                qSoc_SpaceCategory category = new qSoc_SpaceCategory(space_category_id);

                lblTitle.Text = "Edit Group Type (ID: " + category.SpaceCategoryID + ")";
                txtSpaceCategory.Text = category.CatgoryName;
                txtDescription.Text = category.CategoryDescription;
                rblAvailable.SelectedValue = category.Available;
            }
            else
            {
                lblTitle.Text = "New group type";
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

            if (!String.IsNullOrEmpty(Request.QueryString["spaceCategoryID"]))
            {
                space_category_id = Convert.ToInt32(Request.QueryString["spaceCategoryID"]);
                qSoc_SpaceCategory category = new qSoc_SpaceCategory(space_category_id);

                category.CatgoryName = txtSpaceCategory.Text;
                category.CategoryDescription = txtDescription.Text;
                category.Available = rblAvailable.SelectedValue;
                category.LastModified = DateTime.Now;
                category.LastModifiedBy = user_id;
                category.Update();
            }
            else
            {
                qSoc_SpaceCategory category = new qSoc_SpaceCategory();
                category.ScopeID = 1;
                category.Created = DateTime.Now;
                category.CreatedBy = user_id;
                category.LastModified = DateTime.Now;
                category.LastModifiedBy = user_id;
                category.Available = "Yes";
                category.MarkAsDelete = 0;
                category.CatgoryName = txtSpaceCategory.Text;
                category.CategoryDescription = txtDescription.Text;
                category.Available = rblAvailable.SelectedValue;
                category.Insert();

                space_category_id = category.SpaceCategoryID;
            }

            // redirect to page to add keyword + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["spaceCategoryID"]))
            {
                //lblMessage.Text = "*** Record Successfully Updated ***";
                //lblMessageBottom.Text = "*** Record Successfully Updated ***";
                Response.Redirect("group-types-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&spaceCategoryID=" + space_category_id);
                //Response.Redirect("keywords-list.aspx");
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        space_category_id = Convert.ToInt32(Request.QueryString["spaceCategoryID"]);

        qSoc_SpaceCategory category = new qSoc_SpaceCategory(space_category_id);
        category.Available = "No";
        category.MarkAsDelete = 1;
        category.Update();

        Response.Redirect("group-types-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("group-types-list.aspx");
    }
}
