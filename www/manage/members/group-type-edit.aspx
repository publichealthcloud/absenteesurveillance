<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="group-type-edit.aspx.cs" Inherits="edit_group_type" %>
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
                        <a href="group-types-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Group Types</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="group-type-edit.aspx?spaceCategoryID=<%= space_category_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE GROUP TYPE" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this group type? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Group Type"></asp:LinkButton>
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
        Group Type *
        </td>
        <td>
            <telerik:RadTextBox ID="txtSpaceCategory" runat="server" Width="500px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtSpaceCategory"
                ErrorMessage="Group type required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
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
</table>

<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="group-types-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Group Types</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="edit-group-type.aspx?spaceCategoryID=<%= space_category_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE GROUP TYPE" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


