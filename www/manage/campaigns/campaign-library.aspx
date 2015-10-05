﻿<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="campaign-library.aspx.cs" Inherits="manage_campaign_details" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSidebar.ascx" TagPrefix="epg" TagName="campaignsidebar" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSelector.ascx" TagPrefix="epg" TagName="CampaignSelector" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignTopNav.ascx" TagPrefix="epg" TagName="CampaignTopNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
    <epg:campaignsidebar runat="server" ID="campaignsidebar" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="row-fluid">                  
    <div class="span12">
        <div class="box">
            <div class="box-title">
                <epg:CampaignSelector runat="server" ID="CampaignSelector" />
                <epg:CampaignTopNav runat="server" ID="CampaignTopNav" />
            </div>
	        <div class="box-content nopadding">
                <br />
                <asp:Literal ID="litMainContent" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
</div>
<div class="row-fluid">                  
    <div class="span12">
        <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Skin="Sitefinity" Width="750px" Height="600px" 
            EnableCreateNewFolder="True">
            <Configuration MaxUploadFileSize="256000000" />
        </telerik:RadFileExplorer>
    </div>
</div>
</asp:Content>
