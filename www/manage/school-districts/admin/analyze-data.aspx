<%@ Page Title="" MasterPageFile="~/manage/manage.master"  Language="C#" AutoEventWireup="true" CodeFile="analyze-data.aspx.cs" Inherits="school_district_analyze_data" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-cogs"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Analyze Absentee Data</asp:Label>
		            </h3>
	            </div>
            </div>
            <div style="height:10px;"></div>
            <table border="0" cellpadding="5">
                <tr>
                    <td colspan="2">
                        <div style="padding:15px 10px 15px 10px; background-color:#eee; border-color:darkgray;">
                            <strong>Instructions:</strong> Select a data range below and click the <strong>Analyze Data</strong> button to run or re-run the analysis of the data from the selected dates.
                            All prior analysis, status displays and health warnings will be deleted and re-created. To better understand the analysis, the analysis variables are visible below.
                            <br />
                            </div>
                            <br />
                            <strong>ANALYSIS VARIABLES</strong><br />
                            <blockquote>
                                <asp:Literal ID="litAnalysisVariables" runat="server"></asp:Literal>
                            </blockquote>                                      
                        <br />
                    </td>
                </tr>
            <tr>
                <td width="150" valign="top">
                <strong>Step 1: Select a Date</strong>
                </td>
                <td>
                     <telerik:RadDatePicker ID="rdtStartDate" runat="server"></telerik:RadDatePicker>
                </td>
            </tr>
           <asp:PlaceHolder ID="plhEndDate" runat="server" Visible="false">
            <tr>
                <td width="150" valign="top">
                <strong>Step 2: Select End Date</strong>
                </td>
                <td class="Normal">
                    <telerik:RadDatePicker ID="rdtEndDate" runat="server"></telerik:RadDatePicker>
                </td>
            </tr>
            </asp:PlaceHolder>
            <tr>
                <td width="150" valign="top">
                    <strong>Step 2: Run Analysis</strong>
                </td>
                <td>
                    <asp:Button CssClass="btn btn-primary" ID="btnAnalyzeData" runat="server" OnClick="btnAnalyzeData_Click" Text="Analyze Data" />
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">
                    <strong>Step 3: View Results</strong>
                </td>
                <td>
                    <asp:PlaceHolder ID="plhStep4" runat="server" Visible="false">
                    <div style="padding:10px 10px 10px 10px; height: 60px; width: 600px; background-color:#eee; border-color:darkgray; overflow:scroll;">
                        <asp:Label ID="lblMessage" CssClass="NormalDarkGray" Text="No final results yet" runat="server"></asp:Label>
                    </div>
                    <br />
                    <a href="/manage/school-districts/school-health-warnings.aspx" class="btn btn-primary">View Warnings</a>
                    </asp:PlaceHolder>
                </td>
            </tr>
            </table>
            </div>
        </div>
</asp:Content>
