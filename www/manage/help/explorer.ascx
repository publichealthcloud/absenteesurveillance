<%@ Control Language="C#" AutoEventWireup="true" CodeFile="explorer.ascx.cs" Inherits="qDbs_explorer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
    function client_node_clicking(sender, event_args) {
        var node = event_args.get_node();
        var topic = node.get_text();
        var url = "help-viewer.aspx?topic=" + topic;

        if (url && url != "#") {
            set_content_pane_url(url);
            show_loading_panel();
        }
    }
</script>

<telerik:RadTreeView ID="rtv_explorer" runat="server" Width="100%" OnClientNodeClicking="client_node_clicking"
    DataFieldID="HelpTopicID" DataFieldParentID="ParentTopicID" DataTextField="Title"
    DataSourceID="helpTopics">
    <DataBindings>
        <telerik:RadTreeNodeBinding Target="<%=ContentPaneClientID %>" Expanded="true"></telerik:RadTreeNodeBinding>
    </DataBindings>
</telerik:RadTreeView>

<asp:SqlDataSource runat="server" ID="helpTopics" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="System.Data.SqlClient"  SelectCommand="SELECT * From qHlp_HelpTopics ORDER BY TopicOrder ASC"></asp:SqlDataSource>