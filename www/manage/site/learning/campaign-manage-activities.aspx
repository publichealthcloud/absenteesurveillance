<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="campaign-manage-activities.aspx.cs" Inherits="campaign_manage_activities" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/CampaignActivitiesListEnhanced.ascx" TagPrefix="epg" TagName="CampaignActivitiesListEnhanced" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="position: relative; left: 10px;">
    <div class="box">
	    <div class="box-title">
		    <h3>
			    <i class="icon-group"></i>
			    <asp:Label ID="lblTitle" runat="server">Group</asp:Label>
		    </h3>
                <ul class="tabs">
                    <li runat="server" id="li1">
                        <div class="btn-group">
                            <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaign</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:HyperLink ID="hplRefreshTop" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                        </div>
                    </li>
                </ul>
                <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
        <div style="height:10px;"></div>
        <table border="0" cellpadding="5" width="900">
            <tr>
                <td class="Normal" width="100%">
                    <a href="/manage/site/learning/campaign-activity-edit.aspx?campaignID=<%= campaign_id %>" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD ACTIVITY</a>
                    &nbsp;
                    <a target="_blank" href="/social/learning/campaigns/campaign-details.aspx?campaignID=<%= campaign_id %>&mode=preview" class="btn"><i class="icon-search"></i> Preview Activities</a>
                    <div style="height:5px;"></div>
                    <epg:CampaignActivitiesListEnhanced runat="server" ID="CampaignActivitiesListEnhanced" />
                </td>
            </tr>
        </table>

    <div class="box">
	    <div class="box-title">
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaign</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:HyperLink ID="hplRefreshBottom" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    </div>
</div>

</asp:Content>


