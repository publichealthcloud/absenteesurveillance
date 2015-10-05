using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Portal;
using Quartz.Learning;
using Quartz.Data;
using Quartz.Core;

public partial class manage_members_member_learning : System.Web.UI.Page
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
            lit5Class.Text = "";
            litTab1Class.Text = "class=\"tab-pane\"";
            litTab2Class.Text = "class=\"tab-pane\"";
            litTab3Class.Text = "class=\"tab-pane\"";
            litTab4Class.Text = "class=\"tab-pane\"";
            litTab5Class.Text = "class=\"tab-pane\"";
            if (curr_tab == "overview")
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab1Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "trainings")
            {
                lit2Class.Text = "class='active'";
                litTab2Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab2Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "assessments")
            {
                lit3Class.Text = "class='active'";
                litTab3Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab3Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "certificates")
            {
                lit4Class.Text = "class='active'";
                litTab4Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab4Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "tools")
            {
                lit5Class.Text = "class='active'";
                litTab5Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab5Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
            }

            // tab 2 - assigned trainings
            var u_trainings = qLrn_UserTraining_View.GetUserTrainings(profile_id);
            string u_training_html = string.Empty;

            if (u_trainings != null)
            {
                foreach (var t in u_trainings)
                {
                    u_training_html += "<strong>" + t.Title + "</strong><br>Status: " + t.Status + "<br><br>";
                }
                litUserTrainingList.Text = u_training_html;
            }
            else
                litUserTrainingList.Text = "No trainings assigned";

            // tab 3 - assessments
            userAssessments.SelectCommand = "SELECT * FROM qLrn_UserAssessments_View WHERE UserID = " + profile_id + " AND Available = 'Yes' AND MarkAsDelete = 0 AND Created > '" + System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"] + "' ORDER BY Created DESC";

            startDate = null;
            endDate = null;

            // tab 4 - certificates
            var certificates = qLrn_UserTrainingCertificates.GetUserTrainingCertificates(profile_id);

            if (certificates != null)
            {
                string cert_html = string.Empty;
                
                foreach (var c in certificates)
                {
                    if (c.UserTrainingCertificateID > 0)
                    {
                        qLrn_UserTraining_View training = new qLrn_UserTraining_View(c.UserTrainingID);

                        cert_html += "<li><i class=\"icon-download-alt\"></i>  <a href=\"/user_data/" + profile.UserName + "/" + c.FileName + "\"><strong>" + training.Title + "</strong></a> uploaded at: " + c.Created + "</li>";
                    }
                }

                if (!String.IsNullOrEmpty(cert_html))
                    litCertificates.Text = cert_html;
                else
                    litCertificates.Text = "No Certificates.";
            }
            else
            {
                litCertificates.Text = "No Certificates.";
            }

            // tab 5 - tools
            // TOOL: Add Trainings
            string sqlCode = string.Empty;
            qDbs_SQLcode sql = new qDbs_SQLcode();
            q_Helper helper = new q_Helper();

            // get roles
            sqlCode = "SELECT TrainingID, Title FROM qLrn_Trainings WHERE Available = 'Yes' ORDER BY TrainingID";

            DataTable dtTrainings;

            using (dtTrainings = sql.GetDataTable(sqlCode))
            {

                cblTrainings.DataSource = dtTrainings;
                cblTrainings.DataValueField = "TrainingID";
                cblTrainings.DataTextField = "Title";
                cblTrainings.DataBind();
            }

            // mark current trainings
            sqlCode = "SELECT TrainingID FROM qLrn_UserTrainings WHERE UserID = " + Request.QueryString["userID"];

            SqlDataReader rReader;

            int i = 0;
            using (rReader = sql.GetDataReader(sqlCode))
            {
                while (rReader.Read())
                {
                    ListItem currentCheckBox = cblTrainings.Items.FindByValue(rReader["TrainingID"].ToString());
                    if (currentCheckBox != null)
                    {
                        currentCheckBox.Selected = true;
                    }
                    i++;
                }
            }
            litNumTrainings.Text = "Member is enrolled in <strong>" + i + " trainings</strong>";

            // TOOL: Reset/Delete Trainings
            plhTrainings.Visible = false;
            plhResetOptions.Visible = false;
            btnProcessTrainings.Visible = false;
            dpkStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            txtDaysAvailable.Text = "4000";

            var u_assessments = qLrn_UserAssessment.GetUserAssessments(profile_id, "");
            int num_assessments = 0;
            if (u_assessments != null)
                num_assessments = u_assessments.Count;
            litNumAssessments.Text = "Member has taken <strong>" + num_assessments + " assessments</strong>";
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

    protected void btnProcessTrainings_Click(object sender, EventArgs e)
    {
        int n = 0;
        string selectedItems = string.Empty;
        string strMessage = string.Empty;

        int userID = Convert.ToInt32(Request.QueryString["userID"]);
        qPtl_User user = new qPtl_User(userID);

        int daysBetweenTrainings = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Learning_DaysBetweenTrainings"]);
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
        btnProcessTrainings.Visible = false;
    }

    protected void ddlAction_SelectedItemChanged(object sender, EventArgs e)
    {
        if (ddlAction.SelectedValue == "reset")
        {
            plhResetOptions.Visible = true;
            plhTrainings.Visible = true;
            populateTrainings();
            btnProcessTrainings.Visible = true;
        }
        else if (ddlAction.SelectedValue == "delete")
        {
            plhResetOptions.Visible = false;
            plhTrainings.Visible = true;
            populateTrainings();
            btnProcessTrainings.Visible = true;
        }
        else
        {
            plhResetOptions.Visible = false;
            plhTrainings.Visible = false;
            btnProcessTrainings.Visible = false;
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

    protected void btnAddTrainings_Click(object sender, EventArgs e)
    {
        // create records for all new roles
        int n;
        string selectedItems = string.Empty;

        n = 0;

        //int userID = Convert.ToInt32(37);
        int userID = Convert.ToInt32(Request.QueryString["userID"]);
        qPtl_User user = new qPtl_User(userID);

        foreach (ListItem item in cblTrainings.Items)
        {
            if (item.Selected)
            {
                DateTime initialDate = DateTime.Now;
                int numDays = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Learning_DaysBetweenTrainings"]) * n;
                TimeSpan duration = new System.TimeSpan(numDays, 0, 0, 0);
                TimeSpan duration2 = new System.TimeSpan(1000, 0, 0, 0);
                DateTime startDate = initialDate.Add(duration);
                DateTime endDate = initialDate.Add(duration2);

                // create training
                qLrn_UserTraining utraining = new qLrn_UserTraining();

                utraining.UserID = user.UserID;
                utraining.TrainingID = Convert.ToInt32(item.Value);
                utraining.ScopeID = user.ScopeID;
                utraining.Available = "Yes";
                utraining.AvailableToUser = "Yes";
                utraining.Status = "Not Started";
                utraining.StartAvailable = startDate;
                utraining.EndAvailable = endDate;
                utraining.Certificate = "No";
                utraining.InitialAssessmentRequired = "Yes";
                utraining.InitialAssessmentPassable = "Yes";
                utraining.InitialAssessmentMinimumProficiency = Convert.ToDouble(txtInitialAssessmentMinimumProficiency.Text);
                utraining.InitialAssessmentScore = 0;
                utraining.PostAssessmentRequired = "Yes";
                utraining.PostAssessmentMinimumProficiency = Convert.ToDouble(txtInitialAssessmentMinimumProficiency.Text);
                utraining.ProgressMode = Convert.ToString(ddlNavType.SelectedValue);

                utraining.Insert();

                n++;
            }

        }

        Response.Redirect("/manage/members/member-learning.aspx?userID=" + userID + "&currTab=tools&message=successfully added trainings");
    }
}