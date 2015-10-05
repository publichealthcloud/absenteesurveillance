using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quartz.Communication;
using Quartz.Portal;

namespace Quartz.Portal
{
    public class qPtl_Notification
    {
        protected static qPtl_Notification schema = new qPtl_Notification();

        protected DbRow container;
        protected readonly DbColumn<Int32> notification_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> owner_id;
        protected readonly DbColumn<Int32> actor_id;
        protected readonly DbColumn<String> type;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> text;
        protected readonly DbColumn<Boolean> owner_viewed;
        protected readonly DbColumn<Int32> comment_id;
        protected readonly DbColumn<Int32> feed_id;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Int32> campaign_id;

        public Int32 NotificationID { get { return notification_id.Value; } set { notification_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 OwnerID { get { return owner_id.Value; } set { owner_id.Value = value; } }
        public Int32 ActorID { get { return actor_id.Value; } set { actor_id.Value = value; } }
        public String Type { get { return type.Value; } set { type.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Text { get { return text.Value; } set { text.Value = value; } }
        public Boolean OwnerViewed { get { return owner_viewed.Value; } set { owner_viewed.Value = value; } }
        public Int32 CommentID { get { return comment_id.Value; } set { comment_id.Value = value; } }
        public Int32 FeedID { get { return feed_id.Value; } set { feed_id.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }

        public qPtl_Notification()
            : this(new DbRow())
        {
        }

        protected qPtl_Notification(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Notifications");
            notification_id = container.NewColumn<Int32>("NotificationID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            owner_id = container.NewColumn<Int32>("OwnerID");
            actor_id = container.NewColumn<Int32>("ActorID");
            type = container.NewColumn<String>("Type");
            title = container.NewColumn<String>("Title");
            text = container.NewColumn<String>("Text");
            owner_viewed = container.NewColumn<Boolean>("OwnerViewed");
            comment_id = container.NewColumn<Int32>("CommentID");
            feed_id = container.NewColumn<Int32>("FeedID");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            campaign_id = container.NewColumn<Int32>("CampaignID");
        }

        public qPtl_Notification(Int32 notification_id)
            : this()
        {
            container.Select("NotificationID = @NotificationID", new SqlQueryParameter("@NotificationID", notification_id));
        }

        public void Update()
        {
            container.Update("NotificationID = @NotificationID");
        }

        public void Insert()
        {
            NotificationID = Convert.ToInt32(container.Insert());

            //if email notification enabled, send an email
            string send_notification_email = System.Configuration.ConfigurationManager.AppSettings["Email_SendNotificationEmail"];
            if (!String.IsNullOrEmpty(send_notification_email))
            {
                if (send_notification_email == "true")
                {
                    qPtl_Notification notification = new qPtl_Notification(NotificationID);
                    qPtl_User owner = new qPtl_User(notification.OwnerID);
                    qPtl_User actor = new qPtl_User(notification.ActorID);
                    int notification_email_id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Email_NotificationEmailID"]);
                    if (!String.IsNullOrEmpty(Convert.ToString(notification_email_id)))
                    {
                        if (notification_email_id > 0)
                        {
                            try
                            {
                                qCom_EmailTool email = new qCom_EmailTool(notification_email_id);
                                email.SendDatabaseMail(owner.Email, notification_email_id, owner.UserID, notification.Text, actor.UserName, "", "", "", false);
                            }
                            catch
                            {
                                // do nothing
                            }
                        }
                    }
                }
            }
        }

        public static void DeleteAllUserNotifications(int user_id)
        {
            schema.container.Delete(string.Concat("UserID = ", user_id));
        }

        public static void DeleteNotification(int notification_id)
        {
            schema.container.Delete(string.Concat("NotificationID = ", notification_id));
        }

        public static ICollection<qPtl_Notification> GetUserNotifications(int user_id)
        { 
            return schema.container.Select<qPtl_Notification>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                },
                c => new qPtl_Notification(c));
        }

        public static ICollection<qPtl_Notification> GetNotificationsByCampaign(int campaign_id)
        {
            return schema.container.Select<qPtl_Notification>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND CampaignID = @CampaignID",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@CampaignID", campaign_id) },
                    OrderBy = "Title"
                },
                c => new qPtl_Notification(c));
        }
    }

    public class qPtl_Notification_View
    {
        protected static qPtl_Notification_View schema = new qPtl_Notification_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> notification_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> owner_id;
        protected readonly DbColumn<Int32> actor_id;
        protected readonly DbColumn<String> type;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> text;
        protected readonly DbColumn<Boolean> owner_viewed;
        protected readonly DbColumn<Int32> comment_id;
        protected readonly DbColumn<Int32> feed_id;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        private readonly DbColumn<String> comment;
        private readonly DbColumn<String> username;
        private readonly DbColumn<String> profile_pict;

        public Int32 NotificationID { get { return notification_id.Value; } set { notification_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 OwnerID { get { return owner_id.Value; } set { owner_id.Value = value; } }
        public Int32 ActorID { get { return actor_id.Value; } set { actor_id.Value = value; } }
        public String Type { get { return type.Value; } set { type.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Text { get { return text.Value; } set { text.Value = value; } }
        public Boolean OwnerViewed { get { return owner_viewed.Value; } set { owner_viewed.Value = value; } }
        public Int32 CommentID { get { return comment_id.Value; } set { comment_id.Value = value; } }
        public Int32 FeedID { get { return feed_id.Value; } set { feed_id.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String Comment { get { return comment.Value; } set { comment.Value = value; } }
        public String UserName { get { return username.Value; } set { username.Value = value; } }
        public String ProfilePict { get { return profile_pict.Value; } set { profile_pict.Value = value; } }

        public qPtl_Notification_View()
            : this(new DbRow())
        {
        }

        protected qPtl_Notification_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Notifications_View");
            notification_id = container.NewColumn<Int32>("NotificationID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            owner_id = container.NewColumn<Int32>("OwnerID");
            actor_id = container.NewColumn<Int32>("ActorID");
            type = container.NewColumn<String>("Type");
            title = container.NewColumn<String>("Title");
            text = container.NewColumn<String>("Text");
            owner_viewed = container.NewColumn<Boolean>("OwnerViewed");
            comment_id = container.NewColumn<Int32>("CommentID");
            feed_id = container.NewColumn<Int32>("FeedID");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            comment = container.NewColumn<String>("Comment");
            username = container.NewColumn<String>("UserName");
            profile_pict = container.NewColumn<String>("ProfilePict");;
        }

        public qPtl_Notification_View(Int32 notification_id)
            : this()
        {
            container.Select("NotificationID = @NotificationID", new SqlQueryParameter("@NotificationID", notification_id));
        }

        public static ICollection<qPtl_Notification_View> GetAvailableUserNotifications(int user_id, int num_return)
        {
            string top_sql = "";
            if (num_return > 0)
                top_sql = "TOP(" + num_return + ")";
            
            return schema.container.Select<qPtl_Notification_View>(
                new DbQuery
                {
                    Top = top_sql,
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND MarkAsDelete = 0 AND OwnerID = @OwnerID",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@OwnerID", user_id) }
                },
                c => new qPtl_Notification_View(c));
        }

        public static ICollection<qPtl_Notification_View> GetUnviewedAvailableUserNotifications(int user_id, int num_return)
        {
            string top_sql = "";
            if (num_return > 0)
                top_sql = "TOP(" + num_return + ")";
            
            return schema.container.Select<qPtl_Notification_View>(
                new DbQuery
                {
                    Top = top_sql,
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND OwnerID = @OwnerID AND (OwnerViewed = 'false' OR OwnerViewed Is Null)",
                    OrderBy = "Created DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@OwnerID", user_id) }
                },
                c => new qPtl_Notification_View(c));
        }
    }
}
