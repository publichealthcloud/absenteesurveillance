<%@ WebService Language="C#" Class="Quartz.Services.Notification" %>

using System;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

using Quartz.Portal;
using Quartz.Social;

namespace Quartz.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    public class Notification : System.Web.Services.WebService
    {
        [WebMethod]
        public string MarkNotificationAsViewed(int notification_id)
        {
            // send reply
            qPtl_Notification notification = new qPtl_Notification(notification_id);

            if (notification.NotificationID > 0)
            {
                notification.OwnerViewed = true;
                notification.Update();

                return String.Format(Convert.ToString(notification.NotificationID));
            }
            else
                return String.Format("failure");
        }
    }
}
