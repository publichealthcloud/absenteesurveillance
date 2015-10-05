using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Quartz.Health;

public partial class manage_school_districts_reports_controls_TypesIlnessesPieChart : System.Web.UI.UserControl
{
    protected qHtl_DailySchoolDistrictAbsenteeSummary summary;

    public qHtl_DailySchoolDistrictAbsenteeSummary Summary
    {
        get { return summary; }
        set { summary = value; }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (summary != null)
        {
            // create opening/closing for chart javascript
            string strJSLoad = string.Empty;

            string strJSOpening = string.Empty;
            strJSOpening += "<script type=\"text/javascript\">\n";
            litJSOpening.Text = strJSOpening;

            string strJSClosing = string.Empty;
            strJSClosing += "</script>\n";
            litJSClosing.Text = strJSClosing;

            // add individual charts
            string strJSChart = string.Empty;

            // ******************* google chart pie -- absentee rates *******************
            strJSChart += "function drawAbsenteeRates() {\n";
            strJSChart += "var data = google.visualization.arrayToDataTable([\n";
            strJSChart += "['Reason', 'Absentee Rates'],\n";
            strJSChart += "['Elementary School', " + Math.Round(summary.ElementarySchoolAbsenteeRate, 2) + "],\n";
            strJSChart += "['Junior High', " + Math.Round(summary.JuniorHighSchoolAbsenteeRate, 2) + "],\n";
            strJSChart += "['High School', " + Math.Round(summary.HighSchoolAbsenteeRate, 2) + "]\n";
            strJSChart += "]);\n";
            strJSChart += "new google.visualization.PieChart(document.getElementById('visualizationAbsenteeRates')).draw(data, {title:\"Absentee Rates\"});\n";
            strJSChart += "}\n";

            strJSLoad += "drawAbsenteeRates();\n";
            litAbsenteeRatesChartGoogle.Text = "<div id=\"visualizationAbsenteeRates\" style=\"width: 600px; height: 400px;\"></div>";

            // ******************* google chart bar -- symptoms *******************
            strJSChart += "function drawSymptoms() {\n";
            strJSChart += "var wrapper = new google.visualization.ChartWrapper({\n";
            strJSChart += "chartType: 'ColumnChart',\n";
            strJSChart += "dataTable: [['', 'Gastrointestinal', 'Respiratory', 'Rash', 'Unknown', 'Other'],";
            strJSChart += "['', " + summary.Gastrointestinal + ", " + summary.Respiratory + ", " + summary.Rash + ", " + summary.UnknownIllness + ", " + summary.OtherIllness + "]],\n";
            strJSChart += "options: {'title': 'Types of Illnesses'},\n";
            strJSChart += "containerId: 'visualization'\n";
            strJSChart += "});\n";
            strJSChart += "wrapper.draw();\n";
            strJSChart += "}\n";

            strJSLoad += "drawSymptoms();\n";
            litTypesOfSymptomsChartGoogle.Text = "<div id=\"visualization\" style=\"width: 600px; height: 400px;\"></div>";

            // finish load JS
            string finalJSLoad = "google.load(\"visualization\", \"1\", {packages:[\"corechart\"]});\n";
            finalJSLoad += "google.setOnLoadCallback(init);\n\n";
            finalJSLoad += "function init() {\n";
            finalJSLoad = finalJSLoad + strJSLoad + "\n}\n\n";

            // add all finalized JS
            litJSChart.Text = strJSChart;

            // finalize load code
            litJSLoad.Text = "<script type=\"text/javascript\">\n" + finalJSLoad + "</script>";
        }
    }
}