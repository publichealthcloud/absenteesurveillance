<%@ Control Language="C#" AutoEventWireup="true" CodeFile="register.ascx.cs" Inherits="controls_register" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
    function handle_combo_key_press(sender, args) {
        if (args.get_domEvent().keyCode == 13) {
            search_redirect();
        }
    }
</script>

<style>
    school-selector {
        height: 50px;
    }
</style>

<telerik:RadScriptManager ID="radScriptManager" runat="server"></telerik:RadScriptManager>
<span style="font-size: 11px!important; color: darkgray;">Already have an account? <a href="logon.aspx">Sign In Now</a><br /></span>
<asp:PlaceHolder ID="plhStep1" runat="server" Visible="true">
    
    <asp:Label id="lblMsgTop" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />

    <asp:PlaceHolder ID="plhMobileAvailable" runat="server" Visible="false">
	<div class="control-group">
		<div class="controls">
				<div class="check-line">
                    <asp:Checkbox class='icheck-me' data-skin='minimal' ID="chkAlreadyMobile" AutoPostBack="true" OnCheckedChanged="chkAlreadyMobile_CheckedChanged" runat="server" /> <label class='inline' for="c1">already receiving text messages</label>
                </div>
		</div>
	</div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhMobileNumber" runat="server" Visible="false">
	<div class="control-group">
		<div class="controls">
            Mobile Number: <asp:TextBox ID="txtMobileNumber" MaxLength="10" placeholder="xxxxxxxxxx" runat="server" class='input-small mask_phone'></asp:TextBox>
		</div>
	</div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhInvitation" runat="server" Visible="false">
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtInvitationCode" runat="server" placeholder="Enter Required Invitation Code Here" class='input-block-level'></asp:TextBox>
		</div>
	</div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhSpaceCode" runat="server" Visible="false">
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtSpaceCode" runat="server" placeholder="Optional Registration Code? Enter it here." class='input-block-level'></asp:TextBox>
            <asp:Label ID="lblSpaceDescription" runat="server"></asp:Label>
		</div>
	</div>
    </asp:PlaceHolder>
   <asp:PlaceHolder ID="plhSchool" runat="server">
	<div class="control-group">
		<div class="controls">
            <strong>My School District</strong><br />
                <asp:DropDownList ID="ddlSchoolDistrict" Width="240px" AutoPostBack="true" OnSelectedIndexChanged="ddlSchoolDistrict_SelectedIndexChanged" runat="server">
                </asp:DropDownList>
                <asp:PlaceHolder ID="plhSchoolInfo" runat="server">
                    <br /><strong>My School</strong><br />
                    <asp:TextBox ID="txtSchoolOther" runat="server" placeholder="School Name" class='input-block-level'></asp:TextBox>
                    <telerik:RadComboBox ID="radCBSearch" runat="server" Width="240px" Height="150px" 
                        EmptyMessage="Type School Name to Find" EnableLoadOnDemand="True" ShowMoreResultsBox="true"
                        EnableVirtualScrolling="true" OnItemsRequested="radCBSearch_ItemsRequested"
                        OnClientKeyPressing="handle_combo_key_press" Skin="Sitefinity" Class="school-selector">
                        </telerik:RadComboBox>
                </asp:PlaceHolder>
		</div>
	</div>
   </asp:PlaceHolder>
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" class='input-large'></asp:TextBox><asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" class='input-large'></asp:TextBox>
		</div>
	</div>
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class='input-block-level'></asp:TextBox>
		</div>
	</div>
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="30" placeholder="Username" class='input-block-level'></asp:TextBox>
		</div>
	</div>
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" class='input-large'></asp:TextBox><asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm Password" class='input-large'></asp:TextBox>
		</div>
	</div>
	<div class="control-group">
		<div class="controls"><strong>Date of Birth</strong>
        <span>
            <br />
        <asp:DropDownList ID="ddlMonth" Width="98px" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlDay" Width="60px" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlYear" Width="75px" runat="server">
                    </asp:DropDownList>
            </span>
		</div>
	</div>
	<div class="control-group">
		<div class="controls">
            <strong>Gender</strong> <asp:DropDownList ID="ddlGender" Width="125px" runat="server">
                        <asp:ListItem />
                        <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                        <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                        <asp:ListItem Value="Intersex" Text="Intersex"></asp:ListItem>
                        <asp:ListItem Value="Transgender" Text="Trans*"></asp:ListItem>
                    </asp:DropDownList>
		</div>
	</div>
    <asp:PlaceHolder ID="plhRace" runat="server">
	<div class="control-group">
		<div class="controls">
            <strong>Race</strong> (check all that appy) 
                <asp:CheckBoxList ID="cblRace" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="2">
                    <asp:ListItem Value="Asian/SouthAsian" Text="Asian/SouthAsian"></asp:ListItem>
                    <asp:ListItem Value="Biracial" Text="Biracial"></asp:ListItem>
                    <asp:ListItem Value="Black/African American" Text="Black/African American&nbsp;&nbsp;&nbsp;&nbsp;"></asp:ListItem>
                    <asp:ListItem Value="Latino/a" Text="Latino/a"></asp:ListItem>                   
                    <asp:ListItem Value="Middle Eastern" Text="Middle Eastern"></asp:ListItem>
                    <asp:ListItem Value="Multiracial" Text="Multiracial"></asp:ListItem>
                    <asp:ListItem Value="Pacific Islander" Text="Pacific Islander"></asp:ListItem>
                    <asp:ListItem Value="White/European-American" Text="White/European-American"></asp:ListItem>
                </asp:CheckBoxList>
		</div>
	</div>
    </asp:PlaceHolder>
    <span style="font-size: 11px!important; color: darkgray;">By clicking Sign Up, you agree to our <a href="#" onclick="showInfoModal('terms', 'Terms of Use'); return false;">Terms of Use.</a></span>
	<div class="submit">
        <asp:HyperLink ID="hplBack" NavigateUrl="~/default.aspx" CssClass="btn" runat="server"><i class="icon-home"></i> Home</asp:HyperLink>
        <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" ValidationGroup="register" OnClick="btnSignUp_Click" CssClass="btn btn-primary" />

        <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
            <asp:CustomValidator ID="cvValidateAllData" runat="server"
                OnServerValidate = "ValidateEverything"
                Display = "Dynamic"
                ValidationGroup="register"
                ValidateEmptyText="true"
                ControlToValidate = "txtFirstName"
                Text="*"
                ErrorMessage = "*" /> 
	</div>   


<asp:Label runat="server" Visible="false" ID="lblMinAge"></asp:Label>
<asp:Label runat="server" Visible="false" ID="lblMaxAge"></asp:Label>  
     
</asp:PlaceHolder>

<asp:PlaceHolder ID="plhStep2" runat="server" Visible="false">
 
</asp:PlaceHolder>

<asp:PlaceHolder ID="plhStep3" runat="server" Visible="false">

</asp:PlaceHolder>

<asp:PlaceHolder ID="plhStep4" runat="server" Visible="false">

</asp:PlaceHolder>