//(function () {
//    /* global google: false*/
//    'use strict';
//    $(function () {
//        var phrasalTextSpans = $('span.phrase'),
//            highlightClass = 'active-phrase-highlight';
//        phrasalTextSpans.click(function () {
//            phrasalTextSpans.each(function () {
//                $(this).removeClass(highlightClass);
//            });
//            $(this).addClass(highlightClass);
//        });
//    });
//    // Load the Visualization API and the piechart package.
//    google.load('visualization', '1.0', { 'packages': ['corechart'] });
//    $('#myTab a').click(function (event) {
//        event.preventDefault();
//        $(this).tab('show');
//    });
//    var drawChart = function (targetElement, rowData) {
//        return function () {
//            // Create the data table.
//            var dataTable = new google.visualization.DataTable({
//                cols: [
//                    { id: 'Entity', type: 'string' },
//                    { id: 'Weight', type: 'string' }
//                ]
//            });
//            dataTable.addRows(rowData);
//            // Set chart options
//            var options = {
//                title: 'Top Results',
//                width: 500,
//                height: 500
//            };
//            // Instantiate and draw our chart, passing in some options.
//            var chart = new google.visualization.BarChart(targetElement);
//            chart.draw(dataTable, options);
//        };
//    };
//    var charts = $('.chart-container');
//    charts.each(function () {
//        var element = $(this);
//        var rowData = $.parseJSON($(element.children('span')[0]).text());
//        google.setOnLoadCallback(drawChart(element, rowData));
//    });
//}());

