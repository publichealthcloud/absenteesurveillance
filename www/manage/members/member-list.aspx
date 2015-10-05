<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="member-list.aspx.cs" Inherits="custom_member_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-user"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Member List</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare" runat="server">
                <div class="btn-group">
			        <a href="/manage/members/add-member.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD MEMBER</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/members/member-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
			        <a href="#" data-toggle="dropdown" class="btn dropdown-toggle"><i class="icon-filter"></i> Filter by Member Type <span class="caret"></span></a>
			        <ul class="dropdown-menu">
				        <li><a href="member-list.aspx">All Members</a></li>
                        <li><a href="member-list.aspx?searchType=active-only">Active Members</a></li>
                        <li><a href="member-list.aspx?searchType=inactive-only">Inactive Members</a></li>
                        <li><a href="member-list.aspx?searchType=advisors-only">Advisors Only</a></li>
                        <li><a href="member-list.aspx?searchType=hosts-only">Hosts Only</a></li>
                        <li><a href="member-list.aspx?searchType=admins-only">Admins Only</a></li>
			        </ul>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
    <telerik:RadGrid ID="RadGrid1" runat="server" CaseSensitive="false" EnableLinqExpressions="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Position="Bottom" Width="100%">        
        <ExportSettings IgnorePaging="true" OpenInNewWindow="true">
            <Pdf AllowAdd="false" AllowCopy="true" AllowModify="true" AllowPrinting="true" Author="Anonymous"
                Keywords="None" PageBottomMargin="1in" PageLeftMargin="1in" PageRightMargin="1in"
                PageTopMargin="1in" PageTitle="RadGrid export document" Subject="RadGrid Export"
                Title="RadGrid export" PaperSize="Letter" />
        </ExportSettings>
        <MasterTableView DataKeyNames="UserID" Width="100%">
            <RowIndicatorColumn>
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn Visible="True">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                    <ItemTemplate>
			                <a href="member-profile.aspx?userID=<%# DataBinder.Eval(Container.DataItem, "UserID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>             
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="UserID" Groupable="false" AllowFiltering="true"
                    HeaderText="UserID" UniqueName="UserID" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="20" HeaderStyle-Width="20" ItemStyle-Width="20">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" Groupable="false" AllowFiltering="true"
                    HeaderText="Username" UniqueName="UserName" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="100" HeaderStyle-Width="120" ItemStyle-Width="120">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FirstName" Groupable="false" AllowFiltering="true"
                    HeaderText="First Name" UniqueName="FirstName" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="40" HeaderStyle-Width="60" ItemStyle-Width="60">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastName" Groupable="false" AllowFiltering="true"
                    HeaderText="Last Name" UniqueName="LastName" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="40" HeaderStyle-Width="60" ItemStyle-Width="60">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Email" Groupable="false" AllowFiltering="true"
                    HeaderText="Email" UniqueName="Email" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="100" HeaderStyle-Width="60" ItemStyle-Width="60">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="AccountStatus" HeaderText="Status" AllowFiltering="false" HeaderStyle-Width="100" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="plhModerator" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "AccountStatus") == "Active" || (string)DataBinder.Eval(Container.DataItem, "AccountStatus") == "Mobile Only") %>'>
                            <span class="label label-satgreen">Active</span>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhOther" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "AccountStatus") != "Active"  && (string)DataBinder.Eval(Container.DataItem, "AccountStatus") != "Mobile Only") %>'>
                            <span class="label label-lightred">Inactive</span>
                        </asp:PlaceHolder>			                             
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
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
                <telerik:GridTemplateColumn DataField="HighestRole" HeaderText="Role" Groupable="false"
                    UniqueName="Highestrole" SortExpression="Highestrole" AllowFiltering="true" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="60" ItemStyle-Width="60" FilterControlWidth="80">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="plhModerator" runat="server" Visible='<%# (DataBinder.Eval(Container.DataItem, "HighestRole") != DBNull.Value) ? ((string)DataBinder.Eval(Container.DataItem, "HighestRole") == "Moderator") : false %>'>
                            Advisor
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhOther" runat="server" Visible='<%# (DataBinder.Eval(Container.DataItem, "HighestRole") != DBNull.Value) ? ((string)DataBinder.Eval(Container.DataItem, "HighestRole") != "Moderator") : false %>'>
                            <%# DataBinder.Eval(Container.DataItem, "HighestRole") %>
                        </asp:PlaceHolder>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridBoundColumn DataField="SpaceShortName" Groupable="false" AllowFiltering="true"
                    HeaderText="Group" UniqueName="SpaceShortName" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="60" HeaderStyle-Width="60" ItemStyle-Width="60">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastTimeSeen" Groupable="false" AllowFiltering="false"
                    HeaderText="Last Online" UniqueName="LastTimeSeen" ReadOnly="True"
                    HeaderStyle-Width="60" ItemStyle-Width="60">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
    </div>
</asp:Content>
<%--</asp:Content>--%>
