<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="tip-edit.aspx.cs" Inherits="edit_tip" %>
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
                        <a href="tips-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Tips</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="tip-edit.aspx?tipID=<%= tip_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE TIP" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this tip? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Tip"></asp:LinkButton>
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
        <strong>Tip Type: *</strong>
        </td>
        <td class="Normal">
            <asp:DropDownList ID="ddlTipType" runat="server">
                <asp:ListItem Value="Fact">Fact</asp:ListItem>
                <asp:ListItem Value="Rule">Rule</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label1" CssClass="NormalItalics" runat="server">If unsure, just set as Fact.</asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlTipType"
                ErrorMessage="Tip type required">
            </asp:RequiredFieldValidator>    
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Reference Name: *</strong>
        </td>
        <td>
            <asp:Textbox ID="txtTitle" class="input-block-level" runat="server" Width="500px"></asp:Textbox>      
            <asp:RequiredFieldValidator 
                ID="rfvTitle" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtTitle"
                ErrorMessage="Tip name required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Tip Preview: *</strong>
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtSummary" TextMode="MultiLine" MaxLength="250" runat="server" class="input-block-level" Width="500px"></asp:Textbox> Preview max lwww.ength 250 characters
            <asp:RequiredFieldValidator 
                ID="rfvTip" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtSummary"
                ErrorMessage="Tip required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        <strong>Tip Full Text: *</strong>
        </td>
        <td>
            <asp:Textbox ID="txtTip" Height="100px" TextMode="MultiLine" class="input-block-level" runat="server" Width="500px"></asp:Textbox>      
            <asp:RequiredFieldValidator 
                ID="rfvFullTip" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtTip"
                ErrorMessage="Tip name required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Optional Author Name:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtAuthor" Width="500px" runat="server" class="input-block-level"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Learn More URL:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtURL" Width="500px" runat="server" class="input-block-level"></asp:Textbox> NOTE: all external links need a http:// or https:// at the beginning
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Theme:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlTheme"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Associated Keywords:
        </td>
        <td>
            <asp:CheckBoxList ID="cblKeywords" CssClass="Normal" RepeatColumns="4" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Available to Users:
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
                    <a href="tips-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Tips</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-tip.aspx?tipID=<%= tip_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE TIP" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


