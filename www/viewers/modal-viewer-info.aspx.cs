using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;
using Quartz.Learning;

public partial class viewers_modal_viewer_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];

        if (!String.IsNullOrEmpty(type))
        {
            qSoc_Element html = new qSoc_Element(type);

            if (html != null)
            {
                if (html.ElementID > 0)
                    litBody.Text = html.HTML;
            }
        }

        if (String.IsNullOrEmpty(litBody.Text))
            litBody.Text = "Sorry, this page was not found";
    }
}