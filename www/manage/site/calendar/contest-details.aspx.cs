using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;
using Quartz.Portal;

public partial class manage_manage_contests : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_space_id = 0;
        if (Page.IsPostBack)
        {
            DropDownList space_list = (DropDownList) Master.FindControl("ddlSpaces");
            curr_space_id = Convert.ToInt32(space_list.SelectedValue);
        }
        else
        {
            curr_space_id = Convert.ToInt32(Session["manage_space_id"]);
        }

        int reference_id = Convert.ToInt32(Request.QueryString["contestID"]);
        loadControls(curr_space_id, reference_id);
    }
    
    protected void loadControls(int space_id, int reference_id)
    {
        string list_html = string.Empty;
        qSoc_Contest contest = new qSoc_Contest(reference_id);
        litSubtitle.Text = "Submissions for " + contest.Name;


        var list = qSoc_ContestEntry.GetContestEntriesByContest(reference_id);
        if (list != null)
        {
            foreach (var l in list)
            {
                qPtl_User user = new qPtl_User(l.UserID);

                string submission_html = "Submitted by " + user.UserName + " at: " + l.Created;
                if (l.ContentTypeID == (int)qSoc_ContentType.Types.Picture)
                {
                    qSoc_Image image = new qSoc_Image(l.ReferenceID);
                    submission_html += "<br><a href=\"/user_data/" + user.UserName + "/" + image.FileName +"\" target=\"_blank\"><img src=\"/user_data/" + user.UserName + "/" + image.FileName + ".ashx?maxwidth=400\"><br>Click to view full size in a new tab/window</a>";
                }
                
                list_html += "<li>" + submission_html + "</li>";
            }

            litSubmissionList.Text = list_html;
        }
    }
}