<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignLanguage.ascx.cs" Inherits="manage_campaigns_controls_CampaignSummaryRaw" %>

<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="glyphicon-pie_chart"></i>
                    Languages
				</h3>
			</div>
		</div>
		<div class="box-content">
            <asp:Literal ID="litLanaguagePieChart" runat="server"></asp:Literal>
        </div>