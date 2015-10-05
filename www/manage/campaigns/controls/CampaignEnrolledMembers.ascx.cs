using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.CMS;
using Quartz.Social;
using Quartz.Portal;
using Quartz.Report;

public partial class manage_campaigns_controls_CampaignEnrolledMembers : System.Web.UI.UserControl
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
        siteData.SelectCommand = "SELECT * FROM qSoc_UserCampaignLanguageInvitation_View WHERE Available='Yes' AND MarkAsDelete = 0 AND CampaignID=" + campaign_id + " ORDER BY CREATED DESC";        
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Campaign-Members_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }
}