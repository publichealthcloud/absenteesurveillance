<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="family-invitation-edit.aspx.cs" Inherits="edit_family_invitation" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Family Invitation</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackTop" runat="server" CssClass="btn"></asp:HyperLink>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="family-invitation-edit.aspx?invitationID=<%= invitation_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE INVITATION" onclick="btnSave_OnClick" />
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
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this invitation? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Invitation"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <i class="icon-print"></i>&nbsp; <asp:HyperLink ID="hplPrint" CssClass="NormalBold" runat="server">Print Invitation</asp:HyperLink>
            &nbsp;&nbsp;&nbsp;
            <i class="icon-envelope"></i>&nbsp; <a href="#" onclick="openEmailInvitationWindow('<%= invitation_id %>'); return false;">Email Invitation</a>
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
        <td width="200" class="NormalBold" valign="top">
        Invitation For *
        </td>
        <td>
            <asp:DropDownList ID="ddlInvitationAudience" runat="server" AutoPostBack="true" OnSelectedIndexChanged="loadInviteSettings">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="new family" Text="Create a New Family"></asp:ListItem>
                <asp:ListItem Value="family" Text="Existing Family"></asp:ListItem>
            </asp:DropDownList> 
        </td>
    </tr>
    <asp:PlaceHolder ID="plhExistingFamily" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Select an Existing Family *
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlExistingFamilies" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ID="rfvExistingFamily" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="ddlExistingFamilies"
                    ErrorMessage="Family required">
                </asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhExistingGroup" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Select an Existing Group *
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlExistingGroups" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator 
                    ID="rfvGroups" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="ddlExistingGroups"
                    ErrorMessage="Group required">
                </asp:RequiredFieldValidator>
                <br />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhEditAudienceName" runat="server">
    <tr>
        <td width="200" class="NormalBold" valign="top">
        <asp:Label ID="lblAudience" runat="server"></asp:Label>
        </td>
        <td>
            <telerik:RadTextBox ID="txtAudienceName" runat="server" Width="500px"></telerik:RadTextBox><asp:Label ID="lblAudienceName" runat="server" CssClass="Normal"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvAudienceName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtAudienceName"
                ErrorMessage="Group name required">
            </asp:RequiredFieldValidator>   
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhCreateFamilyInvitationSettings" runat="server" Visible="false">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Number of New Parent Invitations *
            </td>
            <td class="Normal">
                <telerik:RadNumericTextBox ID="txtNumParents" NumberFormat-DecimalDigits="0" runat="server" Width="100px"></telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvNumParents" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtNumParents"
                    ErrorMessage="Number of parents required">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Number of New Teens Invitations *
            </td>
            <td class="Normal">
                <telerik:RadNumericTextBox ID="txtNumTeens" NumberFormat-DecimalDigits="0" runat="server" Width="100px"></telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvNumTeens" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtNumTeens"
                    ErrorMessage="Number of teens required">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhCreateNewModeratedGroup" runat="server" Visible="false">
         <tr>
            <td width="200" class="NormalBold" valign="top">
            Group Name *
            </td>
            <td class="Normal">
            <telerik:RadTextBox ID="txtModeratedGroupNameShort" MaxLength="100" runat="server" Width="500px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 100 characters</span>
            <asp:RequiredFieldValidator 
                ID="rfvModeratedGroupNameShort" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtModeratedGroupNameShort"
                ErrorMessage="Short group name required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            Full Group Name (optional)
            </td>
            <td class="Normal">
            <telerik:RadTextBox ID="txtModeratedGroupName" MaxLength="500" runat="server" Width="500px"></telerik:RadTextBox>&nbsp;
           </td>
        </tr>
         <tr>
            <td width="200" class="NormalBold" valign="top">
            Group Type *
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlGroupType" runat="server">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="class" Text="Class"></asp:ListItem>
                <asp:ListItem Value="club" Text="Club"></asp:ListItem>
                <asp:ListItem Value="organization" Text="Organization"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
                ID="rfvGroupType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlGroupType"
                ErrorMessage="Group type required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>       
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhCreateModeratedGroupInvitationSettings" runat="server" Visible="false">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Number of New Moderator Invitations *
            </td>
            <td class="Normal">
                <telerik:RadNumericTextBox ID="txtNumGroupModerators" NumberFormat-DecimalDigits="0" runat="server" Value="1" Width="100px"></telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvNumGroupModerators" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="txtNumGroupModerators"
                    ErrorMessage="Number of moderators required">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhModeratedGroupInfo" runat="server" Visible="false">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Invitation For
            </td>
            <td class="Normal">
                <asp:Label ID="lblInvitationForUserRole" runat="server"></asp:Label>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhFamilyContactInfo" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" valign="top">
            Family Contact Information (Optional)
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <table cellpadding="2" width="100%">
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact First Name
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtFirstName" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Last Name
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtLastName" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Address1
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtAddress1" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Address2
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtAddress2" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact City
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtCity" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact State/Province
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtStateProvince" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Postal Code
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtPostalCode" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Country
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtCountry" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Phone 1
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtPhone1" runat="server" Width="100px"></telerik:RadTextBox>&nbsp;
                            <asp:DropDownList ID="ddlPhone1Type" runat="server">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="Home" Text="Home"></asp:ListItem>
                                <asp:ListItem Value="Work" Text="Work"></asp:ListItem>
                                <asp:ListItem Value="Mobile" Text="Mobile"></asp:ListItem>
                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Phone 2
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtPhone2" runat="server" Width="100px"></telerik:RadTextBox>&nbsp;
                            <asp:DropDownList ID="ddlPhone2Type" runat="server">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="Home" Text="Home"></asp:ListItem>
                                <asp:ListItem Value="Work" Text="Work"></asp:ListItem>
                                <asp:ListItem Value="Mobile" Text="Mobile"></asp:ListItem>
                                <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Email
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtEmail" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="200" class="Normal" valign="top">
                        Contact Relationship
                        </td>
                        <td class="Normal">
                            <telerik:RadTextBox ID="txtRelationship" runat="server" Width="200px"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhInvitationCode" runat="server">
    <tr>
        <td width="200" class="NormalBold" valign="top">
        Invitation Code
        </td>
        <td class="Normal">
            <telerik:RadTextBox ID="txtInvitationCode" Enabled="false" runat="server" Width="100px"></telerik:RadTextBox>&nbsp;&nbsp;<span class="NormalItalics">Automatically generated by the system</span>
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhNumRedemptions" runat="server" Visible="false">
    <tr>
        <td width="200" class="Normal" valign="top">
            <asp:Label ID="lblMaxRedemptions" runat="server" Text="Max Number of Redemptions"></asp:Label>
        </td>
        <td class="Normal">
            <telerik:RadNumericTextBox ID="txtMaxRedemptions" NumberFormat-DecimalDigits="0" runat="server" Width="100px"></telerik:RadNumericTextBox>&nbsp;&nbsp;<span class="NormalItalics">Leave empty for unlimited</span>
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhCurrRedemptions" runat="server" Visible="false">
    <tr>
        <td width="200" class="Normal" valign="top">
        Current Redemptions
        </td>
        <td class="Normal">
            <asp:Label ID="lblCurrRedemptions" runat="server"></asp:Label>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="200" class="NormalBold" valign="top">
        Valid From *
        </td>
        <td>
            <telerik:RadDateTimePicker ID="rdtStartTime" runat="server" Width="200"></telerik:RadDateTimePicker>
            <asp:RequiredFieldValidator 
                ID="rfvStartTime" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rdtStartTime"
                ErrorMessage="Valid from date required">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td width="200" class="NormalBold" valign="top">
        Valid Until *
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
        Can be Redeemed *
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
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <asp:HyperLink ID="hplBackBottom" runat="server" CssClass="btn"></asp:HyperLink>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="edit-invitation.aspx?invitationID=<%= invitation_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE INVITATION" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>
</asp:Content>


