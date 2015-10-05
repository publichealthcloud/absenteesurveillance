<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="email-edit.aspx.cs" Inherits="manage_communications_email_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Email</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="emails-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Emails</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="email-edit.aspx?emailID=<%= email_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE EMAIL" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this email? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Email"></asp:LinkButton>
                &nbsp;&nbsp;<i class="icon-paste"></i>&nbsp; <asp:HyperLink runat="server" ID="hplDuplicate" Text="Duplicate Email"></asp:HyperLink>
                &nbsp;&nbsp;<i class="icon-envelope-alt"></i>&nbsp; <asp:HyperLink ID="hplTestSend" runat="server" Text="Send Test Email"></asp:HyperLink>
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
            <td width="100" class="NormalBold">
                <strong>URI *</strong>
            </td>
            <td>
                <asp:TextBox ID="txtURI" width="750px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvURI" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtURI"
                    ErrorMessage="URI required">
                </asp:RequiredFieldValidator> 
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                <strong>Email Type *</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlEmailType" Width="450px" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="bulk" Text="Bulk Email - Used for large-scale email campaigns, not essential"></asp:ListItem>
                    <asp:ListItem Value="custom" Text="Custom Email - Created for any new reason"></asp:ListItem>
                    <asp:ListItem Value="system" Text="System Email - Necessary for system to work correctly, essential"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ID="rfvEmailType" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="ddlEmailType"
                    ErrorMessage="Email type required">
                </asp:RequiredFieldValidator> 
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                <strong>Language *</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlLanguages" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ID="rfvLanguage" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="ddlLanguages"
                    ErrorMessage="Language required">
                </asp:RequiredFieldValidator> 
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                Campaign
            </td>
            <td>
                <asp:DropDownList ID="ddlCampaigns" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                <strong>Subject *</strong>
            </td>
            <td>
                <asp:TextBox ID="txtSubject" width="750px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvSubject" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtSubject"
                    ErrorMessage="Subject required">
                </asp:RequiredFieldValidator> 
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold" valign="top">
                <strong>Body *</strong>
            </td>
            <td>
                <span>Load prior saved versions:</span>
                <telerik:RadComboBox ID="cmbRevisions" runat="server" Width="450px" Height="150px" AllowCustomText="true"
                    EmptyMessage="Select a Version" EnableAutomaticLoadOnDemand="True" ItemsPerRequest="25"
                    ShowMoreResultsBox="true" EnableVirtualScrolling="true">
                </telerik:RadComboBox>
                    <asp:Button runat="server" Text="Load" ID="btnLoadRevision" OnClick="btnLoadRevision_Click">
                </asp:Button>
                <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools" 
                    AutoResizeHeight="True" Width="750px" OnClientCommandExecuting="changeImageManager">
                    <ImageManager ViewPaths="~/resources/emails" UploadPaths="~/resources/emails" DeletePaths="~/resources/emails" />
                    <DocumentManager ViewPaths="~/resources/emails" 
                        UploadPaths="~/resources/emails" DeletePaths="~/resources/emails"></DocumentManager>
                    <Content>
                    </Content>
                        <ImageManager ViewPaths="~/resources/emails" 
                        UploadPaths="~/resources/emails" DeletePaths="~/resources/emails"></ImageManager>
                        <DocumentManager ViewPaths="~/resources/emails" 
                        UploadPaths="~/resources/emails" DeletePaths="~/resources/emails"></DocumentManager>
                </telerik:radeditor>
            </td>
        </tr>
    </table>

    <div class="box">
        <div class="box-title">
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <a href="emails-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Emails</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="edit-email.aspx?emailID=<%= email_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE EMAIL" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
