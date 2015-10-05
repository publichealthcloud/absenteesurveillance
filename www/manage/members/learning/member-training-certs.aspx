<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="member-training-certs.aspx.cs" Inherits="custom_member_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
        <link href="../quartz.css" rel="stylesheet" type="text/css" />
        <br />
        <span><strong>&nbsp;<asp:Label ID="lblTitle" class="CapsHeader3" runat="server" Text="Order List"></asp:Label></strong></span>
    <telerik:RadGrid ID="RadGrid1" runat="server" CaseSensitive="false" EnableLinqExpressions="false"
        AllowFilteringByColumn="True"  Width="100%">        
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
                <telerik:GridBoundColumn DataField="Title" Groupable="true" AllowFiltering="true"
                    HeaderText="Training" UniqueName="Title" ReadOnly="True" AutoPostBackOnFilter="true"
                    FilterControlWidth="250" HeaderStyle-Width="250" ItemStyle-Width="250">
                </telerik:GridBoundColumn> 
                <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Uploaded" UniqueName="DateCreated"
                    AutoPostBackOnFilter="true" FilterControlWidth="60" HeaderStyle-Width="60" ItemStyle-Width="60">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDate" Text='<%#  String.Format("{0:d}",Eval("Created")) %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="FileName" HeaderText="Link to Certificate" 
                    UniqueName="FileName" Groupable="False">
                    <ItemTemplate>
                        <asp:Label ID="Download" runat="server">
                            <a target"blank" href="<% = viewPath %>user_data/<%# Eval("Username") %>/<%# Eval("FileName") %>">Download</a></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn DataField="FileName" HeaderText="Delete Certificate" 
                    UniqueName="Delete" Groupable="False">
                    <ItemTemplate>
                        <asp:Label ID="Delete" runat="server">
                            <a href="<% = viewPath %>public/delete-training-cert.aspx?userTrainingCertificateID=<%# Eval("UserTrainingCertificateID") %>">Delete Certificate</a>
                        </asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
        </MasterTableView>
        <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
</asp:Content>
<%--</asp:Content>--%>
