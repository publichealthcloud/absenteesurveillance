<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignAllEnrolledScrolling.ascx.cs" Inherits="manage_campaigns_controls_CampaignAllEnrolledScrolling" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteData" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" 
            ShowGroupPanel="False" AutoGenerateColumns="False" GridLines="None">
           <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="true" />
            </ClientSettings>
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridBoundColumn HeaderText="UserID" HeaderButtonType="TextButton" DataField="UserID" HeaderStyle-Width="25" ItemStyle-Width="25">
                </telerik:GridBoundColumn>
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
    <asp:SqlDataSource ID="siteData" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>