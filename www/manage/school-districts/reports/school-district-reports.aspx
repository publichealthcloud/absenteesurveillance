<%@ Page Title="" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" AutoEventWireup="true" CodeFile="school-district-reports.aspx.cs" Inherits="manage_school_districts_reports" %>
<%@ Register Src="~/manage/school-districts/controls/school-district-sidebar.ascx" TagPrefix="epg" TagName="schooldistrictsidebar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="school_nav" Runat="Server">
    <epg:schooldistrictsidebar runat="server" ID="schooldistrictsidebar" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="school_main" Runat="Server">
<div class="row-fluid">                  
    <div class="span12">
        <h2><asp:Literal ID="litTitle" runat="server">Daily Absentee Overview</asp:Literal></h2>
        <div style="height:10px;"></div>
        <a href="" class="btn btn-large"><i class="glyphicon-step_backward"></i></a>
        <a href-="" class="btn btn-large"><asp:Literal ID="litDataDate" runat="server">Monday, December 12, 2014</asp:Literal></a>
        <a href="" class="btn btn-large"><i class="glyphicon-step_forward"></i></a>
        <a href="" class="btn btn-danger btn-large"><i class="icon-dashboard"></i> View Complete Dashboard</a>
        <div style="height:15px;"></div>
            <strong>Jump to Date:</strong> 
            <telerik:RadDatePicker ID="rdtDataDate" runat="server"></telerik:RadDatePicker>
            <asp:RequiredFieldValidator 
                ID="rfvStartTime" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rdtDataDate"
                ErrorMessage="Valid from date required">
            </asp:RequiredFieldValidator>
    </div>
</div>
<div class="row-fluid">                             
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="icon-search"></i> Summary
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="litTableSummaryWidth" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Data Processed</td>
				        <td><asp:Literal ID="litSummaryDataProcessed" runat="server">3/19/2014 at 1:30am</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Absent</td>
				        <td><asp:Literal ID="litTotalAbsent" runat="server">1,624</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Unknown</td>
				        <td><asp:Literal ID="litTotalUnknown" runat="server">546</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Other</td>
				        <td><asp:Literal ID="litTotalOther" runat="server">234</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Total Sick</td>
				        <td><asp:Literal ID="litTotalSick" runat="server">428</asp:Literal></td>
			        </tr>
    			    <tr>
				        <td>Absentee Rate</td>
				        <td><asp:Literal ID="litAbsenteeRate" runat="server">528</asp:Literal></td>
			        </tr>
        			    <tr>
				        <td>Warning Level</td>
				        <td><asp:Literal ID="Literal2" runat="server">1.2</asp:Literal></td>
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
                        Health Warnings
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="litTableDescriptionWidth" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="litCampaignDescription" runat="server">No health notices.</asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
        </div>
        </div>
</div>

<div class="row-fluid">                             
    <div class="span6">
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="glyphicon-stats"></i>
                        <asp:Literal ID="litSchoolLevelDetails" runat="server" Text="School Level Details"></asp:Literal>
				    </h3>
				</div>
			</div>
			<div class="box-content">
            <table class="table table-hover table-nomargin" <asp:Literal id="Literal3" runat="server"/>>
		        <tbody>
			        <tr>
				        <td>Total Elementary Schools Reporting</td>
				        <td><asp:Literal ID="litElementarySchools" runat="server">52</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Students Enrolled</td>
				        <td><asp:Literal ID="litElementarySchoolsStudentsEnrolled" runat="server">1,624</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Absentee Rate</td>
				        <td><asp:Literal ID="litElementarySchoolAbsenteeRate" runat="server">546</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elemenatary School Warning Level</td>
				        <td><asp:Literal ID="litElementarySchoolWarningLevel" runat="server">234</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total Junior Highs Reporting</td>
				        <td><asp:Literal ID="litJuniorHighSchools" runat="server">52</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Students Enrolled</td>
				        <td><asp:Literal ID="litJuniorHighStudents" runat="server">1,624</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Absentee Rate</td>
				        <td><asp:Literal ID="litJunionHighAbsenteeRate" runat="server">546</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elemenatary School Warning Level</td>
				        <td><asp:Literal ID="litJuniorHighWarningLevel" runat="server">234</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Total High Schools Reporting</td>
				        <td><asp:Literal ID="litHighSchools" runat="server">52</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Students Enrolled</td>
				        <td><asp:Literal ID="litHighSchoolStudentsEnrolled" runat="server">1,624</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Absentee Rate</td>
				        <td><asp:Literal ID="litHighSchoolAbsenteeRates" runat="server">546</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Warning Level</td>
				        <td><asp:Literal ID="litHighSchoolWarningLevel" runat="server">234</asp:Literal></td>
			        </tr>
		        </tbody>
	        </table>
        </div>
    </div>
    <div class="span6">
        <asp:Literal ID="Literal11" runat="server"></asp:Literal>
            <div class="box">
                <div class="box-title">
				    <h3>
                        <i class="glyphicon-pie_chart"></i>
                        School Level Charts
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <table class="table table-hover table-nomargin" <asp:Literal id="Literal12" runat="server"/>>
		            <tbody>
			            <tr>
				            <td><asp:Literal ID="Literal13" runat="server"></asp:Literal></td>
			            </tr>
                    </tbody>
                </table>
        </div>
        </div>
</div>
<!--
<div class="row-fluid">                  
    <div class="span12">
        <div class="box">
            <div class="box-title">
				<h3>
                    <i class="icon-bar-chart"></i>
                    Available Reports
				</h3>
			</div>
        </div>
	        <div class="box-content nopadding">
                <br>
		            <div class="blog-list-post small">
			            <div class="post-content span12">
				            <h5 class="post-title">
					            Weekly Influenza Report - Week Ending 3/1/2014 (Week 9)
				            </h5>
				            <div class="post-text">
					            <p>Influenza activity in Davis County during Week 9 (February 23 - March 1, 2014) rose slightly and is back at a low/moderate level.</p>
                                <a class="btn" href="http://resources-projects-daviscounty.quartzhealthsolutions.com/school-districts/1/reports/2014/w9/week9.pdf"><i class="icon-external-link"></i> View Report</a>
				            </div>
			            </div>
		            </div>
                <br>
                <hr>
                &nbsp;  
        </div>
        </div>
    </div>
-->

</asp:Content>



