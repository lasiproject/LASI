(function (app) {
    app.enableActiveHighlighting = (function () {
        'use strict';
        var onReady = function () {
            var phrasalTextSpans = $('span.phrase'),
                highlightClass = 'active-phrase-highlight',
                recolor = function () {
                    phrasalTextSpans.each(function () {
                        $(this).removeClass(highlightClass);
                    });
                    $(this).addClass(highlightClass);
                };
            phrasalTextSpans.click(recolor);
            phrasalTextSpans.on('contextmenu', recolor);

            // bootstrap requires that tooltips be manually enabled. The data-toggle="tooltip" attributes set on each element
            // have no effect without this or an equivalent call. By setting container to 'body', we allow the contents of the 
            // tooltip to overflow its container. This is generally close to the desired behavior as it is difficult to predict width
            // and this gives good flexibility. There is probably a cleaner and more precise/obvious way of doing this, change to that if discovered.
            $('[data-toggle="tooltip"]').tooltip({
                delay: 250,
                container: 'body'
            });
            // TODO: look into fixing tooltips on elements containing a line break or remove such breaks.
        };
        $(onReady);
        return onReady;
    }());
}(LASI));
// Load the Visualization API and the piechart package.
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

