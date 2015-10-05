<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignActivitiesList.ascx.cs" Inherits="manage_site_learning_camapign_activities" %>

<table class="table table-hover table-nomargin table-striped">
	<thead>
		<tr>
			<th>Activity</th>
			<th class="hidden-1024">Type</th>
			<th class="hidden-480">Points</th>
		</tr>
	</thead>
	<tbody>
        <asp:Panel ID="pnlActivities" runat="server"></asp:Panel>
	</tbody>
</table>
<asp:Label ID="lblCampaignID" runat="server" Visible="false"></asp:Label>
