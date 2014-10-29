(function () {
    'use strict';
    // Load the Visualization API and the piechart package.
    google.load('visualization', '1.0', { 'packages': ['corechart'] });
    'use strict';
    $('#myTab a').click(function (event) {
        event.preventDefault();
        $(this).tab('show');
    });

    var drawChart = function (targetElement: Element, rowData) {
        return function () {
            // Create the data table.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Entity');
            data.addColumn('number', 'Weight');
            data.addRows(rowData);

            // Set chart options
            var options = {
                'title': 'Top Results',
                'width': 500,
                'height': 500
            };
            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.BarChart(targetElement);
            chart.draw(data, options);

        };
    };
    var charts = $('.chart-container');
    charts.each(function (index, element) {
        google.setOnLoadCallback(drawChart(element, $.parseJSON($($(element).children('span')[0]).text())));

    });
})();
// Set a callback to run when the Google Visualization API is loaded.
// google.setOnLoadCallback(drawChart);

// on('shown', function () {
//    google.maps.event.trigger(map, 'resize');
// });
// Callback that creates and populates a data table,
// instantiates the pie chart, passes in the data and
// draws it.

