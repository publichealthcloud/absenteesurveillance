using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Quartz;

public partial class session : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        int sessionID = Convert.ToInt32(Context.Items["SessionID"]);
        int userID = Convert.ToInt32(Context.Items["UserID"]);
        int highestRank = Convert.ToInt32(Context.Items["highestRank"]);
        string highestRole = Convert.ToString(Context.Items["highestRole"]);
        
        if (sessionID != null)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand com = new SqlCommand("qPtl_UpdateSession", con);
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] paramsToStore = new SqlParameter[1];

            paramsToStore[0] = new SqlParameter("@SessionID", SqlDbType.Int);
            paramsToStore[0].Value = sessionID;

            for (int i = 0; i < paramsToStore.Length; i++)
            {
                com.Parameters.Add(paramsToStore[i]);
            }

            com.ExecuteNonQuery();

            if (userID != null && int.TryParse(userID.ToString(), out userID))
            {
                Quartz.qPtl_Users user = new Quartz.qPtl_Users(userID);
                string roles = string.Empty;
                if (user.roles.Contains("Admin"))
                {
                    roles += " Admin";
                }
                if (user.roles.Contains("Host"))
                {
                    roles += " Host";
                }
                Session["UserRole"] = roles;
                Session["OrgUnit"] = user.orgUnitID;
                Session["ScopeID"] = user.scopeID;
            }

        }
        else
        {
            Response.Redirect("~/default.aspx");
        }
        */
    }
}
