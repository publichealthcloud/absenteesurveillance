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
using Quartz.Core;
using Quartz.Portal;
using Quartz.Learning;
using Quartz.Data;

public partial class qLrn_reset_trainings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            plhTrainings.Visible = false;
            plhResetOptions.Visible = false;
            btnReload.Visible = false;
            btnSave.Visible = false;
            dpkStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            txtDaysAvailable.Text = "1000";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int n = 0;
        string selectedItems = string.Empty;
        string strMessage = string.Empty;

        int userID = Convert.ToInt32(Request.QueryString["userID"]);
        qPtl_User user = new qPtl_User(userID);

        int daysBetweenTrainings = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["learning_daysbetweentrainings"]);
        int daysTillUnavailable = 0;
        DateTime seedDate = DateTime.Now;
        string action = ddlAction.SelectedValue;
        string trainingMode = "controlled";
        string surveyRequired = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Learning_SurveyRequired"]);
        if (action == "reset")
        {
            daysTillUnavailable = Convert.ToInt32(txtDaysAvailable.Text);
            seedDate = Convert.ToDateTime(dpkStartDate.SelectedDate);
            trainingMode = Convert.ToString(ddlTrainingMode.SelectedValue);
        }

        foreach (ListItem item in cblUserTrainings.Items)
        {
            if (item.Selected)
            {
                if (action == "reset")
                    strMessage += qLrn_UserTraining.manageUserTrainings(userID, daysBetweenTrainings, daysTillUnavailable, trainingMode, action, Convert.ToInt32(item.Value), seedDate, surveyRequired);
                else if (action == "delete")
                {
                    qLrn_UserTraining.Delete(userID, Convert.ToInt32(item.Value));
                    strMessage += "*** Following Training was deleted: " + item.Text + "<br>";
                }

                n++;
            }
        }

        lblMessage.Text = "*** RESULTS ***<br><br>" + strMessage;
        plhAction.Visible = false;
        plhTrainings.Visible = false;
        plhResetOptions.Visible = false;
        btnReload.Visible = true;
        btnSave.Visible = false;
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        Response.Redirect("reset-trainings.aspx?userID=" + Request.QueryString["userID"]);
    }

    protected void ddlAction_SelectedItemChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "reset")
        {
            plhResetOptions.Visible = true;
            plhTrainings.Visible = true;
            populateTrainings();
            btnSave.Visible = true;
        }
        else if (ddlAction.SelectedValue == "delete")
        {
            plhResetOptions.Visible = false;
            plhTrainings.Visible = true;
            populateTrainings();
            btnSave.Visible = true;
        }
        else
        {
            plhResetOptions.Visible = false;
            plhTrainings.Visible = false;
            btnSave.Visible = false;
        }

    }

    protected void populateTrainings()
    {
        int userID = Convert.ToInt32(Request.QueryString["userID"]);
        string sqlCode = string.Empty;
        qDbs_SQLcode sql = new qDbs_SQLcode();
        q_Helper helper = new q_Helper();

        // get roles
        sqlCode = "SELECT TrainingID, Title FROM qLrn_UserTrainings_View WHERE Available = 'Yes' AND UserID = " + userID + " ORDER BY Title ASC";

        DataTable dtRoles;

        using (dtRoles = sql.GetDataTable(sqlCode))
        {
            cblUserTrainings.DataSource = dtRoles;
            cblUserTrainings.DataValueField = "TrainingID";
            cblUserTrainings.DataTextField = "Title";
            cblUserTrainings.DataBind();
        }
    }
}
