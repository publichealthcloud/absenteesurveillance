<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="create-email-event.aspx.cs" Inherits="qCom_create_email_event" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<link href="../quartz.css" rel="stylesheet" type="text/css" />
<div style="position: relative; left: 10px; top:100;">
    <br /><br />
<table>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 1: Select Email Recipients</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlSearches" runat="server" Width="500px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 2: Select Email Template</span>
        </td>
        <td>
            <asp:DropDownList ID="ddlEmails" runat="server" Width="500px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 3: Select Start Date</span>
        </td>
        <td>
           <telerik:RadDatePicker ID="datStart" runat="server">
           </telerik:RadDatePicker>           
        </td>
    </tr>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 4: Send Every Day</span>
        </td>
        <td>
            <asp:RadioButtonList ID="rblRecurring" CssClass="Normal" runat="server">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 5: Is this Send Event currently Active?</span>
        </td>
        <td>
            <asp:RadioButtonList ID="rblRunning" CssClass="Normal" runat="server">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td width="250" valign="top">
        <span class="NormalBold">Step 6: Include Header/Footer/Unsubscribe</span>
        </td>
        <td>
            <span class="NormalBold">Include the standard Header
            <asp:RadioButtonList ID="rblHeader" CssClass="Normal" runat="server">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
            <br>
            Include the standard Footer
            <asp:RadioButtonList ID="rblFooter" CssClass="Normal" runat="server">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
            <br>
            Include the unsubscribe block</span>
            <asp:RadioButtonList ID="rblUnsubscribe" CssClass="Normal" runat="server">
                <asp:ListItem>Yes</asp:ListItem>
                <asp:ListItem>No</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td width="250">
        </td>
            
        <td><asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save Send Event" />
        </td>
    </tr>

</table>
</div>
</asp:Content>

