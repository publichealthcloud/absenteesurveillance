<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="member-communications.aspx.cs" Inherits="manage_members_member_communications" %>
<%@ Register Src="~/manage/members/controls/MemberNav.ascx" TagPrefix="epg" TagName="MemberNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/members/member-list.aspx">Member List</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/members/member-profile.aspx?userID=<%= profile_id %>">Member Profile (<%= username %>)</a>
				</li>
			</ul>
			<div class="close-bread">
				<a href="#"><i class="icon-remove"></i></a>
			</div>
		</div>
    </div>   
</div>
	<div class="box box-bordered box-color lightgrey">
		<div class="box-title">
			<epg:MemberNav runat="server" ID="MemberNav" />
		</div>
		<div class="box-content nopadding">
			<ul class="tabs tabs-inline tabs-top">
				<li <asp:Literal ID="lit1Class" runat="server"></asp:Literal>>
					<a href="#1" data-toggle='tab'><i class="icon-info-sign"></i> Overview</a>
				</li>
				<li <asp:Literal ID="lit2Class" runat="server"></asp:Literal>>
					<a href="#2" data-toggle='tab'><i class="icon-envelope"></i> Email</a>
				</li>
				<li <asp:Literal ID="lit3Class" runat="server"></asp:Literal>>
					<a href="#3" data-toggle='tab'><i class="glyphicon-conversation"></i> Chat</a>
				</li>
				<li <asp:Literal ID="lit4Class" runat="server"></asp:Literal>>
					<a href="#4" data-toggle='tab'><i class="icon-mobile-phone"></i> Text Messaging</a>
				</li>
			</ul>
			<div class="tab-content padding tab-content-inline tab-content-bottom">
				<div <asp:Literal ID="litTab1Class" runat="server"></asp:Literal> id="1">
					<asp:Label class="NormalRed" ID="lblTab1Message" runat="server"></asp:Label>	
                    <div class="row-fluid">
					    <div class="span12">
                            <h4>Email Activity</h4>
                            <asp:Literal ID="litNumEmails" runat="server"></asp:Literal>
					    </div>
			        </div>
				</div>

				<div <asp:Literal ID="litTab2Class" runat="server"></asp:Literal> id="2">
                     <h4>Email</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab2Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
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
                                        DataField="EmailAddress" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="40">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Subject" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="Subject" HeaderStyle-Width="200" ItemStyle-Width="200" AllowSorting="true" FilterControlWidth="100">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="URI" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="URI" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="Type" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Language" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="DisplayName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Campaign" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="CampaignName" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="40">
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

				<div <asp:Literal ID="litTab3Class" runat="server"></asp:Literal> id="3">
                     <h4>Chat</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab3Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            No chat activity.
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab4Class" runat="server"></asp:Literal> id="4">
                     <h4>Text Messaging</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab4Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            No text messaging activity.
					    </div>
			        </div>               
				</div>
			</div>
		</div>
	</div>

</asp:Content>

