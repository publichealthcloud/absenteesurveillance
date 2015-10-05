using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Quartz.Communication
{
    public class qCom_TempBulkEmailLog
    {
        protected static qCom_TempBulkEmailLog schema = new qCom_TempBulkEmailLog();

        protected DbRow container;
        protected readonly DbColumn<Int32> temp_bulk_email_log_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> email_item_id;
        protected readonly DbColumn<String> email_address;
        protected readonly DbColumn<DateTime?> timestamp;

        public Int32 TempBulkEmailLogID { get { return temp_bulk_email_log_id.Value; } set { temp_bulk_email_log_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 EmailItemID { get { return email_item_id.Value; } set { email_item_id.Value = value; } }
        public String EmailAddress { get { return email_address.Value; } set { email_address.Value = value; } }
        public DateTime? Timestamp { get { return timestamp.Value; } set { timestamp.Value = value; } }

        public qCom_TempBulkEmailLog()
            : this(new DbRow())
        {
        }

        protected qCom_TempBulkEmailLog(DbRow c)
        {
            container = c;
            container.SetContainerName("qCom_TempBulkEmailLogs");
            temp_bulk_email_log_id = container.NewColumn<Int32>("TempBulkEmailLogID", true);
            user_id = container.NewColumn<Int32>("UserID");
            email_item_id = container.NewColumn<Int32>("EmailItemID");
            email_address = container.NewColumn<String>("EmailAddress");
            timestamp = container.NewColumn<DateTime?>("Timestamp");
        }

        public qCom_TempBulkEmailLog(Int32 temp_bulk_email_log_id)
            : this()
        {
            container.Select("TempBulkEmailLogID = @TempBulkEmailLogID", new SqlQueryParameter("@TempBulkEmailLogID", temp_bulk_email_log_id));
        }

        public void Update()
        {
            container.Update("TempBulkEmailLogID = @TempBulkEmailLogID");
        }

        public void Insert()
        {
            TempBulkEmailLogID = Convert.ToInt32(container.Insert());
        }
    }
}
