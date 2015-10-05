using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Text.RegularExpressions;

using Quartz.Portal;
using Quartz.Social;
using Quartz.Data;
using Quartz.Help;
using Quartz.Core;
using Quartz.Organization;

public partial class manage_members_member_content : System.Web.UI.Page
{
    protected int profile_id;
    protected string required_indicator;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            profile_id = Convert.ToInt32(Request.QueryString["userID"]);

            qPtl_User profile = new qPtl_User(profile_id);

            string curr_tab = string.Empty;
            curr_tab = Request.QueryString["currTab"];
            lit1Class.Text = "";
            lit2Class.Text = "";
            lit3Class.Text = "";
            lit4Class.Text = "";
            litTab1Class.Text = "class=\"tab-pane\"";
            litTab2Class.Text = "class=\"tab-pane\"";
            litTab3Class.Text = "class=\"tab-pane\"";
            litTab4Class.Text = "class=\"tab-pane\"";
            if (curr_tab == "1")
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab1Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "2")
            {
                lit2Class.Text = "class='active'";
                litTab2Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab2Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "3")
            {
                lit3Class.Text = "class='active'";
                litTab3Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab3Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else if (curr_tab == "4")
            {
                lit4Class.Text = "class='active'";
                litTab4Class.Text = "class=\"tab-pane active\"";
                if (!String.IsNullOrEmpty(Request.QueryString["message"]))
                    lblTab4Message.Text = " *** " + Request.QueryString["message"] + "***";
            }
            else
            {
                lit1Class.Text = "class='active'";
                litTab1Class.Text = "class=\"tab-pane active\"";
            }            
        }
    }
}