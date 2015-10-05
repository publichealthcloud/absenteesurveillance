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
using Quartz.Portal;
using Quartz.Learning;
using Quartz.Communication;

public partial class qLrn_manage_trainings : System.Web.UI.Page
{
    public int TrainingID
    {
        get { return Convert.ToInt32(ViewState["training_id"]); }
        set { ViewState["training_id"] = value; }
    }
    public int UserID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserID = Convert.ToInt32(Request.QueryString["userID"]);
            qPtl_User user = new qPtl_User(UserID);
            lblLearner.Text = user.FirstName + " " + user.LastName;

            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {
                TrainingID = Convert.ToInt32(Request.QueryString["trainingID"]);
                plhSelectTraining.Visible = true;
                plhManage.Visible = true;
                hplResetTrainings.NavigateUrl = "reset-trainings.aspx?userID=" + Request.QueryString["userID"];
                PopulateTrainings(UserID);
                ddlTrainings.SelectedValue = Convert.ToString(TrainingID);
                DisplayManageTools(UserID, TrainingID);
            }
            else
            {
                plhSelectTraining.Visible = true;
                plhManage.Visible = false;
                plhInPersonTraining.Visible = false;
                hplResetTrainings.NavigateUrl = "~/reset-trainings.aspx?userID=" + Request.QueryString["userID"];
                PopulateTrainings(UserID);
                plhInPersonTraining.Visible = false;
                plhManage.Visible = false;
            }
        }
    }

    protected void ddlTrainings_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlTrainings.SelectedValue))
        {
            TrainingID = Convert.ToInt32(ddlTrainings.SelectedValue);
            DisplayManageTools(Convert.ToInt32(Request.QueryString["userID"]), TrainingID);
        }
    }

    protected void DisplayManageTools(int user_id, int training_id)
    {
        qLrn_UserTraining userTraining = qLrn_UserTraining.GetUserTraining(user_id, training_id);
        TrainingID = userTraining.TrainingID;

        if (userTraining != null && userTraining.UserTrainingID > 0)
        {
            plhManage.Visible = true;
            qLrn_Training training = new qLrn_Training(userTraining.TrainingID);

            if (training.TrainingTypeID == 3)
            {
                plhInPersonTraining.Visible = true;            
                dpkCompletedDate.SelectedDate = userTraining.Completed;
                ddlStatus.SelectedValue = userTraining.Status;

                qPtl_UserProfile u_profile = new qPtl_UserProfile(user_id);
                if (u_profile.Phone1Type == "Work")
                    txtPhone.Text = u_profile.Phone1;
                else
                    txtPhone.Text = u_profile.Phone2;

                populateCECredits(TrainingID);
                ddlCredits.SelectedValue = userTraining.ApplyingForCECredits;
                if (userTraining.CECreditID > 0)
                    ddlSelectCredit.SelectedValue = Convert.ToString(userTraining.CECreditID);
                rblPaymentOption.SelectedValue = userTraining.PaymentMethod;
            }
            else
                plhInPersonTraining.Visible = false;

        }
    }

    protected void populateCECredits(int trainingID)
    {
        ddlSelectCredit.DataSource = qLrn_TrainingCredit_View.GetTrainingCredits(trainingID);
        ddlSelectCredit.DataTextField = "Name";
        ddlSelectCredit.DataValueField = "CreditID";
        ddlSelectCredit.DataBind();
        ddlSelectCredit.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int user_id = Convert.ToInt32(Request.QueryString["userID"]);
        int training_id = Convert.ToInt32(ddlTrainings.SelectedValue);

        qLrn_UserTraining u_training = qLrn_UserTraining.GetUserTraining(user_id, training_id);
        qPtl_UserProfile u_profile = new qPtl_UserProfile(user_id);
        qLrn_Training training = new qLrn_Training(training_id);

        u_profile.Phone1 = txtPhone.Text;
        u_profile.Phone1Type = "Work";
        u_profile.LastModified = DateTime.Now;
        u_profile.Update();

        // if changing from waitlist then display option for email notification
        if (u_training.Status.Contains("Waitlist") && !Convert.ToString(ddlStatus.SelectedValue).Contains("Waitlist"))
            btnSendWaitlistEmail.Visible = true;
        else
            btnSendWaitlistEmail.Visible = false;

        u_training.Status = Convert.ToString(ddlStatus.SelectedValue);
        u_training.PaymentMethod = rblPaymentOption.SelectedValue;
        u_training.ApplyingForCECredits = ddlCredits.SelectedValue;
        string evalDate = Convert.ToString(dpkCompletedDate.SelectedDate);
        DateTime newCompletedDate = new DateTime();

        if (training.TrainingTypeID == 3 && ddlStatus.SelectedValue == "Completed" && !String.IsNullOrEmpty(evalDate))
            if (DateTime.TryParse(evalDate, out newCompletedDate))
                u_training.Completed = newCompletedDate;
        else if (training.TrainingTypeID == 3 && ddlStatus.SelectedValue == "Completed")
            u_training.Completed = training.EndTime;
        else
            u_training.Completed = null;        

        if (!String.IsNullOrEmpty(ddlSelectCredit.SelectedValue))
            u_training.CECreditID = Convert.ToInt32(ddlSelectCredit.SelectedValue);
        else
            u_training.CECreditID = 0;

        u_training.Update();

        lblMessage.Text = " * Training information successfully updated";
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        Response.Redirect("reset-trainings.aspx?userID=" + Request.QueryString["userID"]);
    }

    protected void PopulateTrainings(int userID)
    {
        ddlTrainings.DataSource = qLrn_UserTraining_View.GetUserTrainings(userID);
        ddlTrainings.DataValueField = "TrainingID";
        ddlTrainings.DataTextField = "Title";
        ddlTrainings.DataBind();
        ddlTrainings.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // delete training --> will actually delete the user training record
        int user_id = Convert.ToInt32(Request.QueryString["userID"]);

        qLrn_UserTraining.Delete(user_id, TrainingID);

        lblMessage.Text = "User training has been deleted";
        btnSave.Visible = false;
        hplResetTrainings.Visible = false;
    }

    protected void btnSendWaitlistEmail_Click(object sender, EventArgs e)
    {
        int user_id = Convert.ToInt32(Request.QueryString["userID"]);
        int training_id = Convert.ToInt32(Request.QueryString["trainingID"]);

        qPtl_User user = new qPtl_User(user_id);
        qLrn_Training training = new qLrn_Training(training_id);

        if (user.UserID > 0)
        {
            if (!String.IsNullOrEmpty(user.Email))
            {
                int email_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["email_RemovedWaitlistID"]);

                qCom_EmailTool email = new qCom_EmailTool(email_id);

                int sent_email_log_id = email.SendDatabaseMail(user.Email, email_id, user.UserID, user.FirstName, training.Title, Convert.ToString(training.TrainingID), "", "", false);

                if (sent_email_log_id > 0)
                {
                    lblMessage.Text = "* Email Successfully Sent";
                    btnSendWaitlistEmail.Visible = false;
                }
                else
                    lblMessage.Text += "** An error occured sending message for: " + user.Email + "<br><br>";
            } 
            else 
            {
                lblMessage.Text += "** This user does not have an email on record -- welcome email cannot be sent ***<br><br>";
            }
        }
    }
}
