<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="user-health-kits-list.aspx.cs" Inherits="manage_health_user_health_kits_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/health/controls/HealthKitsList.ascx" TagPrefix="epg" TagName="HealthKitsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:HealthKitsList runat="server" ID="HealthKitsList" />
</asp:Content>
