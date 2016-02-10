<%@ Page Title="" Language="C#" MasterPageFile="~/www/sass.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="www_lms_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <div class="row-fluid">
        <div class="span12">
            <div id="profile-background-pic" style="z-index: 1; position: relative;">
                    <div style="position: relative; left: 0px; top: 0px;"><img src="images/sass-background-graphic.jpg" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div style="height:25px;"></div>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <span><h1><asp:Literal runat="server" ID="litIntroTitle"></asp:Literal>Student Absentee Surveillance System (SASS)</h1></span>
            </div>
        </div>
        <div class="row-fluid">
            <div class="span9">
                <div style="height:30px;"></div>
               <h3>Introduction</h3> 
        SASS is an online absentee surveillance system designed for use by school districts and public health agencies. The system is optimized to process, analyze and display daily absentee data for a school district. The system not only displays absentee trends but also generates a range of different absentee warnings based on complex rules. <strong>The SASS platform is completely free to anyone who wants to download and run the software.</strong>
                <br /><br />
                <asp:HyperLink runat="server" ID="hplDownloadNow" CssClass="btn btn-danger" NavigateUrl="https://github.com/publichealthcloud/absenteesurveillance" Target="_blank"><i class="glyphicon-download_alt"></i> Download SASS Solution</asp:HyperLink>&nbsp;&nbsp;
                <asp:HyperLink runat="server" ID="phlGettingStartedDocument" NavigateUrl="/resources/school absentee surveillance system - getting started.pdf" Target="_blank" CssClass="btn"><i class="glyphicon-circle_info"></i> Getting Started Document</asp:HyperLink>
<br /><br />
        The solution consists of a district wide absentee dashboard as well as school-specific dashboards. The solution also consists of tools for uploading data files and downloading processed data. All data loaded into the system can be easily downloaded for additional processing.

                <h3>Dashboard Views</h3>
                Click on the images below to see the full-sized dashboards.<br /><br />
                <a href="../resources/school-district-dashboard.png" target="_blank"><img src="../resources/school-district-dashboard.png.ashx?width=250&height=300&mode=crop" /></a>
                <a href="../resources/school-dashboard.png" target="_blank"><img src="../resources/school-dashboard.png.ashx?width=250&height=300&mode=crop" /></a>
<h3>Necessary Data</h3>
The solution requires two sets of data to operate:
        <ul>
            <li>
(1)	<strong>A <u>one-time</u> upload of school information for a district</strong> – an Excel file with the name, address, educational level of every school in the district
            </li>
            <li>
(2)	<strong>A <u>daily</u> upload of school absentee data for the district</strong> – Excel files with the absentee information for each school and classroom
            </li>
        </ul>
<i>The daily uploads of absentee data can be done manually or using an automated SFTP option.</i><br /><br />
        <p>
            <code><strong>Example One-Time School Data (NOTE: detailed example files contain the full spec):</strong><br />

School – Washington Elementary<br />
Address1 – 123 Main Street<br />
City - Layton<br />
State - Utah<br />
Zip – 84040<br />
Level - Elementary<br />
Phone – (801)123-4567<br />
Fax – (801)765-4321<br />
                <br />
<strong>Example School Daily Absentee Data (NOTE: detailed example files contain the full spec):</strong><br />
CurrentDay- June 4, 2015<br />
SchoolName – Washington<br />
GradeLevel - 1<br />
ClassName – First Grade<br />
Teacher – K.D.<br />
TotalEnrolled - 23<br />
TotalAbsent - 2<br />
TotalUnknown - 1<br />
TotalSick - 1<br />
Gastrointestinal - 0<br />
Repiratory - 1<br />
Rash - 0<br />
OtherIllness - 0<br />
UnknownIllness - 0<br />
            </code>
        </p>
                      <span><asp:Literal ID="litIntroText" runat="server"></asp:Literal></span>
                <div style="height:20px;"></div>
            </div>
            <div class="span3">
            <strong>SASS Partners include:</strong> University of Utah School of Public Health and Davis County, Utah (School District and Department of Public Health).
            </div>
        </div>  
        <div class="row-fluid">
            <div class="span12">
                <div style="height:40px;"></div>
            </div>
        </div>       

</asp:Content>

