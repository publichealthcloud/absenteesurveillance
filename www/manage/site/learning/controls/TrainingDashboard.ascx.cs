using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Learning;
using Quartz.Portal;
using Quartz.Report;

public partial class manage_site_controls_SiteDashboard : System.Web.UI.UserControl
{
    public int training_id;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string resources_url = System.Configuration.ConfigurationManager.AppSettings["Learning_SlideIconBasePath"];
        string view_mode = "review";
        
        if (!Page.IsPostBack)
        {
            training_id = Convert.ToInt32(Request.QueryString["trainingID"]);
            
            qLrn_Training_View training = new qLrn_Training_View(training_id);
            litAboutTraining.Text = training.Description;

            // get icons for training
            var slide_icons = qLrn_TrainingSlide_View.GetAvailableSlidesInOrder(training_id);

            // get basic report data
            List<TrainingReport> report = new List<TrainingReport>();
            report = qRpt_LearningReport.GenerateTrainingReportSummary(training_id);

            litCreated.Text = Convert.ToString(training.Created.ToString("M/d/yyyy"));
            litEnrolledMembers.Text = Convert.ToString(report[0].num_enrolled);
            litFinishedMembers.Text = Convert.ToString(report[0].num_finished);
            litPassedOutMembers.Text = Convert.ToString(report[0].num_passed_out);
            litCompletionRate.Text = Convert.ToString(Math.Round(report[0].percent_finished, 2)) + "%";

            // check to see if there is a pre-assessment
            var pre = qLrn_Assessment.GetTrainingAssessment(training_id, "pre");
            var post = qLrn_Assessment.GetTrainingAssessment(training_id, "post");

            if (pre != null)
                litPreAssessmentPassRate.Text = Convert.ToString(Math.Round(report[0].percent_passed_pre_assessment, 2)) + "%";
            else
                litPreAssessmentPassRate.Text = "no pre-assessment";

            if (post != null)
                litPostAssessmentPassRate.Text = Convert.ToString(Math.Round(report[0].percent_passed_post_assessment, 2)) + "%";
            else
                litPostAssessmentPassRate.Text = "no post-assessment";

            // get prior 28 days data
            DateTime end_time = new DateTime();
            end_time = DateTime.Now.AddDays(1);
            DateTime start_time = new DateTime();
            start_time = DateTime.Now.AddDays(-28);

            List<TrainingReport> range_report = new List<TrainingReport>();
            range_report = qRpt_LearningReport.GenerateTrainingReportByDateRange(training_id, start_time, end_time);

            if (range_report != null)
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

                // ******************* google chart bar -- site report *******************
                strJSChart += "function drawSiteReport() {\n";
                strJSChart += "var dataReport = google.visualization.arrayToDataTable([\n";

                strJSChart += "['Date', 'Enrollment', { role: 'style' }, 'Training Started', 'Training Completed'],\n";
                int j = 0;
                foreach (var s in range_report)
                {
                    if (j < range_report.Count && j > 0)
                        strJSChart += ",\n";
                    string curr_chart_date = s.data_date.ToString("M/d/yyyy");
                    strJSChart += "['" + curr_chart_date + "', " + s.num_enrolled + ", 'lightblue', " + s.num_started + ", " + s.num_finished + "]";
                    j++;
                }
                strJSChart += "]);\n";

                strJSChart += "var options = {\n";
                strJSChart += "title: 'Last 28 Days of Training Activity',\n";
                strJSChart += "legend: 'none',\n";
                strJSChart += "hAxis: {title: 'Days', slantedText: true, textStyle: {fontSize: 12}},\n";
                strJSChart += "vAxis: {title: 'Activity'},\n";
                strJSChart += "seriesType: \"bars\",\n";
                strJSChart += "series: {1: {color: 'gray', type: \"line\"}, 2: {color: 'black', type: \"line\"}}\n";
                strJSChart += "};\n\n";

                strJSChart += "var chart = new google.visualization.ComboChart(document.getElementById('visualizationSiteReports'))\n";
                strJSChart += "chart.draw(dataReport, options);\n\n";
                strJSChart += "}\n";

                strJSLoad += "drawSiteReport();\n";

                litTrainingActivity.Text = "<div id=\"visualizationSiteReports\" style=\"height: 600px;\"></div>";
                // *********************************************************************

                // ******************* google chart bar -- completion rate *******************
                if (report != null)
                {
                    int not_started = report[0].num_enrolled - (report[0].num_finished + report[0].num_in_progress);
                    strJSChart += "function drawEnrollment() {\n";
                    strJSChart += "var data = google.visualization.arrayToDataTable([\n";
                    strJSChart += "['Status', 'Members'],\n";
                    strJSChart += "['Not Started', " + not_started + "],\n";
                    strJSChart += "['In Progress', " + report[0].num_in_progress + "],\n";
                    strJSChart += "['Finished', " + report[0].num_finished + "]\n";
                    strJSChart += "]);\n";
                    strJSChart += "new google.visualization.PieChart(document.getElementById('visualizationIllness')).draw(data, {title:\"Member Progress\"});\n";
                    strJSChart += "}\n";

                    strJSLoad += "drawEnrollment();\n";
                    litCompletionChart.Text = "<div id=\"visualizationIllness\" style=\"width: 600px; height: 400px;\"></div>";
                }
                // *********************************************************************

                // ******************* google combo chart -- assessment results *******************
                if (report != null)
                {
                    strJSChart += "function drawAssessment() {\n";
                    strJSChart += "var data = google.visualization.arrayToDataTable([\n";
                    strJSChart += "['Type', 'Passed', 'Failed'],\n";
                    if (pre != null)
                        strJSChart += "['Pre-Assessment', " + report[0].num_pre_assessments_passed + ", " + report[0].num_pre_assessments_failed + "],\n";
                    strJSChart += "['Post-Assessment', " + report[0].num_post_assessments_passed + ", " + report[0].num_post_assessments_failed + "]\n";
                    strJSChart += "]);\n";
                    strJSChart += "new google.visualization.ColumnChart(document.getElementById('visualizationAssessment')).draw(data, {title:\"Assessment Results\"});\n";
                    strJSChart += "}\n";

                    strJSLoad += "drawAssessment();\n";
                    litAssessmentChart.Text = "<div id=\"visualizationAssessment\" style=\"width: 600px; height: 400px;\"></div>";
                }
                // *********************************************************************

                // add all finalized JS
                string finalJSLoad = "google.load(\"visualization\", \"1.1\", {packages:[\"corechart\"]});\n";
                finalJSLoad += "google.setOnLoadCallback(init);\n\n";
                finalJSLoad += "function init() {\n";
                finalJSLoad = finalJSLoad + strJSLoad + "\n}\n\n";

                litJSChart.Text = strJSChart;

                // finalize load code
                litJSLoad.Text = "<script type=\"text/javascript\">\n" + finalJSLoad + "</script>";
            }

            if (slide_icons != null)
            {
                string icon_html = "<ul class=\"gallery\">";
                int i = 0;
                foreach (var s in slide_icons)
                {
                    i++;
                    icon_html += "<li>";
                    if (!String.IsNullOrEmpty(s.BgImage) && view_mode != "review")
						icon_html += "<a href=\"#\"><img src=\"" + resources_url + "/" + training_id + "/" + s.BgImage + "?width=200&height=150&mode=crop\"></a>";
                    else
                        icon_html += "<a href=\"#\"><img src=\"" + resources_url + "/" + training_id + "/" + s.SlideID + ".png?width=200&height=150&mode=crop\">";
                    icon_html += "<div class=\"extras\">";
                    icon_html += "<div class=\"extras-inner\">";
                    if (!String.IsNullOrEmpty(s.BgImage) && view_mode != "review")
                        icon_html += "<a href=\"" + resources_url + "/" + training_id + "/" + s.BgImage + "\" class='colorbox-image' rel=\"group-1\"><i class=\"icon-search\"></i></a>";
                    else
                        icon_html += "<a href=\"" + resources_url + "/" + training_id + "/" + s.SlideID + ".png\" class='colorbox-image' rel=\"group-1\"><i class=\"icon-search\"></i></a>";
                    icon_html += "</div>";
                    icon_html += i + ". " + s.Title;
                    icon_html += "</div>";
                    icon_html += "</li>";
                }
                icon_html += "</ul>";
                litSlides.Text = icon_html;
            }            
        }            
    }
}