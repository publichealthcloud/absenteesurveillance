using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class learning_printing_print_options : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            rbl_options.SelectedValue = "Training";
        }
    }

    protected void lb_print_OnClick(object sender, EventArgs args)
    {
        int training_id = Convert.ToInt32(Request.QueryString["TrainingID"]);
        int slide_id = Convert.ToInt32(Request.QueryString["SlideID"]);
        bool print_slide_only = rbl_options.SelectedValue == "Slide";
        bool include_notebook = cb_include_notebook.Checked;

        var sb_parameters = new StringBuilder();

        sb_parameters.AppendFormat("TrainingID={0}", training_id);
        if (slide_id > 0 && print_slide_only) sb_parameters.AppendFormat("&SlideID={0}", slide_id);
        if (include_notebook) sb_parameters.Append("&IncludeNotebook=true");

        Page.Header.Controls.Add (new LiteralControl (string.Format ("<meta http-equiv=\"refresh\" content=\"2;url={0}?{1}\">",
            ResolveUrl ("~/social/learning/printing/download-training-pdf.aspx"),
            sb_parameters.ToString ())));

        lit_message_log.Text = "Your download will begin in a few seconds.";
    }
}