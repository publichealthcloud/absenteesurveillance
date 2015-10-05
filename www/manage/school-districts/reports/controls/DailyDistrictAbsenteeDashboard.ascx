<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailyDistrictAbsenteeDashboard.ascx.cs" Inherits="school_districts_reports_DailyDistrictAbsenteeDashboard" %>

<style>
    html, body, #map-canvas {
    height: 700px;
    margin: 0px;
    padding: 0px
    }
</style>
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
			</div>
		</div>
		<div class="box-content">
            <table cellpadding="5">
                <tr>
                    <td><div style="width:25px;"></div></td>
                    <td><strong>School Status by Color:</strong></td>
                    <td bgcolor="#C0C0C0"><font color="white">Normal</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#e51400"><font color="white">3 Sigma Warning</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#f8a31f"><font color="white">Sustained 2-3 Sigma Watch</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#ffff00"><font color="black">Consistently Above Mean Watch</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#393"><font color="white">2 Sigma Alert</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#368ee0"><font color="white">Sustained Above 1 Sigma Alert</font></td>
                    <td><div style="width:1px;"></div></td>
                    <td bgcolor="#a200ff"><font color="white">Sustained Increase Alert</font></td>
                </tr>
            </table>
            <div style="height:10px;"></div>
            <table>
                <tr>
                    <td><div style="width:37px;"></div></td>
                    <td><strong>District Rates:&nbsp;</strong></td>
                    <td><div style="width:53px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:gray;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>District Daily Absentee Rate</td>
                    <td><div style="width:45px;"></div></td>
                    <td><div style="width:25px; height:10px; background-color:black;"></div></td>
                    <td><div style="width:1px;"></div></td>
                    <td>District Moving Average Absentee Rate</td>
                </tr>
            </table>
            <asp:Literal ID="litSchools" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<div class="row-fluid">
    <div class="span12">
		<div class="box-content">
            <div style="height:10px;"></div>
            <h4><asp:Literal ID="litMapTitle" runat="server">School Absentee Status Map</asp:Literal></h4>
            <div id="map-canvas"></div>
            <asp:Literal ID="litMap" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<div class="row-fluid">                             
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-search"></i> District Summary
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableSummaryWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Calendar Date</td>
				        <td><asp:Literal ID="litCalendarDate" runat="server">1/1/2014 at 1:30am</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Enrolled</td>
				        <td><asp:Literal ID="litTotalEnrolled" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Absent</td>
				        <td><asp:Literal ID="litTotalAbsent" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Total Sick</td>
				        <td><asp:Literal ID="litTotalSick" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Absentee Rate</td>
				        <td><asp:Literal ID="litAbsenteeRate" runat="server">0</asp:Literal>%</td>
			        </tr>
    			    <tr>
				        <td>Historic Absentee Rate</td>
				        <td><asp:Literal ID="litHistoricAbsenteeRate" runat="server">0</asp:Literal>%</td>
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
                    <i class="icon-warning-sign"></i>
                   Warnings
				</h3>
			</div>
		</div>
		<div class="box-content">
            <ul class="tabs tabs-inline tabs-top">
			    <li class='active'>
				    <a href="#first11" data-toggle='tab'><asp:Literal ID="litNumAbsenteeWarnings" runat="server"></asp:Literal> Absentee Warnings</a>
			    </li>
			    <li>
				    <a href="#second22" data-toggle='tab'><asp:Literal ID="litNumSymptomWarnings" runat="server"></asp:Literal> Symptom Warnings</a>
			    </li>
		    </ul>
		<div class="tab-content padding tab-content-inline tab-content-bottom">
			<div class="tab-pane active" id="first11">
                <blockquote>
                    <strong>Absentee Warnings</strong> focus on overall absentee rates and trends.
                </blockquote>
                <table class="table table-hover table-nomargin">
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litHealthWarnings" runat="server">No health warnings for this date.</asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
			</div>
			<div class="tab-pane" id="second22">
				<blockquote>
                    <strong>Symptom Warnings</strong> focus on symptom-related trends.
                </blockquote>
                <table class="table table-hover table-nomargin">
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litSymptomWarnings" runat="server">No symptom warnings for this date.</asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
			</div>
        </div>
    </div>
   </div>
</div>
<div class="row-fluid">                  
    <div class="span6">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <div class="box">
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="Literal2" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litTypesOfIllnessesChartGoogle" runat="server"></asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
            </div>
        </div>
    <div class="span6">
        <asp:Literal ID="Literal4" runat="server"></asp:Literal>
            <div class="box">
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="Literal5" runat="server"/>>
		        <tbody>
			        <tr>
				        <td><asp:Literal ID="litTypesOfSymptomsChartGoogle" runat="server"></asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
</div>
<div class="row-fluid">                             
    <div class="span12">
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="glyphicon-stats"></i> <asp:Literal ID="lit28DayTitle" runat="server">4 Weeks of School Absentee Data</asp:Literal>
				</h3>
                <ul class="tabs">
                <li>
                    <div class="btn-group">
			            <a href="#" data-toggle="dropdown" class="btn dropdown-toggle"><i class="icon-filter"></i> Change Length of Time <span class="caret"></span></a>
			            <ul class="dropdown-menu">
                            <asp:Literal ID="litFilterByDateOptions" runat="server"></asp:Literal>
			            </ul>
		            </div>
                </li>
		    </ul>
			</div>
		</div>
		<div class="box-content">
            <asp:Literal ID="lit28DayDistrictRateChart" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="span12">
		<div class="box-content">
            <asp:Literal ID="lit28DayDistrictIllnessChart" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="span12">
		<div class="box-content">
            <asp:Literal ID="lit28DayDistrictWarningChart" runat="server"></asp:Literal>
        </div>
    </div>
</div>

