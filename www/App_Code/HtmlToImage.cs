using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Diagnostics;

public class HtmlToImageConverterOptions
{
    public int CropWidth { get; set; }
    public int CropHeight { get; set; }
    public int Quality { get; set; }
    public string CookieName { get; set; }
    public string CookieValue { get; set; }
}

/// <summary>
/// Converts HTML to Images
/// </summary>
///
public class HtmlToImageConverter
{
	public HtmlToImageConverter()
	{
	}

    public void ConvertFromUrl (string url, string file_out, HtmlToImageConverterOptions options)
    {
        string converter_path = HttpContext.Current.Server.MapPath ("~/bin/wkhtmltopdf/wkhtmltoimage.exe");

        string param_options = null;

        if (options != null)
        {
            StringBuilder sb_params = new StringBuilder();

            if (options.CropWidth > 0) sb_params.Append(" --crop-w ").Append(options.CropWidth);
            if (options.CropHeight > 0) sb_params.Append(" --crop-h ").Append(options.CropHeight);
            if (options.Quality > 0) sb_params.Append(" --quality ").Append(options.Quality);
            if (!string.IsNullOrEmpty(options.CookieName)) sb_params.Append(" --cookie ").Append(options.CookieName).Append(' ').Append(options.CookieValue);

            param_options = sb_params.ToString();
        }

        ProcessStartInfo psi = new ProcessStartInfo(converter_path, string.Format("{0} \"{1}\" \"{2}\"", param_options.Trim(), url, file_out));

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;

        Process proc = new Process();
        proc.StartInfo = psi;
        proc.Start();

        proc.WaitForExit();
    }
}