<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reset-trainings.aspx.cs" Inherits="qLrn_reset_trainings" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../quartz.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <table cellpadding="5">
    
    <tr>
        <td colspan="2">
            <asp:Label class="NormalRed" ID="lblMessage" runat="server"></asp:Label>
        </td>
    </tr>
    <asp:PlaceHolder ID="plhAction" runat="server">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblInstructions" CssClass="CapsHeader3" runat="server" Text="Reset / Delete Trainings"></asp:Label>
        </td>
    </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
                STEP 1: Select Action
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedItemChanged">                    
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="reset">Reset Trainings</asp:ListItem>
                    <asp:ListItem Value="delete">Delete Trainings</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhTrainings" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            STEP 2: Select Training(s)
            </td>
            <td>
                <asp:CheckBoxList ID="cblUserTrainings" CssClass="Normal" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhResetOptions" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
                STEP 4: Training Mode
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlTrainingMode" runat="server">
                <asp:ListItem Value="open">Open - jump to any slide</asp:ListItem>
                <asp:ListItem Value="open">Controlled - must advance one slide at a time</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
                STEP 3: Training Start Date
            </td>
            <td class="Normal">
                <telerik:RadDatePicker ID="dpkStartDate" runat="server"></telerik:RadDatePicker>
                <asp:RequiredFieldValidator id="StartDateValidator" runat="server" ValidationGroup="reset" Display="Dynamic" ErrorMessage="Start Date Required" ControlToValidate="dpkStartDate"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
                STEP 4: Days Training is Available
            </td>
            <td class="Normal">
                <telerik:RadNumericTextBox ID="txtDaysAvailable" runat="server" MinValue="0" MaxValue="10000"></telerik:RadNumericTextBox> 
                <asp:RequiredFieldValidator id="DaysAvailableValidator" runat="server" ValidationGroup="reset" Display="Dynamic" ErrorMessage="Days Available Required" ControlToValidate="txtDaysAvailable"></asp:RequiredFieldValidator>
                (between 0 and 10,000 days)
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="reset" OnClientClick="return confirm('Are you sure you want to perform this action? WARNING - this action cannot be undone');" onclick="btnSave_Click" /><asp:Button ID="btnReload" Visible="false" Text="Reload Manage Trainings" OnClick="btnReload_Click" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
