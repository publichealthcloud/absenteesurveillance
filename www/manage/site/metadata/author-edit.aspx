<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="author-edit.aspx.cs" Inherits="edit_author" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Articles</asp:Label>
		</h3>
            <ul class="tabs">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="authors-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Authors</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="author-edit.aspx?authorID=<%= author_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE AUTHOR" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this author? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Author"></asp:LinkButton>
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
        Name:
        </td>
        <td>
            <telerik:RadTextBox ID="txtName" ValidationGroup="edit" runat="server" Width="500px"></telerik:RadTextBox>      
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available:
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" ValidationGroup="edit" RepeatDirection="Horizontal">
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
                    <a href="authors-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Authors</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="edit-author.aspx?authorID=<%= author_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSaveBottom" ValidationGroup="edit" CssClass="btn btn-primary" runat="server" Text="SAVE AUTHOR" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


