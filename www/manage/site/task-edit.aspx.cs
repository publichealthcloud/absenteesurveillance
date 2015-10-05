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
using Quartz.Portal;
using Quartz.Social;
using Quartz.Learning;

public partial class qPtl_task_edit : System.Web.UI.Page
{
    public int task_id;
    public string searchType;
    public int actor_id;
    public string actor;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            searchType = Convert.ToString(Request.QueryString["searchType"]);
            hplBackTop.NavigateUrl = "tasks-list.aspx?searchType=" + searchType;
            hplBackBottom.NavigateUrl = "tasks-list.aspx?searchType=" + searchType;

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();
            
            if (!String.IsNullOrEmpty(Request.QueryString["taskID"]))
            {
                string curr_role = string.Empty;
                qPtl_Role role = new qPtl_Role("host");

                ddlAssignedTo.DataSource = qPtl_UserRole_View.GetRoleUsers(role.RoleID);
                ddlAssignedTo.DataTextField = "FullName";
                ddlAssignedTo.DataValueField = "UserID";
                ddlAssignedTo.DataBind();
                ddlAssignedTo.Items.Insert(0, new ListItem("", string.Empty));
                
                task_id = Convert.ToInt32(Request.QueryString["taskID"]);
                qPtl_Task task = new qPtl_Task(task_id);

                qPtl_User user = new qPtl_User(Convert.ToInt32(task.LastModifiedBy));
                actor_id = user.UserID;
                actor = user.UserName;

                lblTaskName.Text = task.Name;
                lblStatus.Text = task.Status;
                txtImportance.Text = Convert.ToString(task.Importance);

                if (!String.IsNullOrEmpty(Convert.ToString(task.PercentCompleted)))
                {
                    ddlPercentCompleted.SelectedValue = Convert.ToString(task.PercentCompleted);
                }
                if (!String.IsNullOrEmpty(Convert.ToString(task.AssignedTo)))
                {
                    ddlAssignedTo.SelectedValue = Convert.ToString(task.AssignedTo);
                }

                lblTitle.Text = "Edit Task (ID: " + task.TaskID +")";

                if (task.LastModifiedBy > 0)
                {
                    qPtl_User reviewed_by = new qPtl_User(Convert.ToInt32(task.LastModifiedBy));
                    lblLastUpdated.Text = "Reviewed by " + reviewed_by.UserName + " at " + task.LastModified;
                }
                else
                    lblLastUpdated.Visible = false;
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        searchType = Convert.ToString(Request.QueryString["searchType"]);

        if (!String.IsNullOrEmpty(Request.QueryString["taskID"]))
        {  
            task_id = Convert.ToInt32(Request.QueryString["taskID"]);
            qPtl_Task task = new qPtl_Task(task_id);

            task.PercentCompleted = Convert.ToInt32(ddlPercentCompleted.SelectedValue);
            if (Convert.ToInt32(ddlPercentCompleted.SelectedValue) == 100)
                task.Status = "Completed";
            else
                task.Status = "Open";
            task.Importance = Convert.ToInt32(txtImportance.Text);
            if (!String.IsNullOrEmpty(Convert.ToString(ddlAssignedTo.SelectedItem)))
                task.AssignedTo = Convert.ToInt32(ddlAssignedTo.SelectedValue);
            else
                task.AssignedTo = null;
            task.Update();
        }

        Response.Redirect("~/qPtl/tasks-list.aspx?searchType=" + searchType);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        task_id = Convert.ToInt32(Request.QueryString["taskID"]);
        qPtl_Task task = new qPtl_Task(task_id);

        task.Available = "No";
        task.MarkAsDelete = 0;
        task.Update();

        Response.Redirect("~/qPtl/tasks-list.aspx?searchType=" + searchType);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/qPtl/tasks-list.aspx?searchType=" + searchType);
    }
}
