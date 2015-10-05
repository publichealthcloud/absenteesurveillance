using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Telerik.Web.UI;
using Telerik.Web;

using Quartz.Portal;

public partial class redirects_list : System.Web.UI.Page
{
    public string base_path = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

    protected void Page_Load(object sender, EventArgs e)
    {
        int curr_user_id = Convert.ToInt32(Context.Items["UserID"]);

        qPtl_User user = new qPtl_User(curr_user_id);
        
        siteRedirects.SelectCommand = "SELECT * FROM qPtl_Redirects WHERE MarkAsDelete = 0 AND Created > '" + System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"] + "' ORDER BY EntryURL ASC";

        if (!Page.IsPostBack)
        {
        }
    }

    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        RadGrid1.ExportSettings.ExportOnlyData = true;
        RadGrid1.ExportSettings.IgnorePaging = true;
        RadGrid1.ExportSettings.OpenInNewWindow = true;
        RadGrid1.ExportSettings.FileName = "Redirects_" + DateTime.Now;
        RadGrid1.MasterTableView.ExportToExcel();
    } 
}
