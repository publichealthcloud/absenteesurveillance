<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="video-edit.aspx.cs" Inherits="video_edit" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Videos</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="videos-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Videos</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="video-edit.aspx?videoID=<%= video_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE VIDEO" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this video? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Video"></asp:LinkButton>
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
        Video *
        </td>
        <td>
            <asp:PlaceHolder ID="plhVideo" runat="server">
                <asp:Literal ID="litViddlerEmbed" runat="server"></asp:Literal>
            <br />
            <asp:TextBox ID="txtEmbedTag" TextMode="MultiLine" Height="70px" width="460px" runat="server"> 
            </asp:TextBox> Copyable Embed Tags
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhExternalVideo" runat="server">
                <asp:Literal ID="litExternalEmbed" runat="server"></asp:Literal>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhVideoProcessing" runat="server">
                <span class="NormalRed"><asp:Literal ID="litVideo" runat="server"></asp:Literal></span>
            </asp:PlaceHolder>
        </td>
    </tr>
    <asp:PlaceHolder ID="plhAdditionalExternalInfo" runat="server">
    <tr>
        <td width="100" class="NormalBold" valign="top">
        External Source *
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlExternalSource" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="YouTube" Text="YouTube"></asp:ListItem>
                <asp:ListItem Value="Vimeo" Text="Vimeo"></asp:ListItem>
                <asp:ListItem Value="Viddler" Text="Viddler"></asp:ListItem>
                <asp:ListItem Value="DailyMotion" Text="Daily Motion"></asp:ListItem>
                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvExternalSource" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlExternalSource"
                ErrorMessage="External source is required">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="100" class="NormalBold" valign="top">
        Source Video ID
        </td>
        <td valign="top">
            <telerik:RadTextBox ID="txtSourceVideoID" runat="server" Width="460px" EmptyMessage="Enter Video ID" Font-Names="Arial"></telerik:RadTextBox>
            <span class="NormalItalics"><br />* Source Video ID is often part of the main address for a video; if a YouTube URL is http://www.youtube.com/watch?v=Lm1DPre8tSQ&feature=related then the Source Video ID is: <strong>Lm1DPre8tSQ</strong></span>    
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Video Type *
        </td>
        <td class="Normal">
            <asp:Label ID="lblVideoType" runat="server"></asp:Label><br />
            <asp:TextBox ID="txtEmbed" runat="server" TextMode="MultiLine" Width="460px" Height="50px" Enabled="false"></asp:TextBox>&nbsp;<asp:LinkButton ID="btnEnableEmbed" CssClass="Normal" runat="server" OnClick="btnEnableEmbed_Click" Text="Edit Embed"></asp:LinkButton>
            <asp:RequiredFieldValidator 
                ID="rfvEmbedCode" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtEmbed"
                ErrorMessage="Video embed required">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Video Thumbnail
        </td>
        <td>
            <asp:TextBox ID="txtPreviewImage" width="460px" runat="server"></asp:TextBox>&nbsp;<asp:LinkButton CssClass="Normal" ID="btnEnableThumbnail" Text="Edit Thumbnail" runat="server" OnClick="btnEnableThumbnail_Click"></asp:LinkButton><br /> <span class="NormalItalics">* Should be at least 150px wide</span>           
            <br />
            <asp:Image ID="imgThumbnail" runat="server" />    
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Title *
        </td>
        <td>
            <telerik:RadTextBox ID="txtTitle" runat="server" Width="460px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvTitle" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtTitle"
                ErrorMessage="Title required">
            </asp:RequiredFieldValidator>     
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="460px" Height="80px"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Posted By:
        </td>
        <td class="Normal">
            <a href="#" onclick="openUserWindow('2', '<%= owner_id %>'); return false;">
            <img src="../images/magnifying_glass.gif" width="12" height="13" border="0" />&nbsp;<%= owner %></a>
            <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Author
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlAuthor"></asp:DropDownList>
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
        Approval Status *
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlStatus">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem>
                <asp:ListItem Value="Declined" Text="Declined"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvStatus" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlStatus"
                ErrorMessage="Status required">
            </asp:RequiredFieldValidator>   
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Highlighted Video
        </td>
        <td class="NormalBold">
            <asp:CheckBox ID="chkHighlightedVideo" CssClass="Normal" runat="server" /> <asp:Label ID="lblHighlightedMessage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available to Users *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="Label1" CssClass="NormalItalics" runat="server"></asp:Label>
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
                    <a href="videos-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Videos</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-video.aspx?videoID=<%= video_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE VIDEO" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


