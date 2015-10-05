using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Communication
{
    public class qCom_EmailLog
    {
        protected static qCom_EmailLog schema = new qCom_EmailLog();

        protected DbRow container;
        protected readonly DbColumn<Int32> email_log_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> email_item_id;
        protected readonly DbColumn<String> email_to;
        protected readonly DbColumn<String> email_from;
        protected readonly DbColumn<String> email_address;
        protected readonly DbColumn<String> email_type;
        protected readonly DbColumn<String> subject;
        protected readonly DbColumn<DateTime?> timestamp;
        protected readonly DbColumn<String> custom_message;
        protected readonly DbColumn<DateTime?> read_time;
        protected readonly DbColumn<DateTime?> click_thru_time;
        protected readonly DbColumn<Boolean> report_as_spam;
        protected readonly DbColumn<Boolean> bounce_back;
        protected readonly DbColumn<Int32> campaign_id;

        public Int32 EmailLogID { get { return email_log_id.Value; } set { email_log_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 EmailItemID { get { return email_item_id.Value; } set { email_item_id.Value = value; } }
        public String EmailTo { get { return email_to.Value; } set { email_to.Value = value; } }
        public String EmailFrom { get { return email_from.Value; } set { email_from.Value = value; } }
        public String EmailAddress { get { return email_address.Value; } set { email_address.Value = value; } }
        public String EmailType { get { return email_type.Value; } set { email_type.Value = value; } }
        public String Subject { get { return subject.Value; } set { subject.Value = value; } }
        public DateTime? Timestamp { get { return timestamp.Value; } set { timestamp.Value = value; } }
        public String CustomMessage { get { return custom_message.Value; } set { custom_message.Value = value; } }
        public DateTime? ReadTime { get { return read_time.Value; } set { read_time.Value = value; } }
        public DateTime? ClickThruTime { get { return click_thru_time.Value; } set { click_thru_time.Value = value; } }
        public Boolean ReportAsSpam { get { return report_as_spam.Value; } set { report_as_spam.Value = value; } }
        public Boolean BounceBack { get { return bounce_back.Value; } set { bounce_back.Value = value; } }
        public Int32 CampaignID { get { return campaign_id.Value; } set { campaign_id.Value = value; } }

        public qCom_EmailLog()
            : this(new DbRow())
        {
        }

        protected qCom_EmailLog(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_EmailLogs");
            email_log_id = container.NewColumn<Int32>("EmailLogID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            user_id = container.NewColumn<Int32>("UserID");
            email_item_id = container.NewColumn<Int32>("EmailItemID");
            email_to = container.NewColumn<String>("EmailTo");
            email_from = container.NewColumn<String>("EmailFrom");
            email_address = container.NewColumn<String>("EmailAddress");
            email_type = container.NewColumn<String>("EmailType");
            subject = container.NewColumn<String>("Subject");
            timestamp = container.NewColumn<DateTime?>("Timestamp");
            custom_message = container.NewColumn<String>("CustomMessage");
            read_time = container.NewColumn<DateTime?>("ReadTime");
            click_thru_time = container.NewColumn<DateTime?>("ClickThruTime");
            report_as_spam = container.NewColumn<Boolean>("ReportAsSpam");
            bounce_back = container.NewColumn<Boolean>("Bounceback");
            campaign_id = container.NewColumn<Int32>("CampaignID");
        }

        public qCom_EmailLog(Int32 email_log_id)
            : this()
        {
            container.Select("EmailLogID = @EmailLogID", new SqlQueryParameter("@EmailLogID", email_log_id));
        }

        public void Update()
        {
            container.Update("EmailLogID = @EmailLogID");
        }

        public void Insert()
        {
            EmailLogID = Convert.ToInt32(container.Insert());
        }

        public static ICollection<qCom_EmailLog> GetEmailLogsBySubject(string subject)
        {
            return schema.container.Select<qCom_EmailLog>(
                new DbQuery
                {
                    Where = "Subject LIKE '%@Subject%'",
                    OrderBy = "Timestamp DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@Subject", subject) }
                }, c => new qCom_EmailLog(c));
        }

        public static ICollection<qCom_EmailLog> GetEmailLogsByCampaignID(int campaign_id)
        {
            return schema.container.Select<qCom_EmailLog>(
                new DbQuery
                {
                    Where = "CampaignID = @CampaignID",
                    OrderBy = "Timestamp DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@CampaignID", campaign_id) }
                }, c => new qCom_EmailLog(c));
        }

        public static ICollection<qCom_EmailLog> GetEmailLogsByEmailItemID(int email_item_id)
        {
            return schema.container.Select<qCom_EmailLog>(
                new DbQuery
                {
                    Where = "EmailItemID = @EmailItemID",
                    OrderBy = "Timestamp DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@EmailItemID", email_item_id) }
                }, c => new qCom_EmailLog(c));
        }

        public static ICollection<qCom_EmailLog> GetEmailLogsByEmailItemIDANDEmail(int email_item_id, string email)
        {
            return schema.container.Select<qCom_EmailLog>(
                new DbQuery
                {
                    Where = "EmailItemID = @EmailItemID AND EmailAddress LIKE '%" + email + "%'",
                    OrderBy = "Timestamp DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@EmailItemID", email_item_id) }
                }, c => new qCom_EmailLog(c));
        }

        public static ICollection<qCom_EmailLog> GetEmailLogsByUserID(int user_id)
        {
            return schema.container.Select<qCom_EmailLog>(
                new DbQuery
                {
                    Where = "UserID = @UserID",
                    OrderBy = "Timestamp DESC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) }
                }, c => new qCom_EmailLog(c));
        }

        public static qCom_EmailLog CheckForEmailSentLastDay(string email_address)
        {
            qCom_EmailLog log = new qCom_EmailLog();

            log.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("DateDiff(DAY, Timestamp, GETDATE()) = 0 AND EmailAddress LIKE '%" + email_address + "%'"),
                OrderBy = "Timestamp DESC"
            });

            if (log.EmailLogID > 0) return log;
            else return null;
        }
    }
}
