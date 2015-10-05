<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="page-list.aspx.cs" Inherits="manage_manage_pages" %>
<%@ Register Src="~/manage/site/controls/PageList.ascx" TagPrefix="epg" TagName="PageList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <epg:PageList runat="server" ID="PageList" />

</asp:Content>

