<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignMemberListView.ascx.cs" Inherits="Quartz.Controls.CampaignMemberListView" %>

<script type="text/javascript">
    function openUserInfo(user_id) {
        var win = window.open('/manage/members/member-profile.aspx?userID=' + user_id, '_blank');
        win.focus();
    }
    function openUserProfile(user_id) {
        var win = window.open('/social/profile/profile.aspx?userID=' + user_id, '_blank');
        win.focus();
    }
    function openUserCampaignActivity(user_id) {
        var win = window.open('/manage/members/?userID=' + user_id, '_blank');
        win.focus();
    }
</script>

	<tr id="tr-<%=user_id %>">
		<td><asp:Label ID="lblUserID" runat="server">ID</asp:Label></td>
        <td><asp:Literal ID="litUsername" runat="server">Username</asp:Literal></td>
        <td><asp:Literal ID="litFullName" runat="server">First Name Last Name</asp:Literal></td>
		<td><asp:Literal ID="litEmail" runat="server">email@email.com</asp:Literal></td>
		<td class='hidden-350'><asp:Literal ID="litGroupRole" runat="server">Username</asp:Literal></td>
		<td class='hidden-1024'><asp:Literal ID="litEnrolled" runat="server">Username</asp:Literal></td>
		<td class='hidden-480'>
            <a href="#" onclick="viewMemberCampaignActivity('<%=user_id %>', '<%=campaign_id %>'); return false;" class="btn" rel="tooltip" title="Member Activity"><i class="icon-dashboard"></i></a>
			<a href="#" onclick="openUserProfile(<%=user_id %>)" class="btn" rel="tooltip" title="View in Site"><i class="icon-search"></i></a>
			<a href="#" onclick="openUserInfo(<%=user_id %>)" class="btn" rel="tooltip" title="Manage Member"><i class="icon-user"></i></a>
            <a href="#" onclick="RemoveCampaignUser('<%=user_id %>', '<%=campaign_id %>'); return false;" class="btn" rel="tooltip" title="Remove from Campaign"><i class="icon-remove"></i></a>
		</td>
	</tr>