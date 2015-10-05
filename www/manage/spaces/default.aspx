<%@ Page Title="" Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="manage_default" %>
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
						<ul class="tabs tabs-inline tabs-top">
							<li class='active'>
								<a href="#active" data-toggle='tab'>Active Campaigns</a>
							</li>
							<li>
								<a href="#archived" data-toggle='tab'>Archived Campaigns</a>
							</li>
						</ul>
						<div class="tab-content padding tab-content-inline tab-content-bottom">
							<div class="tab-pane active" id="active">
                                <asp:Literal ID="litActiveCampaigns" runat="server"></asp:Literal>
							</div>
							<div class="tab-pane" id="archived">
                                <asp:Literal ID="litArchivedCampaigns" runat="server"></asp:Literal>
							</div>
						</div>
					</div>
                </div>
            </div>
        </div>
</asp:Content>

