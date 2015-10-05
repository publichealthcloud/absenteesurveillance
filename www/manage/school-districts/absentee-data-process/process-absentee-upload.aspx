<%@ Page Title="" MasterPageFile="~/manage/manage.master"  Language="C#" AutoEventWireup="true" CodeFile="process-absentee-upload.aspx.cs" Inherits="process_absentee_upload" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
     
    <script type="text/javascript">
        function displayUpload() {
            var step2 = document.getElementById('step2_display');
            step2.style.visibility = 'visible';
            document.getElementById('main_features_upFile1').className = "btn";
        }
    </script>

    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-group"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Upload Absentee Data</asp:Label>
		            </h3>
	            </div>
            </div>
            <div style="height:10px;"></div>
            <table border="0" cellpadding="5">
                <tr>
                    <td colspan="2">
                        <div style="padding:15px 10px 15px 10px; background-color:#eee; border-color:darkgray;">
                            <strong>Instructions:</strong> Download the example file and add your data. Note below which columns are required and which are optional. You can leave the optional columns blank. <strong>DO NOT remove any columns from the example file.</strong><br />
                            </div>
                            <br />
                            <strong>COLUMNS</strong><br />
                            <blockquote>
                                <strong>AbsentDate</strong> <i>required</i><br />
                                <strong>SchoolName</strong> <i>required</i><br />
                                <strong>SchoolType</strong> <i>required</i>&nbsp;&nbsp;<i>(i.e. Elementary)</i><br />
                                <strong>GradeLevel</strong> <i>required</i>&nbsp;&nbsp;<i>(i.e. 3 or Kindergarten)</i><br />
                                <strong>ClassName</strong> <i>required</i>&nbsp;&nbsp;<i>(i.e. 3rd Grade)</i><br />
                                <strong>TeacherName</strong> <i>required</i>&nbsp;&nbsp;<i>(i.e. John.Doe)</i><br />
                                <strong>DaysInSession</strong> <i>required</i>&nbsp;&nbsp;<i>(i.e. 1 if uploaded every day)</i><br />
                                <strong>TotalEnrolled</strong> <i>required</i><br />
                                <strong>TotalAbsent</strong> <i>required</i><br />
                                <strong>TotalUnknown</strong> <i>required</i><br />
                                <strong>TotalOther</strong> <i>required</i><br />
                                <strong>TotalSick</strong> <i>required</i><br />
                                <strong>Gastrointestinal</strong> <i>required</i><br />
                                <strong>Respiratory</strong> <i>required</i><br />
                                <strong>Rash</strong> <i>required</i><br />
                                <strong>OtherIllness</strong> <i>required</i><br />
                                <strong>UnknownIllness</strong> <i>required</i><br />
                            </blockquote>                                      
                        <br />
                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <a href="/manage/school-districts/absentee-data-process/process-absentee-upload.aspx" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Restart Upload</a>&nbsp;&nbsp;
                    <asp:HyperLink ID="hplDownloadExample" CssClass="btn" runat="server"><i class="icon-download-alt"></i>&nbsp;&nbsp;Download Example File</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">
                <strong>Step 1: Select File</strong>
                </td>
                <td>
                     <asp:PlaceHolder ID="plhRestartUpload" runat="server" Visible="false">
                        <i class="icon-check"></i>&nbsp;Completed&nbsp;&nbsp;
                        
                    </asp:PlaceHolder>                   
                    <asp:PlaceHolder ID="plhUpload" runat="server">
                        <input type="file" class="btn btn-primary" id="upFile1" runat="server" onchange="displayUpload()" name="upFile" />
                    </asp:PlaceHolder>
                </td>
            </tr>
           
            <tr>
                <td width="150" valign="top">
                <strong>Step 2: Test File</strong>
                </td>
                <td class="Normal">
                    <asp:PlaceHolder ID="plhStep2Completed" runat="server" Visible="false">
                        <i class="icon-check"></i>&nbsp;Completed
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhStep2" runat="server" Visible="true">
                        <div id="step2_display" style="visibility:hidden">
                            <asp:Button CssClass="btn btn-primary" runat="server" ID="btnTestProcess" Text="Test Uploaded File" OnClick="btnTestProcess_OnClick" />
                        </div>
                        <asp:PlaceHolder ID="plhStep2Results" runat="server" Visible="false">
                            <div style="padding:10px 10px 10px 10px; height: 400px; width: 600px; background-color:#eee; border-color:darkgray; overflow:scroll;">
                                <asp:Label CssClass="NormalDarkGray" Text="No test results yet" ID="lblTestOutput" runat="server"></asp:Label>
                            </div>
                        </asp:PlaceHolder>
                    </asp:PlaceHolder>
                </td>
            </tr>
            
   
            <tr>
                <td width="100" valign="top">
                <strong>Step 3: Process File</strong>
                </td>
                <td>
                    <asp:PlaceHolder ID="plhStep3Completed" runat="server" Visible="false">
                        <i class="icon-check"></i>&nbsp;Completed
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plhStep3" runat="server" Visible="false">
                    <asp:Label ID="lblFileName" CssClass="NormalItalics" Text="No file selected" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button CssClass="btn btn-primary" runat="server" ID="btnProcess" Text="Process Data" OnClick="btnProcess_OnClick" />
                    <asp:Label CssClass="validation2" ID="lblUploadFail" runat="server"></asp:Label>
                </asp:PlaceHolder>

                </td>
            </tr>
            <tr>
                <td width="150" valign="top">
                    <strong>Step 4: View Results</strong>
                </td>
                <td>
                    <asp:PlaceHolder ID="plhStep4" runat="server" Visible="false">
                    <div style="padding:10px 10px 10px 10px; height: 400px; width: 600px; background-color:#eee; border-color:darkgray; overflow:scroll;">
                        <asp:Label ID="lblMessage" CssClass="NormalDarkGray" Text="No final results yet" runat="server"></asp:Label>
                    </div>
                    <br />
                    <a href="/manage/school-districts/daily-classroom-absentee-data.aspx" class="btn btn-primary">View All School Absentee Data</a>
                    </asp:PlaceHolder>
                </td>
            </tr>
            </table>
            </div>
        </div>
</asp:Content>
