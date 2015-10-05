<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="health-providers-list.aspx.cs" Inherits="manage_health_providers_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Articles">Health Providers</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">
			        <a href="/manage/health/health-provider-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD HEALTH PROVIDER</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/health/health-provider-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteHealthProviders" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None">
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn DataField="Available" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                <ItemTemplate>
                    <a href="health-provider-edit.aspx?healthProviderID=<%# DataBinder.Eval(Container.DataItem, "HealthProviderID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a> 
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                DataField="SchoolID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Name" HeaderStyle-Width="125" ItemStyle-Width="125" AllowSorting="true" FilterControlWidth="100">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Provider Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="HealthProviderType" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="40">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="City" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="City" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="State/Province" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="StateProvince" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="25">
            </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Available" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
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
    <asp:SqlDataSource ID="siteHealthProviders" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>
</div>
</asp:Content>
