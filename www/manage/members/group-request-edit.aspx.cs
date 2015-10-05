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
using Quartz.Organization;
using Quartz.Social;
using Quartz.Communication;

public partial class edit_group_request : System.Web.UI.Page
{
    public int group_request_id;
    public int user_id;
    public string order_by;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            group_request_id = Convert.ToInt32(Request.QueryString["groupRequestID"]);

            if (!String.IsNullOrEmpty(Convert.ToString(group_request_id)))
            {
                qOrg_GroupRequests request = new qOrg_GroupRequests(group_request_id);

                populateSchools();

                lblTitle.Text = "Edit Group Request (ID: " + request.GroupRequestID + ")";

                lblRequestTimestamp.Text = Convert.ToString(request.Created);
                
                string advisor_info = string.Empty;
                advisor_info = "<span class=\"NormalBoldDarkGray\">Name:</span> " + request.AdvisorFirstName + " " + request.AdvisorLastName;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Position:</span> " + request.AdvisorPosition;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Other Position:</span> " + request.AdvisorPositionOther;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Email:</span> " + request.AdvisorEmail;
                advisor_info += "<br><br><span class=\"NormalBoldDarkGray\">Phone:</span> " + request.AdvisorPhone;
                litAdvisorInfo.Text = advisor_info;

                string group_info = string.Empty;
                group_info = "<span class=\"NormalBoldDarkGray\">Name:</span> " + request.GroupShortName;
                qSoc_SpaceCategory category = new qSoc_SpaceCategory(Convert.ToInt32(request.SpaceCategoryID));
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">Type:</span> " + category.CatgoryName;
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">Other Type:</span> " + request.CategoryOther;
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">Description:</span> " + request.GroupDescriptionOther;
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">Why Join:</span> " + request.WhyJoin;
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">How Many Members:</span> " + request.NumNumbers;
                group_info += "<br><br><span class=\"NormalBoldDarkGray\">When Founded:</span> " + request.WhenFounded;
                litGroupInfo.Text = group_info;

                string school_info = string.Empty;
                school_info += "<br><br><span class=\"NormalBoldDarkGray\">School:</span> " + request.OtherSchool;
                if (!String.IsNullOrEmpty(Convert.ToString(request.SchoolDistrictID)))
                {
                    int school_district_id = request.SchoolDistrictID;
                    qOrg_SchoolDistrict district = new qOrg_SchoolDistrict(school_district_id);
                    school_info += "<br><br><span class=\"NormalBoldDarkGray\">District:</span> " + district.DistrictName;
                }
                else
                {
                    school_info += "<br><br><span class=\"NormalBoldDarkGray\">District:</span> none";
                }
                school_info += "<br><br><span class=\"NormalBoldDarkGray\">Type:</span> " + request.SchoolType;
                school_info += "<br><br><span class=\"NormalBoldDarkGray\">Address:</span><blockquote><span class=\"Normal\"> " + request.OtherSchoolAddress1 + ", " + request.OtherSchoolAddress2 + "<br>" + request.OtherSchoolCity + ", " + request.OtherSchoolStateProvince + " " + request.OtherSchoolPostalCode + "</span></blockquote>";
                school_info += "<br><br><span class=\"NormalBoldDarkGray\">Phone:</span> " + request.OtherSchoolPhone;
                litSchoolInfo.Text = school_info;

                string principal_info = string.Empty;
                principal_info = "<span class=\"NormalBoldDarkGray\">Name:</span> " + request.PrincipalFirstName + " " + request.PrincipalLastName;
                principal_info += "<br><br><span class=\"NormalBoldDarkGray\">Position:</span> " + request.PrincipalRole;
                principal_info += "<br><br><span class=\"NormalBoldDarkGray\">Email:</span> " + request.PrincipalEmail;
                principal_info += "<br><br><span class=\"NormalBoldDarkGray\">Phone:</span> " + request.PrincipalPhone;
                litPrincipalInfo.Text = principal_info;

                txtAdvisorEmail.Text = request.AdvisorEmail;
                txtGroupName.Text = request.GroupShortName;
                txtPrincipalEmail.Text = request.PrincipalEmail;
                txtAdvisorNotes.Text = request.AdvisorNotes;
                txtGroupNotes.Text = request.GroupNotes;
                txtSchoolNotes.Text = request.SchoolNotes;
                txtPrincipalNotes.Text = request.PrincipalNotes;
                ddlStatus.SelectedValue = request.Status;
                if (!String.IsNullOrEmpty(Convert.ToString(request.SchoolID)))
                    if (request.SchoolID > 0)
                        ddlSchools.SelectedValue = Convert.ToString(request.SchoolID);
                    else
                    {
                        btnApproveRequest.Enabled = false;
                        lblApprovalInfo.Text = "<br><br><strong>*** WARNING: This request does NOT have a school associated with it yet. A school must be selected from the pull down in the School Info section below. ***</strong>";
                    }

                else
                {
                    btnApproveRequest.Enabled = false;
                    lblApprovalInfo.Text = "<br><br><strong>*** WARNING: This request does NOT have a school associated with it yet. A school must be selected from the pull down in the School Info section below. ***</strong>";
                }

                if (!String.IsNullOrEmpty(request.ApprovedBy))
                {
                    btnApproveRequest.Visible = true;
                    lblApprovalInfo.Text = "Approved by " + request.ApprovedBy + " and email last sent at " + request.WhenApproved;
                }
                else
                    btnApproveRequest.Visible = true;

                if (request.Status == "Completed")
                    lblPrincipalApprval.Text = "Request APPROVED by " + request.PrincipalInitials + " at " + request.PrincipalWhenApproved;
                else if (request.Status == "Rejected-Principal")
                    lblPrincipalApprval.Text = "Request DENIED by " + request.PrincipalInitials + " at " + request.PrincipalWhenApproved;

            }
            else
            {
                lblTitle.Text = "A Problem Has Occurred: Please go back and try again";
            }
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        request.Status = ddlStatus.SelectedValue;
        request.AdvisorEmail = txtAdvisorEmail.Text;
        request.GroupShortName = txtGroupName.Text;
        request.PrincipalEmail = txtPrincipalEmail.Text;
        request.AdvisorNotes = txtAdvisorNotes.Text;
        request.GroupNotes = txtGroupNotes.Text;
        request.SchoolNotes = txtSchoolNotes.Text;
        request.PrincipalNotes = txtPrincipalNotes.Text;
        if (!String.IsNullOrEmpty(ddlSchools.SelectedValue))
            request.SchoolID = Convert.ToInt32(ddlSchools.SelectedValue);
        request.Update();

        Response.Redirect("/manage/members/group-request-edit.aspx?groupRequestID=" + Request.QueryString["groupRequestID"]);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        request.Available = "No";
        request.MarkAsDelete = 0;
        request.Update();

        Response.Redirect("/manage/members/group-requests-list.aspx");
    }

    protected void populateSchools()
    {
        ddlSchools.DataSource = qOrg_School.GetSchools();
        ddlSchools.DataTextField = "School";
        ddlSchools.DataValueField = "SchoolID";
        ddlSchools.DataBind();
        ddlSchools.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void btnApproveRequest_Click(object sender, EventArgs e)
    {
        qOrg_GroupRequests request = new qOrg_GroupRequests(Convert.ToInt32(Request.QueryString["groupRequestID"]));
        qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
        request.ApprovedBy = user.UserName;
        request.WhenApproved = DateTime.Now;
        request.Status = "Pending-WaitingPrincipalApproval";
        request.Update();

        // send email to principal
        int principal_email_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["GroupRequest_principal_EmailID"]);
        qCom_EmailTool email = new qCom_EmailTool(principal_email_id);
        string principal_name = request.PrincipalRole + " " + request.PrincipalLastName;
        email.SendDatabaseMail(request.PrincipalEmail, principal_email_id, 0, request.GroupShortName, request.GroupShortName, request.WhyJoin, Convert.ToString(request.GroupRequestID), principal_name, false);

        Response.Redirect(Request.Url.ToString());
    }
}
