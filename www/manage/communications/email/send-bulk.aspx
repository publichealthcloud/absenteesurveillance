<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="send-bulk.aspx.cs" Inherits="qCom_send_bulk" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div style="position: relative; left: 10px; top:100;">
    <br /><br />
    <table width="100%" cellpadding="5" border="0">
        <tr>
            <td width="100">
                <span class="NormalBold">NOTE:</span>
            </td>
            <td>
                <span class="NormalItalics">This page allows you to immediately run all active send events. ONLY use this option if the system is not automated OR a bulk email needs to be sent AFTER the daily scheduled send time.
        </span></td>
        </tr>
        <tr>
            <td width="100">
            </td>
            <td>
                <asp:Button ID="btnSendBulk" runat="server" Text="Process All Send Events Now" CssClass="btn btn-primary" OnClick="btnSendBulk_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" /><br /><br />
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

