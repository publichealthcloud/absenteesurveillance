using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_campaigns_reports_controls_AvailableCampaignReports : System.Web.UI.UserControl
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int reference_id = Convert.ToInt32(Request.QueryString["campaignID"]);
        campaign_id = reference_id;
    }
}