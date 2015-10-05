<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="campaign-activity-edit.aspx.cs" Inherits="campaign_manage_activities" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/CampaignActivitiesListEnhanced.ascx" TagPrefix="epg" TagName="CampaignActivitiesListEnhanced" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="position: relative; left: 10px;">
    <div class="box">
	    <div class="box-title">
		    <h3>
			    <i class="icon-group"></i>
			    <asp:Label ID="lblTitle" runat="server">Group</asp:Label>
		    </h3>
                <ul class="tabs">
                    <li runat="server" id="li1">
                        <div class="btn-group">
                            <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaign Activities</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:HyperLink ID="hplRefreshTop" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE ACTIVITY" onclick="btnSave_OnClick" />
                        </div>
                    </li>
                </ul>
                <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
        <div style="height:10px;"></div>
        <table border="0" cellpadding="5" width="900">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
                <i class="icon-external-link"></i> <a target="_blank" href="/social/learning/campaigns/campaign-details.aspx?campaignID=<%=campaign_id %>">View in Site</a>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><strong><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label></strong>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
        <asp:PlaceHolder ID="plhSelectType" runat="server">
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Step 1: Select Type *</strong>
            </td>
            <td class="NormalBold">
                <asp:DropDownList runat="server" Width="450px"  OnSelectedIndexChanged="ddlContentTypes_SelectedIndexChanged" AutoPostBack="true" ID="ddlContentTypes">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="28">Article</asp:ListItem>
                    <asp:ListItem Value="49">Assessment</asp:ListItem>
                    <asp:ListItem Value="6">Blog</asp:ListItem>
                    <asp:ListItem Value="59">Link</asp:ListItem>
                    <asp:ListItem Value="16">Poll</asp:ListItem>
                    <asp:ListItem Value="58">Tip</asp:ListItem>
                    <asp:ListItem Value="47">Training</asp:ListItem>
                    <asp:ListItem Value="31">Thoughts</asp:ListItem>
                    <asp:ListItem Value="12">Video</asp:ListItem>
                </asp:DropDownList>
           </td>
        </tr>
       <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Step 2: Select Activity *</strong>
            </td>
            <td class="NormalBold">
                <asp:DropDownList Enabled="false" Width="450px" runat="server" ID="ddlFeedItems" OnSelectedIndexChanged="ddlFeedItems_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
       </tr>
       <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Step 3: Add Activity *</strong>
            </td>
            <td class="NormalBold">
                <asp:Button ID="btnSelectActivity" Visible="false" runat="server" OnClick="btnSelectActivity_Click" CssClass="btn btn-primary" Text="Add This Activity" />
            </td>
       </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhDisplayType" runat="server">
        <tr>
            <td colspan="2"><h4>BASIC INFORMATION</h4></td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            Activity Type
            </td>
            <td class="NormalBold">
                <asp:Label ID="lblTypeInformation" runat="server"></asp:Label>
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Activity Name *</strong>
            </td>
            <td class="NormalBold">
            <telerik:RadTextBox ID="txtActivityName" MaxLength="100" runat="server" Width="350px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 100 characters</span>
            <asp:RequiredFieldValidator 
                ID="rfvCampaignName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtActivityName"
                ErrorMessage="Activity name required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Points *</strong>
            </td>
            <td class="Normal">
            <telerik:RadNumericTextBox ID="txtPoints" runat="server" Width="50px" MinValue="0" MaxLength="100000" Value="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>&nbsp;
            <asp:RequiredFieldValidator 
                ID="rfvPoints" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtPoints"
                ErrorMessage="Activitiy points required - this can be 0">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td colspan="2"><h4>ACTIVITY SCHEDULE</h4></td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Day in Campaign Active *</strong>
            </td>
            <td class="Normal">
            <telerik:RadNumericTextBox ID="txtDayInCampaign" runat="server" Width="50px" MinValue="0" MaxLength="365" Value="0" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>&nbsp;
                <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="The number of days (i.e. 1, 2, 3) into the campaign that this action is scheduled. NOTE - The day a user signs up is day = 0. Do not schedule any actions for day 0 other than a pre-assessment since you do not know what time of day a member may enroll." data-original-title="Schedule - Day in Campaign Active"><i class="icon-question-sign"></i></a>
            <asp:RequiredFieldValidator 
                ID="rfvDayInCampaign" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtDayInCampaign"
                ErrorMessage="Day in the campaign this action is scheduled required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Hour of Day Active *</strong>
            </td>
            <td class="Normal">
                <asp:DropDownList ID="ddlTimeOfDay" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="0:00:00">12:00 AM</asp:ListItem>
                    <asp:ListItem Value="1:00:00">1:00 AM</asp:ListItem>
                    <asp:ListItem Value="2:00:00">2:00 AM</asp:ListItem>
                    <asp:ListItem Value="3:00:00">3:00 AM</asp:ListItem>
                    <asp:ListItem Value="4:00:00">4:00 AM</asp:ListItem>
                    <asp:ListItem Value="5:00:00">5:00 AM</asp:ListItem>
                    <asp:ListItem Value="6:00:00">6:00 AM</asp:ListItem>
                    <asp:ListItem Value="7:00:00">7:00 AM</asp:ListItem>
                    <asp:ListItem Value="8:00:00">8:00 AM</asp:ListItem>
                    <asp:ListItem Value="9:00:00">9:00 AM</asp:ListItem>
                    <asp:ListItem Value="10:00:00">10:00 AM</asp:ListItem>
                    <asp:ListItem Value="11:00:00">11:00 AM</asp:ListItem>
                    <asp:ListItem Value="12:00:00">12:00 PM</asp:ListItem>
                    <asp:ListItem Value="13:00:00">1:00 PM</asp:ListItem>
                    <asp:ListItem Value="14:00:00">2:00 PM</asp:ListItem>
                    <asp:ListItem Value="15:00:00">3:00 PM</asp:ListItem>
                    <asp:ListItem Value="16:00:00">4:00 PM</asp:ListItem>
                    <asp:ListItem Value="17:00:00">5:00 PM</asp:ListItem>
                    <asp:ListItem Value="18:00:00">6:00 PM</asp:ListItem>
                    <asp:ListItem Value="19:00:00">7:00 PM</asp:ListItem>
                    <asp:ListItem Value="20:00:00">8:00 PM</asp:ListItem>
                    <asp:ListItem Value="21:00:00">9:00 PM</asp:ListItem>
                    <asp:ListItem Value="22:00:00">10:00 PM</asp:ListItem>
                    <asp:ListItem Value="23:00:00">11:00 PM</asp:ListItem>
                </asp:DropDownList>&nbsp;
                <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="The scheduled hour of day when this action is active. This is used to schedule when an email, text message or notification is automatically sent by the system. Both scheduled values - day in campaign and time of day - complete the schedule (i.e. A text message will be sent to all enrolled members of day 1 of the campaign at 10am.)" data-original-title="Schedule - Hour of Day Active"><i class="icon-question-sign"></i></a>
            <asp:RequiredFieldValidator 
                ID="rfvTimeOfDay" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="ddlTimeOfDay"
                ErrorMessage="Scheduled time of day is required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td colspan="2"><h4>ALTERNATE FORMATS</h4></td>
        </tr>
        <asp:PlaceHolder ID="plhEmails" runat="server">
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Associated Email</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlEmails"  Width="350" runat="server"></asp:DropDownList>
           </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhTextMessages" runat="server">
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Associated Text Message</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlTextMessages"  Width="350" runat="server"></asp:DropDownList>
           </td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhNotifications" runat="server">
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Associated Mobile App Notification</strong>
            </td>
            <td class="Normal">
            <asp:DropDownList ID="ddlNotifications" Width="350" runat="server"></asp:DropDownList>
           </td>
        </tr>
        </asp:PlaceHolder>
        </asp:PlaceHolder>
    </table>

    <div class="box">
	    <div class="box-title">
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaign Activities</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:HyperLink ID="hplRefreshBottom" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE ACTIVITY" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    </div>
</div>

</asp:Content>


