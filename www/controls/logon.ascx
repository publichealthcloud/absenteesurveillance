<%@ Control Language="C#" AutoEventWireup="true" CodeFile="logon.ascx.cs" Inherits="logon" %>

	<asp:PlaceHolder ID="plhSpaceCode" runat="server" Visible="false">
	<div class="control-group">
		<div class="controls">
            <asp:TextBox ID="txtSpaceCode" runat="server" placeholder="Optional Registration Code? Enter it here." class='input-block-level'></asp:TextBox>
            <asp:Label ID="lblSpaceDescription" runat="server"></asp:Label>
		</div>
	</div>
    </asp:PlaceHolder>
    <div class="control-group">
			<div class="email controls">
				<input type="text" id="txtUserName" name='txtUserName' runat="server" placeholder="Username" class='input-block-level' data-rule-required="true" data-rule-email="true">
                <ASP:RequiredFieldValidator ControlToValidate="txtUserName"
                    Display="Static" ErrorMessage="*" runat="server" 
                    ID="vUserName" />
			</div>
		</div>
		<div class="control-group">
			<div class="pw controls">
				<input type="password" id="txtUserPass" name="upw" runat="server" placeholder="Password" class='input-block-level' data-rule-required="true">
                <ASP:RequiredFieldValidator ControlToValidate="txtUserPass"
                    Display="Static" ErrorMessage="*" runat="server" 
                    ID="vUserPass" />
			</div>
		</div>
		<div class="submit">
           <div class="remember">
                <asp:CheckBox ID="chkLeaveLoggedIn" class='icheck-me' data-skin="square" data-color="aero" runat="server" checked /> <label for="remember">Keep me logged in</label>
			</div>
			<input type="submit" value="Sign me in" runat="server" ID="cmdLogin" class='btn btn-primary'>
            <asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" /> 
		</div>
    <asp:PlaceHolder ID="plhLoginExtraNav" runat="server">
        <div>
            <br />
            <asp:HyperLink ID="hplBack" CssClass="btn" runat="server"><i class="icon-home"></i> Home</asp:HyperLink>
            <asp:HyperLink ID="hplRegister" CssClass="btn" runat="server">Join</asp:HyperLink>
        </div>
    </asp:PlaceHolder>