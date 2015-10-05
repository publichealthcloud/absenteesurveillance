using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;

public partial class controls_community_rules : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            qSoc_Element html = new qSoc_Element("rules");

            if (html != null)
            {
                if (html.ElementID > 0)
                    litCommunityRules.Text = html.HTML;
                else
                    litCommunityRules.Text = "Sorry, we hare having a problem loading the community rules.";
            }
            else
            {
                litCommunityRules.Text = "Sorry, we hare having a problem loading the community rules.";
            }
        }
    }

    protected void btnAgreeRules_Click(object sender, EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        qPtl_UserProfile profile = new qPtl_UserProfile(user_id);
        profile.AgreeRules = DateTime.Now;
        profile.Update();

        // see if this is the action page -- if so, update actionID
        int action_id = 0;
        if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["actionID"])))
        {
            action_id = Convert.ToInt32(Request.QueryString["actionID"]);
            qPtl_UserAction action = new qPtl_UserAction(user_id, action_id);
            action.LastModified = DateTime.Now;
            action.LastModifiedBy = user_id;
            action.UserCompleted = DateTime.Now;
            action.Update();

            Response.Redirect(action.RedirectURL);
        }
    }

    protected void btnDoNotAgree_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(lblMsg.Text))
            lblMsg.Text = "NOTE: You must agree to the rules to be able to enter T2X<br><br>";
        else
            Response.Redirect("~/logout.aspx");

    }
}