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

public partial class process_process_contacts_upload : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hplDownloadExample.NavigateUrl = "~/resources/pdf/templates/process-contacts.csv";
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

                process_process_contacts_upload target = new process_process_contacts_upload();
                string mode = "test";
                string[] to_pass = new string[2] {mode, fileFullName};

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

                    string first_name = string.Empty;
                    string last_name = string.Empty;
                    string email = string.Empty;
                    string keywords = string.Empty;
                    string source = string.Empty;
                    string ok_email = string.Empty;
                    string did = string.Empty;
                    string partner = string.Empty;
                    string main_group = string.Empty;
                    string sub_group = string.Empty;
                    string custom_html_element = string.Empty;

                    first_name = c[0];
                    last_name = c[1];
                    email = c[2];
                    keywords = c[3];
                    source = c[4];
                    ok_email = c[5];
                    partner = c[6];
                    main_group = c[7];
                    sub_group = c[8];
                    custom_html_element = c[9];

                    bool success = false;
                    string reason = string.Empty;

                    if (i > 0)
                    {
                        // check to see if valid email
                        string pat = @"^([0-9a-zA-Z]+[-._+&amp;])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
                        Regex r = new Regex(pat, RegexOptions.IgnoreCase);

                        Match m = r.Match(email);
                        if (m.Success && mode == "process")
                        {
                            // check to see if already exists
                            qCom_Contact e_contact = qCom_Contact.GetContactByEmail(email);

                            if (e_contact != null)
                            {
                                if (e_contact.ContactID > 0)
                                {
                                    // update existing contact (except email address)
                                    string curr_keywords = e_contact.Keywords;
                                    string curr_source = e_contact.Source;
                                    e_contact.FirstName = first_name;
                                    e_contact.LastName = last_name;
                                    e_contact.Email = email;
                                    e_contact.Keywords = keywords;
                                    e_contact.Source = source;
                                    // run check to insure that OkEmail is carried over from existing record 
                                    if (e_contact.OKEmail == "No")
                                        e_contact.OKEmail = "No";
                                    else
                                        e_contact.OKEmail = ok_email;
                                    e_contact.LastModified = DateTime.Now;
                                    e_contact.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                                    if (!String.IsNullOrEmpty(main_group))
                                    {
                                        int int_main_group;
                                        if (int.TryParse(main_group, out int_main_group))
                                            e_contact.MainGroup = Convert.ToInt32(int_main_group);
                                    }
                                    if (!String.IsNullOrEmpty(sub_group))
                                    {
                                        int int_sub_group;
                                        if (int.TryParse(sub_group, out int_sub_group))
                                            e_contact.SubGroup = Convert.ToInt32(int_sub_group);
                                    }
                                    e_contact.CustomHTMLElement = custom_html_element;

                                    // does a registered user with the same email exist? only perform this check if the conctact does NOT have a valid userID
                                    if (!String.IsNullOrEmpty(Convert.ToString(e_contact.UserID)) && Convert.ToString(e_contact.UserID) != "0")
                                    {
                                        qPtl_User user = new qPtl_User(email);
                                        if (user != null)
                                        {
                                            if (user.UserID > 0)
                                                e_contact.UserID = user.UserID;
                                        }
                                        else
                                            e_contact.UserID = 0;
                                    }

                                    e_contact.Update();
                                    success = true;
                                }
                            }
                            else
                            {
                                // create new contact
                                qCom_Contact n_contact = new qCom_Contact();
                                n_contact.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
                                n_contact.Available = "Yes";
                                n_contact.Created = DateTime.Now;
                                n_contact.CreatedBy = Convert.ToInt32(Context.Items["UserID"]);
                                n_contact.LastModified = DateTime.Now;
                                n_contact.LastModifiedBy = Convert.ToInt32(Context.Items["UserID"]);
                                n_contact.MarkAsDelete = 0;
                                n_contact.FirstName = first_name;
                                n_contact.LastName = last_name;
                                n_contact.Email = email;
                                n_contact.Keywords = keywords;
                                n_contact.Source = source;
                                n_contact.OKEmail = ok_email;
                                n_contact.Partner = partner;
                                n_contact.CustomHTMLElement = custom_html_element;
                                if (!String.IsNullOrEmpty(main_group))
                                {
                                    int int_main_group;
                                    if (int.TryParse(main_group, out int_main_group))
                                        n_contact.MainGroup = Convert.ToInt32(int_main_group);
                                }
                                if (!String.IsNullOrEmpty(sub_group))
                                {
                                    int int_sub_group;
                                    if (int.TryParse(sub_group, out int_sub_group))
                                        n_contact.SubGroup = Convert.ToInt32(int_sub_group);
                                }

                                // does a registered user with the same email exist?
                                qPtl_User user = new qPtl_User(email);
                                if (user != null)
                                {
                                    if (user.UserID > 0)
                                        n_contact.UserID = user.UserID;
                                }
                                else
                                    n_contact.UserID = 0;

                                n_contact.Insert();
                                success = true;
                            }
                        }
                        else if (m.Success && mode == "test")
                        {
                            success = true;
                        }
                        else if (m.Success == false)
                        {
                            reason = " bad email address";
                        }
                    }

                    i++;

                    if (mode == "process" && success == true)
                    {
                        message += "Line " + i + " SUCCESS - " + email + " successfully added to contact list<br>";
                    }
                    else if (mode == "test" && success == true)
                    {
                        message += "Line " + i + " SUCCESS - " + email + " will be processed<br>";
                    }
                    else if (i > 1)
                    {
                        message += "Line " + i + " FAILURE - " + email + " could not be added for the following reason: " + reason + "<br>";
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