<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging/test-message.master" AutoEventWireup="true" CodeFile="programs.aspx.cs" Inherits="text_messages_programs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1>Message Programs</h1>
    <div style="background-color:lightgray; padding:4px 4px 4px 4px; text-align:left;">
        <asp:Button ID="btnNewRecord" runat="server" Text="New Program">
        </asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnRefresh" OnClick="btnRefresh_Click" Skin="Metro" runat="server" Text="Refresh"></asp:Button>&nbsp;&nbsp;
        <asp:Button ID="btnDownloadExcel" OnClick="btnDownloadExcel_Click" Skin="Metro" runat="server" Text="Download to Excel"></asp:Button>&nbsp;&nbsp;
    </div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="programs" Skin="MetroTouch" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="False" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="CampaignName" HeaderText="Program Name" HeaderStyle-Width="250px"
                    UniqueName="CampaignName" AllowFiltering="true" AutoPostBackOnFilter="true" FilterControlWidth="150px" ItemStyle-Width="150">
                    <ItemTemplate>
                        <a href="program-edit.aspx?programID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>"><img src="images/grid_edit.gif" />&nbsp;<%# DataBinder.Eval(Container.DataItem, "CampaignName") %></a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="Description" HeaderText="Description"
                    UniqueName="Description" AllowFiltering="true" AutoPostBackOnFilter="true" FilterControlWidth="150px" HeaderStyle-Width="400px" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Description") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="CampaignID" HeaderText="Program Actions"
                    UniqueName="CampaignID" HeaderStyle-Width="400px" ItemStyle-Width="150">
                    <ItemTemplate>
                        <a href="message-logs.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>">Program Logs</a>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="messages.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>">Program Messages</a>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="calendar.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>">Program Calendar</a>&nbsp;&nbsp;&nbsp;&nbsp;
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
    <asp:SqlDataSource ID="programs" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="System.Data.SqlClient" runat="server"></asp:SqlDataSource>  
</asp:Content>

