<%@ Page Title="" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" AutoEventWireup="true" CodeFile="map.aspx.cs" Inherits="manage_school_districts_map" %>
<%@ Register Src="~/manage/school-districts/controls/school-district-sidebar.ascx" TagPrefix="epg" TagName="schooldistrictsidebar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="school_nav" Runat="Server">
    <epg:schooldistrictsidebar runat="server" ID="schooldistrictsidebar" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="school_main" Runat="Server">

<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d48259.7995870416!2d-111.93504702235668!3d40.888605422365146!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8752f7b0aab9eb81%3A0x964efff0a957e2f1!2sBoulton+School!5e0!3m2!1sen!2sus!4v1395256169369" width="800" height="600" frameborder="0" style="border:0"></iframe>

</asp:Content>
