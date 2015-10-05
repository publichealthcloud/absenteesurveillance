using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_HealthWarning
    {
        protected static qHtl_HealthWarning schema = new qHtl_HealthWarning();

        protected DbRow container;
        protected readonly DbColumn<Int32> health_warning_id;
        protected readonly DbColumn<DateTime> created; 
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<DateTime?> data_date;
        protected readonly DbColumn<String> type;
        protected readonly DbColumn<String> title;
        protected readonly DbColumn<String> text;
        protected readonly DbColumn<String> notes;
        protected readonly DbColumn<String> severity;
        protected readonly DbColumn<Int32> issued_by;
        protected readonly DbColumn<String> issued_by_name; 
        protected readonly DbColumn<String> review_status;
        protected readonly DbColumn<String> issued_to;
        protected readonly DbColumn<DateTime?> valid_from;
        protected readonly DbColumn<DateTime?> valid_until;
        protected readonly DbColumn<String> multiple_classrooms;
        protected readonly DbColumn<String> multiple_schools;
        protected readonly DbColumn<String> school_levels;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;
        protected readonly DbColumn<Int32> email_id;
        protected readonly DbColumn<Int32> notification_id;
        protected readonly DbColumn<Int32> sms_message_id;

        public Int32 HealthWarningID { get { return health_warning_id.Value; } set { health_warning_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public DateTime? DataDate { get { return data_date.Value; } set { data_date.Value = value; } }
        public String Type { get { return type.Value; } set { type.Value = value; } }
        public String Title { get { return title.Value; } set { title.Value = value; } }
        public String Text { get { return text.Value; } set { text.Value = value; } }
        public String Notes { get { return notes.Value; } set { notes.Value = value; } }
        public String Severity { get { return severity.Value; } set { severity.Value = value; } }
        public Int32 IssuedBy { get { return issued_by.Value; } set { issued_by.Value = value; } }
        public String IssuedByName { get { return issued_by_name.Value; } set { issued_by_name.Value = value; } }
        public String ReviewStatus { get { return review_status.Value; } set { review_status.Value = value; } }
        public String IssuedTo { get { return issued_to.Value; } set { issued_to.Value = value; } }
        public DateTime? ValidFrom { get { return valid_from.Value; } set { valid_from.Value = value; } }
        public DateTime? ValidUntil { get { return valid_until.Value; } set { valid_until.Value = value; } }
        public String MultipleClassrooms { get { return multiple_classrooms.Value; } set { multiple_classrooms.Value = value; } }
        public String MultipleSchools { get { return multiple_schools.Value; } set { multiple_schools.Value = value; } }
        public String SchoolLevels { get { return school_levels.Value; } set { school_levels.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }
        public Int32 EmailID { get { return email_id.Value; } set { email_id.Value = value; } }
        public Int32 NotificationID { get { return notification_id.Value; } set { notification_id.Value = value; } }
        public Int32 SMSMessageID { get { return sms_message_id.Value; } set { sms_message_id.Value = value; } }

        public qHtl_HealthWarning()
            : this(new DbRow())
        {
        }

        protected qHtl_HealthWarning(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_HealthWarnings");
            health_warning_id = container.NewColumn<Int32>("HealthWarningID", true);
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            data_date = container.NewColumn<DateTime?>("DataDate");
            type = container.NewColumn<String>("Type");
            title = container.NewColumn<String>("Title");
            text = container.NewColumn<String>("Text");
            notes = container.NewColumn<String>("Notes");
            severity = container.NewColumn<String>("Severity");
            issued_by = container.NewColumn<Int32>("IssuedBy");
            issued_by_name = container.NewColumn<String>("IssuedByName");
            review_status = container.NewColumn<String>("ReviewStatus");
            issued_to = container.NewColumn<String>("IssuedTo");
            valid_from = container.NewColumn<DateTime?>("ValidFrom");
            valid_until = container.NewColumn<DateTime?>("ValidUntil");
            multiple_classrooms = container.NewColumn<String>("MultipleClassrooms");
            multiple_schools = container.NewColumn<String>("MultipleSchools");
            school_levels = container.NewColumn<String>("SchoolLevels");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
            email_id = container.NewColumn<Int32>("EmailID");
            notification_id = container.NewColumn<Int32>("NotificationID");
            sms_message_id = container.NewColumn<Int32>("SMSMessageID");
        }

        public qHtl_HealthWarning(Int32 health_warning_id)
            : this()
        {
            container.Select("HealthWarningID = @HealthWarningID", new SqlQueryParameter("@HealthWarningID", health_warning_id));
        }

        public qHtl_HealthWarning(String title)
            : this()
        {
            container.Select("Title LIKE @Title", new SqlQueryParameter("@Title", title));
        }

        public void Update()
        {
            container.Update("HealthWarningID = @HealthWarningID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            HealthWarningID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteWarningsBySchoolAndDate(int school_id, string curr_date)
        {
            schema.container.Delete(string.Format(string.Format("DataDate = '{0}' AND ReferenceID = {1}", curr_date, school_id)));
        }

        public static ICollection<qHtl_HealthWarning> GeHealthWarningsByDayAndSeverity(DateTime current_day, string severity)
        {
            string sql = string.Empty;
            DateTime eval_date = new DateTime();
            eval_date = Convert.ToDateTime(current_day);
            string eval_date_string = String.Format("{0:d}", eval_date);

            sql = "Severity LIKE '%" + severity + "%' AND DataDate = '" + eval_date_string + "'";

            return schema.container.Select<qHtl_HealthWarning>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "IssuedTo ASC",
                }, c => new qHtl_HealthWarning(c));
        }
    }
}
