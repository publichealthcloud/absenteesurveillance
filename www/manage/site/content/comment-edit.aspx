<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="comment-edit.aspx.cs" Inherits="edit_comment" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Thoughts</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="comments-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Thoughts</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="comment-edit.aspx?commentID=<%= comment_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE THOUGHT" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this thought? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Thought"></asp:LinkButton>
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
        Posted By:*
        </td>
        <td>
            <asp:Label ID="lblPostedBy" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Thought:*
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtComment" TextMode="MultiLine" runat="server" class="input-block-level"></asp:Textbox>
            <asp:RequiredFieldValidator 
                ID="rfvComment" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtComment"
                ErrorMessage="Comment required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Associated Keywords:
        </td>
        <td>
            <asp:CheckBoxList ID="cblKeywords" CssClass="Normal" RepeatColumns="4" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available to Users:
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        Available for Campaigns?
        </td>
        <td>
            <asp:Button CssClass="btn" Text="Make Available for Campaigns" runat="server" ID="btnMakeAvailableCampaigns" OnClick="btnMakeAvailableCampaigns_Click" /><asp:Label ID="lblExistsFeed" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="comments-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Comments</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="comment-tip.aspx?commentID=<%= comment_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE THOUGHT" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


