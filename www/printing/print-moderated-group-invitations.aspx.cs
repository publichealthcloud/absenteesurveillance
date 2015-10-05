using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Data;
using Quartz.Portal;
using Quartz.Social;

public partial class qDbs_print_print_moderated_group_invitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int invitation_id = 0;
            invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);

            qPtl_Invitation invite = new qPtl_Invitation(invitation_id);
            int space_id = invite.SpaceID;

            qSoc_Space space = new qSoc_Space(space_id);

            var group_invites = qPtl_Invitation_View.GetInvitationsBySpaceID(space_id);

            string tableHTML = string.Empty;
            tableHTML =     "<table align=\"center\" width=\"650\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\" style=\"border:solid 1px #000; background:#CCC;\">";
            tableHTML +=    "<tr>";
            tableHTML +=    "<td colspan=\"2\" style=\"text-align: center; font-weight:bold; font-size:16px;\"><div style=\"height:10px;\"></div>Group: " + space.SpaceShortName + "<div style=\"height:10px;\"></div>";
            tableHTML +=    "</td><tr>";
            tableHTML +=    "<td align=\"center\"><b>Group Members</b></td><td align=\"center\"><b>Invitation Code</b></td></tr>";

            if (group_invites != null)
            {
                foreach (var i in group_invites)
                {
                    tableHTML += "<tr><td align=\"center\">";
                    tableHTML += Convert.ToString(i.RoleName);
                    tableHTML += "</td><td align=\"center\">";
                    tableHTML += Convert.ToString(i.InviteCode);
                    tableHTML += "</td><td align=\"center\">";
                }
            }

            tableHTML += "<br /><div style=\"height:10px;\"></div></td></tr></table>";

            litInvitationHTML.Text = tableHTML;
            lblSpaceID.Text = "Space ID=" + invite.SpaceID;

            int scope_id = Convert.ToInt32(Context.Items["ScopeID"]);
            qPtl_InvitationTemplate template = qPtl_InvitationTemplate.GetTemplateByScopeID(scope_id);
            litHeader.Text = template.Header;
            litFooter.Text = template.Footer;
        }
    }
}