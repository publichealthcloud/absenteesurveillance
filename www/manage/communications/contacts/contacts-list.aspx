<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="contacts-list.aspx.cs" Inherits="manage_communications_contacts_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-group"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Groups List</asp:Label>
		            </h3>
                    <ul class="tabs">
                        <li runat="server" id="liShare">
                            <div class="btn-group">
			                    <a href="/manage/communication/contacts/contact-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD CONTACT</a>
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <a href="/manage/commuications/contacts/contacts-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                    </div>
                        </li>
		            </ul>
	            </div>
                <telerik:RadGrid ID="RadGrid1" DataSourceID="siteContacts" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                    AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                    GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                            <ItemTemplate>
			                        <a href="contact-edit.aspx?contactID=<%# DataBinder.Eval(Container.DataItem, "ContactID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>   
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                            DataField="ContactID" HeaderStyle-Width="40" ItemStyle-Width="40" AllowSorting="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="First Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="FirstName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Last Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="LastName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Email" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Ok To Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="OkEmail" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Keywords" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Keywords" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Source" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Source" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Partner" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Partner" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Main Group" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="MainGroup" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Sub Group" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="SubGroup" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Custom HTML" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="CustomHTMLElement" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="UserID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="UserID" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="25">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                                HeaderStyle-Width="125px">
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
                <asp:SqlDataSource ID="siteContacts" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    runat="server"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
