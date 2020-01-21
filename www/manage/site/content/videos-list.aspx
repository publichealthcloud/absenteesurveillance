<%@ Page Language="C#" MasterPageFile="~/manage/manage.master" AutoEventWireup="true" CodeFile="videos-list.aspx.cs" Inherits="qSoc_video_list" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="side_nav" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="main_features" Runat="Server">
<div class="box">
	<div class="box-title">
		<h3>
			<i class="icon-edit"></i>
			<asp:Label ID="lblTitle" runat="server" Text="Videos">Videos</asp:Label>
		</h3>
        <ul class="tabs">
            <li runat="server" id="liShare">
                <div class="btn-group">
			        <a href="/manage/site/content/video-upload.aspx" class="btn btn-primary"><i class="glyphicon-circle_plus"></i> ADD VIDEO</a>
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <a href="/manage/site/content/videos-list.aspx" class="btn"><i class="icon-refresh"></i> Refresh</a> 
		        </div>
            </li>
            <li>
                <div class="btn-group">
                    <asp:LinkButton ID="btnDownloadExcel" CssClass="btn" runat="server" OnClick="btnDownloadExcel_Click"><i class="icon-cloud-download"></i> Download Excel</asp:LinkButton>
		        </div>
            </li>
		</ul>
	</div>
        <telerik:RadGrid ID="RadGrid1" DataSourceID="siteVideos" runat="server" PageSize="50" CaseSensitive="false" GroupingSettings-CaseSensitive="false" AllowSorting="true"
            AllowPaging="True" ShowGroupPanel="True" AutoGenerateColumns="False" AllowFilteringByColumn="true" PagerStyle-Mode="NextPrevNumericAndAdvanced" PagerStyle-Position="TopAndBottom"
            GridLines="None" onitemcommand="RadGrid1_ItemCommand">
            <MasterTableView Width="100%" GroupLoadMode="Client" TableLayout="Fixed">
            <Columns>
                <telerik:GridTemplateColumn DataField="LastName" HeaderText="Manage" AllowFiltering="false" HeaderStyle-Width="25" ItemStyle-Width="25">
                    <ItemTemplate>
                        <a href="video-edit.aspx?videoID=<%# DataBinder.Eval(Container.DataItem, "VideoID") %>" class="btn btn-primary" rel="tooltip" title="Manage"><i class="icon-pencil"></i></a> 
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="ID" HeaderButtonType="TextButton" AutoPostBackOnFilter="true" FilterControlWidth="13"
                    DataField="VideoID" HeaderStyle-Width="35" ItemStyle-Width="35" AllowSorting="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Title" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Title" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Description" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Description" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn DataField="ViddlerID" HeaderText="Watch Now" UniqueName="Title" HeaderStyle-Width="140" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:PlaceHolder ID="plhExternal" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "Source") == "external") %>'>
                            <center>
                            <a href="edit-video.aspx?videoID=<%# DataBinder.Eval(Container.DataItem, "VideoID") %>">
                                <img alt="Video Thumbnail" src="<%# DataBinder.Eval(Container.DataItem, "ThumbnailURL") %>?maxwidth=125"><br>External video--click to watch.
                            </a>
                            </center>
                        </asp:Placeholder>
                        <asp:Placeholder ID="plhEmbed" runat="server" Visible='<%# ((string)DataBinder.Eval(Container.DataItem, "Source") == "internal") %>'>
                            <!--[if IE]><object width="247" height="139" id="viddlerOuter-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
                            <param name="movie" value="//www.viddler.com/mini/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/">
                            <param name="allowScriptAccess" value="always">
                            <param name="allowNetworking" value="all"><param name="allowFullScreen" value="true"><param name="flashVars" value="f=1&openURL=95817547&autoplay=f&loop=0&nologo=0&hd=0">
                            <object id="viddlerInner-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>"><video id="viddlerVideo-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>" src="//www.viddler.com/file/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/html5mobile?openURL=95817547" type="video/mp4" width="247" height="139" poster="//www.viddler.com/thumbnail/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/" controls="controls" x-webkit-airplay="allow"></video></object></object><![endif]--> <!--[if !IE]> <!--> 
                        <object width="247" height="139" id="viddlerOuter-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>" type="application/x-shockwave-flash" data="//www.viddler.com/mini/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/"> <param name="movie" value="//www.viddler.com/mini/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/"> <param name="allowScriptAccess" value="always"><param name="allowNetworking" value="all"><param name="allowFullScreen" value="true"><param name="flashVars" value="f=1&openURL=95817547&autoplay=f&loop=0&nologo=0&hd=0"><object id="viddlerInner-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>"> <video id="viddlerVideo-<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>" src="//www.viddler.com/file/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/html5mobile?openURL=95817547" type="video/mp4" width="247" height="139" poster="//www.viddler.com/thumbnail/<%# DataBinder.Eval(Container.DataItem, "ViddlerID") %>/" controls="controls" x-webkit-airplay="allow"></video> </object></object> <!--<![endif]-->
                        </asp:Placeholder>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn UniqueName="Created" DataField="Created" HeaderText="Created" AllowSorting="true"
                        HeaderStyle-Width="100px">
                        <FilterTemplate>
                            From
                            <telerik:RadDatePicker ID="FromDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="FromDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# startDate %>' />
                            <br />To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <telerik:RadDatePicker ID="ToDatePicker" runat="server" Width="100px" ClientEvents-OnDateSelected="ToDateSelected"
                                MinDate='<%# minDate %>' MaxDate="1/1/2025" DbSelectedDate='<%# endDate %>' />
                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                                <script type="text/javascript">
                                    function FromDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var ToPicker = $find('<%# ((GridItem)Container).FindControl("ToDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(sender);
                                        var toDate = FormatSelectedDate(ToPicker);

                                        tableView.filter("Created", fromDate + " " + toDate, "Between");

                                    }
                                    function ToDateSelected(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        var FromPicker = $find('<%# ((GridItem)Container).FindControl("FromDatePicker").ClientID %>');

                                        var fromDate = FormatSelectedDate(FromPicker);
                                        var toDate = FormatSelectedDate(sender);

                                        tableView.filter("Created", fromDate + " " + toDate, "Between");
                                    }
                                    function FormatSelectedDate(picker) {
                                        var date = picker.get_selectedDate();
                                        var dateInput = picker.get_dateInput();
                                        var formattedDate = dateInput.get_dateFormatInfo().FormatDate(date, dateInput.get_displayDateFormat());

                                        return formattedDate;
                                    }
                                </script>

                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Highlighted Video" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="HighlightedWF" HeaderStyle-Width="75" ItemStyle-Width="75" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Visible On Site" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="Available" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Theme" HeaderButtonType="TextButton" AutoPostBackOnFilter="true"
                    DataField="ThemeName" HeaderStyle-Width="50" ItemStyle-Width="50" AllowSorting="true" FilterControlWidth="50">
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
                        <ClientSettings AllowGroupExpandCollapse="True" ReorderColumnsOnClient="True" AllowDragToGroup="True"
                AllowColumnsReorder="True">
            </ClientSettings>
             <GroupingSettings ShowUnGroupButton="true" />
        <FilterMenu EnableTheming="True">
            <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        </FilterMenu>
    </telerik:RadGrid>
        <asp:SqlDataSource ID="siteVideos" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="System.Data.SqlClient" 
            runat="server"></asp:SqlDataSource>
</asp:Content>
