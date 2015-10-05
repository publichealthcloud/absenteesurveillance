<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewer-school.aspx.cs" Inherits="manage_viewers_viewer_school" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/school-districts/reports/controls/DailySchoolAbsenteeDashboard.ascx" TagPrefix="epg" TagName="DailySchoolAbsenteeDashboard" %>

<form id="FormArticleViewer" runat="server" onsubmit="return false">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1">
    </telerik:RadScriptManager>

	<div class="activity">
        <epg:DailySchoolAbsenteeDashboard runat="server" ID="DailySchoolAbsenteeDashboard" />
    </div>
</form>
