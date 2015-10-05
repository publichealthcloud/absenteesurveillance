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

public partial class edit_author : System.Web.UI.Page
{
    public int author_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_ThemesFolder"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["authorID"]))
            {
                author_id = Convert.ToInt32(Request.QueryString["authorID"]);

                qLrn_Author author = new qLrn_Author(author_id);

                lblTitle.Text = "Edit Author (ID: " + author.AuthorID + ")";
                txtName.Text = author.AuthorName;
                rblAvailable.SelectedValue = author.Available;
            }
            else
            {
                lblTitle.Text = "New Author";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);

        if (!String.IsNullOrEmpty(Request.QueryString["authorID"]))
        {
            author_id = Convert.ToInt32(Request.QueryString["authorID"]);
            qLrn_Author author = new qLrn_Author(author_id);

            author.AuthorName = txtName.Text;
            author.Available = rblAvailable.SelectedValue;
            author.Update();
        }
        else
        {
            author_id = createNewAuthor(user_id);
        }
        
        Response.Redirect("authors-list.aspx");
    }

    protected int createNewAuthor(int user_id)
    {
        int new_author_id = 0;

        qLrn_Author author = new qLrn_Author();

        author.ScopeID = 1;
        author.Created = DateTime.Now;
        author.CreatedBy = user_id;
        author.LastModified = DateTime.Now;
        author.LastModifiedBy = user_id;
        author.Available = "Yes";
        author.MarkAsDelete = 0;
        author.AuthorName = txtName.Text;
        author.Available = rblAvailable.SelectedValue;
        author.Insert();
        new_author_id = author.AuthorID;

        return new_author_id;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        author_id = Convert.ToInt32(Request.QueryString["AuthorID"]);

        qLrn_Author author = new qLrn_Author(author_id);
        author.Available = "No";
        author.MarkAsDelete = 1;
        author.Update();

        Response.Redirect("authors-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("authors-list.aspx");
    }
}
