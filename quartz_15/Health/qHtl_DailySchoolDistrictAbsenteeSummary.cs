using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Quartz.Data;

namespace Quartz.Health
{
    public class qHtl_DailySchoolDistrictAbsenteeSummary
    {
        protected static qHtl_DailySchoolDistrictAbsenteeSummary schema = new qHtl_DailySchoolDistrictAbsenteeSummary();

        protected DbRow container;
        protected readonly DbColumn<Int32> daily_school_district_absentee_summary_id;
        protected readonly DbColumn<DateTime> created;
        protected readonly DbColumn<Int32> mark_as_delete;
        protected readonly DbColumn<DateTime?> data_date;
        protected readonly DbColumn<Int32> school_district_id;
        protected readonly DbColumn<Int32> num_elementary_schools;
        protected readonly DbColumn<Int32> total_elementary_students;
        protected readonly DbColumn<Int32> total_elementary_students_absent;
        protected readonly DbColumn<Decimal> elementary_school_absentee_rate;
        protected readonly DbColumn<Decimal> elementary_school_absentee_std;
        protected readonly DbColumn<Int32> num_junior_highs;
        protected readonly DbColumn<Int32> total_junior_high_students;
        protected readonly DbColumn<Int32> total_junior_high_students_absent;
        protected readonly DbColumn<Decimal> junior_high_school_absentee_rate;
        protected readonly DbColumn<Decimal> junior_high_school_absentee_std;
        protected readonly DbColumn<Int32> num_high_schools;
        protected readonly DbColumn<Int32> total_high_school_students;
        protected readonly DbColumn<Int32> total_high_school_students_absent;
        protected readonly DbColumn<Decimal> high_school_absentee_rate;
        protected readonly DbColumn<Decimal> high_school_absentee_std;
        protected readonly DbColumn<Decimal> overall_absentee_rate;
        protected readonly DbColumn<Decimal> two_week_absentee_rate;
        protected readonly DbColumn<Decimal> prior_year_absentee_rate;
        protected readonly DbColumn<Decimal> historic_absentee_rate;
        protected readonly DbColumn<Decimal> historic_district_absentee_std;
        protected readonly DbColumn<Decimal> overall_absentee_std;
        protected readonly DbColumn<Decimal> historic_absentee_std;
        protected readonly DbColumn<Int32> total_absent;
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
        protected readonly DbColumn<Int32> num_a_warnings;
        protected readonly DbColumn<Int32> num_b_warnings;
        protected readonly DbColumn<Int32> num_c_warnings;
        protected readonly DbColumn<Int32> num_d_warnings;
        protected readonly DbColumn<Int32> num_e_warnings;
        protected readonly DbColumn<Int32> num_f_warnings;

        public Int32 DailySchoolDistrictAbsenteeSummaryID { get { return daily_school_district_absentee_summary_id.Value; } set { daily_school_district_absentee_summary_id.Value = value; } }
        public DateTime Created { get { return created.Value; } set { created.Value = value; } }
        public Int32 MarkAsDelete { get { return mark_as_delete.Value; } set { mark_as_delete.Value = value; } }
        public DateTime? DataDate { get { return data_date.Value; } set { data_date.Value = value; } }
        public Int32 SchoolDistrictID { get { return school_district_id.Value; } set { school_district_id.Value = value; } }
        public Int32 NumElementarySchools { get { return num_elementary_schools.Value; } set { num_elementary_schools.Value = value; } }
        public Int32 TotalElementaryStudents { get { return total_elementary_students.Value; } set { total_elementary_students.Value = value; } }
        public Int32 TotalElementaryStudentsAbsent { get { return total_elementary_students_absent.Value; } set { total_elementary_students_absent.Value = value; } }
        public Decimal ElementarySchoolAbsenteeRate { get { return elementary_school_absentee_rate.Value; } set { elementary_school_absentee_rate.Value = value; } }
        public Decimal ElementarySchoolAbsenteeSTD { get { return elementary_school_absentee_std.Value; } set { elementary_school_absentee_std.Value = value; } }
        public Int32 NumJuniorHighs { get { return num_junior_highs.Value; } set { num_junior_highs.Value = value; } }
        public Int32 TotalJuniorHighStudents { get { return total_junior_high_students.Value; } set { total_junior_high_students.Value = value; } }
        public Int32 TotalJuniorHighStudentsAbsent { get { return total_junior_high_students_absent.Value; } set { total_junior_high_students_absent.Value = value; } }
        public Decimal JuniorHighSchoolAbsenteeRate { get { return junior_high_school_absentee_rate.Value; } set { junior_high_school_absentee_rate.Value = value; } }
        public Decimal JuniorHighSchoolAbsenteeSTD { get { return junior_high_school_absentee_std.Value; } set { junior_high_school_absentee_std.Value = value; } }
        public Int32 NumHighSchools { get { return num_high_schools.Value; } set { num_high_schools.Value = value; } }
        public Int32 TotalHighSchoolStudents { get { return total_high_school_students.Value; } set { total_high_school_students.Value = value; } }
        public Int32 TotalHighSchoolStudentsAbsent { get { return total_high_school_students_absent.Value; } set { total_high_school_students_absent.Value = value; } }
        public Decimal HighSchoolAbsenteeRate { get { return high_school_absentee_rate.Value; } set { high_school_absentee_rate.Value = value; } }
        public Decimal HighSchoolAbsenteeSTD { get { return high_school_absentee_std.Value; } set { high_school_absentee_std.Value = value; } }
        public Decimal OverallAbsenteeRate { get { return overall_absentee_rate.Value; } set { overall_absentee_rate.Value = value; } }
        public Decimal TwoWeekAbsenteeRate { get { return two_week_absentee_rate.Value; } set { two_week_absentee_rate.Value = value; } }
        public Decimal PriorYearAbsenteeRate { get { return prior_year_absentee_rate.Value; } set { prior_year_absentee_rate.Value = value; } }
        public Decimal HistoricAbsenteeRate { get { return historic_absentee_rate.Value; } set { historic_absentee_rate.Value = value; } }
        public Decimal HistoricDistrictAbsenteeSTD { get { return historic_district_absentee_std.Value; } set { historic_district_absentee_std.Value = value; } }
        public Decimal OverallAbsenteeSTD { get { return overall_absentee_std.Value; } set { overall_absentee_std.Value = value; } }
        public Decimal HistoricAbsenteeSTD { get { return historic_absentee_std.Value; } set { historic_absentee_std.Value = value; } }
        public Int32 TotalAbsent { get { return total_absent.Value; } set { total_absent.Value = value; } }
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
        public Int32 NumAWarnings { get { return num_a_warnings.Value; } set { num_a_warnings.Value = value; } }
        public Int32 NumBWarnings { get { return num_b_warnings.Value; } set { num_b_warnings.Value = value; } }
        public Int32 NumCWarnings { get { return num_c_warnings.Value; } set { num_c_warnings.Value = value; } }
        public Int32 NumDWarnings { get { return num_d_warnings.Value; } set { num_d_warnings.Value = value; } }
        public Int32 NumEWarnings { get { return num_e_warnings.Value; } set { num_e_warnings.Value = value; } }
        public Int32 NumFWarnings { get { return num_f_warnings.Value; } set { num_f_warnings.Value = value; } }

        public qHtl_DailySchoolDistrictAbsenteeSummary()
            : this(new DbRow())
        {
        }

        protected qHtl_DailySchoolDistrictAbsenteeSummary(DbRow c)
        {
            container = c;
            container.SetContainerName("qHtl_DailySchoolDistrictAbsenteeSummary");
            daily_school_district_absentee_summary_id = container.NewColumn<Int32>("DailySchoolDistrictAbsenteeSummaryID", true);
            created = container.NewColumn<DateTime>("Created");
            mark_as_delete = container.NewColumn<Int32>("MarkAsDelete");
            data_date = container.NewColumn<DateTime?>("DataDate");
            school_district_id = container.NewColumn<Int32>("SchoolDistrictID");
            num_elementary_schools = container.NewColumn<Int32>("NumElementarySchools");
            total_elementary_students = container.NewColumn<Int32>("TotalElementaryStudents");
            total_elementary_students_absent = container.NewColumn<Int32>("TotalElementaryStudentsAbsent");
            elementary_school_absentee_rate = container.NewColumn<Decimal>("ElementarySchoolAbsenteeRate");
            elementary_school_absentee_std = container.NewColumn<Decimal>("ElementarySchoolAbsenteeSTD");
            num_junior_highs = container.NewColumn<Int32>("NumJuniorHighs");
            total_junior_high_students = container.NewColumn<Int32>("TotalJuniorHighStudents");
            total_junior_high_students_absent = container.NewColumn<Int32>("TotalJuniorHighStudentsAbsent");
            junior_high_school_absentee_rate = container.NewColumn<Decimal>("JuniorHighSchoolAbsenteeRate");
            junior_high_school_absentee_std = container.NewColumn<Decimal>("JuniorHighSchoolAbsenteeSTD");
            num_high_schools = container.NewColumn<Int32>("NumHighSchools");
            total_high_school_students = container.NewColumn<Int32>("TotalHighSchoolStudents");
            total_high_school_students_absent = container.NewColumn<Int32>("TotalHighSchoolStudentsAbsent");
            high_school_absentee_rate = container.NewColumn<Decimal>("HighSchoolAbsenteeRate");
            two_week_absentee_rate = container.NewColumn<Decimal>("TwoWeekAbsenteeRate");
            prior_year_absentee_rate = container.NewColumn<Decimal>("PriorYearAbsenteeRate");
            historic_absentee_rate = container.NewColumn<Decimal>("HistoricAbsenteeRate");
            high_school_absentee_std = container.NewColumn<Decimal>("HighSchoolAbsenteeSTD");
            overall_absentee_rate = container.NewColumn<Decimal>("OverallAbsenteeRate");
            historic_district_absentee_std = container.NewColumn<Decimal>("HistoricDistrictAbsenteeSTD");
            overall_absentee_std = container.NewColumn<Decimal>("OverallAbsenteeSTD");
            historic_absentee_std = container.NewColumn<Decimal>("HistoricAbsenteeSTD");
            total_absent = container.NewColumn<Int32>("TotalAbsent");
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
            num_a_warnings = container.NewColumn<Int32>("NumAWarnings");
            num_b_warnings = container.NewColumn<Int32>("NumBWarnings");
            num_c_warnings = container.NewColumn<Int32>("NumCWarnings");
            num_d_warnings = container.NewColumn<Int32>("NumDWarnings");
            num_e_warnings = container.NewColumn<Int32>("NumEWarnings");
            num_f_warnings = container.NewColumn<Int32>("NumFWarnings");
        }

        public qHtl_DailySchoolDistrictAbsenteeSummary(Int32 daily_school_district_absentee_summary_id)
            : this()
        {
            container.Select("DailySchoolDistrictAbsenteeSummaryID = @DailySchoolDistrictAbsenteeSummaryID", new SqlQueryParameter("@DailySchoolDistrictAbsenteeSummaryID", daily_school_district_absentee_summary_id));
        }

        public qHtl_DailySchoolDistrictAbsenteeSummary(DateTime data_date)
            : this()
        {
            DateTime curr_date = data_date.Date;
            container.Select("DataDate = @DataDate", new SqlQueryParameter("@DataDate", curr_date));
        }

        public void Update()
        {
            container.Update("DailySchoolDistrictAbsenteeSummaryID = @DailySchoolDistrictAbsenteeSummaryID");
        }

        public void Insert()
        {
            Created = DateTime.Now;

            DailySchoolDistrictAbsenteeSummaryID = Convert.ToInt32(container.Insert());
        }

        public static void DeleteSchoolDistrictAbsenteeSummary(int daily_school_district_absentee_summary_id)
        {
            schema.container.Delete(string.Concat("DailySchoolDistrictAbsenteeSummaryID = ", daily_school_district_absentee_summary_id));
        }

        public static void DeleteSchoolDistrictAbsenteeSummaryByDate(string curr_date)
        {
            schema.container.Delete(string.Format(string.Format("DataDate = '{0}'", curr_date)));
        }

        public static string BuildDailySchoolDistrictAbsenteeSummarySQLByFilters(int curr_user_id, string start_time, string end_time, int school_district_id)
        {
            string sql_where = string.Empty;
            string sql_where_schools = string.Empty;
            string sql_where_school_levels = string.Empty;
            string sql_where_grade_levels = string.Empty;

            sql_where = " DailySchoolDistrictAbsenteeSummaryID > 0";

            return sql_where;
        }

        public static DataTable GetDailySchoolDistrictAbsenteeSummaryDataTable(string sql)
        {
            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql("SELECT * FROM qHtl_DailySchoolDistrictAbsenteeSummary " + sql);

            return dt;
        }

        public static ICollection<qHtl_DailySchoolDistrictAbsenteeSummary> GetDailySchoolDistrictAbsenteeSummaryCollection(string sql)
        {
            return schema.container.Select<qHtl_DailySchoolDistrictAbsenteeSummary>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "DataDate ASC",
                }, c => new qHtl_DailySchoolDistrictAbsenteeSummary(c));
        }

        public static qHtl_DailySchoolDistrictAbsenteeSummary GetMostRecentDailySummary()
        {
            qHtl_DailySchoolDistrictAbsenteeSummary summary = new qHtl_DailySchoolDistrictAbsenteeSummary();

            summary.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("MarkAsDelete = 0"),
                OrderBy = "DataDate DESC"
            });

            if (summary.DailySchoolDistrictAbsenteeSummaryID > 0) return summary;
            else return null;
        }

        public static qHtl_DailySchoolDistrictAbsenteeSummary GetDailySummaryByDate(string curr_date)
        {
            qHtl_DailySchoolDistrictAbsenteeSummary summary = new qHtl_DailySchoolDistrictAbsenteeSummary();

            summary.container.Select(new DbQuery
            {
                Top = "Top(1)",
                Where = string.Format("MarkAsDelete = 0 AND DataDate = '" + curr_date + "'"),
                OrderBy = "DataDate DESC"
            });

            if (summary.DailySchoolDistrictAbsenteeSummaryID > 0) return summary;
            else return null;
        }

        public static ICollection<qHtl_DailySchoolDistrictAbsenteeSummary> GetDailySummaryByDateRange(DateTime start_date, DateTime end_date)
        {
            string sql = string.Empty;

            sql = "DataDate BETWEEN '" + Convert.ToDateTime(start_date.AddHours(-1)) + "' AND '" + Convert.ToDateTime(end_date.AddHours(1)) + "'";

            return schema.container.Select<qHtl_DailySchoolDistrictAbsenteeSummary>(
                new DbQuery
                {
                    Where = sql,
                    OrderBy = "DataDate ASC",
                }, c => new qHtl_DailySchoolDistrictAbsenteeSummary(c));
        }

        public static qHtl_DailySchoolDistrictAbsenteeSummary GetDailySummaryPriorOrAfter(string direction, string curr_date)
        {
            qHtl_DailySchoolDistrictAbsenteeSummary summary = new qHtl_DailySchoolDistrictAbsenteeSummary();

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

            if (summary.DailySchoolDistrictAbsenteeSummaryID > 0) return summary;
            else return null;
        }

        public static decimal CalculateAbsenteeRateForRange(string start_date, string end_date, decimal curr_day_rate)
        {
            DateTime eval_start_date = new DateTime();
            eval_start_date = Convert.ToDateTime(start_date);
            DateTime eval_end_date = new DateTime();
            eval_end_date = Convert.ToDateTime(end_date);
            string sql = string.Empty;
            
            sql = "SELECT OverallAbsenteeRate FROM qHtl_DailySchoolDistrictAbsenteeSummary WHERE MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "'";

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

                    if (Convert.ToInt32(dr["OverallAbsenteeRate"]) > 0)
                    {
                        curr_value = Convert.ToDecimal(dr["OverallAbsenteeRate"]);
                        total += curr_value;
                    }
                }

                if (num_values > 0)
                    moving_avg = total / num_values;
                else
                    moving_avg = curr_day_rate;

            }

            return moving_avg;
        }

        public static decimal CalculateAbsenteeRateSTDForRange(string start_date, string end_date, decimal curr_day_rate)
        {
            DateTime eval_start_date = new DateTime();
            eval_start_date = Convert.ToDateTime(start_date);
            DateTime eval_end_date = new DateTime();
            eval_end_date = Convert.ToDateTime(end_date);
            string sql = string.Empty;

            sql = "SELECT OverallAbsenteeSTD FROM qHtl_DailySchoolDistrictAbsenteeSummary WHERE MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "'";

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

                    if (Convert.ToInt32(dr["OverallAbsenteeSTD"]) > 0)
                    {
                        curr_value = Convert.ToDecimal(dr["OverallAbsenteeSTD"]);
                        total += curr_value;
                    }
                }

                if (num_values > 0)
                    moving_avg = total / num_values;
                else
                    moving_avg = curr_day_rate;

            }

            return moving_avg;
        }

        public static List<DistrictData> LoadDayDailyDistrictDataInfoListByMode(string mode, DateTime report_date, int school_district_id)
        {
            List<DistrictData> district_list = new List<DistrictData>();
            
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

                var data = GetDailySummaryByDateRange(start_range, end_range);

                decimal prior_rate = 0;
                decimal prior_historic_rate = 0;
                decimal prior_historic_std = 0;
                decimal prior_historic_district_std = 0;

                for (int i = 1; i <= high_range; i++ )
                { 
                    decimal one_sigma = 0;
                    decimal two_sigma = 0;
                    decimal three_sigma = 0;
                    decimal district_one_sigma = 0;
                    decimal district_two_sigma = 0;
                    decimal district_three_sigma = 0;

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
                    decimal eval_historic_district_absentee_std = 0;
                    decimal eval_overall_absentee_std = 0;
                    decimal eval_historic_absentee_std = 0;
                    int num_a_warn = 0;
                    int num_b_warn = 0;
                    int num_c_warn = 0;
                    int num_d_warn = 0;
                    int num_e_warn = 0;
                    int num_f_warn = 0;

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
                            eval_overall_absentee_rate = d.OverallAbsenteeRate;
                            eval_historic_absentee_rate = d.HistoricAbsenteeRate;
                            eval_historic_district_absentee_std = d.HistoricDistrictAbsenteeSTD;
                            eval_historic_absentee_std = d.HistoricAbsenteeSTD;
                            num_a_warn = d.NumAWarnings;
                            num_b_warn = d.NumBWarnings;
                            num_c_warn = d.NumCWarnings;
                            num_d_warn = d.NumDWarnings;
                            num_e_warn = d.NumEWarnings;
                            num_f_warn = d.NumFWarnings;
                        }
                    }

                    if (eval_overall_absentee_rate == 0)
                    {
                        eval_overall_absentee_rate = prior_rate;
                        eval_historic_absentee_rate = prior_historic_rate;
                        eval_historic_absentee_std = prior_historic_std;
                        eval_historic_district_absentee_std = prior_historic_district_std;
                    }
                    else
                    {
                        prior_rate = eval_overall_absentee_rate;
                        prior_historic_rate = eval_historic_absentee_rate;
                        prior_historic_std = eval_historic_absentee_std;
                        prior_historic_district_std = eval_historic_district_absentee_std;
                    }

                    one_sigma = eval_overall_absentee_rate + (1 * eval_historic_absentee_std);
                    two_sigma = eval_overall_absentee_rate + (2 * eval_historic_absentee_std);
                    three_sigma = eval_overall_absentee_rate + (3 * eval_historic_absentee_std);
                    district_one_sigma = eval_historic_absentee_rate + (1 * eval_historic_district_absentee_std);
                    district_two_sigma = eval_historic_absentee_rate + (2 * eval_historic_district_absentee_std);
                    district_three_sigma = eval_historic_absentee_rate + (3 * eval_historic_district_absentee_std);

                    district_list.Add(new DistrictData()
                    {
                        data_date = curr_day_increment,
                        school_district_id = school_district_id,
                        total_absent = eval_total_absent,
                        total_unknown = eval_total_unknown,
                        total_other = eval_total_other,
                        total_sick = eval_total_sick,
                        gastrointestinal = eval_gastrointestinal,
                        respiratory = eval_respiratory,
                        rash = eval_rash,
                        other_illness = eval_other,
                        total_enrolled = eval_total_enrolled,
                        overall_absentee_rate = eval_overall_absentee_rate,
                        historic_absentee_rate = eval_historic_absentee_rate,
                        overall_absentee_std = eval_overall_absentee_std,
                        historic_absentee_std = eval_historic_absentee_std,
                        one_sigma = one_sigma,
                        two_sigma = two_sigma,
                        three_signma = three_sigma,
                        district_one_sigma = district_one_sigma,
                        district_two_sigma = district_two_sigma,
                        district_three_sigma = district_three_sigma,
                        num_a_warnings = num_a_warn,
                        num_b_warnings = num_b_warn,
                        num_c_warnings = num_c_warn,
                        num_d_warnings = num_d_warn,
                        num_e_warnings = num_e_warn,
                        num_f_warnings = num_f_warn
                    });

                    curr_day_increment = curr_day_increment.AddDays(1);
                }
            }

            return district_list;
        }

        public static List<double> GetAllPriorDailyRatesforDistrict(string start_date, string end_date, double additional_value)
        {
            DateTime eval_start_date = new DateTime();
            DateTime eval_end_date = new DateTime();
            eval_end_date = Convert.ToDateTime(end_date);
            string sql = string.Empty;
            sql = "SELECT OverallAbsenteeRate FROM qHtl_DailySchoolDistrictAbsenteeSummary WHERE MarkAsDelete = 0 AND DataDate BETWEEN '" + eval_start_date + "' AND '" + eval_end_date + "' ORDER BY DataDate ASC";

            DataTable dt = new DataTable();
            dt = SqlQuery.execute_sql(sql);

            List<double> district_rates = new List<double>();

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    double curr_value = 0;
                    curr_value = Convert.ToDouble(dr["OverallAbsenteeRate"]);

                    district_rates.Add(curr_value);
                }
            }

            if (additional_value != 0)
                district_rates.Add(additional_value);

            return district_rates;
        }
    }

    public class DistrictData
    {
        public DateTime data_date { get; set; }
        public int school_district_id { get; set; }
        public int total_absent { get; set; }
        public int total_unknown { get; set; }
        public int total_other { get; set; }
        public int total_sick { get; set; }
        public int gastrointestinal { get; set; }
        public int respiratory { get; set; }
        public int rash { get; set; }
        public int other_illness { get; set; }
        public int total_enrolled { get; set; }
        public decimal overall_absentee_rate { get; set; }
        public decimal historic_absentee_rate { get; set; }
        public decimal historic_district_absentee_std { get; set; }         // STD of all existing district absentee rates
        public decimal overall_absentee_std { get; set; }                   // STD of all existing school absentee rates
        public decimal historic_absentee_std { get; set; }
        public decimal one_sigma { get; set; }
        public decimal two_sigma { get; set; }
        public decimal three_signma { get; set; }
        public decimal district_one_sigma { get; set; }
        public decimal district_two_sigma { get; set; }
        public decimal district_three_sigma { get; set; }
        public int num_a_warnings { get; set; }
        public int num_b_warnings { get; set; }
        public int num_c_warnings { get; set; }
        public int num_d_warnings { get; set; }
        public int num_e_warnings { get; set; }
        public int num_f_warnings { get; set; }
    }
}
