<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="polls-list.aspx.cs" Inherits="polls_list" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Polls">Polls</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">
			        <a href="/manage/site/content/poll-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD POLL</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/site/content/polls-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="sitePolls" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="LastName" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                    <ItemTemplate>
                        <a href="poll-edit.aspx?pollID=<%# DataBinder.Eval(Container.DataItem, "PollID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="PollID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Poll Question" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Question" HeaderStyle-Width="250" ItemStyle-Width="250" AllowSorting="true" FilterControlWidth="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Question Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="PollType" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Response Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="ExperienceType" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Theme" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="ThemeName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Available" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Highlighted" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Highlighted" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
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
        <asp:SqlDataSource ID="sitePolls" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</div>
</asp:Content>
