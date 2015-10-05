<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print-moderated-group-invitations.aspx.cs" Inherits="qDbs_print_print_moderated_group_invitations" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body style="
	margin: 0;
	padding: 0;
	font-size:16px;
    line-height:16px;
    font-family: arial,sans-serif;
">
    <form id="form1" runat="server">
    <center>
	<table width="800" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td style="padding:15px 50px; text-align:left; font-style:Arial; font-size:18px; line-height: 20px" align="center">
        <asp:Literal ID="litHeader" runat="server"></asp:Literal>
        <div align="center">
        <div style="height:25px;"></div>
        <asp:Literal ID="litInvitationHTML" runat="server"></asp:Literal>    
        <br />
        </div>
        <div align="left">
            <asp:Literal ID="litFooter" runat="server"></asp:Literal>
            <br />
            <center><strong>Reference Only:</strong> <asp:Label runat="server" ID="lblSpaceID"></asp:Label></center>
        </div>    
        &nbsp;</td>
      </tr>
    </table>
    </center>
    </form>
</body>
</html>
