<%@ Page Title="" Language="C#" MasterPageFile="~/manage/campaigns/campaign.master" AutoEventWireup="true" CodeFile="enrolled-members.aspx.cs" Inherits="manage_campaigns_enrolled_members" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignEnrolledMembers.ascx" TagPrefix="epg" TagName="CampaignEnrolledMembers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="main_campaign_feature" Runat="Server">
    <epg:CampaignEnrolledMembers runat="server" ID="CampaignEnrolledMembers" />
</asp:Content>

