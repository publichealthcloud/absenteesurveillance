<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="task-edit.aspx.cs" Inherits="qPtl_task_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <link href="../quartz.css" rel="stylesheet" type="text/css" />

<div style="position: relative; left: 10px;">
    <br />
    <span><strong>&nbsp;<asp:Label ID="lblTitle" CssClass="CapsHeader3" runat="server" Text="Edit Task"></asp:Label></strong></span>
    <div>
    <span class="Normal"> <asp:HyperLink ID="hplBackTop" runat="server"><img src="../images/PagingPrev.gif" border="0">&nbsp;&nbsp;Back to Tasks List</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;
    <span class="Normal"> <asp:HyperLink ID="hplRefreshTop" runat="server"><img src="../images/refresh.gif" border="0">&nbsp;&nbsp;Refresh</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;    
    <asp:Button ID="btnSave_top" runat="server" Text="SAVE TASK" onclick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
    </div>
    <div style="height:10px;"></div>
    <table border="0" cellpadding="5" width="600">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
                <img src="../images/delete.gif" /> <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="return confirm('Are you sure you want to delete this task? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Task"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Task Name
            </td>
            <td>
                <asp:Label ID="lblTaskName" CssClass="Normal" runat="server"></asp:Label>      
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Importance
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtImportance" Width="25px" NumberFormat-DecimalDigits="0" MaxValue="10" MinValue="1" runat="server"></telerik:RadNumericTextBox>&nbsp;&nbsp;<span class="NormalDarkGrayItalics">(1 = lowest, 10 = highest)</span>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Status
            </td>
            <td>
                <asp:Label ID="lblStatus" CssClass="Normal" runat="server"></asp:Label>      
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Percent Complete
            </td>
            <td>
                <asp:DropDownList ID="ddlPercentCompleted" runat="server">
                    <asp:ListItem Value="0" Text="0%"></asp:ListItem>
                    <asp:ListItem Value="25" Text="25%"></asp:ListItem>
                    <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                    <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Last Updated
            </td>
            <td>
                <asp:Label ID="lblLastUpdated" CssClass="Normal" runat="server"></asp:Label>      
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Assigned To
            </td>
            <td>
                <asp:DropDownList ID="ddlAssignedTo" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
        <td width="150" class="NormalBold" valign="top">
            Task Status *
        </td>
    </tr>
    <tr>
        <td colspan="2" class="Normal">            
            <span class="Normal"> <asp:HyperLink ID="hplBackBottom" runat="server"><img src="../images/PagingPrev.gif" border="0">&nbsp;&nbsp;Back to Tasks List</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="Normal"> <asp:HyperLink ID="hplRefreshBottom" runat="server"><img src="../images/refresh.gif" border="0">&nbsp;&nbsp;Refresh</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;   
            <asp:Button ID="btnSave" runat="server" Text="SAVE TASK" onclick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
</asp:Content>


