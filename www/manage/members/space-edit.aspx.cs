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

public partial class space_edit : System.Web.UI.Page
{
    public int space_id;
    public int user_id;
    public string username;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["spaceID"]))
        {
            space_id = Convert.ToInt32(Request.QueryString["spaceID"]);
            plhGroupMemberList.Visible = true;

            var space_users = qSoc_UserSpace_View.GetSpaceUsers(space_id);
            lblEnrollmentInfo.Text = "<span id=\"member-count\">" + Convert.ToString(space_users.Count) + "</span> members enrolled";

            if (space_users != null)
            {
                foreach (var u in space_users)
                {
                    Quartz.Controls.GroupMemberListView curr_member = (Quartz.Controls.GroupMemberListView)LoadControl("~/manage/members/controls/GroupMemberListView.ascx");
                    curr_member.UserID = u.UserID;
                    curr_member.SpaceID = space_id;
                    pnlGroupMembers.Controls.Add(curr_member);
                }
            }
        }
        else
            plhGroupMemberList.Visible = false;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populateSchools();
            populateSpaceCategories();
            
            hplBackTop.NavigateUrl = "spaces-list.aspx";
            hplBackBottom.NavigateUrl = "spaces-list.aspx";

            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();

            if (!String.IsNullOrEmpty(Request.QueryString["spaceID"]))
            {
                space_id = Convert.ToInt32(Request.QueryString["spaceID"]);
                qSoc_Space space = new qSoc_Space(space_id);
                txtGroupNameNew.Text = space.SpaceShortName;
                txtGroupNameNewFull.Text = space.SpaceName;
                ddlGroupTypeNew.SelectedValue = space.SpaceType;
                if (!String.IsNullOrEmpty(Convert.ToString(space.SchoolID)))
                    ddlSchoolNew.SelectedValue = Convert.ToString(space.SchoolID);
                if (!String.IsNullOrEmpty(Convert.ToString(space.SpaceCategoryID)))
                    ddlSpaceCategoriesNew.SelectedValue = Convert.ToString(space.SpaceCategoryID);
                lblPostedTime.Text = Convert.ToString(space.Created);
                lblTitle.Text = "Edit Group (ID: " + space.SpaceID + ")";
                rblAvailableNew.Text = space.Available;
                rblVisibleInDirectory.Text = space.VisibleInDirectory;
                ddlEnrollmentType.SelectedValue = space.AccessMode;

                qPtl_User user = new qPtl_User(space.LastModifiedBy);
                user_id = user.UserID;
                username = user.UserName;

                qPtl_User reviewed_by = new qPtl_User(space.LastModifiedBy);
                lblPostedTime.Text = " at " + space.LastModified;
                plhMoreInfo.Visible = true;

                // get existing invitations
                var invitations = qPtl_Invitation_View.GetInvitationsBySpaceID(space_id);
                string invitation_html = string.Empty;

                foreach (var i in invitations)
                {
                    string invite_html = "<tr>";

                    invite_html += "<td><font color=\"gray\">" + i.InvitationAudience + "</font></td>";
                    invite_html += "<td class=\"hidden-1024\">" + i.RoleName + "</td>";
                    invite_html += "<td class=\"hidden-480\">" + i.InviteCode + "</td>";
                    invite_html += "</tr>";

                    invitation_html += invite_html;
                }

                if (!String.IsNullOrEmpty(invitation_html))
                {
                    litInvitations.Text = invitation_html;
                    plhInvitations.Visible = true;
                }
                else
                    plhInvitations.Visible = false;
            }
            else
            {
                lblTitle.Text = "Create New Group";
                rblVisibleInDirectory.SelectedValue = "No";
                rblAvailableNew.Text = "Yes";
                plhMoreInfo.Visible = false;
                plhTools.Visible = false;
            }


        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        Page.Validate("form");

        if (Page.IsValid)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);
            int space_id = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["spaceID"]))
            {
                space_id = Convert.ToInt32(Request.QueryString["spaceID"]);
                qSoc_Space space = new qSoc_Space(space_id);
                space.LastModified = DateTime.Now;
                space.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                space.MarkAsDelete = 0;
                space.SpaceShortName = txtGroupNameNew.Text;
                space.SpaceType = ddlGroupTypeNew.SelectedValue;
                space.SpaceName = txtGroupNameNewFull.Text;
                space.AccessMode = ddlEnrollmentType.SelectedValue;
                space.VisibleInDirectory = rblVisibleInDirectory.SelectedValue;
                space.SpaceType = ddlGroupTypeNew.SelectedValue;
                if (!String.IsNullOrEmpty(ddlSchoolNew.SelectedValue))
                    space.SchoolID = Convert.ToInt32(ddlSchoolNew.SelectedValue);
                if (!String.IsNullOrEmpty(ddlSpaceCategoriesNew.SelectedValue))
                    space.SpaceCategoryID = Convert.ToInt32(ddlSpaceCategoriesNew.SelectedValue);
                space.Available = rblAvailableNew.SelectedValue;
                space.Update();

                space_id = space.SpaceID;
            }
            else
            {
                qSoc_Space space = new qSoc_Space();
                space.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                space.Created = DateTime.Now;
                space.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                space.LastModified = DateTime.Now;
                space.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                space.MarkAsDelete = 0;
                space.SpaceShortName = txtGroupNameNew.Text;
                space.SpaceName = txtGroupNameNewFull.Text;
                space.SpaceType = ddlGroupTypeNew.SelectedValue;
                space.AccessMode = ddlEnrollmentType.SelectedValue;
                space.VisibleInDirectory = rblVisibleInDirectory.SelectedValue;
                space.SpaceType = ddlGroupTypeNew.SelectedValue;
                if (!String.IsNullOrEmpty(ddlSchoolNew.SelectedValue))
                    space.SchoolID = Convert.ToInt32(ddlSchoolNew.SelectedValue);
                if (!String.IsNullOrEmpty(ddlSpaceCategoriesNew.SelectedValue))
                    space.SpaceCategoryID = Convert.ToInt32(ddlSpaceCategoriesNew.SelectedValue);
                space.Available = rblAvailableNew.SelectedValue;
                space.Insert();

                space_id = space.SpaceID;
            }
            
            Response.Redirect("/manage/members/space-edit.aspx?spaceID=" + space_id);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        space_id = Convert.ToInt32(Request.QueryString["spaceID"]);
        qSoc_Space space = new qSoc_Space(space_id);
        space.Available = "No";
        space.MarkAsDelete = 1;
        space.VisibleInDirectory = "No";
        space.Update();

        // loop through all users and delete their space records
        var space_users = qSoc_UserSpace_View.GetSpaceUsers(space_id);

        if (space_users != null)
        {
            foreach (var u in space_users)
            {
                qSoc_UserSpace.DeleteUserSpace(u.UserID, u.SpaceID);
            }
        }

        Response.Redirect("~/manage/members/spaces-list.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/manage/members/spaces-list.aspx");
    }

    protected void populateSchools()
    {
        ddlSchoolNew.DataSource = qOrg_School.GetSchools();
        ddlSchoolNew.DataTextField = "School";
        ddlSchoolNew.DataValueField = "SchoolID";
        ddlSchoolNew.DataBind();
        ddlSchoolNew.Items.Insert(0, new ListItem("", string.Empty));
    }

    protected void populateSpaceCategories()
    {
        ddlSpaceCategoriesNew.DataSource = qSoc_SpaceCategory.GetSpaceCategories();
        ddlSpaceCategoriesNew.DataTextField = "CatgoryName";
        ddlSpaceCategoriesNew.DataValueField = "SpaceCategoryID";
        ddlSpaceCategoriesNew.DataBind();
        ddlSpaceCategoriesNew.Items.Insert(0, new ListItem("", string.Empty));
        int num_space_typesNew = ddlSpaceCategoriesNew.Items.Count;
        ddlSpaceCategoriesNew.Items.Insert(num_space_typesNew, new ListItem("Other", "-1"));
    }
    protected void btnViewAddMember_Click(object sender, EventArgs e)
    {
        plhAddMember.Visible = true;
        btnViewAddMember.Visible = false;
    }

    protected void btnAddMember_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(rblNewMemberRole.SelectedValue) && !String.IsNullOrEmpty(txtMemberUserName.Text))
        {
            // make sure user exists
            qPtl_User new_member = new qPtl_User(txtMemberUserName.Text);

            if (new_member.UserID > 0)
            {
                var existing_space_id = qSoc_UserSpace.GetMostRecentAddedUserSpace(new_member.UserID);
                if (existing_space_id == 0)
                {
                    // member can now be added
                    qSoc_UserSpace u_space = new qSoc_UserSpace();
                    u_space.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                    u_space.Available = "Yes";
                    u_space.Created = DateTime.Now;
                    u_space.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                    u_space.LastModified = DateTime.Now;
                    u_space.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                    u_space.MarkAsDelete = 0;
                    u_space.UserID = new_member.UserID;
                    u_space.SpaceID = Convert.ToInt32(Request.QueryString["spaceID"]);
                    if (rblNewMemberRole.SelectedValue == "Advisor")
                    {
                        u_space.SpaceRole = "Moderator";
                        qPtl_SpaceAdmin admin = new qPtl_SpaceAdmin();
                        admin.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                        admin.Available = "Yes";
                        admin.Created = DateTime.Now;
                        admin.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                        admin.LastModified = DateTime.Now;
                        admin.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                        admin.MarkAsDelete = 0;
                        admin.UserID = new_member.UserID;
                        admin.SpaceID = Convert.ToInt32(Request.QueryString["spaceID"]);
                        admin.Insert();
                    }
                    u_space.Insert();

                    Response.Redirect("/manage/members/space-edit.aspx?spaceID=" + Request.QueryString["spaceID"]);
                }
                else
                {
                    qSoc_Space e_space = new qSoc_Space(existing_space_id);
                    lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> This member is already a member of the group: <strong>" + e_space.SpaceShortName + "</strong>. The member must first be removed from this group.";
                }
            }
            else
            {
                lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> This member does not exist; please enter another username";
            }
        }
        else
            lblMessageMember.Text = "&nbsp;&nbsp;<strong>***ERROR***</strong> You need to enter a username and select a role";
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("/manage/members/space-edit.aspx?spaceID=" + Request.QueryString["spaceID"]);
    }
}
