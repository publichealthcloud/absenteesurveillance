using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reports_GoogleChartReferece : System.Web.UI.UserControl
{
    protected static string google_api_key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_GoogleAPIKey"]);

    public string GoogleAPIKey
    {
        get { return google_api_key; }
        set { google_api_key = value; }
    }  

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}