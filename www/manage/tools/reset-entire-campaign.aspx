<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="reset-entire-campaign.aspx.cs" Inherits="manage_tools_reset_entire_campaign" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    Select Camapign: <asp:DropDownList ID="ddlCampaigns" runat="server"></asp:DropDownList><br /><br />
    Select Start Date for Users: <telerik:RadDatePicker ID="datStart" runat="server"></telerik:RadDatePicker><br /><br />
    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnReset" OnClientClick="return confirm('Are you sure you want to this entire campaign? This action cannot be undone and will result in all user actions being reset.');" OnClick="btnReset_Click" Text="Reset Entire Campaign"></asp:Button>
</asp:Content>

