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
using System.IO;
using System.Text.RegularExpressions;

using Microsoft.VisualBasic;
using Quartz;
using Quartz.Communication;
using Quartz.Portal;
using System.Threading;

public partial class manage_tools_generate_custom_invitation_codes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hplDownloadExample.NavigateUrl = "~/resources/pdf/templates/process-invites.csv";
        }
    }

    protected void btnProcess_OnClick(object sender, System.EventArgs e)
    {
        string[] to_pass = new string[2] { "process", lblFileName.Text };
        ProcessContacts(to_pass);
        plhStep2.Visible = false;
        plhStep4.Visible = true;
    }

    protected void btnTestProcess_OnClick(object sender, System.EventArgs e)
    {
        int userID = System.Convert.ToInt32(Context.Items["userID"]);

        if (Request.Files.Count > 0)
        {
            if (null != upFile1.PostedFile && upFile1.PostedFile.FileName != "")
            {
                string fileName = System.IO.Path.GetFileName(upFile1.PostedFile.FileName);
                string fileFullName = DateTime.Now.ToString("yyyyddMMHHmmss") + System.IO.Path.GetFileName(upFile1.PostedFile.FileName);
                string fileLocation = string.Format("{0}{1}", Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_PDFFolder"])), fileFullName);

                upFile1.PostedFile.SaveAs(fileLocation);

                manage_tools_generate_custom_invitation_codes target = new manage_tools_generate_custom_invitation_codes();
                string mode = "test";
                string[] to_pass = new string[2] { mode, fileFullName };

                //Thread newThread = new Thread(() => { target.ProcessContacts(to_pass); });
                //newThread.Start();

                //processContacts = new Thread(new ThreadStart(ProcessContacts));
                //processContacts.Start();

                ProcessContacts(to_pass);

                btnTestProcess.Visible = false;
                lblFileName.Text = fileFullName;
                plhUpload.Visible = false;
                plhRestartUpload.Visible = true;
                plhStep2Results.Visible = true;
            }
            else
            {
                plhStep3.Visible = false;
                lblUploadFail.Text = "You must first upload a file";
            }

        }
        else
        {
            plhStep3.Visible = false;
        }
    }

    protected void ProcessContacts(string[] to_pass)
    {
        string mode = to_pass[0];
        string f_name = to_pass[1];
        lblFileName.Text = fileFullName;
        string fileName = string.Format("{0}{1}", Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_PDFFolder"])), f_name);
        string message = string.Empty;

        if (File.Exists(fileName))
        {
            List<string[]> contacts = parseCSV(fileName);

            message = "RESULTS<br>";

            //try
            //{
            int i = 0;
            foreach (string[] c in contacts)
            {

                string invite_code = string.Empty;
                string first_name = string.Empty;
                string last_name = string.Empty;
                string email = string.Empty;
                string mobile = string.Empty;
                string postal_code = string.Empty;
                string gender = string.Empty;
                string reference_value = string.Empty;

                invite_code = c[0];
                first_name = c[1];
                last_name = c[2];
                email = c[3];
                mobile = c[4];
                postal_code = c[5];
                gender = c[6];
                reference_value = c[7];

                bool success = false;
                string reason = string.Empty;

                if (i > 0)
                {
                    if (mode == "process")
                    {
                        // get existing invite
                        qPtl_Invitation invite = new qPtl_Invitation(invite_code);

                        if (invite.InvitationID > 0)
                        {
                            invite.InitFirstName = first_name;
                            invite.InitLastName = last_name;
                            invite.InitEmail = email;
                            invite.InitMobile = mobile;
                            invite.InitPostal = postal_code;
                            invite.InitGender = gender;
                            invite.ReferenceValue = reference_value;

                            invite.Update();
                            success = true;
                        }
                    }
                    else if (mode == "test")
                    {
                        success = true;
                    }
                }

                i++;

                if (mode == "process" && success == true)
                {
                    message += "Line " + i + " SUCCESS - Invite Code: " + invite_code + " successfully update<br>";
                }
                else if (mode == "test" && success == true)
                {
                    message += "Line " + i + " SUCCESS - Invite Code: " + invite_code + " will be processed<br>";
                }
                else if (i > 1)
                {
                    message += "Line " + i + " SUCCESS - Invite Code: " + invite_code + " could not be updated<br>";
                }
                else
                    message += "Line " + i + " LINE NOT PROCESSED - header line in the file<br>";
            }

            if (mode == "process")
            {
                lblMessage.Text = message;
                plhStep3.Visible = false;
                plhStep3Completed.Visible = true;
            }
            else if (mode == "test")
            {
                lblTestOutput.Text = message;
                plhStep3.Visible = true;
                plhStep2Completed.Visible = true;
                lblUploadFail.Text = "";
            }
            //}
            //catch
            //{
            //lblTestOutput.Text = "WARNING: File processing failed. Make sure to use a properly formatted .csv file. You can download an example from the top of the page.";
            //}
        }
    }

    public List<string[]> parseCSV(string path)
    {
        List<string[]> parsedData = new List<string[]>();
        string error_message = string.Empty;

        //try
        //{
        using (StreamReader readFile = new StreamReader(path))
        {
            string line;
            string[] row;

            while ((line = readFile.ReadLine()) != null)
            {
                row = line.Split(',');
                parsedData.Add(row);
            }
        }
        //}
        //catch (Exception e)
        //{
        //    error_message += e.Message + "<br>";
        //}

        return parsedData;
    }


    public string fileFullName { get; set; }

}