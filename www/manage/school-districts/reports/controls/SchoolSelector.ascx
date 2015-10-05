<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SchoolSelector.ascx.cs" Inherits="manage_school_districts_reports_controls_SchoolSelector" %>

<table>
    <tr>
        <td>
            <strong>Jump to School: </strong>
        </td>
        <td>
            <div class="control-group">
                <div class="controls">
		            <select name="s2" id="s2" class='select2-me input-xlarge' onchange="location = this.options[this.selectedIndex].value;">
                        <asp:Literal ID="litSelector" runat="server"></asp:Literal>
		            </select>
	            </div>
            </div>
        </td>
    </tr>
</table>