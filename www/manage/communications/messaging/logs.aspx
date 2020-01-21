<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging/test-message.master" AutoEventWireup="true" CodeFile="logs.aspx.cs" Inherits="text_messages_logs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <h1><asp:Label ID="lblTitle" runat="server" Text="Message Logs"></asp:Label></h1>
        <div style="background-color:lightgray; padding:4px 4px 4px 4px; text-align:left;">
            <div style="position:relative; right:0px; top:0px; padding-bottom:0px; text-align: left;">
                <asp:Button ID="btnRefresh" Skin="Metro" runat="server" Text="All Logs"></asp:Button>
            </div>
            <div style="position:relative; right:0px; top:0px; padding-bottom:0px; text-align: right;">
                <telerik:RadComboBox ID="RadComboBox1" Skin="Metro" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                    Width="300" Label="Select a program...">
                    <Items>
                            <telerik:RadComboBoxItem Text="Healthy Relationships" />
                            <telerik:RadComboBoxItem Text="Project U LA Tips and Info" />
                            <telerik:RadComboBoxItem Text="Sexual Health" />
                            <telerik:RadComboBoxItem Text="Tobacco and Smoking" />
                    </Items>
                </telerik:RadComboBox>
                <telerik:RadComboBox ID="RadComboBox2" Skin="Metro" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                        Width="300" Label="Select a calendar event...">
                        <Items>
                             <telerik:RadComboBoxItem Text="Weekly Tip: Tuesday 4pm" />
                        </Items>
                   </telerik:RadComboBox>
                <asp:Button ID="Button1" Skin="Metro" runat="server" Text="Refresh Logs"></asp:Button>
            </div>
        </div>
        <telerik:RadGrid ID="RadGrid1" Skin="Metro" DataSourceID="smsLogs" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <GroupByExpressions>
                <telerik:GridGroupByExpression>
                        <SelectFields>
                            <telerik:GridGroupByField FieldAlias="Title" FieldName="MessageURI"></telerik:GridGroupByField>
                        </SelectFields>
                    <GroupByFields>
                        <telerik:GridGroupByField FieldName="MessageURI" SortOrder="Descending"></telerik:GridGroupByField>
                    </GroupByFields>
                </telerik:GridGroupByExpression>
            </GroupByExpressions>
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="20"
                    DataField="SMSMessageLogID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="Title" HeaderText="Source Message" FilterControlWidth="150px"
                    UniqueName="TrainingTitle" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="100" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MessageURI") %> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Message Text" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="MessageText" HeaderStyle-Width="200" ItemStyle-Width="200" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="MobilePhoneNumber" HeaderText="Member Phone Number"
                    UniqueName="LastName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="120" ItemStyle-Width="120">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MobilePhoneNumber") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn UniqueName="Created" DataField="Timestamp" HeaderText="Timestamp" AllowSorting="true"
                        HeaderStyle-Width="165px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# startDate %>' />
                            to
                            <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="85px" ClientEvents-OnDateSelected="ToDateSelected"
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
                <telerik:GridBoundColumn HeaderText="Direction" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Direction" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="Campaign" HeaderText="Program" FilterControlWidth="150px"
                    UniqueName="CampaignID" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="100" ItemStyle-Width="100">
                    <ItemTemplate>
                        Project U LA Tips and Info
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="MarkAsDelete" HeaderText="Send Details" FilterControlWidth="150px"
                    UniqueName="MarkAsDelete" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="100" ItemStyle-Width="100">
                    <ItemTemplate>
                        Weekly Tip: Tuesday at 4pm
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
        <asp:SqlDataSource ID="smsLogs" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</asp:Content>

