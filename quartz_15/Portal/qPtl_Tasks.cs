using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

using Quartz.Communication;
using Quartz.Core;

namespace Quartz.Portal
{
    public class qPtl_Task
    {
        protected static qPtl_Task schema = new qPtl_Task();

        protected DbRow container;
        protected readonly DbColumn<Int32> task_id;
        protected readonly DbColumn<Int32> scope_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32?> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> task_type_id;
        protected readonly DbColumn<Int32?> assigned_to;
        protected readonly DbColumn<String> assigned_text;
        protected readonly DbColumn<DateTime?> start_date;
        protected readonly DbColumn<DateTime?> due_date;
        protected readonly DbColumn<Double> percent_completed;
        protected readonly DbColumn<String> status;
        protected readonly DbColumn<String> name;
        protected readonly DbColumn<String> description;
        protected readonly DbColumn<Int32> importance;
        protected readonly DbColumn<Int32> content_type_id;
        protected readonly DbColumn<Int32> reference_id;

        public Int32 TaskID { get { return task_id.Value; } set { task_id.Value = value; } }
        public Int32 ScopeID { get { return scope_id.Value; } set { scope_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32? LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 TaskTypeID { get { return task_type_id.Value; } set { task_type_id.Value = value; } }
        public Int32? AssignedTo { get { return assigned_to.Value; } set { assigned_to.Value = value; } }
        public String AssignedText { get { return assigned_text.Value; } set { assigned_text.Value = value; } }
        public DateTime? StartDate { get { return start_date.Value; } set { start_date.Value = value; } }
        public DateTime? DueDate { get { return due_date.Value; } set { due_date.Value = value; } }
        public Double PercentCompleted { get { return percent_completed.Value; } set { percent_completed.Value = value; } }
        public String Status { get { return status.Value; } set { status.Value = value; } }
        public String Name { get { return name.Value; } set { name.Value = value; } }
        public String Description { get { return description.Value; } set { description.Value = value; } }
        public Int32 Importance { get { return importance.Value; } set { importance.Value = value; } }
        public Int32 ContentTypeID { get { return content_type_id.Value; } set { content_type_id.Value = value; } }
        public Int32 ReferenceID { get { return reference_id.Value; } set { reference_id.Value = value; } }

        public qPtl_Task()
            : this(new DbRow())
        {
        }

        protected qPtl_Task(DbRow c)
        {
            container = c;
            container.SetContainerName("qPtl_Tasks");
            task_id = container.NewColumn<Int32>("TaskID", true);
            scope_id = container.NewColumn<Int32>("ScopeID");
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32?>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            task_type_id = container.NewColumn<Int32>("TaskTypeID");
            assigned_to = container.NewColumn<Int32?>("AssignedTo");
            assigned_text = container.NewColumn<String>("AssignedText");
            start_date = container.NewColumn<DateTime?>("StartDate");
            due_date = container.NewColumn<DateTime?>("DueDate");
            percent_completed = container.NewColumn<Double>("PercentCompleted");
            status = container.NewColumn<String>("Status");
            name = container.NewColumn<String>("Name");
            description = container.NewColumn<String>("Description");
            importance = container.NewColumn<Int32>("Importance");
            content_type_id = container.NewColumn<Int32>("ContentTypeID");
            reference_id = container.NewColumn<Int32>("ReferenceID");
        }

        public qPtl_Task(Int32 task_id)
            : this()
        {
            container.Select("TaskID = @TaskID", new SqlQueryParameter("@TaskID", task_id));
        }

        public qPtl_Task(Int32 content_type_id, Int32 reference_id)
            : this()
        {
            container.Select("ContentTypeID = @ContentTypeID AND ReferenceID = @ReferenceID", new SqlQueryParameter("@ContentTypeID", content_type_id), new SqlQueryParameter("@ReferenceID", reference_id));
        }

        public void Update()
        {
            container.Update(string.Format("TaskID = {0}", TaskID));
        }

        public void Update(Int32 scope_id, String available, DateTime created, Int32 created_by, DateTime last_modified, Int32 last_modified_by, Int32 mark_as_delete, Int32 task_type_id, Int32 assigned_to, String assigned_text, DateTime start_date, DateTime due_date, Double percent_completed, String status, String name, String description, Int32 importance, Int32 content_type_id, Int32 reference_id)
        {
            ScopeID = scope_id;
            Available = available;
            Created = created;
            CreatedBy = created_by;
            LastModified = last_modified;
            LastModifiedBy = last_modified_by;
            MarkAsDelete = mark_as_delete;
            TaskTypeID = task_type_id;
            AssignedTo = assigned_to;
            AssignedText = assigned_text;
            StartDate = start_date;
            DueDate = due_date;
            PercentCompleted = percent_completed;
            Status = status;
            Name = name;
            Description = description;
            Importance = importance;
            ContentTypeID = content_type_id;
            ReferenceID = reference_id;

            Update();
        }

        public void Insert(Int32 created_by, Int32 task_type_id, Int32? assigned_to, String assigned_text, DateTime? start_date, DateTime? due_date, Double percent_completed, String status, String name, String description, Int32 importance, Int32 content_type_id, Int32 reference_id)
        {
            Available = "Yes";
            Created = DateTime.Now;
            CreatedBy = created_by;
            MarkAsDelete = 0;
            TaskTypeID = task_type_id;
            AssignedTo = assigned_to;
            AssignedText = assigned_text;
            StartDate = start_date;
            DueDate = due_date;
            PercentCompleted = percent_completed;
            Status = status;
            Name = name;
            Description = description;
            Importance = importance;
            ContentTypeID = content_type_id;
            ReferenceID = reference_id;

            Insert();

        }

        public void Insert()
        {
            TaskID = Convert.ToInt32(container.Insert());

            // send email to site admin(s) with task info
            try
            {
                qPtl_Task new_task = new qPtl_Task(TaskID);
                SendTaskEmail(new_task.TaskTypeID, new_task.CreatedBy, new_task.ReferenceID, new_task.Description);
            }
            catch
            {
            }
        }

        public static ICollection<qPtl_Task> GetTasks (DateTime start, DateTime end)
        {
            return schema.container.Select<qPtl_Task>(
                new DbQuery
                {
                    Where = "StartDate >= @Start AND DueDate <= @End",
                    OrderBy = "StartDate",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@Start", start), new SqlQueryParameter("@End", end) }
                }, c => new qPtl_Task (c));
        }

        protected void SendTaskEmail(int task_type_id, int created_by, int reference_id, string description)
        {
            string request_task_emails = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TaskEmailList"]);
            qCom_EmailPreference preferences = new qCom_EmailPreference();
            string system_from = preferences.FromName;
            string system_email_address = preferences.FromEmailAddress;
            int curr_email_id = 0;

            qPtl_User curr_user = new qPtl_User(created_by);

            if (!String.IsNullOrEmpty(request_task_emails))
            {
                switch (task_type_id)
                {
                    case 1:     // review blog
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_blog_EmailID"]));
                        break;
                    case 2:     // approve image
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_newpic_EmailID"]));
                        break;
                    case 3:     // approve video
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_video_EmailID"]));
                        break;
                    case 4:     // review site report
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_issue_EmailID"]));
                        break;
                    case 5:     // review reported message
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_message_EmailID"]));
                        break;
                    case 6:     // review banned word
                        curr_email_id = Convert.ToInt32(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["report_banned_word_EmailID"]));
                        break;
                }

                qCom_EmailItem email = new qCom_EmailItem(curr_email_id);

                ArrayList addresses = new ArrayList();
                q_Helper helper = new q_Helper();
                addresses = helper.optionsToArrayList(request_task_emails);

                qCom_EmailTool email_send = new qCom_EmailTool();
                int sent_email_log_id = 0;

                foreach (string address in addresses)
                {
                    try
                    {
                        sent_email_log_id = email_send.SendDatabaseMail(address, curr_email_id, created_by, curr_user.UserName, Convert.ToString(reference_id), description, "", "", false);
                    }
                    catch
                    {
                        // email failure
                    }
                }
            }
        }

        public static int GetPendingTaskCount()
        {
            DateTime yesterday = new DateTime();
            yesterday = DateTime.Now;
            yesterday = yesterday.AddDays(-1);
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(TaskID) FROM qPtl_Tasks WHERE Status = 'Pending'"), CommandType.Text, null));
        }
    }
}
