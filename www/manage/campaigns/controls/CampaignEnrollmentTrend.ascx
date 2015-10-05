<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignEnrollmentTrend.ascx.cs" Inherits="manage_campaigns_controls_CampaignEnrollmentTrend" %>

<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

    <div class="box">
        <div class="box-title">
			<h3>
                <i class="glyphicon-stats"></i> <asp:Literal ID="litEnrollmentTendTitle" runat="server">Enrollment Trend</asp:Literal>
			</h3>
            <ul class="tabs">
                <li>
                    <div class="btn-group">
			            <a href="#" data-toggle="dropdown" class="btn dropdown-toggle" runat="server" id="filterMore"><i class="icon-sort-by-attributes"></i> Change Date Range <span class="caret"></span></a>
			            <ul class="dropdown-menu">
				            <li>
					            <a href="campaign-details.aspx?campaignID=<%=CampaignID %>&mode=28days">Last 28 Days</a>
				            </li>
				            <li>
					            <a href="campaign-details.aspx?campaignID=<%=CampaignID %>&mode=this-year">Current Year</a>
				            </li>
				            <li>
					            <a href="campaign-details.aspx?campaignID=<%=CampaignID %>&mode=since-start">Since Site Launch</a>
				            </li>
			            </ul>
		            </div>
                </li>
		    </ul>
		</div>
	</div>
	<div class="box-content">
        <!--
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
        -->
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </div>