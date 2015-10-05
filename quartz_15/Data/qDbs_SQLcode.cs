using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Quartz.Data
{
    public class qDbs_SQLcode
    {

        public DataTable GetDataTable(string query)
        {
            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
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
            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
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

        public SqlDataReader GetDataReader(string query)
        {
            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader myReader;

            SqlCommand cmd = new SqlCommand(query, conn);
            myReader = cmd.ExecuteReader();
            return myReader;
        }

        public String GetSingleValue(string query)
        {
            string returnedValue = string.Empty;

            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection sqlConn = new SqlConnection(connString);

            try
            {
                sqlConn.Open();

                SqlCommand sqlCom = new SqlCommand(query, sqlConn);
                SqlDataReader qReader = sqlCom.ExecuteReader(CommandBehavior.CloseConnection);

                while (qReader.Read())
                {
                    returnedValue = Convert.ToString(qReader[0]);
                }

                qReader.Close();
            }
            catch (Exception excp)
            {
                return excp.Message;
            }
            finally
            {
                sqlConn.Close();
            }

            return returnedValue;
        }

        public string ExecuteSQL(string query)
        {
            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
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

        public DataTable ShuffleAndReduce(DataTable i_table, int num_final_rows)
        {
            DataTable clone = i_table.Clone();
            int i_num_rows = i_table.Rows.Count;
            int diff_num_rows = 0;
            if (num_final_rows > 0)
                diff_num_rows = i_num_rows - num_final_rows;

            if (i_table.Rows.Count > diff_num_rows)
            {
                Random random = new Random();

                while (i_table.Rows.Count > 0)
                {
                    int row = random.Next(0, i_table.Rows.Count);
                    clone.ImportRow(i_table.Rows[row]);
                    i_table.Rows[row].Delete();
                }
            }
            return clone;
        }
    }
}
