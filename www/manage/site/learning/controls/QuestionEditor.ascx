<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionEditor.ascx.cs" Inherits="manage_site_learning_controls_QuestionEditor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/QuestionElementEdit.ascx" TagName="QuestionElement" TagPrefix="epg" %>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <link type="text/css" href="<%= ResolveUrl ("~/styles/smoothness/jquery-ui.css") %>" rel="stylesheet" />
        <script src="<%= ResolveUrl ("~/scripts/jquery-1.4.2.min.js") %>" type="text/javascript"></script>
        <script src="<%= ResolveUrl ("~/scripts/jquery-ui.js") %>" type="text/javascript"></script>
        <script src="<%= ResolveUrl ("~/scripts/ui-utility.js") %>" type="text/javascript"></script>

        <script type="text/javascript">
            $(function () {
                HideLoadingPanel();
                $("#training_tabs").tabs();

                $("#assessment-dialog").dialog({
                    autoOpen: false,
                    width: 560,
                    height: 345
                });

                $("#show-assessments-link").click(function () {
                    $("#assessment-dialog").dialog("open");
                });

                $("#<%= btn_remove_question.ClientID %>").click(function () {
                    $("#remove-question-dialog").dialog("open");
                    return false;
                });

                $("#remove-question-dialog").dialog({
                    autoOpen: false,
                    resizable: false,
                    height: 140,
                    modal: true,
                    buttons: {
                        "Delete": function () {
                            __doPostBack('<%= btn_remove_question.UniqueID %>', '');
                        },
                        Cancel: function () {
                            $(this).dialog("close");
                        }
                    }
                });

            });
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" />
    <telerik:RadFormDecorator runat="server" ID="RadFormDecorator1" DecoratedControls="Default,Zone" />
    <div class="box-title">
		    <h3>
			    <i class="icon-edit"></i>
			    <asp:Label ID="lblTitle" runat="server" Text="Articles">Question Editor</asp:Label>
		    </h3>
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">
			            <asp:LinkButton ID="btnNewQuestion" CssClass="btn btn-primary" runat="server" OnClick="btn_new_question_OnClick" />
                        <asp:LinkButton ID="btnDisplayQuestions" CssClass="btn" runat="server" OnClick="btnReloadQuestions_Click"></asp:LinkButton>
		            </div>
                </li>
                <li>
                    <div class="btn-group">
                        <a href="/manage/site/learning/question-editor.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		            </div>
                </li>
		    </ul>
	    </div>

    <div style="padding: 15px 15px 15px 15px;">
            <asp:PlaceHolder ID="plhNewQuestions" runat="server">
                <div>    
                    <span class="validation2"><asp:Label ID="lblModeNewTitle" Visible="false" runat="server"></asp:Label></span>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="plhEditQuestion" runat="server" Visible="false">
                <blockquote>
                <h4>Find an Existing Question</h4>
                <div>    
                    <strong><asp:Label ID="lblMode" runat="server" Visible="false"></asp:Label></strong><asp:Button ID="btnReloadQuestions" Visible="true" runat="server" CssClass="btn" OnClick="btnReloadQuestions_Click" Text="Reload All Questions" /><br />
                    <label><strong>Pick Category to Find Existing Questions:</strong></label>
                    <div>
                        <asp:DropDownList Width="250px" runat="server" ID="ddl_question_categories" AutoPostBack="true" OnSelectedIndexChanged="ddl_question_categories_OnSelectedIndexChanged" />
                    </div>        
                </div>
            
                <div>
                    <label><strong>Pick An Existing Question:</strong></label>
                    <div>
                        <asp:DropDownList Width="450px" runat="server" ID="ddl_questions" AutoPostBack="true" OnSelectedIndexChanged="ddl_questions_OnSelectedIndexChanged" />
                    </div>
                </div>
            
                <div>
                    <label><strong>-OR- enter a question ID:</strong></label>
                    <asp:TextBox runat="server" ID="txt_question_id" />&nbsp;<asp:Button runat="server" CssClass="btn" ID="btn_select_question_by_id" OnClick="btn_select_question_by_id_OnClick" Text="Find by ID" />
                </div>
                </blockquote>           
            </asp:PlaceHolder>
        <% if (UsageCount > 0)
           { %>

        <fieldset>
            <div>
                <span class="Normal">This question is used in <a id="show-assessments-link" href="#"><%= UsageCount %> assessment(s).</a></span>
            </div>
        </fieldset>
        <div id="assessment-dialog" style="display:none;">
            <ul>
            <% foreach (var a in AssessmentsContainingQuestion)
               { %>
                <li>
                    <%= !string.IsNullOrEmpty (a.Name) ? a.Name : "No name" %> - <span style="text-transform:capitalize;"><%= a.AssessmentType %></span> - ID: <%= a.AssessmentID %>
                </li>
            <% } %>
            </ul>
        </div>
        <% } %>
        
        <asp:PlaceHolder runat="server" ID="plhQuestionDetails" Visible="false">
        <h4><asp:Literal ID="litQuestionEditTitle" runat="server">Edit Question Details</asp:Literal></h4>
        <div> 
            <div>
                <label><strong>Question:</strong></label>
                <asp:TextBox runat="server" ID="txt_new_question" TextMode="MultiLine" Rows="4" Width="99%" />
                <label><strong>Assign a category to this question:</strong></label>
                <div>
                    <asp:DropDownList runat="server" ID="ddl_question_categories_inactive" />
                </div>    
            </div>
            <div style="padding: 5px 0px 15px 50px;">
                <div>
                    <asp:PlaceHolder runat="server" ID="ph_question_elements">
                        <epg:QuestionElement ID="QuestionElement1" OrderNumber="1" runat="server" />
                        <epg:QuestionElement ID="QuestionElement2" OrderNumber="2" runat="server" />
                        <epg:QuestionElement ID="QuestionElement3" OrderNumber="3" runat="server" />
                        <epg:QuestionElement ID="QuestionElement4" OrderNumber="4" runat="server" />
                        <epg:QuestionElement ID="QuestionElement5" OrderNumber="5" runat="server" />
                        <epg:QuestionElement ID="QuestionElement6" OrderNumber="6" runat="server" />
                    </asp:PlaceHolder>
                </div>
                <div>
                    <label><strong>Answer/Overall Question Feedback:</strong></label>
                    <asp:TextBox runat="server" ID="txt_answer" TextMode="MultiLine" Rows="4" Width="99%" />
                </div>
            </div>
            <asp:Button runat="server" ID="btn_add_new_question" CssClass="btn btn-primary" OnClick="btn_save_question_OnClick" Text="Save Question" ValidationGroup="new_question" />&nbsp;&nbsp;
            <asp:Button runat="server" ID="btn_remove_question" CssClass="btn" OnClick="btn_remove_question_OnClick" Text="Remove" />        
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_message_log" EnableViewState="false" />
        </div>

        <div id="remove-question-dialog" style="display:none;">
	        <p>
                <span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>
                Are you sure you want to remove this question? 
                <% if (UsageCount > 0)
                   { %>
                    (This question is used in <%= UsageCount%> assessment(s).
                <% } %>
            </p>
        </div>
        </asp:PlaceHolder>
        </div>