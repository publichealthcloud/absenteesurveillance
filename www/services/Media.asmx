<%@ WebService Language="C#" Class="Quartz.Services.Media" %>

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
    public class Media : System.Web.Services.WebService
    {
        [WebMethod]
        public string AddPhotoLike(int user_id, int reference_id, int scope_id)
        {
            // get image
            qSoc_Image image = new qSoc_Image(reference_id);
            
            // add like
            qSoc_Like like = new qSoc_Like();
            like.ScopeID = scope_id;
            like.Available = "Yes";
            like.Created = DateTime.Now;
            like.CreatedBy = user_id;
            like.LastModified = DateTime.Now;
            like.LastModifiedBy = user_id;
            like.MarkAsDelete = 0;
            like.OwnerMarkAsDelete = 0;
            like.OwnerID = image.UserID;
            like.ActorID = user_id;
            like.ReferenceID = reference_id;
            like.ContentTypeID = (int)qSoc_ContentType.Types.Picture;
            like.Insert();

            return String.Format("success");
        }

        [WebMethod]
        public string AddPhotoFollowing(int user_id, int reference_id, int scope_id)
        {
            // get image
            qSoc_Image image = new qSoc_Image(reference_id);

            // 
            // add following
            qSoc_FollowingContent following = new qSoc_FollowingContent();
            following.ScopeID = scope_id;
            following.Available = "Yes";
            following.Created = DateTime.Now;
            following.CreatedBy = user_id;
            following.LastModified = DateTime.Now;
            following.LastModifiedBy = user_id;
            following.MarkAsDelete = 0;
            following.OwnerMarkAsDelete = 0;
            following.ActorMarkAsDelete = 0;
            following.OwnerID = image.UserID;
            following.ActorID = user_id;
            following.ReferenceID = reference_id;
            following.ContentTypeID = (int)qSoc_ContentType.Types.Picture;
            following.Insert();

            return String.Format("success");
        }
    }
}
