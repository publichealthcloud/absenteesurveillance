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
using Quartz.Communication;
using Quartz.Organization;
using Quartz.Controls;

public partial class campaign_edit : System.Web.UI.Page
{
    public int campaign_id;
    public int user_id;
    public string username;
    public string base_path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
        {
            campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
            CampaignActivitiesList.CampaignID = campaign_id;
            plhCampaignMemberList.Visible = true;

            var campaign_users = qSoc_UserCampaign_View.GetCampaignUsersOrderBy(campaign_id, "CampaignStart DESC");
            DataTable members = new DataTable();
            members.Columns.Add("UserCampaignID", typeof(int));
            members.Columns.Add("UserID", typeof(int));
            members.Columns.Add("CampaignID", typeof(int));
            members.Columns.Add("Username", typeof(string));
            members.Columns.Add("Name", typeof(string));
            members.Columns.Add("Status", typeof(string)); 
            members.Columns.Add("Email", typeof(string));
            members.Columns.Add("DayInCampaign", typeof(int));
            members.Columns.Add("DeliveryMethods", typeof(string));
            members.Columns.Add("StartDate", typeof(DateTime));

            lblEnrollmentInfo.Text = "<span id=\"member-count\">" + Convert.ToString(campaign_users.Count) + "</span> members enrolled";

            if (campaign_users != null)
            {
                foreach (var u in campaign_users)
                {
                    Quartz.Controls.CampaignMemberListView curr_member = (Quartz.Controls.CampaignMemberListView)LoadControl("~/manage/site/learning/controls/CampaignMemberListView.ascx");
                    curr_member.UserID = u.UserID;
                    curr_member.CampaignID = campaign_id;
                    //pnlCampaignMembers.Controls.Add(curr_member);
                    string status = string.Empty;
                    if (u.CampaignStatus == "Not Started")
                        status = "<span class=\"label label-lightred\">Not Started</span>";
                    else if (u.CampaignStatus == "Completed")
                        status = "<span class=\"label label-lightred\">Finished</span>";
                    else
                        status = "<span class=\"label label-satgreen\">In Progress</span>";
                    string delivery_methods = string.Empty;
                    if (u.BrowserOk == "Yes")
                        delivery_methods += "Browser";
                    if (u.EmailOk == "Yes")
                        delivery_methods += " Email";
                    if (u.MobileOk == "Yes")
                        delivery_methods += " Mobile";
                    if (u.SMSOk == "Yes")
                        delivery_methods += " Text Messaging";
                    string name = u.LastName + ", " + u.FirstName;
                    DateTime short_date = new DateTime();
                    short_date = Convert.ToDateTime(u.CampaignStart).Date;
                    members.Rows.Add(u.UserCampaignID, u.UserID, u.CampaignID, u.UserName, name, status, u.Email, u.DaysInCampaign, delivery_methods, short_date);
                }
            }
            gridMembers.DataSource = members;
            gridMembers.DataBind();
        }
        else
        {
            plhCampaignMemberList.Visible = false;
            plhActivities.Visible = false;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // configure library
            string[] library_path = new string[] { "~/resources/campaigns/" + campaign_id + "/" };
            fxpCampaignLibrary.Configuration.ViewPaths = library_path;
            fxpCampaignLibrary.Configuration.MaxUploadFileSize = 256000000;
            fxpCampaignLibrary.Configuration.UploadPaths = library_path;
            fxpCampaignLibrary.Configuration.DeletePaths = library_path;
            
            hplBackTop.NavigateUrl = "campaigns-list.aspx";
            hplBackBottom.NavigateUrl = "campaigns-list.aspx";

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            //hplRefreshTop.NavigateUrl = Request.Url.ToString();

            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
            {
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
                txtCampaignName.Text = campaign.CampaignName;
                txtCampaignDescription.Text = campaign.Description;
                lblPostedTime.Text = Convert.ToString(campaign.Created);
                lblTitle.Text = "Edit Campaign (ID: " + campaign.CampaignID + ")";
                rblAvailableNew.Text = campaign.Available;
                lblNumberUserActivities.Text = Convert.ToString(campaign.NumUserActivities);
                lblTotalDays.Text = Convert.ToString(campaign.TotalDays);
                txtCode.Text = campaign.Code;
                txtKeyword.Text = campaign.Keyword;

                lblQuickLauchURL.Text = "<i>not created yet</i>";
                if (!String.IsNullOrEmpty(campaign.Code))
                {
                    qPtl_Redirect redirect = new qPtl_Redirect("/" + campaign.Code + "/");
                    if (redirect != null)
                    {
                        if (redirect.RedirectID > 0)
                        {
                            lblQuickLauchURL.Text = base_path + campaign.Code;
                            btnCreateCampaignRedirect.Text = "Delete URL";
                        }
                    }
                }
                else
                {
                    lblQuickLauchURL.Text = "<i>You must first save a registration code to create a quick launch url</i>";
                    btnCreateCampaignRedirect.Visible = false;
                }

                qPtl_User user = new qPtl_User(campaign.LastModifiedBy);
                user_id = user.UserID;
                username = user.UserName;

                qPtl_User reviewed_by = new qPtl_User(campaign.LastModifiedBy);
                lblPostedTime.Text = " at " + campaign.LastModified;
                plhMoreInfo.Visible = true;

                string delivery_options = string.Empty;
                if (campaign.SMS == "Yes")
                    delivery_options += "&nbsp;&nbsp;<i class=\"glyphicon-phone\"></i>&nbsp;&nbsp;Text Messaging (SMS)";
                if (campaign.Mobile == "Yes")
                {
                    if (!String.IsNullOrEmpty(delivery_options))
                        delivery_options += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    delivery_options += "<i class=\"glyphicon-iphone\"></i>&nbsp;&nbsp;Mobile App";
                }
                if (campaign.Browser == "Yes")
                {
                    if (!String.IsNullOrEmpty(delivery_options))
                        delivery_options += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    delivery_options += "<i class=\"glyphicon-display\"></i>&nbsp;&nbsp;Browser";
                }
                if (campaign.Email == "Yes")
                {
                    if (!String.IsNullOrEmpty(delivery_options))
                        delivery_options += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    delivery_options += "<i class=\"icon-envelope\"></i></i>&nbsp;&nbsp;Email";
                }
                lblDeliveryModes.Text = delivery_options;
                if (campaign.Browser == "Yes")
                    chkBrowser.Enabled = true;
                else
                {
                    chkBrowser.Enabled = false;
                    chkBrowser.Text = "<i>Browser - Not Available for this campaign</i>";
                }
                if (campaign.Mobile == "Yes")
                    chkMobile.Enabled = true;
                else
                {
                    chkMobile.Enabled = false;
                    chkMobile.Text = "<i>Mobile App - Not Available for this campaign</i>";
                }
                if (campaign.SMS == "Yes")
                    chkSMS.Enabled = true;
                else
                {
                    chkSMS.Enabled = false;
                    chkSMS.Text = "<i>SMS - Not Available for this campaign</i>";
                }
                if (campaign.Email == "Yes")
                    chkEmail.Enabled = true;
                else
                {
                    chkEmail.Enabled = false;
                    chkEmail.Text = "<i>Email - Not Available for this campaign</i>";
                }
            }
            else
            {
                lblTitle.Text = "Create New Campaign";
                rblAvailableNew.Text = "Yes";
                plhMoreInfo.Visible = false;
                plhTools.Visible = false;
            }
        }
    }

    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.FilterCommandName)
        {
            Pair filterPair = (Pair)e.CommandArgument;

            switch (filterPair.Second.ToString())
            {
                case "CampaignStart":
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

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        gridMembers.ExportSettings.ExportOnlyData = true;
        gridMembers.ExportSettings.IgnorePaging = true;
        gridMembers.ExportSettings.OpenInNewWindow = true;
        gridMembers.ExportSettings.FileName = "CampaignMembers_" + DateTime.Now;
        gridMembers.MasterTableView.ExportToExcel();
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);
            int campaign_id = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["campaignID"]))
            {
                campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
                campaign.LastModified = DateTime.Now;
                campaign.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                campaign.MarkAsDelete = 0;
                campaign.CampaignName = txtCampaignName.Text;
                campaign.Description = txtCampaignDescription.Text;
                campaign.Available = rblAvailableNew.SelectedValue;
                campaign.Code = txtCode.Text;
                campaign.Keyword = txtKeyword.Text;
                campaign.Update();
                campaign_id = campaign.CampaignID;
            }
            else
            {
                qSoc_Campaign campaign = new qSoc_Campaign();
                campaign.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                campaign.Created = DateTime.Now;
                campaign.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                campaign.LastModified = DateTime.Now;
                campaign.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                campaign.MarkAsDelete = 0;
                campaign.CampaignName = txtCampaignName.Text;
                campaign.Description = txtCampaignDescription.Text;
                campaign.Available = rblAvailableNew.SelectedValue;
                campaign.TotalDays = 0;
                campaign.Code = txtCode.Text;
                campaign.Keyword = txtKeyword.Text;
                campaign.Insert();
                campaign_id = campaign.CampaignID;
            }

            Response.Redirect("~/manage/site/learning/campaign-edit.aspx?campaignID=" + campaign_id);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
        qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
        campaign.Available = "No";
        campaign.MarkAsDelete = 1;
        campaign.Update();

        // loop through all users and delete their campaign records
        var campaign_users = qSoc_UserCampaign_View.GetCampaignUsers(campaign_id);

        if (campaign_users != null)
        {
            foreach (var u in campaign_users)
            {
                //qSoc_UserCamapign.DeleteUserCampaign(u.UserID, u.CampaignID);

                // add class level logic to remove all campaign actions/activities
            }
        }

        Response.Redirect("~/manage/site/learning/campaigns-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaigns-list.aspx");
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaign-edit.aspx?" + Request.QueryString["campaignID"]);
    }

    protected void btnViewAddMember_Click(object sender, EventArgs e)
    {
        plhAddMember.Visible = true;
        btnViewAddMember.Visible = false;
    }

    protected void btnAddMember_Click(object sender, EventArgs e)
    {
        string sms_ok = "No";
        string email_ok = "No";
        string mobile_ok = "No";
        string browser_ok = "No";
        string any_mode = string.Empty;

        if (chkEmail.Checked)
        {
            any_mode += "email ";
            email_ok = "Yes";
        }
        if (chkMobile.Checked)
        {
            any_mode += "mobile ";
            mobile_ok = "Yes";
        }
        if (chkSMS.Checked)
        {
            any_mode += "sms ";
            sms_ok = "Yes";
        }
        if (chkBrowser.Checked)
        {
            any_mode += "web ";
            browser_ok = "Yes";
        }
        
        if (!String.IsNullOrEmpty(any_mode) && !String.IsNullOrEmpty(txtMemberUserName.Text))
        {
            // make sure user exists
            qPtl_User new_member = new qPtl_User(txtMemberUserName.Text);

            if (new_member.UserID > 0)
            {
                int campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
                var existing_campaign = qSoc_UserCampaign.GetCampaignByUser(new_member.UserID, campaign_id);
                if (existing_campaign == null)
                {
                    DateTime start_date = new DateTime();
                    start_date = DateTime.Now;

                    qSoc_UserCampaign new_campaign = qSoc_UserCampaign.EnrollUserInCampaign(new_member.UserID, campaign_id, "controlled", start_date, Convert.ToInt32(Context.Items["ScopeID"]), "role", browser_ok, mobile_ok, sms_ok, email_ok);

                    Response.Redirect("/manage/site/learning/campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"]);
                }
                else
                {
                    lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> This member is already enrolled in this campaign.";
                }
            }
            else
            {
                lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> This member does not exist; please enter another username";
            }
        }
        else
            lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> You need to enter a username and select at least one delivery method";
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaign-edit.aspx?campaignID=" + Request.QueryString["campaignID"]);
    }

    protected void btnManageActivities_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/site/learning/campaign-manage-activities.aspx?campaignID=" + Request.QueryString["campaignID"]);
    }

    protected void btnCreateCampaignRedirect_Click(object sender, EventArgs e)
    {
        // see if redirect exists, if yes -- then delete, else -- create
        int campaign_id = Convert.ToInt32(Request.QueryString["campaignID"]);
        qSoc_Campaign campaign = new qSoc_Campaign(campaign_id);
        qPtl_Redirect redirect = new qPtl_Redirect("/" + campaign.Code + "/");

        if (redirect != null)
        {
            if (redirect.RedirectID > 0)
            {
                redirect.DeleteRedirect(redirect.RedirectID);
            }
            else
            {
                int user_id = Convert.ToInt32(Context.Items["UserID"]);
                qPtl_Redirect n_redirect = new qPtl_Redirect();
                n_redirect.Available = "Yes";
                n_redirect.MarkAsDelete = 0;
                n_redirect.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                n_redirect.Created = DateTime.Now;
                n_redirect.CreatedBy = user_id;
                n_redirect.LastModified = DateTime.Now;
                n_redirect.LastModifiedBy = user_id;
                n_redirect.EntryURL = "/" + campaign.Code + "/";
                n_redirect.RedirectURL = base_path + "/public/campaigns/campaign-start.aspx?campaignID=" + campaign_id + "&keyword=" + campaign.Keyword;
                n_redirect.Insert();

            }
            Response.Redirect("~/manage/site/learning/campaign-edit.aspx?campaignID=" + campaign_id);
        }
    }
}
