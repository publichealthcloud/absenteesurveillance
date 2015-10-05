using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;
using Quartz.Organization;

namespace Quartz.Health
{
    public class qHtl_DailySchoolAbsenteeData
    {
        protected static qHtl_DailySchoolAbsenteeData schema = new qHtl_DailySchoolAbsenteeData();

        protected DbRow container;
        protected readonly DbColumn<Int32> daily_school_absentee_data_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<Int32> school_id;
        protected readonly DbColumn<DateTime?> data_date;
        protected readonly DbColumn<String> school;
        protected readonly DbColumn<String> school_level;
        protected readonly DbColumn<Int32> days_in_session;
        protected readonly DbColumn<Decimal> rate;
        protected readonly DbColumn<Decimal> two_week_rate;
        protected readonly DbColumn<Decimal> prior_year_rate;
        protected readonly DbColumn<Decimal> historic_rate;
        protected readonly DbColumn<Decimal> rate_std;
        protected readonly DbColumn<Decimal> historic_rate_std;
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
        protected readonly DbColumn<Decimal> illness_rate;
        protected readonly DbColumn<Decimal> gastrointestinal_rate;
        protected readonly DbColumn<Decimal> respiratory_rate;
        protected readonly DbColumn<Decimal> rash_rate;
        protected readonly DbColumn<Decimal> other_rate;
        protected readonly DbColumn<Decimal> unknown_rate;
        protected readonly DbColumn<String> absentee_status;
        protected readonly DbColumn<String> absentee_status_7day;
        protected readonly DbColumn<String> absentee_status_school;
        protected readonly DbColumn<String> absentee_status_school_7day;
        protected readonly DbColumn<String> illness_status;
        protected readonly DbColumn<String> gastrointestinal_status;
        protected readonly DbColumn<String> respiratory_status;
        protected readonly DbColumn<String> rash_status;
        protected readonly DbColumn<String> other_illness_status;
        protected readonly DbColumn<String> unknown_illness_status;
        protected readonly DbColumn<Boolean> a_warning;
        protected readonly DbColumn<String> a_warning_values;
        protected readonly DbColumn<Boolean> b_warning;
        protected readonly DbColumn<String> b_warning_values;
        protected readonly DbColumn<Boolean> c_warning;
        protected readonly DbColumn<String> c_warning_values;
        protected readonly DbColumn<Boolean> d_warning;
        protected readonly DbColumn<String> d_warning_values;
        protected readonly DbColumn<Boolean> e_warning;
        protected readonly DbColumn<String> e_warning_values;
        protected readonly DbColumn<Boolean> f_warning;
        protected readonly DbColumn<String> f_warning_values;

        public Int32 DailySchoolAbsenteeDataID { get { return daily_school_absentee_data_id.Value; } set { daily_school_absentee_data_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Int32 SchoolID { get { return school_id.Value; } set { school_id.Value = value; } }
        public DateTime? DataDate { get { return data_date.Value; } set { data_date.Value = value; } }
        public String School { get { return school.Value; } set { school.Value = value; } }
        public String SchoolLevel { get { return school_level.Value; } set { school_level.Value = value; } }
        public Int32 DaysInSession { get { return days_in_session.Value; } set { days_in_session.Value = value; } }
        public Decimal Rate { get { return rate.Value; } set { rate.Value = value; } }
        public Decimal TwoWeekRate { get { return two_week_rate.Value; } set { two_week_rate.Value = value; } }
        public Decimal PriorYearRate { get { return prior_year_rate.Value; } set { prior_year_rate.Value = value; } }
        public Decimal HistoricRate { get { return historic_rate.Value; } set { historic_rate.Value = value; } }
        public Decimal RateSTD { get { return rate_std.Value; } set { rate_std.Value = value; } }
        public Decimal HistoricRateSTD { get { return historic_rate_std.Value; } set { historic_rate_std.Value = value; } }
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
        public Decimal IllnessRate { get { return illness_rate.Value; } set { illness_rate.Value = value; } }
        public Decimal GastrointestinalRate { get { return gastrointestinal_rate.Value; } set { gastrointestinal_rate.Value = value; } }
        public Decimal RespiratoryRate { get { return respiratory_rate.Value; } set { respiratory_rate.Value = value; } }
        public Decimal RashRate { get { return rash_rate.Value; } set { rash_rate.Value = value; } }
        public Decimal OtherRate { get { return other_rate.Value; } set { other_rate.Value = value; } }
        public Decimal UnknownRate { get { return unknown_rate.Value; } set { unknown_rate.Value = value; } }
        public String AbsenteeStatus { get { return absentee_status.Value; } set { absentee_status.Value = value; } }
        public String AbsenteeStatus7Day { get { return absentee_status_7day.Value; } set { absentee_status_7day.Value = value; } }
        public String AbsenteeStatusSchool { get { return absentee_status_school.Value; } set { absentee_status_school.Value = value; } }
        public String AbsenteeStatusSchool7Day { get { return absentee_status_school_7day.Value; } set { absentee_status_school_7day.Value = value; } }
        public String IllnessStatus { get { return illness_status.Value; } set { illness_status.Value = value; } }
        public String GastrointestinalStatus { get { return gastrointestinal_status.Value; } set { gastrointestinal_status.Value = value; } }
        public String RespiratoryStatus { get { return respiratory_status.Value; } set { respiratory_status.Value = value; } }
        public String RashStatus { get { return rash_status.Value; } set { rash_status.Value = value; } }
        public String OtherIllnessStatus { get { return other_illness_status.Value; } set { other_illness_status.Value = value; } }
        public String UnknownIllnessStatus { get { return unknown_illness_status.Value; } set { unknown_illness_status.Value = value; } }
        public Boolean A_Warning { get { return a_warning.Value; } set { a_warning.Value = value; } }
        public String A_WarningValues { get { return a_warning_values.Value; } set { a_warning_values.Value = value; } }
        public Boolean B_Warning { get { return b_warning.Value; } set { b_warning.Value = value; } }
        public String B_WarningValues { get { return b_warning_values.Value; } set { b_warning_values.Value = value; } }
        public Boolean C_Warning { get { return c_warning.Value; } set { c_warning.Value = value; } }
        public String C_WarningValues { get { return c_warning_values.Value; } set { c_warning_values.Value = value; } }
        public Boolean D_Warning { get { return d_warning.Value; } set { d_warning.Value = value; } }
        public String D_WarningValues { get { return d_warning_values.Value; } set { d_warning_values.Value = value; } }
        public Boolean E_Warning { get { return e_warning.Value; } set { e_warning.Value = value; } }
        public String E_WarningValues { get { return e_warning_values.Value; } set { e_warning_values.Value = value; } }
        public Boolean F_Warning { get { return f_warning.Value; } set { f_warning.Value = value; } }
        public String F_WarningValues { get { return f_warning_values.Value; } set { f_warning_values.Value = value; } }

        public qHtl_DailySchoolAbsenteeData()
            : this(new DbRow())
        {
        }

        protected qHtl_DailySchoolAbsenteeData(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_DailySchoolAbsenteeData");
            daily_school_absentee_data_id = container.NewColumn<Int32>("DailySchoolAbsenteeDataID", true);
            created = container.NewColumn<DateTime>("Created");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            school_id = container.NewColumn<Int32>("SchoolID");
            data_date = container.NewColumn<DateTime?>("DataDate");
            school = container.NewColumn<String>("School");
            school_level = container.NewColumn<String>("SchoolLevel");
            days_in_session = container.NewColumn<Int32>("DaysInSession");
            rate = container.NewColumn<Decimal>("Rate");
            two_week_rate = container.NewColumn<Decimal>("TwoWeekRate");
            prior_year_rate = container.NewColumn<Decimal>("PriorYearRate");
            historic_rate = container.NewColumn<Decimal>("HistoricRate");
            rate_std = container.NewColumn<Decimal>("RateSTD");
            historic_rate_std = container.NewColumn<Decimal>("HistoricRateSTD");
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
            illness_rate = container.NewColumn<Decimal>("IllnessRate");
            gastrointestinal_rate = container.NewColumn<Decimal>("GastrointestinalRate");
            respiratory_rate = container.NewColumn<Decimal>("RespiratoryRate");
            rash_rate = container.NewColumn<Decimal>("RashRate");
            other_rate = container.NewColumn<Decimal>("OtherRate"); 
            unknown_rate = container.NewColumn<Decimal>("UnknownRate");
            absentee_status = container.NewColumn<String>("AbsenteeStatus");
            absentee_status_7day = container.NewColumn<String>("AbsenteeStatus7Day");
            absentee_status_school = container.NewColumn<String>("AbsenteeStatusSchool");
            absentee_status_school_7day = container.NewColumn<String>("AbsenteeStatusSchool7Day");
            illness_status = container.NewColumn<String>("IllnessStatus");
            gastrointestinal_status = container.NewColumn<String>("GastrointestinalStatus");
            respiratory_status = container.NewColumn<String>("RespiratoryStatus");
            rash_status = container.NewColumn<String>("RashStatus");
            other_illness_status = container.NewColumn<String>("OtherIllnessStatus");
            unknown_illness_status = container.NewColumn<String>("UnknownIllnessStatus");
            a_warning = container.NewColumn<Boolean>("A_Warning");
            a_warning_values = container.NewColumn<String>("A_WarningValues");
            b_warning = container.NewColumn<Boolean>("B_Warning");
            b_warning_values = container.NewColumn<String>("B_WarningValues");
            c_warning = container.NewColumn<Boolean>("C_Warning");
            c_warning_values = container.NewColumn<String>("C_WarningValues");
            d_warning = container.NewColumn<Boolean>("D_Warning");
            d_warning_values = container.NewColumn<String>("D_WarningValues");
            e_warning = container.NewColumn<Boolean>("E_Warning");
            e_warning_values = container.NewColumn<String>("E_WarningValues");
            f_warning = container.NewColumn<Boolean>("F_Warning");
            f_warning_values = container.NewColumn<String>("F_WarningValues");
        }

        public qHtl_DailySchoolAbsenteeData(Int32 daily_school_absentee_data_id)
            : this()
        {
            container.Select("DailySchoolAbsenteeDataID = @DailySchoolAbsenteeDataID", new SqlQueryParameter("@DailySchoolAbsenteeDataID", daily_school_absentee_data_id));
        }

        public qHtl_DailySchoolAbsenteeData(DateTime data_date)
            : this()
        {
            DateTime curr_date = data_date.Date;
            container.Select("DataDate = @DataDate", new SqlQueryParameter("@DataDate", curr_date));
        }

        public qHtl_DailySchoolAbsenteeData(String school, DateTime data_date)
            : this()
        {
            container.Select("School = @School AND DataDate = @DataDate", new SqlQueryParameter("@School", school), new SqlQueryParameter("DataDate", data_date));
        }

        public qHtl_DailySchoolAbsenteeData(int school_id, DateTime data_date)
            : this()
        {
            container.Select("SchoolID = @SchoolID AND DataDate = @DataDate", new SqlQueryParameter("@SchoolID", school_id), new SqlQueryParameter("DataDate", data_date));
        }

        public void Update()
        {
            container.Update("DailySchoolAbsenteeDataID = @DailySchoolAbsenteeDataID");
        }

        public void Insert()
        {
            Created = DateTime.Now;

            DailySchoolAbsenteeDataID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteSchoolAbsenteeData(int daily_school_absentee_data_id)
        {
            schema.container.Delete(string.Concat("DailySchoolAbsenteeID = ", daily_school_absentee_data_id));
        }

        public static void DeleteSchoolAbsenteeDataByDate(string curr_date)
        {
            schema.container.Delete(string.Format(string.Format("DataDate = '{0}'", curr_date)));
        }

        public static string BuildDailySchoolAbsenteeDataSQLByFilters(int curr_user_id, string start_time, string end_time, int school_district_id, int[] schools)
        {
            string sql_where = string.Empty;
            string sql_where_schools = string.Empty;
            string sql_where_school_levels = string.Empty;
            string sql_where_grade_levels = string.Empty;

            sql_where = " DailySchoolAbsenteeDataID > 0";

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

            return sql_where;
        }

        public static DataTable GetDailySchoolAbsenteeDataDataTable(string sql)
        {
            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql("SELECT * FROM qHtl_DailySchoolDataAbsenteeData " + sql);

            return dt;
        }

        public static qHtl_DailySchoolAbsenteeData GetMostRecentDailySummary(int school_id)
        {
            qHtl_DailySchoolAbsenteeData summary = new qHtl_DailySchoolAbsenteeData();

            summary.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("SchoolID = " + school_id + " AND MarkAsDelete = 0"),
                OrderBy = "DataDate DESC"
            });

            if (summary.DailySchoolAbsenteeDataID > 0) return summary;
            else return null;
        }

        public static qHtl_DailySchoolAbsenteeData GetSchoolDailySummaryByDate(int school_id, string curr_date)
        {
            qHtl_DailySchoolAbsenteeData summary = new qHtl_DailySchoolAbsenteeData();

            summary.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("SchoolID = " + school_id + " AND MarkAsDelete = 0 AND DataDate = '" + curr_date + "'"),
                OrderBy = "DataDate DESC"
            });

            if (summary.DailySchoolAbsenteeDataID > 0) return summary;
            else return null;
        }

        public static qHtl_DailySchoolAbsenteeData GetDailySchoolSummaryPriorOrAfter(string direction, string curr_date)
        {
            qHtl_DailySchoolAbsenteeData summary = new qHtl_DailySchoolAbsenteeData();

            DateTime eval_date = new DateTime();
            eval_date = Convert.ToDateTime(curr_date);
            string sql = string.Empty;
            string order_by = string.Empty;

            if (direction == "prior")
            {
                sql = "MarkAsDelete = 0 AND DataDate < '" + curr_date + "'";
                order_by = "DataDate DESC";
            }
            else if (direction == "after")
            {
                sql = "MarkAsDelete = 0 AND DataDate > '" + curr_date + "'";
                order_by = "DataDate ASC";
            }

            curr_date = Convert.ToString(eval_date.ToShortDateString());

            summary.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = sql,
                OrderBy = order_by
            });

            if (summary.DailySchoolAbsenteeDataID > 0) return summary;
            else return null;
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetDailySchoolAbsenteeDataCollection(string sql)
        {
            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "DataDate ASC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetDailySchoolAbsenteeDataCollectionByDateRange(DateTime start_date, DateTime end_date)
        {
            string sql = string.Empty;

            sql = "DataDate BETWEEN '" + Convert.ToDateTime(start_date.AddHours(-1)) + "' AND '" + Convert.ToDateTime(end_date.AddHours(1)) + "'";
            
            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "School ASC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetDailySchoolAbsenteeDataCollectionByDateRangeAndSchool(DateTime start_date, DateTime end_date, int school_id)
        {
            string sql = string.Empty;

            sql = "SchoolID = " + school_id + " AND DataDate BETWEEN '" + Convert.ToDateTime(start_date.AddHours(-1)) + "' AND '" + Convert.ToDateTime(end_date.AddHours(1)) + "'";
            
            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "DataDate ASC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetDailySchoolAbsenteeDataCollectionByDate(DateTime curr_date)
        {
            string sql = string.Empty;

            sql = "DataDate = '" + Convert.ToDateTime(curr_date) + "'";

            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "School ASC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetRedSymptomWarningsByDate(DateTime curr_date)
        {
            string sql = string.Empty;

            sql = "(GastrointestinalStatus = 'red' OR RespiratoryStatus = 'red' OR RashStatus = 'red') AND DataDate = '" + Convert.ToDateTime(curr_date) + "'";

            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "School ASC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static ICollection<qHtl_DailySchoolAbsenteeData> GetDailySchoolAbsenteeDataCollectionByDateRangeAndSchool(int num_records_to_return, DateTime end_date, int school_id)
        {
            string sql = string.Empty;

            sql = "SchoolID = " + school_id + " AND DataDate <= '" + Convert.ToDateTime(end_date.Date) + "'";

            return schema.container.Select<qHtl_DailySchoolAbsenteeData>(
                new DbQuery
                {
                    Top = "TOP(8)",
                    Where = sql,
                    OrderBy = "DataDate DESC",
                }, c => new qHtl_DailySchoolAbsenteeData(c));
        }

        public static int CountNumberAEWarningsByDateRange(DateTime start_date, DateTime end_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(DailySchoolAbsenteeDataID) FROM qHtl_DailySchoolAbsenteeData WHERE DataDate BETWEEN '" + Convert.ToDateTime(start_date.AddHours(-1)) + "' AND '" + Convert.ToDateTime(end_date.AddHours(1)) + "' AND (A_Warning = 1 OR B_Warning = 1 OR C_Warning = 1 OR D_Warning = 1 OR E_Warning = 1)"), CommandType.Text, null));
        }

        public static int CountNumberAEWarningsByDate(DateTime curr_date)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(DailySchoolAbsenteeDataID) FROM qHtl_DailySchoolAbsenteeData WHERE DataDate = '" + Convert.ToDateTime(curr_date) + "' AND (A_Warning = 1 OR B_Warning = 1 OR C_Warning = 1 OR D_Warning = 1 OR E_Warning = 1)"), CommandType.Text, null));
        }

        public static int CountNumberAEWarningsByDateAndSchool(DateTime curr_date, int school_id)
        {
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT(DailySchoolAbsenteeDataID) FROM qHtl_DailySchoolAbsenteeData WHERE SchoolID = " + school_id + " AND DataDate = '" + Convert.ToDateTime(curr_date) + "' AND (A_Warning = 1 OR B_Warning = 1 OR C_Warning = 1 OR D_Warning = 1 OR E_Warning = 1)"), CommandType.Text, null));
        }

        public static int AnalyzeDailySchoolDataByDate(DateTime curr_date, int school_district_id)
        {
            int schools_analyzed = 0;

            var schools = qHtl_DailySchoolAbsenteeData.GetDailySchoolAbsenteeDataCollectionByDate(curr_date);

            if (schools != null)
            {
                if (schools.Count > 0)
                {
                    qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);

                    foreach (var s in schools)
                    {
                        // delete any warnings for this school on this date
                        qHtl_HealthWarning.DeleteWarningsBySchoolAndDate(s.SchoolID, Convert.ToString(s.DataDate));

                        var summary = new qHtl_DailySchoolDistrictAbsenteeSummary();

                        if (summary != null)
                        {
                            if (summary.DataDate != s.DataDate)
                                summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetDailySummaryByDate(Convert.ToString(s.DataDate));
                        }
                        else
                        {
                            summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetDailySummaryByDate(Convert.ToString(s.DataDate));
                        }

                        bool a_warning_status = false;
                        string a_warning_evals = string.Empty;
                        bool b_warning_status = false;
                        string b_warning_evals = string.Empty;
                        bool c_warning_status = false;
                        string c_warning_evals = string.Empty;
                        bool d_warning_status = false;
                        string d_warning_evals = string.Empty;
                        bool e_warning_status = false;
                        string e_warning_evals = string.Empty;
                        bool f_warning_status = false;
                        string f_warning_evals = string.Empty;
                        string absentee_status = "green";
                        string absentee_status_7day = "green";
                        string absentee_status_school = "green";
                        string absentee_status_school_7day = "green";
                        string illness_status = "green";
                        string gast_status = "green";
                        string resp_status = "green";
                        string rash_status = "green";
                        string othr_status = "green";
                        string unkn_status = "green";
                        string warning_type = "School Daily Absentee Status Warning";
                        string warning_status = string.Empty;
                        string warning_title = string.Empty;
                        string warning_text = string.Empty;

                        int num_warnings = 0;

                        // perform A-E warning analysis
                        DateTime anal_date = new DateTime();
                        anal_date = Convert.ToDateTime(s.DataDate);
                        var data_days = GetDailySchoolAbsenteeDataCollectionByDateRangeAndSchool(8, anal_date.AddDays(1), s.SchoolID);
                        int x = 0;
                        bool run_check = true;
                        decimal curr_rate = 0;
                        decimal prior_rate = 0;

                        // check A -- One point/day above 3 sigma
                        decimal historic_avg = 0;
                        decimal curr_std = 0;
                        decimal a_eval = 0;
                        foreach (var d in data_days)
                        {
                            x++;
                            if (x == 1)
                            {
                                curr_rate = d.Rate;
                                curr_std = d.RateSTD;
                                historic_avg = d.HistoricRate;
                            }
                        }
                        if (curr_std > 0)
                        {
                            a_eval = historic_avg + (3 * curr_std);
                            a_warning_evals = Convert.ToString(Math.Round(a_eval, 2));
                            if (curr_rate > a_eval)
                                a_warning_status = true;
                        }

                        // check F -- 1 day above 2 sigma
                        decimal f_eval = 0;
                        foreach (var d in data_days)
                        {
                            x++;
                            if (x == 1)
                            {
                                curr_rate = d.Rate;
                                curr_std = d.RateSTD;
                                historic_avg = d.HistoricRate;
                            }
                        }
                        if (curr_std > 0)
                        {
                            f_eval = historic_avg + (2 * curr_std);
                            f_warning_evals = Convert.ToString(Math.Round(f_eval, 2));
                            if (curr_rate > f_eval)
                                f_warning_status = true;
                        }

                        // check B -- 2 out of 3 days above 2 standard deviations
                        x = 0;
                        curr_std = 0;
                        curr_rate = 0;
                        historic_avg = 0;
                        decimal b_value = 0;
                        int b_meets = 0;
                        foreach (var d in data_days)
                        {
                            if (x < 3)
                            {
                                curr_rate = d.Rate;
                                curr_std = d.RateSTD;
                                historic_avg = d.HistoricRate;

                                if (x == 0 || x == 1 || x == 2)
                                {
                                    b_value = historic_avg + (2 * curr_std);
                                    b_warning_evals += " " + Convert.ToString(Math.Round(b_value, 2));
                                    if (curr_rate > b_value)
                                        b_meets++;
                                }
                            }
                            x++;
                        }
                        if (b_meets >= 2)
                            b_warning_status = true;

                        // check C -- 4 out of 5 days above 1 standard deviations
                        x = 0;
                        curr_std = 0;
                        curr_rate = 0;
                        historic_avg = 0;
                        decimal c_value = 0;
                        int c_meets = 0;
                        foreach (var d in data_days)
                        {
                            if (x < 5)
                            {
                                curr_rate = d.Rate;
                                curr_std = d.RateSTD;
                                historic_avg = d.HistoricRate;

                                if (x == 0 || x == 1 || x == 2 || x == 3 || x == 4)
                                {
                                    c_value = historic_avg + (1 * curr_std);
                                    c_warning_evals += " " + Convert.ToString(Math.Round(c_value, 2));
                                    if (curr_rate > c_value)
                                        c_meets++;
                                }
                            }
                            x++;
                        }
                        if (c_meets >= 4)
                            c_warning_status = true;

                        // check D -- 8 consecutive days above the moving average
                        x = 0;
                        int d_check_threshold = 8;
                        run_check = true;
                        int d_num_days = 0;
                        decimal moving = 0;
                        prior_rate = 0;
                        foreach (var d in data_days)
                        {
                            if (x == 0)
                                moving = d.HistoricRate;
                            
                            if (x < d_check_threshold)
                            {
                                curr_rate = d.Rate;
                                if (run_check == true)
                                {
                                    if (curr_rate > moving)
                                    {
                                        run_check = true;
                                        d_num_days++;
                                    }
                                    else
                                        run_check = false;
                                }
                                prior_rate = curr_rate;
                                d_warning_evals += " " + Convert.ToString(Math.Round(curr_rate, 2));
                                x++;
                            }
                        }
                        if (d_num_days == d_check_threshold)
                            d_warning_status = true;

                        // check E -- 6 days of increasing absentee rates
                        x = 0;
                        int e_check_threshold = 6;
                        run_check = true;
                        int e_num_days = 0;
                        curr_rate = 0;
                        prior_rate = 0;
                        foreach (var d in data_days)
                        {
                            if (x < e_check_threshold)
                            {
                                curr_rate = d.Rate;
                                if (run_check == true)
                                {
                                    if (curr_rate < prior_rate)
                                    {
                                        run_check = true;
                                        e_num_days++;
                                    }
                                    else
                                        run_check = false;
                                }
                                prior_rate = curr_rate;
                                e_warning_evals += " " + Convert.ToString(Math.Round(curr_rate, 2));
                                x++;
                            }
                        }
                        if (e_num_days == e_check_threshold)
                            e_warning_status = true;


                        // perform analysis
                        if (s.Rate < (summary.OverallAbsenteeSTD * variables.GreenRateSTDMultiplier))
                            absentee_status = "green";
                        else if (s.Rate >= (summary.OverallAbsenteeSTD * variables.YellowRateSTDMultiplier))
                        {
                            absentee_status = "yellow";
                            warning_status += "yellow ";
                            warning_status += "red ";
                            warning_title = "Yellow Status | Absentee Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Absentee Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Absentee rate of " + Math.Round(s.Rate, 2) + "% is greater than " + (summary.OverallAbsenteeSTD * variables.YellowRateSTDMultiplier) + "% or less than " + (summary.OverallAbsenteeSTD * variables.RedRateSTDMultiplier) + "% of the school's enrollment.<br><br>";
                            num_warnings++;
                        }
                        else if (s.Rate >= (s.RateSTD * variables.RedRateSTDMultiplier))
                        {
                            absentee_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Absentee Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Absentee Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Absentee rate of " + Math.Round(s.Rate, 2) + "% is greater than " + (summary.OverallAbsenteeSTD * variables.RedRateSTDMultiplier) + " of the school's enrollment.<br><br>";
                            num_warnings++;
                        }

                        if (s.IllnessRate < variables.GreenIllnessBoundary)
                            illness_status = "green";
                        else if (s.IllnessRate >= variables.GreenIllnessBoundary && s.IllnessRate <= variables.RedIllnessBoundary)
                        {
                            illness_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Illness rate of " + Math.Round(s.IllnessRate, 2) + "% is between " + variables.GreenIllnessBoundary + " and " + variables.RedIllnessBoundary + " of the school's enrollment.<br><br>";
                            num_warnings++;
                        }
                        else if (s.IllnessRate > variables.RedIllnessBoundary)
                        {
                            illness_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Illness rate of " + Math.Round(s.IllnessRate, 2) + "% is greater than " + variables.RedIllnessBoundary + " of the school's enrollment.<br><br>";
                            num_warnings++;
                        }

                        if (s.GastrointestinalRate < variables.GreenGastrointestinalBoundary)
                            gast_status = "green";
                        else if (s.GastrointestinalRate >= variables.GreenGastrointestinalBoundary && s.GastrointestinalRate <= variables.RedGastrointestinalBoundary)
                        {
                            gast_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Gastrintestinal Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Gastrintestinal Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Gastrintestinal symptom rate of " + Math.Round(s.GastrointestinalRate, 2) + "% is between " + variables.GreenGastrointestinalBoundary + " and " + variables.RedGastrointestinalBoundary + " of all school absences with reported symptoms.<br><br>";
                            num_warnings++;
                        }
                        else if (s.GastrointestinalRate > variables.RedGastrointestinalBoundary)
                        {
                            gast_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Gastrintestinal Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Gastrintestinal Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Gastrintestinal symptom rate of " + Math.Round(s.GastrointestinalRate, 2) + "% is greater than " + variables.RedGastrointestinalBoundary + " of all school absences with reported symptoms.<br><br>"; ;
                            num_warnings++;
                        }

                        if (s.RespiratoryRate < variables.GreenRespiratoryBoundary)
                            resp_status = "green";
                        else if (s.RespiratoryRate >= variables.GreenRespiratoryBoundary && s.RespiratoryRate <= variables.RedRespiratoryBoundary)
                        {
                            resp_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Respiratory Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Respiratory Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Respiratory symptom rate of " + Math.Round(s.RespiratoryRate, 2) + "% is between " + variables.GreenRespiratoryBoundary + " and " + variables.RedRespiratoryBoundary + " of all school absences with reported symptoms.<br><br>";
                            num_warnings++;
                        }
                        else if (s.RespiratoryRate > variables.RedRespiratoryBoundary)
                        {
                            resp_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Respiratory Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Respiratory Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Respiratory symptom rate of " + Math.Round(s.RespiratoryRate, 2) + "% is greater than " + variables.RedRespiratoryBoundary + "% of all school absences with reported symptoms.<br><br>"; ;
                            num_warnings++;
                        }

                        if (s.RashRate < variables.GreenRashBoundary)
                            rash_status = "green";
                        else if (s.RashRate >= variables.GreenRashBoundary && s.RespiratoryRate <= variables.RedRashBoundary)
                        {
                            rash_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Rash Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Rash Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Rash symptom rate of " +Math.Round(s.RashRate, 2) + "% is between " + variables.GreenRashBoundary + "% and " + variables.RedRashBoundary + "% of all school absences with reported symptoms.<br><br>";
                            num_warnings++;
                        }
                        else if (s.RashRate > variables.RedRashBoundary)
                        {
                            rash_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Rash Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Rash Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Rash symptom rate of " + Math.Round(s.RashRate, 2) + "% is greater than " + variables.RedRashBoundary + "% of all school absences with reported symptoms.<br><br>"; ;
                            num_warnings++;
                        }

                        if (s.OtherRate < variables.GreenOtherIllnessBoundary)
                            othr_status = "green";
                        else if (s.OtherRate >= variables.GreenOtherIllnessBoundary && s.RespiratoryRate <= variables.RedOtherIllnessBoundary)
                        {
                            othr_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Other Illness Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Other Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Other illness symptom rate of " + Math.Round(s.OtherRate, 2) + "% is between " + variables.GreenOtherIllnessBoundary + "% and " + variables.RedOtherIllnessBoundary + "% of all school absences with reported symptoms.<br><br>";
                            num_warnings++;
                        }
                        else if (s.OtherRate > variables.RedOtherIllnessBoundary)
                        {
                            othr_status = "red";
                            warning_status += "yellow ";
                            warning_title = "Red Status | Other Illness Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Other Illness Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Other illness symptom rate of " + Math.Round(s.OtherRate, 2) + "% is greater than " + variables.RedOtherIllnessBoundary + "% of all school absences with reported symptoms.<br><br>"; ;
                            num_warnings++;
                        }

                        if (s.UnknownRate < variables.GreenUnknownIllnessBoundary)
                            unkn_status = "green";
                        else if (s.UnknownRate >= variables.GreenUnknownIllnessBoundary && s.RespiratoryRate <= variables.RedUnknownIllnessBoundary)
                        {
                            unkn_status = "yellow";
                            warning_status += "yellow ";
                            warning_title = "Yellow Status | Unknown Illness Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Yellow Status</strong> | Unknown Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Unknown illness symptom rate of " + Math.Round(s.UnknownRate, 2) + "% is between " + variables.GreenUnknownIllnessBoundary + "% and " + variables.RedUnknownIllnessBoundary + "% of all school absences with reported symptoms.<br><br>";
                            num_warnings++;
                        }
                        else if (s.UnknownRate > variables.RedUnknownIllnessBoundary)
                        {
                            unkn_status = "red";
                            warning_status += "red ";
                            warning_title = "Red Status | Unknown Illness Symptom Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<strong>Red Status</strong> | Unknown Illness Rate at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);
                            warning_text += "<br><strong>Reason</strong>: Unknown illness symptom rate of " + Math.Round(s.OtherRate, 2) + "% is greater than " + variables.RedUnknownIllnessBoundary + "% of all school absences with reported symptoms.<br><br>"; ;
                            num_warnings++;
                        }

                        // update record
                        qHtl_DailySchoolAbsenteeData curr_school = new qHtl_DailySchoolAbsenteeData(s.SchoolID, curr_date);
                        curr_school.AbsenteeStatus = absentee_status;
                        curr_school.AbsenteeStatus7Day = absentee_status_7day;
                        curr_school.AbsenteeStatusSchool = absentee_status_school;
                        curr_school.AbsenteeStatusSchool7Day = absentee_status_school_7day;
                        curr_school.IllnessStatus = illness_status;
                        curr_school.GastrointestinalStatus = gast_status;
                        curr_school.RespiratoryStatus = resp_status;
                        curr_school.RashStatus = rash_status;
                        curr_school.OtherIllnessStatus = othr_status;
                        curr_school.UnknownIllnessStatus = unkn_status;
                        curr_school.A_Warning = a_warning_status;
                        curr_school.A_WarningValues = a_warning_evals;
                        curr_school.B_Warning = b_warning_status;
                        curr_school.B_WarningValues = b_warning_evals;
                        curr_school.C_Warning = c_warning_status;
                        curr_school.C_WarningValues = c_warning_evals;
                        curr_school.D_Warning = d_warning_status;
                        curr_school.D_WarningValues = d_warning_evals;
                        curr_school.E_Warning = e_warning_status;
                        curr_school.E_WarningValues = e_warning_evals;
                        curr_school.F_Warning = f_warning_status;
                        curr_school.F_WarningValues = f_warning_evals;
                        curr_school.Update();

                        if (num_warnings > 1)
                            warning_title = "Multiple School Absentee Status Warnings at " + s.School + " (" + s.SchoolLevel + " School) on " + String.Format("{0:MMMM d, yyyy}", s.DataDate);

                        if (num_warnings > 0)
                            CreateSchoolWarning(s.SchoolID, Convert.ToDateTime(s.DataDate), warning_status, warning_type, warning_title, warning_text);
                    }

                    schools_analyzed = schools.Count;
                }
            }

            return schools_analyzed;
        }

        protected static void CreateSchoolWarning(int school_id, DateTime data_date, string status, string type, string title, string text)
        {
            qOrg_School school = new qOrg_School(school_id);
            
            qHtl_HealthWarning warning = new qHtl_HealthWarning();
            warning.Created = DateTime.Now;
            warning.CreatedBy = 0;
            warning.MarkAsDelete = 0;
            warning.DataDate = data_date.Date;
            warning.Type = type;
            warning.Title = title;
            warning.Text = text;
            warning.IssuedBy = 0;
            warning.IssuedTo = school.School;
            warning.IssuedByName = "System";
            warning.Severity = "not reviewed";
            string eval_status = string.Empty;
            if (status.ToLower().Contains("red"))
                eval_status = "red";
            if (status.ToLower().Contains("yellow"))
            {
                if (!String.IsNullOrEmpty(eval_status))
                    eval_status += " ";
                eval_status += "yellow";
            }
            warning.Severity = eval_status;
            warning.ValidFrom = DateTime.Now;
            warning.ReferenceID = school_id;
            warning.Insert();
        }

        public static decimal CalculateSchoolValuesForRange(string start_date, string end_date, decimal curr_day_rate, int school_id, string value_type)
        {
            DateTime eval_start_date = new DateTime();
            eval_start_date = Convert.ToDateTime(start_date);
            DateTime eval_end_date = new DateTime();
            eval_end_date = Convert.ToDateTime(end_date);
            string sql = string.Empty;

            if (value_type == "std")
                sql = "SELECT RateSTD FROM qHtl_DailySchoolAbsenteeData WHERE SchoolID = " + school_id + " AND MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "'";
            else
                sql = "SELECT Rate FROM qHtl_DailySchoolAbsenteeData WHERE SchoolID = " + school_id + " AND MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "'";

            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql(sql);

            decimal moving_avg = 0;
            decimal total = 0;
            int num_values = 0;

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    decimal curr_value = 0;
                    num_values++;

                    if (value_type == "std")
                    {
                        if (Convert.ToInt32(dr["RateSTD"]) > 0)
                        {
                            curr_value = Convert.ToDecimal(dr["RateSTD"]);
                            total += curr_value;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dr["Rate"]) > 0)
                        {
                            curr_value = Convert.ToDecimal(dr["Rate"]);
                            total += curr_value;
                        }
                    }
                }

                if (num_values > 0)
                    moving_avg = total / num_values;
                else
                    moving_avg = curr_day_rate;

            }

            return moving_avg;
        }

        public static List<double> GetAllPriorDailyRatesforSpecificSchool(string start_date, string end_date, int school_id, double additional_value)
        {
            DateTime eval_start_date = new DateTime();
            DateTime eval_end_date = new DateTime();
            eval_end_date = Convert.ToDateTime(end_date);
            string sql = string.Empty;
            sql = "SELECT Rate FROM qHtl_DailySchoolAbsenteeData WHERE SchoolID = " + school_id + " AND MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "' ORDER BY DataDate ASC";

            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql(sql);

            List<double> school_rates = new List<double>();

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    double curr_value = 0;
                    curr_value = Convert.ToDouble(dr["Rate"]);
                    
                    school_rates.Add(curr_value);
                }
            }

            if (additional_value != 0)
                school_rates.Add(additional_value);

            return school_rates;
        }

        public static List<SchoolData> LoadDailySchoolDataInfoList(DateTime curr_date) 
        {
            var school_data = GetDailySchoolAbsenteeDataCollectionByDate(curr_date);

            List<SchoolData> school_list = new List<SchoolData>();

            if (school_data != null)
            {
                foreach (var d in school_data)
                {
                    qOrg_School school = new qOrg_School(d.SchoolID);
                    decimal one_sigma = 0;
                    decimal two_sigma = 0;
                    decimal three_sigma = 0;
                    decimal latitude = 0;
                    decimal longitude = 0;
                    latitude = school.Latitude;
                    longitude = school.Longitude;

                    one_sigma = d.HistoricRate + (1 * d.HistoricRateSTD);
                    two_sigma = d.HistoricRate + (2 * d.HistoricRateSTD);
                    three_sigma = d.HistoricRate + (3 * d.HistoricRateSTD);

                    string school_color = "#C0C0C0";
                    if (d.A_Warning == true)
                        school_color = "#e51400";
                    else if (d.B_Warning == true)
                        school_color = "#f8a31f";
                    else if (d.C_Warning == true)
                        school_color = "#ffff00";
                    else if (d.D_Warning == true)
                        school_color = "#368ee0";
                    else if (d.E_Warning == true)
                        school_color = "#a200ff";
                    else if (d.F_Warning == true)
                        school_color = "#393";
                    /*
                    else if (one_sigma > 0)
                    {
                        if (d.Rate > two_sigma)
                            school_color = "#393";
                    }
                    */
                    
                    school_list.Add(new SchoolData()
                    {
                        school_id = d.SchoolID,
                        school_name = d.School,
                        num_days = 1,
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
                        moving_average_absences = d.HistoricRate,
                        rate_std = d.RateSTD,
                        moving_rate_std = d.HistoricRateSTD,
                        a_warning = d.A_Warning,
                        a_warning_values = d.A_WarningValues,
                        b_warning = d.B_Warning,
                        b_warning_values = d.B_WarningValues,
                        c_warning = d.C_Warning,
                        c_warning_values = d.C_WarningValues,
                        d_warning = d.D_Warning,
                        d_warning_values = d.D_WarningValues,
                        e_warning = d.E_Warning,
                        e_warning_values = d.E_WarningValues,
                        f_warning = d.F_Warning,
                        f_warning_values = d.F_WarningValues,
                        illness_status = d.IllnessStatus,
                        gastrointestinal_status = d.GastrointestinalStatus,
                        respiratory_status = d.RespiratoryStatus,
                        rash_status = d.RashStatus,
                        other_illness_status = d.OtherIllnessStatus,
                        unknown_illness_status = d.UnknownIllnessStatus,
                        one_sigma = one_sigma,
                        two_sigma = two_sigma,
                        three_signma = three_sigma,
                        school_color = school_color,
                        latitude = latitude,
                        longitude = longitude
                    });
                }
            }

            return school_list;
        }

        public static List<SchoolData> LoadDayDailySchoolDataInfoListByMode(string mode, DateTime report_date, int school_id)
        {
            List<SchoolData> district_list = new List<SchoolData>();
            
            if (report_date != null && report_date != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                DateTime start_range = new DateTime();
                DateTime end_range = new DateTime();
                DateTime curr_calendar_day = new DateTime();
                curr_calendar_day = DateTime.Now;
                DateTime curr_day_increment = new DateTime();

                int high_range = 28;
                int low_range = 13;

                if (String.IsNullOrEmpty(mode))
                    mode = "28_day";

                if (mode == "28_day")
                {
                    high_range = 28;
                    low_range = 13;
                    // figure out if there are calendar days afer report date
                    int days_after_available = (curr_calendar_day - report_date).Days;
                    if (days_after_available > low_range)
                        days_after_available = low_range;

                    start_range = report_date.AddDays(-(high_range - days_after_available));
                    end_range = report_date.AddDays(days_after_available);

                    curr_day_increment = start_range;
                }
                else if (mode == "this_year")
                {
                    DateTime start_year = new DateTime();
                    start_year = Convert.ToDateTime("8/1/2014");
                    high_range = Convert.ToInt32((DateTime.Now - start_year).TotalDays);
                    low_range = (high_range / 2) - 1;

                    start_range = Convert.ToDateTime("8/1/2014");
                    end_range = DateTime.Now;

                    curr_day_increment = start_range;
                }

                var data = GetDailySchoolAbsenteeDataCollectionByDateRangeAndSchool(start_range, end_range, school_id);

                decimal prior_rate = 0;
                decimal prior_historic_rate = 0;
                decimal prior_historic_std = 0;

                for (int i = 1; i <= high_range; i++ )
                { 
                    decimal one_sigma = 0;
                    decimal two_sigma = 0;
                    decimal three_sigma = 0;

                    int eval_total_absent = 0;
                    int eval_total_unknown = 0;
                    int eval_total_other = 0;
                    int eval_total_sick = 0;
                    int eval_gastrointestinal = 0;
                    int eval_respiratory = 0;
                    int eval_rash = 0;
                    int eval_other = 0;
                    int eval_total_enrolled = 0;
                    decimal eval_overall_absentee_rate = 0;
                    decimal eval_historic_absentee_rate = 0;
                    decimal eval_overall_absentee_std = 0;
                    decimal eval_historic_absentee_std = 0;
                    bool eval_a_warning = false;
                    bool eval_b_warning = false;
                    bool eval_c_warning = false;
                    bool eval_d_warning = false;
                    bool eval_e_warning = false;
                    bool eval_f_warning = false;
                    string eval_gastrointestinal_status = string.Empty;
                    string eval_rash_status = string.Empty;
                    string eval_respiratory_status = string.Empty;

                    foreach (var d in data)
                    {
                        DateTime eval_data_date = new DateTime();
                        eval_data_date = Convert.ToDateTime(d.DataDate);
                        if (curr_day_increment == eval_data_date)
                        {
                            eval_total_absent = d.TotalAbsent;
                            eval_total_unknown = d.TotalAbsent - (d.Gastrointestinal + d.Respiratory + d.Rash + d.OtherIllness);
                            eval_total_enrolled = d.TotalEnrolled;
                            eval_total_sick = d.Gastrointestinal + d.Respiratory + d.Rash + d.OtherIllness;
                            eval_gastrointestinal = d.Gastrointestinal;
                            eval_respiratory = d.Respiratory;
                            eval_rash = d.Rash;
                            eval_other = d.OtherIllness;
                            eval_overall_absentee_rate = d.Rate;
                            eval_historic_absentee_rate = d.HistoricRate;
                            eval_historic_absentee_std = d.HistoricRateSTD;
                            eval_a_warning = d.A_Warning;
                            eval_b_warning = d.B_Warning;
                            eval_c_warning = d.C_Warning;
                            eval_d_warning = d.D_Warning;
                            eval_e_warning = d.E_Warning;
                            eval_f_warning = d.F_Warning;
                            eval_gastrointestinal_status = d.GastrointestinalStatus;
                            eval_rash_status = d.RashStatus;
                            eval_respiratory_status = d.RespiratoryStatus;
                        }
                    }

                    if (eval_overall_absentee_rate == 0)
                    {
                        eval_overall_absentee_rate = prior_rate;
                        eval_historic_absentee_rate = prior_historic_rate;
                        eval_historic_absentee_std = prior_historic_std;
                    }
                    else
                    {
                        prior_rate = eval_overall_absentee_rate;
                        prior_historic_rate = eval_historic_absentee_rate;
                        prior_historic_std = eval_historic_absentee_std;
                    }

                    one_sigma = eval_historic_absentee_rate + (1 * eval_historic_absentee_std);
                    two_sigma = eval_historic_absentee_rate + (2 * eval_historic_absentee_std);
                    three_sigma = eval_historic_absentee_rate + (3 * eval_historic_absentee_std);

                    district_list.Add(new SchoolData()
                    {
                        data_date = curr_day_increment,
                        school_id = school_id,
                        total_absent = eval_total_absent,
                        total_unknown = eval_total_unknown,
                        total_other = eval_total_other,
                        total_sick = eval_total_sick,
                        gastrointestinal = eval_gastrointestinal,
                        respiratory = eval_respiratory,
                        rash = eval_rash,
                        other_illness = eval_other,
                        total_enrolled = eval_total_enrolled,
                        rate = eval_overall_absentee_rate,
                        moving_rate_std = eval_historic_absentee_rate,
                        rate_std = eval_overall_absentee_std,
                        one_sigma = one_sigma,
                        two_sigma = two_sigma,
                        three_signma = three_sigma,
                        a_warning = eval_a_warning,
                        b_warning = eval_b_warning,
                        c_warning = eval_c_warning,
                        d_warning = eval_d_warning,
                        e_warning = eval_e_warning,
                        f_warning = eval_f_warning,
                        gastrointestinal_status = eval_gastrointestinal_status,
                        rash_status = eval_rash_status,
                        respiratory_status = eval_respiratory_status
                    });

                    curr_day_increment = curr_day_increment.AddDays(1);
                }
            }

            return district_list;
        }

        public static int GetNumWarningsByTypeAndDay(string warning_type, DateTime data_date)
        {
            string warning_letter = warning_type.ToUpper();
            return Convert.ToInt32(SqlQuery.execute_sql_scalar(string.Format("SELECT COUNT (DailySchoolAbsenteeDataID) FROM qHtl_DailySchoolAbsenteeData WHERE {0}_Warning = 1 AND DataDate = '{1}'", warning_letter, Convert.ToString(data_date)), CommandType.Text, null));
        }
    }

    public class SchoolData
    {
        public DateTime data_date { get; set; }
        public int school_district_id { get; set; }
        public int school_id { get; set; }
        public string school_name { get; set; }
        public string school_level { get; set; }
        public string grade_level { get; set; }
        public string classroom { get; set; }
        public int days_in_session { get; set; }
        public int num_days { get; set; }
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
        public decimal rate { get; set; }
        public decimal moving_average_absences { get; set; }
        public decimal rate_std { get; set; }
        public decimal moving_rate_std { get; set; }
        public bool a_warning { get; set; }
        public string a_warning_values { get; set; }
        public bool b_warning { get; set; }
        public string b_warning_values { get; set; }
        public bool c_warning { get; set; }
        public string c_warning_values { get; set; }
        public bool d_warning { get; set; }
        public string d_warning_values { get; set; }
        public bool e_warning { get; set; }
        public string e_warning_values { get; set; }
        public bool f_warning { get; set; }
        public string f_warning_values { get; set; }
        public string illness_status { get; set; }
        public string gastrointestinal_status { get; set; }
        public string respiratory_status { get; set; }
        public string rash_status { get; set; }
        public string other_illness_status { get; set; }
        public string unknown_illness_status { get; set; }
        public decimal one_sigma { get; set; }
        public decimal two_sigma { get; set; }
        public decimal three_signma { get; set; }
        public string school_color { get; set; }
        public decimal longitude { get; set; }
        public decimal latitude { get; set;}
    }
}
