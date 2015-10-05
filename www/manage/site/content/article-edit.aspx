<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="article-edit.aspx.cs" Inherits="edit_article" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<script type="text/javascript">
    function changeImageManager(editor, args) {
        if (args.get_commandName() == "DocumentManager") {

            var callbackFunction = function (sender, args) {
                var result = args.get_value();
                result.src = "<%= imageURL %>" + result.src.substring(result.src.lastIndexOf("/") + 1, result.src.length);
                result = Telerik.Web.UI.Editor.Utils.getOuterHtml(result);

                editor.pasteHtml("This is a test", "DocumentManager");
            };

            args.set_callbackFunction(callbackFunction); //register callback function
        }
        if (args.get_commandName() == "ImageManager") {

            var callbackFunction = function (sender, args) {
                var result = args.get_value(); //get returned value of ImageManager which is IMG element
                result.src = "<%= imageURL %>" + result.src.substring(result.src.lastIndexOf("/") + 1, result.src.length);
                result = Telerik.Web.UI.Editor.Utils.getOuterHtml(result); //get HTML source of the DOM element

                editor.pasteHtml(result, "ImageManager");
            };

            args.set_callbackFunction(callbackFunction); //register callback function
        }
    }

</script> 

<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Articles</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="articles-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Articles</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="article-edit.aspx?articleID=<%= article_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE ARTICLE" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this article? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Article"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <i class="icon-external-link"></i>&nbsp; <asp:HyperLink ID="hplPreviewArticle" runat="server">Preview Article</asp:HyperLink>
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
        Title:
        </td>
        <td>
            <asp:Textbox ID="txtTitle" class="input-block-level" runat="server" Width="500px"></asp:Textbox>      
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Summary:
        </td>
        <td class="Normal">
            <asp:Textbox ID="txtSummary" TextMode="MultiLine" runat="server" class="input-block-level"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Body:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools"
                Height="700px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/articles" UploadPaths="~/resources/articles" MaxUploadFileSize="110000000"  DeletePaths="~/resources/articles" />
                <DocumentManager ViewPaths="~/resources/articles" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/articles" DeletePaths="~/resources/articles"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/articles" 
                    UploadPaths="~/resources/articles" DeletePaths="~/resources/articles"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/articles" 
                    UploadPaths="~/resources/articles" DeletePaths="~/resources/articles"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <asp:PlaceHolder ID="plhPostedBy" runat="server">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Posted By:
        </td>
        <td class="Normal">
            <a href="/manage/members/member-profile.aspx?userID=<%= owner_id %>" target="_blank">
            <i class="icon-external-link"></i>&nbsp;<%= owner %></a>
            <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Author:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlAuthor"></asp:DropDownList>
        </td>
    </tr>
    <asp:PlaceHolder ID="plhArticleType" runat="server" Visible="false">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Health Care Article Type:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlArticleType">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="Helpful Links">Helpful Links</asp:ListItem>
                <asp:ListItem Value="General Info">General Info</asp:ListItem>
                <asp:ListItem Value="Parent Info">Parent Info</asp:ListItem>
                <asp:ListItem Value="General Info">Programs</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Language:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlLanguage">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem Value="en">English</asp:ListItem>
                <asp:ListItem Value="es">Spanish</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Theme:
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlTheme"></asp:DropDownList>
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
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
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
                    <a href="articles-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Articles</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-article.aspx?articleID=<%= article_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE ARTICLE" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


