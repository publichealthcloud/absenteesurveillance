<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="add-member.aspx.cs" Inherits="manage_members_add_member" %>

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
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Add New Member</asp:Label>
		</h3>
            <ul class="tabs">
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="ADD MEMBER" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    <table width="100%" border="0" cellspacing="0" cellpadding="5">
            <tbody>
              <tr>
                <td colspan="2" class="NormalBold">&nbsp;<br />
                    <span class="validation2">
                    <asp:Label id="Label2" runat="server" Text="<br><br>The Following Errors Occurred:" visible="false"></asp:Label>
                    </span> &nbsp;&nbsp;
                    <asp:ValidationSummary id="ValidationSummary" runat="server" class="NormalRed"></asp:ValidationSummary>
                </td>
              </tr>
              <tr>
                  <td><h4>MEMBER INFO</h4></td>
              </tr>
              <tr valign="top" bgcolor="#FFFFFF">
                <td width="200px"><strong>First Name *</strong></td>
                <td class="Normal"><div align="left">
                  <telerik:RadTextBox ID="txtFirstName" MaxLength="50" runat="server" Columns="50"></telerik:RadTextBox>           
				* Required
				<span class="validation">
				&nbsp;&nbsp;
				<asp:RequiredFieldValidator id="FirstNameValidator" runat="server" ValidationGroup="account" Display="Dynamic" ErrorMessage="First Name Required" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
				</span>
				</div></td>
              </tr>
              <tr valign="top" bgcolor="#FFFFFF">
                <td><strong>Last Name *</strong></td>
                <td class="Normal"><div align="left">
                  <Telerik:RadTextBox ID="txtLastName" runat="server" Columns="50" MaxLength="50" />                  
				* Required
				<span class="validation">
				&nbsp;&nbsp;
				<asp:RequiredFieldValidator id="LastNameValidator" runat="server" ValidationGroup="account" Display="Dynamic" ErrorMessage="Last Name Required" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
				</span>
				</div></td>
              </tr>
          <tr bgcolor="#FFFFFF">
            <td valign="top"><strong>Username *</strong></td>
            <td valign="top" bgcolor="#FFFFFF" class="Normal"><div align="left">
                <Telerik:RadTextBox ID="txtUserName" runat="server" Columns="50" MaxLength="100" />				
                * Required
				<span class="validation">
				&nbsp;&nbsp;
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserName" ValidationGroup="account" ControlToValidate="txtUserName" Text="Username required" ErrorMessage="Username required" />
	                <asp:CustomValidator ID="cvUserName" runat="server"
                        OnServerValidate = "ValidateUserName"
                        Display = "Dynamic"
                        ValidationGroup="register"
                        ControlToValidate = "txtUserName"
                        Text="<br>This username has already been used. Please select another."
                        ErrorMessage = "This username has already been used. Please select another." />  
				</span>
				</div>
          </td>
          <tr bgcolor="#FFFFFF">
            <td valign="top"><strong>Email *</strong></td>
            <td valign="top" class="Normal"><div align="left">
                <Telerik:RadTextBox ID="txtEmail" runat="server" Columns="50" MaxLength="100" />				
                * Required
				<span class="validation">
				&nbsp;&nbsp;
                    <asp:RequiredFieldValidator runat="server" ID="rfvEmail" ValidationGroup="account" ControlToValidate="txtEmail" Text="Email required" ErrorMessage="Email required" />
                    <asp:CustomValidator ID="cvEmail" runat="server"
                            OnServerValidate = "ValidateEmailAddress"
                            Display = "Dynamic"
                            ValidationGroup="account"
                            ControlToValidate = "txtEmail"
                            Text = "<br>Email address has already been used. Please select another."
                            ErrorMessage = "Email address has already been used. Please select another." />
		            <asp:RegularExpressionValidator runat="server" ID="regexEmail" ValidationGroup="account" ControlToValidate="txtEmail" Text="<br>Email format is not valid." ErrorMessage="Email format is not valid." ValidationExpression="^.*<?[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*@(([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){1,63}\.)+([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){2,63}>?$" />
				</span>
				</div>
          </td>
          </tr>
            <tr valign="top" bgcolor="#FFFFFF">
                <td width="200px"><strong>Primary Member Role *</strong></td>
                <td class="Normal"><div align="left">
                    <asp:DropDownList ID="ddlUserRoles" runat="server">
                    </asp:DropDownList>
                    * Required 
				    <span class="validation">
				    &nbsp;&nbsp;
                    <asp:RequiredFieldValidator runat="server" ID="rfvUserRole" ValidationGroup="account" ControlToValidate="ddlUserRoles" Text="Primary member role required" ErrorMessage="Member role required" />             
			        </span>
                </div>
                </td>
            </tr>
            <tr valign="top" bgcolor="#FFFFFF">
                <td width="200px" class="NormalBold">Registration Type: </td>
                <td class="Normal"><div align="left">
                    <asp:DropDownList ID="ddlRegistrationTypes" runat="server">
                        <asp:ListItem Value="">--</asp:ListItem>
                        <asp:ListItem Value="Pre-LMS Anonymous">Pre-LMS Anonymous</asp:ListItem>
                        <asp:ListItem Value="Pre-LMS Anonymous with Email">Pre-LMS with Email</asp:ListItem>
                        <asp:ListItem Value="On-Site Enrollment">On-Site Enrollment</asp:ListItem>
                        <asp:ListItem Value="3rd Party Registration">3rd Party Registration</asp:ListItem>
                    </asp:DropDownList>              
			    </div></td>
            </tr>
            <tr valign="top" bgcolor="#FFFFFF">
                <td width="200px" class="NormalBold">Registration Notes: </td>
                <td class="Normal"><div align="left">
                    <Telerik:RadTextBox ID="txtRegistrationNotes" TextMode="MultiLine" runat="server" Rows="8" Width="500px"  />
			    </div></td>
            </tr>
            <asp:PlaceHolder ID="plhFunctionalRoles" runat="server">
            <tr>
                <td colspan="2"><h4>FUNCTIONAL ROLES</h4>
                    <div style="background-color:#f2dede; border-color: #eed3d7; color: #b94a48; padding: 5px;">
                        <strong>Optional</strong> - Functional roles are used to automatically assign trainings to users.
                    </div>
                </td>
            </tr>
            <tr>
            <td valign="top" width="200px">Available Functional Roles: </td>
            <td valign="top" class="Normal">
            <asp:CheckBoxList ID="cblFunctionalRoles" CssClass="Normal" runat="server">
            </asp:CheckBoxList>
                <asp:Image runat="server" ID="imgFunctionalRoles" />
			</td>
            </tr>
            </asp:PlaceHolder>
        </tbody>
    </table>

<div class="box">
    <div class="box-title">
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="ADD MEMBER" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>

