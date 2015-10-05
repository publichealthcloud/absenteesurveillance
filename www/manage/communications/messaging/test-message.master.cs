using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;

public partial class text_messages_test_message : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            string curr_url = Request.Url.ToString();

            if (curr_url.Contains("members"))
                tabMainMenu.FindTabByValue("members").Selected = true;
            else if (curr_url.Contains("calendar"))
                tabMainMenu.FindTabByValue("calendar").Selected = true;
            else if (curr_url.Contains("dashboard"))
                tabMainMenu.FindTabByValue("dashboard").Selected = true;
            else if (curr_url.Contains("messages"))
                tabMainMenu.FindTabByValue("messages").Selected = true;
            else if (curr_url.Contains("programs"))
                tabMainMenu.FindTabByValue("programs").Selected = true;
            else if (curr_url.Contains("tools"))
                tabMainMenu.FindTabByValue("tools").Selected = true;
            else if (curr_url.Contains("add-ons"))
                tabMainMenu.FindTabByValue("add-ons").Selected = true;
            else if (curr_url.Contains("account"))
                tabMainMenu.FindTabByValue("account").Selected = true;
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("create-message.aspx");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Response.Redirect("send-message.aspx");
    }
}
