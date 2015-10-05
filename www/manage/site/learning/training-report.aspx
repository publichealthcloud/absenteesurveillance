<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="training-report.aspx.cs" Inherits="report_training" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/TrainingDashboard.ascx" TagPrefix="epg" TagName="TrainingDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-bar-chart"></i>
			<asp:Literal ID="litTitle" runat="server" Text="Page Zones"></asp:Literal>
		</h3>
            <ul class="tabs">
                <li>
                    <div class="btn-group">
                        <asp:Literal ID="litBtnManageEnrolled" runat="server"></asp:Literal>
                    </div>
                    <div class="btn-group">
                        <asp:Literal ID="litBtnManageTraining" runat="server"></asp:Literal>
                    </div>
                </li>
            </ul>
            <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    <epg:TrainingDashboard runat="server" ID="TrainingDashboard" />

</asp:Content>


