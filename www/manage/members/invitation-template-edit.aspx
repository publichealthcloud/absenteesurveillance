<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="invitation-template-edit.aspx.cs" Inherits="edit_article" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

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

    <link href="../quartz.css" rel="stylesheet" type="text/css" />

<div style="padding-left:10px;">
    <br />
    <span><strong>&nbsp;<asp:Label ID="lblTitle" CssClass="CapsHeader3" runat="server" Text="Edit Invitation Template"></asp:Label></strong></span>
    <div>
    <span class="Normal"> <asp:HyperLink ID="hplRefreshTop" runat="server"><img src="../images/refresh.gif" border="0">&nbsp;&nbsp;Refresh</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;    
    <asp:Button ID="btnSave_top" runat="server" Text="SAVE INVITATION TEMPLATE" onclick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;
    </div>
    <table cellpadding="5">
        <tr>
            <td colspan="2" class="NormalBold">TOOLS:&nbsp;&nbsp;
            <img src="../images/printpreview.gif" />&nbsp;<asp:HyperLink ID="hplPrint" CssClass="NormalBold" runat="server">Preview Invitation</asp:HyperLink>
                <asp:LinkButton ID="btnCustomPrint" runat="server" onclick="btnPrintCustomForm_Click" Visible="false" CssClass="NormalBold">Preview Invitation</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="NormalBold">
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
            </td>
        </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Header:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reHeader" SkinID="DefaultSetOfTools" 
                Height="350px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/www2/site" UploadPaths="~/www2/site" DeletePaths="~/www2/site" />
                <DocumentManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></ImageManager>
                    <DocumentManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Footer:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reFooter" SkinID="DefaultSetOfTools" 
                Height="350px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/www2/site" UploadPaths="~/www2/site" DeletePaths="~/www2/site" />
                <DocumentManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></ImageManager>
                    <DocumentManager ViewPaths="~/www2/site" 
                    UploadPaths="~/www2/site" DeletePaths="~/www2/site"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="Normal">            
            <span class="Normal"> <asp:HyperLink ID="hplRefreshBottom" runat="server"><img src="../images/refresh.gif" border="0">&nbsp;&nbsp;Refresh</asp:HyperLink></span>&nbsp;&nbsp;&nbsp;&nbsp;  
            <asp:Button ID="btnSave" runat="server" Text="SAVE INVITATION TEMPLATE" onclick="btnSave_OnClick" />&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
</asp:Content>


