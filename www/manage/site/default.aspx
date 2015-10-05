<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="manage_site_default" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/members/controls/GroupRequestsList.ascx" TagPrefix="epg" TagName="GroupRequestsList" %>
<%@ Register Src="~/manage/members/controls/HealthProviderGroupRequestsList.ascx" TagPrefix="epg" TagName="HealthProviderGroupRequestsList" %>
<%@ Register Src="~/manage/site/controls/SiteDashboard.ascx" TagPrefix="epg" TagName="SiteDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:SiteDashboard runat="server" ID="SiteDashboard" />
    <epg:GroupRequestsList StatusFilter="Pending" runat="server" ID="GroupRequestsList" />
    <epg:HealthProviderGroupRequestsList runat="server" ID="HealthProviderGroupRequestsList" />
</asp:Content>

