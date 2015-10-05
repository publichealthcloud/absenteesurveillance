<%@ Control Language="C#" AutoEventWireup="true" CodeFile="forgot-password.ascx.cs" Inherits="controls_forgot_password" %>

<asp:PlaceHolder ID="plhRequest" runat="server" Visible="true">
	    <div class="control-group">
		    <div class="email controls">
                <asp:Label ID="lblRequestInstructions" runat="server"></asp:Label>
                <asp:TextBox ID="txtOrigEmail" runat="server" placeholder="Enter your email" class='input-block-level'></asp:TextBox>
		    </div>
	    </div>
	    <div class="submit">
            <asp:HyperLink ID="hplCancelRequest" NavigateUrl="~/default.aspx" CssClass="btn" runat="server"><i class="icon-home"></i> Home</asp:HyperLink>
		    <asp:Button ID="btnRequestPasswordReset" runat="server" CssClass="btn btn-primary" OnClick="btnRequestPasswordReset_Click" Text="Request reset" />
            <asp:Label id="lblMsgRequest" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" /> 
	    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plhReset" runat="server" Visible="false">
	    <div class="control-group">
		    <div class="controls">
                <asp:Label ID="lblResetInstructions" runat="server"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" class='input-medium'></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" placeholder="Confirm Password" class='input-medium'></asp:TextBox>
		    </div>
	    </div>
	    <div class="submit">
            <asp:HyperLink ID="hplCancelReset" NavigateUrl="~/default.aspx" CssClass="btn" runat="server"><i class="icon-home"></i> Home</asp:HyperLink>
		    <asp:Button ID="btnResetPassword" runat="server" CssClass="btn btn-primary" OnClick="btnResetPassword_Click" Text="Reset password" />
            <asp:Label id="lblMsgReset" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" /> 
	    </div>
</asp:PlaceHolder>