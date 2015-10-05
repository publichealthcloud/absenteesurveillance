using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.CMS;
using Quartz.Social;

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
        loadPageInfo(curr_space_id);
    }

    protected void loadPageInfo(int space_id)
    {
        int contest_id = (int)qSoc_ContentType.Types.Contest;
        string list_html = string.Empty;
        if (space_id > 0)
        {
            var list = qSoc_SpaceAssociation.GetSpaceAssociationsByContentTypeAndSpace(contest_id, space_id);
            if (list != null)
            {
                foreach (var l in list)
                {
                    qSoc_Contest contest = new qSoc_Contest(l.ReferenceID);
                    list_html += "<li><a href=\"contest-details.aspx?contestID=" + contest.ContestID + "\">" + contest.Name + " Submission Date: " + contest.StartVoteDateTime + "</a></li>";
                }

                litContestList.Text = list_html;
            }
        }
        else
        {
            var list = qSoc_SpaceAssociation.GetSpaceAssociationsByContentType(contest_id);
            if (list != null)
            {
                foreach (var l in list)
                {
                    qSoc_Contest contest = new qSoc_Contest(l.ReferenceID);
                    list_html += "<li><a href=\"contest-details.aspx?contestID=" + contest.ContestID + "\">" + contest.Name + " Submission Date: " + contest.StartVoteDateTime + "</a></li>";
                }

                litContestList.Text = list_html;
            }
        }

    }
}