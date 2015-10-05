﻿<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="themes-list.aspx.cs" Inherits="qSoc_theme_list" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Themes</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">
			        <a href="/manage/site/metadata/theme-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD THEME</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/site/metadata/theme-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="siteThemes" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="Created" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                    <ItemTemplate>
                        <a href="theme-edit.aspx?themeID=<%# DataBinder.Eval(Container.DataItem, "ThemeID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="ThemeID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Theme" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Name" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Description" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Description" HeaderStyle-Width="200" ItemStyle-Width="200" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Available" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Active Site Nav" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="URL" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
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
        <asp:SqlDataSource ID="siteThemes" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</div>
</asp:Content>
