<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignSidebar.ascx.cs" Inherits="manage_programs_controls_campaign_sidebar" %>
            
    <asp:PlaceHolder ID="plhCampaignInfo" runat="server">
		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span><asp:Literal ID="litCampaignTitle" runat="server">Campaign Name</asp:Literal></span></a>
			</div>

			<div class="subnav-content">
				<ul class="quickstats">
                    <li>
						<span class="value"><asp:Literal ID="litNumContactsAvailable" runat="server"></asp:Literal></span>
						<span class="name">Contacts Available</span>
                    </li>
					<li>
						<span class="value"><asp:Literal ID="litNumUniqueInvites" runat="server"></asp:Literal></span>
						<span class="name">Contacts Invited</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litNumContacted" runat="server"></asp:Literal></span>
						<span class="name">Invitations Sent</span>
					</li>
                    <li>
						<span class="value"><asp:Literal ID="litNumFlyers" runat="server"></asp:Literal></span>
						<span class="name">Flyers Distributed</span>
                    </li>
					<li>
						<span class="value"><asp:Literal ID="litVisitedEnrollment" runat="server"></asp:Literal></span>
						<span class="name">Enrollment Visits</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEnrolledMembers" runat="server"></asp:Literal></span>
						<span class="name">Enrolled Members</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litConversionRate" runat="server"></asp:Literal></span>
						<span class="name">Conversion Rate Invites</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litConversionRateVisits" runat="server"></asp:Literal></span>
						<span class="name">Conversion Rate Visits</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litInProgress" runat="server"></asp:Literal></span>
						<span class="name">Active</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litFinished" runat="server"></asp:Literal></span>
						<span class="name">Finished</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litCancelled" runat="server"></asp:Literal></span>
						<span class="name">Stopped Mid-campaign</span>
					</li>
				</ul>
			</div>
		</div>

        <div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Enrollment</span></a>
			</div>
			<div class="subnav-content">
				<div class="pagestats bar">
					<span>Web (<asp:Literal ID="litEnrolledWeb" runat="server"></asp:Literal>)</span>
					<div class="progress small">
						<div class="bar bar-lightred" <asp:Literal ID="litWebPercent" runat="server"></asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Mobile App (<asp:Literal ID="litEnrolledMobileApp" runat="server"></asp:Literal>)</span>
					<div class="progress small">
						<div class="bar bar-green" <asp:Literal ID="litAppPercent" runat="server"></asp:Literal>style="width:0%"></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Text Messaging/SMS (<asp:Literal ID="litEnrolledSMS" runat="server"></asp:Literal>)</span>
					<div class="progress small">
						<div class="bar" <asp:Literal ID="litSMSPercent" runat="server"></asp:Literal>style="width:0%"></div>
					</div>
				</div>
			</div>
		</div>
        
        <asp:PlaceHolder ID="plhEmailSideBarSummary" runat="server">
		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Email Overview</span></a>
			</div>
			<div class="subnav-content">
				<ul class="quickstats">
					<li>
						<span class="value"><asp:Literal ID="litEmailSent" runat="server">0</asp:Literal></span>
						<span class="name">Sent</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailBounce" runat="server">0</asp:Literal></span>
						<span class="name">Bad Addresses</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailUnsubscribe" runat="server">0</asp:Literal></span>
						<span class="name">Unsubscribe</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailReportSpam" runat="server">0</asp:Literal></span>
						<span class="name">Reported As Spam</span>
					</li>
				</ul>
			</div>
		</div>

		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Email Actions</span></a>
			</div>
			<div class="subnav-content">
				<div class="pagestats bar">
					<span>Emails Read</span>
					<div class="progress small">
						<div class="bar bar-teal" <asp:Literal ID="litPercentRead" runat="server">style="width:0%"</asp:Literal> ></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Enroll Links Clicked</span>
					<div class="progress small">
						<div class="bar bar-teal" <asp:Literal ID="litPercentClicked" runat="server">style="width:0%"</asp:Literal> ></div>
					</div>
				</div>
				<ul class="quickstats">
					<li>
						<span class="value"><asp:Literal ID="litEmailReads" runat="server">0</asp:Literal></span>
						<span class="name">Reads</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailReadRate" runat="server">0%</asp:Literal></span>
						<span class="name">Read Rate</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailClicks" runat="server">0</asp:Literal></span>
						<span class="name">Clicks</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litEmailClickRate" runat="server">0%</asp:Literal></span>
						<span class="name">Click Rate</span>
					</li>
				</ul>
			</div>
		</div>
        </asp:PlaceHolder>

		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Available</span></a>
			</div>
				<div class="pagestats bar">
					<span><asp:Literal ID="litAvailableRange" runat="server"></asp:Literal></span>
				</div>
		</div>

		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Last Compiled</span></a>
			</div>
				<div class="pagestats bar">
					<span><asp:Literal ID="litLastCompiled" runat="server"></asp:Literal></span>
				</div>
		</div>
    </asp:PlaceHolder>
