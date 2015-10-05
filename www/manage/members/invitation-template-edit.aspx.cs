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

using Telerik.Web.UI;

using Quartz;
using Quartz.Portal;
using Quartz.Learning;
using Quartz.CMS;

public partial class edit_article : System.Web.UI.Page
{
    public static string imageURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["site_imageLocation"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hplRefreshBottom.NavigateUrl = Request.Url.ToString();
            hplRefreshTop.NavigateUrl = Request.Url.ToString();

            // load styles for this project
            string css_text_file = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CmsTextCSS"]))
                css_text_file = Convert.ToString(ConfigurationManager.AppSettings["CmsTextCSS"]);

            reHeader.CssFiles.Add(css_text_file);
            reFooter.CssFiles.Add(css_text_file);

            qPtl_InvitationTemplate template = new qPtl_InvitationTemplate(Convert.ToInt32(Context.Items["ScopeID"]));

            if (template != null)
            {
                if (template.InvitationTemplateID > 0)
                {
                    reHeader.Content = template.Header;
                    reFooter.Content = template.Footer;
                }
            }

            // configure print link
            string baseURL = ConfigurationManager.AppSettings["returnURL"];
            string rawURL = string.Empty;

            rawURL = baseURL + "/qDbs/print/print-individual-invitations.aspx?invitationID=-1";      // example invitation

            string passURL = Server.UrlEncode(rawURL);
            string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);
            hplPrint.NavigateUrl = "~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=example_invitation_UserID_" + Context.Items["UserID"] + "_" + timeStamp + ".pdf";
            hplPrint.Target = "_blank";
        }
    }

    protected void btnSave_OnClick(object sender, System.EventArgs e)
    {
        int user_id = Convert.ToInt32(Context.Items["UserID"]);
        int scope_id = Convert.ToInt32(Context.Items["ScopeID"]);

        qPtl_InvitationTemplate template = qPtl_InvitationTemplate.GetTemplateByScopeID(scope_id);
        template.Created = template.Created;
        template.Header = reHeader.Content;
        template.Footer = reFooter.Content;
        template.LastModified = DateTime.Now;
        template.LastModifiedBy = user_id;
        template.Update();

        lblMessage.Text = "*** Record Successfully Updated ***";
        lblMessageBottom.Text = "*** Record Successfully Updated ***"; 
    }

    protected void btnPrintCustomForm_Click(object sender, EventArgs e)
    {
        //currently disabled -- we are using an open in a new window approach here
        string baseURL = ConfigurationManager.AppSettings["returnURL"];
        string rawURL = string.Empty;

        rawURL = baseURL + "/qDbs/print/print-individual-invitations.aspx?invitationID=-1";      // example invitation

        string passURL = Server.UrlEncode(rawURL);
        string timeStamp = Convert.ToString(DateTime.Now.Month) + "-" + Convert.ToString(DateTime.Now.Day) + "-" + Convert.ToString(DateTime.Now.Year);
        Response.Redirect("~/qDbs/GeneratePDF.aspx?PageOrientation=landscape&htmlSource=" + passURL + "&mode=read&pdfOutput=example_invitation_UserID_" + Context.Items["UserID"] + "_" + timeStamp + ".pdf");
    }
}
