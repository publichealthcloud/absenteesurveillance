<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingDashboard.ascx.cs" Inherits="manage_site_controls_SiteDashboard" %>
<%@ Register Src="~/reports/GoogleChartReferece.ascx" TagPrefix="epg" TagName="GoogleChartReferece" %>

<epg:GoogleChartReferece runat="server" id="GoogleChartReferece" />

<asp:Literal ID="litJSMap" runat="server"></asp:Literal>
<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

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
				        <td>Created</td>
				        <td><asp:Literal ID="litCreated" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members Enrolled</td>
				        <td><asp:Literal ID="litEnrolledMembers" runat="server">0</asp:Literal>&nbsp;&nbsp;<a href="/manage/members/learning/user-training-list.aspx?trainingID=<%=training_id %>" class="btn btn-small">View Enrolled Members</a></td>
			        </tr>
			        <tr>
				        <td>Members Finished</td>
				        <td><asp:Literal ID="litFinishedMembers" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Members Passed Out</td>
				        <td><asp:Literal ID="litPassedOutMembers" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Completion Rate</td>
				        <td><asp:Literal ID="litCompletionRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Pre-Assessment Pass Rate</td>
				        <td><asp:Literal ID="litPreAssessmentPassRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Post-Assessment Pass Rate</td>
				        <td><asp:Literal ID="litPostAssessmentPassRate" runat="server">0</asp:Literal></td>
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
                        About the Training
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <asp:Literal ID="litAboutTraining" runat="server"></asp:Literal>
            </div>
        </div>
</div>
<div class="row-fluid">
	<div class="span12">
		<div class="box">
			<div class="box-title">
				<h3>
					<i class="glyphicon-stats"></i>
					Activity
				</h3>
			</div>
			<div class="box-content nopadding">
            <table>
                <tr>
                    <td><div style="width:37px;"></div></td>
                    <td><strong>Trends:&nbsp;</strong></td>
                    <td><div style="width:53px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:gray;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>Members Starting Training</td>
                    <td><div style="width:45px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:black;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>Members Finishing Training</td>
                </tr>
            </table>
                <asp:Literal ID="litTrainingActivity" runat="server"></asp:Literal>>
			</div>
		</div>
	</div>
</div>
<div class="row-fluid">                             
    <div class="span6">
        <asp:Literal ID="Literal6" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="glyphicon-pie_chart"></i>
                        Progress
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <asp:Literal ID="litCompletionChart" runat="server"></asp:Literal>
            </div>
    </div>
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="glyphicon-charts"></i>
                        Assessments
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <asp:Literal ID="litAssessmentChart" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<div class="row-fluid">
	<div class="span12">
		<div class="box">
			<div class="box-title">
				<h3>
					<i class="icon-picture"></i>
					Training Slides
				</h3>
			</div>
			<div class="box-content nopadding">
				<ul class="gallery">
                    <asp:Literal ID="litSlides" runat="server"></asp:Literal>
				</ul>
			</div>
		</div>
	</div>
</div>