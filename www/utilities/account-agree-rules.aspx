<%@ Page Title="" Language="C#" MasterPageFile="~/utilities/user-actions.master" AutoEventWireup="true" CodeFile="account-agree-rules.aspx.cs" Inherits="utilities_account_agree_rules" %>
<%@ Register Src="~/controls/community-rules.ascx" TagPrefix="epg" TagName="agreerules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%;">
        <div class="pull-left">
            <h3><span style="font-weight: 300;">COMMUNITY RULES</span></h3>
        </div>
    </div>
    <div style="height: 60px;">

    </div>
    <div> 
            <epg:agreerules runat="server" ID="agreecommunityrules" />
    </div>  
</asp:Content>

