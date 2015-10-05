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

public partial class qDbs_print_print_family_invitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int invitation_id = 0;
            invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);

            qPtl_Invitation invite = new qPtl_Invitation(invitation_id);
            int family_id = invite.FamilyID;

            var family_invites = qPtl_Invitation_View.GetInvitationsByFamilyID(family_id);

            string tableHTML = string.Empty;
            tableHTML =     "<table align=\"center\" width=\"350\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\" style=\"border:solid 1px #000; background:#CCC;\">";
            tableHTML +=    "<tr>";
            tableHTML +=    "<td colspan=\"2\" style=\"text-align: center; font-weight:bold; font-size:16px;\"><div style=\"height:10px;\"></div>Your Family Invitations<div style=\"height:10px;\"></div>";
            tableHTML +=    "</td><tr>";
            tableHTML +=    "<td align=\"center\"><b>Family Member</b></td><td align=\"center\"><b>Invitation Code</b></td></tr>";

            if (family_invites != null)
            {
                foreach (var i in family_invites)
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
            lblFamilyID.Text = "Family ID=" + invite.FamilyID;

            int scope_id = Convert.ToInt32(Context.Items["ScopeID"]);
            qPtl_InvitationTemplate template = qPtl_InvitationTemplate.GetTemplateByScopeID(scope_id);
            litHeader.Text = template.Header;
            litFooter.Text = template.Footer;
        }
    }
}