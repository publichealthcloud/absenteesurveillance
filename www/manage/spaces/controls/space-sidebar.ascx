<%@ Control Language="C#" AutoEventWireup="true" CodeFile="space-sidebar.ascx.cs" Inherits="manage_spaces_controls_sidebar" %>

       <div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span><asp:Literal ID="litSpaceTitle" runat="server"></asp:Literal></span></a>
			</div>
			<div class="subnav-content">
				<ul class="quickstats">
					<li>
						<span class="value"><asp:Literal ID="litEnrolled" runat="server">453</asp:Literal></span>
						<span class="name">Enrolled</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litNumCampaigns" runat="server">2</asp:Literal></span>
						<span class="name">Campaigns</span>
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
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Active Campaigns</span></a>
			</div>
				<div class="pagestats bar">
					<span>By Number Enrolled</span>
				</div>
			<div class="subnav-content">
				<asp:Literal ID="litCampaignDetails" runat="server"></asp:Literal>
			</div>
		</div>
