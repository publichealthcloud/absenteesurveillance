using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Learning;
using Quartz.Portal;

public partial class manage_viewer_campaign_activity : System.Web.UI.Page
{
    protected int user_id, campaign_id;

    public int UserID
    {
        get { return user_id; }
        set { user_id = value; }
    }
    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        int user_id = Convert.ToInt32(Request.QueryString["userID"]);
        int campaign_id = Convert.ToInt32(Context.Items["campaignID"]);

        if (user_id > 0 && campaign_id > 0)
        {
            
        }
    }
}