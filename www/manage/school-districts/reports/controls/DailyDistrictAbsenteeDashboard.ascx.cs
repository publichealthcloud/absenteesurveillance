using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;

public partial class school_districts_reports_DailyDistrictAbsenteeDashboard : System.Web.UI.UserControl
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
            // get district day summary
            var daily_summary = qHtl_DailySchoolDistrictAbsenteeSummary.GetDailySummaryByDate(Convert.ToString(curr_date));
            
            // get school data
            List<SchoolData> data = new List<SchoolData>();
            data = qHtl_DailySchoolAbsenteeData.LoadDailySchoolDataInfoList(curr_date);

            // get past data
            string curr_mode = string.Empty;
            string mode_x_axis = "4 Weeks";
            List<DistrictData> d_data = new List<DistrictData>();
            if (!String.IsNullOrEmpty(Request.QueryString["mode"]))
            {
                curr_mode = Request.QueryString["mode"];
                if (curr_mode == "28_day")
                {
                    lit28DayTitle.Text = "4 Weeks of District Absentee Data";
                    mode_x_axis = "4 Weeks";
                }
                else if (curr_mode == "this_year")
                {
                    lit28DayTitle.Text = "Current School Year District Absentee Data";
                    mode_x_axis = "Current School Year";
                }
                else
                {
                    lit28DayTitle.Text = "4 Weeks of District Absentee Data";
                    mode_x_axis = "4 Weeks";
                }
            }

            // build options
            string filter_options = string.Empty;
            string qs_options = string.Empty;
            if (!String.IsNullOrEmpty(Request.QueryString["currDate"]))
                qs_options = "currDate=" + Request.QueryString["currDate"];

            filter_options += "<li><a href=\"/manage/school-districts/default.aspx?" + qs_options + "&mode=28_day\">Last 28 Days</a></li>";
            filter_options += "<li><a href=\"/manage/school-districts/default.aspx?" + qs_options + "&mode=this_year\">This School Year</a></li>";
            litFilterByDateOptions.Text = filter_options;

            d_data = qHtl_DailySchoolDistrictAbsenteeSummary.LoadDayDailyDistrictDataInfoListByMode(curr_mode, curr_date, 1);

            if (daily_summary != null)
            {
                litDailyDashboardTitle.Text = "District Absentee Data for " + String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
                litMapTitle.Text = "School Absentee Status Map for " + String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
                litCalendarDate.Text = String.Format("{0:dddd, MMMM d, yyyy}", curr_date);
                litTotalEnrolled.Text = String.Format("{0:0,0}", daily_summary.TotalEnrolled);
                litTotalAbsent.Text = String.Format("{0:0,0}", daily_summary.TotalAbsent);
                litTotalSick.Text = String.Format("{0:0,0}", daily_summary.TotalSick);
                litAbsenteeRate.Text = Convert.ToString(Math.Round(daily_summary.OverallAbsenteeRate, 2));
                litHistoricAbsenteeRate.Text = Convert.ToString(Math.Round(daily_summary.HistoricAbsenteeRate, 2));
            }

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
                string school_chart_title = "School Absentee Rates " + String.Format("{0:M/d/yyyy}", curr_date);

                strJSChart += "function drawAbsences() {\n";
                strJSChart += "var dataAbsentee = google.visualization.arrayToDataTable([\n";

                strJSChart += "['School', 'School Daily Rate', { role: 'style' }, 'District Daily Absentee Rate', 'District Moving Average Absentee Rate'],\n";
                int j = 0;
                foreach (var s in data)
                {
                    if (j < data.Count && j > 0)
                        strJSChart += ",\n";
                    strJSChart += "['" + s.school_name + "', " + Math.Round(s.rate, 2) + ", '" + s.school_color + "', " + Math.Round(daily_summary.OverallAbsenteeRate, 2) + ", " + Math.Round(daily_summary.HistoricAbsenteeRate, 2) + "]";
                    j++;
                }
                strJSChart += "]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: '" + school_chart_title + "',\n";
                strJSChart += "legend: 'none',\n";
                strJSChart += "hAxis: {title: '" + mode_x_axis + "', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Absentee Rate (%)'},\n";
                strJSChart += "seriesType: \"bars\",\n";
                strJSChart += "series: {1: {color: 'gray', type: \"line\"}, 2: {color: 'black', type: \"line\"}}\n";
                strJSChart += "};\n\n";

                strJSChart += "var chart = new google.visualization.ComboChart(document.getElementById('visualizationSchoolAbsences'))\n";
                strJSChart += "chart.draw(dataAbsentee, options);\n\n";
                strJSChart += "var selectHandler = function(e) {\n";
                strJSChart += "alert('just clicked on this item');\n";
                strJSChart += "window.location = data.getValue(chart.getSelection()[0]['row'], 1 );\n";
                strJSChart += "google.visualization.events.addListener(chart, 'select', selectHandler);\n";
                strJSChart += "}\n";
                strJSChart += "}\n";

                strJSLoad += "drawAbsences();\n";

                litSchools.Text = "<div id=\"visualizationSchoolAbsences\" style=\"height: 600px;\"></div>";
                // *********************************************************************

                // ******************* google map -- school status *******************
                string strJSMap = string.Empty;
                strJSMap += "<script>\n";
                strJSMap += "function initialize() {\n";
                strJSMap += "var myLatlng = new google.maps.LatLng(41.003231, -111.938957);\n";
                strJSMap += "var mapOptions = {\n";
                strJSMap += "zoom: 11,\n";
                strJSMap += "scrollwheel: false,\n";
                strJSMap += "center: myLatlng\n";
                strJSMap += "}\n";
                strJSMap += "var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);\n\n";
                foreach (var s in data)
                {
                    string warning_type = "Normal Absentee Rate";
                    string font_color = "white";
                    string map_color = s.school_color.Remove(0, 1);
                    string status_icon = "<i class=\"icon-warning-sign\"></i>";
                    string warning_details = string.Empty;

                    if (s.a_warning == true)
                        warning_details = "<strong>A 3 sigma warning is triggered for a school when current day absentee rate is greater than 3 sigma of the absentee rate.</strong> Data = [current day absentee rate = " + Math.Round(s.rate, 2) + ", 3 sigma absentee rate = " + s.a_warning_values + "]";
                    else if (s.b_warning == true)
                        warning_details = "<strong>Sustained 2-3 Sigma Watch is triggered for a school when the 2 of the prior 3 absentee rates exceed 2 sigma of the absentee rate.</strong> Data = [current day absentee rate = " + Math.Round(s.rate, 2) + ", 2 sigma absentee rates = " + s.b_warning_values + "]";
                    else if (s.c_warning == true)
                        warning_details = "<strong>Consistently Above Mean Watch is triggered for a school when at least 4 of the 5 prior absentee rates all exceed 1 sigma of the absentee rate.</strong> Data = [current day absentee rate = " + Math.Round(s.rate, 2) + ", 1 sigma absentee rates = " + s.c_warning_values + "]";
                    else if (s.d_warning == true)
                        warning_details = "<strong>Sustained Above 1 Sigma Alert is triggered for a school when the 8 prior absentee rates are greater than the historic absentee rate.</strong> Data = [historic absentee rate = " + Math.Round(s.moving_average_absences, 2) + ", 8 days prior absentee rates = " + s.d_warning_values + "]";
                    else if (s.e_warning == true)
                        warning_details = "<strong>Sustained Increase Alert is triggered for a school when the 6 prior absentee rates are all increasing.</strong> Data = [current day absentee rate = " + Math.Round(s.rate, 2) + ", 6 days prior absentee rates = " + s.e_warning_values + "]";
                    else if (s.f_warning == true)       //(s.school_color == "393")
                        warning_details = "<strong>2 Sigma Alert triggered for a school when current day absentee rate is greater than 2 sigma of the absentee rate.</strong>";

                    if (map_color == "e51400")
                        warning_type = "3 Sigma Warning";
                    else if (map_color == "f8a31f")
                        warning_type = "Sustained 2-3 Sigma Watch";
                    else if (map_color == "ffff00")
                    {
                        warning_type = "Consistently Above Mean Watch";
                        font_color = "black";
                    }
                    else if (map_color == "368ee0")
                        warning_type = "Sustained Above 1 Sigma Alert";
                    else if (map_color == "a200ff")
                        warning_type = "Sustained Increase Alert";
                    else if (map_color == "393")
                        warning_type = "2 Sigma Alert";
                    else
                        status_icon = "<i class=\"icon-ok-sign\"></i>";
                    strJSMap += "var contentString_" + s.school_id + " = '<div id=\"content\">'+\n";
                    strJSMap += "'<div id=\"siteNotice\">'+\n";
                    strJSMap += "'</div>'+\n";
                    strJSMap += "'<h2 id=\"firstHeading\" class=\"firstHeading\">" + s.school_name + "</h2>'+\n";
                    strJSMap += "'<div id=\"bodyContent\">'+\n";
                    strJSMap += "'<p><h4><i class=\"icon-flag\"></i> School Status for " + String.Format("{0:d}", curr_date) + "</h4></p>'+\n";
                    strJSMap += "'<p><table cellpadding=\"4\"><tr><td bgcolor=\"#" + map_color + "\">'+\n";
                    strJSMap += "'<font color=\"" + font_color + "\">" + warning_type + " " + status_icon + "</font>'+\n";
                    strJSMap += "'</td></tr></table></p>'+\n";
                    strJSMap += "'<p>" + warning_details + "</p>'+\n";
                    strJSMap += "'<p><a class=\"btn btn-small\" href=\"javascript:openSchoolWindowGeneric(\\\'/manage/school-districts/school-default.aspx?schoolID=" + s.school_id + "&currDate=" + curr_date + "\\\')\"><strong>Open " + s.school_name + "</strong> <i class=\"icon-external-link\"></i></a>'+\n";
                    strJSMap += "'</p>'+\n";
                    strJSMap += "'</div>'+\n";
                    strJSMap += "'</div>';\n\n";
                    strJSMap += "var infowindow_" + s.school_id + " = new google.maps.InfoWindow({content: contentString_" + s.school_id + "  });\n";
                    
                    strJSMap += "var pinColor_" + s.school_id + " = \"" + map_color + "\";\n";
                    strJSMap += "var pinImage_" + s.school_id + " = new google.maps.MarkerImage(\"http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=%E2%80%A2|\" + pinColor_" + s.school_id + ",\n";
                    strJSMap += "new google.maps.Size(21, 34),\n";
                    strJSMap += "new google.maps.Point(0,0),\n";
                    strJSMap += "new google.maps.Point(10, 34));\n";
                    strJSMap += "var pinShadow_" + s.school_id + " = new google.maps.MarkerImage(\"http://chart.apis.google.com/chart?chst=d_map_pin_shadow\",\n";
                    strJSMap += "new google.maps.Size(40, 37),\n";
                    strJSMap += "new google.maps.Point(0, 0),\n";
                    strJSMap += "new google.maps.Point(12, 35));\n";
                    strJSMap += "var marker_"+s.school_id+" = new google.maps.Marker(\n";
                    strJSMap += "{\n";
                    strJSMap += "position: new google.maps.LatLng(" + s.latitude + ", " + s.longitude + "),\n";
                    strJSMap += "map: map,\n";
                    strJSMap += "icon: pinImage_" + s.school_id + ",\n";
                    strJSMap += "shadow: pinShadow_" + s.school_id + ",\n";
                    strJSMap += "title: '" + s.school_name + "'\n";
                    strJSMap += "});\n";
                    strJSMap += "google.maps.event.addListener(marker_"+s.school_id+", 'click', function() {\n";
                    strJSMap += "infowindow_" + s.school_id + ".open(map,marker_" + s.school_id + ");\n";
                    strJSMap += "});\n";
                }
                strJSMap += "}\n\n";
                strJSMap += "google.maps.event.addDomListener(window, 'load', initialize);\n";
                strJSMap += "</script>\n\n";

                litMap.Text = strJSMap;
                // *********************************************************************


                // ******************* google chart pie -- illnesses *******************
                if (daily_summary != null)
                {
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
                }
                // *********************************************************************


                // ******************* google chart bar -- symptoms *******************
                if (daily_summary != null)
                {
                    strJSChart += "function drawSymptoms() {\n";
                    strJSChart += "var wrapper = new google.visualization.ChartWrapper({\n";
                    strJSChart += "chartType: 'ColumnChart',\n";
                    strJSChart += "dataTable: [['', 'Gastrointestinal', 'Respiratory', 'Rash', 'Other'], ";
                    strJSChart += "['', " + daily_summary.Gastrointestinal + ", " + daily_summary.Respiratory + ", " + daily_summary.Rash + ", " + daily_summary.OtherIllness + "]],\n";
                    strJSChart += "options: {'title': 'Types of Illnesses'},\n";
                    strJSChart += "containerId: 'visualization'\n";
                    strJSChart += "});\n";
                    strJSChart += "wrapper.draw();\n";
                    strJSChart += "}\n\n";

                    strJSLoad += "drawSymptoms();\n";
                    litTypesOfSymptomsChartGoogle.Text = "<div id=\"visualization\" style=\"width: 600px; height: 400px;\"></div>";
                }
                // *********************************************************************


                // ******************* google chart -- absentee rate *******************
                strJSChart += "function drawAbsenteeRates() {\n";
                strJSChart += "var dataAbsenteeRates = new google.visualization.DataTable();\n";
                strJSChart += "dataAbsenteeRates.addColumn('string', 'Date');\n";
                strJSChart += "dataAbsenteeRates.addColumn('number', 'District Daily Absentee Rate');\n";
                strJSChart += "dataAbsenteeRates.addColumn({type:'string', role:'annotation'});\n";
                strJSChart += "dataAbsenteeRates.addColumn({type:'string', role:'annotationText'});\n";
                strJSChart += "dataAbsenteeRates.addColumn('number', 'District Moving Average Absentee Rate');\n";
                strJSChart += "dataAbsenteeRates.addColumn('number', 'District Moving Average 2 Sigma');\n";
                strJSChart += "dataAbsenteeRates.addRows([\n";
                int l = 0;
                foreach (var r in d_data)
                {
                    if (l < d_data.Count && l > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = r.data_date.ToString("M/d/yyyy");
                    DateTime curr_day = new DateTime();
                    curr_day = Convert.ToDateTime(curr_chart_date);
                    string date_format = String.Format("{0:M/d/yyyy}", curr_day); ;
                    if (curr_day.DayOfWeek == DayOfWeek.Monday)
                        date_format = String.Format("{0:dddd, M/d/yyyy}", curr_day);
                    if (r.data_date == curr_date)
                        strJSChart += "['" + date_format + "', " + Math.Round(r.overall_absentee_rate, 2) + ", 'Report Date', '" + r.data_date.ToString("M/d/yyyy") + "', " + Math.Round(r.historic_absentee_rate, 2) + ", " + Math.Round(r.district_two_sigma, 2) + "]";
                    else
                        strJSChart += "['" + date_format + "', " + Math.Round(r.overall_absentee_rate, 2) + ", null, null, " + Math.Round(r.historic_absentee_rate, 2) + ", " + Math.Round(r.district_two_sigma, 2) + "]";
                    l++;
                }
                strJSChart += "\n]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: 'District Absenteeism Trends',\n";
                strJSChart += "hAxis: {title: '" + mode_x_axis + "', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'District Absentee Rate (%)'},\n";
                strJSChart += "height: 500\n";
                strJSChart += "};\n";

                strJSChart += "var chart = new google.visualization.LineChart(document.getElementById('visualizationAbsenteeRate'));\n";
                strJSChart += "chart.draw(dataAbsenteeRates, options);\n";
                strJSChart += "}\n";

                strJSLoad += "drawAbsenteeRates();\n";
                lit28DayDistrictRateChart.Text = "<div id=\"visualizationAbsenteeRate\" style=\"height: 600px;\"></div>";
                // *********************************************************************

                // ******************* google chart -- illness chart over time *******************
                strJSChart += "function drawIllnessRates() {\n";
                strJSChart += "var dataIllnessRates = new google.visualization.DataTable();\n";
                strJSChart += "dataIllnessRates.addColumn('string', 'Date');\n";
                strJSChart += "dataIllnessRates.addColumn('number', 'Gastrointestinal');\n";
                strJSChart += "dataIllnessRates.addColumn('number', 'Respiratory');\n";
                strJSChart += "dataIllnessRates.addColumn('number', 'Rash');\n";
                strJSChart += "dataIllnessRates.addColumn('number', 'Other Illnesses');\n";
                strJSChart += "dataIllnessRates.addColumn({type:'string', role:'annotation'});\n";
                //strJSChart += "dataIllnessRates.addColumn('number', 'Total Illnesses');\n";
                strJSChart += "dataIllnessRates.addRows([\n";
                int i = 0;
                foreach (var r in d_data)
                {
                    if (i < d_data.Count && i > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = r.data_date.ToString("M/d/yyyy");
                    //strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + r.gastrointestinal + ", " + r.respiratory + ", " + r.rash + ", " + r.other_illness + ", " + (r.gastrointestinal + r.respiratory + r.rash + r.other_illness) + "]";
                    DateTime curr_day = new DateTime();
                    curr_day = Convert.ToDateTime(curr_chart_date);
                    if (r.data_date == curr_date)
                    {
                        strJSChart += "['REPORT DATE', " + r.gastrointestinal + ", " + r.respiratory + ", " + r.rash + ", " + r.other_illness + ", '']";
                    }
                    else if (curr_day.DayOfWeek == DayOfWeek.Monday)
                    {
                        string date_format = String.Format("{0:dddd, M/d/yyyy}", curr_day);
                        strJSChart += "['" + date_format + "', " + r.gastrointestinal + ", " + r.respiratory + ", " + r.rash + ", " + r.other_illness + ", '']";
                    }
                    else
                        strJSChart += "['" + curr_chart_date + "', " + r.gastrointestinal + ", " + r.respiratory + ", " + r.rash + ", " + r.other_illness + ", '']";
                    i++;
                }
                strJSChart += "\n]);\n";

                strJSChart += "var options_i = {\n";
                strJSChart += "title: 'District Weekly Illness Trend',\n";
                strJSChart += "hAxis: {title: '" + mode_x_axis + "', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Illnesses'},\n";
                strJSChart += "height: 500,\n";
                strJSChart += "seriesType: \"bars\",\n";
                strJSChart += "isStacked: true\n";
                //strJSChart += "series: {4: {type: \"line\"}}\n";
                strJSChart += "};\n";

                //strJSChart += "var chart_i = new google.visualization.ComboChart(document.getElementById('visualizationIllnessRate'));\n";
                strJSChart += "var chart_i = new google.visualization.ColumnChart(document.getElementById('visualizationIllnessRate'));\n";
                strJSChart += "chart_i.draw(dataIllnessRates, options_i);\n";
                strJSChart += "}\n";

                strJSLoad += "drawIllnessRates();\n";
                lit28DayDistrictIllnessChart.Text = "<div id=\"visualizationIllnessRate\" style=\"height: 600px;\"></div>";
                // *********************************************************************

                // ******************* google chart -- warning chart over time *******************
                strJSChart += "function drawWarningRates() {\n";
                strJSChart += "var dataWarningRates = new google.visualization.DataTable();\n";
                strJSChart += "dataWarningRates.addColumn('string', 'Date');\n";
                strJSChart += "dataWarningRates.addColumn('number', '3 Sigma Warning');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addColumn('number', 'Sustained 2-3 Sigma Watch');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addColumn('number', 'Consistently Above Mean Watch');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addColumn('number', '2 Sigma Warning');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addColumn('number', 'Sustained Above 1 Sigma Alert');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addColumn('number', 'Sustained Increase Alert');\n";
                strJSChart += "dataWarningRates.addColumn({type:'string', role:'style'});\n";
                strJSChart += "dataWarningRates.addRows([\n";
                int w = 0;
                foreach (var r in d_data)
                {
                    if (w < d_data.Count && w > 0)
                        strJSChart += ",\n";
                    int curr_num_warnings = 0;
                    curr_num_warnings = r.num_a_warnings + r.num_b_warnings + r.num_c_warnings + r.num_d_warnings + r.num_e_warnings + r.num_f_warnings;

                    string curr_chart_date = r.data_date.ToString("M/d/yyyy");
                    DateTime curr_day = new DateTime();
                    curr_day = Convert.ToDateTime(curr_chart_date);
                    if (r.data_date == curr_date)
                    {
                        strJSChart += "['REPORT DATE', " + r.num_a_warnings + ", '#e51400', " + r.num_b_warnings + ", '#f8a31f', " + r.num_c_warnings + ", '#ffff00', " + r.num_f_warnings + ", '#393'," + r.num_d_warnings + ", '#368ee0', " + r.num_e_warnings + ", '#a200ff']";
                    }
                    else if (curr_day.DayOfWeek == DayOfWeek.Monday)
                    {
                        string date_format = String.Format("{0:dddd, M/d/yyyy}", curr_day);
                        //strJSChart += "['" + date_format + "', " + curr_num_warnings + "], '";
                        //strJSChart += "['" + date_format + "', '#e51400', " + r.num_a_warnings + ", '#f8a31f', " + r.num_b_warnings + ", '#8cbf26', " + r.num_c_warnings + ", '#368ee0', " + r.num_d_warnings + ", '#a200ff', " + r.num_e_warnings + "]";
                        strJSChart += "['" + date_format + "', " + r.num_a_warnings + ", '#e51400', " + r.num_b_warnings + ", '#f8a31f', " + r.num_c_warnings + ", '#ffff00', " + r.num_f_warnings + ", '#393'," + r.num_d_warnings + ", '#368ee0', " + r.num_e_warnings + ", '#a200ff']";
                    }
                    else
                    {
                        //strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + curr_num_warnings + "]";
                        //strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + r.num_a_warnings + ", '#e51400', " + r.num_b_warnings + ", '#f8a31f', " + r.num_c_warnings + ", '#8cbf26'," + r.num_d_warnings + ", '#368ee0', " + r.num_e_warnings + ", '#a200ff']";
                        strJSChart += "['" + String.Format("{0:d}", curr_chart_date) + "', " + r.num_a_warnings + ", '#e51400', " + r.num_b_warnings + ", '#f8a31f', " + r.num_c_warnings + ", '#8cbf26'," + r.num_f_warnings + ", '#393', " + r.num_d_warnings + ", '#368ee0', " + r.num_e_warnings + ", '#a200ff']";
                    }
                    w++;
                }
                strJSChart += "\n]);\n";

                strJSChart += "var options_w = {\n";
                strJSChart += "title: 'District Absentee Warning Trends',\n";
                strJSChart += "hAxis: {title: '" + mode_x_axis + "', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Warnings', gridlines: { count: 2 }},\n";
                strJSChart += "height: 500,\n";
                strJSChart += "legend: 'none',\n";
                strJSChart += "seriesType: \"bars\",\n";
                //strJSChart += "{format: 'none'},\n";
                //strJSChart += "vAxis: { gridlines: { count: 2 } },\n";
                strJSChart += "isStacked: true\n";
                strJSChart += "};\n";

                strJSChart += "var chart_w = new google.visualization.ColumnChart(document.getElementById('visualizationWarningRate'));\n";
                strJSChart += "chart_w.draw(dataWarningRates, options_w);\n";
                strJSChart += "}\n";

                strJSLoad += "drawWarningRates();\n";
                lit28DayDistrictWarningChart.Text = "<div id=\"visualizationWarningRate\" style=\"height: 600px;\"></div>";
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
                int num_a_warnings = 0;
                int num_b_warnings = 0;
                int num_c_warnings = 0;
                int num_d_warnings = 0;
                int num_e_warnings = 0;
                int num_f_warnings = 0;

                string warnings_html = string.Empty;
                string text = string.Empty;
                string school_link = string.Empty;
                warnings_html += "<ul>";

                foreach (var d in data)
                { 
                    string warning_type = string.Empty;
                    string warning_message = string.Empty;
                    string tip_message = string.Empty;
                    string warning_label = string.Empty;
                    string warning_color = string.Empty;
                    string font_color = "white";
                    string status_warning_code = string.Empty;

                    school_link = "at <strong><a href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + d.school_id + "&currDate=" + curr_date + "')\">" + d.school_name + " <i class=\"icon-external-link\"></i></a></strong>";

                    tip_message = d.school_name;

                    if (d.a_warning == true)
                    {
                        warning_type = "3 sigma warning";
                        warning_message += "A 3 sigma warning is triggered for a school when current day absentee rate is greater than 3 sigma of the absentee rate";
                        tip_message += " Data = [current day absentee rate = " + Math.Round(d.rate, 2) + ", 3 sigma absentee rate = " + d.a_warning_values + "]";
                        a_e_warnings++;
                        num_a_warnings++;
                        warning_label = "btn-red";
                        //warning_color = "btn-red";
                    }
                    else if (d.b_warning == true)
                    {
                        if (!String.IsNullOrEmpty(warning_type))
                            warning_type += ", Sustained 2-3 Sigma Watch";
                        else
                            warning_type = "Sustained 2-3 Sigma Watch";
                        warning_message += "Sustained 2-3 Sigma Watch is triggered for a school when the 2 of the prior 3 absentee rates exceed 2 sigma of the absentee rate";
                        tip_message += " Data = [current day absentee rate = " + Math.Round(d.rate, 2) + ", 2 sigma absentee rates = " + d.b_warning_values + "]";
                        a_e_warnings++;
                        num_b_warnings++;
                        warning_label = "btn-orange";
                        //warning_color = "btn-orange";
                    }
                    else if (d.c_warning == true)
                    {
                        if (!String.IsNullOrEmpty(warning_type))
                            warning_type += ", Consistently Above Mean Watch";
                        else
                            warning_type = "Consistently Above Mean Watch";
                        warning_message += "Consistently Above Mean Watch is triggered for a school when at least 4 of the 5 prior absentee rates all exceed 1 sigma of the absentee rate";
                        tip_message += " Data = [current day absentee rate = " + Math.Round(d.rate, 2) + ", 1 sigma absentee rates = " + d.c_warning_values + "]";
                        a_e_warnings++;
                        num_c_warnings++;
                        warning_label = "btn-lime";
                        font_color = "black";
                    }
                    else if (d.d_warning == true)
                    {
                        if (!String.IsNullOrEmpty(warning_type))
                            warning_type += ", Sustained Above 1 Sigma Alert";
                        else
                            warning_type = "Sustained Above 1 Sigma Alert";
                        warning_message += "Sustained Above 1 Sigma Alert is triggered for a school when the 8 prior absentee rates are greater than the historic absentee rate";
                        tip_message += " Data = [historic absentee rate = " + Math.Round(d.moving_average_absences, 2) + ", 8 days prior absentee rates = " + d.d_warning_values + "]";
                        a_e_warnings++;
                        num_d_warnings++;
                        warning_label = "btn-blue";
                    }
                    else if (d.e_warning == true)
                    {
                        if (!String.IsNullOrEmpty(warning_type))
                            warning_type += ", Sustained Increase Alert";
                        else
                            warning_type = "Sustained Increase Alert";
                        warning_message += " Sustained Increase Alert is triggered for a school when the 6 prior absentee rates are all increasing";
                        tip_message += " Data = [current day absentee rate = " + Math.Round(d.rate, 2) + ", 6 days prior absentee rates = " + d.e_warning_values + "]";
                        a_e_warnings++;
                        num_e_warnings++;
                        warning_label = "btn-purple";
                    }
                    else if (d.f_warning == true)
                    {
                        if (!String.IsNullOrEmpty(warning_type))
                            warning_type += ", 2 Sigma Alert";
                        else
                            warning_type = "2 Sigma Alert";
                        warning_message += " 2 Sigma Alert triggered for a school when current day absentee rate is greater than 2 sigma of the absentee rate";
                        tip_message += " Data = [current day absentee rate = " + Math.Round(d.rate, 2) + ", 2 sigma absentee rate = " + d.f_warning_values + "]";
                        a_e_warnings++;
                        num_f_warnings++;
                        warning_label = "btn-green";
                    }


                    if (!String.IsNullOrEmpty(warning_type))
                    {
                        if (font_color == "black")
                            warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" rel=\"popover\" class=\"btn " + warning_label + " btn-small\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + warning_message + ": " + tip_message + "\" data-original-title=\"" + warning_type + " at " + d.school_name + "\"><font color=\"black\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></font></a> " + school_link + "</div></li>";
                        else
                            warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" rel=\"popover\" class=\"btn " + warning_label + " btn-small\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + warning_message + ": " + tip_message + "\" data-original-title=\"" + warning_type + " at " + d.school_name + "\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></a> " + school_link + "</div></li>";
                    }
                }
                warnings_html += "</ul>";
                litHealthWarnings.Text = warnings_html;
                litNumAbsenteeWarnings.Text = "<span class=\"badge\">" + a_e_warnings + "</span>";
                if (a_e_warnings == 0)
                    litHealthWarnings.Text = "No absentee warnings for this date.";


                // symptom warnings
                warnings_html = "<ul>";
                int num_symptom_warnings = 0;
                string str_symptom_warnings = string.Empty;
                var symptom_warnings = qHtl_DailySchoolAbsenteeData.GetRedSymptomWarningsByDate(curr_date);
                if (symptom_warnings != null)
                {
                    foreach (var s in symptom_warnings)
                    {
                        qHtl_AbsenteeAnalysisVariable variables = new qHtl_AbsenteeAnalysisVariable(s.SchoolDistrictID);

                        string warning_type = string.Empty;
                        string tip_message = string.Empty;
                        string tip_data = string.Empty;

                        school_link = "at <strong><a href=\"javascript:openSchoolWindowGeneric('/manage/school-districts/school-default.aspx?schoolID=" + s.SchoolID + "&currDate=" + curr_date + "')\">" + s.School + " <i class=\"icon-external-link\"></i></a></strong>";

                        if (s.GastrointestinalStatus == "red")
                        {
                            warning_type = "Gastrointestinal Warning";
                            tip_message += "A Gastrointestinal warning is triggered for a school when gastroinstestinal-related absenses make up " + variables.RedGastrointestinalBoundary + "% or more of all reported absenses.";
                            tip_data = s.School + " Gastrointestinal-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(s.GastrointestinalRate, 2) + "%";
                            num_symptom_warnings++;
                        }
                        if (s.RashStatus == "red")
                        {
                            warning_type = "Rash Warning";
                            tip_message += "A Rash warning is triggered for a school when rash-related absenses make up " + variables.RedRashBoundary + "% or more of all resported absenses.";
                            tip_data = s.School + " Rash-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(s.RashRate, 2) + "%"; ;
                            num_symptom_warnings++;
                        }
                        if (s.RespiratoryStatus == "red")
                        {
                            warning_type = "Respiratory Warning";
                            tip_message += "A Respiratory warning is triggered for a school when respiratory-related absenses make up " + variables.RedRespiratoryBoundary + "% or more of all reported absenses.";
                            tip_data = s.School + " Respiratory-related absenses on " + String.Format("{0:d}", curr_date) + " = " + Math.Round(s.RespiratoryRate, 2) + "%";
                            num_symptom_warnings++;
                        }

                        if (!String.IsNullOrEmpty(warning_type))
                        {
                            warnings_html += "<li><div style=\"height:30px!important;\"><a href=\"#\" class=\"btn btn-darkblue btn-small\" rel=\"popover\" data-trigger=\"hover\" data-placement=\"left\" title=\"\" data-content=\"" + tip_message + " " + tip_data + "\" data-original-title=\"" + warning_type + " at " + s.School + "\">" + warning_type + "&nbsp;&nbsp;<i class=\"icon-info-sign\"></i></a> " + school_link;
                            warnings_html += "</div></li>";
                        }
                    }
                }
                warnings_html += "</ul>";
                litSymptomWarnings.Text = warnings_html;
                litNumSymptomWarnings.Text = "<span class=\"badge\">" + symptom_warnings.Count + "</span>";
                if (num_symptom_warnings == 0)
                    litSymptomWarnings.Text = "No symptom warnings for this date.";
            }
        }
    }
}