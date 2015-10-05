<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="keyword-edit.aspx.cs" Inherits="edit_keyword" %>
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
                        <a href="keywords-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Keywords</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="keyword-edit.aspx?keywordID=<%= keyword_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE KEYWORD" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this keyword? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Keyword"></asp:LinkButton>
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
        Keyword *
        </td>
        <td>
            <telerik:RadTextBox ID="txtKeyword" runat="server" Width="500px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtKeyword"
                ErrorMessage="Keyword required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Associated Keywords
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtAssociatedKeywords" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
            <br />
            <span class="NormalItalics">NOTE: separate multiple keywords with commas (i.e. social,community)</span>
         </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Definition
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtDefinition" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
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
        <strong>Available *</strong>
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
</table>

<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="keywords-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Keywords</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="edit-keyword.aspx?keywordID=<%= keyword_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE KEYWORD" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


