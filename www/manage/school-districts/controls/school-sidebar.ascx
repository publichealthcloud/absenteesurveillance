<%@ Control Language="C#" AutoEventWireup="true" CodeFile="school-sidebar.ascx.cs" Inherits="manage_school_districts_controls_school_sidebar" %>

       <div class="subnav">
           <div style="text-align: center;"><h4><asp:Literal ID="litMostRecentDate" runat="server">Most Recent Data</asp:Literal></h4></div>
			<div class="subnav-content">
				<ul class="quickstats">
					<li>
						<span class="value"><asp:Literal ID="litEnrolled" runat="server">0</asp:Literal></span>
						<span class="name">Enrolled</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litAbsences" runat="server">0</asp:Literal></span>
						<span class="name">Absences</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litIllness" runat="server">0</asp:Literal></span>
						<span class="name">Illness Absences</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litUnknown" runat="server">0</asp:Literal></span>
						<span class="name">Unknown Absences</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litRate" runat="server">0</asp:Literal>%</span>
						<span class="name">Absentee Rate</span>
					</li>
					<li>
						<span class="value"><asp:Literal ID="litMovingAverage" runat="server">0</asp:Literal>%</span>
						<span class="name">Historic Avg Rate</span>
					</li>
				</ul>
			</div>
		</div>

		<div class="subnav">
			<div class="subnav-content">
				<div class="pagestats bar">
					<span>Gastrointestinal <asp:Literal ID="litGastrointestinal" runat="server">(0)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-blue" <asp:Literal ID="litGastBar" runat="server">style="width:25%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Respiratory <asp:Literal ID="litRespiratory" runat="server">(30)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-red" <asp:Literal ID="litRespBar" runat="server">style="width:25%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Rash <asp:Literal ID="litRash" runat="server">(0)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-orange" <asp:Literal ID="litRashBar" runat="server">style="width:25%"</asp:Literal>></div>
					</div>
				</div>
				<div class="pagestats bar">
					<span>Other Illness <asp:Literal ID="litOtherIllness" runat="server">(0)</asp:Literal></span>
					<div class="progress small">
						<div class="bar bar-green" <asp:Literal ID="litOthrBar" runat="server">style="width:25%"</asp:Literal>></div>
					</div>
				</div>
			</div>
		</div>

		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Absentee Warnings</span></a>
			</div>
				<div class="pagestats bar">
					<span><asp:Literal ID="litNumWarnings" runat="server"></asp:Literal></span>
				</div>
			<div class="subnav-content">
				<asp:Literal ID="litCampaignDetails" runat="server"></asp:Literal>
			</div>
		</div>

		<div class="subnav">
			<div class="subnav-title">
				<a href="#" class='toggle-subnav'><i class="icon-angle-down"></i><span>Data Processed</span></a>
			</div>
			<div class="pagestats bar">
				<span><asp:Literal ID="litProcessed" runat="server">3/19/2014 at 1:30am</asp:Literal></span>
			</div>
		</div>
