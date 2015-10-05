using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class qLrn_generate_cert_printout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int user_id = Convert.ToInt32(Context.Items["UserID"]);
            int training_id = Convert.ToInt32(Request.QueryString["TrainingID"]);

            string file_out = string.Format("{0}\\user_id{1}_training_id{2}.pdf", Server.MapPath("temp"), user_id, training_id);

            var authentication_cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            var sb_parameters = new StringBuilder();

            sb_parameters.AppendFormat("TrainingID={0}", training_id);

            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            string host_url = string.Format("/social/learning/printing/generate-cert-printout.aspx?"+ sb_parameters.ToString());

            converter.ConvertFromUrl(host_url,
                                     file_out,
                                     new HtmlToPdfConverterOptions
                                     {
                                         Orientation = "landscape",
                                         CookieName = authentication_cookie.Name,
                                         CookieValue = authentication_cookie.Value
                                     });

            if (File.Exists(file_out))
            {
                var file_info = new FileInfo(file_out);

                Response.ContentType = string.Format("application/{0}", Path.GetExtension(file_out).Trim('.'));
                Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", Path.GetFileName(file_out)));
                Response.AppendHeader("Content-Length", file_info.Length.ToString());
                Response.TransmitFile(file_out);
                Response.End();

                File.Delete(file_out);
            }
        }
    }
}