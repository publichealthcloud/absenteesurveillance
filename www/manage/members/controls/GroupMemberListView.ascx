<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupMemberListView.ascx.cs" Inherits="Quartz.Controls.GroupMemberListView" %>

	<tr id="tr-<%=user_id %>">
		<td><asp:Label ID="lblUserID" runat="server">ID</asp:Label></td>
        <td><asp:Literal ID="litUsername" runat="server"></asp:Literal></td>
        <td><asp:Literal ID="litFullName" runat="server"></asp:Literal></td>
		<td><asp:Literal ID="litEmail" runat="server"></asp:Literal></td>
		<td class='hidden-350'><asp:Literal ID="litGroupRole" runat="server"></asp:Literal></td>
		<td class='hidden-1024'><asp:Literal ID="litEnrolled" runat="server"></asp:Literal></td>
		<td class='hidden-480'>
			<a target="_blank" href="/social/profile/profile.aspx?userID=<%=user_id %>" class="btn" rel="tooltip" title="View in Site"><i class="icon-search"></i></a>
			<a href="/manage/members/member-profile.aspx?userID=<%=user_id %>" class="btn" rel="tooltip" title="Manage Member"><i class="icon-user"></i></a>
            <a href="#" onclick="RemoveSpaceUser('<%=user_id %>', '<%=space_id %>', 'group'); return false;" class="btn" rel="tooltip" title="Remove from Group"><i class="icon-remove"></i></a>
		</td>
	</tr>