<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="user-assessment-list.aspx.cs" Inherits="qLrn_user_training_list" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Member Assessments</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="user-training-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="userAssessments" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Assessment" FieldName="Name"></telerik:GridGroupByField>
                        </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="Name" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="UserAssessmentID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Assessment" DataField="Name" HeaderButtonType="TextButton" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="180" ItemStyle-Width="180">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="AssessmentType" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="LastName" HeaderText="Member"
                    UniqueName="LastName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="120" ItemStyle-Width="120" FilterControlWidth="50">
                    <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "FirstName") %> <%# DataBinder.Eval(Container.DataItem, "LastName") %> (<%# DataBinder.Eval(Container.DataItem, "UserName") %>)
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
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
                <telerik:GridBoundColumn HeaderText="Outcome" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Outcome" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Score" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="40"
                    DataField="Score" HeaderStyle-Width="50" ItemStyle-Width="50">
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
        <asp:SqlDataSource ID="userAssessments" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</asp:Content>
