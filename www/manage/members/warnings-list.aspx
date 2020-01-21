<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="warnings-list.aspx.cs" Inherits="manage_members_warnings_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-warning-sign"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Member List</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="/manage/members/member-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
			        <a href="#" data-toggle="dropdown" class="btn dropdown-toggle"><i class="icon-filter"></i> Filter by Warning Status <span class="caret"></span></a>
			        <ul class="dropdown-menu">
				        <li><a href="warnings-list.aspx">All Warnings</a></li>
                        <li><a href="warnings-list.aspx?searchType=teens-only">Pending Warnings</a></li>
                        <li><a href="warnings-list.aspx?searchType=advisors-only">Reviewed Warnings</a></li>
                        <li><a href="warnings-list.aspx?searchType=hosts-only">Escalated Warnings</a></li>
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
    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteWarnings" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None" onitemcommand="RadGrid1_ItemCommand">
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
            <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                <ItemTemplate>
			            <a href="warning-edit.aspx?warningID=<%# DataBinder.Eval(Container.DataItem, "WarningID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>             
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                DataField="WarningID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Warning Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="WarningType" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Content" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="ContentType" HeaderStyle-Width="100" ItemStyle-Width="1100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Banned Words" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="BannedWords" HeaderStyle-Width="100" ItemStyle-Width="1100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="Username" HeaderText="Warning Issued To"
                    UniqueName="UserName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="75" ItemStyle-Width="75">
                    <ItemTemplate>
                        <a href="/manage/members/member-profile.aspx?userID=<%# DataBinder.Eval(Container.DataItem, "ActorID") %>">
                            <i class="icon-zoom-in"></i>&nbsp;<%# DataBinder.Eval(Container.DataItem, "UserName") %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Status" HeaderStyle-Width="100" ItemStyle-Width="1100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                    HeaderStyle-Width="100px">
                    <FilterTemplate>
                        From
                        <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                            MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# startDate %>' />
                        <br />To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="ToDateSelected"
                            MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# endDate %>' />
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
    <asp:SqlDataSource ID="siteWarnings" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>
</div>
</asp:Content>
