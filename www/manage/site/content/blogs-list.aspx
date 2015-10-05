﻿<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="blogs-list.aspx.cs" Inherits="qSoc_blog_list" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Blogs</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">
			        <a href="/manage/site/content/blog-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD BLOG</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/site/content/blogs-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="siteBlogs" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="LastName" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                    <ItemTemplate>
                        <a href="blog-edit.aspx?blogID=<%# DataBinder.Eval(Container.DataItem, "BlogID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="BlogID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Title" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Title" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Summary" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Summary" HeaderStyle-Width="200" ItemStyle-Width="200" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Highlighted" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Highlighted" HeaderStyle-Width="25" ItemStyle-Width="25" AllowSorting="true" FilterControlWidth="20">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                        HeaderStyle-Width="100px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2020" DbSelectedDate='<%# startDate %>' />
                            <br />To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="ToDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2020" DbSelectedDate='<%# endDate %>' />
                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                <script type="text/javascript">
                                    function FromDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var ToPicker = $find('<%# ((GridItem)Container).FindControl("ToDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(sender);
                                        var toDate = FormatSelectedDate(ToPicker);

                                        tableView.filter("Created", fromDate + " " + toDate, "Between");

                                    }
                                    function ToDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(FromPicker);
                                        var toDate = FormatSelectedDate(sender);

                                        tableView.filter("Created", fromDate + " " + toDate, "Between");
                                    }
                                    function FormatSelectedDate(picker) {
                                        var date = picker.get_selectedDate();
                                        var dateInput = picker.get_dateInput();
                                        var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                        return formattedDate;
                                    }
                                </script>

                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Visible On Site" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Theme" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="ThemeName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="Username" HeaderText="Posted By"
                    UniqueName="UserName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="75" ItemStyle-Width="75">
                    <ItemTemplate>
                        <a href="#" onclick="openUserWindow('2', '<%# DataBinder.Eval(Container.DataItem, "UserID") %>'); return false;">
                            <img src="../images/magnifying_glass.gif" width="12" height="13" border="0" />&nbsp;<%# DataBinder.Eval(Container.DataItem, "UserName") %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="ApprovedStatus" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Uploaded From" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="UploadedFrom" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
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
        <asp:SqlDataSource ID="siteBlogs" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</div>
</asp:Content>