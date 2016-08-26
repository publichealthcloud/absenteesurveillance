using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

using Microsoft.VisualBasic;
using Quartz;
using Quartz.Communication;
using Quartz.Portal;
using System.Threading;
using Quartz.Health;
using Quartz.Organization;

public partial class process_absentee_upload : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            hplDownloadExample.NavigateUrl = "~/resources/absentee/template/process_absentee_data.csv";
        }
    }

    protected void btnProcess_OnClick(object sender, System.EventArgs e)
    {
        string[] to_pass = new string[2] { "process", lblFileName.Text };
        ProcessAbsenteeData(to_pass);
        plhStep2.Visible = false;
        plhStep4.Visible = true;
    }

    protected void btnTestProcess_OnClick(object sender, System.EventArgs e)
    {
        int userID = System.Convert.ToInt32(Context.Items["userID"]);

        if (Request.Files.Count > 0)
        {
            if (null != upFile1.PostedFile && upFile1.PostedFile.FileName != "")
            {
                string fileName = System.IO.Path.GetFileName(upFile1.PostedFile.FileName);
                string fileFullName = DateTime.Now.ToString("yyyyddMMHHmmss") + System.IO.Path.GetFileName(upFile1.PostedFile.FileName);
                string fileLocation = string.Format("{0}{1}", Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_AbsenteeFolder"])), fileFullName);

                upFile1.PostedFile.SaveAs(fileLocation);

                process_absentee_upload target = new process_absentee_upload();
                string mode = "test";
                string[] to_pass = new string[2] {mode, fileFullName};

                //Thread newThread = new Thread(() => { target.ProcessContacts(to_pass); });
                //newThread.Start();

                //processContacts = new Thread(new ThreadStart(ProcessContacts));
                //processContacts.Start();

                ProcessAbsenteeData(to_pass);

                btnTestProcess.Visible = false;
                lblFileName.Text = fileFullName;
                plhUpload.Visible = false;
                plhRestartUpload.Visible = true;
                plhStep2Results.Visible = true;
            }
            else
            {
                plhStep3.Visible = false;
                lblUploadFail.Text = "You must first upload a file";
            }
            
        }
        else
        {
            plhStep3.Visible = false;
        }
    }

    protected void ProcessAbsenteeData(string[] to_pass)
    {
        int school_district_id = 0;

        if (school_district_id == 0)
            school_district_id = 1;

        qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(school_district_id);
        
        string mode = to_pass[0];
        string f_name = to_pass[1];
        lblFileName.Text = fileFullName;
        string fileName = string.Format("{0}{1}", Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Resources_AbsenteeFolder"])), f_name);
        string message = string.Empty;
        string str_solution_start_date = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Solution_StartDate"]);

        if (File.Exists(fileName))
        {
            List<string[]> data = parseCSV(fileName);

            message = "RESULTS<br>";
            string eval_date = string.Empty;
            int curr_school_id = 0;
            int curr_school_district_id = 0;
            string curr_school_name = string.Empty;
            string curr_school_level = string.Empty;
            int curr_school_total_absent = 0;
            int curr_school_total_unknown = 0;
            int curr_school_total_other = 0;
            int curr_school_total_sick = 0;
            int curr_school_gastrointestinal = 0;
            int curr_school_respiratory = 0;
            int curr_school_rash = 0;
            int curr_school_other_illness = 0;
            int curr_school_unknown_illness = 0;
            int curr_school_total_enrolled = 0;
            decimal curr_school_rate = 0;
            int num_school_saves = 0;

            bool overall_success = false;
            int total_elementary_schools = 0;
            int total_elementary_students_enrolled = 0;
            int total_elementary_students_absent = 0;
            decimal total_elementary_absentee_rate = 0;
            int total_junior_high_schools = 0;
            int total_junior_high_students_enrolled = 0;
            int total_junior_high_students_absent = 0;
            decimal total_junior_high_absentee_rate = 0;
            int total_high_schools = 0;
            int total_high_school_students_enrolled = 0;
            int total_high_school_students_absent = 0;
            decimal total_high_school_absentee_rate = 0;
            int total_students_enrolled = 0;
            decimal total_absentee_rate = 0;

            int summary_total_absent = 0;
            int summary_total_sick = 0;
            int summary_total_gastrointestinal = 0;
            int summary_total_respiratory = 0;
            int summary_total_rash = 0;
            int summary_total_other_illness = 0;
            int summary_total_unknown_illness = 0;
            int summary_total_enrolled = 0;

            List<double> summary_sch_rates = new List<double>();
            List<double> sch_rates = new List<double>();
            List<double> elem_rates = new List<double>();
            List<double> jr_rates = new List<double>();
            List<double> high_rates = new List<double>();
            List<double> illness_rates = new List<double>();
            List<double> gast_rates = new List<double>();
            List<double> resp_rates = new List<double>();
            List<double> rash_rates = new List<double>();
            List<double> othr_rates = new List<double>();
            List<double> unkn_rates = new List<double>();

            List<ClassroomAbsenteeData> a_data = new List<ClassroomAbsenteeData>();

            string curr_descriptor = string.Empty;
            int prior_school_id = 0;

            string absent_date = string.Empty;
            string school_name = string.Empty;
            string school_type = string.Empty;
            string grade_level = string.Empty;
            string grade_number = string.Empty;
            string class_name = string.Empty;
            string teacher_name = string.Empty;
            int days_in_session = 0;
            int total_enrolled = 0;
            int total_absent = 0;
            int total_unknown = 0;
            int total_other = 0;
            int total_sick = 0;
            int gastrointestinal = 0;
            int respiratory = 0;
            int rash = 0;
            int other_illness = 0;
            int unknown_illness = 0;

            bool success = false;
            string reason = string.Empty;

            int i = 0;
            int s = 0;
            foreach (string[] c in data)
            {
                if (i > 0)
                {
                    absent_date = c[0];
                    school_name = c[1];
                    school_name = school_name.Trim();
                    school_type = c[2];
                    school_type = school_type.Trim();
                    grade_level = c[3];
                    grade_level = grade_level.Trim();
                    class_name = c[4];
                    class_name = class_name.Trim();
                    teacher_name = c[5];
                    teacher_name = teacher_name.Trim();
                    days_in_session = Convert.ToInt32(c[6]);
                    total_enrolled = Convert.ToInt32(c[7]);
                    total_absent = Convert.ToInt32(c[8]);
                    total_unknown = Convert.ToInt32(c[9]);
                    total_other = Convert.ToInt32(c[10]);
                    total_sick = Convert.ToInt32(c[11]);
                    gastrointestinal = Convert.ToInt32(c[12]);
                    respiratory = Convert.ToInt32(c[13]);
                    rash = Convert.ToInt32(c[14]);
                    other_illness = Convert.ToInt32(c[15]);
                    unknown_illness = Convert.ToInt32(c[16]);

                    if (school_type.ToLower().Contains("elem"))
                    {
                        int curr_grade_number = 0;
                        if (grade_level.ToLower().Contains("kinder"))
                            curr_grade_number = 0;
                        else if (grade_level.ToLower().Contains("pre"))
                            curr_grade_number = -1;
                        else
                        {
                            try
                            {
                                curr_grade_number = Convert.ToInt32(grade_level);
                            }
                            catch
                            {
                                curr_grade_number = -2;
                            }
                        }
                        string extra_sort_val = string.Empty;
                        if (curr_grade_number == -1 || curr_grade_number == 0)
                            extra_sort_val = "a";
                        
                        // add to list
                        a_data.Add(new ClassroomAbsenteeData()
                        {
                            sort_by = absent_date + " " + school_name + " " + extra_sort_val + "" + curr_grade_number,
                            absent_date = absent_date,
                            school_name = school_name,
                            school_type = school_type,
                            grade_level = grade_level,
                            grade_number = curr_grade_number,
                            class_name = class_name,
                            teacher_name = teacher_name,
                            days_in_session = days_in_session,
                            total_enrolled = total_enrolled,
                            total_absent = total_absent,
                            total_unknown = total_unknown,
                            total_other = total_other,
                            total_sick = total_sick,
                            gastrointestinal = gastrointestinal,
                            respiratory = respiratory,
                            rash = rash,
                            other_illness = other_illness,
                            unknown_illness = unknown_illness
                        });
                    }
                }

                i++;
            }

            // sort array to insure that it is ordered by school
            a_data.Sort((a, b) => a.sort_by.CompareTo(b.sort_by));

            // clear initial list
            data.Clear();

            int class_count = 0;
            List<double> curr_school_rates = new List<double>();
            List<double> curr_school_all_prior_rates = new List<double>();
            List<double> curr_district_all_prior_rates = new List<double>();

            foreach (var a in a_data)
            {
                
                class_count ++;

                absent_date = a.absent_date;
                school_name = a.school_name;
                school_type = a.school_type;
                grade_level = a.grade_level;
                grade_number = Convert.ToString(a.grade_number);
                class_name = a.class_name;
                teacher_name = a.teacher_name;
                days_in_session = a.days_in_session;
                total_enrolled = a.total_enrolled;
                total_absent = a.total_absent;
                total_unknown = a.total_unknown;
                total_other = a.total_other;
                total_sick = a.total_sick;
                gastrointestinal = a.gastrointestinal;
                respiratory = a.respiratory;
                rash = a.rash;
                other_illness = a.other_illness;
                unknown_illness = a.unknown_illness;

                curr_descriptor = teacher_name + ", Grade: " + grade_level + " at " + school_name;

                DateTime curr_date = new DateTime();
                curr_date = Convert.ToDateTime(absent_date);

                if (mode == "process")
                {
                    if (String.IsNullOrEmpty(eval_date))
                    {
                        eval_date = absent_date;
                        DateTime curr_eval_date = new DateTime();
                        curr_eval_date = Convert.ToDateTime(eval_date);

                        string final_eval_date = curr_eval_date.ToShortDateString();

                        qHtl_DailyClassroomAbsenteeData.DeleteClassroomAbsenteeDataByDate(final_eval_date);
                        qHtl_DailySchoolAbsenteeData.DeleteSchoolAbsenteeDataByDate(final_eval_date);
                        qHtl_DailySchoolDistrictAbsenteeSummary.DeleteSchoolDistrictAbsenteeSummaryByDate(final_eval_date);
                    }

                    // match the current school and get the school and district_id
                    // first make sure no apostrophes
                    string name_check = school_name.Replace("'", "");
                    var curr_school = qOrg_School.GetSchoolBySelector(name_check);

                    if (curr_school.SchoolID > 0)
                    {
                        // evalute if new school and run appropriate elements
                        curr_school_id = curr_school.SchoolID;
                        if (prior_school_id == 0)
                        {
                            prior_school_id = curr_school_id;
                            curr_school_district_id = curr_school.SchoolDistrictID;
                            curr_school_name = curr_school.School;
                            curr_school_level = curr_school.SchoolLevel;
                        }
                        else if (prior_school_id != curr_school_id || class_count == a_data.Count)
                        {
                            // save values
                            qHtl_DailySchoolAbsenteeData s_data = new qHtl_DailySchoolAbsenteeData();
                            s_data.Created = DateTime.Now;
                            s_data.MarkAsDelete = 0;
                            s_data.SchoolDistrictID = curr_school_district_id;
                            s_data.SchoolID = prior_school_id;
                            s_data.DataDate = Convert.ToDateTime(eval_date).Date;
                            s_data.School = curr_school_name;
                            s_data.SchoolLevel = curr_school_level;
                            s_data.DaysInSession = 1;

                            decimal eval_school_total_absent = Convert.ToDecimal(curr_school_total_absent);
                            decimal eval_school_total_enrolled = Convert.ToDecimal(curr_school_total_enrolled);
                            decimal eval_school_rate = 0;

                            if (num_school_saves > 0)
                            {
                                curr_school_total_absent += total_absent;
                                curr_school_total_unknown += total_unknown;
                                curr_school_total_other += total_other;
                                curr_school_total_sick += total_sick;
                                curr_school_gastrointestinal += gastrointestinal;
                                curr_school_respiratory += respiratory;
                                curr_school_rash += rash;
                                curr_school_other_illness += other_illness;
                                curr_school_unknown_illness += unknown_illness;
                                curr_school_total_enrolled += total_enrolled;
                            }
                            s_data.TotalAbsent = curr_school_total_absent;
                            s_data.TotalUnknown = curr_school_total_unknown;
                            s_data.TotalOther = curr_school_total_other;
                            s_data.TotalSick = curr_school_total_sick;
                            s_data.Gastrointestinal = curr_school_gastrointestinal;
                            s_data.Respiratory = curr_school_respiratory;
                            s_data.Rash = curr_school_rash;
                            s_data.OtherIllness = curr_school_other_illness;
                            s_data.UnknownIllness = curr_school_unknown_illness;
                            s_data.TotalEnrolled = curr_school_total_enrolled;

                            eval_school_rate = Decimal.Divide(curr_school_total_absent, s_data.TotalEnrolled) * 100;
                            s_data.Rate = eval_school_rate;

                            // school rate warning level -- need to get all prior day rates for this school
                            // curr_school_all_prior_rates --> load all prior rates into this list
                            double eval_rate = 0;
                            curr_school_all_prior_rates = qHtl_DailySchoolAbsenteeData.GetAllPriorDailyRatesforSpecificSchool(str_solution_start_date, Convert.ToString(DateTime.Now), prior_school_id, Convert.ToDouble(eval_school_rate));
                            if (variables.SDFormulaType == "population")
                                eval_rate = CalculateSTDPopulationFromList(curr_school_all_prior_rates);      // used to be curr_school_rates when was based on individual classrooms
                            else
                                eval_rate = CalculateSTDFromList(curr_school_all_prior_rates);

                            decimal overal_std = Convert.ToDecimal(eval_rate);
                            s_data.RateSTD = overal_std;

                            // school daily status evaluations
                            int school_illness_absences = (curr_school_gastrointestinal + curr_school_respiratory + curr_school_rash + curr_school_other_illness + curr_school_unknown_illness);

                            decimal school_gast_sym = CalculateSymptomRate(curr_school_gastrointestinal, school_illness_absences);
                            s_data.GastrointestinalRate = school_gast_sym;

                            decimal school_resp_sym = CalculateSymptomRate(curr_school_respiratory, school_illness_absences);
                            s_data.RespiratoryRate = school_resp_sym;

                            decimal school_rash_sym = CalculateSymptomRate(curr_school_rash, school_illness_absences);
                            s_data.RashRate = school_rash_sym;

                            decimal school_othr_sym = CalculateSymptomRate(curr_school_other_illness, school_illness_absences);
                            s_data.OtherRate = school_othr_sym;

                            decimal school_unkn_sym = CalculateSymptomRate(curr_school_unknown_illness, school_illness_absences);
                            s_data.UnknownRate = school_unkn_sym;

                            s_data.Insert();
                            num_school_saves++;

                            summary_sch_rates.Add(Convert.ToDouble(eval_school_rate));

                            // CALCULATE PRIOR SCHOOL RATE AVERAGES - 2 week average rate, prior year average, historic average
                            DateTime solution_start_date = new DateTime();
                            solution_start_date = Convert.ToDateTime(variables.SolutionStartDate);
                            DateTime end_date = new DateTime();
                            end_date = Convert.ToDateTime(s_data.DataDate);
                            DateTime two_weeks_prior = new DateTime();
                            two_weeks_prior = end_date.AddDays(-14);

                            decimal two_week_prior_rate = qHtl_DailySchoolAbsenteeData.CalculateSchoolValuesForRange(Convert.ToString(two_weeks_prior), Convert.ToString(end_date), s_data.Rate, s_data.SchoolID, "rate");
                            decimal historic_rate = qHtl_DailySchoolAbsenteeData.CalculateSchoolValuesForRange(Convert.ToString(solution_start_date), Convert.ToString(end_date), s_data.Rate, s_data.SchoolID, "rate");
                            decimal historic_rate_std = qHtl_DailySchoolAbsenteeData.CalculateSchoolValuesForRange(Convert.ToString(solution_start_date), Convert.ToString(end_date), s_data.RateSTD, s_data.SchoolID, "std");

                            s_data.TwoWeekRate = two_week_prior_rate;
                            s_data.HistoricRate = historic_rate;
                            s_data.HistoricRateSTD = historic_rate_std;
                            s_data.Update();

                            // reset values
                            curr_school_id = curr_school.SchoolID;
                            prior_school_id = curr_school_id;
                            curr_school_district_id = curr_school.SchoolDistrictID;
                            curr_school_name = curr_school.School;
                            curr_school_level = curr_school.SchoolLevel;
                            curr_school_total_absent = 0;
                            curr_school_total_unknown = 0;
                            curr_school_total_other = 0;
                            curr_school_total_sick = 0;
                            curr_school_gastrointestinal = 0;
                            curr_school_respiratory = 0;
                            curr_school_rash = 0;
                            curr_school_other_illness = 0;
                            curr_school_unknown_illness = 0;
                            curr_school_total_enrolled = 0;
                            curr_school_rate = 0;
                            s = 0;
                            curr_school_rates.Clear();

                            if (curr_school.SchoolLevel.ToLower().Contains("elem"))
                            {
                                total_elementary_schools++;
                            }
                            else if (curr_school.SchoolLevel.ToLower().Contains("jr") || curr_school.SchoolLevel.ToLower().Contains("middle"))
                            {
                                total_junior_high_schools++;
                            }
                            else if (curr_school.SchoolLevel.ToLower().Contains("high"))
                            {
                                total_high_schools++;
                            }
                        }

                        // run rate calculation
                        decimal eval_total_absent = Convert.ToDecimal(total_absent);
                        decimal eval_total_enrolled = Convert.ToDecimal(total_enrolled);
                        decimal curr_rate = 0;
                        if (eval_total_enrolled > 0)
                            curr_rate = (eval_total_absent / eval_total_enrolled) * 100;

                        // create daily absentee data
                        qHtl_DailyClassroomAbsenteeData c_data = new qHtl_DailyClassroomAbsenteeData();
                        c_data.Created = DateTime.Now;
                        c_data.MarkAsDelete = 0;
                        c_data.SchoolDistrictID = curr_school.SchoolDistrictID;
                        c_data.SchoolID = curr_school.SchoolID;
                        c_data.DataDate = Convert.ToDateTime(eval_date).Date;
                        c_data.School = curr_school.School;
                        c_data.SchoolLevel = curr_school.SchoolLevel;
                        c_data.GradeLevel = grade_level;
                        c_data.ClassRoom = class_name;
                        c_data.GradeNumber = Convert.ToInt32(grade_number);
                        c_data.Instructor = teacher_name;
                        c_data.DaysInSession = days_in_session;
                        c_data.Rate = curr_rate;
                        c_data.TotalAbsent = total_absent;
                        c_data.TotalUnknown = total_unknown;
                        c_data.TotalOther = total_other;
                        c_data.TotalSick = total_sick;
                        c_data.Gastrointestinal = gastrointestinal;
                        c_data.Respiratory = respiratory;
                        c_data.Rash = rash;
                        c_data.OtherIllness = other_illness;
                        c_data.UnknownIllness = unknown_illness;
                        c_data.TotalEnrolled = total_enrolled;
                        c_data.Insert();

                        // add values to overall school values
                        curr_school_total_absent += total_absent;
                        curr_school_total_unknown += total_unknown;
                        curr_school_total_other += total_other;
                        curr_school_total_sick += total_sick;
                        curr_school_gastrointestinal += gastrointestinal;
                        curr_school_respiratory += respiratory;
                        curr_school_rash += rash;
                        curr_school_other_illness += other_illness;
                        curr_school_unknown_illness += unknown_illness;
                        curr_school_total_enrolled += total_enrolled;
                        curr_school_rate += curr_rate;
                        s++;

                        // add values to daily summary values
                        if (c_data.SchoolLevel.ToLower().Contains("elem"))
                        {
                            total_elementary_students_enrolled += total_enrolled;
                            total_elementary_students_absent += total_absent;
                            total_elementary_absentee_rate += curr_rate;
                            elem_rates.Add(Convert.ToDouble(curr_rate));
                        }
                        else if (c_data.SchoolLevel.ToLower().Contains("jr") || c_data.SchoolLevel.ToLower().Contains("middle"))
                        {
                            total_junior_high_students_enrolled += total_enrolled;
                            total_junior_high_students_absent += total_absent;
                            total_junior_high_absentee_rate += curr_rate;
                            jr_rates.Add(Convert.ToDouble(curr_rate));
                        }
                        else if (c_data.SchoolLevel.ToLower().Contains("high"))
                        {
                            total_high_school_students_enrolled += total_enrolled;
                            total_high_school_students_absent += total_absent;
                            total_high_school_absentee_rate += curr_rate;
                            high_rates.Add(Convert.ToDouble(curr_rate));
                        }
                        total_students_enrolled += total_enrolled;
                        total_absentee_rate += curr_rate;

                        // add to overall list
                        sch_rates.Add(Convert.ToDouble(curr_rate));

                        // add to curr school list
                        curr_school_rates.Add(Convert.ToDouble(curr_rate));

                        // add to other lists
                        illness_rates.Add(Convert.ToDouble(gastrointestinal + respiratory + rash + other_illness + unknown_illness));
                        gast_rates.Add(Convert.ToDouble(gastrointestinal));
                        resp_rates.Add(Convert.ToDouble(respiratory));
                        rash_rates.Add(Convert.ToDouble(rash));
                        othr_rates.Add(Convert.ToDouble(other_illness));
                        unkn_rates.Add(Convert.ToDouble(unknown_illness));

                        // add values to summary list
                        summary_total_absent += total_absent;
                        summary_total_sick += total_sick;
                        summary_total_gastrointestinal += gastrointestinal;
                        summary_total_respiratory += respiratory;
                        summary_total_rash += rash;
                        summary_total_other_illness += other_illness;
                        summary_total_unknown_illness += unknown_illness;
                        summary_total_enrolled += total_enrolled;

                        success = true;
                        overall_success = true;
                    }
                }
                else if (mode == "test")
                {
                    success = true;
                }

                i++;

                if (mode == "process" && success == true)
                {
                    message += "Line " + i + " SUCCESS - " + curr_descriptor + " successfully added to absentee data<br>";
                }
                else if (mode == "test" && success == true)
                {
                    message += "Line " + i + " SUCCESS - " + curr_descriptor + " will be processed<br>";
                }
                else if (i > 1)
                {
                    message += "Line " + i + " FAILURE - could not be added for the following reason: " + reason + "<br>";
                }
                else
                {
                    message += "Line " + i + " LINE NOT PROCESSED - header line in the file<br>";
                }
            }

            // write summary data
            if (overall_success == true)
            {
                qHtl_DailySchoolDistrictAbsenteeSummary d_data = new qHtl_DailySchoolDistrictAbsenteeSummary();
                d_data.Created = DateTime.Now;
                d_data.DataDate = Convert.ToDateTime(eval_date).Date;
                d_data.SchoolDistrictID = curr_school_district_id;

                if (total_elementary_schools > 0)
                {
                    d_data.NumElementarySchools = total_elementary_schools;
                    d_data.TotalElementaryStudents = total_elementary_students_enrolled;
                    d_data.TotalElementaryStudentsAbsent = total_elementary_students_absent;
                    decimal eval_num_elementary_students_enrolled = Convert.ToDecimal(total_elementary_students_enrolled);
                    decimal eval_elem_rate = Convert.ToDecimal(total_elementary_students_absent / eval_num_elementary_students_enrolled) * 100;
                    d_data.ElementarySchoolAbsenteeRate = eval_elem_rate;
                }

                if (total_junior_high_schools > 0)
                {
                    d_data.NumJuniorHighs = total_junior_high_schools;
                    d_data.TotalJuniorHighStudents = total_junior_high_students_enrolled;
                    d_data.TotalJuniorHighStudentsAbsent = total_junior_high_students_absent;
                    decimal eval_num_junior_high_schools = Convert.ToDecimal(total_junior_high_schools);
                    decimal eval_jr_rate = Convert.ToDecimal(total_junior_high_absentee_rate / eval_num_junior_high_schools) * 100;
                    d_data.JuniorHighSchoolAbsenteeRate = eval_jr_rate;
                }

                if (total_high_schools > 0)
                {
                    d_data.NumHighSchools = total_high_schools;
                    d_data.TotalHighSchoolStudents = total_junior_high_students_enrolled;
                    d_data.TotalHighSchoolStudentsAbsent = total_high_school_students_absent;
                    decimal eval_num_high_schools = Convert.ToDecimal(total_high_schools);
                    decimal eval_high_rate = Convert.ToDecimal(total_high_school_absentee_rate / eval_num_high_schools) * 100;
                    d_data.HighSchoolAbsenteeRate = eval_high_rate;
                }

                // overall absentee rate
                decimal eval_total_enrolled = Convert.ToDecimal(total_elementary_students_enrolled + total_high_school_students_enrolled + total_high_school_students_enrolled);
                decimal eval_total_absent = Convert.ToDecimal(total_elementary_students_absent + total_junior_high_absentee_rate + total_high_school_students_absent);
                decimal eval_total_rate = Convert.ToDecimal(eval_total_absent / eval_total_enrolled) * 100;
                d_data.OverallAbsenteeRate = eval_total_rate;

                // calculate warning levels
                double elem_eval_rate = 0;
                if (variables.SDFormulaType == "population")
                    elem_eval_rate = CalculateSTDPopulationFromList(elem_rates);
                else
                    elem_eval_rate = CalculateSTDFromList(elem_rates);
                decimal elem_overal_std = Convert.ToDecimal(elem_eval_rate);
                d_data.ElementarySchoolAbsenteeSTD = elem_overal_std;

                double jr_eval_rate = 0;
                if (variables.SDFormulaType == "population")
                    jr_eval_rate = CalculateSTDPopulationFromList(jr_rates);
                else
                    jr_eval_rate = CalculateSTDFromList(jr_rates);
                decimal jr_overal_std = Convert.ToDecimal(jr_eval_rate);
                d_data.JuniorHighSchoolAbsenteeSTD = jr_overal_std;

                double high_eval_rate = 0;
                if (variables.SDFormulaType == "population")
                    high_eval_rate = CalculateSTDPopulationFromList(high_rates);
                else
                    high_eval_rate = CalculateSTDFromList(high_rates);
                decimal high_overal_std = Convert.ToDecimal(high_eval_rate);
                d_data.HighSchoolAbsenteeSTD = high_overal_std;

                double eval_rate = 0;
                if (variables.SDFormulaType == "population")
                    eval_rate = CalculateSTDPopulationFromList(summary_sch_rates);
                else
                    eval_rate = CalculateSTDFromList(summary_sch_rates);
                decimal overal_std = Convert.ToDecimal(eval_rate);
                d_data.OverallAbsenteeSTD = overal_std;

                // calculate detailed rates -- illness and symptom
                decimal illness_overall_rate = (summary_total_absent / summary_total_enrolled) * 100;
                d_data.IllnessRate = illness_overall_rate;

                int total_illness_absences = (summary_total_gastrointestinal + summary_total_respiratory + summary_total_rash + summary_total_other_illness + summary_total_unknown_illness);

                decimal gast_overall_std = CalculateSymptomRate(summary_total_gastrointestinal, total_illness_absences);
                d_data.GastrointestinalRate = gast_overall_std;

                decimal resp_overall_std = CalculateSymptomRate(summary_total_respiratory, total_illness_absences);
                d_data.RespiratoryRate = resp_overall_std;

                decimal rash_overall_std = CalculateSymptomRate(summary_total_rash, total_illness_absences);
                d_data.RashRate = rash_overall_std;

                decimal othr_overall_std = CalculateSymptomRate(summary_total_other_illness, total_illness_absences);
                d_data.OtherRate = othr_overall_std;

                decimal unkn_overall_std = CalculateSymptomRate(summary_total_unknown_illness, total_illness_absences);
                d_data.UnknownRate = unkn_overall_std;

                d_data.TotalAbsent = summary_total_absent;
                d_data.TotalSick = summary_total_sick;
                d_data.Gastrointestinal = summary_total_gastrointestinal;
                d_data.Respiratory = summary_total_respiratory;
                d_data.Rash = summary_total_rash;
                d_data.OtherIllness = summary_total_other_illness;
                d_data.UnknownIllness = summary_total_unknown_illness;
                d_data.TotalEnrolled = summary_total_enrolled;

                d_data.Insert();

                // CALCULATE PRIOR SCHOOL RATE AVERAGES - 2 week average rate, prior year average, historic average
                DateTime solution_start_date = new DateTime();
                solution_start_date = Convert.ToDateTime(variables.SolutionStartDate);
                DateTime end_date = new DateTime();
                end_date = Convert.ToDateTime(d_data.DataDate);
                DateTime two_weeks_prior = new DateTime();
                two_weeks_prior = end_date.AddDays(-14);

                // historic rate
                decimal two_week_prior_rate = qHtl_DailySchoolDistrictAbsenteeSummary.CalculateAbsenteeRateForRange(Convert.ToString(two_weeks_prior), Convert.ToString(end_date), d_data.OverallAbsenteeRate);
                decimal historic_rate = qHtl_DailySchoolDistrictAbsenteeSummary.CalculateAbsenteeRateForRange(Convert.ToString(solution_start_date), Convert.ToString(end_date), d_data.OverallAbsenteeRate);
                d_data.TwoWeekAbsenteeRate = two_week_prior_rate;
                d_data.HistoricAbsenteeRate = historic_rate;

                // historic std
                decimal historic_std = qHtl_DailySchoolDistrictAbsenteeSummary.CalculateAbsenteeRateSTDForRange(Convert.ToString(solution_start_date), Convert.ToString(end_date), d_data.OverallAbsenteeSTD);
                d_data.HistoricAbsenteeSTD = historic_std;

                // district prior rate stds
                double eval_historic_district_std = 0;
                curr_district_all_prior_rates = qHtl_DailySchoolDistrictAbsenteeSummary.GetAllPriorDailyRatesforDistrict(str_solution_start_date, Convert.ToString(DateTime.Now), 0);
                if (variables.SDFormulaType == "population")
                    eval_historic_district_std = CalculateSTDPopulationFromList(curr_district_all_prior_rates);      // used to be curr_school_rates when was based on individual classrooms
                else
                    eval_historic_district_std = CalculateSTDFromList(curr_district_all_prior_rates);

                decimal overall_district_std = Convert.ToDecimal(eval_historic_district_std);
                d_data.HistoricDistrictAbsenteeSTD = overall_district_std;

                // run analysis for this date
                DateTime curr_date = DateTime.Now;
                curr_date = Convert.ToDateTime(eval_date);

                int num_schools = qHtl_DailySchoolAbsenteeData.AnalyzeDailySchoolDataByDate(curr_date, school_district_id);

                // run code to count number of daily warnings
                int num_a_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("A", curr_date);
                int num_b_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("B", curr_date);
                int num_c_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("C", curr_date);
                int num_d_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("D", curr_date);
                int num_e_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("E", curr_date);
                int num_f_warn = qHtl_DailySchoolAbsenteeData.GetNumWarningsByTypeAndDay("F", curr_date);

                d_data.NumAWarnings = num_a_warn;
                d_data.NumBWarnings = num_b_warn;
                d_data.NumCWarnings = num_c_warn;
                d_data.NumDWarnings = num_d_warn;
                d_data.NumEWarnings = num_e_warn;
                d_data.NumFWarnings = num_f_warn;
                d_data.Update();
            }

            if (mode == "process")
            {
                lblMessage.Text = message;
                plhStep3.Visible = false;
                plhStep3Completed.Visible = true;
            }
            else if (mode == "test")
            {
                lblTestOutput.Text = message;
                plhStep3.Visible = true;
                plhStep2Completed.Visible = true;
                lblUploadFail.Text = "";
            }
        }
    }

    public List<string[]> parseCSV(string path)
    {
        List<string[]> parsedData = new List<string[]>();
        string error_message = string.Empty;

        //try
        //{
            using (StreamReader readFile = new StreamReader(path))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    parsedData.Add(row);
                }
            }
        //}
        //catch (Exception e)
        //{
        //    error_message += e.Message + "<br>";
        //}

        return parsedData;
    }


    public string fileFullName { get; set; }

    public double CalculateSTDFromList(List<double> rates)
    {
        if (rates.Count == 0)
            return 0;
        
        double sd = 0;

        if (rates.Count() > 0)
        {
            //Compute the Average      
            double avg = rates.Average();
            //Perform the Sum of (value-avg)_2_2      
            double sum = rates.Sum(d => Math.Pow(d - avg, 2));
            //Put it all together      
            sd = Math.Sqrt((sum) / (rates.Count() - 1));
        }

        return sd;
    }

    public static double CalculateSTDPopulationFromList(List<double> valueList)
    {
        if (valueList.Count == 0)
            return 0;

        double M = 0.0;
        double S = 0.0;
        int k = 1;
        foreach (double value in valueList)
        {
            double tmpM = M;
            M += (value - tmpM) / k;
            S += (value - tmpM) * (value - M);
            k++;
        }
        return Math.Sqrt(S / (k - 1));
    }

    public decimal CalculateSymptomRate(decimal num_syptoms, decimal num_absent)
    {
        decimal rate = 0;

        if (num_syptoms != 0 && num_absent != 0)
        {
            decimal eval_rate = (num_syptoms / num_absent) * 100;
            rate = eval_rate;
        }

        return rate;
    }

    public double CalculateAvgFromList(List<double> rates)
    {
        double avg = 0;

        if (rates.Count() > 0)
        {
            //Compute the Average      
            avg = rates.Average();
        }

        return avg;
    }

    public class ClassroomAbsenteeData
    {
        public string sort_by { get; set; }
        public string absent_date { get; set; }
        public string school_name { get; set; }
        public string school_type { get; set; }
        public string grade_level { get; set; }
        public int grade_number { get; set; }
        public string class_name { get; set; }
        public string teacher_name { get; set; }
        public int days_in_session { get; set; }
        public int total_enrolled { get; set; }
        public int total_absent { get; set; }
        public int total_unknown { get; set; }
        public int total_other { get; set; }
        public int total_sick { get; set; }
        public int gastrointestinal { get; set; }
        public int respiratory { get; set; }
        public int rash { get; set; }
        public int other_illness { get; set; }
        public int unknown_illness { get; set; }
    }
}