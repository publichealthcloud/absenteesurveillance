<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="video-upload.aspx.cs" Inherits="upload_video" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">

    <div class="body_normal" style="margin:20px; padding:10px;">
        <h3>Step 1: Enter Title and Select Video Type</h3>
        <table cellpadding="5">
            <tr>
                <td width="100" class="NormalBold" valign="top">
                Title:
                </td>
                <td valign="top">
                    <telerik:RadTextBox ID="txtTitle" runat="server" Width="375px" EmptyMessage="Enter Title" Font-Names="Arial"></telerik:RadTextBox>
                    <asp:RequiredFieldValidator 
                        ID="rfvTitle" 
                        runat="server"
                        Text="*"
                        ValidationGroup="form" 
                        ControlToValidate="txtTitle"
                        ErrorMessage="Title is required">
                    </asp:RequiredFieldValidator>  
                </td>
            </tr>
            <tr>
                <td width="100" class="NormalBold" valign="top">
                Type of Video:
                </td>
                <td valign="top">
                    <asp:DropDownList ID="ddlVideoType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="loadVideoMethod">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="internal" Text="Internal - Upload a Video for Processing"></asp:ListItem>
                        <asp:ListItem Value="external" Text="External - Link to an outside video (i.e. YouTube)"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator 
                        ID="rfvType" 
                        runat="server"
                        Text="*"
                        ValidationGroup="form" 
                        ControlToValidate="ddlVideoType"
                        ErrorMessage="Video type is required">
                    </asp:RequiredFieldValidator> 
                </td>
            </tr>
            </table>
            <asp:PlaceHolder ID="plhInternal" runat="server" Visible="false">
                <h3>Step 2: Upload a Video</h3>
               <table cellpadding="5">
                    <tr>
                        <td width="100" class="NormalBold" valign="top">
                            Select Video:
                        </td>
                        <td class="NormalItalics">
                            <div style="padding-left:35px;">
                            <div style="background:#eee; border:solid 1px #444; padding:10px; width:320px;">
                            <telerik:RadProgressManager ID="progress_manager" runat="server" />    
                            <telerik:RadUpload ID="rad_upload" runat="server" MaxFileInputsCount="1" AllowedFileExtensions=".wmv,.avi,.mov,.m4v,.mpeg,.flv,.mp4"
                                OverwriteExistingFiles="true" Width="100%" InitialFileInputsCount="1" />
                            </div>
                            Allowed file types incldue: .wmv, .avi, .mov, .m4v, .mpeg, .flv and .mp4
                        </td>
                    </tr>
                </table>
                <div class="body_normal" style="margin:20px; padding:10px;">        
                    <div style="width:50%; display:block;">
                        <telerik:RadProgressArea ID="progress_area" runat="server" DisplayCancelButton="true" Width="100%" />
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhExternal" Visible="false" runat="server">
                <h3 style="vertical-align:middle; font-weight:bold; font-family:Arial">Step 2: Enter the Info for an External Video</h3>
               <table cellpadding="5">
                    <tr>
                        <td width="100" class="NormalBold" valign="top">
                            Enter Embed Code:
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtEmbedCode" runat="server" Width="375px" Height="200px" TextMode="MultiLine"></telerik:RadTextBox>
                                <asp:RequiredFieldValidator 
                                    ID="rfvEmbed" 
                                    runat="server"
                                    Text="*"
                                    ValidationGroup="form" 
                                    ControlToValidate="txtEmbedCode"
                                    ErrorMessage="Embed code is required">
                                </asp:RequiredFieldValidator>
                                <span class="NormalItalics"><br />* The best width is 437 pixels. Avoid widths of more than 437 pixels.</strong></span> 
                        </td>
                    </tr>
                    <tr>
                        <td width="100" class="NormalBold" valign="top">
                        External Source:
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
                        Source Video ID:
                        </td>
                        <td valign="top">
                            <telerik:RadTextBox ID="txtSourceVideoID" runat="server" Width="375px" EmptyMessage="Enter Video ID" Font-Names="Arial"></telerik:RadTextBox>
                            <asp:RequiredFieldValidator 
                                ID="rfvSourceVideoID" 
                                runat="server"
                                Text="*"
                                ValidationGroup="form" 
                                ControlToValidate="txtSourceVideoID"
                                ErrorMessage="Source video ID is required">
                            </asp:RequiredFieldValidator>
                            <span class="NormalItalics"><br />* Source Video ID is often part of the main address for a video; if a YouTube URL is http://www.youtube.com/watch?v=Lm1DPre8tSQ&feature=related then the Source Video ID is: <strong>Lm1DPre8tSQ</strong></span>    
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
            <br />
            <asp:PlaceHolder ID="plhSubmit" runat="server" Visible="false">
                <h3>Step 3: Submit</h3>
                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Save Video" OnClick="btn_submit_OnClick" CausesValidation="false" OnClientClick="return Page_IsValid=true;" />
            </asp:PlaceHolder>
        </div>   

</asp:Content>


