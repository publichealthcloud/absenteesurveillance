<%@ Page Title="" Language="C#" MasterPageFile="~/manage/campaigns/campaign.master" AutoEventWireup="true" CodeFile="health-kit-orders.aspx.cs" Inherits="manage_campaigns_health_kit_orders" %>
<%@ Register Src="~/manage/health/controls/HealthKitsList.ascx" TagPrefix="epg" TagName="HealthKitsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="main_campaign_feature" Runat="Server">
    <epg:HealthKitsList runat="server" ID="HealthKitsList" />
</asp:Content>

