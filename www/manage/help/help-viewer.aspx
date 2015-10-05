<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help-viewer.aspx.cs" MasterPageFile="~/simple.master" Inherits="qHlp_help_viewer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content_main">
    <link href="../quartz.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function changeImageManager(editor, args) {
            if (args.get_commandName() == "DocumentManager") {

                var callbackFunction = function (sender, args) {
                    var result = args.get_value();
                    result.src = "<%= imageURL %>" + result.src.substring(result.src.lastIndexOf("/") + 1, result.src.length);
                    result = Telerik.Web.UI.Editor.Utils.getOuterHtml(result);

                    editor.pasteHtml("This is a test", "DocumentManager");
                };

                args.set_callbackFunction(callbackFunction); //register callback function
            }
            if (args.get_commandName() == "ImageManager") {

                var callbackFunction = function (sender, args) {
                    var result = args.get_value(); //get returned value of ImageManager which is IMG element
                    result.src = "<%= imageURL %>" + result.src.substring(result.src.lastIndexOf("/") + 1, result.src.length);
                    result = Telerik.Web.UI.Editor.Utils.getOuterHtml(result); //get HTML source of the DOM element

                    editor.pasteHtml(result, "ImageManager");
                };

                args.set_callbackFunction(callbackFunction); //register callback function
            }
        }

    </script> 

    <div ID="divEdit" runat="server" style="background-color:#EEE; padding:5px;">
        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" Text="Edit Help" /><asp:Button ID="btnClose" runat="server" onclick="btnClose_Click" Text="Cancel Edit Help" />
    </div>
    <table cellpadding="5" width="100%">
        <asp:PlaceHolder ID="plhViewHelp" runat="server">
        <tr>
            <td colspan="2" class="NormalBold">
            <img src="<%= ResolveUrl ("~/images/PagingPrev.gif") %>" /> <asp:HyperLink ID="hplReturnNav" NavigateUrl="javascript:history.back(1)" Text="Back" runat="server"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
            <img src="<%= ResolveUrl ("~/images/print.gif") %>" /> <asp:HyperLink ID="hplPrint" CssClass="NormalBold" runat="server">Print</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2"><br />
                <asp:Label class="NormalRed" ID="lblMessage" runat="server"></asp:Label>
                <asp:Label ID="lblTitle" CssClass="CapsHeader3" runat="server" Text="Help Topic"></asp:Label>
                <div style="height:20px;"></div>
                <span class="NormalDarkGray"><asp:Literal ID="litSummary" runat="server"></asp:Literal></span>
                <div style="height:5px;"></div>
                <div style="padding:5px; background-color:#EEE; width:max-content;"><span class="NormalDarkGrayItalics"><asp:Literal ID="litKeywords" runat="server"></asp:Literal></span></div>
                <div style="padding-top:20px;">
                    <span class="Normal"><asp:Literal ID="litBody" runat="server"></asp:Literal></span>
                </div>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhEditHelp" runat="server">
        <tr>
            <td class="NormalBold" width="150" valign="top">
                Title *
            </td>
            <td class="Normal">
                <telerik:RadTextBox runat="server" ID="txtTitle" Width="500px" MaxLength="100"></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="NormalBold" width="150" valign="top">
                Summary *
            </td>
            <td class="Normal">
                <telerik:RadTextBox runat="server" ID="txtSummary" TextMode="MultiLine" Height="40px" Width="500px" MaxLength="500"></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="NormalBold" width="150" valign="top">
                Keywords *
            </td>
            <td class="Normal">
                <telerik:RadTextBox runat="server" ID="txtKeywords" TextMode="MultiLine" Height="40px" Width="500px" MaxLength="500"></telerik:RadTextBox>
            </td>
        </tr>
    <tr>
        <td colspan="2" class="Normal">
            <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools"  Height="500px"
                AutoResizeHeight="True" Width="670px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/www2/help" UploadPaths="~/www2/help" DeletePaths="~/www2/help" />
                <DocumentManager ViewPaths="~/www2/help" 
                    UploadPaths="~/www2/help" DeletePaths="~/www2/help"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/www2/help" 
                    UploadPaths="~/www2/help" DeletePaths="~/www2/help"></ImageManager>
                    <DocumentManager ViewPaths="~/www2/help" 
                    UploadPaths="~/www2/help" DeletePaths="~/www2/help"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
            </td>
        </tr>
        </asp:PlaceHolder>
    </table>
</asp:Content>
