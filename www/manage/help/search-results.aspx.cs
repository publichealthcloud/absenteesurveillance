using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Help;

public partial class qHlp_search_results : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Query"]))
            {
                repSearchResults.DataSource = qHlp_HelpTopic.GetHelpTopicsByKeyword(Request.QueryString["Query"]);
                repSearchResults.DataBind();

                lblMessage.Text = repSearchResults.Items.Count + " results for: <strong>" + Request.QueryString["Query"] + "</strong>";

                if (repSearchResults.Items.Count > 0)
                {
                    plhSearchResults.Visible = true;
                    plhNoSearchResults.Visible = false;
                }
                else
                {
                    plhSearchResults.Visible = false;
                    plhNoSearchResults.Visible = true;
                    lblNoResults.Text = "No results were found";
                }
            }      
        }
    }

}
