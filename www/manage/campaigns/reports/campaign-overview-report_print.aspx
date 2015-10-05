<%@ Page Title="" Language="C#" MasterPageFile="~/manage/campaigns/reports/campaign-report.master" AutoEventWireup="true" CodeFile="campaign-overview-report_print.aspx.cs" Inherits="manage_campaigns_reports_campaign_overview" %>
<%@ Register Src="~/manage/campaigns/reports/controls/CampaignOverviewReport.ascx" TagPrefix="epg" TagName="CampaignOverviewReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="details" Runat="Server">

    <epg:CampaignOverviewReport runat="server" ID="CampaignOverviewReport" />

</asp:Content>

