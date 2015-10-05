<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="manage_school_districts_default" %>
<%@ Register Src="~/manage/school-districts/controls/school-district-sidebar.ascx" TagPrefix="epg" TagName="schooldistrictsidebar" %>
<%@ Register Src="~/manage/school-districts/reports/controls/DailySummaryCharts.ascx" TagPrefix="epg" TagName="DailySummaryCharts" %>
<%@ Register Src="~/manage/school-districts/reports/controls/DailyDistrictAbsenteeDashboard.ascx" TagPrefix="epg" TagName="DailyDistrictAbsenteeDashboard" %>
<%@ Register Src="~/manage/school-districts/reports/controls/SchoolSelector.ascx" TagPrefix="epg" TagName="SchoolSelector" %>


<asp:Content ID="Content2" ContentPlaceHolderID="school_nav" Runat="Server">
    <epg:schooldistrictsidebar runat="server" ID="schooldistrictsidebar" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="school_main" Runat="Server">
<div class="row-fluid">                  
    <div class="span12">
        <h2><asp:Literal ID="litTitle" runat="server">Daily Absentee Overview</asp:Literal></h2>
        <div style="height:10px;"></div>
        <asp:Literal ID="litDateBefore" runat="server"></asp:Literal>
        <a href="#" class="btn btn-large"><asp:Literal ID="litDataDate" runat="server">Monday, December 12, 2014</asp:Literal></a>
        <asp:Literal ID="litDateAfter" runat="server"></asp:Literal>
        <asp:PlaceHolder ID="plhCustomReportBuilder" runat="server" Visible="false">
            <a href="#" class="btn btn-danger btn-large"><i class="icon-dashboard"></i> Custom Report Builder</a> <i>Custom report builder not enabled yet</i>
        </asp:PlaceHolder>
        <div style="height:15px;"></div>
        <strong>Jump to Date:</strong> 
        <telerik:RadDatePicker ID="rdtDataDate" runat="server"></telerik:RadDatePicker> <asp:Button CssClass="btn btn-small" Text="Reload" runat="server" OnClick="Unnamed_Click" />
        <asp:Literal ID="litDatePickWarning" runat="server"></asp:Literal>
        <br />
        <epg:SchoolSelector runat="server" ID="SchoolSelector" />
    </div>
</div>

<epg:DailyDistrictAbsenteeDashboard runat="server" ID="DailySchoolAbsenteeDashboard" />

</asp:Content>

