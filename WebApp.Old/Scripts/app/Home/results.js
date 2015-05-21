(function () {
    'use strict';
    // Load the Visualization API and the piechart package.
    google.load('visualization', '1.0', { 'packages': ['corechart'] });
    $('#myTab a').click(function (event) {
        event.preventDefault();
        $(this).tab('show');
    });
    var drawChart = function (targetElement, rowData) {
        return function () {
            // Create the data table.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Entity');
            data.addColumn('number', 'Weight');
            data.addRows(rowData);
            // Set chart options
            var options = {
                title: 'Top Results',
                width: 500,
                height: 500
            };
            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.BarChart(targetElement);
            chart.draw(data, options);
        };
    };
    var charts = $('.chart-container');
    charts.each(function (index, element) {
        var rowData = $.parseJSON($($(element).children('span')[0]).text());
        google.setOnLoadCallback(drawChart(element, rowData));
    });
})();
