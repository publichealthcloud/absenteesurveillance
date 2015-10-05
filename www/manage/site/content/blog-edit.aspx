<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="blog-edit.aspx.cs" Inherits="edit_blog" %>
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

    function handle_combo_key_press(sender, args) {
        if (args.get_domEvent().keyCode == 13) {
            search_redirect();
        }
    }

</script> 

<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Blogs</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="blogs-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Blogs</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="blog-edit.aspx?blogID=<%= blog_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE BLOG" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this blog? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Blog"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <i class="icon-external-link"></i>&nbsp; <asp:HyperLink ID="hplPreviewArticle" runat="server">Preview Blog</asp:HyperLink>
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
        Type *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="blog" Text="Blog"></asp:ListItem>
                <asp:ListItem Value="story" Text="Story"></asp:ListItem>
            </asp:RadioButtonList>&nbsp;&nbsp;<asp:LinkButton CssClass="Normal" ID="btnEnableType" Text="Edit Type" runat="server" OnClick="btnEnableType_Click"></asp:LinkButton>
            
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rblType"
                ErrorMessage="Type required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Title *
        </td>
        <td>
            <telerik:RadTextBox ID="txtTitle" runat="server" Width="500px"></telerik:RadTextBox>      
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Summary *
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtSummary" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Body *
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools" 
                Height="700px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/blogs" UploadPaths="~/resources/blogs" DeletePaths="~/resources/blogs" />
                <DocumentManager ViewPaths="~/resources/blogs" 
                    UploadPaths="~/resources/blogs" DeletePaths="~/resources/blogs"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/blogs" 
                    UploadPaths="~/resources/blogs" DeletePaths="~/resources/blogs"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/blogs" 
                    UploadPaths="~/resources/blogs" DeletePaths="~/resources/blogs"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <asp:PlaceHolder ID="plhPostedBy" runat="server">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Posted By *
        </td>
        <td class="Normal">
            <a href="#" onclick="openUserWindow('2', '<%= owner_id %>'); return false;">
            <img src="../images/magnifying_glass.gif" width="12" height="13" border="0" />&nbsp;<%= owner %></a>
            <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
        </td>
    </tr>
    </asp:PlaceHolder>
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
        Available to Users *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
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
        Highlighted Item
        </td>
        <td class="NormalBold">
            <asp:CheckBox ID="chkHighlightedItem" CssClass="Normal" runat="server" /> <asp:Label ID="lblHighlightedMessage" runat="server"></asp:Label>
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
                    <a href="blogs-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Blogs</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="blog-edit.aspx?blogID=<%= blog_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE BLOG" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


