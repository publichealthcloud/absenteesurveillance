<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generate-cert-printout.aspx.cs" Inherits="qLrn_generate_cert_printout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" href="styles/pdf.css" />
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <table width="1350">
        <tr>
        <td width="1350" align="center">
        <asp:Image ID="imgCertBgImage" runat="server" />
        <div style="position:absolute; top:345px; left:410px; width:600px; height:80px">
        <p style="color:Black; text-align:left; font-family:Times New Roman; font-size: 36px">This Certificate is to acknowledge that</p>
        </div>
        <div style="position:absolute; top:426px; left:390px; width:600px; height:80px">
        <p style="color:Black; text-align:center; font-family:Arial; font-weight: bold; font-size: 32px"><asp:Label ID="lblName" runat="server"></asp:Label></p>
        </div>
        <div style="position:absolute; top:505px; left:428px; width:600px; height:80px">
        <p style="color:Black; text-align:left; font-family:Times New Roman; font-size: 36px">Has completed the following course:</p>
        </div>
        <div style="position:absolute; top:595px; left:83px; width:1200px; height:120px">
        <p style="color:Black; text-align:center; font-family:Arial; font-weight: bold; font-size: 24px"><asp:Label ID="lblTrainingTitle" runat="server"></asp:Label></p>
        <p style="color:Black; text-align:center; font-family:Arial; font-style:italic; font-size: 16px"><asp:Label ID="lblCompletionDate" runat="server"></asp:Label><asp:Label ID="lblCompletionHours" runat="server"></asp:Label></p>
        </div>
        <div style="position:absolute; top:810px; left:800px; width:600px; height:80px">
        <p style="color:Black; text-align:left; font-family:Arial; font-size: 16px"><asp:Label ID="lblSignature" runat="server"></asp:Label></p>
        </div>
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
