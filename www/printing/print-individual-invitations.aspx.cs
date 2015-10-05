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

public partial class qDbs_print_print_individual_invitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // get invitation information
            int invitation_id = 0;
            invitation_id = Convert.ToInt32(Request.QueryString["invitationID"]);

            qPtl_Invitation invite = new qPtl_Invitation(invitation_id);

            string audience_name = string.Empty;
            string code = string.Empty;

            if (invitation_id > 0)
            {
                audience_name = Convert.ToString(invite.InvitationAudienceName);
                code = Convert.ToString(invite.InviteCode);
            }
            else
            {
                audience_name = "Example Group";
                code = "1234";
            }
            
            string tableHTML = string.Empty;
            tableHTML = "<table align=\"center\" width=\"350\" border=\"0\" cellspacing=\"0\" cellpadding=\"15\" style=\"border:solid 1px #000; background:#CCC;\">";
            tableHTML += "<tr>";
            tableHTML += "<td colspan=\"2\" style=\"text-align: center; font-weight:bold; font-size:16px;\"><div style=\"height:15px;\"></div>Your Invitation";
            tableHTML += "<br /><b>" + audience_name + "</b><br /><br /><b>Invitation Code</b><br>";
            tableHTML += code + "<br />";
            tableHTML += "<br /></td></tr></table>";

            litInvitationHTML.Text = tableHTML;
            lblInvitationID.Text = Request.QueryString["invitationID"];

            int scope_id = Convert.ToInt32(Context.Items["ScopeID"]);
            qPtl_InvitationTemplate template = qPtl_InvitationTemplate.GetTemplateByScopeID(scope_id);
            litHeader.Text = template.Header;
            litFooter.Text = template.Footer;
        }
    }
}