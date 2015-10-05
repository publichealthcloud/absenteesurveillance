<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="contest-details.aspx.cs" Inherits="manage_manage_contests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
    <div class="row-fluid">                           
        <div class="span9">
            <div class="box">
	            <div class="box-title">
		        <h3>
			        <i class="icon-trophy"></i>
			            <asp:Label ID="lblTitle" runat="server" Text="Manage Contests"></asp:Label>
		        </h3>
	            </div>
	            <div class="box-content nopadding">
                    <h5><asp:Literal ID="litSubtitle" runat="server"></asp:Literal></h5>
                    <br />
                    <ul>
                        <asp:Literal ID="litSubmissionList" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

