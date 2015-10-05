<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignEnrolledMembers.ascx.cs" Inherits="manage_campaigns_controls_CampaignEnrolledMembers" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

 <div class="row-fluid">                  
    <div class="span12">
        <div class="box">
	        <div class="box-title">
		        <h3>
			        <i class="icon-group"></i>
			        <asp:Label ID="lblTitle" runat="server" Text="Links">Campaign Members</asp:Label>
		        </h3>
                <ul class="tabs">
                    <li>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                </div>
                    </li>
		        </ul>
	        </div>
            <telerik:RadGrid ID="RadGrid1" DataSourceID="siteData" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                GridLines="None">
                <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                <Columns>
                    <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="20"
                        DataField="UserCampaignID" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="UserID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="20"
                        DataField="UserID" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="Username" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="Email" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Start Date" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="CampaignStart" HeaderStyle-Width="125" ItemStyle-Width="125" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Days In Campaign" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="DaysInCampaign" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Enrollment Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="EnrollmentType" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Language" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="Language" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Campaign Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="CampaignStatus" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Invite Code" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="InviteCode" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Invite First Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="InitFirstName" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Invite Last Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="InitLastName" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Invite Gender" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="InitGender" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="Value" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                        DataField="ReferenceValue" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="40">
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
            <asp:SqlDataSource ID="siteData" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                ProviderName="System.Data.SqlClient" 
                runat="server"></asp:SqlDataSource>
        </div>
    </div>
</div> 