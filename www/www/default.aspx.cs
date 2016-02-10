using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Social;

public partial class www_lms_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            qSoc_Element title = new qSoc_Element("homepage-title");
            string title_html = title.HTML;
            litIntroTitle.Text = title_html;

            qSoc_Element intro = new qSoc_Element("homepage-intro-text");
            string intro_html = intro.HTML;
            litIntroText.Text = intro_html;

            bool register_disabled = false;
            if (!String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["Register_RegisterDisabled"]))
                register_disabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["Register_RegisterDisabled"]);

            if (register_disabled == true)
                plhRegisterNowLink.Visible = false;
        }
    }
}