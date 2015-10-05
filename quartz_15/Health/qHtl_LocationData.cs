using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_LocationData
    {
        protected static qHtl_LocationData schema = new qHtl_LocationData();

        protected DbRow container;
        protected readonly DbColumn<Int32> location_data_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> meter_id;
        protected readonly DbColumn<Int32> user_id;
        protected readonly DbColumn<String> hardware_id;
        protected readonly DbColumn<String> upload_key;
        protected readonly DbColumn<Decimal> sampling_frequency;
        protected readonly DbColumn<Int64> activity_timestamp;
        protected readonly DbColumn<DateTime?> activity_datetime;
        protected readonly DbColumn<Int32> num_packets;
        protected readonly DbColumn<String> data;
        protected readonly DbColumn<Boolean> processed;
        protected readonly DbColumn<DateTime?> transferred;

        public Int32 LocationDataID { get { return location_data_id.Value; } set { location_data_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 MeterID { get { return meter_id.Value; } set { meter_id.Value = value; } }
        public Int32 UserID { get { return user_id.Value; } set { user_id.Value = value; } }
        public String HardwareID { get { return hardware_id.Value; } set { hardware_id.Value = value; } }
        public String UploadKey { get { return upload_key.Value; } set { upload_key.Value = value; } }
        public Decimal SamplingFrequency { get { return sampling_frequency.Value; } set { sampling_frequency.Value = value; } }
        public Int64 ActivityTimestamp { get { return activity_timestamp.Value; } set { activity_timestamp.Value = value; } }
        public DateTime? ActivityDateTime { get { return activity_datetime.Value; } set { activity_datetime.Value = value; } }
        public Int32 NumPackets { get { return num_packets.Value; } set { num_packets.Value = value; } }
        public String Data { get { return data.Value; } set { data.Value = value; } }
        public Boolean Processed { get { return processed.Value; } set { processed.Value = value; } }
        public DateTime? Transferred { get { return transferred.Value; } set { transferred.Value = value; } }

        public qHtl_LocationData()
            : this(new DbRow())
        {
        }

        protected qHtl_LocationData(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_LocationData");
            location_data_id = container.NewColumn<Int32>("LocationDataID", true);
            created = container.NewColumn<DateTime>("Created");
            meter_id = container.NewColumn<Int32>("MeterID");
            user_id = container.NewColumn<Int32>("UserID");
            hardware_id = container.NewColumn<String>("HardwareID");
            upload_key = container.NewColumn<String>("UploadKey");
            sampling_frequency = container.NewColumn<Decimal>("SamplingFrequency");
            activity_timestamp = container.NewColumn<Int64>("ActivityTimestamp");
            activity_datetime = container.NewColumn<DateTime?>("ActivityDatetime");
            num_packets = container.NewColumn<Int32>("NumPackets");
            data = container.NewColumn<String>("Data");
            processed = container.NewColumn<Boolean>("Processed");
            transferred = container.NewColumn<DateTime?>("Transferred");
        }

        public qHtl_LocationData(Int32 location_data_id)
            : this()
        {
            container.Select("LocationDataID = @LocationDataID", new SqlQueryParameter("@LocationDataID", location_data_id));
        }

        public qHtl_LocationData(Int32 meter_id, Int64 activity_timestamp)
            : this()
        {
            container.Select("MeterID = @MeterID AND ActivityTimestamp = @ActivityTimestamp", new SqlQueryParameter("@MeterID", meter_id), new SqlQueryParameter("@ActivityTimestamp", activity_timestamp));
        }

        public qHtl_LocationData(String hwid, Int64 activity_timestamp)
            : this()
        {
            container.Select("HardwareID = @HardwareID AND ActivityTimestamp = @ActivityTimestamp", new SqlQueryParameter("@HardwareID", hwid), new SqlQueryParameter("@ActivityTimestamp", activity_timestamp));
        }

        public qHtl_LocationData(Int32 meter_id, Int64 activity_timestamp, Boolean processed)
            : this()
        {
            container.Select("MeterID = @MeterID AND ActivityTimestamp = @ActivityTimestamp AND Processed = @Processed", new SqlQueryParameter("@LocationDataID", location_data_id), new SqlQueryParameter("@ActivityTimestamp", activity_timestamp));
        }

        public void Update()
        {
            container.Update("LocationDataID = @LocationDataID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            LocationDataID = Convert.ToInt32(container.Insert());
        }

        public void DeleteLocationData(int location_data_id)
        {
            container.Delete(string.Concat("LocationDataID = ", location_data_id));
        }

        public static ICollection<qHtl_LocationData> GetAllIntransferredLocationData()
        {
            return schema.container.Select<qHtl_LocationData>(
                new DbQuery
                {
                    Where = "MarkAsDelete = 0 AND Evaluated != 1",
                    OrderBy = "LocationDataID ASC",
                }, c => new qHtl_LocationData(c));
        }

        public static ICollection<qHtl_LocationData> GetUnprocessedDataByUserID(int user_id)
        {
            return schema.container.Select<qHtl_LocationData>(
                new DbQuery
                {
                    Where = "MarkAsDelete = 0 AND Processed != 1 AND UserID = @UserID",
                    OrderBy = "ActivityTimestamp ASC",
                    Parameters = new SqlQueryParameter[] { new SqlQueryParameter("@UserID", user_id) },
                }, c => new qHtl_LocationData(c));
        }

        public static DateTime UnixTimeStampToDateTime(Int64 unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);

            // Convert from UTC to Local Time
            DateTime dt = dtDateTime.ToLocalTime();

            return dt;
        }
    }
}
