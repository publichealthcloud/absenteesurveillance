<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DailySummaryCharts.ascx.cs" Inherits="manage_school_districts_reports_controls_TypesIlnessesPieChart" %>

<asp:Literal ID="litJSLoad" runat="server"></asp:Literal>
<asp:Literal ID="litJSOpening" runat="server"></asp:Literal>
<asp:Literal ID="litJSChart" runat="server"></asp:Literal>
<asp:Literal ID="litJSClosing" runat="server"></asp:Literal>

<table class="table table-hover table-nomargin">
	<tbody>
		<tr>
			<td><asp:Literal ID="litAbsenteeRatesChartGoogle" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<table class="table table-hover table-nomargin">
	<tbody>
		<tr>
			<td><asp:Literal ID="litTypesOfSymptomsChartGoogle" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>