<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignActivitiesListEnhanced.ascx.cs" Inherits="manage_site_learning_camapign_activities_enhanced" %>

<table class="table table-hover table-nomargin table-striped">
	<thead>
		<tr>
			<th>Activity</th>
			<th class="hidden-1024">Type</th>
			<th class="hidden-1024">Alt Formats</th>
			<th class="hidden-480">Points</th>
			<th class="hidden-1024">Schedule</th>
		    <th class='hidden-480'>Manage</th>
		</tr>
	</thead>
	<tbody>
        <asp:Panel ID="pnlActivities" runat="server"></asp:Panel>
	</tbody>
</table>
<asp:Label ID="lblCampaignID" runat="server" Visible="false"></asp:Label>
