using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Portal;
using Quartz.Communication;

public partial class text_messages_message_editor : System.Web.UI.UserControl
{
    public string final_text;    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //ddlPrograms.DataSource = qSoc_Campaign.GetGenerallyAvailableCampaigns();
            //ddlPrograms.DataTextField = "CampaignName";
            //ddlPrograms.DataValueField = "CampaignID";
            //ddlPrograms.DataBind();
        }
    }

    protected void btnSaveMessage_Click(object sender, EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

        }
    }
    protected void btnSendNow_Click(object sender, EventArgs e)
    {
        plhStep3Initial.Visible = false;
        plhStep3Details.Visible = true;
    }
    protected void btnSendLater_Click(object sender, EventArgs e)
    {
        plhStep3Initial.Visible = false;
        plhStep3Details.Visible = true;
    }
}