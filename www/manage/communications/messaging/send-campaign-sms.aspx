<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="send-campaign-sms.aspx.cs" Inherits="manage_communications_messaging_send_campaign_sms" %>
<%@ Register Src="~/manage/campaigns/controls/CampaignAllEnrolledScrolling.ascx" TagPrefix="epg" TagName="CampaignAllEnrolledScrolling" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-envelope"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Send Text Message to All Campaign Members</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="plhManagerViewOnly" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="/manage/site/learning/campaign-edit.aspx?campaignID=<%=CampaignID %>" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Campaign</a>
                    </div>
                </li>
                </asp:PlaceHolder>
            </ul>
            <asp:Label ID="lblMessageTop" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
    <div style="height:10px;"></div>
    <table width="100%" cellpadding="5" border="0">
        <tr>
            <td width="200" valign="top">
                <strong>Step 1: Pick Message</strong>
            </td>
            <td>
                <asp:DropDownList ID="ddlCampaignMessages" runat="server" Width="800px" AutoPostBack="true" OnSelectedIndexChanged="ddlCampaignMessages_SelectedIndexChanged"></asp:DropDownList>
                <asp:Literal ID="litMessageText" runat="server" Text="<br>"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="200" valign="top">
                 <strong>Step 2: View Recipient(s)</strong>
            </td>
            <td>
                <span class="Normal"><asp:Literal ID="litRecipientList" runat="server"></asp:Literal></span>
                <epg:CampaignAllEnrolledScrolling runat="server" id="CampaignAllEnrolledScrolling" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plhLimitByDays" runat="server">
        <tr>
            <td width="200" valign="top">
                 <strong>Step 3: Limit Recipient(s)</strong>
            </td>
            <td>
                    <strong>Limit Recipients From Above List To ONLY Those Members Between Specified <i>Days into Campaign</i></strong><br />
                    Minimum Day = <telerik:RadNumericTextBox ID="txtMinDays" NumberFormat-DecimalDigits="0" runat="server" Width="40" MinValue="0" MaxValue="1000" Value="0"></telerik:RadNumericTextBox> *<br />
                    Maximum Day = <telerik:RadNumericTextBox ID="txtMaxDays" NumberFormat-DecimalDigits="0" runat="server" Width="40" MaxValue="1000" Value="0"></telerik:RadNumericTextBox> *
                    <br />* NOTE: Just leave both above values as 0 for the system to ignore limiting by <i>Days into Campaign</i>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td width="200">
            </td>
            <td>
                <asp:CheckBox ID="chkAlignSendTimes" runat="server" /> Adjust sent timestamps to match members days in the campaign<br /><br />
                <asp:Button ID="btnSendConfirm" CssClass="btn btn-danger" runat="server" OnClick="btnSendConfirm_Click" OnClientClick="return confirm('Are you sure you want to send this message to all campaign members? This action CANNOT BE UNDONE. NOTE: If there is more than 1 language of this Message, then the language-appropriate version will be sent.');" Text="SEND NOW" Visible="false" />&nbsp;
                <asp:HyperLink ID="hlpRefresh" runat="server" Visible="false" CssClass="btn"><i class="icon-refresh"></i> Refresh to Send Again</asp:HyperLink>
                <asp:Literal ID="litOutputMessage" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>

</asp:Content>

