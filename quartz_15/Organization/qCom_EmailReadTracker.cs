using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Quartz.Communication
{
    public class qCom_EmailReadTracker : IHttpModule
	{
        private string pattern = @"/partners/health-net/be-health-wise/emails/footer.png";
        private string spacerFile = "~/partners/health-net/be-health-wise/emails/footer.png";

		public void Dispose() 
		{
		}


		public void Init(System.Web.HttpApplication Appl) 
		{
			Appl.BeginRequest+=new System.EventHandler(GetImage_BeginRequest);
		}


		public void GetImage_BeginRequest(object sender, System.EventArgs args) 
		{
			//cast the sender to a HttpApplication object
			System.Web.HttpApplication application =(System.Web.HttpApplication)sender;

			string url = application.Request.Path; //get the url path

			//create the regex to match for becon images
			Regex r =new Regex( pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
			if( r.IsMatch( url ) )
			{
                string full_url = application.Request.RawUrl;

                if (full_url.Contains("?id="))
                {
                    int start_qs = full_url.IndexOf("?id=");
                    int email_item_id = 0;
                    string email = string.Empty;
                    int length_qs = full_url.Length;
                    string raw_id = full_url.Substring(start_qs + 4, length_qs - (start_qs + 4));

                    if (!String.IsNullOrEmpty(raw_id))
                    {
                        int t_log_id = Convert.ToInt32(raw_id);
                        qCom_TempBulkEmailLog t_log = new qCom_TempBulkEmailLog(t_log_id);
                        email_item_id = t_log.EmailItemID;
                        email = t_log.EmailAddress;

                        try
                        {
                            SaveToDB(t_log, email_item_id, email);
                        }
                        catch
                        {
                            // do nothing
                        }
                    }
                }

				//now send the image to the client
				application.Response.ContentType = "image/gif";
				application.Response.WriteFile(  application.Request.MapPath( spacerFile ) );

				//end the resposne
				application.Response.End();
			}

		}


		private void SaveToDB(qCom_TempBulkEmailLog t_log, int email_item_id, string email)
		{
			int curr_log_id = 0;
            int i = 0;
            if( ( email==null) || ( email.Trim().Length == 0 ) )
				return;

            var logs = qCom_EmailLog.GetEmailLogsByEmailItemIDANDEmail(email_item_id, email);

            if (logs != null)
            {
                foreach (var l in logs)
                {
                    if (i == 0)
                        curr_log_id = l.EmailLogID;
                    i++;
                }
            }
            else
            {
                // log doesn't exist so create one
                qCom_EmailItem email_sent = new qCom_EmailItem(email_item_id);
                qCom_EmailLog log = new qCom_EmailLog();
                log.ScopeID = 1;
                log.EmailItemID = email_item_id;
                log.UserID = 0;
                log.EmailAddress = email;
                log.Subject = email_sent.Subject;
                log.Timestamp = t_log.Timestamp;
                log.Insert();

                curr_log_id = log.EmailLogID;

            }

            if (curr_log_id > 0)
            {
                qCom_EmailLog e_log = new qCom_EmailLog(curr_log_id);
                if (e_log.EmailLogID > 0)
                {
                    e_log.ReadTime = DateTime.Now;
                    e_log.Update();
                }
            }

		}
	}
}
