<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewer-campaign-activity.aspx.cs" Inherits="manage_viewer_campaign_activity" %>
<%@ Register Src="~/social/controls/profile/username-tip.ascx" TagPrefix="epg" TagName="usernametip" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/social/controls/media/viewer_post-info.ascx" TagPrefix="epg" TagName="ViewerPostInfo" %>
<%@ Register Src="~/social/controls/media/viewer_comments.ascx" TagPrefix="epg" TagName="ViewerComments" %>
<%@ Register Src="~/social/controls/profile/user-image-tooltip.ascx" TagPrefix="epg" TagName="userimagetooltip" %>

<form id="FormArticleViewer" runat="server" onsubmit="return false">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1">
    </telerik:RadScriptManager>

	<div class="activity">
		Campaign Info
    </div>
</form>
