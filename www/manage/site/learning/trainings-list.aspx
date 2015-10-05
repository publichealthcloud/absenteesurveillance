<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="trainings-list.aspx.cs" Inherits="manage_site_learning_trainings_list" %>
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
			                <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Groups List</asp:Label>
		                </h3>
                        <ul class="tabs">
                            <li runat="server" id="liShare">
                                <div class="btn-group">
			                        <a href="/manage/site/learning/training-edit.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD TRAINING</a>
		                        </div>
                            </li>
                            <li>
                                <div class="btn-group">
                                    <a href="/manage/site/learning/trainings-list.aspx" class="btn"><i class="icon-refresh"></i> Reload</a> 
		                        </div>
                            </li>
                            <li>
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		                        </div>
                            </li>
		                </ul>
	                </div>
                    <telerik:RadGrid ID="RadGrid1" DataSourceID="siteTrainings" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
                        GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                        <Columns>
                            <telerik:GridTemplateColumn DataField="ScopeID" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                                <ItemTemplate>
			                        <a href="training-edit.aspx?trainingID=<%# DataBinder.Eval(Container.DataItem, "TrainingID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a>  
                                    <asp:PlaceHolder ID="plhInteral" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "TrainingTypeName") == "Internal") %>'>
                                        <a target="_blank" href="<%= final_manage_url %>&returnURL=/slide-modules/slide-editor.aspx?TrainingID=<%# DataBinder.Eval(Container.DataItem, "TrainingID") %>" class="btn" rel="tooltip" title="Design Training"><i class="icon-picture"></i></a>
                                    </asp:PlaceHolder>      
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                                DataField="TrainingID" HeaderStyle-Width="40" ItemStyle-Width="40" AllowSorting="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Title" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="Title" HeaderStyle-Width="150" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Description" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="Description" HeaderStyle-Width="200" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Author" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="AutorName" HeaderStyle-Width="200" ItemStyle-Width="150" AllowSorting="true" FilterControlWidth="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Training Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="TrainingTypeName" AllowSorting="true" HeaderStyle-Width="100" ItemStyle-Width="100" FilterControlWidth="50">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Available In Site" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                DataField="Available" AllowSorting="true" HeaderStyle-Width="100" ItemStyle-Width="100" FilterControlWidth="50">
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
                    <asp:SqlDataSource ID="siteTrainings" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                        ProviderName="System.Data.SqlClient" 
                        runat="server"></asp:SqlDataSource>
                </div>
            </div>
        </div>
</asp:Content>
