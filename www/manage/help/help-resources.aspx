<%@ Page Title="" Language="C#" MasterPageFile="~/simple.master" AutoEventWireup="true" CodeFile="help-resources.aspx.cs" Inherits="custom_manage_images" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_main" Runat="Server">
<link href="../quartz.css" rel="stylesheet" type="text/css" />
    <!--
    <script type="text/javascript">
        //<![CDATA[
        function OnClientItemSelected(sender,args)
        {
            var pvwImage = $get("pvwImage");
            var imageSrc = args.get_path();

            LogEvent(args.get_item().get_name());

            if(imageSrc.match(/\.(gif|jpg)$/gi))
            {
                pvwImage.src = imageSrc;
                pvwImage.style.display = "";
            }
            else
            {
                pvwImage.src = imageSrc;
                pvwImage.style.display = "none";
            }
        }
        function LogEvent(text) {

            //var selectedFile = = $get("selectedFile");
            selectedFile = document.getElementById("selectedFile"); 
    		selectedFile.value = text;

            var eventConsole = $get("eventConsole");
            eventConsole.innerHTML = "<img src=\"../images/library/" + text + "\">";
        }
        //]]>
    </script>
    -->
        <link href="../quartz.css" rel="stylesheet" type="text/css" />
        <span><strong>&nbsp;<asp:Label ID="lblTitle" class="CapsHeader3" runat="server" Text="Help Library"></asp:Label></strong></span>
        <telerik:RadFileExplorer runat="server" ID="FileExplorer1" Width="650px" Height="550px"
            EnableCreateNewFolder="True">
            <Configuration ViewPaths="~/www2/help/" MaxUploadFileSize="256000000" UploadPaths="~/www2/help/" DeletePaths="~/www2/" />
        </telerik:RadFileExplorer>

</asp:Content>

