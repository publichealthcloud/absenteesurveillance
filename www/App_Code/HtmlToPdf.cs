using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Diagnostics;

public class HtmlToPdfConverterOptions
{
    public string Orientation { get; set; }
    public int PageWidth { get; set; }
    public int PageHeight { get; set; }
    public string PageSize { get; set; }
    public string CookieName { get; set; }
    public string CookieValue { get; set; }

}

/// <summary>
/// Converts HTML to Images
/// </summary>
///
public class HtmlToPdfConverter
{
	public HtmlToPdfConverter()
	{
	}

    public void ConvertFromUrl (string url, string file_out, HtmlToPdfConverterOptions options)
    {
        string converter_path = HttpContext.Current.Server.MapPath("~/bin/wkhtmltopdf/wkhtmltopdf.exe");

        string param_options = null;

        if (options != null)
        {
            StringBuilder sb_params = new StringBuilder();

            if (!string.IsNullOrEmpty(options.Orientation)) sb_params.Append(" --orientation ").Append(options.Orientation);
            if (options.PageWidth > 0) sb_params.Append(" --page-width ").Append(options.PageWidth);
            if (options.PageHeight > 0) sb_params.Append(" --page-height ").Append(options.PageHeight);
            if (!string.IsNullOrEmpty(options.PageSize)) sb_params.Append(" --page-size ").Append(options.PageSize);
            if (!string.IsNullOrEmpty(options.CookieName)) sb_params.Append(" --cookie ").Append(options.CookieName).Append(' ').Append(options.CookieValue);

            param_options = sb_params.ToString();
        }

        ProcessStartInfo psi = new ProcessStartInfo(converter_path, string.Format("{0} \"{1}\" \"{2}\"", param_options.Trim(), url, file_out));

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;

        Process proc = new Process ();
        proc.StartInfo = psi;
        proc.Start();
        proc.WaitForExit();

        //!= 0) throw new Exception(string.Format("Could not generate {0}", file_out));
    }
}