<%@ Page Title="" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" AutoEventWireup="true" CodeFile="daily-district-absentee-data_extended.aspx.cs" Inherits="manage_daily_district_absentee_data_extended" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/school-districts/controls/school-district-sidebar.ascx" TagPrefix="epg" TagName="schooldistrictsidebar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="school_nav" Runat="Server">
    <epg:schooldistrictsidebar runat="server" ID="schooldistrictsidebar" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="school_main" Runat="Server">
    <div class="row-fluid">                           
        <div class="span12">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="glyphicon-notes_2"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">School Daily Absentee Data (Extended)</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="/manage/school-districts/daily-district-absentee-data.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteSchoolWeeklyAbsenteeData" runat="server" PageSize="100" Height="700px" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None" onitemcommand="RadGrid1_ItemCommand">
           <ClientSettings>
                <Scrolling AllowScroll="True" UseStaticHeaders="true" />
            </ClientSettings>
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
            <telerik:GridBoundColumn UniqueName="DataDate" DataField="DataDate" HeaderText="Calendar Day" AllowSorting="true"
                    HeaderStyle-Width="150px">
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

                                    tableView.filter("DataDate", fromDate + " " + toDate, "Between");

                                }
                                function ToDateSelected(sender, args) {
                                    var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                    var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                    var fromDate = FormatSelectedDate(FromPicker);
                                    var toDate = FormatSelectedDate(sender);

                                    tableView.filter("Last Day Period", fromDate + " " + toDate, "Between");
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
            <telerik:GridBoundColumn HeaderText="Elementary Absences" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalElementaryStudentsAbsent" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Elementary Absence Rate" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="ElementarySchoolAbsenteeRate" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Jr High Absences" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalJuniorHighStudentsAbsent" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Jr High Absence Rate" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="JuniorHighSchoolAbsenteeRate" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="High School Absences" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalHighSchoolStudentsAbsent" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="High School Absence Rate" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="HighSchoolAbsenteeRate" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Overall Absence Rate" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="OverallAbsenteeRate" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Total Abset" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalAbsent" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Total Sick" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalSick" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Gastrointestinal" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="Gastrointestinal" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Respiratory" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="Respiratory" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Rash" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="Rash" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Other Illness" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="OtherIllness" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Unknown Illness" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="UnknownIllness"  AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Total Enrolled" HeaderButtonType="TextButton" AllowFiltering="false"
                DataField="TotalEnrolled" AllowSorting="true">
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
    <asp:SqlDataSource ID="siteSchoolWeeklyAbsenteeData" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>
</div>
            </div>
        </div>
</asp:Content>
