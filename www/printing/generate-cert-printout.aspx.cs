using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;
using Quartz.Learning;

public partial class qLrn_generate_cert_printout : System.Web.UI.Page
{
    protected int TrainingID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TrainingID = Convert.ToInt32 (Request.QueryString ["TrainingID"]);

            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            lblName.Text = user.FirstName + " " + user.LastName;
            string signature = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Learning_certSignature1"]) + "<br>" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Learning_certSignature2"]);
            lblSignature.Text = signature;

            imgCertBgImage.ImageUrl = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_URL"]) + "/learning/certs/" + Context.Items["ScopeID"] + "/cert_bg.png";

            qLrn_UserTraining_View u_training = new qLrn_UserTraining_View(user.UserID, TrainingID);
            if (u_training != null)
            {
                lblTrainingTitle.Text = u_training.Title;
                lblCompletionDate.Text = "Completed: " + String.Format("{0:M/d/yyyy}", u_training.Completed);
                if (u_training.EstMinutesToComplete > 0)
                {
                    float num_hours = u_training.EstMinutesToComplete / 60;
                    lblCompletionHours.Text = "&nbsp;for a total of " + num_hours + " contact hours";
                }
            }
            else
            {
                lblTrainingTitle.Text = "Training title not available";
                lblCompletionDate.Text = "Training completion date not available";
            }
        }
    }
}