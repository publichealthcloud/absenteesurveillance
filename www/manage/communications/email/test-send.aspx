<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="test-send.aspx.cs" Inherits="manage_communications_email_test_send" Title="Test Send" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-envelope"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Send Test Email</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="emails-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Emails</a>
                    </div>
                </li>
                </asp:PlaceHolder>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
    <div style="height:10px;"></div>
    <table width="100%" cellpadding="5" border="0">
        <tr>
            <td width="125">
                <span class="NormalBold">To:</span>
            </td>
            <td>
                <span class="NormalItalics">This test email will be sent to:
                <telerik:RadTextBox ID="txtEmail" runat="server" Width="230" /> 
        </span></td>
        </tr>
        <tr>
            <td width="125">
                <span class="NormalBold">Replacement Values:</span>
            </td>
            <td>
                <span class="Normal">{value1}</span>
                <telerik:RadTextBox ID="txtValue1" runat="server" Width="100" />
                &nbsp;&nbsp;&nbsp;&nbsp; 
                <span class="Normal">{value2}</span>
                <telerik:RadTextBox ID="txtValue2" runat="server" Width="100" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <span class="Normal">{value3}</span>
                <telerik:RadTextBox ID="txtValue3" runat="server" Width="100" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <span class="Normal">{value4}</span>
                <telerik:RadTextBox ID="txtValue4" runat="server" Width="100" />
                &nbsp;&nbsp;<span class="NormalItalics">All values are optional</span> 
            </td>
        </tr>
        <tr>
            <td width="125">
                 <span class="NormalBold">Subject:</span>
            </td>
            <td>
                <span class="Normal"><asp:Label ID="lblSubject" runat="server" Text="Label"></asp:Label></span>
            </td>
        </tr>
        <tr>
            <td width="125">
                 <span class="NormalBold">Body:</span>
            </td>
            <td>
                <asp:Label ID="lblBody" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="125">
            </td>
            <td>
                <asp:Button ID="btnSendTest" CssClass="btn btn-primary" runat="server" Text="Send Test Email" OnClick="btnSendTest_Click" />&nbsp;
                <asp:Label CssClass="validation2" ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

