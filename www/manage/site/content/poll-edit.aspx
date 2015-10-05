<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="poll-edit.aspx.cs" Inherits="edit_poll" %>
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
			<asp:Label ID="lblTitle" runat="server" Text="Polls">Polls</asp:Label>
		</h3>
            <ul class="tabs">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <li runat="server" id="li1">
                    <div class="btn-group">  
                        <a href="polls-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Polls</a>
                    </div>
                </li>
                </asp:PlaceHolder>
                <li>
                    <div class="btn-group">
                        <a href="poll-edit.aspx?pollID=<%= poll_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Reload</a>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE POLL" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>
    <div style="height:10px;"></div>
    <table border="0" cellpadding="5">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
            <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this poll? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Poll"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Poll Question *
        </td>
        <td>
            <telerik:RadTextBox ID="txtQuestion" TextMode="MultiLine" runat="server" Width="500px" Height="80px"></telerik:RadTextBox>
            <asp:RequiredFieldValidator 
                ID="rfvQuestion" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtQuestion"
                ErrorMessage="Question required">
            </asp:RequiredFieldValidator>       
        </td>
    </tr>
    <tr>
        <td width="100" class="NormalBold" valign="top">
        Poll Type *
        </td>
        <td valign="top">
            <asp:DropDownList ID="ddlPollType" runat="server" Width="250px">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="Fact" Text="Fact - Multiple Choice Response"></asp:ListItem>
                <asp:ListItem Value="Opinion" Text="Opinion - Multiple Choice Response"></asp:ListItem>
                <asp:ListItem Value="Think" Text="Opinion - Agree or Disagree Response Only"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label  runat="server" ID="lblPollTypeMessage" CssClass="NormalBoldDarkGrayItalics"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvType" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlPollType"
                ErrorMessage="Poll type is required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <asp:PlaceHolder ID="plhChoices" runat="server">
    <tr>
        <td width="100" class="NormalBold" valign="top">
        Responses *
        </td>
        <td valign="top">
            <asp:PlaceHolder ID="plhAddResponse" runat="server">
            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-small" OnClick="lnkAddresponse_Click" ID="lnkAddresponse" Text="New Response"><i class="glyphicon-circle_plus"></i>&nbsp;&nbsp;New Response</asp:LinkButton>
            </asp:PlaceHolder>
            <table width="700" cellspacing="0" cellpadding="2">
                <tr>
                    <td width="300" class="NormalBoldDarkGray">
                        Response
                    </td>
                    <td width="100" class="NormalBoldDarkGray">
                        Correct? 
                    </td>
                    <td width="300" class="NormalBoldDarkGray">
                        Feedback
                    </td>
                </tr>
                    <asp:Repeater ID="repNoEditChoices" runat="server">
                        <ItemTemplate>
                                <tr><td width="300" class="Normal" bgcolor="#f4f4f4">
                                    <strong><%# DataBinder.Eval(Container.DataItem, "Choice") %></strong>&nbsp;&nbsp; <span class="NormalItalics">Not Editable due to Poll Type</span>
                                </td>
                                <td width="100" class="Normal" bgcolor="#f4f4f4">
                                    <%# DataBinder.Eval(Container.DataItem, "Correct") %>   
                                </td>
                                <td width="300" class="Normal" bgcolor="#f4f4f4">
                                    <%# DataBinder.Eval(Container.DataItem, "FeedbackTitle") %>
                                </td></tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                                <tr><td width="300" class="Normal">
                                    <strong><%# DataBinder.Eval(Container.DataItem, "Choice") %></strong>&nbsp;&nbsp; <span class="NormalItalics">Not Editable due to Poll Type</span>
                                </td>
                                <td width="100" class="Normal">
                                    <%# DataBinder.Eval(Container.DataItem, "Correct") %>   
                                </td>
                                <td width="300" class="Normal">
                                    <%# DataBinder.Eval(Container.DataItem, "FeedbackTitle") %>
                                </td></tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="repChoices" runat="server">
                        <ItemTemplate>
                                <tr><td width="300" class="Normal" bgcolor="#f4f4f4">
                                    <a href="poll-edit.aspx?pollID=<%= poll_id %>&pollChoiceID=<%# DataBinder.Eval(Container.DataItem, "PollChoiceID") %>&mode=edit-choice"><strong><%# DataBinder.Eval(Container.DataItem, "Choice") %></strong>&nbsp;&nbsp; [Edit]</a>
                                </td>
                                <td width="100" class="Normal" bgcolor="#f4f4f4">
                                    <%# DataBinder.Eval(Container.DataItem, "Correct") %>   
                                </td>
                                <td width="300" class="Normal" bgcolor="#f4f4f4">
                                    <%# DataBinder.Eval(Container.DataItem, "FeedbackTitle") %>
                                </td></tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                                <tr><td width="300" class="Normal">
                                    <a href="poll-edit.aspx?pollID=<%= poll_id %>&pollChoiceID=<%# DataBinder.Eval(Container.DataItem, "PollChoiceID") %>&mode=edit-choice"><strong><%# DataBinder.Eval(Container.DataItem, "Choice") %></strong>&nbsp;&nbsp; [Edit]</a>
                                </td>
                                <td width="100" class="Normal">
                                    <%# DataBinder.Eval(Container.DataItem, "Correct") %>  
                                </td>
                                <td width="300" class="Normal">
                                    <%# DataBinder.Eval(Container.DataItem, "FeedbackTitle") %>   
                                </td></tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
            </table>
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhEditChoice" runat="server" Visible="false">
    <tr>
        <td width="200" class="NormalBold" valign="top">
        </td>
        <td bgcolor="#eeeeee">
            <table>
                <asp:PlaceHolder ID="plhChoiceValidationSummary" runat="server" Visible="false">
                    <tr>
                        <td>
                            <blockquote>
                                <asp:ValidationSummary runat="server" ID="vldChoiceSummary" DisplayMode="List" ValidationGroup="choice" />
                            </blockquote>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td colspan="2" class="NormalBold">
                        CREATE NEW CHOICE
                    </td>
                </tr>
                <tr>
                    <td class="NormalBold">
                        Response *
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtChoice" runat="server" Width="400px"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator 
                            ID="rfvChoice" 
                            runat="server"
                            Text="*"
                            ValidationGroup="choice" 
                            ControlToValidate="txtChoice"
                            ErrorMessage="Choice required">
                        </asp:RequiredFieldValidator> 
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="NormalBold">
                        Media Choice HTML
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtMediaChoiceHTML" runat="server" Width="400px"></telerik:RadTextBox>
                        <br />Media embed must not be more than 350px height (best 315-320px)
                    </td>
                </tr>
                <asp:PlaceHolder ID="plhChoiceCorrect" runat="server">
                <tr>
                    <td class="Normal" valign="top">
                        Correct Answer?
                    </td>
                    <td class="Normal">
                        <asp:RadioButtonList runat="server" ID="rblIsCorrect" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <tr>
                    <td class="Normal" valign="top">
                        Feedback Title
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFeedbackTitle" runat="server" Width="400px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Normal" valign="top">
                        Feedback Text
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFeedbackText" TextMode="MultiLine" runat="server" Width="400px" Height="50px"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="Normal">
                        Feedback Link
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFeedbackLink" runat="server" Width="400px"></telerik:RadTextBox>
                        <br />
                        <span class="NormalItalics">*** make sure to include http or https (i.e https://www.somesite.com/info.html)</span>
                     </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSaveChoice" CssClass="btn btn-primary btn-small" OnClick="btnSaveChoice_Click" Text="SAVE CHOICE" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDeleteChoice" CssClass="btn btn-small" OnClick="btnDeleteChoice_Click" Text="Delete" runat="server" />
                        <asp:Button ID="btnCancelChoice" CssClass="btn btn-small" OnClick="btnCancelChoice_Click" Text="Cancel" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhAdditionalInfo" runat="server">
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Available *
        </td>
        <td class="Normal">
            <asp:RadioButtonList ID="rblAvailable" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                <asp:ListItem Value="No" Text="No"></asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblApprovedBy" CssClass="NormalItalics" runat="server"></asp:Label>
            <asp:RequiredFieldValidator 
                ID="rfvAvailable" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="rblAvailable"
                ErrorMessage="Available required">
            </asp:RequiredFieldValidator> 
        </td>
    </tr>
    <asp:PlaceHolder ID="plhAvailableTimes" runat="server">
    <tr>
        <td width="200" class="NormalBold" valign="top">
        </td>
        <td>
            <table width="500">
                <tr>
                    <td width="200" class="NormalBold" valign="top">
                    Available From *
                    </td>
                    <td>
                        <telerik:RadDateTimePicker ID="rdtStartTime" runat="server"></telerik:RadDateTimePicker>
                        <asp:RequiredFieldValidator 
                            ID="rfvStartTime" 
                            runat="server"
                            Text="*"
                            ValidationGroup="form" 
                            ControlToValidate="rdtStartTime"
                            ErrorMessage="Valid from date required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="NormalBold" valign="top">
                        Available Until *
                    </td>
                    <td>
                        <telerik:RadDateTimePicker ID="rdtEndTime" runat="server"></telerik:RadDateTimePicker>
                        <asp:RequiredFieldValidator 
                            ID="rfvEndTime" 
                            runat="server"
                            Text="*"
                            ValidationGroup="form" 
                            ControlToValidate="rdtEndTime"
                            ErrorMessage="Valid until date required">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Theme
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlTheme"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Associated Keywords
        </td>
        <td>
            <asp:CheckBoxList ID="cblKeywords" CssClass="Normal" RepeatColumns="4" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plhDisplayInSiteSettings" runat="server">
        <asp:PlaceHolder ID="plhHighlightedPoll" runat="server">
            <tr>
                <td width="150" class="NormalBold" valign="top">
                Highlighted Poll
                </td>
                <td class="NormalBold">
                    <asp:CheckBox ID="chkHighlightedPoll" CssClass="Normal" runat="server" /> <asp:Label ID="lblHighlightedMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </asp:PlaceHolder>
    <tr>
        <td width="150" class="NormalBold" valign="top">
        Display in Locations(s)
        </td>
        <td class="NormalBold">
            <asp:CheckBox ID="chkDisplayInFeed" CssClass="Normal" runat="server" /> <asp:Label ID="lblDisplayInFeedMessage" runat="server">Main Site Feed</asp:Label>
            <asp:CheckBox ID="chkDisplayInExplore" CssClass="Normal" runat="server" /> <asp:Label ID="lblDisplayInExploreMessage" runat="server">Explore Section</asp:Label>
            <br /><br />
            Display in Topic Feeds:
            <br />
            <asp:CheckBoxList ID="chkTopics" CssClass="Normal" RepeatColumns="4" runat="server"></asp:CheckBoxList>
            <asp:PlaceHolder ID="plhExistingFeedItem" runat="server">
            <br />
            Move to Top of Feed:
            <br />
            <asp:CheckBox ID="chkMoveToTop" CssClass="Normal" RepeatColumns="4" runat="server"></asp:CheckBox> Move to Top of Feed
            </asp:PlaceHolder>
        </td>
    </tr>
    </asp:PlaceHolder>
    </table>
    <div class="box">
    <div class="box-title">
        <ul class="tabs">
            <asp:PlaceHolder ID="plhManagerViewOnlyBottom" runat="server">
            <li runat="server" id="liShare">
                <div class="btn-group">  
                    <a href="polls-list.aspx" class="btn"><i class="icon-circle-arrow-left"></i>&nbsp;&nbsp;Back to Polls</a>
                </div>
            </li>
            </asp:PlaceHolder>
            <li>
                <div class="btn-group">
                    <a href="edit-poll.aspx?pollID=<%= poll_id %>" class="btn"><i class="icon-refresh"></i>&nbsp;&nbsp;Refresh</a>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="SAVE POLL" onclick="btnSave_OnClick" />
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessageBottom" CssClass="validation2" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>


