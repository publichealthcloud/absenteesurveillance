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

using Telerik.Web.UI;

using Quartz;
using Quartz.Core;
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;

public partial class edit_training_extended : System.Web.UI.Page
{
    public int training_id;
    public static int post_as_user_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PostToFeedAs"]);
    protected string manage_url = System.Configuration.ConfigurationManager.AppSettings["Site_ManageURL"];
    protected string key = System.Configuration.ConfigurationManager.AppSettings["Site_AutomationKey"];
    protected string final_manage_url;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);

            final_manage_url = manage_url + "/public/launch-as-user.aspx?key=" + key + "&userID=" + curr_user_id;

            training_id = Convert.ToInt32(Request.QueryString["trainingID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {

                /*
                 ViewState.Add("vsTrainingID", training_id);

                 qLrn_Training_View training = new qLrn_Training_View(training_id);

                 lblTitle.Text = "Edit Training Properties (ID: " + training.TrainingID + ")";
                 txtTitle.Text = training.Title;
                 txtDescription.Text = training.Description;
                 rblAvailable.SelectedValue = training.Available;
                 if (!String.IsNullOrEmpty(Convert.ToString(training.PersonAuthorID)))
                     ddlAuthors.SelectedValue = Convert.ToString(training.PersonAuthorID);

                 ddlTrainingTypes.Enabled = false;
                 ddlTrainingTypes.SelectedValue = Convert.ToString(training.TrainingTypeID);
                 lblTrainingType.Text = "<i>NOTE: Once set, this cannot be changed</i>";
                 if (training.TrainingTypeName == "Internal")
                 {
                     plhInternalTraining.Visible = true;
                     ddlDesignTemplates.SelectedValue = Convert.ToString(training.DesignThemeID);
                     rfvTrainingLink.Enabled = false;
                 }
                 else if (training.TrainingTypeName == "External")
                 {
                     plhExternalTraining.Visible = true;
                     rfvDesignTemplate.Enabled = false;
                 }
                 else if (training.TrainingTypeName == "In Person")
                 {
                     plhInPersonTraining.Visible = true;
                 }
                 plhMetaData.Visible = true;
                 */
            }
        }

        if (String.IsNullOrEmpty(Convert.ToString(training_id)))
            training_id = (Int32)ViewState["vsTrainingID"];
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {

            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["trainingID"]))
            {
                /*
                training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
                qLrn_Training training = new qLrn_Training(training_id);
                training.Title = txtTitle.Text;
                training.Description = txtDescription.Text;
                training.LastModified = DateTime.Now;
                training.LastModifiedBy = user_id;
                training.Available = rblAvailable.SelectedValue;
                if (!String.IsNullOrEmpty(ddlAuthors.SelectedValue))
                    training.PersonAuthorID = Convert.ToInt32(ddlAuthors.SelectedValue);
                if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "Internal")
                {
                    training.DesignThemeID = Convert.ToInt32(ddlDesignTemplates.SelectedValue);
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "External")
                {
                    training.Link = txtLink.Text;
                }
                else if (Convert.ToString(ddlTrainingTypes.SelectedItem) == "In Person")
                {
                    // do something
                }
                training.Update();
                 */
            }

            string user_name = (new qPtl_User(user_id)).UserName;

            Response.Redirect("training-edit.aspx?trainingID=" + Request.QueryString["trainingID"]);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("training-edit.aspx?trainingID=" + Request.QueryString["trainingID"]);
    }

    protected void btnBackList_Click(object sender, EventArgs e)
    {
        Response.Redirect("training-edit.aspx?trainingID=" + Request.QueryString["trainingID"]);
    }
}