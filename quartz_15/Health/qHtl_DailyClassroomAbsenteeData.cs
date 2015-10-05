using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_DailyClassroomAbsenteeData
    {
        protected static qHtl_DailyClassroomAbsenteeData schema = new qHtl_DailyClassroomAbsenteeData();

        protected DbRow container;
        protected readonly DbColumn<Int32> daily_classroom_absentee_data_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<Int32> school_id;
        protected readonly DbColumn<DateTime?> data_date;
        protected readonly DbColumn<String> school;
        protected readonly DbColumn<String> school_level;
        protected readonly DbColumn<String> grade_level;
        protected readonly DbColumn<Int32> grade_number;
        protected readonly DbColumn<String> classroom;
        protected readonly DbColumn<String> instructor;
        protected readonly DbColumn<Int32> days_in_session;
        protected readonly DbColumn<Decimal> rate;
        protected readonly DbColumn<Int32> total_absent;
        protected readonly DbColumn<Int32> total_unknown;
        protected readonly DbColumn<Int32> total_other;
        protected readonly DbColumn<Int32> total_sick;
        protected readonly DbColumn<Int32> gastrointestinal;
        protected readonly DbColumn<Int32> respiratory;
        protected readonly DbColumn<Int32> rash;
        protected readonly DbColumn<Int32> other_illness;
        protected readonly DbColumn<Int32> unknown_illness;
        protected readonly DbColumn<Int32> total_enrolled;

        public Int32 DailyClassroomAbsenteeDataID { get { return daily_classroom_absentee_data_id.Value; } set { daily_classroom_absentee_data_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Int32 SchoolID { get { return school_id.Value; } set { school_id.Value = value; } }
        public DateTime? DataDate { get { return data_date.Value; } set { data_date.Value = value; } }
        public String School { get { return school.Value; } set { school.Value = value; } }
        public String SchoolLevel { get { return school_level.Value; } set { school_level.Value = value; } }
        public String GradeLevel { get { return grade_level.Value; } set { grade_level.Value = value; } }
        public Int32 GradeNumber { get { return grade_number.Value; } set { grade_number.Value = value; } }
        public String ClassRoom { get { return classroom.Value; } set { classroom.Value = value; } }
        public String Instructor { get { return instructor.Value; } set { instructor.Value = value; } }
        public Int32 DaysInSession { get { return days_in_session.Value; } set { days_in_session.Value = value; } }
        public Decimal Rate { get { return rate.Value; } set { rate.Value = value; } }
        public Int32 TotalAbsent { get { return total_absent.Value; } set { total_absent.Value = value; } }
        public Int32 TotalUnknown { get { return total_unknown.Value; } set { total_unknown.Value = value; } }
        public Int32 TotalOther { get { return total_other.Value; } set { total_other.Value = value; } }
        public Int32 TotalSick { get { return total_sick.Value; } set { total_sick.Value = value; } }
        public Int32 Gastrointestinal { get { return gastrointestinal.Value; } set { gastrointestinal.Value = value; } }
        public Int32 Respiratory { get { return respiratory.Value; } set { respiratory.Value = value; } }
        public Int32 Rash { get { return rash.Value; } set { rash.Value = value; } }
        public Int32 OtherIllness { get { return other_illness.Value; } set { other_illness.Value = value; } }
        public Int32 UnknownIllness { get { return unknown_illness.Value; } set { unknown_illness.Value = value; } }
        public Int32 TotalEnrolled { get { return total_enrolled.Value; } set { total_enrolled.Value = value; } }

        public qHtl_DailyClassroomAbsenteeData()
            : this(new DbRow())
        {
        }

        protected qHtl_DailyClassroomAbsenteeData(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_DailyClassroomAbsenteeData");
            daily_classroom_absentee_data_id = container.NewColumn<Int32>("DailyClassroomAbsenteeDataID", true);
            created = container.NewColumn<DateTime>("Created");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            school_id = container.NewColumn<Int32>("SchoolID");
            data_date = container.NewColumn<DateTime?>("DataDate");
            school = container.NewColumn<String>("School");
            school_level = container.NewColumn<String>("SchoolLevel");
            grade_level = container.NewColumn<String>("GradeLevel");
            grade_number = container.NewColumn<Int32>("GradeNumber");
            classroom = container.NewColumn<String>("Classroom");
            instructor = container.NewColumn<String>("Instructor");
            days_in_session = container.NewColumn<Int32>("DaysInSession");
            rate = container.NewColumn<Decimal>("Rate");
            total_absent = container.NewColumn<Int32>("TotalAbsent");
            total_unknown = container.NewColumn<Int32>("TotalUnknown");
            total_other = container.NewColumn<Int32>("TotalOther");
            total_sick = container.NewColumn<Int32>("TotalSick");
            gastrointestinal = container.NewColumn<Int32>("Gastrointestinal");
            respiratory = container.NewColumn<Int32>("Respiratory");
            rash = container.NewColumn<Int32>("Rash");
            other_illness = container.NewColumn<Int32>("OtherIllness");
            unknown_illness = container.NewColumn<Int32>("UnknownIllness");
            total_enrolled = container.NewColumn<Int32>("TotalEnrolled");
        }

        public qHtl_DailyClassroomAbsenteeData(Int32 daily_classroom_absentee_data_id)
            : this()
        {
            container.Select("DailyClassroomAbsenteeDataID = @DailyClassroomAbsenteeDataID", new SqlQueryParameter("@DailyClassroomAbsenteeDataID", daily_classroom_absentee_data_id));
        }

        public qHtl_DailyClassroomAbsenteeData(DateTime data_date)
            : this()
        {
            DateTime curr_date = data_date.Date;
            container.Select("DataDate = @DataDate", new SqlQueryParameter("@DataDate", curr_date));
        }

        public void Update()
        {
            container.Update("DailyClassroomAbsenteeDataID = @DailyClassroomAbsenteeDataID");
        }

        public void Insert()
        {
            Created = DateTime.Now;

            DailyClassroomAbsenteeDataID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteClassroomAbsenteeData(int daily_classroom_absentee_data_id)
        {
            schema.container.Delete(string.Concat("DailyClassroomAbsenteeID = ", daily_classroom_absentee_data_id));
        }

        public static void DeleteClassroomAbsenteeDataByDate(string curr_date)
        {
            schema.container.Delete(string.Format(string.Format("DataDate = '{0}'", curr_date)));
        }

        public static string BuildDailyClassroomAbsenteeDataSQLByFilters(int curr_user_id, string start_time, string end_time, int school_district_id, int[] schools, string[] school_levels, string[] grade_levels)
        {
            string sql_where = string.Empty;
            string sql_where_schools = string.Empty;
            string sql_where_school_levels = string.Empty;
            string sql_where_grade_levels = string.Empty;

            sql_where = " DailyClassroomAbsenteeDataID > 0";

            if (schools.Length > 1 && schools[0] != 0)
            {
                sql_where += " AND (";
                sql_where_schools = string.Empty;
                foreach (int s in schools)
                {
                    if (!String.IsNullOrEmpty(sql_where_schools))
                        sql_where_schools += " OR SchoolID = " + s;
                    else
                        sql_where_schools += "SchoolID = " + s;
                }
                sql_where += sql_where_schools;
                sql_where += ")";
            }

            if (school_levels.Length > 1 && school_levels[0] != "all")
            {
                sql_where += " AND (";
                sql_where_school_levels = string.Empty;
                foreach (string l in school_levels)
                {
                    if (!String.IsNullOrEmpty(sql_where_school_levels))
                        sql_where_school_levels += " OR SchoolLevel = '" + l + "'";
                    else
                        sql_where_school_levels += "SchoolLevel = '" + l + "'";
                }
                sql_where += sql_where_school_levels;
                sql_where += ")";
            }

            if (grade_levels.Length > 1 && grade_levels[0] != "all")
            {
                sql_where += " AND (";
                sql_where_grade_levels = string.Empty;
                foreach (string g in grade_levels)
                {
                    if (!String.IsNullOrEmpty(sql_where_grade_levels))
                        sql_where_grade_levels += " OR GradeLevel = " + g + "'";
                    else
                        sql_where_grade_levels += "GradeLevel = " + g + "'";
                }
                sql_where += sql_where_grade_levels;
                sql_where += ")";
            }

            return sql_where;
        }

        public static DataTable GetDailyClassroomAbsenteeDataDataTable(string sql)
        {
            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql("SELECT * FROM qHtl_DailyClassroomDataAbsenteeData " + sql);

            return dt;
        }

        public static ICollection<qHtl_DailyClassroomAbsenteeData> GetDailyClassroomAbsenteeDataCollection(string sql)
        {
            return schema.container.Select<qHtl_DailyClassroomAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "DataDate ASC",
                }, c => new qHtl_DailyClassroomAbsenteeData(c));
        }

        public static ICollection<qHtl_DailyClassroomAbsenteeData> GetDailyClassroomAbsenteeDataCollectionByDate(DateTime curr_date, int school_id)
        {
            string sql = string.Empty;

            sql = "SchoolID = " + school_id + " AND DataDate = '" + Convert.ToDateTime(curr_date) + "'";

            return schema.container.Select<qHtl_DailyClassroomAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "GradeNumber ASC",
                }, c => new qHtl_DailyClassroomAbsenteeData(c));
        }

        public static List<ClassroomData> LoadDailyClassroomDataInfoList(DateTime curr_date, int school_id)
        {
            var classroom_data = GetDailyClassroomAbsenteeDataCollectionByDate(curr_date, school_id);

            List<ClassroomData> classroom_list = new List<ClassroomData>();

            if (classroom_data != null)
            {
                foreach (var d in classroom_data)
                {
                    //decimal one_sigma = 0;
                    //decimal two_sigma = 0;
                    //decimal three_sigma = 0;

                    //one_sigma = d.HistoricRate + (1 * d.HistoricRateSTD);
                    //two_sigma = d.HistoricRate + (2 * d.HistoricRateSTD);
                    //three_sigma = d.HistoricRate + (3 * d.HistoricRateSTD);

                    string classroom_color = "#3366CC";
                    string classroom_name = string.Empty;
                    if (d.GradeNumber > 0)
                        classroom_name = "Grade " + d.GradeNumber;
                    else if (d.GradeNumber == 0)
                        classroom_name = "Kinder";  // + d.Instructor;
                    else if (d.GradeNumber == -1)
                        classroom_name = "PreSch";  // + d.Instructor;
                    else
                        classroom_name = d.ClassRoom + ": " + d.Instructor;
                    /*
                    if (d.A_Warning == true || d.B_Warning == true || d.C_Warning == true || d.D_Warning == true || d.E_Warning == true)
                        school_color = "red";
                    else if (one_sigma > 0)
                    {
                        if (d.Rate > one_sigma)
                            school_color = "yellow";
                    }
                     */

                    classroom_list.Add(new ClassroomData()
                    {
                        school_id = d.SchoolID,
                        school_name = d.School,
                        days_in_session = d.DaysInSession,
                        total_absent = d.TotalAbsent,
                        total_unknown = d.TotalUnknown,
                        total_other = d.TotalOther,
                        total_sick = d.TotalSick,
                        gastrointestinal = d.Gastrointestinal,
                        respiratory = d.Respiratory,
                        rash = d.Rash,
                        other_illness = d.OtherIllness,
                        unknow_illness = d.UnknownIllness,
                        total_enrolled = d.TotalEnrolled,
                        rate = d.Rate,
                        classroom = d.ClassRoom,
                        grade_number = d.GradeNumber,
                        grade_level = d.GradeLevel,
                        instructor = d.Instructor,
                        classroom_color = classroom_color,
                        chart_classroom_name = classroom_name
                    });
                }
            }

            return classroom_list;
        }
    }

    public class ClassroomData
    {
        public DateTime data_date { get; set; }
        public int school_district_id { get; set; }
        public int school_id { get; set; }
        public string school_name { get; set; }
        public string school_level { get; set; }
        public string grade_level { get; set; }
        public int grade_number { get; set; }
        public string classroom { get; set; }
        public string instructor { get; set; }
        public int days_in_session { get; set; }
        public decimal rate { get; set; }
        public int total_absent { get; set; }
        public int total_unknown { get; set; }
        public int total_other { get; set; }
        public int total_sick { get; set; }
        public int gastrointestinal { get; set; }
        public int respiratory { get; set; }
        public int rash { get; set; }
        public int other_illness { get; set; }
        public int unknow_illness { get; set; }
        public int total_enrolled { get; set; }
        public string classroom_color { get; set; }
        public string chart_classroom_name { get; set; }
    }
}
