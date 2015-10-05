<%@ Control Language="C#" AutoEventWireup="true" CodeFile="send-editor.ascx.cs" Inherits="text_messages_message_editor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div style="padding:10px 10px 10px 10px">
    <div>
        <div style="position:relative; left:0px; top:0px; padding:15px 15px 15px 15px; width:600px; border:solid; border-width:1px; border-color:lightgray;">
            <span class="NormalBold">STEP 1: Who do you want to send a message to?</span><br />
                add checkbox list with list of all programs
                <br /><span class="NormalDarkGrayItalics">A member will ONLY get 1 message even if they appear in multiple groups</span>
        </div>         
        <div style="height:10px;"></div>
            <div style="position:relative; left:0px; top:0px; padding:15px 15px 15px 15px; width:600px; border:solid; border-width:1px; border-color:lightgray;">
                <span class="NormalBold">STEP 2: What do you want to send?</span><br />
                <br />
                <div style="text-align:right;">
                <telerik:RadComboBox ID="RadComboBox2" Skin="Metro" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                    Width="300" Label="Want to shorten the lists...">
                    <Items>
                         <telerik:RadComboBoxItem Text="Healthy Relationships" />
                         <telerik:RadComboBoxItem Text="Project U LA Tips and Info" />
                         <telerik:RadComboBoxItem Text="Sexual Health" />
                         <telerik:RadComboBoxItem Text="Tobacco and Smoking" />
                    </Items>
               </telerik:RadComboBox>
                </div>
                <br />
                <div>
                <asp:DropDownList ID="ddlMessages" Width="600px" runat="server" Skin="Metro">
                    <Items>
                        <asp:ListItem Text="Welcome to Project U LA! Before we complete your sign-up, we need to make sure that you want to sign up for the Project U Mobile program? Text YES or NO" />
                        <asp:ListItem Text="Getting herpes is not the end of the world. Use condoms 2 help prevent giving it 2 partners & medication can reduce symptoms. GO LEARN @ ProjectULA.org" />
                    </Items>
                </asp:DropDownList>
                <span class="NormalBold"><br />- OR -<br /></span>
                <asp:DropDownList ID="ddlRules" Width="600px" runat="server" Skin="Metro">  
                    <Items>
                        <asp:ListItem Text ="Condom Sign Up" />
                    </Items>
                </asp:DropDownList>
                </div>
            </div>
            <div style="height:10px;"></div>
            <div style="position:relative; left:0px; top:0px; padding:15px 15px 15px 15px; width:600px; border:solid; border-width:1px; border-color:lightgray;">
                <span class="NormalBold">STEP 3: When do you want to send it?</span><br />
                <div style="height:10px;"></div>
                <asp:PlaceHolder ID="plhStep3Initial" runat="server">
                <div>
                    <div style="position:relative; left:0px; top:0px; width:300px; text-align:center">
                        <telerik:RadButton ID="btnSendNow" OnClick="btnSendNow_Click" runat="server" Skin="Metro" Height="60px" Width="150px" Text="Send Now"></telerik:RadButton>
                    </div>
                    <div style="position:relative; left:300px; top:-62px; width:300px; text-align:center">
                        <telerik:RadButton ID="btnSendLater" OnClick="btnSendLater_Click" runat="server" Skin="Metro" Height="60px" Width="150px" Text="Send Later"></telerik:RadButton>
                    </div>
                </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plhStep3Details" runat="server" Visible="false">
                    <telerik:RadButton Text="Send Now" runat="server" Skin="Metro"></telerik:RadButton>
                    <span class="NormalBold"><br />- OR -<br /></span>
                    <div>
                        <span class="Normal">Send time</span>&nbsp;<telerik:RadDateTimePicker ID="rdtStartTime" Skin="Metro" runat="server" Width="200"></telerik:RadDateTimePicker>
                        <div><asp:CheckBox ID="chkRecurring" runat="server" Text="Recurring" /></div>
                        <div><telerik:RadButton ID="RadButton1" Text="Save as Calendar Event" runat="server" Skin="Metro"></telerik:RadButton></div>
                    </div>
                </asp:PlaceHolder>
            </div>
    </div>
</div>