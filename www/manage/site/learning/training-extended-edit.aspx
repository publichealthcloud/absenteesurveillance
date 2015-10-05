<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="training-extended-edit.aspx.cs" Inherits="edit_training_extended" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Training</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="trainings-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Trainings</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="training-edit.aspx?trainingID=<%= training_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE TRAINING" onclick="btnSave_OnClick" />
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
        <td width="150" valign="top">
        <strong>Training Title *</strong>
        </td>
        <td>
            <asp:Label ID="lblTrainingTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Description *</strong>
        </td>
        <td>
            <asp:Textbox ID="txtDescription" class="input-block-level" runat="server" TextMode="MultiLine" Height="80px" Width="500px"></asp:Textbox>      
            <asp:RequiredFieldValidator 
                ID="rfvTrainingDescription" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtDescription"
                ErrorMessage="Training description required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Author *</strong>
        </td>
        <td class="Normal">
            <asp:DropDownList ID="ddlAuthors" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvAuthors" 
                runat="server"
                Text=" *"
                ValidationGroup="form" 
                ControlToValidate="ddlAuthors"
                ErrorMessage="Author required">
            </asp:RequiredFieldValidator>  
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Available *</strong>
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
                Text=" *"
                ValidationGroup="form" 
                ControlToValidate="rblAvailable"
                ErrorMessage="Available to users required">
            </asp:RequiredFieldValidator>    
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Training Type *</strong>
        </td>
        <td class="Normal">
            <asp:DropDownList ID="ddlTrainingTypes" runat="server" >
            </asp:DropDownList>
            <asp:Label ID="lblTrainingType" CssClass="NormalItalics" runat="server">
                <br />
                <strong>Internal Trainings</strong> are created inside this site using the built-in slide editor<br />
                <strong>External Trainings</strong> are hosted on other websites; you will adding links to these trainings<br />
            </asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvTrainingType" 
                runat="server"
                Text=" *"
                ValidationGroup="form" 
                ControlToValidate="ddlTrainingTypes"
                ErrorMessage="Training type required">
            </asp:RequiredFieldValidator>  
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Extended Properties</strong>
        </td>
        <td><a href="training-extended-edit.aspx?trainingID=<%= training_id %>" class="btn btn-primary"><i class="icon-pencil"></i> Edit All Training Properties</a> 
            <asp:Label ID="lblExtendPropertiesText" CssClass="NormalItalics" runat="server"> <i>* Includes location, learning objects, assessment settings, etc.</i></asp:Label></td>
    </tr>
    <asp:PlaceHolder ID="plhInternalTraining" Visible="false" runat="server">
    <tr>
        <td width="150" valign="top">
        <strong>Slide Template *</strong>
        </td>
        <td class="Normal">
            <asp:DropDownList ID="ddlDesignTemplates" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvDesignTemplate" 
                runat="server"
                Text="  *"
                ValidationGroup="form" 
                ControlToValidate="ddlDesignTemplates"
                ErrorMessage="Slide template required">
            </asp:RequiredFieldValidator>  
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Design Tools</strong>
        </td>
        <td><a target="_blank" href="<%= final_manage_url %>&returnURL=/slide-modules/slide-editor.aspx?TrainingID=<%= training_id %>" class="btn btn-primary"><i class="icon-picture"></i> Design Training</a></td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhExternalTraining" Visible="false" runat="server">
        <tr>
            <td width="150" valign="top">
            <strong>Link to Training *</strong>
            </td>
            <td>
                <asp:Textbox ID="txtLink" class="input-block-level" Placeholder="Example Link -- http://www.somewebsite.com" runat="server" Width="500px"></asp:Textbox>
                <asp:RequiredFieldValidator 
                    ID="rfvTrainingLink" 
                    runat="server"
                    Text=" *"
                    ValidationGroup="form" 
                    ControlToValidate="txtLink"
                    ErrorMessage="External training link required">
                </asp:RequiredFieldValidator>         
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhInPersonTraining" Visible="false" runat="server">
        in person information
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhMetaData" runat="server" Visible="false">
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
    </asp:PlaceHolder>
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="trainings-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Trainings</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="training-edit.aspx?trainingID=<%= training_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE TRAINING" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


