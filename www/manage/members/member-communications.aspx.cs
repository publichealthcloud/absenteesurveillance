using System;
using System.Collections;
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
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Data;
using Quartz.Help;
using Quartz.Core;
using Quartz.Organization;
using Quartz.Communication;

public partial class manage_members_member_communications : System.Web.UI.Page
{
    protected int profile_id;
    protected string username;
    protected string required_indicator;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            profile_id = Convert.ToInt32(Request.QueryString["userID"]);

            qPtl_User profile = new qPtl_User(profile_id);
            username = profile.UserName;

            string curr_tab = string.Empty;
            curr_tab = Request.QueryString["currTab"];
            lit1Class.Text = "";
            lit2Class.Text = "";
            lit3Class.Text = "";
            lit4Class.Text = "";
            litTab1Class.Text = "class=\"tab-pane\"";
            litTab2Class.Text = "class=\"tab-pane\"";
            litTab3Class.Text = "class=\"tab-pane\"";
            litTab4Class.Text = "class=\"tab-pane\"";
            if (curr_tab == "1")
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab1Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "2")
            {
                lit2Class.Text = "class='active'";
                litTab2Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab2Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "3")
            {
                lit3Class.Text = "class='active'";
                litTab3Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab3Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "4")
            {
                lit4Class.Text = "class='active'";
                litTab4Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab4Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
            }

            siteEmailLog.SelectCommand = "SELECT * FROM qCom_EmailLogs_View WHERE EmailAddress = '" + profile.Email + "' ORDER BY EmailLogID DESC";

            var email_logs = qCom_EmailLog.GetEmailLogsByUserID(profile.UserID);
            int num_emails = 0;
            if (email_logs != null)
                num_emails = email_logs.Count;
            litNumEmails.Text = "Member has been sent <strong>" + num_emails + " emails</strong>";
        }
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
}