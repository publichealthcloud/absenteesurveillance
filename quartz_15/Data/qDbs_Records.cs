using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Quartz.Data
{
    public class qDbs_Records
    {
        public qDbs_Records()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlDataReader GetDataElementsByForm(int dataFormID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataElementsByForm", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataFormID = new SqlParameter("@DataFormID", SqlDbType.Int, 4);
            parameterDataFormID.Value = dataFormID;
            myCommand.Parameters.Add(parameterDataFormID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }        

        public SqlDataReader GetTempRecord(int tempRecordID, string parameterName, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName +"_GetRecordTemp", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterTempRecordID = new SqlParameter("@TempCandidateID", SqlDbType.Int, 4);
            parameterTempRecordID.Value = tempRecordID;
            myCommand.Parameters.Add(parameterTempRecordID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetRecord(int recordID, string parameterName, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName +"_GetRecord", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRecordID = new SqlParameter(parameterName, SqlDbType.Int, 4);
            parameterRecordID.Value = recordID;
            myCommand.Parameters.Add(parameterRecordID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetTempRecordByParentID(int recordID, string parameterName, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_GetRecordTempByParentID", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRecordID = new SqlParameter(parameterName, SqlDbType.Int, 4);
            parameterRecordID.Value = recordID;
            myCommand.Parameters.Add(parameterRecordID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public DataSet GetAllTempRecords(string available, int scope, string parameterName, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_GetAllRecordsTemp", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAvailable = new SqlParameter("@Available", SqlDbType.VarChar, 3);
            parameterAvailable.Value = available;
            myCommand.Parameters.Add(parameterAvailable);

            SqlParameter parameterScope = new SqlParameter("@Scope", SqlDbType.Int, 4);
            parameterScope.Value = scope;
            myCommand.Parameters.Add(parameterScope);

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "ContactsTemp");

            return ds;
        }

        public int ProcessPendingRecordsCheck(int recordID, string parameterName, string recordGroupName, string tempParameterName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_ProcessPendingRecordsCheck", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterRecordID = new SqlParameter(parameterName, SqlDbType.Int, 4);
            parameterRecordID.Value = recordID;
            myCommand.Parameters.Add(parameterRecordID);

            SqlParameter parameterTempRecordID = new SqlParameter(tempParameterName, SqlDbType.Int, 4);
            parameterTempRecordID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterTempRecordID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            if (parameterTempRecordID.Value != null && parameterTempRecordID.Value != System.DBNull.Value)
                return (int)parameterTempRecordID.Value;

            return 0;

        }

        public int AddRecord(string recordGroupName, string parameterName, int userID, int scope)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_AddRecord", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterScopeID = new SqlParameter("@ScopeID", SqlDbType.Int, 4);
            parameterScopeID.Value = scope;
            myCommand.Parameters.Add(parameterScopeID);

            SqlParameter parameterCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int, 4);
            if (userID == 0)
            {
                parameterCreatedBy.Value = System.Configuration.ConfigurationManager.AppSettings["DefaultUserID"];
            }
            else
            {
                parameterCreatedBy.Value = userID;
            }
            myCommand.Parameters.Add(parameterCreatedBy);

            SqlParameter parameterDateCreated = new SqlParameter("@DateCreated", SqlDbType.DateTime, 8);
            parameterDateCreated.Value = DateTime.Now;
            myCommand.Parameters.Add(parameterDateCreated);

            SqlParameter parameterDateLastModified = new SqlParameter("@DateLastModified", SqlDbType.DateTime, 8);
            parameterDateLastModified.Value = DateTime.Now;
            myCommand.Parameters.Add(parameterDateLastModified);

            SqlParameter parameterRecordID = new SqlParameter(parameterName, SqlDbType.Int, 4);
            parameterRecordID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterRecordID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            if (parameterRecordID.Value != null && parameterRecordID.Value != System.DBNull.Value)
                return (int)parameterRecordID.Value;

            return 0;

        }

        public DataSet GetAllRecords(int scope, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_GetAllRecords", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterScope = new SqlParameter("@Scope", SqlDbType.Int, 4);
            parameterScope.Value = scope;
            myCommand.Parameters.Add(parameterScope);

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "dsRecords");

            return ds;
        }

        public DataSet GetAvailableRecords(string available, int scope, string recordGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand(recordGroupName + "_GetAllAvailableRecords", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterAvailable = new SqlParameter("@Available", SqlDbType.VarChar, 3);
            parameterAvailable.Value = available;
            myCommand.Parameters.Add(parameterAvailable);

            SqlParameter parameterScope = new SqlParameter("@Scope", SqlDbType.Int, 4);
            parameterScope.Value = scope;
            myCommand.Parameters.Add(parameterScope);

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "dsRecords");

            return ds;
        }
    }
}
