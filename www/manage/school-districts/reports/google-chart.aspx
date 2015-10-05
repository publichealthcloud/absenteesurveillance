<%@ Page Title="" Language="C#" MasterPageFile="~/manage/school-districts/school-district.master" AutoEventWireup="true" CodeFile="google-chart.aspx.cs" Inherits="manage_school_districts_reports_google_chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="school_nav" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="school_main" Runat="Server">

    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1');
    </script>
    <script type="text/javascript">
        function drawVisualization() {
            var wrapper = new google.visualization.ChartWrapper({
                chartType: 'ColumnChart',
                dataTable: [['', 'Germany', 'USA', 'Brazil', 'Canada', 'France', 'RU'],
                            ['', 700, 300, 400, 500, 600, 800]],
                options: { 'title': 'Countries' },
                containerId: 'visualization'
            });
            wrapper.draw();
        }
        google.setOnLoadCallback(drawVisualization);
    </script>

<div id="visualization" style="width: 600px; height: 400px;"></div>

</asp:Content>

