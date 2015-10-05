<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="group-requests-list.aspx.cs" Inherits="qHtl_condom_orders_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/members/controls/GroupRequestsList.ascx" TagPrefix="epg" TagName="GroupRequestsList" %>
<%@ Register Src="~/manage/members/controls/HealthProviderGroupRequestsList.ascx" TagPrefix="epg" TagName="HealthProviderGroupRequestsList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:GroupRequestsList runat="server" ID="GroupRequestsList" />
    <epg:HealthProviderGroupRequestsList runat="server" ID="HealthProviderGroupRequestsList" Visible="false" />
</asp:Content>
