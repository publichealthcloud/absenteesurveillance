<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignHeader.ascx.cs" Inherits="manage_campaigns_controls_CampaignHeader" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignSelector.ascx" TagPrefix="epg" TagName="CampaignSelector" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignTopNav.ascx" TagPrefix="epg" TagName="CampaignTopNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="row-fluid">                  
    <div class="span12">
        <div class="box">
            <div class="box-title">
                <epg:CampaignSelector runat="server" ID="CampaignSelector" />
                <epg:CampaignTopNav runat="server" ID="CampaignTopNav" />
            </div>
        </div>
    </div>
</div>
<div class="row-fluid">                  
    <div class="span12">
        <h2><asp:Literal ID="litCampaignName" runat="server"></asp:Literal></h2>
        <asp:Literal ID="litDateReportGenerated" runat="server"></asp:Literal><p>&nbsp;</p>
        <div style="height:10px;"></div>
    </div>
</div>
