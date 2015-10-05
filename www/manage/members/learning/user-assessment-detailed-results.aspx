<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="user-assessment-detailed-results.aspx.cs" Inherits="manage_members_learning_user_assessment_defailed_results" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="Label1" runat="server" Text="Page Zones">Member Assessment Results</asp:Label>
		</h3>
        <ul class="tabs">
            <li>
                <div class="btn-group">
                    <a href="/manage/members/group-types-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="radAssessmentResults" runat="server" PageSize="250" CaseSensitive="false" GroupingSettings-CaseSensitive="false" 
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton"
                    DataField="UserAssessmentID">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Username" HeaderButtonType="TextButton"
                    DataField="Username">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="First Name" HeaderButtonType="TextButton"
                    DataField="FirstName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="LastName" HeaderButtonType="TextButton"
                    DataField="LastName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Email" HeaderButtonType="TextButton"
                    DataField="Email">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="HighestRole" HeaderButtonType="TextButton"
                    DataField="HighestRole">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Profession" HeaderButtonType="TextButton"
                    DataField="Profession">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Location" HeaderButtonType="TextButton"
                    DataField="EmploymentLocation">
                </telerik:GridBoundColumn>                
                <telerik:GridBoundColumn HeaderText="Setting" HeaderButtonType="TextButton"
                    DataField="Setting">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Worksites" HeaderButtonType="TextButton"
                    DataField="WorkSites">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Degrees" HeaderButtonType="TextButton"
                    DataField="Degrees">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Position" HeaderButtonType="TextButton"
                    DataField="Position">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Agency" HeaderButtonType="TextButton"
                    DataField="Agency">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Division" HeaderButtonType="TextButton"
                    DataField="Division">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Gender" HeaderButtonType="TextButton"
                    DataField="Gender">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Race" HeaderButtonType="TextButton"
                    DataField="Race">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Assessment" HeaderButtonType="TextButton"
                    DataField="AssessmentName">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Started" HeaderButtonType="TextButton"
                    DataField="StartTime">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Finished" HeaderButtonType="TextButton"
                    DataField="EndTime">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Min Proficiency" HeaderButtonType="TextButton"
                    DataField="MinimumPassingScore">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Score" HeaderButtonType="TextButton"
                    DataField="MemberScore">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Outcome" HeaderButtonType="TextButton"
                    DataField="MemberOutcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat1" HeaderButtonType="TextButton"
                    DataField="Cat1_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat1 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat1_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat1 Questions" HeaderButtonType="TextButton"
                    DataField="Cat1_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat1 Correct" HeaderButtonType="TextButton"
                    DataField="Cat1_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat2" HeaderButtonType="TextButton"
                    DataField="Cat2_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat2 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat2_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat2 Questions" HeaderButtonType="TextButton"
                    DataField="Cat2_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat2 Correct" HeaderButtonType="TextButton"
                    DataField="Cat2_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat3" HeaderButtonType="TextButton"
                    DataField="Cat3_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat3 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat3_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat3 Questions" HeaderButtonType="TextButton"
                    DataField="Cat3_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat3 Correct" HeaderButtonType="TextButton"
                    DataField="Cat3_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat4" HeaderButtonType="TextButton"
                    DataField="Cat4_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat4 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat4_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat4 Questions" HeaderButtonType="TextButton"
                    DataField="Cat4_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat4 Correct" HeaderButtonType="TextButton"
                    DataField="Cat4_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat5" HeaderButtonType="TextButton"
                    DataField="Cat6_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat5 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat5_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat5 Questions" HeaderButtonType="TextButton"
                    DataField="Cat5_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat5 Correct" HeaderButtonType="TextButton"
                    DataField="Cat5_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat6" HeaderButtonType="TextButton"
                    DataField="Cat6_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat6 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat6_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat6 Questions" HeaderButtonType="TextButton"
                    DataField="Cat6_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat6 Correct" HeaderButtonType="TextButton"
                    DataField="Cat6_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat7" HeaderButtonType="TextButton"
                    DataField="Cat7_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat7 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat7_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat7 Questions" HeaderButtonType="TextButton"
                    DataField="Cat7_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat7 Correct" HeaderButtonType="TextButton"
                    DataField="Cat7_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat8" HeaderButtonType="TextButton"
                    DataField="Cat8_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat8 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat8_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat8 Questions" HeaderButtonType="TextButton"
                    DataField="Cat8_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat8 Correct" HeaderButtonType="TextButton"
                    DataField="Cat8_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat9" HeaderButtonType="TextButton"
                    DataField="Cat9_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat9 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat9_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat9 Questions" HeaderButtonType="TextButton"
                    DataField="Cat9_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat9 Correct" HeaderButtonType="TextButton"
                    DataField="Cat9_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat10" HeaderButtonType="TextButton"
                    DataField="Cat10_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat10 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat10_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat10 Questions" HeaderButtonType="TextButton"
                    DataField="Cat10_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat10 Correct" HeaderButtonType="TextButton"
                    DataField="Cat10_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat11" HeaderButtonType="TextButton"
                    DataField="Cat11_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat11 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat11_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat11 Questions" HeaderButtonType="TextButton"
                    DataField="Cat11_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat11 Correct" HeaderButtonType="TextButton"
                    DataField="Cat11_NumCorrect">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat12" HeaderButtonType="TextButton"
                    DataField="Cat12_Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat12 Outcome" HeaderButtonType="TextButton"
                    DataField="Cat12_Outcome">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat12 Questions" HeaderButtonType="TextButton"
                    DataField="Cat12_NumQuestions">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Cat12 Correct" HeaderButtonType="TextButton"
                    DataField="Cat12_NumCorrect">
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

</div>

</asp:Content>

