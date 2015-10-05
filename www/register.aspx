<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/simple.master" CodeFile="register.aspx.cs" Inherits="register" %>
<%@ Register Src="~/controls/register.ascx" TagPrefix="epg" TagName="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h2>REGISTER</h2>
		<form id="form1" runat="server">
            <epg:register runat="server" ID="register_control" />
        </form>
	<div class="forget">
		<a href="#" onclick="showInfoModal('registration-questions', 'Registration Questions'); return false;"><span>Registration Questions?</span></a>
	</div>
</asp:Content>
