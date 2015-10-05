<%@ WebService Language="C#" Class="Quartz.Services.UserInfo" %>

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
    public class UserInfo : System.Web.Services.WebService
    {
        [WebMethod]
        // sets a picture as a user's profile picture
        public string SetProfilePicture(int user_id, int reference_id)
        {
            bool success = false;

            success = qSoc_Image.SetImageAsProfilePicture(user_id, reference_id);

            return Convert.ToString(success);
        }

        [WebMethod]
        // sets a picture as a user's profile picture
        public string SetCoverArt(int user_id, int image_id, int reference_id, int content_type_id)
        {
            string success = "fail";

            qSoc_CoverArt art = new qSoc_CoverArt();
            art.ScopeID = Convert.ToInt32(Context.Items["ScopeID"]);
            art.Available = "Yes";
            art.Created = DateTime.Now;
            art.CreatedBy = user_id;
            art.LastModified = DateTime.Now;
            art.LastModifiedBy = user_id;
            art.MarkAsDelete = 0;
            art.OwnerMarkAsDelete = 0;
            art.ReferenceID = reference_id;
            art.ContentTypeID = content_type_id;
            art.ImageID = image_id;
            art.Insert();

            if (art.CoverArtID > 0)
                success = Convert.ToString(art.CoverArtID);

            return Convert.ToString(success);
        }
    }
}
