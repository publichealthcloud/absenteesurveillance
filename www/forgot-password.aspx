<%@ Page Title="" Language="C#" MasterPageFile="~/simple.master" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs" Inherits="forgot_password" %>
<%@ Register Src="~/controls/forgot-password.ascx" TagPrefix="epg" TagName="forgotpassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h2>RESET PASSWORD</h2>
		<form id="form1" runat="server">
            <epg:forgotpassword runat="server" ID="forgotpassword" />
        </form>
	<div class="forget">
		<a href="#" onclick="showInfoModal('registration-questions', 'Registration Questions'); return false;"><span>Password Reset Questions?</span></a>
	</div>
</asp:Content>

