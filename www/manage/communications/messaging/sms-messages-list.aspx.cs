using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Social;

public partial class manage_communications_messaging_sms_messages_list : System.Web.UI.Page
{
    protected int campaign_id;

    public int CampaignID
    {
        get { return campaign_id; }
        set { campaign_id = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Convert.ToString(Request.QueryString["campaignID"])))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            siteSMSMessages.SelectCommand = "SELECT * FROM qCom_SMSMessages_View WHERE MarkAsDelete = 0 AND CampaignID=" + campaign_id + " ORDER BY CampaignID, DayInCampaign, MessageURI ASC";
        }
        else
            siteSMSMessages.SelectCommand = "SELECT * FROM qCom_SMSMessages_View WHERE MarkAsDelete = 0 ORDER BY CampaignID, DayInCampaign, MessageURI ASC";

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
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "TextMessages_run=" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    }

    protected void ddlCampaignList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("sms-messages-list.aspx?campaignID=" + ddlCampaigns.SelectedValue);
    }
}