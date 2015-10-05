using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Social;
using Quartz.Communication;
using Quartz.Portal;

public partial class manage_communications_messaging_controls_SMSMessageLog : System.Web.UI.UserControl
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }

    /*
    protected void Page_Init(object sender, EventArgs e)
    {
        ICollection<qCom_SMSMessageLog> logs;
        
        if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["campaignID"])))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            logs = qCom_SMSMessageLog.GetSMSMessageLogsByCampaign(campaign_id);
        }
        else
        {
            logs = qCom_SMSMessageLog.GetSMSMessageLogs();
        }

        // build datatable
        if (logs != null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SMSMessageLogID", typeof(int));
            dt.Columns.Add("Timestamp", typeof(DateTime));
            dt.Columns.Add("MessageURI", typeof(string));
            dt.Columns.Add("MobilePhoneNumber", typeof(string));
            dt.Columns.Add("MessageText", typeof(string));
            dt.Columns.Add("DID", typeof(string));
            dt.Columns.Add("Direction", typeof(string));
            dt.Columns.Add("CampaignID", typeof(int));
            dt.Columns.Add("CampaignName", typeof(string));
            dt.Columns.Add("LanguageID", typeof(int));
            dt.Columns.Add("Language", typeof(string));
            dt.Columns.Add("UserID", typeof(int));
            
            foreach (var l in logs)
            {
                string curr_language = string.Empty;
                string curr_campaign = string.Empty;
                int curr_campaign_id = 0;
                int curr_user_id = 0;
                string curr_username = string.Empty;

                if (campaign_id > 0)
                    curr_campaign_id = campaign_id;

                if (l.LanguageID == 1)
                    curr_language = "English";
                else if (l.LanguageID == 2)
                    curr_language = "Spanish";

                if (l.UserID == 0)
                {
                    // try and match using the phone number
                    var profile = qPtl_UserProfile.GetProfileByMobileNumber(l.MobilePhoneNumber);

                    if (profile != null)
                        curr_user_id = profile.UserID;
                }

                dt.Rows.Add(l.SMSMessageLogID, l.Timestamp, l.MessageURI, l.MobilePhoneNumber, l.MessageText, l.DID, l.Direction, curr_campaign_id, curr_campaign, l.LanguageID, curr_language, curr_user_id);
            }

            gridSMSmessageLog.DataSource = dt;
            gridSMSmessageLog.DataBind();
         
        }
    }
    */

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["campaignID"])))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            siteSMSLog.SelectCommand = "SELECT * FROM qCom_SMSMessageLogProfile_View WHERE CampaignID=" + campaign_id + " ORDER BY Timestamp DESC";
        }
        else
            siteSMSLog.SelectCommand = "SELECT * FROM qCom_SMSMessageLogProfile_View ORDER BY Timestamp DESC";        
        
        if (!Page.IsPostBack)
        {
            populateCampaigns();
            ddlCampaigns.SelectedValue = Convert.ToString(campaign_id);
        }

        lblTitle.Text = "Text Messages (SMS Messages)";
    }

    protected void populateCampaigns()
    {
        ddlCampaigns.DataSource = qSoc_Campaign.GetCampaigns();
        ddlCampaigns.DataTextField = "CampaignName";
        ddlCampaigns.DataValueField = "CampaignID";
        ddlCampaigns.DataBind();
        ddlCampaigns.Items.Insert(0, new ListItem("", string.Empty));
        ddlCampaigns.Items.Insert(1, new ListItem("<All Campaigns>", string.Empty));
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.FilterCommandName)
        {
            Pair filterPair = (Pair)e.CommandArgument;

            switch (filterPair.Second.ToString())
            {
                case "Created":
                    this.startDate = ((e.Item as GridFilteringItem)[filterPair.Second.ToString()].FindControl("FromDatePicker") as RadDatePicker).SelectedDate;
                    this.endDate = ((e.Item as GridFilteringItem)[filterPair.Second.ToString()].FindControl("ToDatePicker") as RadDatePicker).SelectedDate;
                    break;
                default:
                    break;
            }
        }
    }

    protected DateTime? startDate
    {
        set
        {
            ViewState["strD"] = value;
        }
        get
        {
            if (ViewState["strD"] != null)
                return (DateTime)ViewState["strD"];
            else
            {
                DateTime? beginningDate = new DateTime();
                beginningDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
                ViewState["strD"] = beginningDate;
                return beginningDate;
            }
        }
    }
    protected DateTime? endDate
    {
        set
        {
            ViewState["endD"] = value;
        }
        get
        {
            if (ViewState["endD"] != null)
                return (DateTime)ViewState["endD"];
            else
            {
                return DateTime.Now.AddDays(1);
            }
        }
    }
    protected DateTime? minDate
    {
        set
        {
            DateTime? minDate = new DateTime();
            minDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
        }
        get
        {
            DateTime? minDate = new DateTime();
            minDate = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);
            return minDate;
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        gridSMSmessageLog.ExportSettings.ExportOnlyData = true;
        gridSMSmessageLog.ExportSettings.IgnorePaging = true;
        gridSMSmessageLog.ExportSettings.OpenInNewWindow = true;
        gridSMSmessageLog.ExportSettings.FileName = "TextMessages_run=" + DateTime.Now;
        gridSMSmessageLog.MasterTableView.ExportToExcel();
    }

    protected void ddlCampaignList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("sms-message-log.aspx?campaignID=" + ddlCampaigns.SelectedValue);
    }
}