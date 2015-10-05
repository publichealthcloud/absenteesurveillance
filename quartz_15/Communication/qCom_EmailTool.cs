using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Quartz.Portal;
using Quartz.Data;

namespace Quartz.Communication
{
    public class qCom_EmailTool
    {
        private int emailID, userID, scopeID;
        public string sqlStatement, emailSubject, email_es_Subject, email_fr_Subject, emailType, emailDraft, email_es_Draft, email_fr_Draft, emailURI;

        static string[] parseableItems = { "UserID", "UserName", "FirstName", "LastName", "EmailAddress", "Now", "value1", "value2", "value3", "value4", "DID", "EmailItemID", "TempBulkEmailID" };

        public qCom_EmailTool()
        {
        }

        public qCom_EmailTool(int emailID)
        {
            this.emailID = emailID;
            userID = 0;

            InitializeObject();
        }

        public qCom_EmailTool(int emailID, int userID)
        {
            this.emailID = emailID;
            this.userID = userID;

            InitializeObject();
        }

        public void InitializeObject()
        {
            string query = "SELECT * FROM qCom_EmailItem WHERE EmailID = " + emailID.ToString();

            String connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable dt = new DataTable();

            conn.Open();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                this.scopeID = (int)dt.Rows[0]["ScopeID"];
                this.emailType = (string)dt.Rows[0]["Type"];
                this.emailSubject = (string)dt.Rows[0]["Subject"];
                this.emailDraft = dt.Rows[0]["Draft"].ToString();
                this.emailURI = (string)dt.Rows[0]["URI"];
            }
            conn.Close();
        }

        //Send to just one address with text replacement
        public int SendDatabaseMail(string address, int emailID, int userID, string userName, string value1, string value2, string value3, string value4, bool no_duplicates_today)
        {
            // get email info
            qCom_EmailTool email = new qCom_EmailTool(emailID);
            qCom_EmailItem full_email = new qCom_EmailItem(emailID);
            
            string header = string.Empty;
            string footer = string.Empty;
            string strFromEmail = string.Empty;
            string fromName = string.Empty;
            string emailUsername = null;
            string emailPassword = null;
            string emailServer = null;
            bool sslEnabled = false;
            int portNum = 0;
            string strDID = string.Empty;
            int return_email_log_id = 0;
            bool passed_send_checks = false;
            bool already_sent_today = true;
            string returnedEmail = string.Empty;
            bool ok_to_send = false;
            string noEmail = string.Empty;


            // Check to see if email already sent to user today
            if (no_duplicates_today == true)
            {
                var prior_current_day_log = qCom_EmailLog.CheckForEmailSentLastDay(address);

                if (prior_current_day_log == null)
                {
                    already_sent_today = false;
                }
            }
            else
            {
                already_sent_today = false;
            }

            qCom_UserPreference_View u_pref = new qCom_UserPreference_View(address);
            if (u_pref.UserID > 0)
            {
                if (u_pref.OkBulkEmail == "Yes" || u_pref.OkEmail == "Yes")
                {
                    ok_to_send = true;
                }
            }
            else
                ok_to_send = true;

            if (already_sent_today == false && ok_to_send == true)
                passed_send_checks = true;
            else
                passed_send_checks = false;

            qPtl_User user = new qPtl_User(Convert.ToInt32(userID));
            if (user.UserID == 0)
            {
                user.Email = "";
                user.FirstName = "";
                user.LastName = "";
            }

            // load default email preferences
            if (String.IsNullOrEmpty(emailServer))
            {
                var email_pref = qCom_EmailPreference.GetEmailPreferences();

                if (email_pref != null)
                {
                    if (email_pref.EmailPreferencesID > 0)
                    {
                        header = email_pref.Header;
                        footer = email_pref.Footer;
                        strFromEmail = email_pref.FromEmailAddress;
                        fromName = email_pref.FromName;
                        emailServer = email_pref.SMTPServer;
                        emailUsername = email_pref.SMTPUsername;
                        emailPassword = email_pref.SMTPPassword;
                        sslEnabled = email_pref.SMTPSSL;
                        portNum = email_pref.SMTPPort;
                    }
                }
            }

            // format as friendly send address
            string strFullEmail = string.Empty;
            if (!String.IsNullOrEmpty(fromName))
                strFullEmail = fromName + " <" + strFromEmail + ">";
            else
                strFullEmail = strFromEmail;

            string messageBody = header;

            messageBody += email.emailDraft;
            emailSubject = email.emailSubject;

            messageBody += footer;

            if (!String.IsNullOrEmpty(emailServer) && passed_send_checks == true)
            {
                SmtpClient emailClient = new SmtpClient(emailServer, portNum);
                emailClient.EnableSsl = sslEnabled;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                emailClient.Credentials = new System.Net.NetworkCredential(emailUsername, emailPassword);

                Regex emailCheck = new Regex("^.*<?[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(\\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*@(([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){1,63}\\.)+([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){2,63}>?$");

                // create temp bulk email log -- used for tracking (must be created before sent so can include id in message html)
                int t_log_id = 0;
                qCom_TempBulkEmailLog t_log = new qCom_TempBulkEmailLog();
                t_log.EmailItemID = emailID;
                t_log.EmailAddress = strFullEmail;
                t_log.Timestamp = DateTime.Now;
                t_log.Insert();
                t_log_id = t_log.TempBulkEmailLogID;

                if (emailCheck.IsMatch(address.ToLower()))
                {
                    string finalBody = parseContentField(new string[] { Convert.ToString(user.UserID), userName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2, value3, value4, strDID, Convert.ToString(emailID), Convert.ToString(t_log_id) }, messageBody);

                    string finalSubject = parseContentField(new string[] { Convert.ToString(user.UserID), userName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2, value3, value4, strDID, Convert.ToString(emailID), Convert.ToString(t_log_id) }, emailSubject);

                    MailMessage mailout = new MailMessage(strFullEmail, address, finalSubject, finalBody);
                    mailout.IsBodyHtml = true;

                    emailClient.Send(mailout);

                    return_email_log_id = AddTransmissionLog(scopeID, address, userID, "single", finalSubject, emailID, full_email.CampaignID, strFromEmail);
                }
            }

            return return_email_log_id;
        }

        public int SendDatabaseMailWithAttachment(string address, int emailID, int userID, string value1, string value2, string value3, string attachment)
        {
            int return_email_log_id = 0;
            string header = string.Empty;
            string footer = string.Empty;
            string strFromEmail = string.Empty;
            string fromName = string.Empty;
            string emailUsername = null;
            string emailPassword = null;
            string emailServer = null;
            bool sslEnabled = false;
            int portNum = 0;

            qPtl_User user = new qPtl_User((int)userID);

            // get email preferences
            var email_pref = qCom_EmailPreference.GetEmailPreferences();

            if (email_pref != null)
            {
                if (email_pref.EmailPreferencesID > 0)
                {
                    header = email_pref.Header;
                    footer = email_pref.Footer;
                    strFromEmail = email_pref.FromEmailAddress;
                    fromName = email_pref.FromName;
                    emailServer = email_pref.SMTPServer;
                    emailUsername = email_pref.SMTPUsername;
                    emailPassword = email_pref.SMTPPassword;
                    sslEnabled = email_pref.SMTPSSL;
                    portNum = email_pref.SMTPPort;
                }
            }

            // format as friendly send address
            string strFullEmail = fromName + " <" + strFromEmail + ">";

            // get email info
            qCom_EmailTool email = new qCom_EmailTool(emailID);

            string messageBody = header;
            messageBody += email.emailDraft;
            messageBody += footer;

            if (email_pref.EmailPreferencesID > 0)
            {
                SmtpClient emailClient = new SmtpClient(emailServer, portNum);
                emailClient.EnableSsl = sslEnabled;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                emailClient.Credentials = new System.Net.NetworkCredential(emailUsername, emailPassword);

                emailSubject = email.emailSubject;

                Regex emailCheck = new Regex("^.*<?[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(([a-z]([-a-z0-9]*[a-z0-9]+)?){1,63}\\.)+([a-z]([-a-z0-9]*[a-z0-9]+)?){2,63}>?$");

                if (emailCheck.IsMatch(address.ToLower()))
                {
                    string finalBody = string.Empty;
                    string finalSubject = string.Empty;
                    if (userID == 0)
                    {
                        finalBody = parseContentField(new string[] { "", "", "", "", "", DateTime.Now.ToString(), value1, value2, value3 }, messageBody);
                        finalSubject = parseContentField(new string[] { "", "", "", "", "", DateTime.Now.ToString(), value1, value2, value3 }, emailSubject);
                    }
                    else
                    {
                        finalBody = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2, value3 }, messageBody);
                        finalSubject = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2, value3 }, emailSubject);
                    }

                    MailMessage mailout = new MailMessage(strFullEmail, address, finalSubject, finalBody);
                    if (!String.IsNullOrEmpty(attachment))
                    {
                        Attachment attach = new Attachment(attachment, "");
                        mailout.Attachments.Add(attach);
                    }
                    mailout.IsBodyHtml = true;

                    emailClient.Send(mailout);

                    return_email_log_id = AddTransmissionLog(scopeID, address, userID, "single", finalSubject, 0, 0, address);
                }
            }

            return return_email_log_id;
        }

        //Send to just one address with a preset body (with text replacement)
        public int SendCustomMail(string address, string customMessage, string customSubject, int userID, string value1, string value2)
        {
            int return_email_log_id = 0;
            string header = string.Empty;
            string footer = string.Empty;
            string strFromEmail = string.Empty;
            string fromName = string.Empty;
            string emailUsername = null;
            string emailPassword = null;
            string emailServer = null;
            bool sslEnabled = false;
            int portNum = 0;

            qPtl_User user = new qPtl_User((int)userID);

            // get email preferences
            var email_pref = qCom_EmailPreference.GetEmailPreferences();

            if (email_pref != null)
            {
                if (email_pref.EmailPreferencesID > 0)
                {
                    header = email_pref.Header;
                    footer = email_pref.Footer;
                    strFromEmail = email_pref.FromEmailAddress;
                    fromName = email_pref.FromName;
                    emailServer = email_pref.SMTPServer;
                    emailUsername = email_pref.SMTPUsername;
                    emailPassword = email_pref.SMTPPassword;
                    sslEnabled = email_pref.SMTPSSL;
                    portNum = email_pref.SMTPPort;
                }
            }

            // format as friendly send address
            string strFullEmail = fromName + " <" + strFromEmail + ">";

            string messageBody = header;
            messageBody += customMessage;
            messageBody += footer;

            if (email_pref.EmailPreferencesID > 0)
            {
                SmtpClient emailClient = new SmtpClient(emailServer, portNum);
                emailClient.EnableSsl = sslEnabled;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                emailClient.Credentials = new System.Net.NetworkCredential(emailUsername, emailPassword);

                emailSubject = customSubject;

                Regex emailCheck = new Regex("^.*<?[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(\\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*@(([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){1,63}\\.)+([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){2,63}>?$");

                if (emailCheck.IsMatch(address.ToLower()))
                {
                    string finalBody = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2 }, messageBody);

                    string finalSubject = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2 }, emailSubject);

                    MailMessage mailout = new MailMessage(strFullEmail, address, finalSubject, finalBody);
                    mailout.IsBodyHtml = true;

                    emailClient.Send(mailout);

                    return_email_log_id = AddTransmissionLog(scopeID, address, userID, "single", finalSubject, 0, 0, address);
                }
            }

            return return_email_log_id;
        }

        //Send to just one address with a preset body (no text replacement)
        public int SendEventTestMail(string address, string customMessage, string customSubject, int userID, string value1, string value2)
        {
            int return_email_log_id = 0;
            string header = string.Empty;
            string footer = string.Empty;
            string strFromEmail = string.Empty;
            string fromName = string.Empty;
            string emailUsername = null;
            string emailPassword = null;
            string emailServer = null;
            bool sslEnabled = false;
            int portNum = 0;

            qPtl_User user = new qPtl_User((int)userID);

            // get email preferences
            var email_pref = qCom_EmailPreference.GetEmailPreferences();

            if (email_pref != null)
            {
                if (email_pref.EmailPreferencesID > 0)
                {
                    header = email_pref.Header;
                    footer = email_pref.Footer;
                    strFromEmail = email_pref.FromEmailAddress;
                    fromName = email_pref.FromName;
                    emailServer = email_pref.SMTPServer;
                    emailUsername = email_pref.SMTPUsername;
                    emailPassword = email_pref.SMTPPassword;
                    sslEnabled = email_pref.SMTPSSL;
                    portNum = email_pref.SMTPPort;
                }
            }

            // format as friendly send address
            string strFullEmail = fromName + " <" + strFromEmail + ">";

            if (email_pref.EmailPreferencesID > 0)
            {
                SmtpClient emailClient = new SmtpClient(emailServer, portNum);
                emailClient.EnableSsl = sslEnabled;
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                emailClient.Credentials = new System.Net.NetworkCredential(emailUsername, emailPassword);

                emailSubject = customSubject;

                Regex emailCheck = new Regex("^.*<?[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(\\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*@(([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){1,63}\\.)+([a-zA-Z]([-a-zA-Z0-9]*[a-zA-Z0-9]+)?){2,63}>?$");

                if (emailCheck.IsMatch(address.ToLower()))
                {
                    string finalBody = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2 }, customMessage);

                    string finalSubject = parseContentField(new string[] { Convert.ToString(user.UserID), user.UserName, user.FirstName, user.LastName, user.Email, DateTime.Now.ToString(), value1, value2 }, emailSubject);

                    MailMessage mailout = new MailMessage(strFullEmail, address, finalSubject, finalBody);
                    mailout.IsBodyHtml = true;

                    emailClient.Send(mailout);

                    return_email_log_id = AddTransmissionLog(scopeID, strFullEmail, userID, "test", customSubject, 0, 0, address);
                }
            }

            return return_email_log_id;
        }

        public void SendNotification(int emailID, int UserID, int ProfileUserID, string comment, int ReferenceID)
        {
            qCom_EmailTool email = new qCom_EmailTool();

            qPtl_User user = new qPtl_User(ProfileUserID);
            qPtl_User commentingUser = new qPtl_User(UserID);

            if (!string.IsNullOrEmpty(user.Email))
                email.SendDatabaseMail(user.Email, emailID, user.UserID, commentingUser.UserName, comment, ReferenceID.ToString(), string.Empty, string.Empty, false);

        }

        //Add log for successful transmission
        private static int AddTransmissionLog(int scopeID, string strTo, int userID, string emailType, string emailSubject, int emailID, int campaign_id, string from_email)
        {
            int email_log_id = 0;

            qCom_EmailLog log = new qCom_EmailLog();

            log.ScopeID = scopeID;
            log.EmailItemID = emailID;
            log.UserID = userID;
            log.EmailFrom = from_email;
            log.EmailAddress = strTo;
            log.EmailType = emailType;
            log.Subject = emailSubject;
            log.Timestamp = DateTime.Now;
            log.CampaignID = campaign_id;
            log.Insert();

            email_log_id = log.EmailLogID;

            return email_log_id;
        }

        public string parseContentField(string[] replaceItems, string emailContent)
        {
            for (int i = 0; i < parseableItems.Length && i < replaceItems.Length; i++)
            {
                Regex regParseable = new Regex("{" + parseableItems[i] + "}");

                emailContent = regParseable.Replace(emailContent, replaceItems[i]);
            }

            return emailContent;
        }
    }
}
