using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Quartz.Data
{
    public class qDbs_Config
    {

        public SqlDataReader GetDataGroupInfo(int dataGroupID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataGroupInfo", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataGroupID = new SqlParameter("@DataGroupID", SqlDbType.Int, 4);
            parameterDataGroupID.Value = dataGroupID;
            myCommand.Parameters.Add(parameterDataGroupID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetDataFormInfo(int dataFormID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataFormInfo", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataFormID = new SqlParameter("@DataFormID", SqlDbType.Int, 4);
            parameterDataFormID.Value = dataFormID;
            myCommand.Parameters.Add(parameterDataFormID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetSelectInfo(int dataGroupID, string tableColumn)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataElementSelectInfo", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataGroupID = new SqlParameter("@DataGroupID", SqlDbType.Int, 4);
            parameterDataGroupID.Value = dataGroupID;
            myCommand.Parameters.Add(parameterDataGroupID);

            SqlParameter parameterTableColumn = new SqlParameter("@TableColumn", SqlDbType.VarChar, 50);
            parameterTableColumn.Value = tableColumn;
            myCommand.Parameters.Add(parameterTableColumn);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetDataForms(int dataGroupID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataForms", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataGroupID = new SqlParameter("@DataGroupID", SqlDbType.Int, 4);
            parameterDataGroupID.Value = dataGroupID;
            myCommand.Parameters.Add(parameterDataGroupID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public DataTable GetDataGroupsDDL()
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataGroups", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(myCommand);
            DataSet dsRecords = new DataSet();
            adapter.Fill(dsRecords, "Records");

            DataSet dsRecordsDDL = new DataSet();
            DataTable dtRecordsDDL = new DataTable("RecordsDDL");

            dtRecordsDDL.Columns.Add("RecordID", typeof(int));
            dtRecordsDDL.Columns.Add("DisplayName", typeof(string));

            dsRecordsDDL.Tables.Add(dtRecordsDDL);


            DataRow drRecordsDDL = dtRecordsDDL.NewRow();
            drRecordsDDL[0] = 0;
            drRecordsDDL[1] = "";
            dtRecordsDDL.Rows.Add(drRecordsDDL);

            foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
            {
                DataRow drRecordsDDL2 = dtRecordsDDL.NewRow();
                drRecordsDDL2[0] = theRow["DataGroupID"];
                drRecordsDDL2[1] = theRow["SelectSource"];
                dtRecordsDDL.Rows.Add(drRecordsDDL2);
            }

            return dtRecordsDDL;
        }

        public SqlDataReader GetDataGroupInfoByName(string dataGroupName)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataGroupInfoByName", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataGroupName = new SqlParameter("@DataGroupName", SqlDbType.VarChar, 50);
            parameterDataGroupName.Value = dataGroupName;
            myCommand.Parameters.Add(parameterDataGroupName);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetDataElements(int dataGroupID, int dataFormID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetDataElements", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterDataGroupID = new SqlParameter("@DataGroupID", SqlDbType.Int, 4);
            parameterDataGroupID.Value = dataGroupID;
            myCommand.Parameters.Add(parameterDataGroupID);

            SqlParameter parameterDataFormID = new SqlParameter("@DataFormID", SqlDbType.Int, 4);
            parameterDataFormID.Value = dataFormID;
            myCommand.Parameters.Add(parameterDataFormID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetWizard(int wizardID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetWizard", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterWizardID = new SqlParameter("@WizardID", SqlDbType.Int, 4);
            parameterWizardID.Value = wizardID;
            myCommand.Parameters.Add(parameterWizardID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetWizardSteps(int wizardID)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetWizardSteps", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterWizardID = new SqlParameter("@WizardID", SqlDbType.Int, 4);
            parameterWizardID.Value = wizardID;
            myCommand.Parameters.Add(parameterWizardID);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

        public SqlDataReader GetWizardStepByNumber(int wizardID, int stepNumber)
        {
            SqlConnection myConnection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            SqlCommand myCommand = new SqlCommand("qDbs_GetWizardStepByNumber", myConnection);

            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterWizardID = new SqlParameter("@WizardID", SqlDbType.Int, 4);
            parameterWizardID.Value = wizardID;
            myCommand.Parameters.Add(parameterWizardID);

            SqlParameter parameterNumber = new SqlParameter("@Number", SqlDbType.Int, 4);
            parameterNumber.Value = stepNumber;
            myCommand.Parameters.Add(parameterNumber);

            myConnection.Open();
            SqlDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            return myReader;
        }

    }
}
