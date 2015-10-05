using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Quartz;
using Quartz.Data;

public partial class qCom_create_email_event : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sqlCode = string.Empty;
            qDbs_SQLcode sql = new qDbs_SQLcode();

            // get emails
            sqlCode = "SELECT EmailID, URI FROM qCom_EmailItem WHERE Type <> 'Individual Email' AND ScopeID = " + Context.Items["ScopeID"] + " ORDER BY Subject ASC";

            DataTable dtEmails;
            dtEmails = sql.GetDataTable(sqlCode);

            ddlEmails.DataSource = dtEmails;
            ddlEmails.DataValueField = "EmailID";
            ddlEmails.DataTextField = "URI";
            ddlEmails.DataBind();

            // get searches
            sqlCode = "SELECT SearchID, SavedName FROM qDbs_Searches WHERE Saved = 'Yes' AND YesEmail = 'Yes' ORDER BY SavedName ASC";

            DataTable dtSearches;
            dtSearches = sql.GetDataTable(sqlCode);

            ddlSearches.DataSource = dtSearches;
            ddlSearches.DataValueField = "SearchID";
            ddlSearches.DataTextField = "SavedName";
            ddlSearches.DataBind();

            // get prior data if edit mode
            if (Request.QueryString["sendEventID"] != null && Request.QueryString["sendEventID"] != "")
            {
                sqlCode = "SELECT * FROM qCom_SendEvents WHERE SendEventID = " + Request.QueryString["sendEventID"];

                using (SqlDataReader eReader = sql.GetDataReader(sqlCode))
                {
                    eReader.Read();
                    ddlEmails.SelectedValue = Convert.ToString(eReader["EmailID"]);
                    ddlSearches.SelectedValue = Convert.ToString(eReader["SearchID"]);
                    DateTime startDate = new DateTime();
                    startDate = Convert.ToDateTime(eReader["StartDate"]);
                    datStart.SelectedDate = startDate;
                    rblRecurring.SelectedValue = Convert.ToString(eReader["Recurring"]);
                    rblRunning.SelectedValue = Convert.ToString(eReader["Running"]);
                    rblHeader.SelectedValue = Convert.ToString(eReader["IncludeHeader"]);
                    rblFooter.SelectedValue = Convert.ToString(eReader["IncludeFooter"]);
                    rblUnsubscribe.SelectedValue = Convert.ToString(eReader["IncludeUnsubscribe"]);
                    eReader.Close();
                }
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Write("just clicked");
        
        string sqlQuery = string.Empty;
        
        if (Request.QueryString["sendEventID"] != null && Request.QueryString["sendEventID"] != "")
        {
            sqlQuery = "UPDATE qCom_SendEvents SET [SearchID] = " + ddlSearches.SelectedValue + ", [EmailID] = " + ddlEmails.SelectedValue + ", [StartDate] = '" + datStart.SelectedDate + "',[Recurring] = '" + rblRecurring.SelectedItem + "',[Running] = '" + rblRunning.SelectedItem + "',[IncludeHeader] = '" + rblHeader.SelectedItem + "',[IncludeFooter] = '" + rblFooter.SelectedItem + "',[IncludeUnsubscribe] = '" + rblUnsubscribe.SelectedItem + "' WHERE SendEventID = " + Request.QueryString["sendEventID"];
        } else 
        {
            sqlQuery = "INSERT qCom_SendEvents ([ScopeID],[SearchID],[EmailID],[StartDate],[Recurring],[Running],[IncludeHeader],[IncludeFooter],[IncludeUnsubscribe]) VALUES (" + Context.Items["ScopeID"] + "," + ddlSearches.SelectedValue + " , " + ddlEmails.SelectedValue + " ,'" + datStart.SelectedDate + "','" + rblRecurring.SelectedItem + "','" + rblRunning.SelectedItem + "','" + rblHeader.SelectedItem + "','" + rblFooter.SelectedItem + "','" + rblUnsubscribe.SelectedItem + "')";
        }

        //Response.Write("sql code = " + sqlQuery);

        qDbs_SQLcode sql = new qDbs_SQLcode();
        sql.ExecuteSQL(sqlQuery);

        Response.Redirect("send-event-list.aspx");
    }
}
