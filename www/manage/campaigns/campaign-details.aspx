<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="campaign-details.aspx.cs" Inherits="manage_campaign_details" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSidebar.ascx" TagPrefix="epg" TagName="campaignsidebar" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSelector.ascx" TagPrefix="epg" TagName="CampaignSelector" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignTopNav.ascx" TagPrefix="epg" TagName="CampaignTopNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/campaigns/reports/controls/AvailableCampaignReports.ascx" TagPrefix="epg" TagName="AvailableCampaignReports" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSummaryRaw.ascx" TagPrefix="epg" TagName="CampaignSummaryRaw" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSummaryAnalyzed.ascx" TagPrefix="epg" TagName="CampaignSummaryAnalyzed" %>
<%@ Register Src="~/reports/GoogleChartReferece.ascx" TagPrefix="epg" TagName="GoogleChartReferece" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignLanguage.ascx" TagPrefix="epg" TagName="CampaignLanguage" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignMostRecentEnrolled.ascx" TagPrefix="epg" TagName="CampaignMostRecentEnrolled" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSidebarRaw.ascx" TagPrefix="epg" TagName="CampaignSidebarRaw" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignHeader.ascx" TagPrefix="epg" TagName="CampaignHeader" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignHTMLReport.ascx" TagPrefix="epg" TagName="CampaignHTMLReport" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignMostRecentHealthKits.ascx" TagPrefix="epg" TagName="CampaignMostRecentHealthKits" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignEnrollmentTrend.ascx" TagPrefix="epg" TagName="CampaignEnrollmentTrend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <epg:GoogleChartReferece runat="server" ID="GoogleChartReferece" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
    <epg:campaignsidebar runat="server" ID="campaignsidebar" />
    <epg:CampaignSidebarRaw runat="server" ID="CampaignSidebarRaw" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <epg:CampaignHeader runat="server" ID="CampaignHeader" />
<div class="row-fluid">  
    <div class="span6">                           
        <epg:CampaignSummaryRaw runat="server" ID="CampaignSummaryRaw" />
        <epg:CampaignSummaryAnalyzed runat="server" ID="CampaignSummaryAnalyzed" />
    </div>
    <div class="span6">
        <epg:CampaignLanguage runat="server" ID="CampaignLanguage" />
    </div>
</div>
<div class="row-fluid">                             
    <div class="span12">
        <epg:CampaignEnrollmentTrend runat="server" id="CampaignEnrollmentTrend" />
    </div>
</div>
<div class="row-fluid">                             
    <div class="span12">
        <epg:CampaignMostRecentEnrolled runat="server" id="CampaignMostRecentEnrolled" />
    </div>
</div>
<div class="row-fluid">                             
    <div class="span12">
        <epg:CampaignHTMLReport runat="server" ID="CampaignHTMLReport" />
    </div>
</div>
<div class="row-fluid">                             
    <div class="span12">
        <epg:CampaignMostRecentHealthKits runat="server" ID="CampaignMostRecentHealthKits" />
    </div>
</div>
</asp:Content>

