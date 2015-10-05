<%@ Page Language="C#" MasterPageFile="~/simple.master"  AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="logon" %>
<%@ Register Src="~/controls/logon.ascx" TagPrefix="epg" TagName="logon_control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>SIGN IN</h2> 
        <form runat="server">
            <epg:logon_control runat="server" id="logon_control" />
        </form>
		<div class="forget">
			<a href="forgot-password.aspx"><span>Forgot password?</span></a>
		</div>
</asp:Content>

