<%@ Page Title="" Language="C#" MasterPageFile="~/simple.master" AutoEventWireup="true" CodeFile="search-results.aspx.cs" Inherits="qHlp_search_results" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content_main" Runat="Server">

<link href="../quartz.css" rel="stylesheet" type="text/css" />

<div style="padding-left:10px;">
    <br />
    <span><strong>&nbsp;<asp:Label ID="lblTitle" CssClass="CapsHeader3" runat="server" Text="Search Results"></asp:Label></strong></span>
    <table cellpadding="5" width="550px">
        <tr>
            <td width="99%" class="NormalDarkGrayItalics">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
        <asp:PlaceHolder ID="plhSearchResults" runat="server">
            <tr>
                <td width="99%" class="Normal">
                    <div style="height:25px;"></div>
                     <asp:Repeater ID="repSearchResults" runat="server">
                        <ItemTemplate>
                            <a href="help-viewer.aspx?topic=<%# DataBinder.Eval(Container.DataItem, "Title") %>"><strong><%# DataBinder.Eval(Container.DataItem, "Title") %></strong></a>
                            <br />
                            <div style="height:5px;"></div>
                            <span class="Normal"><%# DataBinder.Eval(Container.DataItem, "Summary") %></span>
                            <div style="height:25px;"></div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plhNoSearchResults" runat="server">
            <tr>
                <td width="99%" class="Normal">
                    <div style="height:25px;"></div>
                     <asp:Label ID="lblNoResults" runat="server"></asp:Label>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</div>
        
</asp:Content>

