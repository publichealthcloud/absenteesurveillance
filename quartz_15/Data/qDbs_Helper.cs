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
    public class qDbs_Helper
    {
        public qDbs_Helper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string PerformCustomValidation(string currControl, string evalString)
        {
            string failMessage = string.Empty;

            switch (currControl)
            {
                case "customvld_Focus":
                    if (evalString != "" && evalString != "Selected Item(s): ")
                    {
                        string[] stringArray = evalString.Split(',');

                        if (stringArray.Length > 6)
                        {
                            failMessage = "Too many specialities selected";
                        }
                        else if (stringArray.Length < 1)
                        {
                            failMessage = "Not enough specialities selected";
                        }
                    }
                    else
                    {
                        failMessage = "Not enough specialities selected";
                    }
                    break;
                default:
                    break;

            }

            return failMessage;
        }

        public DataTable BindDataTable(string bindingName, int recordID, string currentValue)
        {
            string query = String.Empty;
            switch (bindingName)
            {
                case "OrgUnits":
                    query = "SELECT * FROM qPtl_OrgUnits";
                    break;
                case "Events":
                    query = "SELECT * FROM Events WHERE EventID = " + currentValue + " AND Available = 'Yes'";
                    break;
                case "Children":
                    query = "SELECT * FROM Children WHERE ChildrenID = " + currentValue + " AND Available = 'Yes'";
                    break;
                case "Volunteers":
                    query = "SELECT * FROM Volunteers WHERE VolunteerID = " + currentValue + " AND Available = 'Yes'";
                    break;
                case "Vendors":
                    query = "SELECT * FROM Vendors WHERE VendorID = " + currentValue + " AND Available = 'Yes'";
                    break;
                default:
                    break;

            }            
            
            String connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataSet dsRecords = new DataSet();

            conn.Open();
            try
            {
                adapter.Fill(dsRecords, "Records");
            }
            finally
            {
                conn.Close();
            }

            DataSet dsRecordsDDL = new DataSet();
            DataTable dtRecordsDDL = new DataTable("RecordsDDL");

            dtRecordsDDL.Columns.Add("RecordID", typeof(int));
            dtRecordsDDL.Columns.Add("DisplayName", typeof(string));

            dsRecordsDDL.Tables.Add(dtRecordsDDL);

            switch (bindingName)
            {
                case "OrgUnits":

                    DataRow drRecordsDDL = dtRecordsDDL.NewRow();
                    drRecordsDDL[0] = 0;
                    drRecordsDDL[1] = "[Pick an Item]";
                    dtRecordsDDL.Rows.Add(drRecordsDDL);
                    
                    foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
                    {
                        DataRow drRecordsDDL2 = dtRecordsDDL.NewRow();
                        drRecordsDDL2[0] = theRow["OrgUnitID"];
                        drRecordsDDL2[1] = theRow["Name"];
                        dtRecordsDDL.Rows.Add(drRecordsDDL2);
                    }                   
                    break;

                case "Events":

                    foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
                    {
                        DataRow drRecordsDDL4 = dtRecordsDDL.NewRow();
                        drRecordsDDL4[0] = theRow["EventID"];
                        drRecordsDDL4[1] = theRow["Name"];
                        dtRecordsDDL.Rows.Add(drRecordsDDL4);
                    }
                    break;

                case "Children":

                    foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
                    {
                        DataRow drRecordsDDL4 = dtRecordsDDL.NewRow();
                        drRecordsDDL4[0] = theRow["ChildrenID"];
                        drRecordsDDL4[1] = theRow["FirstName"] + " " + theRow["LastName"];
                        dtRecordsDDL.Rows.Add(drRecordsDDL4);
                    }
                    break;

                case "Volunteers":

                    foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
                    {
                        DataRow drRecordsDDL4 = dtRecordsDDL.NewRow();
                        drRecordsDDL4[0] = theRow["VolunteerID"];
                        drRecordsDDL4[1] = theRow["LastName"];
                        dtRecordsDDL.Rows.Add(drRecordsDDL4);
                    }
                    break;

                case "Vendors":

                    foreach (DataRow theRow in dsRecords.Tables["Records"].Rows)
                    {
                        DataRow drRecordsDDL4 = dtRecordsDDL.NewRow();
                        drRecordsDDL4[0] = theRow["VendorID"];
                        drRecordsDDL4[1] = theRow["Company"];
                        dtRecordsDDL.Rows.Add(drRecordsDDL4);
                    }
                    break;

                default:
                    break;
            }  

            return dtRecordsDDL;
        }

    }
}
