using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace Quartz.Core
{
    public class q_SQL
    {
        public q_SQL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetDataTable(string query)
        {
            String connString = ConfigurationManager.AppSettings["CookiesDomain"];
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable myDataTable = new DataTable();

            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }

            return myDataTable;
        }

        public DataSet GetDataSet(string query)
        {
            String connString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataSet myDataSet = new DataSet();

            conn.Open();
            try
            {
                adapter.Fill(myDataSet);
            }
            finally
            {
                conn.Close();
            }

            return myDataSet;
        }

        public string ExecuteSQL(string query)
        {
            String connString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection sqlConn = new SqlConnection(connString);

            try
            {
                sqlConn.Open();

                SqlCommand sqlCom = new SqlCommand(query);
                sqlCom.Connection = sqlConn;
                sqlCom.ExecuteNonQuery();
            }
            catch (Exception excp)
            {
                return excp.Message;
            }
            finally
            {
                sqlConn.Close();
            }
            return "";
        }
    }
}
