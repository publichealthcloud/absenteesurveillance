<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging//test-message.master" AutoEventWireup="true" CodeFile="send-message.aspx.cs" Inherits="text_messages_send_message" %>
<%@ Register Src="~/manage/communications/messaging/send-editor.ascx" TagPrefix="uc1" TagName="sendeditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Send Message</h1>
    <uc1:sendeditor runat="server" ID="sendeditor" />
</asp:Content>

