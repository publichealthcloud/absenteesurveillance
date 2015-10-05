<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mobile-enroll.ascx.cs" Inherits="controls_mobile_enroll" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:PlaceHolder ID="plhManage" runat="server">
    <br /><br />
    <blockquote>
    <div class="control-group">
		<div class="controls">
            <asp:PlaceHolder ID="plhNotYetVerified" runat="server">
                <span class="label label-important"><i class="icon-warning-sign"></i> To receive text messages, you must first enroll your mobile phone below.</span><br />
            </asp:PlaceHolder>
             <asp:PlaceHolder ID="plhCurrentlyVerified" runat="server">
                <span class="label label-success"><i class="icon-check"></i> Your mobile phone has been successfully enrolled.</span><br />
            </asp:PlaceHolder>
            <div class="accordion-group">
				<div class="accordion-heading">
					<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#c4">
						<i class="icon-cog"></i> Click to Manage Text Messaging
					</a>
				</div>
				<div id="c4" class="accordion-body collapse">
					<div class="accordion-inner">
                        <telerik:RadMaskedTextBox ID="txtMobileNumber" class='input-medium' Width="100" runat="server" Mask="###-###-####" ResetCaretOnFocus="True" RoundNumericRanges="False" EmptyMessage="required" SelectionOnFocus="SelectAll" />
                        &nbsp;&nbsp;&nbsp;<span style="font-size: 14px; color:gray;">For example, 123-456-7890</span>
                        <br /><br />
                        <asp:Literal ID="litStatus" runat="server"></asp:Literal>
                        <asp:Button ID="btnEnroll" runat="server" CssClass="btn" OnClick="btnEnroll_Click" Text="Enroll Mobile Phone" />
                        <asp:Label id="litMsg" ForeColor="red" Font-Name="Verdana" Font-Size="12" runat="server" />
                        <br /><br />* Normal text messaging rates apply. You can end messages at any time by texting STOP.											
					</div>
				</div>
			</div>           
		</div>
	</div>
    </blockquote>
</asp:PlaceHolder>

<asp:PlaceHolder ID="plhVerify" runat="server" Visible="false">
    <br /><br />
    <blockquote>
    <span class="label label-warning"><i class="icon-exclamation-sign"></i> To verify we can send you text messages, enter the code we just sent to your phone.</span><br /><br />
        <div class="control-group">
		    <div class="controls">
                <asp:TextBox ID="txtMobileVerify" MaxLength="6" placeholder="xxxx" runat="server" class='input-medium'></asp:TextBox>&nbsp;&nbsp;&nbsp;<span style="font-size: 14px; color:gray;">For example, 1234</span>
		    </div>
	    </div>
        <asp:Button ID="btnVerify" runat="server" CssClass="btn" OnClick="btnVerify_Click" Text="Verify Mobile Phone" />
    <asp:Label id="litMsgVerify" ForeColor="red" Font-Name="Verdana" Font-Size="12" runat="server" />
    </blockquote>
</asp:PlaceHolder>

<asp:Label ID="lblCampaignID" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblReturnURL" runat="server" Visible="false"></asp:Label>