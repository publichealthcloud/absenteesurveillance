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

public partial class report_training : System.Web.UI.Page
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
            int training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
            qLrn_Training_View training = new qLrn_Training_View(training_id);
            litTitle.Text = "Training Report <strong>" + training.Title + "</strong>";

            litBtnManageEnrolled.Text = "<a href=\"/manage/members/learning/user-training-list.aspx?trainingID=" + training_id + "\" class=\"btn\"><i class=\"icon-group\"></i>&nbsp;&nbsp;Enrolled Members</a>";
            litBtnManageTraining.Text = "<a href=\"training-edit.aspx?trainingID=" + training_id + "\" class=\"btn\"><i class=\"icon-edit\"></i>&nbsp;&nbsp;Manage Training</a>";
        }
    }
}
