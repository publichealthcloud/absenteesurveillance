using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_SchoolAbsenteeReport
    {
        protected static qHtl_SchoolAbsenteeReport schema = new qHtl_SchoolAbsenteeReport();

        protected DbRow container;
        protected readonly DbColumn<Int32> school_absentee_report_id;
        protected readonly DbColumn<String> available;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> created_by;
        protected readonly DbColumn<DateTime?> last_modified;
        protected readonly DbColumn<Int32> last_modified_by;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<DateTime?> start_date;
        protected readonly DbColumn<DateTime?> end_date;
        protected readonly DbColumn<String> schools;
        protected readonly DbColumn<String> school_types;
        protected readonly DbColumn<String> school_grade_levels;
        protected readonly DbColumn<String> sql;

        public Int32 SchoolAbsenteeReportID { get { return school_absentee_report_id.Value; } set { school_absentee_report_id.Value = value; } }
        public String Available { get { return available.Value; } set { available.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 CreatedBy { get { return created_by.Value; } set { created_by.Value = value; } }
        public DateTime? LastModified { get { return last_modified.Value; } set { last_modified.Value = value; } }
        public Int32 LastModifiedBy { get { return last_modified_by.Value; } set { last_modified_by.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public DateTime? StartDate { get { return start_date.Value; } set { start_date.Value = value; } }
        public DateTime? EndDate { get { return end_date.Value; } set { end_date.Value = value; } }
        public String Schools { get { return schools.Value; } set { schools.Value = value; } }
        public String SchoolTypes { get { return school_types.Value; } set { school_types.Value = value; } }
        public String SchoolGradeLevels { get { return school_grade_levels.Value; } set { school_grade_levels.Value = value; } }
        public String SQL { get { return sql.Value; } set { sql.Value = value; } }

        public qHtl_SchoolAbsenteeReport()
            : this(new DbRow())
        {
        }

        protected qHtl_SchoolAbsenteeReport(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_SchoolAbsenteeReports");
            school_absentee_report_id = container.NewColumn<Int32>("SchoolAbsenteeReportID", true);
            available = container.NewColumn<String>("Available");
            created = container.NewColumn<DateTime>("Created");
            created_by = container.NewColumn<Int32>("CreatedBy");
            last_modified = container.NewColumn<DateTime?>("LastModified");
            last_modified_by = container.NewColumn<Int32>("LastModifiedBy");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            start_date = container.NewColumn<DateTime?>("StartDate");
            end_date = container.NewColumn<DateTime?>("EndDate");
            schools = container.NewColumn<String>("Schools");
            school_types = container.NewColumn<String>("SchoolTypes");
            school_grade_levels = container.NewColumn<String>("SchoolGradeLevels");
            sql = container.NewColumn<String>("SQL");
        }

        public qHtl_SchoolAbsenteeReport(Int32 school_absentee_report_id)
            : this()
        {
            container.Select("SchoolAbsenteeReportID = @SchoolAbsenteeReportID", new SqlQueryParameter("@SchoolAbsenteeReportID", school_absentee_report_id));
        }

        public void Update()
        {
            LastModified = DateTime.Now;

            container.Update("SchoolAbsenteeReportID = @SchoolAbsenteeReportID");
        }

        public void Insert()
        {
            Created = DateTime.Now;
            LastModified = DateTime.Now;

            SchoolAbsenteeReportID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteMeter(int school_absentee_report_id)
        {
            schema.container.Delete(string.Concat("SchoolAbsenteeReportID = ", school_absentee_report_id));
        }
    }
}
