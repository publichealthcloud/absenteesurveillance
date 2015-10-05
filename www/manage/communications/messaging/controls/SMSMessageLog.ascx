<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SMSMessageLog.ascx.cs" Inherits="manage_communications_messaging_controls_SMSMessageLog" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-envelope"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Text Message (SMS Message) Log</asp:Label>
		            </h3>
                    <ul class="tabs">
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
                <telerik:RadGrid ID="gridSMSmessageLog" DataSourceID="siteSMSLog" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                    AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                    GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="15"
                            DataField="SMSMessageLogID" HeaderStyle-Width="30" ItemStyle-Width="30" AllowSorting="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Timestamp" DataField="Timestamp" HeaderText="Created" AllowSorting="true"
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

                                            tableView.filter("Timestamp", fromDate + " " + toDate, "Between");

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
                        <telerik:GridBoundColumn HeaderText="Phone" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="MobilePhoneNumber" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Direction" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Direction" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="URI" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="MessageURI" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MessageText" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="MessageText" HeaderStyle-Width="300" ItemStyle-Width="300" AllowSorting="true" FilterControlWidth="100">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Language" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="DisplayName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Campaign" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="CampaignName" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Username" HeaderStyle-Width="60" ItemStyle-Width="60" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="View Member" AllowFiltering="false" HeaderStyle-Width="30" ItemStyle-Width="30">
                            <ItemTemplate>
			                    <a href="/manage/members/member-profile.aspx?userID=<%# DataBinder.Eval(Container.DataItem, "ProfileUserID") %>" target="_blank" class="btn btn-primary" rel="tooltip" title="View Member"><i class="icon-external-link"></i></a>  
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
                <asp:SqlDataSource ID="siteSMSLog" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    runat="server"></asp:SqlDataSource>
            </div>
        </div>
    </div>