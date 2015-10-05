using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;
using Quartz.Organization;

public partial class school_districts_reports_DailySchoolAbsenteeDashboard : System.Web.UI.UserControl
{
    protected qHtl_DailySchoolDistrictAbsenteeSummary summary;
    protected DateTime curr_date;
    protected static string google_api_key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Site_GoogleAPIKey"]);

    public DateTime CurrDate
    {
        get { return curr_date; }
        set { curr_date = value; }
    }

    public string GoogleAPIKey
    {
        get { return google_api_key; }
        set { google_api_key = value; }
    }

    public qHtl_DailySchoolDistrictAbsenteeSummary Summary
    {
        get { return summary; }
        set { summary = value; }
    }   
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            int school_id = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["schoolID"]))
                school_id = Convert.ToInt32(Request.QueryString["schoolID"]);

            // get school daily summary
            var daily_summary = qHtl_DailySchoolAbsenteeData.GetSchoolDailySummaryByDate(school_id, Convert.ToString(curr_date));

            // school
            qOrg_School school = new qOrg_School(school_id);
            litSchoolTitle.Text = school.School;
            string address = string.Empty;
            address = school.Address1;
            if (!String.IsNullOrEmpty(school.Address2))
                address += "<br>" + school.Address2;
            address += "<br>" + school.City;
            address += ", " + school.StateProvince;
            address += " " + school.PostalCode;
            litSchoolAddress.Text = address;
            litSchoolPhone.Text = school.SchoolPhone;
            litSchoolFax.Text = school.SchoolFax;
            litSchoolLevel.Text = school.SchoolLevel;
            litDistrict.Text = school.District;
            
            // get classroom data
            List<ClassroomData> data = new List<ClassroomData>();
            data = qHtl_DailyClassroomAbsenteeData.LoadDailyClassroomDataInfoList(curr_date, school_id);

            // get past data
            string curr_mode = string.Empty;
            string mode_x_axis = "4 Weeks";
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                curr_mode = Request.QueryString["mode"];
                if (curr_mode == "28_day")
                {
                    lit28DayTitle.Text = "4 Weeks of " + school.School + " Absentee Data";
                    mode_x_axis = "4 Weeks";
                }
                else if (curr_mode == "this_year")
                {
                    lit28DayTitle.Text = "Current School Year " + school.School + " Absentee Data";
                    mode_x_axis = "Current School Year";
                }
                else
                {
                    lit28DayTitle.Text = "4 Weeks of " + school.School + " Absentee Data";
                    mode_x_axis = "4 Weeks";
                }
            }
            else
                lit28DayTitle.Text = "4 Weeks of " + school.School + " Absentee Data";

            // build options
            string filter_options = string.Empty;
            string qs_options = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["currDate"]))
                qs_options = "currDate=" + Request.QueryString["currDate"];

            filter_options += "<li><a href=\"/manage/school-districts/default.aspx?" + qs_options + "&mode=28_day\">Last 28 Days</a></li>";
            filter_options += "<li><a href=\"/manage/school-districts/default.aspx?" + qs_options + "&mode=this_year\">This School Year</a></li>";
            litFilterByDateOptions.Text = filter_options;

            // get 28 days of district data
            List<SchoolData> d_data = new List<SchoolData>();
            d_data = qHtl_DailySchoolAbsenteeData.LoadDayDailySchoolDataInfoListByMode(curr_mode, curr_date, school_id);

            litDailyDashboardTitle.Text = school.School + " Absentee Data for " + String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
            litCalendarDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
            litStatusCurrentDay.Text = String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
            litTotalEnrolled.Text = String.Format("{0:0,0}", daily_summary.TotalEnrolled);
            litTotalStudents.Text = String.Format("{0:0,0}", daily_summary.TotalEnrolled);
            litTotalAbsent.Text = String.Format("{0:0,0}", daily_summary.TotalAbsent);
            litTotalSick.Text = String.Format("{0:0,0}", daily_summary.TotalSick);
            litAbsenteeRate.Text = Convert.ToString(Math.Round(daily_summary.Rate, 2));
            litHistoricAbsenteeRate.Text = Convert.ToString(Math.Round(daily_summary.HistoricRate, 2));

            // ******************* google map -- school status *******************
            string strJSMap = string.Empty;
            strJSMap += "<script>\n";
            strJSMap += "function initialize() {\n";
            strJSMap += "var myLatlng = new google.maps.LatLng(" + school.Latitude + ", " + school.Longitude + ");\n";
            strJSMap += "var mapOptions = {\n";
            strJSMap += "zoom: 10,\n";
            strJSMap += "scrollwheel: false,\n";
            strJSMap += "center: myLatlng\n";
            strJSMap += "}\n";
            strJSMap += "var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);\n\n";
            strJSMap += "var marker = new google.maps.Marker({position: myLatlng, map: map, title: '" + school.School + "'});};\n\n";
            strJSMap += "google.maps.event.addDomListener(window, 'load', initialize);\n";
            strJSMap += "</script>\n\n";

            litMap.Text = strJSMap;
            // *********************************************************************

            if (data != null)
            {
                // create opening/closing for chart javascript
                string strJSLoad = string.Empty;

                string strJSOpening = string.Empty;
                strJSOpening += "<script type=\"text/javascript\">\n";
                litJSOpening.Text = strJSOpening;

                string strJSClosing = string.Empty;
                strJSClosing += "</script>\n";
                litJSClosing.Text = strJSClosing;

                string strJSChart = string.Empty;

                // ******************* google chart bar -- school list *******************
                string school_chart_title = "Classroom Absentee Rates " + String.Format("{0:M/d/yyyy}", curr_date);

                strJSChart += "function drawAbsences() {\n";
                strJSChart += "var dataAbsentee = google.visualization.arrayToDataTable([\n";

                strJSChart += "['Classroom', 'Classroom Daily Absentee Rate', { role: 'style' }, 'School Daily Absentee Rate', 'School Moving Average Absentee Rate'],\n";
                int j = 0;
                foreach (var s in data)
                {
                    if (j < data.Count && j > 0)
                        strJSChart += ",\n";
                    strJSChart += "['" + s.chart_classroom_name + "', " + Math.Round(s.rate, 2) + ", '" + s.classroom_color + "', " + Math.Round(daily_summary.Rate, 2) + ", " + Math.Round(daily_summary.HistoricRate, 2) + "]";
                    j++;
                }
                strJSChart += "]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: '" + school_chart_title + "',\n";
                strJSChart += "hAxis: {tile: \"Schools\", slantedText: true},\n";
                strJSChart += "vAxis: {title: 'Absentee Rate (%)'},\n";
                strJSChart += "seriesType: \"bars\",\n";
                strJSChart += "series: {1: {color: 'gray', type: \"line\"}, 2: {color: 'blue', type: \"line\"}}\n";
                strJSChart += "};\n\n";

                strJSChart += "var chart = new google.visualization.ComboChart(document.getElementById('visualizationSchoolAbsences'))\n";
                strJSChart += "chart.draw(dataAbsentee, options);\n\n";
                strJSChart += "}\n";

                strJSLoad += "drawAbsences();\n";

                strJSChart += "var selectHandler = function(e) {\n";
                strJSChart += "alert('just clicked on this item')\n";
                strJSChart += "window.location = data.getValue(chart.getSelection()[0]['row'], 1 );\n";
                strJSChart += "}\n";
                strJSChart += "google.visualization.events.addListener(chart, 'select', selectHandler);\n";

                litClassrooms.Text = "<div id=\"visualizationSchoolAbsences\" style=\"height: 600px;\"></div>";
                // *********************************************************************


                // ******************* google chart pie -- illnesses *******************
                int total_illness = daily_summary.Gastrointestinal + daily_summary.Rash + daily_summary.Respiratory + daily_summary.Rash + daily_summary.OtherIllness;
                int total_unknown = daily_summary.TotalAbsent - total_illness;

                strJSChart += "function drawIllnesses() {\n";
                strJSChart += "var data = google.visualization.arrayToDataTable([\n";
                strJSChart += "['Reason', 'Nuumber of Absences'],\n";
                strJSChart += "['Illness', " + total_illness + "],\n";
                strJSChart += "['Unknown', " + total_unknown + "]\n";
                strJSChart += "]);\n";
                strJSChart += "new google.visualization.PieChart(document.getElementById('visualizationIllness')).draw(data, {title:\"Types of Absences\"});\n";
                strJSChart += "}\n";

                strJSLoad += "drawIllnesses();\n";
                litTypesOfIllnessesChartGoogle.Text = "<div id=\"visualizationIllness\" style=\"width: 600px; height: 400px;\"></div>";
                // *********************************************************************


                // ******************* google chart bar -- symptoms *******************
                strJSChart += "function drawSymptoms() {\n";
                strJSChart += "var wrapper = new google.visualization.ChartWrapper({\n";
                strJSChart += "chartType: 'ColumnChart',\n";
                strJSChart += "dataTable: [['', 'Gastrointestinal', 'Respiratory', 'Rash', 'Other'],";
                strJSChart += "['', " + daily_summary.Gastrointestinal + ", " + daily_summary.Respiratory + ", " + daily_summary.Rash + ", " + daily_summary.OtherIllness + "]],\n";
                strJSChart += "options: {'title': 'Types of Illnesses'},\n";
                strJSChart += "containerId: 'visualization'\n";
                strJSChart += "});\n";
                strJSChart += "wrapper.draw();\n";
                strJSChart += "}\n\n";

                strJSLoad += "drawSymptoms();\n";
                litTypesOfSymptomsChartGoogle.Text = "<div id=\"visualization\" style=\"width: 600px; height: 400px;\"></div>";
                // *********************************************************************


                // ******************* google chart -- absentee rate *******************
                strJSChart += "function drawAbsenteeRates() {\n";
                strJSChart += "var dataAbsenteeRates = new google.visualization.DataTable();\n";
                strJSChart += "dataAbsenteeRates.addColumn('string', 'Date');\n";
                strJSChart += "dataAbsenteeRates.addColumn('number', 'School Daily Absentee Rate');\n";
                strJSChart += "dataAbsenteeRates.addColumn({type:'string', role:'annotation'});\n";
                strJSChart += "dataAbsenteeRates.addColumn({type:'string', role:'annotationText'});\n";
                strJSChart += "dataAbsenteeRates.addColumn('number', 'School Moving Average Absentee Rate');\n";
                strJSChart += "dataAbsenteeRates.addRows([\n";
                int l = 0;
                string prior_status_html = string.Empty;
                foreach (var r in d_data)
                {
                    if (l < d_data.Count && l > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = r.data_date.ToString("M/d/yyyy");
                    if (r.data_date == curr_date)
                    {
                        strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + Math.Round(r.rate, 2) + ", 'Report Date', '" + r.data_date.ToString("M/d/yyyy") + "', " + Math.Round(r.moving_rate_std, 2) + "]";
                    }
                    else
                    {
                        strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + Math.Round(r.rate, 2) + ", null, null, " + Math.Round(r.moving_rate_std, 2) + "]";
                    }

                    // build list of prior school day status indicators
                    prior_status_html += "<tr><td>" + String.Format("{0:d}", curr_chart_date) + "</td>";
                    if (r.a_warning == true)
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\"><a class=\"btn btn-red\" href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + r.school_id + "&currDate=" + r.data_date + "')\"><font color=\"white\">3 Sigma Warning <i class=\"icon-warning-sign\"></i></font></a></td>";
                    }
                    else if (r.b_warning == true)
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\"><a class=\"btn btn-orange\" href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + r.school_id + "&currDate=" + r.data_date + "')\"><font color=\"white\">Sustained 2-3 Sigma Watch <i class=\"icon-warning-sign\"></i></font></a></td>";
                    }
                    else if (r.c_warning == true)
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\"><a class=\"btn btn-lime\" href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + r.school_id + "&currDate=" + r.data_date + "')\"><font color=\"black\">Consistently Above Mean Watch <i class=\"icon-warning-sign\"></i></font></a></td>";
                    }
                    else if (r.d_warning == true)
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\"><a class=\"btn btn-blue\" href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + r.school_id + "&currDate=" + r.data_date + "')\"><font color=\"white\">Sustained Above 1 Sigma Alert <i class=\"icon-warning-sign\"></i></font></a></td>";
                    }
                    else if (r.e_warning == true)
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\"><a class=\"btn btn-purple\" href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + r.school_id + "&currDate=" + r.data_date + "')\"><font color=\"white\">Sustained Increase Alert <i class=\"icon-warning-sign\"></i></font></a></td>";
                    }
                    else
                    {
                        prior_status_html += "<td width=\"70%\" align=\"center\">Normal <i class=\"icon-ok-sign\"></i></font></td>";
                    }
                    prior_status_html += "</tr>";
                    l++;
                }
                litSchoolStatusPrior.Text = prior_status_html;

                strJSChart += "\n]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: '" + school.School + " Absenteeism Trends',\n";
                strJSChart += "hAxis: {title: '" + mode_x_axis + "', slantedText: true},\n";
                strJSChart += "vAxis: {title: 'School Absentee Rate (%)'},\n";
                strJSChart += "height: 500\n";
                strJSChart += "};\n";

                strJSChart += "var chart = new google.visualization.LineChart(document.getElementById('visualizationAbsenteeRate'));\n";
                strJSChart += "chart.draw(dataAbsenteeRates, options);\n";
                strJSChart += "}\n";

                strJSLoad += "drawAbsenteeRates();\n";
                lit28DaSchoolRateChart.Text = "<div id=\"visualizationAbsenteeRate\" style=\"height: 600px;\"></div>";
                // *********************************************************************


                // add all finalized JS
                string finalJSLoad = "google.load(\"visualization\", \"1.1\", {packages:[\"corechart\"]});\n";
                finalJSLoad += "google.setOnLoadCallback(init);\n\n";
                finalJSLoad += "function init() {\n";
                finalJSLoad = finalJSLoad + strJSLoad + "\n}\n\n";

                litJSChart.Text = strJSChart;

                // finalize load code
                litJSLoad.Text = "<script type=\"text/javascript\">\n" + finalJSLoad + "</script>";

                int a_e_warnings = 0;

                string warnings_html = string.Empty;
                string text = string.Empty;
                warnings_html += "<ul>";

                string warning_type = string.Empty;
                string warning_message = string.Empty;
                string tip_message = string.Empty;
                string warning_label = string.Empty;
                string warning_color = string.Empty;
                string warning_html_color = string.Empty;
                string font_color = "white";

                tip_message = daily_summary.School;
                warning_message = daily_summary.School;

                if (daily_summary.A_Warning == true)
                {
                    warning_type = "3 sigma warning";
                    warning_message += " (3 sigma warning is triggered for a school when current day absentee rate is greater than 3 sigma of the absentee rate)";
                    tip_message += " Data = [current day absentee rate = " + daily_summary.Rate + ", 3 sigma absentee rate = " + daily_summary.A_WarningValues + "]";
                    //warnings_html += "<li> <span class=\"label label-important\">" + warning_type + "</span> <a href=\"#\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_message + " <i class=\"icon-exclamation-sign\"></i></li>";
                    warning_label = "btn-red";
                    warning_color = "btn-red";
                    warning_html_color = "#e51400";
                    a_e_warnings++;
                }
                if (daily_summary.B_Warning == true)
                {
                    if (!String.IsNullOrEmpty(warning_type))
                        warning_type += ", Sustained 2-3 Sigma Watch";
                    else
                        warning_type = "Sustained 2-3 Sigma Watch";
                    warning_message += " (Sustained 2-3 Sigma Watch is triggered for a school when the 2 of the prior 3 absentee rates exceed 2 sigma of the absentee rate)";
                    tip_message += " Data = [current day absentee rate = " + daily_summary.Rate + ", 2 sigma absentee rates = " + daily_summary.B_WarningValues + "]";
                    //warnings_html += "<li> <span class=\"label label-important\">" + warning_type + "</span> <a href=\"#\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_message + " <i class=\"icon-exclamation-sign\"></i></li>";
                    a_e_warnings++;
                    warning_label = "btn-orange";
                    warning_html_color = "#f8a31f";
                    warning_color = "btn-orange";
                }
                if (daily_summary.C_Warning == true)
                {
                    if (!String.IsNullOrEmpty(warning_type))
                        warning_type += ", Consistently Above Mean Watch";
                    else
                        warning_type = "Consistently Above Mean Watch";
                    warning_message += " (Consistently Above Mean Watch is triggered for a school when at least 4 of the 5 prior absentee rates all exceed 1 sigma of the absentee rate)";
                    tip_message += " Data = [current day absentee rate = " + daily_summary.Rate + ", 1 sigma absentee rates = " + daily_summary.C_WarningValues + "]";
                    //warnings_html += "<li> <span class=\"label label-important\">" + warning_type + "</span> <a href=\"#\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_message + " <i class=\"icon-exclamation-sign\"></i></li>";
                    a_e_warnings++;
                    warning_label = "btn-lime";
                    warning_html_color = "#ffff00";
                    font_color = "black";
                }
                if (daily_summary.D_Warning == true)
                {
                    if (!String.IsNullOrEmpty(warning_type))
                        warning_type += ", Sustained Above 1 Sigma Alert";
                    else
                        warning_type = "Sustained Above 1 Sigma Alert";
                    warning_message += " (Sustained Above 1 Sigma Alert is triggered for a school when the 8 prior absentee rates are greater than the historic absentee rate)";
                    tip_message += " Data = [historic absentee rate = " + daily_summary.HistoricRate + ", 8 days prior absentee rates = " + daily_summary.D_WarningValues + "]";
                    //warnings_html += "<li> <span class=\"label label-important\">" + warning_type + "</span> <a href=\"#\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_message + " <i class=\"icon-exclamation-sign\"></i></li>";
                    a_e_warnings++;
                    warning_html_color = "#368ee0";
                    warning_label = "btn-blue";
                }
                if (daily_summary.E_Warning == true)
                {
                    if (!String.IsNullOrEmpty(warning_type))
                        warning_type += ", Sustained Increase Alert";
                    else
                        warning_type = "Sustained Increase Alert";
                    warning_message += " (Sustained Increase Alert is triggered for a school when the 6 prior absentee rates are all increasing)";
                    tip_message += " Data = [current day absentee rate = " + daily_summary.Rate + ", 8 days prior absentee rates = " + daily_summary.E_WarningValues + "]";
                    //warnings_html += "<li> <span class=\"label label-important\">" + warning_type + "</span> <a href=\"#\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_message + " <i class=\"icon-exclamation-sign\"></i></li>";
                    a_e_warnings++;
                    warning_html_color = "#a200ff";
                    warning_label = "btn-purple";
                }

                if (!String.IsNullOrEmpty(warning_type))
                {
                    if (font_color == "black")
                        warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" rel=\"popover\" class=\"btn " + warning_label + " btn-small\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + warning_message + ": " + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\"><font color=\"black\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></font></a> " + warning_message + " " + tip_message + "</div></li>";
                    else
                        warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" rel=\"popover\" class=\"btn " + warning_label + " btn-small\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + warning_message + ": " + tip_message + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></a> " + warning_message + " " + tip_message + "</div></li>";
                }

                if (a_e_warnings > 0)
                {
                    warnings_html += "</ul>";
                    litHealthWarnings.Text = warnings_html;
                }

                litNumAbsenteeWarnings.Text = "<span class=\"badge\">" + a_e_warnings + "</span>";

                qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(daily_summary.SchoolDistrictID);

                // symptom warnings
                string s_warning_type = string.Empty;
                string s_tip_message = string.Empty;
                string s_warnings_html = string.Empty;
                int num_symptom_warnings = 0;

                string currentStatus = "normal";
                plhStatusNormal.Visible = false;
                plhStatusCaution.Visible = false;
                plhStatusWarning.Visible = false;
                string currStatusDescription = string.Empty;

                string tip_data = string.Empty;
                if (daily_summary.GastrointestinalStatus == "red")
                {
                    warning_type = "Gastrointestinal Warning";
                    tip_message += "A Gastrointestinal warning is triggered for a school when gastroinstestinal-related absenses make up " + variables.RedGastrointestinalBoundary + "% or more of all reported absenses.";
                    tip_data = daily_summary.School + " Gastrointestinal-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(daily_summary.GastrointestinalRate, 2) + "%";
                    num_symptom_warnings++;
                }
                if (daily_summary.RashStatus == "red")
                {
                    warning_type = "Rash Warning";
                    tip_message += "A Rash warning is triggered for a school when rash-related absenses make up " + variables.RedRashBoundary + "% or more of all resported absenses.";
                    tip_data = daily_summary.School + " Rash-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(daily_summary.RashRate, 2) + "%"; ;
                    num_symptom_warnings++;
                }
                if (daily_summary.RespiratoryStatus == "red")
                {
                    warning_type = "Respiratory Warning";
                    tip_message += "A Respiratory warning is triggered for a school when respiratory-related absenses make up " + variables.RedRespiratoryBoundary + "% or more of all reported absenses.";
                    tip_data = daily_summary.School + " Respiratory-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(daily_summary.RespiratoryRate, 2) + "%";
                    num_symptom_warnings++;
                }

                if (!String.IsNullOrEmpty(warning_type))
                {
                    s_warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" class=\"btn btn-darkblue btn-small\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + " " + tip_data + "\" data-original-title=\"" + warning_type + " at " + daily_summary.School + "\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></a>";
                    s_warnings_html += "</div></li>";
                }
                if (a_e_warnings > 0)
                {
                    currentStatus = "warning";
                    currStatusDescription += "There are absentee warnings.<br>";
                }

                litNumSymptomWarnings.Text = "<span class=\"badge\">" + num_symptom_warnings + "</span>";

                string status_text = "School Status for <strong>" + String.Format("{0:M/d/yyyy}", curr_date) + "</strong><br><br>";
                if (currentStatus == "normal")
                {
                    plhStatusNormal.Visible = true;
                    currStatusDescription = "There are no absentee or symptom warnings. All other trends appear to be normal.";
                }
                //else if (currentStatus == "caution")
                //{
                //    plhStatusCaution.Visible = true;
                //    currStatusDescription = "There are illness symptom trends of concern.";
                //}
                else if (currentStatus == "warning")
                {
                    plhStatusWarning.Visible = true;
                    litWarningStatusColor.Text = "bgcolor=\"" + warning_html_color + "\"";
                    litWarningFooter.Text = warning_type;
                }
                litNormalHeader.Text = status_text;
                litCautionHeader.Text = status_text;
                litWarningHeader.Text = status_text;
                litStatusDescription.Text = "<i class=\"icon-info-sign\"></i> " + currStatusDescription;
            }
        }
    }
}