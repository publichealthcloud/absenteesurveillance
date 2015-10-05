using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;

public partial class manage_members_controls_MemberNav : System.Web.UI.UserControl
{
    protected int profile_id;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            profile_id = Convert.ToInt32(Request.QueryString["userID"]);

            qPtl_User profile = new qPtl_User(profile_id);

            string img_url = string.Empty;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

            if (!String.IsNullOrEmpty(profile.ProfilePict))
                img_url = baseUrl + "user_data/" + profile.UserName + "/" + profile.ProfilePict + ".ashx?width=40&height=40&mode=crop";
            else
                img_url = baseUrl + "images/mylife_portrait_default.jpg.ashx?width=40&height=40&mode=crop";

            string curr_url = Request.Url.ToString();
            string page_title = string.Empty;

            litInfoCss.Text = "class=\"btn\"";
            litContentCss.Text = "class=\"btn\"";
            litCommunicationsCss.Text = "class=\"btn\"";
            litAdminToolsCss.Text = "class=\"btn\"";
            litLearningCss.Text = "class=\"btn\"";

            page_title = "<img src=\"" + img_url + "\" />" + " <strong>" + profile.UserName + "</strong> ";

            if (profile.AccountStatus == "Active")
                page_title += "&nbsp;<span class=\"label label-satgreen\">Active</span>";
            else
                page_title += "&nbsp;<span class=\"label label-lightred\">Inactive</span>";

            if (curr_url.Contains("member-profile.asp"))
            {
                page_title += " - Basic Info";
                litInfoCss.Text = "class=\"btn btn-inverse\"";
            }
            else if (curr_url.Contains("member-content.aspx"))
            {
                page_title += " - Warnings";
                litContentCss.Text = "class=\"btn btn-inverse\"";
            }
            else if (curr_url.Contains("member-learning.aspx"))
            {
                page_title += " - Learning";
                litLearningCss.Text = "class=\"btn btn-inverse\"";
            }
            else if (curr_url.Contains("member-communications.aspx"))
            {
                page_title += " - Communications";
                litCommunicationsCss.Text = "class=\"btn btn-inverse\"";
            }
            else if (curr_url.Contains("member-admin-tools.aspx"))
            {
                page_title += " - Admin Tools";
                litAdminToolsCss.Text = "class=\"btn btn-inverse\"";
            }

            litTitleInfo.Text = page_title;
        }
    }
}