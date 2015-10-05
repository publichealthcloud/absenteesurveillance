<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="user-training-list_detailed.aspx.cs" Inherits="qLrn_user_training_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
        <br />
        <span><strong>&nbsp;<asp:Label ID="lblTitle" class="CapsHeader3" runat="server" Text="Training Registrations"></asp:Label></strong></span>

            <telerik:radmenu id="resultsMenu" runat="server" width="100%" onitemclick="resultsMenu_ItemClick">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
            <Items>
                <telerik:RadMenuItem runat="server" Text="Download To Excel">
                </telerik:RadMenuItem>
                <telerik:RadMenuItem runat="server" Text="Reset">
                </telerik:RadMenuItem>
            </Items>
        </telerik:radmenu>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="userTrainings" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="TrainingID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Title" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="125"
                    DataField="Title" HeaderStyle-Width="250" ItemStyle-Width="125" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="TrainingTypeName" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Payment Method" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="PaymentMethod" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="When Registered" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Created" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="When Completed" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Completed" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Status" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Status" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="UserID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="UserID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Username" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="First Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="FirstName" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Last Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="LastName" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Email" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="DOB" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="DOB" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Age" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Age" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Gender" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Gender" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Address1" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Address1" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Address2" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Address2" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="City" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="City" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="State" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="StateProvince" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Zip" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Postal Code" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Phone" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Phone1" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Ethnicity" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Ethnicity" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Race" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Race" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="First Involvement" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="FirstInvolvement" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="First Involvement" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="FirstInvolvement" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Profession" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="StaffTypeName" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Profession Code" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Code" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Profession Extended" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="AltName" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Employment Location" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="EmploymentTypeName" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Employment Setting" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="EmploymentSetting" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Work Sites" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="WorkSites" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Position" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Position" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Agency" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Agency" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Division" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Division" HeaderStyle-Width="150" ItemStyle-Width="150">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Degrees" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Degrees" HeaderStyle-Width="150" ItemStyle-Width="150">
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
        <asp:SqlDataSource ID="userTrainings" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</asp:Content>
