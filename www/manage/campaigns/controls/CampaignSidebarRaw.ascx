<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignSidebarRaw.ascx.cs" Inherits="manage_programs_controls_campaign_sidebar" %>
            
    <asp:PlaceHolder ID="plhCampaignInfo" runat="server">
		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span><asp:Literal ID="litCampaignTitle" runat="server">Campaign Overview</asp:Literal></span></a>
			</div>

			<div class="subnav-content">
				<ul class="quickstats">
					<li>
						<span class="value"><asp:Literal ID="litInvitations" runat="server"></asp:Literal></span>
						<span class="name">Invitations</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litVisitedEnrollment" runat="server"></asp:Literal></span>
						<span class="name">Visitors</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litInvitationConversionRate" runat="server"></asp:Literal></span>
						<span class="name">Invited Converted</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litVisitorConversionRate" runat="server"></asp:Literal></span>
						<span class="name">Visitors Converted</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEnrolledMembers" runat="server"></asp:Literal></span>
						<span class="name">Enrolled</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litCancelled" runat="server"></asp:Literal></span>
						<span class="name">Stopped</span>
					</li>
				</ul>
			</div>
		</div>

        <div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Enrollment Types</span></a>
			</div>
			<div class="subnav-content">
				<div class="pagestats bar">
					<span>Web <asp:Literal ID="litWebEnrolled" runat="server">(361)</asp:Literal></span>
					<div class="progress small">
						<div class="bar" <asp:Literal ID="litWebEnrolledPercent" runat="server">style="width:79%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Mobile App <asp:Literal ID="litMobileAppEnrolled" runat="server">(5)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-green" <asp:Literal ID="litMobileAppEnrolledPercent" runat="server">style="width:15%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Text Messaging/SMS <asp:Literal ID="litSMSEnrolled" runat="server">(92)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-lightred" <asp:Literal ID="litSMSEnrolledPercent" runat="server">style="width:6%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Email <asp:Literal ID="litEmailEnrolled" runat="server">(92)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-orange" <asp:Literal ID="litEmailEnrolledPercent" runat="server">style="width:6%"</asp:Literal>></div>
					</div>
				</div>
			</div>
		</div>

        <div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Progress</span></a>
			</div>
			<div class="subnav-content">
				<div class="pagestats bar">
					<span>Waiting to Start <asp:Literal ID="litWaiting" runat="server">(361)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-teal" <asp:Literal ID="litWaitingPercent" runat="server">style="width:79%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>In Progress <asp:Literal ID="litInProgress" runat="server">(5)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-teal" <asp:Literal ID="litInProgressPercent" runat="server">style="width:15%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Finished <asp:Literal ID="litFinished" runat="server">(92)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-teal" <asp:Literal ID="litFinishedPercent" runat="server">style="width:6%"</asp:Literal>></div>
					</div>
				</div>
			</div>
		</div>
      
    </asp:PlaceHolder>
