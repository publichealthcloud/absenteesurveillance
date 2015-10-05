﻿<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="emails-list.aspx.cs" Inherits="manage_communications_emails_list" %>
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
			            <i class="icon-envelope"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Emails List</asp:Label>
		            </h3>
                    <ul class="tabs">
                        <li runat="server" id="liShare">
                            <div class="btn-group">
			                    <a href="/manage/communications/email/email-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD EMAIL</a>
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <a href="/manage/commuications/email/emails-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                    </div>
                        </li>
		            </ul>
	            </div>
                <telerik:RadGrid ID="RadGrid1" DataSourceID="siteEmails" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                    AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                    GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                            <ItemTemplate>
			                        <a href="email-edit.aspx?emailID=<%# DataBinder.Eval(Container.DataItem, "EmailID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>   
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                            DataField="EmailID" HeaderStyle-Width="40" ItemStyle-Width="40" AllowSorting="true">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="URI" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="URI" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Subject" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Subject" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="Type" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Language" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="DisplayName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Campaign" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                            DataField="CampaignName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
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
                <asp:SqlDataSource ID="siteEmails" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    runat="server"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
