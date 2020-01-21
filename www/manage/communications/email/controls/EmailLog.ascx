<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailLog.ascx.cs" Inherits="manage_communications_email_controls_EmailLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-envelope"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Email Log</asp:Label>
		            </h3>
                    <ul class="tabs">
                        <li>
                            <div class="btn-group">
                                <a href="/manage/commuications/email/email-log.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                    </div>
                        </li>
		            </ul>
	            </div>
                <div style="padding-left: 5px; padding-top: 5px; background-color: #EEEEEE">
                    <strong>Filter List by Campaign: </strong>
                    <asp:DropDownList ID="ddlCampaigns" runat="server" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignList_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <br />
                <telerik:RadGrid ID="RadGrid1" DataSourceID="siteEmailLog" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                    AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                    GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                            DataField="EmailLogID" HeaderStyle-Width="40" ItemStyle-Width="40" AllowSorting="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Timestamp" DataField="Timestamp" HeaderText="Sent" AllowSorting="true" HeaderStyle-Width="125px">
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

                                            tableView.filter("Timestamp", fromDate + " " + toDate, "Between");

                                        }
                                        function ToDateSelected(sender, args) {
                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                            var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                            var fromDate = FormatSelectedDate(FromPicker);
                                            var toDate = FormatSelectedDate(sender);

                                            tableView.filter("Timestamp", fromDate + " " + toDate, "Between");
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
                        <telerik:GridBoundColumn HeaderText="Sent To" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="EmailAddress" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Subject" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Subject" HeaderStyle-Width="200" ItemStyle-Width="200" AllowSorting="true" FilterControlWidth="100">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="URI" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="URI" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="EmailType" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Read" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="ReadTime" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Click" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="ClickThruTime" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
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
                <asp:SqlDataSource ID="siteEmailLog" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    runat="server"></asp:SqlDataSource>
            </div>
        </div>
    </div>