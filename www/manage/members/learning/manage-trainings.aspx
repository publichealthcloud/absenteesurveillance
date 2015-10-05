<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manage/manage.master"  CodeFile="manage-trainings.aspx.cs" Inherits="qLrn_manage_trainings" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Manage User Training</asp:Label>
		</h3>
            <ul class="tabs">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="user-training-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to User Trainings</a>
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    <div style="height:10px;"></div>
    <table cellpadding="5">
    
    <tr>
        <td colspan="2">
            <asp:Label class="NormalRed" ID="lblMessage" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSendWaitlistEmail" runat="server" OnClick="btnSendWaitlistEmail_Click" Text="Send Email Notification to User - Removed from Waitlist" Visible="false" />
        </td>
    </tr>
        <asp:PlaceHolder ID="plhSelectTraining" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
                Current Learner:
            </td>
            <td class="Normal">
                <asp:Label runat="server" ID="lblLearner"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Select Training
            </td>
            <td>
                <asp:DropDownList ID="ddlTrainings" Width="300px" runat="server" OnSelectedIndexChanged="ddlTrainings_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhManage" runat="server">
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                     Additional Training Actions:  
                    </td>
                    <td class="Normal">
                        <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this training? WARNING - this action cannot be undone');" OnClick="btnDelete_Click" Text="Delete Training" />
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:HyperLink ID="hplResetTrainings" runat="server" Text="Reset Training"></asp:HyperLink>
                    </td>
                </tr>
            <asp:PlaceHolder ID="plhInPersonTraining" runat="server">
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                        Status:
                    </td>
                    <td class="Normal">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="Registration Pending - Waitlisted">Registration Pending - Waitlisted</asp:ListItem>
                        <asp:ListItem Value="Registration Pending">Registration Pending</asp:ListItem>
                        <asp:ListItem Value="Registration Complete - Waitlisted">Registration Complete - Waitlisted</asp:ListItem>
                        <asp:ListItem Value="Registration Complete">Registration Complete</asp:ListItem>
                        <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                        <asp:ListItem Value="In Progress">In Progress</asp:ListItem>
                        <asp:ListItem Value="Completed">Completed</asp:ListItem>
                        <asp:ListItem Value="Completed (passed out)">Completed (passed out)</asp:ListItem>
                        <asp:ListItem Value="Did Not Attend">Did Not Attend</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                        Completion Date:
                    </td>
                    <td class="Normal">
                        <telerik:RadDatePicker ID="dpkCompletedDate" runat="server"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                        Applying for CE Credits:
                    </td>
                    <td class="Normal">
                        <asp:DropDownList ID="ddlCredits" runat="server" ValidationGroup="register">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                            <asp:ListItem Value="No">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                        If yes, which CE Credits?
                    </td>
                    <td class="Normal">
                        <asp:DropDownList ID="ddlSelectCredit" runat="server" ValidationGroup="register"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                        How paying for training?
                    </td>
                    <td class="Normal">
                        <asp:RadioButtonList ID="rblPaymentOption" runat="server">
                            <asp:ListItem Value="ByMail"> Mailing a Check</asp:ListItem>
                            <asp:ListItem Value="InPerson"> Binging a Check to the Event</asp:ListItem>
                            <asp:ListItem Value="ByPO"> Processing a Purchase Order</asp:ListItem>
                            <asp:ListItem Value="ByCC"> Paying by Credit Card</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                    Which work phone number?
                    </td>
                    <td class="Normal">
                    <telerik:RadMaskedTextBox ID="txtPhone" Width="100" runat="server" Mask="###-###-####" ResetCaretOnFocus="True" ValidationGroup="register" RoundNumericRanges="False" EmptyMessage="required" SelectionOnFocus="SelectAll" />
                    </td>
             </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="reset" onclick="btnSave_Click" /><asp:Button ID="btnReload" Visible="false" Text="Reload Manage Trainings" OnClick="btnReload_Click" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Content>
