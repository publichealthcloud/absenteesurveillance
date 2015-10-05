<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignMostRecentEnrolled.ascx.cs" Inherits="manage_campaigns_controls_CampaignMostRecentEnrolled" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <div class="box">
        <div class="box-title">
			<h3>
                <i class="glyphicon-table"></i>
                Last 10 Enrolled Members
			</h3>
            <ul class="tabs">
                <li>
                    <div class="btn-group">
                        <a href="/manage/campaigns/enrolled-members.aspx?campaignID=<%=CampaignID %>" class="btn"> View All Enrolled Members</a> 
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
                <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" DataField="UserName" HeaderStyle-Width="100" ItemStyle-Width="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Enrolled" HeaderButtonType="TextButton" DataField="Enrolled" HeaderStyle-Width="75" ItemStyle-Width="75">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Enrollment Method" HeaderButtonType="TextButton" DataField="EnrollmentType" HeaderStyle-Width="50" ItemStyle-Width="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Days into Campaign" HeaderButtonType="TextButton" DataField="DaysInCampaign" HeaderStyle-Width="50" ItemStyle-Width="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" DataField="CampaignStatus" HeaderStyle-Width="50" ItemStyle-Width="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Language Preference" HeaderButtonType="TextButton" DataField="Language" HeaderStyle-Width="50" ItemStyle-Width="50">
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