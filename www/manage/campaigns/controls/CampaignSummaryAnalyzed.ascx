<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignSummaryAnalyzed.ascx.cs" Inherits="manage_campaigns_controls_CampaignSummaryAnalyzed" %>

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
				        <td>Dates Available</td>
				        <td><asp:Literal ID="litSummaryAvailableDates" runat="server">9/29/2013 - open</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Enrolled Members </td>
				        <td><asp:Literal ID="litSummaryTotalEnrolled" runat="server">439</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members Waiting to Start</td>
				        <td><asp:Literal ID="litSummaryWaitingToStart" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members In Progress</td>
				        <td><asp:Literal ID="litSummaryInProgress" runat="server">16</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Members Finished</td>
				        <td><asp:Literal ID="litSummaryFinished" runat="server">423</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Mid-Campaign STOP Requests</td>
				        <td><asp:Literal ID="litSummaryCancelled" runat="server">32</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Total Campaign Length</td>
				        <td><asp:Literal ID="litSummaryTotalDays" runat="server">70 days</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>