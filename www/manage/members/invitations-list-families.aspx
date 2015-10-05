<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="invitations-list-families.aspx.cs" Inherits="qPtl_invitations_list_families" %>
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
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Family Invitations</asp:Label>
		            </h3>
                    <ul class="tabs">
                        <li runat="server" id="liShare">
                            <div class="btn-group">
			                    <a href="/manage/members/family-invitation-edit.aspx?audience=family" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD FAMILY INVITATION</a>
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <a href="/manage/members/invitations-list-families.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                    </div>
                        </li>
                        <li>
                            <div class="btn-group">
                                <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                    </div>
                        </li>
		            </ul>
	            </div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="familyInvitations" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
            GridLines="None">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Family" FieldName="FamilyName"></telerik:GridGroupByField>
                        </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="FamilyName" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="FamilyID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="FamilyName" HeaderText="Family" FilterControlWidth="75px"
                    UniqueName="FamilyTitle" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="60" ItemStyle-Width="60">
                    <ItemTemplate>
                        <a href="family-edit.aspx?familyID=<%# DataBinder.Eval(Container.DataItem, "FamilyID") %>"><%# DataBinder.Eval(Container.DataItem, "FamilyName") %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Code" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" DataField="InviteCode" HeaderStyle-Width="50" 
                    ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" DataField="InvitationAudience" HeaderStyle-Width="50" 
                    ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Role" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="RoleName" HeaderStyle-Width="60" ItemStyle-Width="60" AllowSorting="true" FilterControlWidth="60">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Available" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="StartDate" HeaderText="Valid Dates" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "StartDate") %> - <%# DataBinder.Eval(Container.DataItem, "EndDate") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="InvitationStatus" HeaderText="Status" FilterControlWidth="75" UniqueName="Status" HeaderStyle-Width="100" AllowFiltering="true" AutoPostBackOnFilter="true">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="plhRedeemable" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "InvitationStatus") == "Redeemable") %>'>
                            Redeemable
                        </asp:Placeholder>
                        <asp:Placeholder ID="plhRedeemed" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "InvitationStatus") == "Redeemed") %>'>
                            Redeemed by <a href="#" onclick="openUserWindow('2', '<%# DataBinder.Eval(Container.DataItem, "UserID") %>'); return false;">
                            <img src="../images/magnifying_glass.gif" width="12" height="13" border="0" />&nbsp;<%# DataBinder.Eval(Container.DataItem, "UserName") %></a>
                        </asp:Placeholder>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn UniqueName="UserCreated" DataField="UserCreated" HeaderText="Date Redeemed" AllowSorting="true"
                        HeaderStyle-Width="165px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate="1/1/2009" MaxDate="1/1/2020" DbSelectedDate='<%# startDate %>' />
                            to
                            <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="ToDateSelected"
                                MinDate="1/1/2009" MaxDate="1/1/2020" DbSelectedDate='<%# endDate %>' />
                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                <script type="text/javascript">
                                    function FromDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var ToPicker = $find('<%# ((GridItem)Container).FindControl("ToDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(sender);
                                        var toDate = FormatSelectedDate(ToPicker);

                                        tableView.filter("UserCreated", fromDate + " " + toDate, "Between");

                                    }
                                    function ToDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(FromPicker);
                                        var toDate = FormatSelectedDate(sender);

                                        tableView.filter("UserCreated", fromDate + " " + toDate, "Between");
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
                <telerik:GridTemplateColumn DataField="Created" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                    <ItemTemplate>
                        <a href="family-invitation-edit.aspx?invitationID=<%# DataBinder.Eval(Container.DataItem, "InvitationID") %>&audience=family" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i>
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
                </div>
            </div>
        </div>
        <asp:SqlDataSource ID="familyInvitations" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</asp:Content>
