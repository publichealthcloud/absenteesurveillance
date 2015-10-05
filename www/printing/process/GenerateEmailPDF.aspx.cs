using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using Quartz.Portal;

public partial class GenerateEmailPDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["htmlSource"]) && !string.IsNullOrEmpty(Request.QueryString["pdfOutput"]))
        {
            string host_url = Request.QueryString["htmlSource"];
            string file_out = string.Format("{0}{1}", Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_PdfOutput"])), Request.QueryString["pdfOutput"]);

            var authentication_cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            var sb_parameters = new StringBuilder();

            HtmlToPdfConverter converter = new HtmlToPdfConverter();

            converter.ConvertFromUrl(host_url,
                                     file_out,
                                     new HtmlToPdfConverterOptions
                                     {
                                         Orientation = "portrait",
                                         CookieName = authentication_cookie.Name,
                                         CookieValue = authentication_cookie.Value
                                     });


            // EMAIL PDF
            int email_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Members_InvitationEmailID"]);
            string email_address = Convert.ToString(Request.QueryString["emailAddress"]);

            Quartz.Communication.qCom_EmailTool email = new Quartz.Communication.qCom_EmailTool(email_id);

            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            email.SendDatabaseMailWithAttachment(email_address, email_id, user.UserID, "", "", "", file_out);

            string message = "* Email Successfully Sent";


            // DELETE FILE
            //File.Delete(file_out);


            // REDIRECT
            if (!String.IsNullOrEmpty(Request.QueryString["returnURL"]))
            {
                //Session.Abandon();
                //FormsAuthentication.SignOut();
                Response.Redirect(Request.QueryString["returnURL"]);
            }
            else
                Response.Redirect("~/qPtl/invitation-email.aspx?message=" + message + "&file=" + file_out);

            /*
            Response.ContentType = string.Format("application/{0}", Path.GetExtension(file_out).Trim('.'));
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", Path.GetFileName(file_out)));
            Response.TransmitFile(file_out);
            Response.Flush();

            File.Delete(file_out);
            */

        }
    }
}
