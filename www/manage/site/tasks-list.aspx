<%@ Page Title="Task List" Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="tasks-list.aspx.cs" Inherits="manage_site_tasks_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-check"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Tasks">Tasks</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="/manage/site/tasks-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
			        <a href="#" data-toggle="dropdown" class="btn dropdown-toggle"><i class="icon-filter"></i> Filter by Task Status <span class="caret"></span></a>
			        <ul class="dropdown-menu">
				        <li><a href="tasks-list.aspx?searchType=all">All Tasks</a></li>
                        <li><a href="tasks-list.aspx?searchType=pending">Pending Tasks</a></li>
                        <li><a href="tasks-list.aspx?searchType=completed">Completed Tasks</a></li>
			        </ul>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteTasks" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None" onitemcommand="RadGrid1_ItemCommand">
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="35" ItemStyle-Width="35">
                <ItemTemplate>
                    <asp:PlaceHolder ID="plhWarning" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "Name") == "Pending Banned Word Warning") %>'>
                        <a href="/manage/members/warning-edit.aspx?taskID=<%# DataBinder.Eval(Container.DataItem, "TaskID") %>&warningID=<%# DataBinder.Eval(Container.DataItem, "ReferenceID") %>&status=pending&source=tasks" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhSiteReport" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "Name") == "Pending Site Report") %>'>
                        <a href="/manage/site/site-report-edit.aspx?taskID=<%# DataBinder.Eval(Container.DataItem, "TaskID") %>&reportID=<%# DataBinder.Eval(Container.DataItem, "ReferenceID") %>&status=pending&source=tasks" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhTask" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "Name") != "Pending Banned Word Warning" && (string)DataBinder.Eval(Container.DataItem, "Name") != "Pending Site Report") %>'>
                        <a href="/manage/site/task-edit.aspx?taskID=<%# DataBinder.Eval(Container.DataItem, "TaskID") %>&reportID=<%# DataBinder.Eval(Container.DataItem, "ReferenceID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>
                    </asp:PlaceHolder>             
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                DataField="TaskID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Task Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Name" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Content" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="ContentTypeName" HeaderStyle-Width="100" ItemStyle-Width="1100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Importance" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Importance" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="25">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Status" HeaderStyle-Width="100" ItemStyle-Width="1100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Assigned To" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Username" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                    HeaderStyle-Width="120px">
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
    <asp:SqlDataSource ID="siteTasks" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>
</asp:Content>
