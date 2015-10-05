<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="assessment-editor.aspx.cs" Inherits="assessment_editor" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/manage/site/learning/controls/AssessmentEditor.ascx" TagPrefix="epg" TagName="AssessmentEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
    <epg:AssessmentEditor runat="server" id="AssessmentEditor" />    
</div>
</asp:Content>
