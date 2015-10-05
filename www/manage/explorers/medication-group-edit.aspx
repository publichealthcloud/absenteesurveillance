<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="medication-group-edit.aspx.cs" Inherits="medication_group_edit" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Group Medications</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="articles-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Medication Groups</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="medication-group-edit.aspx?mediationGroupID=<%= medication_group_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE MEDICATION GROUP" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    <div style="height:10px;"></div>
    <table border="0" cellpadding="5">
        <tr>
            <td colspan="2">
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
        <strong>Title *</strong>
        </td>
        <td>
            <asp:Textbox ID="txtMedicationGroupName" class="input-block-level" runat="server" Width="500px"></asp:Textbox>
            <asp:RequiredFieldValidator 
                ID="rfvGroupName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtMedicationGroupName"
                ErrorMessage="Medication group name required">
            </asp:RequiredFieldValidator>      
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>What it does *</strong>
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reWhatIdDoes" SkinID="DefaultSetOfTools" ToolbarMode="ShowOnFocus"
                Height="250px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/explorers" UploadPaths="~/resources/explorers" MaxUploadFileSize="110000000"  DeletePaths="~/resources/explorers" />
                <DocumentManager ViewPaths="~/resources/explorers" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Description *</strong>
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reDescription" SkinID="DefaultSetOfTools" ToolbarMode="ShowOnFocus"
                Height="250px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/explorers" UploadPaths="~/resources/explorers" MaxUploadFileSize="110000000"  DeletePaths="~/resources/explorers" />
                <DocumentManager ViewPaths="~/resources/explorers" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Side Effects Discussion *</strong>
        </td>
        <td class="Normal">
            <telerik:radeditor runat="server" ID="reSideEffects" SkinID="DefaultSetOfTools" ToolbarMode="ShowOnFocus"
                Height="250px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/explorers" UploadPaths="~/resources/explorers" MaxUploadFileSize="110000000"  DeletePaths="~/resources/explorers" />
                <DocumentManager ViewPaths="~/resources/explorers" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
            </telerik:radeditor>
        </td>
    </tr>
    <tr>
        <td colspan="2"><hr /></td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Side Effects:</strong>
        </td>
        <td class="Normal" width="600">
            <blockquote>
                <strong>ADD NEW SIDE EFFECT</strong><br />
                    1. Select Severity:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList runat="server" Width="350px" ID="ddlSeverity">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="Serious">Serious</asp:ListItem>
                        <asp:ListItem Value="Moderate">Moderate</asp:ListItem>
                        <asp:ListItem Value="Common">Common</asp:ListItem>
                    </asp:DropDownList><br />
                    2. Select Side Effect: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" Width="350px" ID="ddlSideEffects"></asp:DropDownList>&nbsp;<br />
                    3. Enter Severity Comment: <asp:TextBox ID="txtSeverityComment" Width="550px" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnManageSideEffects" runat="server" Text="Add Side Effect" OnClick="btnManageSideEffects_Click" CssClass="btn btn-primary" />
                </blockquote>
            <div style="height:5px;"></div>
                <table class="table table-hover table-nomargin table-striped">
	                <thead>
		                <tr>
			                <th>Severity</th>
			                <th class="hidden-1024">Side Effect & Comment</th>
                            <th class="hidden-480">Remove</th>
		                </tr>
	                </thead>
	                <tbody>
                        <asp:Panel ID="pnlSideEffects" runat="server"></asp:Panel>
	                </tbody>
                </table>
                <asp:Label ID="lblSideEffectMessage" runat="server" Visible="false"></asp:Label>
        </td>                
    </tr>
    <tr>
        <td colspan="2"><hr /></td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Associated Links:</strong>
        </td>
        <td class="Normal" width="600">
            <blockquote>
                <strong>ADD NEW LINK</strong><br />
                1. Select Link: <asp:DropDownList runat="server" Width="600px" ID="ddlLinks"></asp:DropDownList>&nbsp;
                <asp:Button ID="btnMedicationLinks" runat="server" Text="Add Link" OnClick="btnManageLinks_Click" CssClass="btn btn-primary" />
            </blockquote>
            <div style="height:5px;"></div>
                <table class="table table-hover table-nomargin table-striped">
	                <thead>
		                <tr>
			                <th>Name</th>
			                <th class="hidden-1024">Link</th>
			                <th class="hidden-480">Remove</th>
		                </tr>
	                </thead>
	                <tbody>
                        <asp:Panel ID="pnlLinks" runat="server"></asp:Panel>
	                </tbody>
                </table>
                <asp:Label ID="lblMedicationLinksMessage" runat="server" Visible="false"></asp:Label>
        </td>                
    </tr>
    <tr>
        <td colspan="2"><hr /></td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Medications:</strong>
        </td>
        <td class="Normal" width="600">
            <blockquote>
            <strong>ADD NEW MEDICATION</strong><br />
            1. Medication Name:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMedicationName" Width="550px" runat="server"></asp:TextBox>
            <br />
            2. Medication Description:<br />
                <telerik:radeditor runat="server" ID="reMedicationDescription" SkinID="DefaultSetOfTools" ToolbarMode="ShowOnFocus"
                Height="150px" Width="750px" OnClientCommandExecuting="changeImageManager">
                <ImageManager ViewPaths="~/resources/explorers" UploadPaths="~/resources/explorers" MaxUploadFileSize="110000000"  DeletePaths="~/resources/explorers" />
                <DocumentManager ViewPaths="~/resources/explorers" MaxUploadFileSize="110000000" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                <Content>
                </Content>
                    <ImageManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></ImageManager>
                    <DocumentManager ViewPaths="~/resources/explorers" 
                    UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                </telerik:radeditor><br />
            3. Medication Special Instructions:<br /> 
                <telerik:radeditor runat="server" ID="reMedicationSpecialInstructions" SkinID="DefaultSetOfTools" ToolbarMode="ShowOnFocus"
                    Height="150px" Width="750px" OnClientCommandExecuting="changeImageManager">
                    <ImageManager ViewPaths="~/resources/explorers" UploadPaths="~/resources/explorers" MaxUploadFileSize="110000000"  DeletePaths="~/resources/explorers" />
                    <DocumentManager ViewPaths="~/resources/explorers" MaxUploadFileSize="110000000" 
                        UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                    <Content>
                    </Content>
                        <ImageManager ViewPaths="~/resources/explorers" 
                        UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></ImageManager>
                        <DocumentManager ViewPaths="~/resources/explorers" 
                        UploadPaths="~/resources/explorers" DeletePaths="~/resources/explorers"></DocumentManager>
                </telerik:radeditor><br />
            <asp:Button ID="btnAddMedication" runat="server" Text="Add Medication" OnClick="btnAddMedication_Click" CssClass="btn btn-primary" />
            </blockquote>
            <div style="height:5px;"></div>
                <table class="table table-hover table-nomargin table-striped">
	                <thead>
		                <tr>
			                <th>Name</th>
			                <th class="hidden-1024">Description</th>
                            <th class="hidden-1024">Special Instructions</th>
                            <th class="hidden-480">Remove</th>
		                </tr>
	                </thead>
	                <tbody>
                        <asp:Panel ID="pnlMedications" runat="server"></asp:Panel>
	                </tbody>
                </table>
                <asp:Label ID="lblAddMedicationMessage" runat="server" Visible="false"></asp:Label>
        </td>                
    </tr>
    <tr>
        <td colspan="2"><hr /></td>
    </tr>
    <tr>
        <td width="150" valign="top">
        <strong>Available to Users *</strong>
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="medication-groups.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Medication Groups</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="medication-group-edit.aspx?medicationGroupID=<%= medication_group_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE MEDICATION GROUP" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


