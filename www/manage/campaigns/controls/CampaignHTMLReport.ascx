<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignHTMLReport.ascx.cs" Inherits="manage_campaigns_controls_CampaignAbout" %>

    <div class="span12">
        <asp:Literal ID="litTableDescriptionSpacer" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-info-sign"></i>
                        Report
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="litTableDescriptionWidth" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litReport" runat="server"></asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
        </div>
    </div>