<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupRequestsList.ascx.cs" Inherits="manage_members_controls_GroupRequestsList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Articles">Group Requests</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="/manage/site/schools/group-requests-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="siteGroupRequests" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None" GroupPanel-Enabled="false">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="LastName" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="35" ItemStyle-Width="25">
                    <ItemTemplate>
                            <a href="/manage/members/group-request-edit.aspx?groupRequestID=<%# DataBinder.Eval(Container.DataItem, "GroupRequestID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="GroupRequestID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="When Submitted"
                        HeaderStyle-Width="200px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate="11/1/2012" MaxDate="1/1/2025" DbSelectedDate='<%# startDate %>' />
                            to
                            <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="ToDateSelected"
                                MinDate="11/1/2012" MaxDate="1/1/2025" DbSelectedDate='<%# endDate %>' />
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

                                        tableView.filter("OrderDate", fromDate + " " + toDate, "Between");
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
                <telerik:GridTemplateColumn DataField="AdvisorFirstName" HeaderText="Advisor Name" AllowFiltering="false" HeaderStyle-Width="100" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "AdvisorFirstName") %> <%# DataBinder.Eval(Container.DataItem, "AdvisorLastName") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Advisor Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="AdvisorEmail" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Status" HeaderStyle-Width="250" ItemStyle-Width="250" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="School" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="OtherSchool" HeaderStyle-Width="150" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
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
        <asp:SqlDataSource ID="siteGroupRequests" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
