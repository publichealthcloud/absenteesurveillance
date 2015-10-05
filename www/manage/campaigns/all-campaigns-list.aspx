<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="all-campaigns-list.aspx.cs" Inherits="manage_manage_all_campaigns_list" %>

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
                            <li>
                                <div class="btn-group">
                                    <a href="/manage/site/learning/campaigns-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                        </div>
                            </li>
                            <li>
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                        </div>
                            </li>
		                </ul>
	                </div>
                    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteCampaigns" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                        GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                        <Columns>
                            <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                                <ItemTemplate>
			                        <a href="campaign-report.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>" class="btn btn-primary" rel="tooltip" title="View Report"><i class="icon-pencil"></i></a>  
                    	            <a target="_blank" href="/social/learning/campaign/campaign-list/campaign-details.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>" class="btn" rel="tooltip" title="View in Site"><i class="icon-external-link"></i></a>           
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                                DataField="CampaignID" HeaderStyle-Width="40" ItemStyle-Width="40" AllowSorting="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="CampaignName" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="CampaignName" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Description" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="Description" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Available In Site" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="Available" AllowSorting="true" HeaderStyle-Width="100" ItemStyle-Width="100" FilterControlWidth="50">
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
                    <asp:SqlDataSource ID="siteCampaigns" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        ProviderName="System.Data.SqlClient" 
                        runat="server"></asp:SqlDataSource>
                </div>
            </div>
        </div>
</asp:Content>

