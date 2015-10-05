<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="campaign-list.aspx.cs" Inherits="manage_manage_contests" %>
<%@ Register Src="~/manage/spaces/controls/space-sidebar.ascx" TagPrefix="epg" TagName="spacesidebar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
    <epg:spacesidebar runat="server" ID="spacesidebar" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		        <h3>
			        <i class="glyphicon-global"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Campaigns"></asp:Label>
		        </h3>
	            </div>
	            <div class="box-content nopadding">
                    <h5>Click on campaign name to view detailed information<asp:Literal ID="litSpaceID" runat="server"></asp:Literal></h5>
                    <br />
                    <ul>
                        <asp:Literal ID="litList" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

