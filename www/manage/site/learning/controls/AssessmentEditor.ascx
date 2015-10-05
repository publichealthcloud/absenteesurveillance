<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AssessmentEditor.ascx.cs" Inherits="manage_site_learning_controls_AssessmentEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" />
    <div class="box-title">
	    <h3>
		    <i class="icon-group"></i>
		    <h3 style="text-transform:capitalize;"><asp:Literal runat="server" ID="lit_assessment_type" /> Assessment <asp:Label ID="lblAssessmentID" runat="server"></asp:Label></h3>
	    </h3>
        <ul class="tabs">
            <li runat="server" id="li1">
                <div class="btn-group">
                    <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Assessments List</asp:HyperLink>
                </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:HyperLink ID="hplRefreshTop" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                </div>
            </li>
        </ul>
        <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
    </div>
    <asp:PlaceHolder ID="plhTools" runat="server">
        <div style="height:10px;"></div>
        <table border="0" cellpadding="5" width="900">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="btn_remove_assessment" Text="Remove Assessment" OnClientClick="return confirm('Click OK to remove this assessment.');" OnClick="btn_remove_assessment_OnClick"></asp:LinkButton>
            </td>
        </tr>
        </table>
    </asp:PlaceHolder>
    <div>
        <asp:PlaceHolder ID="plhUpdateAssessmentInfo" runat="server">
            <h4><asp:Label ID="lblAssessmentInfoUpdateTitle" runat="server">Update Assessment Info</asp:Label></h4>
            <blockquote>
                <div>
                    <table>
                        <tr>
                            <td width="300">
                                <strong>Assessment Name *</strong>
                            </td>
                            <td align="right">
                                <telerik:RadTextBox ID="txtAssessmentName" MaxLength="100" runat="server" Width="250px"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator 
                                        ID="rfvAssessmentName" 
                                        runat="server"
                                        Text="*"
                                        ValidationGroup="form" 
                                        ControlToValidate="txtAssessmentName"
                                        ErrorMessage="Assessment name required">
                                    </asp:RequiredFieldValidator>  
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button CssClass="btn" Text="Save Assessment Name" runat="server" ID="btnSaveAssessmentInfo" OnClick="btnSaveAssessmentInfo_Click" />&nbsp;&nbsp;
                                <asp:Button CssClass="btn" Text="Make Available for Campaigns" runat="server" ID="btnMakeAvailableCampaigns" OnClick="btnMakeAvailableCampaigns_Click" /><asp:Label ID="lblExistsFeed" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>      
                </div>
            </blockquote>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhManageQuestion" runat="server">
            <div style="height:18px;"></div>
            <h4><asp:Label ID="lblLegend" runat="server">Add New Question</asp:Label></h4>
            <blockquote>
            <div>
                <table>
                    <tr>
                        <td width="300">
                            <strong><asp:Label ID="lblQuestionLabel" runat="server">Step 1: Pick a question *</asp:Label></strong>
                        </td>
                        <td align="right">
                            <asp:PlaceHolder ID="plhFilter" runat="server">
                            <span class="NormalDarkGray">Filter questions by category:</span> <asp:DropDownList runat="server" ID="ddl_question_categories" AutoPostBack="true" OnSelectedIndexChanged="ddl_question_categories_OnSelectedIndexChanged" />
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="2">
                            <div>
                                <asp:DropDownList runat="server" Width="700px" ID="ddl_questions" AutoPostBack="true" OnSelectedIndexChanged="ddl_questions_OnSelectedIndexChanged" />
                            </div>
                        </td>
                    </tr>
                </table>      
            </div>
        
            <div>
            
            </div>
            <asp:PlaceHolder ID="plhAdditionalQuestionInfo" runat="server">
            <div>                
                <asp:RadioButtonList CssClass="Normal" runat="server" ID="rbl_question_elements" Enabled="false" />
                <label><strong>Step 2: Number of Attempts *</strong></label> 
                <asp:DropDownList ID="ddl_number_attempts" runat="server">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                </asp:DropDownList><asp:RequiredFieldValidator runat="server" CssClass="validation2" ID="rfv_existing_question_attempts" Text=" * Missing number of attempts." ControlToValidate="ddl_number_attempts" ValidationGroup="existing_question" />
                <asp:Panel runat="server" ID="ph_review_slides">
                <label><strong>Step 3: Review Slide *</strong></label><asp:DropDownList runat="server" ID="ddl_review_slides" /><asp:RequiredFieldValidator runat="server" CssClass="validation2" ID="rfv_review_slides" Text="* Missing review slide." ControlToValidate="ddl_review_slides" ValidationGroup="existing_question" />
                </asp:Panel>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_existing_question_attempts" Text="Missing number of attempts." />--%>
                <%--            <div>
                    <label>Answer Text:</label>
                    <div><asp:TextBox runat="server" ID="txt_answer_text" TextMode="MultiLine" Rows="4" Width="99%" /></div>
                </div>
                --%>
                <asp:Label CssClass="validation2" ID="lblPreWarning" runat="server"></asp:Label>        
                <asp:Button runat="server" ID="btn_update_existing_assessmentquestion" OnClick="btn_update_existing_assessmentquestion_OnClick" Text="Update Existing Question" CssClass="btn btn-primary" ValidationGroup="existing_question" />&nbsp;
                <asp:Button runat="server" ID="btn_add_existing_question" OnClick="btn_add_existing_question_OnClick" Text="Add New Question" CssClass="btn btn-primary" ValidationGroup="existing_question" />
                <asp:Button runat="server" ID="btn_cancel" CssClass="btn" OnClick="btn_cancel_Click" Text="Cancel - Return to All Questions" />&nbsp;&nbsp;&nbsp;<asp:Label CssClass="validation2" ID="lblQuestionMessage" runat="server"></asp:Label>
            </div>
            </asp:PlaceHolder>
        </blockquote>
        </asp:PlaceHolder>
    </div>
    <br />
    <asp:PlaceHolder ID="plhManageQuestionsTitle" runat="server"><h4>Manage Existing Questions</h4>
    <blockquote>
    <div>
        <asp:Button runat="server" CssClass="btn" ID="btn_save_question_order" OnClientClick="SaveQuestionOrder();" Text="Save Order" />
        &nbsp;&nbsp;<asp:Label runat="server" CssClass="validation2" ID="lbl_save_question_order_message" EnableViewState="false" />
        <epg:PostBackHandler3 runat="server" ID="pbh_remove_assessment_question" OnPostBack="pbh_remove_assessment_question_OnPostBack" />
        <epg:PostBackHandler3 runat="server" ID="pbh_edit_assessment_question" OnPostBack="pbh_edit_assessment_question_OnPostBack" />
        <epg:PostBackHandler3 runat="server" ID="pbh_save_question_order" OnPostBack="pbh_save_question_order_OnPostBack" />
    </div>
    <div>
    <asp:Repeater runat="server" ID="rpt_assessment_questions" OnItemDataBound="rpt_assessment_questions_OnItemDataBound">
        <HeaderTemplate>
            <strong>Click and drag questions to reorder.</strong>
            <ol id="assessment_questions">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="NormalBold" id='assessment_question_<%# Eval ("AssessmentQuestionID") %>'>
                    <span class="NormalBold"><%# Eval ("Text") %></span>&nbsp;&nbsp;<span class="NormalDarkGray">ID=<%# Eval ("QuestionID") %></span>
                    &nbsp;&nbsp; <a href="#" class="btn btn-small" onclick="RemoveAssessmentQuestion(<%# Eval ("AssessmentQuestionID") %>);"><i class="icon-trash"></i> <span class="NormalBoldGray">Delete Question</span></a>
                    &nbsp;&nbsp; <a href="#" class="btn btn-small" onclick="EditAssessmentQuestion(<%# Eval ("AssessmentQuestionID") %>);"><i class="icon-pencil"></i> <span class="NormalBoldGray">Edit Question</span></a></span> 
                    <table border="0" cellpadding="5">
                        <tr>
                            <td width="15">
                                &nbsp;
                            </td>
                            <td>
                                <div><span class="Normal"><strong>Answer:</strong> <%# Eval ("AnswerText") %></span></div>
                                <asp:RadioButtonList CssClass="Normal" runat="server" ID="rbl_question_elements" Enabled="false" />
                                <div style="padding: 2px 0px 2px 0px;"><span class="Normal"><strong>Attempts:</strong> <%# Eval ("NumAttemptsAllowed") %></span></div>
                                <asp:PlaceHolder ID="plhNotNull" runat="server" Visible='<%# ((Eval ("ReviewSlideID") != null)) %>'>
                                        <div style="padding: 2px 0px 2px 0px;"><span class="Normal"><strong>Review Slide:</strong> <a href="/www2/learning/trainings/<%= TrainingID %>/<%# Eval ("ReviewSlideID") %>.png" target="_blank"><img src="/images/magnifying_glass.gif" /> Click to View Slide</span><span class="NormalDarkGray">&nbsp;&nbsp;ID=<%# Eval ("ReviewSlideID") %></span></a></div>
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ol>
        </FooterTemplate>
    </asp:Repeater>
    </div>
    </blockquote>
    </asp:PlaceHolder>
    <script type="text/javascript">
        $(function () {
            $("#assessment_questions").sortable({
                axis: "y"
            }).disableSelection();
        });

        function SaveQuestionOrder() {
            __doPostBack("<%= pbh_save_question_order.UniqueID %>", $("#assessment_questions").sortable("toArray"));
        }

        function RemoveAssessmentQuestion(question_id) {
            __doPostBack("<%= pbh_remove_assessment_question.UniqueID %>", question_id);
        }

        function EditAssessmentQuestion(question_id) {
            __doPostBack("<%= pbh_edit_assessment_question.UniqueID %>", question_id);
        }
    </script>