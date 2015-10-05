<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="campaign-pages.aspx.cs" Inherits="manage_campaign_details" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSidebar.ascx" TagPrefix="epg" TagName="campaignsidebar" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSelector.ascx" TagPrefix="epg" TagName="CampaignSelector" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignTopNav.ascx" TagPrefix="epg" TagName="CampaignTopNav" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignPages.ascx" TagPrefix="epg" TagName="CampaignPages" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
    <epg:campaignsidebar runat="server" ID="campaignsidebar" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/spaces/default.aspx">Program Home</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/campaigns/campaign-pages.aspx?campaignID=<%= CampaignID %>">Campaign Pages</a>
				</li>
			</ul>
			<div class="close-bread">
				<a href="#"><i class="icon-remove"></i></a>
			</div>
		</div>
    </div>   
</div>
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
</asp:Content>

