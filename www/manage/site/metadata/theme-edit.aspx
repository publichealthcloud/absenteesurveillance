<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="theme-edit.aspx.cs" Inherits="edit_theme" %>
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
<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">THEMES</asp:Label>
		</h3>
            <ul class="tabs">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="themes-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Themes</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="theme-edit.aspx?themeID=<%= theme_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE THEME" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    <div style="height:10px;"></div>
    <table border="0" cellpadding="5">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this theme? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Theme"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <i class="icon-external-link"></i>&nbsp; <asp:HyperLink ID="hplPreviewTheme" runat="server">Preview Theme</asp:HyperLink>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Theme:
        </td>
        <td>
            <asp:Textbox ID="txtName" class="input-block-level" runat="server" Width="500px"></asp:Textbox>      
        </td>
    </tr>
    <asp:PlaceHolder ID="plhActiveSiteName" runat="server" Visible="false">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Active Site Nav:
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtURL" runat="server" Width="500px"></telerik:RadTextBox>&nbsp;<asp:LinkButton CssClass="Normal" ID="btnEnableSiteNav" Text="Edit Site nav" runat="server" OnClick="btnEnableSiteNav_Click"></asp:LinkButton><br /> <span class="NormalItalics"><asp:Label ID="lblSiteNavInstructions" runat="server"></asp:Label></span>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools" 
                AutoResizeHeight="True" Width="750px" OnClientCommandExecuting="changeImageManager">
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
        Available:
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="themes-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Themes</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="edit-theme.aspx?themeID=<%= theme_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Save" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


