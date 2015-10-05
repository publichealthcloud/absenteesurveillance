<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignSummaryRaw.ascx.cs" Inherits="manage_campaigns_controls_CampaignSummaryRaw" %>

        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="icon-dashboard"></i>
                    Summary
				</h3>
			</div>
		</div>
		<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableSummaryWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Available</td>
				        <td><asp:Literal ID="litSummaryIsAvailable" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Enrolled Members </td>
				        <td><asp:Literal ID="litSummaryTotalEnrolled" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members Waiting to Start</td>
				        <td><asp:Literal ID="litSummaryWaitingToStart" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members In Progress</td>
				        <td><asp:Literal ID="litSummaryInProgress" runat="server"></asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Members Finished</td>
				        <td><asp:Literal ID="litSummaryFinished" runat="server"></asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Total Campaign Length</td>
				        <td><asp:Literal ID="litSummaryTotalDays" runat="server"></asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>