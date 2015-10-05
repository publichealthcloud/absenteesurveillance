<%@ Page Title="" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" CodeFile="schools-list.aspx.cs" Inherits="custom_member_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contents" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="glyphicon-building"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Schools List</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare" runat="server" visible="false">
                <div class="btn-group">
			        <a href="/manage/school-districts/school-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD SCHOOL</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/school-districts/schools-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
   <telerik:RadGrid ID="RadGrid1" DataSourceID="siteSchools" runat="server" PageSize="100" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None">
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="SchoolID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="School" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="School" HeaderStyle-Width="125" ItemStyle-Width="125" AllowSorting="true" FilterControlWidth="100">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Level" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="SchoolLevel" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="40">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="SchoolType" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="40">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="City" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="City" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="State" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="StateProvince" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="25">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Available" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
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
    <asp:SqlDataSource ID="siteSchools" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="System.Data.SqlClient" 
        runat="server"></asp:SqlDataSource>
</asp:Content>
<%--</asp:Content>--%>
