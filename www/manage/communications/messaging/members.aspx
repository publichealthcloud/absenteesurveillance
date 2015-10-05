<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging/test-message.master" AutoEventWireup="true" CodeFile="members.aspx.cs" Inherits="text_messages_members" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Members</h1>
    <div style="background-color:lightgray; padding:4px 4px 4px 4px; text-align:left;">
        <asp:Button ID="btnNewRecord" runat="server" Text="New Member">
        </asp:Button>&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="New Member Group">
        </asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" Skin="Metro" runat="server" Text="Refresh"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnDownloadExcel" OnClick="btnDownloadExcel_Click" Skin="Metro" runat="server" Text="Download to Excel"></asp:Button>&nbsp;&nbsp;
    </div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="members" Skin="Metro" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="False" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="25"
                    DataField="UserID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="Phone1" HeaderText="Mobile Phone"
                    UniqueName="Phone1" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="120" ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Phone1") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                        HeaderStyle-Width="100px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2020" DbSelectedDate='<%# startDate %>' />&nbsp;&nbsp;To
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
                <telerik:GridTemplateColumn DataField="Username" HeaderText="Enrolled Programs"
                    UniqueName="UserName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="120" ItemStyle-Width="120">
                    <ItemTemplate>
                        Healthy Relationships, Project U LA Tips and Info
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
    <asp:SqlDataSource ID="members" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="System.Data.SqlClient" runat="server"></asp:SqlDataSource>  
</asp:Content>

