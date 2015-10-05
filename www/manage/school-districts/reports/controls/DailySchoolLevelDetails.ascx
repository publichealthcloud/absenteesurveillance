<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailySchoolLevelDetails.ascx.cs" Inherits="manage_school_districts_reports_controls_DailySchoolLevelDetails" %>
<%@ Register Src="~/manage/school-districts/reports/controls/DailySummaryCharts.ascx" TagPrefix="epg" TagName="DailySummaryCharts" %>

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
                        <td colspan="2"><strong>Elementary Schools</strong></td>
			        </tr>
                    <tr>
				        <td>Total Elementary Schools Reporting</td>
				        <td><asp:Literal ID="litElementarySchools" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Students Enrolled</td>
				        <td><asp:Literal ID="litElementarySchoolsStudentsEnrolled" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Students Absent</td>
				        <td><asp:Literal ID="litElementarySchoolStudentsAbsent" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elementary School Absentee Rate</td>
				        <td><asp:Literal ID="litElementarySchoolAbsenteeRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Elemenatary School Warning Level</td>
				        <td><asp:Literal ID="litElementarySchoolWarningLevel" runat="server">20</asp:Literal></td>
			        </tr>
			        <tr>
                        <td colspan="2"><strong>Junior High Schools</strong></td>
			        </tr>
			        <tr>
				        <td>Total Junior Highs Reporting</td>
				        <td><asp:Literal ID="litJuniorHighSchools" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Junior High School Students Enrolled</td>
				        <td><asp:Literal ID="litJuniorHighStudents" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Junior High Students Absent</td>
				        <td><asp:Literal ID="litJuniorHighStudentsAbsent" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Junior High School Absentee Rate</td>
				        <td><asp:Literal ID="litJunionHighAbsenteeRate" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>Junior High School Warning Level</td>
				        <td><asp:Literal ID="litJuniorHighWarningLevel" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
                        <td colspan="2"><strong>High Schools</strong></td>
			        </tr>
			        <tr>
				        <td>Total High Schools Reporting</td>
				        <td><asp:Literal ID="litHighSchools" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Students Enrolled</td>
				        <td><asp:Literal ID="litHighSchoolStudentsEnrolled" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Students Absent</td>
				        <td><asp:Literal ID="litHighSchoolStudentsAbsent" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Absentee Rate</td>
				        <td><asp:Literal ID="litHighSchoolAbsenteeRates" runat="server">0</asp:Literal></td>
			        </tr>
			        <tr>
				        <td>High School Warning Level</td>
				        <td><asp:Literal ID="litHighSchoolWarningLevel" runat="server">0</asp:Literal></td>
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
                        Quick Charts
				    </h3>
				</div>
			</div>
			<div class="box-content">
                <epg:DailySummaryCharts runat="server" ID="DailySummaryCharts" />
            </div>
        </div>
</div>