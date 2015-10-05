<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="space-edit.aspx.cs" Inherits="space_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/members/controls/GroupMemberListView.ascx" TagPrefix="epg" TagName="GroupMemberListView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>

<script type="text/javascript">
    $(function () {
        $(".tb").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/services/MemberGroups.asmx/FetchMemberList",
                    data: "{ 'mail': '" + request.term + "', 'user_id': '" + document.getElementById('lblJSUserID').textContent + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item.Email
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2
        });
    });
</script>

<div style="position: relative; left: 10px;">
    <div class="box">
	    <div class="box-title">
		    <h3>
			    <i class="icon-group"></i>
			    <asp:Label ID="lblTitle" runat="server">Group</asp:Label>
		    </h3>
                <ul class="tabs">
                    <li runat="server" id="li1">
                        <div class="btn-group">
                            <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Group List</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:HyperLink ID="hplRefreshTop" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE GROUP" onclick="btnSave_OnClick" />
                        </div>
                    </li>
                </ul>
                <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
        <div style="height:10px;"></div>
        <table border="0" cellpadding="5" width="600">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this group? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Group"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                <i class="icon-external-link"></i> <a target="_blank" href="/social/spaces/space-details.aspx?spaceID=<%=space_id %>">View in Site</a>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><strong><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label></strong>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Group Name *</strong>
            </td>
            <td class="NormalBold">
            <telerik:RadTextBox ID="txtGroupNameNew" MaxLength="100" runat="server" Width="250px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 100 characters</span>
            <asp:RequiredFieldValidator 
                ID="rfvGroupNameNew" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtGroupNameNew"
                ErrorMessage="Group name required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            Full Group Name (optional)
            </td>
            <td class="Normal">
            <telerik:RadTextBox ID="txtGroupNameNewFull" MaxLength="500" runat="server" Width="350px"></telerik:RadTextBox>&nbsp;
           </td>
        </tr>
         <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Group Type *</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlGroupTypeNew" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="class" Text="Class"></asp:ListItem>
                <asp:ListItem Value="club" Text="Club"></asp:ListItem>
                <asp:ListItem Value="organization" Text="Organization"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvGroupTypeNew" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlGroupTypeNew"
                ErrorMessage="Group type required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Enrollment Type *</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlEnrollmentType" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="invitation" Text="Invitation Required"></asp:ListItem>
                <asp:ListItem Value="open" Text="Open to all Members"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlGroupTypeNew"
                ErrorMessage="Group type required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">School</td>
            <td class="Normal">
                <asp:DropDownList ID="ddlSchoolNew" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Group Focus *</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlSpaceCategoriesNew" runat="server">
            </asp:DropDownList> 
            <asp:RequiredFieldValidator 
                ID="rfvGroupFocus" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlSpaceCategoriesNew"
                ErrorMessage="Group focus required">
            </asp:RequiredFieldValidator> 
           </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Visible in Site *</strong>
            </td>
            <td class="Normal">
                <asp:RadioButtonList ID="rblVisibleInDirectory" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                </asp:RadioButtonList> 
                <asp:Label ID="Label3" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Available *</strong>
            </td>
            <td class="Normal">
                <asp:RadioButtonList ID="rblAvailableNew" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="Label2" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder ID="plhMoreInfo" Visible="false" runat="server">
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Last Updated By
            </td>
            <td class="Normal">
                <a target="_blank" href="/manage/members/member-profile.aspx?userID=<%= user_id %>">
                <i class="icon-external-link"></i>&nbsp;<%= username %></a>
                <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Enrollment Information
            </td>
            <td class="Normal">
                <asp:Label ID="lblEnrollmentInfo" CssClass="NormalItalics" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhInvitations" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Invitations</strong>
            </td>
            <td class="Normal" width="600">
                <div style="height:5px;"></div>
                <table class="table table-hover table-nomargin table-striped">
	                <thead>
		                <tr>
			                <th>Group Type</th>
			                <th class="hidden-1024">Role</th>
			                <th class="hidden-480">Code</th>
		                </tr>
	                </thead>
	                <tbody>
                        <asp:Literal ID="litInvitations" runat="server"></asp:Literal>
	                </tbody>
                </table>
           </td>
        </tr>
        </asp:PlaceHolder>
    </table>

    <div class="box">
	    <div class="box-title">
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Group List</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:HyperLink ID="hplRefreshBottom" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE GROUP" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    </div>
</div>

<asp:PlaceHolder ID="plhGroupMemberList" runat="server">
<div class="row-fluid">
	<div class="span12">
		<div class="box box-bordered">
			<div class="box-title">
				<h3>
					Group Members
				</h3>
                <ul class="tabs">
                    <li runat="server" id="li2">
                        <div class="btn-group">
			                <asp:LinkButton ID="btnViewAddMember" runat="server" OnClick="btnViewAddMember_Click" CssClass="btn btn-primary"><i class="glyphicon-circle_plus"></i> ENROLL MEMBER</asp:LinkButton>
		                </div>
                    </li>
                </ul>
                <asp:PlaceHolder ID="plhAddMember" runat="server" Visible="false">
                    <br /><br /><strong>Member to add:</strong>&nbsp;&nbsp;<asp:TextBox runat="server" class="input-xlarge tb" name="textfield" ID="txtMemberUserName" placeholder="Start typing a username..."></asp:TextBox>
                    <br /><strong>Group Role:</strong>&nbsp;&nbsp;
                    <asp:RadioButtonList ID="rblNewMemberRole" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Advisor" Text="Advisor"></asp:ListItem>
                    <asp:ListItem Value="Teen" Text="Teen"></asp:ListItem>
                </asp:RadioButtonList>
                    <br />
                    <asp:Button ID="btnAddMember" runat="server" CssClass="btn btn-primary" OnClick="btnAddMember_Click" Text="Add Member" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_Click1" Text="Cancel Add Member" /> <asp:Label ID="lblMessageMember" runat="server"></asp:Label>
                </asp:PlaceHolder>
			</div>
			<div class="box-content nopadding">
			    <table class="table table-hover table-nomargin table-bordered usertable">
				    <thead>
					    <tr class='thefilter'>
                            <th>ID</th>
						    <th>Username</th>
                            <th>Name</th>
						    <th>Email</th>
						    <th class='hidden-350'>Role</th>
						    <th class='hidden-1024'>Enrolled</th>
						    <th class='hidden-480'>Options</th>
					    </tr>
				    </thead>
				    <tbody>
                        <asp:Panel ID="pnlGroupMembers" runat="server"></asp:Panel>
				    </tbody>
			    </table>
			</div>
		</div>
	</div>
</div>
</asp:PlaceHolder>
</asp:Content>


