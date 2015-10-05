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
using Quartz.Communication;
using Quartz.Data;

public partial class qCom_automated_bulk_send : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // get all send events
            string sqlCode = string.Empty;

            sqlCode = "SELECT * FROM qCom_SendEvents_View WHERE (Available = 'Yes' AND Running = 'Yes' AND Recurring = 'Yes' AND GetDate() > StartDate) OR (Available = 'Yes' AND Running = 'Yes' AND Recurring = 'No' AND (GetDate() Between StartDate AND StartDate+1)) ORDER BY Priority ASC";
            qDbs_SQLcode sql = new qDbs_SQLcode();
            SqlDataReader eReader = sql.GetDataReader(sqlCode);

            if (eReader.HasRows)
            {
                while (eReader.Read())
                {
                    // see if there are custom email preferences if this send event is associated with a campaign
                    int campaign_id = 0;
                    string custom_email = string.Empty;
                    string custom_email_from = string.Empty;
                    bool get_custom_did = false;

                    if (!String.IsNullOrEmpty(Convert.ToString(eReader["CampaignID"])))
                        campaign_id = Convert.ToInt32(eReader["CampaignID"]);

                    if (campaign_id > 0)
                    {
                        var pref = qCom_CampaignPreference.GetCampaignPreferences(campaign_id);
                        if (pref != null)
                        {
                            if (pref.CampaignPreferenceID > 0)
                            {
                                if (!String.IsNullOrEmpty(pref.CampaignEmail))
                                    custom_email = pref.CampaignEmail;
                                if (!String.IsNullOrEmpty(pref.CampaignEmailFrom))
                                    custom_email_from = pref.CampaignEmailFrom;

                                // see if we need to get / integrated a custom DID
                                if (pref.IncludeCustomDID == true)
                                    get_custom_did = true;
                            }
                        }
                    }

                    // get email preferences
                    string sqlCode2 = "SELECT TOP(1) * FROM qCom_EmailPreferences WHERE Available = 'Yes'";
                    SqlDataReader pReader = sql.GetDataReader(sqlCode2);

                    pReader.Read();
                    string header = Convert.ToString(pReader["Header"]);
                    string footer = Convert.ToString(pReader["Footer"]);
                    string unsubscribe = Convert.ToString(pReader["Unsubscribe"]);
                    string fromEmailAddress = string.Empty;
                    string fromName = string.Empty;
                    if (!String.IsNullOrEmpty(custom_email))
                        fromEmailAddress = custom_email;
                    else
                        fromEmailAddress = Convert.ToString(pReader["FromEmailAddress"]);
                    if (!String.IsNullOrEmpty(custom_email_from))
                        fromName = custom_email_from;
                    else
                        fromName = Convert.ToString(pReader["fromName"]);
                    pReader.Close();


                    //try
                    //{
                        lblMessage.Text += "<br>Sending...";
                        // create object
                        int emailID = Convert.ToInt32(eReader["emailID"]);
                        string contactQuery = "SELECT " + Convert.ToString(eReader["sqlSELECT"]) + " FROM " + Convert.ToString(eReader["sqlFROM"]) + " WHERE " + Convert.ToString(eReader["sqlWHERE"]);
                        bool is_contact_list = false;
                        if (Convert.ToString(eReader["sqlFROM"]).Contains("qCom_Contacts"))
                            is_contact_list = true;

                        qCom_EmailTool email = new qCom_EmailTool(emailID);

                        // get addresses
                        string[][] contacts = email.GetSendEventContacts(contactQuery, get_custom_did, campaign_id, Convert.ToInt32(Context.Items["ScopeID"]), is_contact_list);

                        string messageBody = string.Empty;
                        string includeHeader = Convert.ToString(eReader["IncludeHeader"]);
                        string includeFooter = Convert.ToString(eReader["IncludeFooter"]);
                        string includeUnsubscribe = Convert.ToString(eReader["IncludeUnsubscribe"]);
                        
                        if (includeHeader == "Yes")
                        {
                            messageBody += header;
                        }

                        messageBody += email.emailDraft;

                        if (includeFooter == "Yes")
                        {
                            messageBody += footer;
                        }
                        if (includeUnsubscribe == "Yes")
                        {
                            messageBody += unsubscribe;
                        }

                        //(string[][] addresses, string strFrom, string strFromEmail, string requestURL, string strSubject, string rawEmailContent, string userID, string value1, string value2, bool sendCopyToAdmins, bool noDuplicatesToday, bool noDontSend, int emailID)
                        ArrayList[] output = email.SendMail(contacts, fromName, fromEmailAddress, "bulkemail", email.emailSubject, messageBody, "", "", "", "", "", false, true, true, emailID, get_custom_did, campaign_id);

                        lblMessage.Text += "finished sending email: " + email.emailSubject + "; send event id = " + Convert.ToString(eReader["SendEventID"]) + "; number of recipients=" + contacts.Length;
                    //}
                    //catch
                    //{
                    //    lblMessage.Text += "A problem has occurred<br>";
                    //}
                }

                eReader.Close();

            }
            else
            {
                lblMessage.Text += "There are no scheduled send events.<br>";
            }

        }
        else
        {
            lblMessage.Text += "Problem with key or page incorrectly loaded.<br>";
        }
    }
}