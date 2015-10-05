<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MemberNav.ascx.cs" Inherits="manage_members_controls_MemberNav" %>

    <h3>
        <asp:Literal ID="litTitleInfo" runat="server"></asp:Literal>
	</h3>
    <ul class="tabs">
        <li runat="server" id="liShare">
            <div class="btn-group">
			    <a href="/manage/members/member-profile.aspx?userID=<%= profile_id %>" <asp:Literal ID="litInfoCss" runat="server"></asp:Literal>><i class="icon-user"></i> Basic Info</a>
		    </div>
        </li>
        <li runat="server" visible="false">
            <div class="btn-group">
                <a href="/manage/members/member-content.aspx?userID=<%= profile_id %>" <asp:Literal ID="litContentCss" runat="server"></asp:Literal>><i class="icon-edit"></i> Content</a> 
		    </div>
        </li>
        <li>
            <div class="btn-group">
                <a href="/manage/members/member-learning.aspx?userID=<%= profile_id %>" <asp:Literal ID="litLearningCss" runat="server"></asp:Literal>><i class="glyphicon-lightbulb"></i> Learning</a> 
		    </div>
        </li>
        <li>
            <div class="btn-group">
                <a href="/manage/members/member-communications.aspx?userID=<%= profile_id %>" <asp:Literal ID="litCommunicationsCss" runat="server"></asp:Literal>><i class="icon-envelope-alt"></i> Communications</a> 
		    </div>
        </li>
        <li>
            <div class="btn-group">
                <a href="/manage/members/member-admin-tools.aspx?userID=<%= profile_id %>" <asp:Literal ID="litAdminToolsCss" runat="server"></asp:Literal>><i class="icon-cog"></i> Admin Tools</a>
		    </div>
        </li>
	</ul>