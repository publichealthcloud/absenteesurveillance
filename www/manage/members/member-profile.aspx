<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="member-profile.aspx.cs" Inherits="manage_members_member_profile" %>
<%@ Register Src="~/manage/members/controls/MemberNav.ascx" TagPrefix="epg" TagName="MemberNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/members/controls/MemberEnrolledGroup.ascx" TagPrefix="epg" TagName="MemberEnrolledGroup" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<script type="text/JavaScript">
    var confirmed = false;

    function ConfirmRemoveMemberFromGroup(controlID) {
        if (confirmed) { return true; }

        bootbox.confirm("Are you sure want to remove the member from this group?", function (result) {
            if (result) {
                if (controlID != null) {
                    var controlToClick = document.getElementById(controlID);
                    if (controlToClick != null) {
                        confirmed = true;
                        controlToClick.click();
                        confirmed = false;
                    }
                }
            }

        });

        return false;

    }
</script>

<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/members/member-list.aspx">Member List</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/members/member-profile.aspx?userID=<%= profile_id %>">Member Basic Info</a>
				</li>
			</ul>
			<div class="close-bread">
				<a href="#"><i class="icon-remove"></i></a>
			</div>
		</div>
    </div>   
</div>
	<div class="box box-bordered box-color lightgrey">
		<div class="box-title">
			<epg:MemberNav runat="server" ID="MemberNav" />
		</div>
		<div class="box-content nopadding">
			<ul class="tabs tabs-inline tabs-top">
				<li <asp:Literal ID="litOverviewClass" runat="server"></asp:Literal>>
					<a href="#overview" data-toggle='tab'><i class="icon-info-sign"></i> Overview</a>
				</li>
				<li <asp:Literal ID="litProfileClass" runat="server"></asp:Literal>>
					<a href="#profile" data-toggle='tab'><i class="icon-user"></i> Profile</a>
				</li>
				<li <asp:Literal ID="litPermissionClass" runat="server"></asp:Literal>>
					<a href="#permissions" data-toggle='tab'><i class="icon-lock"></i> Permissions</a>
				</li>
				<li <asp:Literal ID="litGroupClass" runat="server"></asp:Literal>>
					<a href="#group" data-toggle='tab'><i class="icon-group"></i> Groups</a>
				</li>
				<li <asp:Literal ID="litWarningsClass" runat="server"></asp:Literal>>
					<a href="#warnings" data-toggle='tab'><i class="icon-warning-sign"></i> Warnings</a>
				</li>
			</ul>
			<div class="tab-content padding tab-content-inline tab-content-bottom">
				<div <asp:Literal ID="litTabOverviewClass" runat="server"></asp:Literal> id="overview">
						<div class="row-fluid">
							<div class="span2">
								<div class="fileupload fileupload-new" data-provides="fileupload">
									<div class="fileupload-new thumbnail" style="max-width: 200px; max-height: 150px;"><asp:Literal ID="litProfilePict" runat="server"></asp:Literal></div>
									<div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
									<div>
										<a target="_blank" href="/social/profile/profile.aspx?userID=<%= profile_id %>" class="btn">View in Site</a>
									</div>
								</div>
							</div>
							<div class="span10">
                                <h4>Account Info</h4>
                                <div class="control-group">
                                    <label for="name" class="control-label right"><strong>ID:</strong>&nbsp;&nbsp;<asp:Label ID="lblUserID" runat="server"></asp:Label></label>
								</div>
                                <div class="control-group">
                                    <label for="name" class="control-label right"><strong>Role:</strong>&nbsp;&nbsp;<asp:Label ID="lblRoleName" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
									<label for="name" class="control-label right"><strong>Username:</strong>&nbsp;&nbsp;<asp:Label ID="lblUsername" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Full Name:</strong>&nbsp;&nbsp;<asp:Label ID="lblFullName" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Email:</strong>&nbsp;&nbsp;<asp:Label ID="lblEmail" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Group:</strong>&nbsp;&nbsp;<asp:Label ID="lblGroupName" runat="server"></asp:Label></label>
								</div>
                                <asp:PlaceHolder ID="plhSchoolDisplay" runat="server">
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>School:</strong>&nbsp;&nbsp;<asp:Label ID="lblSchool" runat="server"></asp:Label></label>
								</div>
                                </asp:PlaceHolder>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Account Created:</strong>&nbsp;&nbsp;<asp:Label ID="lblCreated" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><br /></label>
								</div>
                                <h4>Most Recent Activity</h4>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Last Activity:</strong>&nbsp;&nbsp;<asp:Label ID="lblMostRecentLogin" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><strong>Last IP Address:</strong>&nbsp;&nbsp;<asp:Label ID="lblMostRecentIPAddress" runat="server"></asp:Label></label>
								</div>
								<div class="control-group">
                                    <label for="name" class="control-label right"><br /></label>
								</div>
                                <h4>Common Tools</h4>
								<div class="control-group">
                                    <asp:Button CssClass="btn" Text="Send Welcome Email" runat="server" ID="btnSendWelcomeEmail" OnClick="btnSendWelcomeEmail_Click" />
                                    <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="Send a welcome email with getting started instructions" data-original-title="Send Welcome Email"><div><i class="icon-question-sign"></i></div></a>
                                    &nbsp;&nbsp;<asp:Label ID="lblWelcomeEmailMessage" runat="server"></asp:Label>
								</div>
								<div class="control-group">
                                    <asp:HyperLink ID="hplManageTrainings" CssClass="btn" runat="server" Text="Manage Trainings"></asp:HyperLink>
                                    <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="Add, reset or delete trainings. View training and assessment information." data-original-title="Manage Member Trainings"><div><i class="icon-question-sign"></i></div></a>
								</div>
							</div>
						</div>
				</div>

				<div <asp:Literal ID="litTabProfileClass" runat="server"></asp:Literal> id="profile">
                     <h4>Profile</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblProfileMessage" runat="server"></asp:Label>
			        <table border="0" cellpadding="5" width="600">
                        <tr>
                            <td colspan="2" class="NormalBold">
                            <strong><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label></strong>
                            <asp:PlaceHolder ID="plhValidationProfile" runat="server">
                                <blockquote>
                                <asp:ValidationSummary ForeColor="red" runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="profile" />
                                </blockquote>
                            </asp:PlaceHolder>
                            </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Member Type</strong>
                            </td>
                            <td class="NormalBold">
                                <asp:Label ID="lblMemberTypeProfile" runat="server"></asp:Label>
                           </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Username</strong>
                            </td>
                            <td class="NormalBold">
                                <asp:Label ID="lblUsernameProfile" runat="server"></asp:Label>
                           </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>First Name<%=required_indicator %></strong>
                            </td>
                            <td class="NormalBold">
                            <telerik:RadTextBox ID="txtFirstName" MaxLength="50" runat="server" Width="250px"></telerik:RadTextBox>
                           </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Last Name<%=required_indicator %></strong>
                            </td>
                            <td class="NormalBold">
                            <telerik:RadTextBox ID="txtLastName" MaxLength="50" runat="server" Width="250px"></telerik:RadTextBox>
                           </td>
                        </tr>
                        <asp:PlaceHolder ID="plhSocialProfileElements" runat="server">
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Email<%=required_indicator %></strong>
                            </td>
                            <td class="NormalBold">
                            <telerik:RadTextBox ID="txtEmail" MaxLength="50" runat="server" Width="250px"></telerik:RadTextBox>
 
                           </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>DOB<%=required_indicator %></strong>
                            </td>
                            <td class="Normal">
                            <telerik:RadDatePicker ID="rdtDOB" MinDate="1/1/1900" MaxDate="1/1/2020" runat="server"></telerik:RadDatePicker>
                           </td>
                        </tr>
                         <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Gender<%=required_indicator %></strong>
                            </td>
                            <td class="Normal">
                            <asp:DropDownList ID="ddlGender" Width="125px" runat="server">
                                <asp:ListItem />
                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="Intersex" Text="Intersex"></asp:ListItem>
                                <asp:ListItem Value="Transgender" Text="Trans*"></asp:ListItem>
                            </asp:DropDownList>
                           </td>
                        </tr>
                        <tr>
                            <td width="200" class="Normal" valign="top">
                            <strong>Race<%=race_required_indicator %></strong>
                            </td>
                            <td class="Normal">
                                <asp:CheckBoxList ID="cblRace" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="2">
                                    <asp:ListItem Value="Asian/SouthAsian" Text="Asian/SouthAsian"></asp:ListItem>
                                    <asp:ListItem Value="Biracial" Text="Biracial"></asp:ListItem>
                                    <asp:ListItem Value="Black/African American" Text="Black/African American&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                                    <asp:ListItem Value="Latino/a" Text="Latino/a"></asp:ListItem>                   
                                    <asp:ListItem Value="Middle Eastern" Text="Middle Eastern"></asp:ListItem>
                                    <asp:ListItem Value="Multiracial" Text="Multiracial"></asp:ListItem>
                                    <asp:ListItem Value="Pacific Islander" Text="Pacific Islander"></asp:ListItem>
                                    <asp:ListItem Value="White/European-American" Text="White/European-American"></asp:ListItem>
                                </asp:CheckBoxList>                            
                           </td>
                        </tr>
                        </asp:PlaceHolder>                    
                    </table>
                    <div class="form-actions">
                        <asp:Button ID="btnUpdateProfile" ValidationGroup="profile" OnClick="btnUpdateProfile_Click" runat="server" CssClass="btn btn-primary" Text="Update Profile" /> 
                        <asp:Label id="lblMsg" ForeColor="red" runat="server" />
                                    <asp:CustomValidator ID="cvValidateAllData" runat="server"
                                        OnServerValidate = "ValidateEverything"
                                        Display = "Dynamic"
                                        ValidationGroup="profile"
                                        ValidateEmptyText="true"
                                        ControlToValidate = "txtFirstName"
                                        Text="*"
                                        ErrorMessage = "*" /> 
                     </div> 
				</div>

				<div <asp:Literal ID="litTabPermissionsClass" runat="server"></asp:Literal> id="permissions">
						<div class="control-group">
							<h4>Permissions</h4> &nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblPermissionsMessage" runat="server"></asp:Label>
							<div class="controls">
								<asp:CheckBoxList ID="cblRoles" CssClass="Normal" runat="server"></asp:CheckBoxList>
							</div>
						</div>
						<div class="form-actions">
                            <asp:Button ID="btnSavePermission" OnClick="btnSave_Click" runat="server" CssClass="btn btn-primary" Text="Update Permissions" />
						</div>
				</div>

				<div <asp:Literal ID="litTabGroupClass" runat="server"></asp:Literal> id="group">
                    <div class="row-fluid">
                        <div class="span12">
                            <h4>Groups</h4> &nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblGroupMessage" runat="server"></asp:Label>
                                <div class="control-group">
                                    <label for="name" class="control-label right"><strong>Primary Group:</strong>&nbsp;&nbsp;<asp:Label ID="lblGroupTabGroupName" runat="server"></asp:Label></label>
                                    <br />
                                    <asp:PlaceHolder ID="plhChangeGroup" runat="server" Visible="true">
                                        <strong>Add Member to Group:</strong> <asp:DropDownList runat="server" Width="300px" ID="ddlSpaces"></asp:DropDownList>
                                        <br />
                                        <asp:LinkButton ID="btnChangeGroup" runat="server" CssClass="btn btn-primary" OnClick="btnChangeGroup_Click"><i class="glyphicon-circle_plus"></i> ADD TO GROUP</asp:LinkButton>&nbsp;
                                    </asp:PlaceHolder>
                                    <br />
                                    <br />
								</div>
                            </div>
                        </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="box box-bordered">
                            <div class="box-title">
	                            <h3>
		                            Member Groups
	                            </h3>
                            </div>
                            <div class="box-content nopadding">
	                            <table class="table table-hover table-nomargin table-bordered usertable">
		                            <thead>
			                            <tr class='thefilter'>
				                            <th>Group</th>
                                            <th>Date Enrolled</th>
				                            <th>Type</th>
				                            <th class='hidden-480'>Options</th>
			                            </tr>
		                            </thead>
		                            <tbody>
                                        <asp:Panel ID="pnlUserGroups" runat="server"></asp:Panel>
		                            </tbody>
	                            </table>
                            </div>
                        </div>
                    </div>
                </div>
                    </div>
				<div <asp:Literal ID="litTabWarningsClass" runat="server"></asp:Literal> id="warnings">
				    <h4>Warnings</h4>
				</div>
			</div>
		</div>
	</div>

</asp:Content>

