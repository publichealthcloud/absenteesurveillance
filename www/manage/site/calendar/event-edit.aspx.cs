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

public partial class edit_event : System.Web.UI.Page
{
    public int event_id;
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_SiteFolder"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateTrainings();
            
            if (!String.IsNullOrEmpty(Request.QueryString["eventID"]))
            {
                event_id = Convert.ToInt32(Request.QueryString["eventID"]);

                qSoc_Event curr_event = new qSoc_Event(event_id);

                lblTitle.Text = "Edit Event (ID: " + curr_event.EventID + ")";
                txtName.Text = curr_event.Name;
                txtSummary.Text = curr_event.Summary;
                reContent.Content = curr_event.Description;
                txtLocation.Text = curr_event.Location;
                reLocationDetails.Content = curr_event.LocationDetails;
                rblAvailable.SelectedValue = curr_event.Available;
                rdtStartTime.SelectedDate = curr_event.DateTime;
                rdtEndTime.SelectedDate = curr_event.EndTime;
                txtURL.Text = curr_event.MoreInfoURL;
                if (curr_event.ReferenceID > 0)
                {
                    ddlTrainingList.SelectedValue = Convert.ToString(curr_event.ReferenceID);
                    plhTrainingList.Visible = true;
                }

                ddlType.SelectedValue = curr_event.EventType;
            }
            else
            {
                lblTitle.Text = "New Event";
                plhTools.Visible = false;
                rblAvailable.SelectedValue = "Yes";
            }

            if (Convert.ToString(Request.QueryString["mode"]) == "add-successful")
            {
                lblMessage.Text = "*** Record Successfully Added ***";
                lblMessageBottom.Text = "*** Record Successfully Added ***";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);

            if (!String.IsNullOrEmpty(Request.QueryString["eventID"]))
            {
                event_id = Convert.ToInt32(Request.QueryString["eventID"]);
                qSoc_Event curr_event = new qSoc_Event(event_id);

                curr_event.Name = txtName.Text;
                curr_event.Summary = txtSummary.Text;
                curr_event.Description = reContent.Content;
                curr_event.Location = txtLocation.Text;
                curr_event.LocationDetails = reLocationDetails.Content;
                curr_event.EventType = ddlType.SelectedValue;
                curr_event.Available = rblAvailable.SelectedValue;
                curr_event.DateTime = Convert.ToDateTime(rdtStartTime.SelectedDate);
                curr_event.EndTime = Convert.ToDateTime(rdtEndTime.SelectedDate);
                curr_event.MoreInfoURL = txtURL.Text;
                if (!String.IsNullOrEmpty(ddlTrainingList.SelectedValue))
                {
                    curr_event.ReferenceID = Convert.ToInt32(ddlTrainingList.SelectedValue);
                    curr_event.ContentTypeID = (int)qSoc_ContentType.Types.Training;
                }

                curr_event.Update();
            }
            else
            {
                qSoc_Event new_event = new qSoc_Event();
                new_event.ScopeID = 1;
                new_event.Created = DateTime.Now;
                new_event.CreatedBy = user_id;
                new_event.LastModified = DateTime.Now;
                new_event.LastModifiedBy = user_id;
                new_event.Available = "Yes";
                new_event.MarkAsDelete = 0;
                new_event.Name = txtName.Text;
                new_event.Summary = txtSummary.Text;
                new_event.Description = reContent.Content;
                new_event.Location = txtLocation.Text;
                new_event.LocationDetails = reLocationDetails.Content;
                new_event.Available = rblAvailable.SelectedValue;
                new_event.DateTime = Convert.ToDateTime(rdtStartTime.SelectedDate);
                new_event.EndTime = Convert.ToDateTime(rdtEndTime.SelectedDate);
                new_event.MoreInfoURL = txtURL.Text;
                if (!String.IsNullOrEmpty(ddlTrainingList.SelectedValue))
                {
                    new_event.ReferenceID = Convert.ToInt32(ddlTrainingList.SelectedValue);
                    new_event.ContentTypeID = (int)qSoc_ContentType.Types.Training;
                }

                new_event.Insert();

                event_id = new_event.EventID;
            }


            // redirect to page to add link + keywords
            if (!String.IsNullOrEmpty(Request.QueryString["eventID"]))
            {
                //lblMessage.Text = "*** Record Successfully Updated ***";
                //lblMessageBottom.Text = "*** Record Successfully Updated ***";
                Response.Redirect("events-list.aspx");
            }
            else
            {
                Response.Redirect(Request.Url.ToString() + "?mode=add-successful&eventID=" + event_id);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        event_id = Convert.ToInt32(Request.QueryString["eventID"]);

        qSoc_Event curr_event = new qSoc_Event(event_id);
        curr_event.Available = "No";
        curr_event.MarkAsDelete = 1;
        curr_event.Update();

        Response.Redirect("events-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("events-list.aspx");
    }

    protected void populateTrainings()
    {
        /*
        ddlTrainings.DataSource = qLrn_Training.GetAvailableInPersonTrainingsByAlpha();
        ddlTrainings.DataTextField = "Title";
        ddlTrainings.DataValueField = "TrainingID";
         */

        ddlTrainingList.DataSource = qLrn_Training.GetTrainingsByAlpha();
        ddlTrainingList.DataTextField = "Title";
        ddlTrainingList.DataValueField = "TrainingID";
        ddlTrainingList.DataBind();
        ddlTrainingList.Items.Insert(0, new ListItem("", string.Empty));

        //ddlTrainings.DataBind();
        //ddlTrainings.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlTrainingList.SelectedValue))
            plhTrainingList.Visible = true;
        else
            plhTrainingList.Visible = false;
    }
}
