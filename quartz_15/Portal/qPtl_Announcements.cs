using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Quartz.Portal
{
    public class qPtl_Announcement
    {
        protected static qPtl_Announcement schema = new qPtl_Announcement();

        protected DbRow container;
        protected readonly DbColumn<Int32> announcement_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> announcement_for_campaign_id;
        protected readonly DbColumn<String> uri;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> text;
        protected readonly DbColumn<String> generic;
        protected readonly DbColumn<DateTime?> generic_available_from;
        protected readonly DbColumn<DateTime?> generic_available_to;
        protected readonly DbColumn<int> remind_every_days;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Boolean> one_time;
        protected readonly DbColumn<Int32> associated_announcement_id;

        public Int32 AnnouncementID { get { return announcement_id.Value; } set { announcement_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 AnnouncementForCampaignID { get { return announcement_for_campaign_id.Value; } set { announcement_for_campaign_id.Value = value; } }
        public String URI { get { return uri.Value; } set { uri.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Text { get { return text.Value; } set { text.Value = value; } }
        public String Generic { get { return generic.Value; } set { generic.Value = value; } }
        public DateTime? GenericAvailableFrom { get { return generic_available_from.Value; } set { generic_available_from.Value = value; } }
        public DateTime? GenericAvailableTo { get { return generic_available_to.Value; } set { generic_available_to.Value = value; } }
        public Int32 RemindEveryDays { get { return remind_every_days.Value; } set { remind_every_days.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Boolean OneTime { get { return one_time.Value; } set { one_time.Value = value; } }
        public Int32 AssociatedAnnouncementID { get { return associated_announcement_id.Value; } set { associated_announcement_id.Value = value; } }

        public qPtl_Announcement()
            : this(new DbRow())
        {
        }

        protected qPtl_Announcement(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Announcements");
            announcement_id = container.NewColumn<Int32>("AnnouncementID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            announcement_for_campaign_id = container.NewColumn<Int32>("AnnouncementForCampaignID");
            uri = container.NewColumn<String>("URI");
            title = container.NewColumn<String>("Title");
            text = container.NewColumn<String>("Text");
            generic = container.NewColumn<String>("Generic");
            generic_available_from = container.NewColumn<DateTime?>("GenericAvailableFrom");
            generic_available_to = container.NewColumn<DateTime?>("GenericAvailableTo");
            remind_every_days = container.NewColumn<Int32>("RemindEveryDays");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            one_time = container.NewColumn<Boolean>("OneTime");
            associated_announcement_id = container.NewColumn<Int32>("AssociatedAnnouncementID");
        }

        public qPtl_Announcement(Int32 announcement_id)
            : this()
        {
            container.Select("AnnouncementID = @AnnouncementID", new SqlQueryParameter("@AnnouncementID", announcement_id));
        }

        public void Update()
        {
            container.Update("AnnouncementID = @AnnouncementID");
        }

        public void Insert()
        {
            AnnouncementID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qPtl_Announcement> GetAnnouncements()
        {
            return schema.container.Select<qPtl_Announcement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0",
                }, c => new qPtl_Announcement(c));
        }

        public static qPtl_Announcement GetAnnouncementByCampaignID(int campaign_id)
        {
            var message = new qPtl_Announcement();

            message.container.Select(
                new DbQuery
                {
                    Where = string.Format("AnnouncementForCampaignID = '{0}'", campaign_id)
                });

            return message;
        }

        public static qPtl_Announcement GetAnnouncementByContentTypeID(int content_type_id)
        {
            var message = new qPtl_Announcement();

            message.container.Select(
                new DbQuery
                {
                    Where = string.Format("Available = 'Yes' AND MarkAsDelete = 0 AND ContentTypeID = {0}", content_type_id)
                });

            return message;
        }

        public static qPtl_Announcement GetAnnouncementByContentTypeReferenceID(int content_type_id, int reference_id)
        {
            var message = new qPtl_Announcement();

            message.container.Select(
                new DbQuery
                {
                    Where = string.Format("Available = 'Yes' AND MarkAsDelete = 0 AND ContentTypeID = {0} AND ReferenceID = {1}", content_type_id, reference_id)
                });

            return message;
        }
    }

    public class qPtl_UserAnnouncement
    {
        protected static qPtl_UserAnnouncement schema = new qPtl_UserAnnouncement();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_announcement_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> announcement_id;
        protected readonly DbColumn<DateTime?> user_viewed;
        protected readonly DbColumn<DateTime?> remind_after;

        public Int32 UserAnnouncementID { get { return user_announcement_id.Value; } set { user_announcement_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 AnnouncementID { get { return announcement_id.Value; } set { announcement_id.Value = value; } }
        public DateTime? UserViewed { get { return user_viewed.Value; } set { user_viewed.Value = value; } }
        public DateTime? RemindAfter { get { return remind_after.Value; } set { remind_after.Value = value; } }

        public qPtl_UserAnnouncement()
            : this(new DbRow())
        {
        }

        protected qPtl_UserAnnouncement(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserAnnouncements");
            user_announcement_id = container.NewColumn<Int32>("UserAnnouncementID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            announcement_id = container.NewColumn<Int32>("AnnouncementID");
            user_viewed = container.NewColumn<DateTime?>("UserViewed");
            remind_after = container.NewColumn<DateTime?>("RemindAfter");
        }

        public qPtl_UserAnnouncement(Int32 user_announcement_id)
            : this()
        {
            container.Select("UserAnnouncementID = @UserAnnouncementID", new SqlQueryParameter("@UserAnnouncementID", user_announcement_id));
        }

        public qPtl_UserAnnouncement(Int32 user_id, Int32 announcement_id)
            : this()
        {
            container.Select("UserID = @UserID AND AnnouncementID = @AnnouncementID", new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@AnnouncementID", announcement_id));
        }

        public void Update()
        {
            container.Update("UserAnnouncementID = @UserAnnouncementID");
        }

        public void Insert()
        {
            UserAnnouncementID = Convert.ToInt32(container.Insert());
        }

        public static qPtl_UserAnnouncement GetUserAnnouncements(int user_id, int announcement_id)
        {
            return schema.container.SelectSingle<qPtl_UserAnnouncement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND AnnouncementID = @AnnouncementID",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@UserID", user_id),
                        new SqlQueryParameter ("@AnnouncementID", announcement_id)
                    }
                }, c => new qPtl_UserAnnouncement(c));
        }

        public static qPtl_UserAnnouncement GetActiveUserAnnouncement(int user_id, int announcement_id)
        {
            DateTime compare_date = DateTime.Now;
            compare_date = compare_date.AddDays(1);

            return schema.container.SelectSingle<qPtl_UserAnnouncement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND AnnouncementID = @AnnouncementID AND UserViewed Is Null AND (RemindAfter Is Null OR RemindAfter > '" + compare_date + "')",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@UserID", user_id),
                        new SqlQueryParameter ("@AnnouncementID", announcement_id)
                    }
                }, c => new qPtl_UserAnnouncement(c));
        }

        public static qPtl_UserAnnouncement GetUserAnnouncementViewedWithin(int user_id, int announcement_id, int seconds)
        {
            DateTime view_range = new DateTime();
            view_range = DateTime.Now;
            view_range = view_range.AddSeconds(seconds);

            return schema.container.SelectSingle<qPtl_UserAnnouncement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND AnnouncementID = @AnnouncementID AND UserViewed > @ViewedWithin",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@UserID", user_id),
                        new SqlQueryParameter ("@AnnouncementID", announcement_id),
                        new SqlQueryParameter ("@ViewedWithin", view_range)
                    }
                }, c => new qPtl_UserAnnouncement(c));
        }

        public static qPtl_UserAnnouncement GetLatestUserAnnouncement(int user_id)
        {
            return schema.container.SelectSingle<qPtl_UserAnnouncement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@UserID", user_id)
                    },
                    OrderBy = "LastModified DESC"
                }, c => new qPtl_UserAnnouncement(c));
        }

        public static qPtl_UserAnnouncement GetAnnouncementByUser(int user_id, int announcement_id)
        {
            return schema.container.SelectSingle<qPtl_UserAnnouncement>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND AnnouncementID = @AnnouncementID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@UserID", user_id), new SqlQueryParameter ("@AnnouncementID", announcement_id)
                    },
                    OrderBy = "LastModified DESC"
                }, c => new qPtl_UserAnnouncement(c));
        }
    }

    public class qPtl_UserAnnouncement_View
    {
        protected static qPtl_UserAnnouncement_View schema = new qPtl_UserAnnouncement_View();

        protected DbRow container;
        protected readonly DbColumn<Int32> user_announcement_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime?> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> announcement_id;
        protected readonly DbColumn<DateTime?> user_viewed;
        protected readonly DbColumn<DateTime?> remind_after;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> text;
        protected readonly DbColumn<String> generic;
        protected readonly DbColumn<DateTime?> generic_available_from;
        protected readonly DbColumn<DateTime?> generic_available_to;
        protected readonly DbColumn<int> remind_every_days;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Boolean> one_time;
        protected readonly DbColumn<Int32> associated_announcement_id;

        public Int32 UserAnnouncementID { get { return user_announcement_id.Value; } set { user_announcement_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime? Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 AnnouncementID { get { return announcement_id.Value; } set { announcement_id.Value = value; } }
        public DateTime? UserViewed { get { return user_viewed.Value; } set { user_viewed.Value = value; } }
        public DateTime? RemindAfter { get { return remind_after.Value; } set { remind_after.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Text { get { return text.Value; } set { text.Value = value; } }
        public String Generic { get { return generic.Value; } set { generic.Value = value; } }
        public DateTime? GenericAvailableFrom { get { return generic_available_from.Value; } set { generic_available_from.Value = value; } }
        public DateTime? GenericAvailableTo { get { return generic_available_to.Value; } set { generic_available_to.Value = value; } }
        public Int32 RemindEveryDays { get { return remind_every_days.Value; } set { remind_every_days.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Boolean OneTime { get { return one_time.Value; } set { one_time.Value = value; } }
        public Int32 AssociatedAnnouncementID { get { return associated_announcement_id.Value; } set { associated_announcement_id.Value = value; } }

        public qPtl_UserAnnouncement_View()
            : this(new DbRow())
        {
        }

        protected qPtl_UserAnnouncement_View(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_UserAnnouncements_View");
            user_announcement_id = container.NewColumn<Int32>("UserAnnouncementID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime?>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            announcement_id = container.NewColumn<Int32>("AnnouncementID");
            user_viewed = container.NewColumn<DateTime?>("UserViewed");
            remind_after = container.NewColumn<DateTime?>("RemindAfter");
            title = container.NewColumn<String>("Title");
            text = container.NewColumn<String>("Text");
            generic = container.NewColumn<String>("Generic");
            generic_available_from = container.NewColumn<DateTime?>("GenericAvailableFrom");
            generic_available_to = container.NewColumn<DateTime?>("GenericAvailableTo");
            remind_every_days = container.NewColumn<Int32>("RemindEveryDays");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            one_time = container.NewColumn<Boolean>("OneTime");
            associated_announcement_id = container.NewColumn<Int32>("AssociatedAnnouncementID");
        }

        public qPtl_UserAnnouncement_View(Int32 user_announcement_id)
            : this()
        {
            container.Select("UserAnnouncementID = @UserAnnouncementID", new SqlQueryParameter("@UserAnnouncementID", user_announcement_id));
        }

        public qPtl_UserAnnouncement_View(Int32 user_id, Int32 announcement_id)
            : this()
        {
            container.Select("UserID = @UserID AND AnnouncementID = @AnnouncementID", new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@AnnouncementID", announcement_id));
        }

        public static qPtl_UserAnnouncement_View GetLatestUserAnnouncement(int user_id)
        {
            return schema.container.SelectSingle<qPtl_UserAnnouncement_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND UserViewed Is null AND (RemindAfter Is null OR RemindAfter < GetDate())",
                    Parameters = new[] {
                        new SqlQueryParameter ("@UserID", user_id)
                    },
                    OrderBy = "Created DESC"
                }, c => new qPtl_UserAnnouncement_View (c));
        }

        public static ICollection<qPtl_UserAnnouncement_View> GetUserAnnouncements(int user_id)
        {
            return schema.container.Select<qPtl_UserAnnouncement_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserViewed Is null AND (RemindAfter Is null OR RemindAfter < GetDate()) AND UserID = @UserID",
                    Parameters = new[] {
                        new SqlQueryParameter ("@UserID", user_id)
                    }
                }, c => new qPtl_UserAnnouncement_View(c));
        }

        public static qPtl_UserAnnouncement_View GetUserAnnouncement(int user_id, int announcement_id)
        {
            return schema.container.SelectSingle<qPtl_UserAnnouncement_View>(
                new DbQuery
                {
                    Where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserID = @UserID AND AnnouncementID = @AnnouncementID",
                    Parameters = new[] { 
                        new SqlQueryParameter ("@UserID", user_id),
                        new SqlQueryParameter ("@AnnouncementID", announcement_id)
                    }
                }, c => new qPtl_UserAnnouncement_View(c));
        }

        public static qPtl_UserAnnouncement_View GetUserAnnouncementByContentTypeID(int content_type_id, int user_id)
        {
            var message = new qPtl_UserAnnouncement_View();

            message.container.Select(
                new DbQuery
                {
                    Where = string.Format("Available = 'Yes' AND MarkAsDelete = 0 AND UserViewed Is null AND (RemindAfter Is null OR RemindAfter < GetDate()) AND ContentTypeID = {0} AND UserID = {1}", content_type_id, user_id)
                });

            return message;
        }

        public static int GetAssociatedUserAnnouncement(int user_id, int associated_announcement_id, DateTime within_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(
                "SELECT TOP(1) UserAnnouncementID FROM qPtl_UserAnnouncements_View WHERE UserID = @UserID AND AssociatedAnnouncementID = @AssociatedAnnouncementID AND Created > '" + within_date + "' ORDER BY CREATED DESC",
                CommandType.Text,
                new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("AssociatedAnnouncementID", associated_announcement_id) }));
        }

        public static qPtl_UserAnnouncement_View GetUserAnnouncementByContentTypeReferenceID(int content_type_id, int reference_id, int user_id)
        {
            var message = new qPtl_UserAnnouncement_View();
            string sql_where = string.Empty;

            sql_where = "Available = 'Yes' AND MarkAsDelete = 0 AND UserViewed Is null AND (RemindAfter Is null OR RemindAfter < GetDate())";

            if (content_type_id == 0)
                sql_where += " AND (ContentTypeID = " + content_type_id + " OR ContentTypeID Is null)";
            else
                sql_where += " AND ContentTypeID = " + content_type_id;

            if (reference_id == 0)
                sql_where += " AND (ReferenceID = " + reference_id + " OR ReferenceID Is null)";
            else
                sql_where += " AND ReferenceID = " + reference_id;

            sql_where += " AND UserID = " + user_id;


            message.container.Select(
                new DbQuery
                {
                    Where = sql_where
                });

            return message;
        }
    }
}
