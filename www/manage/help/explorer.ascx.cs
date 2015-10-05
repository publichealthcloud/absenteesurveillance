using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;

using Quartz.Data;
using Quartz.Help;
using Quartz.CMS;

public partial class qDbs_explorer : System.Web.UI.UserControl
{
    public RadPane Target { get; set; }
    public string ContentPaneClientID
    {
        get
        {
            return Convert.ToString(ViewState["content_panel_client_id"]);
        }
        set
        {
            ViewState["content_panel_client_id"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //rtv_explorer.G
        
        if (!Page.IsPostBack)
        {
            //buildTree();
        }
    }

    public void buildTree()
    {
        /*
        rtv_explorer.Nodes.Add(BuildSiteTree());
        
        rtv_explorer.ExpandAllNodes();
         */
    }

    /*
    public RadTreeNode BuildSiteTree()
    {
        string curr_folder_url = folder.URL;
        if (curr_folder_url == "/")
            curr_folder_url = "[Help Topics]"; 

        RadTreeNode folder_node = new RadTreeNode(curr_folder_url, Convert.ToString (folder.SiteFolderID));

        folder_node.NavigateUrl = "";
        folder_node.ImageUrl = "~/images/treeview/folder.gif";
        folder_node.ExpandedImageUrl = "~/images/treeview/folder_o.gif";
        folder_node.ContextMenuID = "mnu_folder";
       
        string folder_full_path = folder.GetFullPath();

        qHlp_HelpTopics topic = new qHlp_HelpTopics();
        //foreach (var page in folder.GetPages())
        foreach (var page in topic.Get
        {
            var page_node = new RadTreeNode(page.URL, Convert.ToString(page.SitePageID), string.Format ("~/qCms/load-cms-page.aspx?page_edit={0}{1}", folder_full_path, page.URL));

            page_node.ImageUrl = "~/images/treeview/aspx.gif";
            page_node.Target = ContentPaneClientID;
            page_node.ContextMenuID = "mnu_page";

            // see if should be selected
            string action = Convert.ToString(Request.QueryString["action"]);
            if (action == "page")
            {
                // select appropriate page
                string page_edit = Convert.ToString(Request.QueryString["page_edit"]);
                if (page_edit == folder_full_path + page.URL)
                    page_node.Selected = true;
            }

            folder_node.Nodes.Add(page_node);
        }

        foreach (var sub_folder in folder.GetSubFolders())
        {
            folder_node.Nodes.Add (BuildSiteTree(sub_folder));
        }

        return folder_node;
    }
    */

    public void refreshExplorer()
    {
        //buildTree();
        //rtv_explorer.CollapseAllNodes();
        //rtv_explorer.CollapseAllNodes();
    }
}
