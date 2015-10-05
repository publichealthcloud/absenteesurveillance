<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="sms-message-log.aspx.cs" Inherits="manage_communications_messaging_sms_messages_log" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/communications/messaging/controls/SMSMessageLog.ascx" TagPrefix="epg" TagName="SMSMessageLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:SMSMessageLog runat="server" ID="SMSMessageLog" />
</asp:Content>

