<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionElementEdit.ascx.cs" Inherits="slide_modules_question_element_edit" %>
<blockquote>
<fieldset>
    <strong><asp:Literal ID="litChoiceLetter" runat="server">Question Choice</asp:Literal></strong><br />
    <div>
        <asp:TextBox runat="server" ID="txt_details" TextMode="MultiLine" Rows="2" Width="99%" />
    </div>
    <table>
        <tr>
            <td><asp:CheckBox CssClass="NormalBold" runat="server" ID="cb_is_correct" /></td>
            <td><asp:Label ID="lblCorrectText" runat="server"></asp:Label></td>
        </tr>
    </table>
</fieldset>
</blockquote>