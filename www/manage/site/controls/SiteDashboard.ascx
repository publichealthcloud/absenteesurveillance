<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteDashboard.ascx.cs" Inherits="manage_site_controls_SiteDashboard" %>

<script type="text/javascript"
    src="https://maps.googleapis.com/maps/api/js?key=<%= GoogleAPIKey %>">
</script>
<asp:Literal ID="litJSMap" runat="server"></asp:Literal>
<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

<div class="row-fluid">                             
    <div class="span12">
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="glyphicon-stats"></i> <asp:Literal ID="litDailyDashboardTitle" runat="server"></asp:Literal>
				</h3>
                <ul class="tabs">
                    <li>
                        <div class="btn-group">
			                <a href="#" data-toggle="dropdown" class="btn dropdown-toggle" runat="server" id="filterMore"><i class="icon-sort-by-attributes"></i> Change Date Range <span class="caret"></span></a>
			                <ul class="dropdown-menu">
				                <li>
					                <a href="default.aspx?mode=28days">Last 28 Days</a>
				                </li>
				                <li>
					                <a href="default.aspx?mode=this-year">Current Year</a>
				                </li>
				                <li>
					                <a href="default.aspx?mode=since-start">Since Site Launch</a>
				                </li>
			                </ul>
		                </div>
                    </li>
		        </ul>
			</div>
		</div>
		<div class="box-content">
            <table>
                <tr>
                    <td><div style="width:37px;"></div></td>
                    <td><strong>Trends:&nbsp;</strong></td>
                    <td><div style="width:53px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:gray;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>Member Logins</td>
                    <td><div style="width:45px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:black;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>New Members</td>
                </tr>
            </table>
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </div>
    </div>
</div>

<div class="row-fluid">                           
    <div class="span6">
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
				        <td><strong>Total Active Members</strong></td>
				        <td><asp:Literal ID="litSummaryTotalMembers" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>New Members This Week</td>
				        <td><asp:Literal ID="litSummaryMembersThisWeek" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>New Members This Month</td>
				        <td><asp:Literal ID="litSummaryMembersThisMonth" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>New Members This Year</td>
				        <td><asp:Literal ID="litSummaryMembersThisYear" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td><strong>Total Sessions</strong></td>
				        <td><asp:Literal ID="litSummaryTotalSessions" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Sessions This Week</td>
				        <td><asp:Literal ID="litSummarySessionsThisWeek" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Sessions This Month</td>
				        <td><asp:Literal ID="litSummarySessionsThisMonth" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Sessions This Year</td>
				        <td><asp:Literal ID="litSummarySessionsThisYear" runat="server">0</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span6">
        <asp:Literal ID="litTableDescriptionSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-info-sign"></i>
                        Jump to Training Report
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="litTableDescriptionWidth" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litTrainingList" runat="server">description here.</asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
        </div>
        </div>
</div>