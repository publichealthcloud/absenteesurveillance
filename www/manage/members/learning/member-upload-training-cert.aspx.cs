using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Portal;

public partial class qLrn_member_upload_training_cert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        int userID = Convert.ToInt32(Request.QueryString["userID"]);

        qPtl_User user = new qPtl_User(userID); string wwwURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CmsBasePath"]);
        string key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["automation_key"]);

        Response.Redirect(wwwURL + "public/upload-training-cert.aspx?userID=" + user.UserID + "&key=" + key + "&redirectURL=" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["public_url"]) + "qLrn/member-training-certs.aspx?userID=" + user.UserID);
    }
}