<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailySchoolAbsenteeDashboard.ascx.cs" Inherits="school_districts_reports_DailySchoolAbsenteeDashboard" %>

<style>
    html, body, #map-canvas {
    height: 350px;
    margin: 0px;
    padding: 0px
    }
</style>
<script type="text/javascript"
    src="https://maps.googleapis.com/maps/api/js?key=<%= GoogleAPIKey %>">
</script>

<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

<div class="row-fluid">                             
    <div class="span4">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-search"></i> School Info
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableSummaryWidth" runat="server"/>>
		        <tbody>
   			        <tr>
				        <td><strong>School</strong></td>
				        <td><asp:Literal ID="litSchoolTitle" runat="server"></asp:Literal></td>
			        </tr>
    			    <tr>
				        <td><strong>District</strong></td>
				        <td><asp:Literal ID="litDistrict" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td><strong>Level</strong></td>
				        <td><asp:Literal ID="litSchoolLevel" runat="server"></asp:Literal></td>
			        </tr>
			        <tr>
				        <td><strong>Students</strong></td>
				        <td><asp:Literal ID="litTotalStudents" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td><strong>Address</strong></td>
				        <td><asp:Literal ID="litSchoolAddress" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td><strong>Phone</strong></td>
				        <td><asp:Literal ID="litSchoolPhone" runat="server">0</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td><strong>Fax</strong></td>
				        <td><asp:Literal ID="litSchoolFax" runat="server">0</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span4">
        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="icon-map-marker"></i> Map
				</h3>
			</div>
		</div>
		<div class="box-content">
            <div id="map-canvas"></div>
            <asp:Literal ID="litMap" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="span4">
        <asp:Literal ID="litTableDescriptionSpacer" runat="server"></asp:Literal>
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="icon-flag"></i> Status
				</h3>
			</div>
		</div>
		<div class="box-content">
            <ul class="tabs tabs-inline tabs-top">
			    <li class='active'>
				    <a href="#status" data-toggle='tab'> <asp:Literal ID="litStatusCurrentDay" runat="server"></asp:Literal></a>
			    </li>
			    <li>
				    <a href="#prior" data-toggle='tab'> 28 Days</a>
			    </li>
		    </ul>
		<div class="tab-content padding tab-content-inline tab-content-bottom">
			<div class="tab-pane active" id="status">
                <div style="align-content: center;">
                    <table cellpadding="5" width="100%">
                        <asp:PlaceHolder ID="plhStatusNormal" runat="server">
                        <tr>
                            <td width="100%" bgcolor="#c0c0c0" align="center" valign="center">
                                <font color="white"><asp:Literal ID="litNormalHeader" runat="server"></asp:Literal></font>
                                <font size="16" color="white">Normal <i class="icon-ok-sign"></i><br />&nbsp;</font>
                            </td>
                        </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhStatusCaution" runat="server">
                        <tr>
                            <td width="100%" bgcolor="#FFA500" align="center" valign="center">
                                <font color="white"><asp:Literal ID="litCautionHeader" runat="server"></asp:Literal></font>
                                <font size="16">Caution <i class="icon-exclamation-sign"></i><br />&nbsp;</font>
                            </td>
                        </tr>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="plhStatusWarning" runat="server">
                        <tr>
                            <td width="100%" <asp:Literal ID="litWarningStatusColor" runat="server"></asp:Literal> align="center" valign="center">
                                <font color="white"><asp:Literal ID="litWarningHeader" runat="server"></asp:Literal></font>
                                <font size="16" color="white">Warning <i class="icon-warning-sign"></i><br />&nbsp;</font>
                                <font color="white"><asp:Literal ID="litWarningFooter" runat="server"></asp:Literal></font>
                            </td>
                        </tr>
                        </asp:PlaceHolder>                        
                        <tr>
                            <td align="center"><asp:Literal ID="litStatusDescription" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                </div>
			</div>
			<div class="tab-pane" id="prior">
                <table class="table table-hover table-nomargin" cellpadding="2">
		            <tbody>
                        <tr><td align="center" width="30%"><strong>Date</strong></td><td align="center" width="70%"><strong>Status</strong></td></tr>
                        <asp:Literal ID="litSchoolStatusPrior" runat="server">No prior values.</asp:Literal>
                    </tbody>
                </table>
			</div>
        </div>
    </div>
    </div>
</div>
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
            <asp:Literal ID="litClassrooms" runat="server"></asp:Literal>
        </div>
    </div>
</div>
<div class="row-fluid">                             
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-search"></i> Enrollment & Absentee Info
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="Literal7" runat="server"/>>
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
        <asp:Literal ID="Literal14" runat="server"></asp:Literal>
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
				    <a href="#absenteewarnings" data-toggle='tab'><asp:Literal ID="litNumAbsenteeWarnings" runat="server"></asp:Literal> Absentee Warnings</a>
			    </li>
			    <li>
				    <a href="#symptomwarnings" data-toggle='tab'><asp:Literal ID="litNumSymptomWarnings" runat="server"></asp:Literal> Symptom Warnings</a>
			    </li>
		    </ul>
		<div class="tab-content padding tab-content-inline tab-content-bottom">
			<div class="tab-pane active" id="absenteewarnings">
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
			<div class="tab-pane" id="symptomwarnings">
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
                    <i class="glyphicon-stats"></i> <asp:Literal ID="lit28DayTitle" runat="server">4 Weeks of District Absentee Data</asp:Literal>
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
            <asp:Literal ID="lit28DaSchoolRateChart" runat="server"></asp:Literal>
        </div>
    </div>
</div>


