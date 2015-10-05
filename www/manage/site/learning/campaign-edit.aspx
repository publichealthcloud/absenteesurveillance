<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="campaign-edit.aspx.cs" Inherits="campaign_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/CampaignMemberListView.ascx" TagPrefix="epg" TagName="CampaignMemberListView" %>
<%@ Register Src="~/manage/site/learning/controls/ManageCampaignActivitiesList.ascx" TagPrefix="epg" TagName="CampaignActivitiesList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>

<script type="text/javascript">
    $(function () {
        $(".tb").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/services/MemberGroups.asmx/FetchMemberList",
                    data: "{ 'mail': '" + request.term + "', 'user_id': '" + document.getElementById('lblJSUserID').textContent + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item.Email
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2
        });
    });
</script>

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
                            <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaigns List</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <a target="_blank" href="/manage/campaigns/campaign-details.aspx?campaignID=<%= campaign_id %>" class="btn btn-info"><i class="icon-bar-chart"></i>&nbsp;&nbsp;Campaign Report</a>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <a href="#" data-toggle="dropdown" class="btn dropdown-toggle">Tools <span class="caret"></span></a>
		                    <ul class="dropdown-menu">
				                <li>
                                    <a href="/manage/communications/email/email-log.aspx?campaignID=<%=campaign_id %>">View Campaign Emails</a>
				                </li>
				                <li>
                                    <a href="/manage/communications/messaging/send-campaign-sms.aspx?campaignID=<%=campaign_id %>">Send Text Message Now</a>
				                </li>
				                <li>
                                    <a href="/manage/communications/messaging/sms-message-log.aspx?campaignID=<%=campaign_id %>">View Sent Campaign Text Messages</a>
				                </li>
                                <!--
				                <li>
                                    <a href="/manage/campaigns/campaign-library.aspx?campaignID=<%=campaign_id %>">Send Email Now</a>
				                </li>
                                -->
		                </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE CAMPAIGN" onclick="btnSave_OnClick" />
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
                <!--
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this campaign? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Campaign"></asp:LinkButton>
                &nbsp;&nbsp;&nbsp;
                -->
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
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Campaign Name *</strong>
            </td>
            <td class="NormalBold">
            <telerik:RadTextBox ID="txtCampaignName" MaxLength="100" runat="server" Width="250px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 100 characters</span>
            <asp:RequiredFieldValidator 
                ID="rfvCampaignName" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtCampaignName"
                ErrorMessage="Campaign name required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Campaign Description *</strong>
            </td>
            <td class="Normal">
            <telerik:RadTextBox ID="txtCampaignDescription" TextMode="MultiLine" MaxLength="1000" runat="server" Width="350px" Height="100px"></telerik:RadTextBox>&nbsp;
                <br /><span class="NormalItalics">Max 1000 characters</span>
            <asp:RequiredFieldValidator 
                ID="rfvDescription" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtCampaignDescription"
                ErrorMessage="Campaign description required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Quick Launch URL</strong>
            </td>
            <td class="NormalBold">
                <asp:Label ID="lblQuickLauchURL" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button CssClass="btn" ID="btnCreateCampaignRedirect" runat="server" OnClick="btnCreateCampaignRedirect_Click" Text="Create Quick Launch URL" />&nbsp;
                <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="The quick launch URL is a easy way for members to connect directly with a page of information about the campaign (i.e. www.yoursite.com/code)" data-original-title="Quick Launch URL"><i class="icon-question-sign"></i></a>
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Registration Code *</strong>
            </td>
            <td class="NormalBold">
            <telerik:RadTextBox ID="txtCode" MaxLength="15" runat="server" Width="250px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 15 characters</span>
            <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="The campaign code is used to insure that members register for the correct campaign." data-original-title="Registration Code"><i class="icon-question-sign"></i></a>
            <asp:RequiredFieldValidator 
                ID="rfvCode" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtCode"
                ErrorMessage="Campaign code required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
         <tr>
            <td width="200" class="Normal" valign="top">
            <strong>Search Keyword *</strong>
            </td>
            <td class="NormalBold">
            <telerik:RadTextBox ID="txtKeyword" MaxLength="30" runat="server" Width="250px"></telerik:RadTextBox>&nbsp;<span class="NormalItalics">Max 30 characters</span>
            <a href="#" class="btn" rel="popover" data-trigger="hover" title="" data-content="The campaign keyword IS ALMOST ALWAYS the same as the registration but might be different to make it easier to 
                display associated content on the public campaign pages. This keyword is used to do a search in the system and display all content associated with the campaign." data-original-title="Search Keyword"><i class="icon-question-sign"></i></a>
            <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" 
                runat="server"
                Text="*"
                ValidationGroup="form" 
                ControlToValidate="txtCode"
                ErrorMessage="Campaign code required">
            </asp:RequiredFieldValidator>  
           </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Delivery Modes</strong>
            </td>
            <td class="Normal">
                <asp:Label ID="lblDeliveryModes" runat="server"></asp:Label>
           </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Total Days</strong>
            </td>
            <td class="Normal">
                <asp:Label ID="lblTotalDays" runat="server"></asp:Label>
           </td>
        </tr>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Available *</strong>
            </td>
            <td class="Normal">
                <asp:RadioButtonList ID="rblAvailableNew" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="Label2" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder ID="plhMoreInfo" Visible="false" runat="server">
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Last Updated
            </td>
            <td class="Normal">
                <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Enrollment Information
            </td>
            <td class="Normal">
                <asp:Label ID="lblEnrollmentInfo" CssClass="NormalItalics" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            Number of User Activities
            </td>
            <td class="Normal">
                <asp:Label ID="lblNumberUserActivities" runat="server"></asp:Label>
           </td>
        </tr>
        <asp:PlaceHolder ID="plhActivities" runat="server">
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Activities List</strong>
            </td>
            <td class="Normal" width="600">
                <asp:Button ID="btnManageActivities" runat="server" Text="Manage Activities" OnClick="btnManageActivities_Click" CssClass="btn btn-primary" />
                <div style="height:5px;"></div>
                <epg:CampaignActivitiesList runat="server" ID="CampaignActivitiesList" />
           </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td width="200" class="NormalBold" valign="top">
            <strong>Library</strong>
            </td>
            <td>
                <telerik:RadFileExplorer  runat="server" ID="fxpCampaignLibrary" Visible="true" Width="450px" Height="300px" EnableCreateNewFolder="True">
                </telerik:RadFileExplorer>
            </td>
        </tr>
    </table>

    <div class="box">
	    <div class="box-title">
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Campaign List</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:HyperLink ID="hplRefreshBottom" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE CAMPAIGN" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    </div>
</div>

<asp:PlaceHolder ID="plhCampaignMemberList" runat="server">
<div class="row-fluid">
	<div class="span12">
		<div class="box box-bordered">
			<div class="box-title">
				<h3>
					Members Enrolled
				</h3>
                <ul class="tabs">
                    <li runat="server" id="li2">
                        <div class="btn-group">
			                <asp:LinkButton ID="btnViewAddMember" runat="server" OnClick="btnViewAddMember_Click" CssClass="btn btn-primary"><i class="glyphicon-circle_plus"></i> ENROLL MEMBER</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnRefresh" CssClass="btn" runat="server" OnClick="btnRefresh_Click"><i class="icon-refresh"></i> Refresh Enrolled</asp:LinkButton>
                        </div>
                        <div class="btn-group">
                            <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Enrolled</asp:LinkButton>
                        </div>
                    </li>
                </ul>
                <asp:PlaceHolder ID="plhAddMember" runat="server" Visible="false">
                    <br /><br /><strong>Member to add:</strong>&nbsp;&nbsp;<asp:TextBox runat="server" class="input-xlarge tb" name="textfield" ID="txtMemberUserName" placeholder="Start typing a username..."></asp:TextBox>
                    <br /><strong>Available Campaign Modes:</strong>&nbsp;&nbsp;
                    <div>
                        <asp:CheckBox ID="chkEmail" Style="display:inline!important;" TextAlign="Right" runat="server" Text="Email" />
                    </div>
                    <asp:CheckBox ID="chkSMS" runat="server" Text="Text Messaging (SMS)" />
                    <asp:CheckBox ID="chkBrowser" runat="server" Text="Web" />
                    <asp:CheckBox ID="chkMobile" runat="server" Text="Mobile App" />
                    <br />
                    <asp:Button ID="btnAddMember" runat="server" CssClass="btn btn-primary" OnClick="btnAddMember_Click" Text="Add Member" />&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancel_Click1" Text="Cancel Add Member" /> <asp:Label ID="lblMessageMember" runat="server"></asp:Label>
                </asp:PlaceHolder>
			</div>
		</div>
	</div>
</div>
</asp:PlaceHolder>

<telerik:RadGrid ID="gridMembers" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
        AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
        GridLines="None">
        <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
        <Columns>
            <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                DataField="UserCampaignID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="User ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                DataField="UserID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Username" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Name" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Name" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Email" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="Email" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="75">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="Day In Campaign" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="DayInCampaign" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderText="DeliveryMethods" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                DataField="DeliveryMethods" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn UniqueName="StartDate" DataField="StartDate" HeaderText="Started" AllowSorting="true"
                HeaderStyle-Width="100px">
                <FilterTemplate>
                    From
                    <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                        MinDate='<%# minDate %>' MaxDate="1/1/2020" DbSelectedDate='<%# startDate %>' />
                    <br />To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="ToDateSelected"
                        MinDate='<%# minDate %>' MaxDate="1/1/2020" DbSelectedDate='<%# endDate %>' />
                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                        <script type="text/javascript">
                            function FromDateSelected(sender, args) {
                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                var ToPicker = $find('<%# ((GridItem)Container).FindControl("ToDatePicker").ClientID %>');

                                var fromDate = FormatSelectedDate(sender);
                                var toDate = FormatSelectedDate(ToPicker);

                                tableView.filter("StartDate", fromDate + " " + toDate, "Between");

                            }
                            function ToDateSelected(sender, args) {
                                var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                var fromDate = FormatSelectedDate(FromPicker);
                                var toDate = FormatSelectedDate(sender);

                                tableView.filter("StartDate", fromDate + " " + toDate, "Between");
                            }
                            function FormatSelectedDate(picker) {
                                var date = picker.get_selectedDate();
                                var dateInput = picker.get_dateInput();
                                var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                return formattedDate;
                            }
                        </script>

                    </telerik:RadScriptBlock>
                </FilterTemplate>
            </telerik:GridBoundColumn>
            <telerik:GridTemplateColumn DataField="Status" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="75" ItemStyle-Width="75">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "Status") %>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
            <telerik:GridTemplateColumn DataField="LastName" HeaderText="Options" AllowFiltering="false" HeaderStyle-Width="70" ItemStyle-Width="70">
                <ItemTemplate>
                    <a href="#" onclick="viewMemberCampaignActivity('<%# DataBinder.Eval(Container.DataItem, "UserID") %>', '<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>'); return false;" class="btn" rel="tooltip" title="Member Activity"><i class="icon-dashboard"></i></a>
			        <a href="#" onclick="openUserInfo('<%# DataBinder.Eval(Container.DataItem, "UserID") %>')" class="btn" rel="tooltip" title="Manage Member"><i class="icon-user"></i></a>
                    <a href="/manage/communications/messaging/send-campaign-sms.aspx?campaignID=<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>&userID=<%# DataBinder.Eval(Container.DataItem, "UserID") %>" class="btn" rel="tooltip" title="Send Text Message"><i class="glyphicon-phone"></i></a>
                    <a href="#" onclick="RemoveCampaignUser('<%# DataBinder.Eval(Container.DataItem, "UserID") %>', '<%# DataBinder.Eval(Container.DataItem, "CampaignID") %>'); return false;" class="btn" rel="tooltip" title="Remove from Campaign"><i class="icon-remove"></i></a>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
                    <ClientSettings AllowGroupExpandCollapse="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
            AllowColumnsReorder="True">
        </ClientSettings>
            <GroupingSettings ShowUnGroupButton="true" />
    <FilterMenu EnableTheming="True">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
    </FilterMenu>
</telerik:RadGrid>
</asp:Content>


