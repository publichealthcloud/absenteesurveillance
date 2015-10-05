<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AvailableCampaignReports.ascx.cs" Inherits="manage_campaigns_reports_controls_AvailableCampaignReports" %>

<div class="row-fluid">                  
    <div class="span12">
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="icon-bar-chart"></i>
                    Available Campaign Reports
				</h3>
			</div>
        </div>
	        <div class="box-content nopadding">
                <br />
		            <div class="blog-list-post small">
			            <div class="post-content span12">
				            <h5 class="post-title">
					            <asp:Literal ID="litTitle" runat="server">Campaign Overview Report</asp:Literal>
				            </h5>
				            <div class="post-text">
					            <p><asp:Literal ID="litBody" runat="server">An overview report of the campaign providing enrollment, progress, completion and learning outcome data.</asp:Literal></p>
                                <a class="btn" href="/manage/campaigns/reports/campaign-overview-report.aspx?campaignID=<%=campaign_id %>"><i class="icon-external-link"></i> View Report</a>
				            </div>
			            </div>
		            </div>
                <br />
                <hr />
                &nbsp;
                <br />
		            <div class="blog-list-post small">
			            <div class="post-content span12">
				            <h5 class="post-title">
					            <asp:Literal ID="Literal1" runat="server">Message Analysis Report</asp:Literal>
				            </h5>
				            <div class="post-text">
					            <p><asp:Literal ID="Literal2" runat="server">An overview report with information about sent and received messages -- possible messages, actual messages and message follow up by requesting more info or clicking on embedded links.</asp:Literal></p>
                                <a class="btn" target="_blank" href="/resources/campaigns/<%=campaign_id %>/reports/message_analysis_<%=campaign_id %>.pdf"><i class="icon-external-link"></i> View Report</a>
				            </div>
			            </div>
		            </div>
                <br />
                <hr />
                &nbsp;
                <br />
		            <div class="blog-list-post small">
			            <div class="post-content span12">
				            <h5 class="post-title">
					            <asp:Literal ID="Literal3" runat="server">Learning Analysis Report</asp:Literal>
				            </h5>
				            <div class="post-text">
					            <p><asp:Literal ID="Literal4" runat="server">An overview report with information campaign learning -- focus is on the pre-and-post assessments.</asp:Literal></p>
                                <a class="btn" target="_blank" href="/resources/campaigns/<%=campaign_id %>/reports/learning_analysis_<%=campaign_id %>.pdf"><i class="icon-external-link"></i> View Report</a>
				            </div>
			            </div>
		            </div>
                <br />
                <hr />
                &nbsp;
                <br />
		            <div class="blog-list-post small">
			            <div class="post-content span12">
				            <h5 class="post-title">
					            <asp:Literal ID="Literal5" runat="server">STOP Report</asp:Literal>
				            </h5>
				            <div class="post-text">
					            <p><asp:Literal ID="Literal6" runat="server">An overview report with information about the participants who opted to STOP the campaign.</asp:Literal></p>
                                <a class="btn" target="_blank" href="/resources/campaigns/<%=campaign_id %>/reports/stop_analysis_<%=campaign_id %>.pdf"><i class="icon-external-link"></i> View Report</a>
				            </div>
			            </div>
		            </div>
        </div>
</div>