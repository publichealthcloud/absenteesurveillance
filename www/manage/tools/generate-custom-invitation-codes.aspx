<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="generate-custom-invitation-codes.aspx.cs" Inherits="manage_tools_generate_custom_invitation_codes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		            <h3>
			            <i class="icon-group"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Upload Contacts</asp:Label>
		            </h3>
	            </div>
            </div>
            <div style="height:10px;"></div>
            <table border="0" cellpadding="5">
                <tr>
                    <td colspan="2">
                        <div style="padding:15px 10px 15px 10px; background-color:#eee; border-color:darkgray;">
                            <strong>Instructions:</strong> Download the example file and add your data. Note below which columns are required and which are optional. You can leave the optional columns blank. <strong>DO NOT remove any columns from the example file.</strong><br />
                            </div>
                        <br />
                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <a href="/manage/communications/contacts/process-contacts-upload.aspx" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Restart Upload</a>&nbsp;&nbsp;
                    <asp:HyperLink ID="hplDownloadExample" CssClass="btn" runat="server"><i class="icon-download-alt"></i>&nbsp;&nbsp;Download Example File</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">
                <strong>Step 1: How Many Codes</strong>
                </td>
                <td>
                    <telerik:RadNumericTextBox ID="txtNumCodes" MinValue="1" MaxValue="100000" runat="server"></telerik:RadNumericTextBox> &nbsp;<i>Maximum number is 100,000</i>
                </td>
            </tr>
           
            <tr>
                <td width="150" valign="top">
                    <strong>Step 2: Code Type</strong>
                </td>
                <td class="Normal">
                    <asp:DropDownList ID="ddlCodeType" runat="server">
                        <asp:ListItem Value="4char_guid">4 random letters and numbers</asp:ListItem>
                        <asp:ListItem Value="5char_guid">5 random letters and numbers</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td width="150" valign="top">
                    <strong>Step 3: Add Campaign and/or Space Information</strong>
                </td>
                <td class="Normal">
                    Campaign ID: <telerik:RadNumericTextBox ID="txtCampaignID" MinValue="1" MaxValue="10000" runat="server"></telerik:RadNumericTextBox> &nbsp;<br />
                    Space ID: <telerik:RadNumericTextBox ID="txtSpaceID" MinValue="1" MaxValue="10000" runat="server"></telerik:RadNumericTextBox> &nbsp;<br />
                </td>
            </tr>
            
            <tr>
                <td width="100" valign="top">
                    <strong>Step 4: Add Phone Numbers (optional)</strong>
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtPhoneNumbers"></asp:TextBox>
                </td>
                <td>

                </td>
            </tr>
            <tr>
                <td width="150" valign="top">
                    <strong>Step 5: Generate Codes</strong>
                </td>
                <td>
                    <asp:Button ID="btnGenerateCodes" runat="server" OnClick="btnGenerateCodes_Click" Text="Generate Codes" CssClass="btn btn-primary" />
                    <asp:PlaceHolder ID="plhStep4" runat="server" Visible="false">
                    <div style="padding:10px 10px 10px 10px; height: 400px; width: 600px; background-color:#eee; border-color:darkgray; overflow:scroll;">
                        <asp:Label ID="lblMessage" CssClass="NormalDarkGray" Text="No final results yet" runat="server"></asp:Label>
                    </div>
                    <br />
                    <a href="/manage/communications/contacts/contacts-list.aspx" class="btn btn-primary">View All Contacts</a>
                    </asp:PlaceHolder>
                </td>
            </tr>
            </table>
            </div>
        </div>

</asp:Content>

