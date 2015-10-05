<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="member-admin-tools.aspx.cs" Inherits="manage_members_member_admin_tools" %>
<%@ Register Src="~/manage/members/controls/MemberNav.ascx" TagPrefix="epg" TagName="MemberNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/members/member-list.aspx">Member List</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/members/member-admin-tools.aspx?userID=<%= profile_id %>">Member Admin Tools</a>
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
				<li <asp:Literal ID="lit1Class" runat="server"></asp:Literal>>
					<a href="#1" data-toggle='tab'><i class="icon-lock"></i> Password Reset</a>
				</li>
				<li <asp:Literal ID="lit2Class" runat="server"></asp:Literal>>
					<a href="#2" data-toggle='tab'><i class="icon-user"></i> Change Username</a>
				</li>
				<li <asp:Literal ID="lit3Class" runat="server"></asp:Literal>>
					<a href="#3" data-toggle='tab'><i class="glyphicon-ban"></i> Disable Account</a>
				</li>
				<li <asp:Literal ID="lit4Class" runat="server"></asp:Literal>>
					<a href="#4" data-toggle='tab'><i class="icon-trash"></i> Delete Account</a>
				</li>
			</ul>
			<div class="tab-content padding tab-content-inline tab-content-bottom">
				<div <asp:Literal ID="litTab1Class" runat="server"></asp:Literal> id="1">
					<h4>Password Reset</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab1Message" runat="server"></asp:Label>	
                    <div class="row-fluid">
					    <div class="span12">
                            <table border="0" cellpadding="5" width="600">
                            <tr>
                                <td height="36">
                                    <div align="right">
                                        <span class="Normal">New Password:</span></div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="left" class="validation2">
                                    <asp:TextBox ID="txtPassword" ValidationGroup="password" runat="server" Width="200" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="password" ID="vldPassword" ControlToValidate="txtPassword" runat="server" ErrorMessage="Password Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td height="36">
                                    <div align="right">
                                        <span class="Normal">Confirm Password:</span></div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="left" class="validation2">
                                    <asp:TextBox ID="txtPasswordConfirm" ValidationGroup="password" runat="server" Width="200" 
                                        TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="password" ID="vldPasswordConfirm" ControlToValidate="txtPasswordConfirm" runat="server" ErrorMessage="Password Confirmation Required"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td height="36" valign="top">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td class="validation2">
                                    <asp:CustomValidator ID="vldPasswordCompare" runat="server"
			                            OnServerValidate = "ValidatePassword"
			                            Display = "Dynamic"
			                            ControlToValidate = "txtPasswordConfirm"
			                            ErrorMessage = "Passwords must match" 
			                            ValidationGroup="password" />         
                                </td>
                            </tr>
                            <tr>
                                <td height="36" valign="top">
                                    <div align="right">
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnUpdatePassword" runat="server" ValidationGroup="password" CssClass="btn btn-primary"
                                        onclick="btnUpdatePassword_Click" Text="Update Password" />
                                        <asp:Label CssClass="validation2" runat="server" ID="lblMessage"></asp:Label>
                                </td>
                            </tr>
                            </table>
					    </div>
			        </div>
				</div>

				<div <asp:Literal ID="litTab2Class" runat="server"></asp:Literal> id="2">
                     <h4>Change Username</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab2Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            <table border="0" cellpadding="5" width="600">
                            <tr>
                                <td height="36">
                                    <div align="right">
                                        <span class="Normal">New Username:</span></div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="left" class="validation2">
                                    <asp:TextBox ID="txtUsername" ValidationGroup="username" runat="server" Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator 
                                            ValidationGroup="username" 
                                            ID="rfvNewUsername" 
                                            ControlToValidate="txtUsername" 
                                            runat="server" 
                                            ErrorMessage="New Username Required">
                                        </asp:RequiredFieldValidator>
	                                    <asp:CustomValidator ID="cvUserName" runat="server"
                                            OnServerValidate = "ValidateUserName"
                                            Display = "Dynamic"
                                            ValidationGroup="username"
                                            ControlToValidate = "txtUserName"
                                            Text=""
                                            ErrorMessage = "WARNING: Username cannot be changed. Either the username is not valid or it already exists." />
                                </td>
                            </tr>
                            <tr>
                                <td height="36">
                                    <div align="right">
                                        <span class="Normal">Confirm New Username:</span></div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td align="left" class="validation2">
                                    <asp:TextBox ID="txtUsernameConfirm" ValidationGroup="username" runat="server" Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator 
                                            ValidationGroup="username" 
                                            ID="rfvUsernameConfirm"
                                            ControlToValidate="txtUsernameConfirm" 
                                            runat="server" 
                                            ErrorMessage="Username Confirmation Required">
                                        </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td height="36" valign="top">
                                    <div align="right">
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnUpdateUsername" 
                                        OnClick="btnUpdateUsername_Click"
                                        runat="server" 
                                        ValidationGroup="username" 
                                        CssClass="btn btn-primary"
                                        Text="Update Username" />
                                </td>
                            </tr>
                            </table>
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab3Class" runat="server"></asp:Literal> id="3">
                     <h4>Change Account Status</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab3Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12"><strong>Current Account Status:</strong> &nbsp;&nbsp;
                            <asp:DropDownList ID="ddlAccountStatus" runat="server" RepeatDirection="Vertical" Width="250px">
                                <asp:ListItem Value="Active" Text="Active (Member can login)"></asp:ListItem>
                                <asp:ListItem Value="Inactive" Text="Inactive (Member CANNOT login)"></asp:ListItem>
                            </asp:DropDownList>
                            <br /><br />
                            <asp:Button ID="btnUpdateAccountStatus" runat="server" CssClass="btn btn-primary" Text="Update Member Status" OnClick="btnUpdateAccountStatus_Click" />
					    </div>
			        </div>               
				</div>
				<div <asp:Literal ID="litTab4Class" runat="server"></asp:Literal> id="4">
                     <h4>Delete Account</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab4Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12"><strong>Do you want to delete this account? WARNING: This action CANNOT be undone.</strong> &nbsp;&nbsp;
                            <asp:Button ID="btnDeleteAccount" runat="server" CssClass="btn btn-primary" Text="Delete Member" OnClick="btnDeleteAccount_Click" OnClientClick="return confirm('Are you sure you want to delete this user? This action cannot be undone.');"  />
					    </div>
			        </div>             
				</div>
            </div>
		</div>
	</div>

</asp:Content>

