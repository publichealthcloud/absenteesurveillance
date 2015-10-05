<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="email-log.aspx.cs" Inherits="manage_communications_email_log" %>
<%@ Register Src="~/manage/communications/email/controls/EmailLog.ascx" TagPrefix="epg" TagName="EmailLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:EmailLog runat="server" ID="EmailLog" />
</asp:Content>
