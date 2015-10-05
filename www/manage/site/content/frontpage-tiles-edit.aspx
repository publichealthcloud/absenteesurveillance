<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="frontpage-tiles-edit.aspx.cs" Inherits="frontpage_tiles_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/frontpage/controls/media/frontpage-tiles.ascx" TagPrefix="uc1" TagName="frontpagetiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="padding-left:10px;">

    <div class="box">
	    <div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Page Zones">Frontpage Tiles - Click on Any Tile to Edit</asp:Label>
		</h3>
        </div>
    </div>
    <div style="height:10px;"></div>

    <uc1:frontpagetiles runat="server" ID="frontpagetiles" />

<div class="box">
    <div class="box-title">
    </div>
</div>
</asp:Content>


