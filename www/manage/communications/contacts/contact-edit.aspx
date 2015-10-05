<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="contact-edit.aspx.cs" Inherits="contact_tip" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Tips</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="contacts-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Contacts</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="contact-edit.aspx?tipID=<%= contact_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE CONTACT" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this contact? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Contact"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><strong><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label></strong>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
    <tr>
        <td width="150" valign="top">
        First Name:
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Last Name:
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Email: *</strong>
        </td>
        <td>
            <asp:Textbox ID="txtEmail" class="input-block-level" runat="server" Width="500px"></asp:Textbox>      
            <asp:RequiredFieldValidator 
                ID="rfvEmail" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtEmail"
                ErrorMessage="Email required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Keywords:
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtKeywords" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Source:
        </td>
        <td class="Normal">
            <asp:TextBox ID="txtSource" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        User:
        </td>
        <td class="Normal">
            <asp:Literal ID="litUserInfo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Ok to Email: *</strong>
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblOkEmail" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator 
                ID="rfvOkEmail" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rblOkEmail"
                ErrorMessage="Ok to email required">
            </asp:RequiredFieldValidator>  
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Pre-Assigned Text Messaging Number:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtDID" Width="500px" runat="server" class="input-block-level"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Partner:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtPartner" Width="500px" runat="server" class="input-block-level"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Main Group:
        </td>
        <td>
            <telerik:RadNumericTextBox ID="txtMainGroup" NumberFormat-DecimalDigits="0" runat="server"></telerik:RadNumericTextBox> <i>* Must be a number</i>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Sub Group:
        </td>
        <td>
            <telerik:RadNumericTextBox ID="txtSubGroup" NumberFormat-DecimalDigits="0" runat="server"></telerik:RadNumericTextBox> <i>* Must be a number</i>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Custom HTML Element:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtCustomHTMLElement" Width="500px" runat="server" class="input-block-level"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Unsubscribed:
        </td>
        <td class="Normal">
            <telerik:RadDateTimePicker ID="rdtUnsubscribed" runat="server"></telerik:RadDateTimePicker>
        </td>
    </tr>
    <tr>
        <td colspan="2" valign="top">
        UNSUBSCRIBED CAMPAIGN INFO: <asp:Literal ID="litUnsubscribedCampaignInfo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td>

        </td>
        <td>
            <asp:DropDownList ID="ddlCampaigns" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" valign="top">
        REPORTED AS SPAM INFO: <telerik:RadDateTimePicker ID="rdtReportedAsSpam" runat="server"></telerik:RadDateTimePicker>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Campaign:
        </td>
        <td class="Normal">
            <asp:Literal ID="litReportedAsSPAMCampaignInfo" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:DropDownList ID="ddlCampaignsSPAM" runat="server"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Available: *</strong>
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
                ErrorMessage="Available to users required">
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
                    <a href="contacts-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Contacts</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="contact-edit.aspx?contactID=<%= contact_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE CONTACT" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


