<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" MasterPageFile="~/manage/manage.master" CodeFile="warning-edit.aspx.cs" Inherits="warning_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">

<div style="position: relative; left: 10px;">
    <div class="box">
	    <div class="box-title">
		    <h3>
			    <i class="icon-warning-sign"></i>
			    <asp:Label ID="lblTitle" runat="server" Text="Page Zones">Warnings</asp:Label>
		    </h3>
                <ul class="tabs">
                    <li runat="server" id="li1">
                        <div class="btn-group">
                            <asp:HyperLink ID="hplBackTop" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Warning List</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:HyperLink ID="hplRefreshTop" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <div class="btn-group">
                            <asp:Button ID="btnSave_top" CssClass="btn btn-primary" runat="server" Text="SAVE WARNING" onclick="btnSave_OnClick" />
                        </div>
                    </li>
                </ul>
                <asp:Label ID="lblMessage" CssClass="validation2" runat="server"></asp:Label>
        </div>
        <div style="height:10px;"></div>
        <table border="0" cellpadding="5" width="600">
        <asp:PlaceHolder ID="plhTools" runat="server">
        <tr>
            <td colspan="2" class="NormalBold" bgcolor="#EEE">
                <i class="icon-trash"></i>&nbsp; <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="return confirm('Are you sure you want to delete this warning? This action cannot be undone.');" OnClick="btnDelete_Click" Text="Delete Warning"></asp:LinkButton>
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2" class="NormalBold">
            <br /><br /><asp:Label ID="lblRequiredFields" runat="server" Text="* Indicates a required field"></asp:Label>
            <asp:PlaceHolder ID="plhValidation" runat="server">
                <blockquote>
                <asp:ValidationSummary runat="server" ID="vsRegister" DisplayMode="List" ValidationGroup="form" />
                </blockquote>
            </asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Warning Details
            </td>
            <td>
                <asp:Label ID="lblWarningType" CssClass="Normal" runat="server"></asp:Label>      
            </td>
        </tr>
        <asp:PlaceHolder ID="plhBannedWord" runat="server">
            <tr>
                <td width="150" class="NormalBold" valign="top">
                Content
                </td>
                <td>
                    <asp:Label ID="lblContent" CssClass="Normal" runat="server"></asp:Label>      
                </td>
            </tr>
            <tr>
                <td width="150" class="NormalBold" valign="top">
                Banned Words
                </td>
                <td>
                    <asp:Label ID="lblBannedWords" CssClass="Normal" runat="server"></asp:Label>      
                </td>
            </tr>
            <tr>
                <td width="150" class="NormalBold" valign="top">
                Full Text Banned Words Appear In 
                </td>
                <td>
                    <asp:Label ID="lblFullText" CssClass="Normal" runat="server"></asp:Label>      
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Issued To
            </td>
            <td class="Normal">
                <a target="_blank" href="/manage/members/member-profile.aspx?userID=<%= actor_id %>">
                <i class="icon-external-link"></i>&nbsp;<%= actor %></a>
                <asp:Label ID="lblPostedTime" CssClass="NormalItalics" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="150" class="NormalBold" valign="top">
            Review Status *
            </td>
            <td>
                <asp:DropDownList runat="server" ID="ddlStatus">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem>
                    <asp:ListItem Value="Reviewed" Text="Reviewed"></asp:ListItem>
                    <asp:ListItem Value="Escalated" Text="Escalated"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblReviewedBy" CssClass="NormalItalics" runat="server"></asp:Label>
                <asp:RequiredFieldValidator 
                    ID="rfvStatus" 
                    runat="server"
                    Text="*"
                    ValidationGroup="form" 
                    ControlToValidate="ddlStatus"
                    ErrorMessage="Status required">
                </asp:RequiredFieldValidator>   
            </td>
        </tr>
    </table>

    <div class="box">
	    <div class="box-title">
            <ul class="tabs">
                <li runat="server" id="liShare">
                    <div class="btn-group">  
                        <asp:HyperLink ID="hplBackBottom" CssClass="btn" runat="server"><i class="icon-circle-arrow-left"></i>&nbsp;Back to Warning List</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:HyperLink ID="hplRefreshBottom" CssClass="btn" runat="server"><i class="icon-refresh"></i>&nbsp;Refresh</asp:HyperLink>
                    </div>
                </li>
                <li>
                    <div class="btn-group">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="SAVE WARNING" onclick="btnSave_OnClick" />
                    </div>
                </li>
            </ul>
            <asp:Label ID="Label1" CssClass="validation2" runat="server"></asp:Label>
        </div>
    </div>

    </div>
</div>
</asp:Content>


