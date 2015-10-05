<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="member-content.aspx.cs" Inherits="manage_members_member_content" %>
<%@ Register Src="~/manage/members/controls/MemberNav.ascx" TagPrefix="epg" TagName="MemberNav" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div class="row-fluid"> 
    <div class="page-header">
        <div class="breadcrumbs">
			<ul>
				<li>
					<a href="/manage/members/member-list.aspx">Member List</a>
					<i class="icon-angle-right"></i>
				</li>
				<li>
					<a href="/manage/members/member-content.aspx?userID=<%= profile_id %>">Member Content</a>
				</li>
			</ul>
			<div class="close-bread">
				<a href="#"><i class="icon-remove"></i></a>
			</div>
		</div>
    </div>   
</div>
	<div class="box box-bordered box-color lightgrey">
		<div class="box-title">
			<epg:MemberNav runat="server" ID="MemberNav" />
		</div>
		<div class="box-content nopadding">
			<ul class="tabs tabs-inline tabs-top">
				<li <asp:Literal ID="lit1Class" runat="server"></asp:Literal>>
					<a href="#1" data-toggle='tab'><i class="icon-info-sign"></i> Overview</a>
				</li>
				<li <asp:Literal ID="lit2Class" runat="server"></asp:Literal>>
					<a href="#2" data-toggle='tab'><i class="icon-bold"></i> 2</a>
				</li>
				<li <asp:Literal ID="lit3Class" runat="server"></asp:Literal>>
					<a href="#3" data-toggle='tab'><i class="icon-user"></i> 3</a>
				</li>
				<li <asp:Literal ID="lit4Class" runat="server"></asp:Literal>>
					<a href="#4" data-toggle='tab'><i class="icon-camera"></i> 4</a>
				</li>
			</ul>
			<div class="tab-content padding tab-content-inline tab-content-bottom">
				<div <asp:Literal ID="litTab1Class" runat="server"></asp:Literal> id="1">
					<h4>1</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab1Message" runat="server"></asp:Label>	
                    <div class="row-fluid">
					    <div class="span12">
                            Tab 1 content
					    </div>
			        </div>
				</div>

				<div <asp:Literal ID="litTab2Class" runat="server"></asp:Literal> id="2">
                     <h4>Tab 2</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab2Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            Tab 2 content
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab3Class" runat="server"></asp:Literal> id="3">
                     <h4>Tab 3</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab3Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            Tab 3 content
					    </div>
			        </div>               
				</div>

				<div <asp:Literal ID="litTab4Class" runat="server"></asp:Literal> id="4">
                     <h4>Tab 4</h4>&nbsp;&nbsp;<asp:Label class="NormalRed" ID="lblTab4Message" runat="server"></asp:Label>
                     <div class="row-fluid">
					    <div class="span12">
                            Tab 4 content
					    </div>
			        </div>               
				</div>
			</div>
		</div>
	</div>

</asp:Content>

