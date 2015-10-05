<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="redirect-edit.aspx.cs" Inherits="edit_redirect" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Server Redirect</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="redirects-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Redirects</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="redirect-edit.aspx?redirectID=<%= redirect_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE REDIRECT" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this redirect? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Redirect"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Watch for URL *</strong>
        </td>
        <td>
            <strong><%= base_path %>/</strong><asp:Textbox ID="txtWatchFor" class="input-block-level" runat="server" Width="150px"></asp:Textbox><strong>/</strong>
            <asp:RequiredFieldValidator 
                ID="rfvWatchFor" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtWatchFor"
                ErrorMessage="Watch for string required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Redirect to URL *</strong>
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtURL" Width="500px" runat="server" class="input-block-level"></asp:Textbox> IMPORTANT: all external links need a http:// or https:// at the beginning
            <asp:RequiredFieldValidator 
                ID="rfvURL" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtURL"
                ErrorMessage="Redirect to URL required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Available *</strong>
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
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
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="redirects-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Redirects</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-redirect.aspx?rdirectID=<%= redirect_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE REDIRECT" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


