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

public partial class edit_redirect : System.Web.UI.Page
{
    public int redirect_id;
    public string base_path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["redirectID"]))
            {
                redirect_id = Convert.ToInt32(Request.QueryString["redirectID"]);

                qPtl_Redirect redirect = new qPtl_Redirect(redirect_id);

                lblTitle.Text = "Edit Server Redirect (ID: " + redirect.RedirectID + ")";
                string raw_watch_for = redirect.EntryURL;
                string watch_for_trimmed = raw_watch_for.Substring(1, raw_watch_for.Length - 2);
                txtWatchFor.Text = watch_for_trimmed;
                txtURL.Text = redirect.RedirectURL;
                rblAvailable.SelectedValue = redirect.Available;

                if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
                {
                    lblMessage.Text = "*** Record Successfully Added ***";
                }
            }

            else
            {
                lblTitle.Text = "New Server Redirect";
                btnDelete.Visible = false;
                rblAvailable.SelectedValue = "Yes";
                plhTools.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["redirectID"]))
            {
                redirect_id = Convert.ToInt32(Request.QueryString["redirectID"]);
                qPtl_Redirect redirect = new qPtl_Redirect(redirect_id);
                redirect.EntryURL = "/" + txtWatchFor.Text + "/";
                redirect.RedirectURL = txtURL.Text;
                redirect.Available = rblAvailable.SelectedValue;
                redirect.Update();
            }
            else
            {
                qPtl_Redirect redirect = new qPtl_Redirect();
                redirect.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                redirect.Created = DateTime.Now;
                redirect.CreatedBy = user_id;
                redirect.LastModified = DateTime.Now;
                redirect.LastModifiedBy = user_id;
                redirect.MarkAsDelete = 0;
                redirect.Available = rblAvailable.SelectedValue;
                redirect.EntryURL = "/" + txtWatchFor.Text + "/";
                redirect.RedirectURL = txtURL.Text;
                redirect.Insert();

                redirect_id = redirect.RedirectID;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["redirectID"]))
            {
                lblMessage.Text = "*** Record Successfully Updated ***";
                lblMessageBottom.Text = "*** Record Successfully Updated ***";
                if (Request.QueryString["edit-mode"] == "in-place")
                    Response.Redirect(Request.QueryString["returnURL"]);
                else
                    Response.Redirect("redirects-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&redirectID=" + redirect_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        redirect_id = Convert.ToInt32(Request.QueryString["redirectID"]);
        qPtl_Redirect redirect = new qPtl_Redirect(redirect_id);
        redirect.DeleteRedirect(redirect.RedirectID);

        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("redirects-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("redirects-list.aspx");
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["edit-mode"] == "in-place")
            Response.Redirect(Request.QueryString["returnURL"]);
        else
            Response.Redirect("redirects-list.aspx");
    }
}
