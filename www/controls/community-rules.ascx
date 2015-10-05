<%@ Control Language="C#" AutoEventWireup="true" CodeFile="community-rules.ascx.cs" Inherits="controls_community_rules" %>

<asp:Literal ID="litCommunityRules" runat="server"></asp:Literal>
<asp:Label id="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />

<asp:Button ID="btnAgreeRules" runat="server" CssClass="btn btn-primary" Text="I AGREE" OnClick="btnAgreeRules_Click" />
<asp:Button ID="btnDoNotAgree" runat="server" CssClass="btn" Text="I Do Not Agree" OnClick="btnDoNotAgree_Click" />