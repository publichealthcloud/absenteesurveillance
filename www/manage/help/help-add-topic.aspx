<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/simple.master" CodeFile="help-add-topic.aspx.cs" Inherits="qHlp_help_add_topic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content_main">

<link href="../quartz.css" rel="stylesheet" type="text/css" />

<div style="position: relative; left: 10px;">
    <br />
    <span><strong>&nbsp;<asp:Label ID="lblTitle" CssClass="CapsHeader3" runat="server" Text="Add New Help Topic"></asp:Label></strong></span>
    <table cellpadding="5">
        <tr>
            <td width="100" colspan="2" class="validation2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Title *
        </td>
        <td>
            <telerik:RadTextBox ID="txtTitle" ValidationGroup="form" runat="server" MaxLength="100" Width="500px"></telerik:RadTextBox> 
                <asp:RequiredFieldValidator
                    CssClass="validation2" 
                    ID="rfvTitle" 
                    runat="server"
                    Text="Title required"
                    ValidationGroup="form" 
                    ControlToValidate="txtTitle"
                    ErrorMessage="Title required">
                </asp:RequiredFieldValidator>     
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Category *
        </td>
        <td>
            <asp:DropDownList ID="ddlHelpCategories" runat="server">
            </asp:DropDownList>
                <asp:RequiredFieldValidator
                    CssClass="validation2" 
                    ID="rfvHelpCategories" 
                    runat="server"
                    Text="Help category required"
                    ValidationGroup="form" 
                    ControlToValidate="ddlHelpCategories"
                    ErrorMessage="Help category required">
                </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="150" class="Normal" valign="top">
        Topic Type 
        </td>
        <td class="Normal">
            <asp:CheckBox ID="chkIsSystem" runat="server" Text="This is a system help topic" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="form" Text="Save" onclick="btnSave_OnClick" />
        </td>
    </tr>
    </table>
</div>
</asp:Content>


