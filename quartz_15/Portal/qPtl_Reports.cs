using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Portal
{
    public class qPtl_Reports
    {
        protected static qPtl_Reports schema = new qPtl_Reports();

        protected DbRow container;
        protected readonly DbColumn<Int32> report_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime? > created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime? > last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<String> message;
        protected readonly DbColumn<String> contact_name;
        protected readonly DbColumn<String> contact_email;
        protected readonly DbColumn<String> status;
        protected readonly DbColumn<Int32> temp_session_id;
        protected readonly DbColumn<String> referral_url;
        protected readonly DbColumn<Int32> feed_id;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<String> review_notes;
        protected readonly DbColumn<Int32> reviewed_by;
        protected readonly DbColumn<DateTime?> reviewed_date;

        public Int32 ReportID { get { return report_id.Value; } set { report_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime?  Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime?  LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public String Message { get { return message.Value; } set { message.Value = value; } }
        public String ContactName { get { return contact_name.Value; } set { contact_name.Value = value; } }
        public String ContactEmail { get { return contact_email.Value; } set { contact_email.Value = value; } }
        public String Status { get { return status.Value; } set { status.Value = value; } }
        public Int32 TempSessionID { get { return temp_session_id.Value; } set { temp_session_id.Value = value; } }
        public String ReferralURL { get { return referral_url.Value; } set { referral_url.Value = value; } }
        public Int32 FeedID { get { return feed_id.Value; } set { feed_id.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public String ReviewNotes { get { return review_notes.Value; } set { review_notes.Value = value; } }
        public Int32 ReviewedBy { get { return reviewed_by.Value; } set { reviewed_by.Value = value; } }

        public qPtl_Reports()
            : this(new DbRow())
        {
        }

        protected qPtl_Reports(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Reports");
            report_id = container.NewColumn<Int32>("ReportID", true);
            user_id = container.NewColumn<Int32>("UserID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime? >("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime? >("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            message = container.NewColumn<String>("Message");
            contact_name = container.NewColumn<String>("ContactName");
            contact_email = container.NewColumn<String>("ContactEmail");
            status = container.NewColumn<String>("Status");
            temp_session_id = container.NewColumn<Int32>("TempSessionID");
            referral_url = container.NewColumn<String>("ReferralURL");
            feed_id = container.NewColumn<Int32>("FeedID");
            scope_id = container.NewColumn<Int32>("ScopeID");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            review_notes = container.NewColumn<String>("ReviewNotes");
            reviewed_by = container.NewColumn<Int32>("ReviewedBy");
        }

        public qPtl_Reports(Int32 report_id)
            : this()
        {
            container.Select("ReportID = @ReportID", new SqlQueryParameter("@ReportID", report_id));
        }

        public void Update(Int32 user_id, Int32 scope_id, String available, DateTime?  created, Int32 created_by, DateTime?  last_modified, Int32 last_modified_by, Int32 mark_as_delete, String message, String contact_name, String contact_email, String status, Int32 temp_session_id, String referral_url)
        {
            UserID = user_id;
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            Message = message;
            ContactName = contact_name;
            ContactEmail = contact_email;
            Status = status;
            TempSessionID = temp_session_id;
            ReferralURL = referral_url;

            container.Update("ReportID = @ReportID");
        }

        public void Update()
        {
            container.Update(string.Format("ReportID = {0}", ReportID));
        }

        public void Insert(Int32 user_id, Int32 scope_id, String available, DateTime? created, Int32 created_by, DateTime? last_modified, Int32 last_modified_by, Int32 mark_as_delete, String message, String contact_name, String contact_email, String status, Int32 temp_session_id, String referral_url)
        {
            UserID = user_id;
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            Message = message;
            ContactName = contact_name;
            ContactEmail = contact_email;
            Status = status;
            TempSessionID = temp_session_id;
            ReferralURL = referral_url;

            ReportID = Convert.ToInt32(container.Insert());

            qPtl_Task task = new qPtl_Task();
            task.Insert(UserID, 4, null, null, null, null, 0, "Pending", "Pending Site Report", "Report pending review -- report = " + message, 2, 22, ReportID); 
        }

        public void Insert()
        {
            ReportID = Convert.ToInt32(container.Insert());

            qPtl_Reports report = new qPtl_Reports();

            qPtl_Task task = new qPtl_Task();
            task.Insert(report.UserID, 4, null, null, null, null, 0, "Pending", "Pending Site Report", "Report pending review -- report = " + report.message, 2, 22, report.ReportID); 
        }

        public static int GetPendingReportCount()
        {
            DateTime yesterday = new DateTime();
            yesterday = DateTime.Now;
            yesterday = yesterday.AddDays(-1);
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(ReportID) FROM qPtl_Reports WHERE Status = 'Pending'"), CommandType.Text, null));
        }
    }
}
