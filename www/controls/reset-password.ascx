<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reset-password.ascx.cs" Inherits="controls_reset_password" %>

        <h5>Reset Your Password</h5>	    
        <div class="control-group">
		    <div class="controls">
                <asp:Label ID="lblResetInstructions" runat="server"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" class='input-medium'></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm Password" class='input-medium'></asp:TextBox>
		    </div>
	    </div>
	    <div class="submit">
		    <asp:Button ID="btnResetPassword" runat="server" CssClass="btn btn-primary" OnClick="btnResetPassword_Click" Text="Reset password" />
            <asp:Label id="lblMsgReset" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" /> 
	    </div>
