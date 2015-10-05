<%@ Page Language="C#" AutoEventWireup="true" CodeFile="examples.aspx.cs" Inherits="sdk_facebook_get_current_app_user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Current User FB Name: <asp:Label ID="lblFacebookUserName" runat="server"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnPostToWall" CssClass="btn" runat="server" Text="Post to User's Wall" OnClick="btnPostToWall_Click" />&nbsp;&nbsp;<asp:Label ID="lblPostSuccess" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
