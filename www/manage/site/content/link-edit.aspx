<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="link-edit.aspx.cs" Inherits="edit_link" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Links</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="links-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Links</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="link-edit.aspx?linkID=<%= link_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE LINK" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this link? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Link"></asp:LinkButton>
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
        Title *
        </td>
        <td>
            <telerik:RadTextBox ID="txtName" runat="server" Width="500px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtName"
                ErrorMessage="Title required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description *
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtSummary" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvSummary" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtSummary"
                ErrorMessage="Description required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="100" class="NormalBold" valign="top">
        Link Type *
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlType" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="loadUploadOptions">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Internal Page" Text="Internal Page"></asp:ListItem>
                <asp:ListItem Value="External Page" Text="External Page"></asp:ListItem>
                <asp:ListItem Value="Internal Document" Text="Internal Document"></asp:ListItem>
                <asp:ListItem Value="External Document" Text="External Document"></asp:ListItem>
                <asp:ListItem Value="Organization" Text="Organization Website"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlType"
                ErrorMessage="Link type is required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <asp:PlaceHolder ID="plhURL" runat="server">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        URL *
        </td>
        <td>
            <telerik:RadTextBox ID="txtURL" runat="server" EmptyMessage="for example - http://www.google.com" Width="500px"></telerik:RadTextBox>
            &nbsp;<asp:LinkButton CssClass="Normal" ID="btnEnableDocumentTools" Text="Change Document" Visible="false" runat="server" OnClick="btnEnableDocumentTools_Click"></asp:LinkButton>
            <asp:PlaceHolder ID="plhDocumentTools" runat="server">
                <br />
                <asp:Label ID="lblDocumentInstructions" CssClass="NormalItalics" runat="server" Text="<strong>Select Document *</strong>"></asp:Label>&nbsp;
                <asp:DropDownList ID="ddlDocument" runat="server" AutoPostBack="true" OnSelectedIndexChanged="setDocumentURL">
                </asp:DropDownList>
                <asp:LinkButton CssClass="Normal" ID="btnRefreshDocuments" runat="server" OnClick="btnRefreshDocument_Click"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh Document List</asp:LinkButton>
                &nbsp;&nbsp;<asp:LinkButton CssClass="Normal" ID="btnUploadDocument" Text="Upload / Manage Documents" runat="server" OnClick="btnUploadDocument_Click"></asp:LinkButton>
                <asp:LinkButton CssClass="Normal" ID="btnHideDocuments" Text="Hide Documents" runat="server" OnClick="btnHideDocument_Click"></asp:LinkButton>
                <br />
                <telerik:RadFileExplorer runat="server" ID="fxpLinkDocuments" Visible="false" Width="450px" Height="300px" EnableCreateNewFolder="True">
                    <Configuration ViewPaths="~/resources/links/" MaxUploadFileSize="256000000" UploadPaths="~/resources/links/" DeletePaths="~/resources/links" />
                </telerik:RadFileExplorer>
            </asp:PlaceHolder>
            <asp:Label ID="lblURLInstructions" Visible="false" runat="server" CssClass="NormalItalics"><br />* Make sure to include http or https -- for example: http://www.google.com</asp:Label>     
            <asp:RequiredFieldValidator 
                ID="rfvURL" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtURL"
                ErrorMessage="URL required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Author:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlAuthor"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Audience / Link Type:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlLinkType">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="Helpful Links">Helpful Links</asp:ListItem>
                <asp:ListItem Value="General Info">General Info</asp:ListItem>
                <asp:ListItem Value="Parent Info">Parent Info</asp:ListItem>
                <asp:ListItem Value="General Info">Programs</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Language:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlLanguage">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="en">English</asp:ListItem>
                <asp:ListItem Value="es">Spanish</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Theme
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlTheme"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Associated Keywords
        </td>
        <td>
            <asp:CheckBoxList ID="cblKeywords" CssClass="Normal" RepeatColumns="4" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvAvailable" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rblAvailable"
                ErrorMessage="Available required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Display in Locations(s)
        </td>
        <td class="NormalBold">
            <asp:CheckBox ID="chkDisplayInFeed" CssClass="Normal" runat="server" /> <asp:Label ID="lblDisplayInFeedMessage" runat="server">Main Site Feed</asp:Label>
            <asp:CheckBox ID="chkDisplayInExplore" CssClass="Normal" runat="server" /> <asp:Label ID="lblDisplayInExploreMessage" runat="server">Explore Section</asp:Label>
            <br /><br />
            Display in Topic Feeds:
            <br />
            <asp:CheckBoxList ID="chkTopics" CssClass="Normal" RepeatColumns="4" runat="server"></asp:CheckBoxList>
            <asp:PlaceHolder ID="plhExistingFeedItem" runat="server">
            <br />
            Move to Top of Feed:
            <br />
            <asp:CheckBox ID="chkMoveToTop" CssClass="Normal" RepeatColumns="4" runat="server"></asp:CheckBox> Move to Top of Feed
            </asp:PlaceHolder>
        </td>
    </tr>
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="links-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Links</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="link-edit.aspx?linkID=<%= link_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE LINK" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


