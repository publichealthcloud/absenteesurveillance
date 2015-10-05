using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Quartz;
using Quartz.Portal;
using Quartz.Communication;
using Quartz.Data;

public partial class manage_communications_email_test_send : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int emailID = Convert.ToInt32(Request.QueryString["emailID"]);
        
            // create object
            qCom_EmailTool email = new qCom_EmailTool(emailID);

            qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));
            txtEmail.Text = user.Email;

            lblSubject.Text = email.emailSubject;

            // get headers and footers
            string query = "SELECT TOP(1) * FROM qCom_EmailPreferences WHERE Available = 'Yes'";
            qDbs_SQLcode sql = new qDbs_SQLcode();
            SqlDataReader eReader = sql.GetDataReader(query);

            eReader.Read();
            string header = Convert.ToString(eReader["Header"]);
            string footer = Convert.ToString(eReader["Footer"]);
            string unsubscribe = Convert.ToString(eReader["Unsubscribe"]);
            eReader.Close();

            // if email type is bulk, then add footer
            string completeMessage = header + email.emailDraft + footer;
            if (email.emailType == "bulk")
            {
                completeMessage += unsubscribe;
            }

            lblBody.Text = completeMessage;
        }
    }

    protected void sendNow(){
        int emailID = Convert.ToInt32(Request.QueryString["emailID"]);
        
        // create object
        qCom_EmailTool email = new qCom_EmailTool(emailID);

        qPtl_User user = new qPtl_User(Convert.ToInt32(Context.Items["UserID"]));

        email.SendDatabaseMail(txtEmail.Text, Convert.ToInt32(Request.QueryString["emailID"]), user.UserID, user.UserName, txtValue1.Text, txtValue2.Text, txtValue3.Text, txtValue4.Text, false);

        lblMessage.Text = "* Email Successfully Sent";
        lblMessage.Visible = true;
    }

    protected void btnSendTest_Click(object sender, EventArgs e)
    {
        sendNow();
    }
}
