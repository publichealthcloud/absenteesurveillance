<%@ Page Title="" Language="C#" MasterPageFile="~/simple.master" AutoEventWireup="true" CodeFile="finalize-account.aspx.cs" Inherits="finalize_account" %>
<%@ Register Src="~/controls/finalize-account.ascx" TagPrefix="epg" TagName="finalize_account" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h2>FINALIZE YOUR ACCOUNT</h2>
		<form id="form1" runat="server">
            <epg:finalize_account runat="server" ID="finalizeAccountID" />
        </form>
	<div class="forget">
		<a href="#" onclick="showInfoModal('registration-questions', 'Registration Questions'); return false;"><span>Password Reset Questions?</span></a>
	</div>
</asp:Content>

