<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberEnrolledGroup.ascx.cs" Inherits="Quartz.Controls.MemberEnrolledGroup" %>

	<tr id="tr-<%=space_id %>">
        <td><asp:Literal ID="litGroupName" runat="server"></asp:Literal></td>
        <td><asp:Literal ID="litCreated" runat="server"></asp:Literal></td>
		<td><asp:Literal ID="litGroupType" runat="server"></asp:Literal></td>
		<td class='hidden-480'>
            <a href="#" onclick="RemoveSpaceUser('<%=user_id %>', '<%=space_id %>', 'member'); return false;" class="btn" rel="tooltip" title="Remove from Group"><i class="icon-remove"></i></a>
            <% if (is_primary != true) { %>
                <a href="#" class="btn" onclick="MakePrimarySpace('<%=user_id %>', '<%=space_id %>', '<%=user_space_id %>'); return false;" rel="tooltip" title="Make Primary Group">Make Primary Group</a>
            <% } %>
		</td>
	</tr>