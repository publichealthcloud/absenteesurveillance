<%@ WebService Language="C#" Class="Quartz.Services.Communication" %>

using System;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

using Quartz.Portal;
using Quartz.Communication;
using Quartz.Social;

namespace Quartz.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Communication : System.Web.Services.WebService
    {
        [WebMethod]
        public string AddMessageReply(int actor_id, int message_thread_id, int scope_id, string post)
        {
            string str_reply_id = qCom_MessageReply.AddMessageReply(actor_id, message_thread_id, scope_id, post);
            
            return String.Format(str_reply_id);
        }
        
        [WebMethod]
        public string AddMessageThread(int actor_id, string username, int scope_id, string subject, string html)
        {
            string str_message_thread_id = qCom_MessageThread.AddMessageThread(actor_id, username, scope_id, subject, html);

            return String.Format(str_message_thread_id);
        }

        [WebMethod]
        public string[][] GetAllMessageThreadReplies(int message_thread_id)
        {
            string[][] replies = qCom_MessageReply_View.GetAvailableMessageReplies(message_thread_id);

            return replies;
        }

        [WebMethod]
        public string[] ChangeMessageReadStatus(int message_thread_id, string mode)
        {
            string[] message_info = qCom_MessageThread.ChangeMessageReadStatus(message_thread_id, mode);
            
            return message_info;       
        }

        [WebMethod]
        public string DeleteMessageThread(int message_thread_id, string mode)
        {
            string str_message_thread_id = qCom_MessageThread.DeleteMessageThread(message_thread_id, mode);
            
            return Convert.ToString(str_message_thread_id);
        }

        [WebMethod]
        public string GetNumUnreadMessages(int user_id)
        {
            int num_unread = qCom_MessageThread.GetTotalUnreadMessageThreadCountByUserID(user_id);

            return Convert.ToString(num_unread);
        }

        [WebMethod]
        public string[] GetNumAllUnreadMessages(int user_id)
        {
            int total_inbox_unread = qCom_MessageThread.GetTotalUnreadMessageThreadCountByUserID(user_id);
            //int total_sent_unread = qCom_MessageThread.GetSentMessageThreadCountBySender(user_id);
            //int total_unread = total_inbox_unread + total_sent_unread;
            int total_sent_unread = 0;                  // after change to move all unreads into inbox, this was uncessary
            int total_unread = total_inbox_unread;      // after change to move all unreads into inbox, this was simpler

            string[] num_unread = new string[3] {Convert.ToString(total_unread), Convert.ToString(total_inbox_unread), Convert.ToString(total_sent_unread)};
            return num_unread;
        }
    }
}
