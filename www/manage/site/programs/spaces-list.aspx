<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="spaces-list.aspx.cs" Inherits="manage_manage_contests" %>
<%@ Register Src="~/manage/spaces/controls/space-sidebar.ascx" TagPrefix="epg" TagName="spacesidebar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="row-fluid">                           
        <div class="span12">
            <div class="box">
	            <div class="box-title">
		        <h3>
			        <i class="glyphicon-posterous_spaces"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Programs"></asp:Label>
		        </h3>
	            </div>
	            <div class="box-content nopadding">
                    <h5>Click on program name to view detailed information</h5>
                    <br />
                    <ul>
                        <asp:Literal ID="litList" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

