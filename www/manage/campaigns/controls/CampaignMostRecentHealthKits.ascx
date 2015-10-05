<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignMostRecentHealthKits.ascx.cs" Inherits="manage_campaigns_controls_CampaignMostRecentHealthKits" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <div class="box">
        <div class="box-title">
			<h3>
                <i class="glyphicon-table"></i>
                Last 10 Health Kit Orders
			</h3>
            <ul class="tabs">
                <li>
                    <div class="btn-group">
                        <a href="/manage/campaigns/health-kit-orders.aspx?campaignID=<%=CampaignID %>" class="btn"> View All Health Kit Orders</a> 
		            </div>
                </li>
		    </ul>
		</div>
	</div>
	<div class="box-content">
        <telerik:RadGrid ID="RadGrid1" DataSourceID="siteData" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" 
            ShowGroupPanel="False" AutoGenerateColumns="False" GridLines="None">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="UserHealthKitID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Order Timestamp" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="OrderTimestamp" HeaderStyle-Width="125" ItemStyle-Width="125" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Name" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="40">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Invite Code" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="InviteCode" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Member First Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="InitFirstName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="25">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Username" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="25">
                </telerik:GridBoundColumn>
            </Columns>
            </MasterTableView>
                <ClientSettings AllowGroupExpandCollapse="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
                    AllowColumnsReorder="True">
                </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="true" />
            <FilterMenu EnableTheming="True">
                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            </FilterMenu>
        </telerik:RadGrid>
    </div>
    <asp:SqlDataSource ID="siteData" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>