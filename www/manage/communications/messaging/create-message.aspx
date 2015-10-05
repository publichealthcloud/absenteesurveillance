<%@ Page Title="" Language="C#" MasterPageFile="~/manage/communications/messaging/test-message.master" AutoEventWireup="true" CodeFile="create-message.aspx.cs" Inherits="text_messages_create_message" %>
<%@ Register Src="~/manage/communications/messaging/message-editor.ascx" TagPrefix="uc1" TagName="messageeditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Create Message</h1>
    <uc1:messageeditor runat="server" id="messageeditor" />
</asp:Content>

