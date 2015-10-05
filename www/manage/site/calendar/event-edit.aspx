<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="event-edit.aspx.cs" Inherits="edit_event" %>
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

<div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Events</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="events-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Events</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="event-edit.aspx?eventID=<%= event_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="SAVE EVENT" onclick="btnSave_OnClick" />
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
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this event? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Event"></asp:LinkButton>
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
        Title *
        </td>
        <td>
            <telerik:RadTextBox ID="txtName" runat="server" Width="500px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtName"
                ErrorMessage="Title required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Summary *
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtSummary" MaxLength="4000" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvSummary" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtSummary"
                ErrorMessage="Summary required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Description:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reContent" SkinID="DefaultSetOfTools"
                Height="350px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/site" UploadPaths="~/resources/site" MaxUploadFileSize="110000000"  DeletePaths="~/resources/site" />
                <DocumentManager ViewPaths="~/resources/site" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/site" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/site" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td width="100" class="NormalBold" valign="top">
        Event Type *
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Event" Text="Event"></asp:ListItem>
                <asp:ListItem Value="Training" Text="Training"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlType"
                ErrorMessage="Event type is required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <asp:PlaceHolder ID="plhTrainingList" Visible="false" runat="server">
    <tr>
        <td width="100" class="NormalBold" valign="top">
            Select Training *
        </td>
        <td valign="top">
            <asp:DropDownList runat="server" ID="ddlTrainingList" Width="450px"></asp:DropDownList>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Location
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtLocation" MaxLength="50" runat="server" Width="300px" ></telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Location Details:
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reLocationDetails" SkinID="DefaultSetOfTools"
                Height="350px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/site" UploadPaths="~/resources/site" MaxUploadFileSize="110000000"  DeletePaths="~/resources/site" />
                <DocumentManager ViewPaths="~/resources/site" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/site" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/site" 
                    UploadPaths="~/resources/site" DeletePaths="~/resources/site"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td width="200" class="NormalBold" valign="top">
        Event Starts *
        </td>
        <td>
            <telerik:RadDateTimePicker ID="rdtStartTime" runat="server" Width="200"></telerik:RadDateTimePicker>
            <asp:RequiredFieldValidator 
                ID="rfvStartTime" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rdtStartTime"
                ErrorMessage="Valid start date required">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="200" class="NormalBold" valign="top">
        Event Ends *
        </td>
        <td>
            <telerik:RadDateTimePicker ID="rdtEndTime" runat="server" Width="200"></telerik:RadDateTimePicker>
            <asp:RequiredFieldValidator 
                ID="rfvEndTime" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rdtEndTime"
                ErrorMessage="Valid end date required">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="200" class="NormalBold" valign="top">
        More Info Link
        </td>
        <td>
            <telerik:RadTextBox ID="txtURL" runat="server" EmptyMessage="for example - http://www.google.com" Width="500px"></telerik:RadTextBox>
            <asp:Label ID="lblURLInstructions" Visible="false" runat="server" CssClass="NormalItalics"><br />* Make sure to include http or https -- for example: http://www.google.com</asp:Label>  
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
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="events-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Events</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-event.aspx?eventID=<%= event_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSaveBottom" CssClass="btn btn-primary" runat="server" Text="SAVE EVENT" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


