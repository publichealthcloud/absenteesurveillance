<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="member-learning.aspx.cs" Inherits="manage_members_member_learning" %>
<%@ Register Src="~/manage/members/controls/MemberNav.ascx" TagPrefix="epg" TagName="MemberNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/members/member-list.aspx">Member List</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/members/member-profile.aspx?userID=<%= profile_id %>">Member Profile</a>
				</li>
			</ul>
			<div class="close-bread">
				<a href="#"><i class="icon-remove"></i></a>
			</div>
		</div>
    </div>   
</div>
	<div class="box box-bordered box-color lightgrey">
		<div class="box-title">
			<epg:MemberNav runat="server" ID="MemberNav" />
		</div>
		<div class="box-content nopadding">
			<ul class="tabs tabs-inline tabs-top">
				<li <asp:Literal ID="lit1Class" runat="server"></asp:Literal>>
					<a href="#overview" data-toggle='tab'><i class="icon-info-sign"></i> Overview</a>
				</li>
				<li <asp:Literal ID="lit2Class" runat="server"></asp:Literal>>
					<a href="#trainings" data-toggle='tab'><i class="glyphicon-lightbulb"></i> Assigned Trainings</a>
				</li>
				<li <asp:Literal ID="lit3Class" runat="server"></asp:Literal>>
					<a href="#assessments" data-toggle='tab'><i class="icon-question-sign"></i> Assessments</a>
				</li>
				<li <asp:Literal ID="lit4Class" runat="server"></asp:Literal>>
					<a href="#certificates" data-toggle='tab'><i class="icon-certificate"></i> Certificates</a>
				</li>
				<li <asp:Literal ID="lit5Class" runat="server"></asp:Literal>>
					<a href="#tools" data-toggle='tab'><i class="icon-cog"></i> Tools</a>
				</li>
			</ul>
			<div class="tab-content padding tab-content-inline tab-content-bottom">
				<div <asp:Literal ID="litTab1Class" runat="server"></asp:Literal> id="overview">
					<asp:Label class="NormalRed" ID="lblTab1Message" runat="server"></asp:Label>	
                    <div class="row-fluid">
					    <div class="span12">
                            <h4>Member Trainings</h4>
                            <asp:Literal ID="litNumTrainings" runat="server"></asp:Literal>
                            <br />
                            <br />
                            <h4>Member Assessments</h4>
                            <asp:Literal ID="litNumAssessments" runat="server"></asp:Literal>
                            <br />
					    </div>
			        </div>
				</div>

				<div <asp:Literal ID="litTab2Class" runat="server"></asp:Literal> id="trainings">
                     <h4>Assigned Trainings</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab2Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            <asp:Literal ID="litUserTrainingList" runat="server"></asp:Literal>
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab3Class" runat="server"></asp:Literal> id="assessments">
                     <h4>Assessments</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab3Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            <telerik:RadGrid ID="RadGrid1" DataSourceID="userAssessments" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
                                AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                GridLines="None" onitemcommand="RadGrid1_ItemCommand">
                                <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                            <SelectFields>
                                                <telerik:GridGroupByField FieldAlias="Assessment" FieldName="Name"></telerik:GridGroupByField>
                                            </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="Name" SortOrder="Descending"></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                                        DataField="UserAssessmentID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Assessment" DataField="Name" HeaderButtonType="TextButton" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="180" ItemStyle-Width="180">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Type" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="AssessmentType" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="LastName" HeaderText="Member"
                                        UniqueName="LastName" AllowFiltering="true" AutoPostBackOnFilter="true" HeaderStyle-Width="120" ItemStyle-Width="120" FilterControlWidth="50">
                                        <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "FirstName") %> <%# DataBinder.Eval(Container.DataItem, "LastName") %> (<%# DataBinder.Eval(Container.DataItem, "UserName") %>)
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                                            HeaderStyle-Width="100px">
                                            <FilterTemplate>
                                                From
                                                <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                                                    MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# startDate %>' />
                                                <br />To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="ToDateSelected"
                                                    MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# endDate %>' />
                                                <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                                    <script type="text/javascript">
                                                        function FromDateSelected(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            var ToPicker = $find('<%# ((GridItem)Container).FindControl("ToDatePicker").ClientID %>');

                                                            var fromDate = FormatSelectedDate(sender);
                                                            var toDate = FormatSelectedDate(ToPicker);

                                                            tableView.filter("Created", fromDate + " " + toDate, "Between");

                                                        }
                                                        function ToDateSelected(sender, args) {
                                                            var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                                            var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                                            var fromDate = FormatSelectedDate(FromPicker);
                                                            var toDate = FormatSelectedDate(sender);

                                                            tableView.filter("Created", fromDate + " " + toDate, "Between");
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
                                    <telerik:GridBoundColumn HeaderText="Outcome" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                                        DataField="Outcome" HeaderStyle-Width="100" ItemStyle-Width="100" AllowSorting="true" FilterControlWidth="50">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="Score" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="40"
                                        DataField="Score" HeaderStyle-Width="50" ItemStyle-Width="50">
                                    </telerik:GridBoundColumn>
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
                        <asp:SqlDataSource ID="userAssessments" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            ProviderName="System.Data.SqlClient" 
                            runat="server"></asp:SqlDataSource>
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab4Class" runat="server"></asp:Literal> id="certificates">
                     <h4>Certificates</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab4Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            <ul>
                                <asp:Literal ID="litCertificates" runat="server"></asp:Literal>
                            </ul>
					    </div>
			        </div>               
				</div>

                <div <asp:Literal ID="litTab5Class" runat="server"></asp:Literal> id="tools">
                     <asp:Label class="NormalRed" ID="lblTab5Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            <div class="accordion accordion-widget" id="accordion3">
							    <div class="accordion-group lightgrey">
								    <div class="accordion-heading">
									    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#c2">
										    <h4><i class="icon-plus-sign"></i> Assign Trainings to Member</h4>
									    </a>
								    </div>
								    <div id="c2" class="accordion-body">
									    <div class="accordion-inner">
                                            <div style="background-color:#f2dede; border-color: #eed3d7; color: #b94a48; padding: 5px;">
                                                <strong>Select trainings to add to this member's account</strong>
                                            </div>
							                <div class="control-group">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td colspan="2"><br />
                                                            <asp:Label class="NormalRed" ID="lblMessage" runat="server"><i>Checked traings are already assigned to this member's account.</i></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200" class="NormalBold" valign="top">
                                                        <strong>Available Trainings</strong>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBoxList ID="cblTrainings" CssClass="Normal" runat="server">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200" class="NormalBold" valign="top">
                                                            Needs Assessment Minimum Proficiency
                                                        </td>
                                                        <td class="Normal">
                                                            <telerik:RadNumericTextBox ID="txtInitialAssessmentMinimumProficiency" runat="server" MinValue="0.00" MaxValue="1.00"></telerik:RadNumericTextBox> (between 0.00 and 1.00)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200" class="NormalBold" valign="top">
                                                            Post Assessment Minimum Proficiency
                                                        </td>
                                                        <td class="Normal">
                                                            <telerik:RadNumericTextBox ID="txtPostAssesmentMinimumProficiency" runat="server" MinValue="0.00" MaxValue="1.00"></telerik:RadNumericTextBox> (between 0.00 and 1.00)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="200" class="NormalBold" valign="top">
                                                            Training Navigation
                                                        <td class="Normal">
                                                            <asp:DropDownList ID="ddlNavType" runat="server" Width="300px">
                                                                <asp:ListItem Value="open" Text="Open-user can jump to any slide"></asp:ListItem>
                                                                <asp:ListItem Value="controlled" Text="Controlled-user must go slide-by-slide"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnAddTrainings" runat="server" CssClass="btn btn-primary" Text="Add Trainings" OnClick="btnAddTrainings_Click" />
                                                        </td>
                                                    </tr>
                                                </table>                            
                                                &nbsp;&nbsp;<asp:Label ID="lblAssignTrainingsMessage" runat="server"></asp:Label>
							                </div>
									    </div>
								    </div>
							    </div>
                            </div>
                            <div class="accordion accordion-widget" id="accordion4">
							    <div class="accordion-group lightgrey">
								    <div class="accordion-heading">
									    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion4" href="#c4">
										    <h4><i class="icon-plus-sign"></i> Reset/Delete Trainings</h4>
									    </a>
								    </div>
								    <div id="c4" class="accordion-body">
									    <div class="accordion-inner">
                                            <div style="background-color:#f2dede; border-color: #eed3d7; color: #b94a48; padding: 5px;">
                                                <strong>Select trainings reset/delete</strong>
                                            </div>
							                <div class="control-group">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label class="NormalRed" ID="Label1" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="plhAction" runat="server">
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
                                                                <asp:Button ID="btnProcessTrainings" runat="server" CssClass="btn btn-primary" Text="Update Trainings" ValidationGroup="reset" OnClientClick="return confirm('Are you sure you want to perform this action? WARNING - this action cannot be undone');" onclick="btnProcessTrainings_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </div>
									    </div>
								    </div>
							    </div>
                            </div>
					    </div>
			        </div>               
				</div>
			</div>
		</div>
	</div>
</asp:Content>

