<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="send-event-list.aspx.cs" Inherits="manage_communications_send_events_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <asp:Button ID="btnNewEvent" runat="server" Text="New Event" CssClass="btn"
        onclick="btnNewEvent_Click" />&nbsp;&nbsp;
    <asp:Button ID="btnSendEvents" runat="server" Text="Send All Events Now" CssClass="btn" 
        onclick="btnSendEvents_Click" />              
    <div class="box">
	    <div class="box-title">
		    <h3>
			    <i class="icon-envelope"></i>
			    <asp:Label ID="lblTitle" runat="server" Text="Links">Send Events</asp:Label>
		    </h3>
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">
			            <a href="/manage/communications/email/create-email-event.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> NEW EVENT</a>
		            </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="/manage/communications/email/send-bulk.aspx" class="btn"><i class="icon-envelope"></i> Send All Events Now</a> 
		            </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		            </div>
                </li>
		    </ul>
	    </div>
                <telerik:RadGrid ID="RadGrid1" DataSourceID="siteEmailSendEvents" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                    AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                    GridLines="None">
                    <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Subject" HeaderText="Email Subject" Groupable="false" UniqueName="Subject" SortExpression="LastName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="100" ItemStyle-Width="100" >
                            <ItemTemplate>
                                <a href="create-email-event.aspx?sendEventID=<%# DataBinder.Eval(Container.DataItem, "SendEventID") %>">
                                <%# DataBinder.Eval(Container.DataItem, "Subject") %>
                                </a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Running" AllowSorting="true" Groupable="true" AllowFiltering="true" HeaderText="Active" UniqueName="Active" ReadOnly="True" AutoPostBackOnFilter="true" FilterControlWidth="50" HeaderStyle-Width="15" ItemStyle-Width="15" >
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Start Date" UniqueName="StartDate" Groupable="true"  AllowFiltering="false" SortExpression="StartDate" HeaderStyle-Width="75" ItemStyle-Width="75" >
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate" Text='<%#  String.Format("{0:g}",Eval("StartDate")) %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="SavedDescription" AllowSorting="true" Groupable="true" AllowFiltering="true" HeaderText="Recipients" UniqueName="SavedDescription" ReadOnly="True" AutoPostBackOnFilter="true" FilterControlWidth="100" HeaderStyle-Width="100" ItemStyle-Width="100" >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Recurring" AllowSorting="true" Groupable="true" AllowFiltering="true" HeaderText="Send Every Day" UniqueName="Recurring" ReadOnly="True" AutoPostBackOnFilter="true" FilterControlWidth="50" HeaderStyle-Width="50" ItemStyle-Width="50" >
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Priority" AllowSorting="true" Groupable="true" AllowFiltering="true" HeaderText="Send Order" UniqueName="Priority" ReadOnly="True" AutoPostBackOnFilter="true" FilterControlWidth="50" HeaderStyle-Width="50" ItemStyle-Width="50" >
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Action" UniqueName="Action" Groupable="true"  AllowFiltering="false" HeaderStyle-Width="100" ItemStyle-Width="100" >
                            <ItemTemplate>
                                <a href="test-automated-bulk-send.aspx?sendEventID=<%# DataBinder.Eval(Container.DataItem, "SendEventID") %>">
                                Send Test Email
                                </a>                        
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
                <asp:SqlDataSource ID="siteEmailSendEvents" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    ProviderName="System.Data.SqlClient" 
                    runat="server"></asp:SqlDataSource>
            </div>
</asp:Content>
