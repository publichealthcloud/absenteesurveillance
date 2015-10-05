using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_DailyPhysicalActivity
    {
        protected static qHtl_DailyPhysicalActivity schema = new qHtl_DailyPhysicalActivity();

        protected DbRow container;
        protected readonly DbColumn<Int32> daily_physical_activity_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<DateTime> last_modified;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<Int32> meter_id;
        protected readonly DbColumn<DateTime> activity_date;
        protected readonly DbColumn<Int32> seconds_vma;
        protected readonly DbColumn<Int32> seconds_zone;
        protected readonly DbColumn<Int32> steps;
        protected readonly DbColumn<Int32> calories_burned;

        public Int32 DailyPhysicalActivityID { get { return daily_physical_activity_id.Value; } set { daily_physical_activity_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public DateTime LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public Int32 MeterID { get { return meter_id.Value; } set { meter_id.Value = value; } }
        public DateTime ActivityDate { get { return activity_date.Value; } set { activity_date.Value = value; } }
        public Int32 SecondsVMA { get { return seconds_vma.Value; } set { seconds_vma.Value = value; } }
        public Int32 SecondsZone { get { return seconds_zone.Value; } set { seconds_zone.Value = value; } }
        public Int32 Steps { get { return steps.Value; } set { steps.Value = value; } }
        public Int32 CaloriesBurned { get { return calories_burned.Value; } set { calories_burned.Value = value; } }

        public qHtl_DailyPhysicalActivity()
            : this(new DbRow())
        {
        }

        protected qHtl_DailyPhysicalActivity(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_DailyPhysicalActivity");
            daily_physical_activity_id = container.NewColumn<Int32>("DailyPhysicalActivityID", true);
            created = container.NewColumn<DateTime>("Created");
            last_modified = container.NewColumn<DateTime>("LastModified");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            user_id = container.NewColumn<Int32>("UserID");
            meter_id = container.NewColumn<Int32>("MeterID");
            activity_date = container.NewColumn<DateTime>("ActivityDate");
            seconds_vma = container.NewColumn<Int32>("SecondsVMA");
            seconds_zone = container.NewColumn<Int32>("SecondsZone");
            calories_burned = container.NewColumn<Int32>("CaloriesBurned");
        }

        public qHtl_DailyPhysicalActivity(Int32 daily_physical_activity_id)
            : this()
        {
            container.Select("DailyPhysicalActivityID = @DailyPhysicalActivityID", new SqlQueryParameter("@DailyPhysicalActivityID", daily_physical_activity_id));
        }

        public qHtl_DailyPhysicalActivity(Int32 user_id, DateTime activity_date)
            : this()
        {
            string start_time = Convert.ToString(activity_date.Date);
            string end_time = Convert.ToString(activity_date.Date.AddDays(1));

            container.Select("UserID = @UserID AND ActivityDate BETWEEN @StartTime AND @EndTime", new SqlQueryParameter("@UserID", user_id), new SqlQueryParameter("@StartTime", start_time), new SqlQueryParameter("@EndTime", end_time));
        }

        public void Update()
        {
            container.Update("DailyPhysicalActivityID = @DailyPhysicalActivityID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            DailyPhysicalActivityID = Convert.ToInt32(container.Insert());
        }

        public static qHtl_DailyPhysicalActivity GetPhysicalActivityRecord(int user_id, DateTime day)
        {
            qHtl_DailyPhysicalActivity record = new qHtl_DailyPhysicalActivity();

            record.container.Select(new DbQuery
            {
                Where = string.Format("MarkAsDelete = 0 AND UserID = {0} AND ActivityDate = '{1}'", user_id, day.Date)
            });

            if (record.DailyPhysicalActivityID > 0) return record;
            else return null;
        }
    }
}
