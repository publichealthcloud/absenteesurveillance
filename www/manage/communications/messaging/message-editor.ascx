<%@ Control Language="C#" AutoEventWireup="true" CodeFile="message-editor.ascx.cs" Inherits="text_messages_message_editor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px 10px 10px 10px">
    <div>
        <asp:PlaceHolder ID="plhMessage" runat="server" Visible="true">
            <div style="position:relative; left:0px; top:0px; padding-bottom:0px;">
                <asp:DropDownList ID="ddlPrograms" Width="350px" runat="server" Skin="Metro">  
                </asp:DropDownList>

            </div>
                <asp:RequiredFieldValidator 
                    ID="rfvProgram" 
                    runat="server"
                    Text="* program required" 
                    CssClass="validation2"
                    ValidationGroup="form" 
                    ControlToValidate="ddlPrograms"
                    ErrorMessage="* program required">
                </asp:RequiredFieldValidator>
            <div style="height:10px;"></div>
            <div>
                <span class="NormalBold" runat="server">Title</span>&nbsp;<telerik:RadTextBox ID="txtMessageURI" MaxLength="150" Width="321px" runat="server"></telerik:RadTextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvMessageURI" 
                    runat="server"
                    Text="<br>* title required"
                    CssClass="validation2"
                    ValidationGroup="form" 
                    ControlToValidate="txtMessageURI"
                    ErrorMessage="* title required">
                </asp:RequiredFieldValidator>
            </div>
            <div style="height:10px;"></div>
            <div>
                <div style="position:relative; left:0px; top:0px; padding-bottom:0px;"><span class="NormalBold">Message</span></div>
                <div style="position:relative; left:280px; top:-19px; padding-bottom:0px;"><span class="NormalItalics">remaining</span> <strong><span class="NormalDarkGray" id='charCount'>160</span></strong></div>
                <div style="position:relative; top:-18px;"><telerik:RadTextBox ID="txtMessage" Width="350px" TextMode="MultiLine" Height="100px" onkeyup="keyUP(this)" MaxLength="160" runat="server"></telerik:RadTextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvMessage" 
                    runat="server"
                    Text="<br>* message required"
                    CssClass="validation2"
                    ValidationGroup="form" 
                    ControlToValidate="txtMessage"
                    ErrorMessage="* message required">
                </asp:RequiredFieldValidator>
                </div>
                <div><telerik:RadButton ID="btnSaveMessage" Skin="Metro" ValidationGroup="form" runat="server" OnClick="btnSaveMessage_Click" Text="Save" /></div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhSavedMessage" runat="server" Visible="false">
             <telerik:RadAjaxLoadingPanel runat="server" ID="ralpConfiguration">
             </telerik:RadAjaxLoadingPanel>
             <telerik:RadAjaxPanel runat="server" ID="rapConfiguration" LoadingPanelID="ralpConfiguration">
                <div><asp:Label CssClass="Normal" ID="lblSavedMessage" runat="server"></asp:Label></div>
                <br />
                <div><telerik:RadButton ID="btnNewMessage" Skin="Metro" runat="server" OnClick="btnNewMessage_Click" Text="New Message" />&nbsp;<telerik:RadButton ID="rbShowDialog" Skin="Metro" Text="Send Test Message" runat="server" /></div>

                  <telerik:RadWindow ID="modalPopup" Skin="Metro" runat="server" Width="360px" Height="360px">
                       <ContentTemplate>
                           <div style="padding:10px 10px 10px 10px;">
                               <div style="text-align: left;">
                               <span class="NormalBold">Number:</span>&nbsp;<telerik:RadMaskedTextBox ID="txtMobileNumber" Width="100" runat="server" Mask="###-###-####"
                            ResetCaretOnFocus="True" RoundNumericRanges="False" EmptyMessage="required" SelectionOnFocus="SelectAll" Text="8189036334" />
                                </div>
                               <div style="height:5px;"></div>
                                <div>
                                    <span class="NormalBold">Message:</span>&nbsp;<asp:Label ID="lblMessageToSend" runat="server"></asp:Label>
                                </div>
                                <div style="padding: 10px; text-align: center;">
                                     <telerik:RadButton ID="rbToggleModality" Skin="Metro" Visible="false" Text="Turn Off Modal" OnClientClicked="togglePopupModality"
                                          AutoPostBack="false" runat="server" />
                                     <telerik:RadButton ID="radSendTestMessage" Skin="Metro" Visible="true" AutoPostBack="false" Text="Send Test Message" OnClick="radSendTestMessage_Click"
                                          runat="server" />
                                </div>
                               <div>
                                   <div><asp:Label CssClass="Normal" Visible="false" ID="lblMessage" runat="server"></asp:Label></div>
                               </div>
                            </div>
                       </ContentTemplate>
                  </telerik:RadWindow>

          
             </telerik:RadAjaxPanel>
             <telerik:RadCodeBlock runat="server" ID="rdbScripts">
                  <script type="text/javascript">
                      function togglePopupModality() {
                          var wnd = $find("<%=modalPopup.ClientID %>");
                          wnd.set_modal(!wnd.get_modal());
                          if (!wnd.get_modal()) document.documentElement.focus();
                      }
                      function showDialogInitially() {
                          var wnd = $find("<%=modalPopup.ClientID %>");
                           wnd.show();
                           Sys.Application.remove_load(showDialogInitially);
                       }
                       //Sys.Application.add_load(showDialogInitially);         // remove to NOT have window open on page load/placeholder view
                  </script>
             </telerik:RadCodeBlock>
        
        </asp:PlaceHolder>
    </div>
</div>