<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="group-request-edit.aspx.cs" Inherits="edit_group_request" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">
    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Groups Request</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="/manage/members/group-requests-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Group Requests</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="/manage/members/group-request-edit.aspx?groupRequestID=<%= group_request_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="GROUP REQUESTS TIP" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
    <table cellpadding="5">
        <tr>
            <td colspan="2" class="NormalBold">TOOLS:&nbsp;&nbsp;<asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this group request? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Group Request"></asp:LinkButton></td>
        </tr>
        <tr>
            <td width="100" colspan="2" class="validation2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
        <tr>
        <td width="150" class="NormalBold" valign="top">
        Request Submitted:
        </td>
        <td class="Normal">
                <asp:Label ID="lblRequestTimestamp" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Current Status:
        </td>
        <td>
            <asp:DropDownList Enabled="false" runat="server" ID="ddlStatus" Width="450px">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Pending" Text="PENDING - Waiting on Internal Approval"></asp:ListItem>
                <asp:ListItem Value="Pending-WaitingPrincipalApproval" Text="PENDING - Waiting on Principal/Head of School Approval"></asp:ListItem>
                <asp:ListItem Value="Completed" Text="COMPLETED - Advisor Emailed Invitation"></asp:ListItem>
                <asp:ListItem Value="Rejected-Internal" Text="REJECTED by Interal"></asp:ListItem>
                <asp:ListItem Value="Rejected-Principal" Text="REJECTED by Principal/Head of School"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <span class="validation2">Internal Approval</span>
        </td>
        <td class="Normal">
            <asp:Button ID="btnApproveRequest" CssClass="btn btn-primary" runat="server" Text="Approve Request & Send Email to Principal/Head of School" OnClick="btnApproveRequest_Click" />
            <asp:Label ID="lblApprovalInfo" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <span class="validation2">Principal/Head of School Approval</span>
        </td>
        <td class="Normal">
            <asp:Label ID="lblPrincipalApprval" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        ADVISOR INFO
        </td>
        <td class="Normal">
            <asp:Literal runat="server" ID="litAdvisorInfo"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <span class="validation2">CONFIRM - Advisor Email</span>
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtAdvisorEmail" Width="400px" runat="server"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        ADVISOR NOTES
        </td>
        <td>
            <telerik:RadTextBox ID="txtAdvisorNotes" TextMode="MultiLine" runat="server" Style="vertical-align: middle; margin-top: 10px;" Height="80px" Width="460px" Font-Italic="True" 
            ForeColor="Gray" EmptyMessage="Enter Any Advisor Notes" Font-Names="Arial"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        GROUP INFO
        </td>
        <td class="Normal">
            <asp:Literal runat="server" ID="litGroupInfo"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <span class="validation2">CONFIRM - Group Name</span>
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtGroupName" Width="400px" runat="server"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        GROUP NOTES
        </td>
        <td>
            <telerik:RadTextBox ID="txtGroupNotes" TextMode="MultiLine" runat="server" Style="vertical-align: middle; margin-top: 10px;" Height="80px" Width="460px" Font-Italic="True" 
            ForeColor="Gray" EmptyMessage="Enter Any Group Notes" Font-Names="Arial"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        SCHOOL INFO
        </td>
        <td class="Normal">
            <span class="NormalBoldDarkGray">School:</span><asp:DropDownList ID="ddlSchools" runat="server" Width="450px"></asp:DropDownList> * MUST BE VALID SELECTION BEFORE REQUEST CAN BE APPROVED<br />
            * If school name is not in the list above, <a href="/manage/site/schools/schools-list.aspx">click here to create the school</a> and then return to complete the request.<br />        
            <asp:Literal runat="server" ID="litSchoolInfo"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        SCHOOL NOTES
        </td>
        <td>
            <telerik:RadTextBox ID="txtSchoolNotes" TextMode="MultiLine" runat="server" Style="vertical-align: middle; margin-top: 10px;" Height="80px" Width="460px" Font-Italic="True" 
            ForeColor="Gray" EmptyMessage="Enter Any School Notes" Font-Names="Arial"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        PRINCIPAL/HEAD OF SCHOOL INFO
        </td>
        <td class="Normal">
            <asp:Literal runat="server" ID="litPrincipalInfo"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <span class="validation2">CONFIRM - Principal/Head of School Email</span>
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtPrincipalEmail" Width="400px" runat="server"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        PRINCICAL/HEAD OF SCHOOL NOTES
        </td>
        <td>
            <telerik:RadTextBox ID="txtPrincipalNotes" TextMode="MultiLine" runat="server" Style="vertical-align: middle; margin-top: 10px;" Height="80px" Width="460px" Font-Italic="True" 
            ForeColor="Gray" EmptyMessage="Enter Any Principal/Head of School Notes" Font-Names="Arial"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <hr />
        </td>
    </tr>
    </table>
    <div class="box">
        <div class="box-title">
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <a href="/manage/members/group-requests-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Group Requests</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="/manage/members/edit-group-request.aspx?tipID=<%= group_request_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE GROUP REQUEST" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>


