<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master" CodeFile="sms-message-edit.aspx.cs" Inherits="manage_communications_sms_message_edit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Edit Text Message (SMS Message)</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackToMessages" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Messages</asp:HyperLink>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="sms-message-edit.aspx?smsMessageID=<%= sms_message_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE MESSAGE" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this message? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Text Message"></asp:LinkButton>
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
                <asp:DropDownList ID="ddlCampaigns" Width="400px" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                Day In Campaign
            </td>
            <td>
                <telerik:RadNumericTextBox Width="40px" ID="txtDayInCampaign" runat="server" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="1000"></telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td width="100" class="NormalBold">
                <strong>Message Text *</strong>
            </td>
            <td>
                <asp:TextBox ID="txtMessage" width="1100px" MaxLength="160" runat="server"></asp:TextBox>
                * 160 Max Characters
                <asp:RequiredFieldValidator 
                    ID="rfvMessage" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtMessage"
                    ErrorMessage="Message required">
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
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Messages</asp:HyperLink>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="sms-message-edit.aspx?smsMessageID=<%= sms_message_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE MESSAGE" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
