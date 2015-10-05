using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace Quartz.Portal
{
    public class qPtl_OrgUnits
    {
        public string name, description;
        public int orgUnitID;

        public qPtl_OrgUnits()
        {
        }

        public qPtl_OrgUnits(int orgUnitID)
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter com = new SqlDataAdapter("qPtl_GetOrgUnitInfo", con);

            com.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter parameterOrgUnitID = new SqlParameter("@OrgUnitID", SqlDbType.Int, 4);
                parameterOrgUnitID.Value = orgUnitID;
                com.SelectCommand.Parameters.Add(parameterOrgUnitID);

                // Create and Fill the DataSet
                DataTable dt = new DataTable();
                com.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    this.orgUnitID = orgUnitID;
                    this.name = (string)dt.Rows[0]["Name"];
                    this.description = (string)dt.Rows[0]["Description"];
                }
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
        }

        public qPtl_OrgUnits(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public int AddOrgUnit()
        {
            // Create Instance of Connection and Command Object
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand com = new SqlCommand("qPtl_AddOrgUnit", con);

            //' Mark the Command as a SPROC
            com.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                parameterName.Value = this.name;
                com.Parameters.Add(parameterName);

                SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 500);
                parameterDescription.Value = this.description;
                com.Parameters.Add(parameterDescription);

                SqlParameter parameterOrgUnitID = new SqlParameter("@OrgUnitID", SqlDbType.Int, 4);
                parameterOrgUnitID.Direction = ParameterDirection.Output;
                com.Parameters.Add(parameterOrgUnitID);

                // Open the database connection and execute the command
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                if (parameterOrgUnitID.Value != System.DBNull.Value)
                {
                    this.orgUnitID = (int)parameterOrgUnitID.Value;
                }
                else
                    this.orgUnitID = -1;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }

            return orgUnitID;
        }

        public DataTable GetOrgUnits()
        {
            // Create Instance of Connection and Command Object
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlDataAdapter com = new SqlDataAdapter("qPtl_GetOrgUnits", con);

            // Mark the Command as a SPROC
            com.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Create and Fill the DataTable
            DataTable dt = new DataTable();
            try
            {
                com.Fill(dt);
            }
            finally
            {
                con.Dispose();
                con.Close();
            }

            // Return the DataSet
            return dt;
        }


        public void UpdateOrgUnit()
        {
            // Create Instance of Connection and Command Object
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand com = new SqlCommand("qPtl_UpdateOrgUnit", con);

            // Mark the Command as a SPROC
            com.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlParameter parameterOrgUnitID = new SqlParameter("@OrgUnitID", SqlDbType.Int, 4);
                parameterOrgUnitID.Value = orgUnitID;
                com.Parameters.Add(parameterOrgUnitID);

                SqlParameter parameterOrgUnitName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                parameterOrgUnitName.Value = name;
                com.Parameters.Add(parameterOrgUnitName);

                SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 500);
                parameterDescription.Value = description;
                com.Parameters.Add(parameterDescription);

                // Open the database connection and execute the command
                con.Open();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
        }

    }
}
