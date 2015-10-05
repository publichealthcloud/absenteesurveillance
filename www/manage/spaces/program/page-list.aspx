<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="page-list.aspx.cs" Inherits="manage_spaces_pages" %>
<%@ Register Src="~/manage/site/controls/PageList.ascx" TagPrefix="epg" TagName="PageList" %>
<%@ Register Src="~/manage/spaces/controls/space-sidebar.ascx" TagPrefix="epg" TagName="spacesidebar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">

        <epg:spacesidebar runat="server" ID="spacesidebar" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <epg:PageList runat="server" ID="PageList" />

</asp:Content>

