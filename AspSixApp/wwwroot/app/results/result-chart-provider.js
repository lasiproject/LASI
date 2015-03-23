define(['jquery', 'bootstrap', 'bootstrapContextmenu'], function ($, bootstrap, bootstrapContextmenu) {
    'use strict';
    if (bootstrap && bootstrapContextmenu) {
        console.log(bootstrap);
        console.log(bootstrapContextmenu);
    }
    google.load('visualization', '1.0', { 'packages': ['corechart', 'table'] });
    var enableActiveHighlighting = (function () {
        var f = function () {
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
            $('[data-toggle="tooltip"]').tooltip({
                delay: 250,
                container: 'body'
            });
            // bootstrap requires that tooltips be manually enabled. The data-toggle="tooltip" attributes set on each element
            // have no effect without this or an equivalent call. By setting container to 'body', we allow the contents of the 
            // tooltip to overflow its container. This is generally close to the desired behavior as it is difficult to predict width
            // and this gives good flexibility. 
            // TODO: look into fixing tooltips on elements containing a line break or remove such breaks.
        };
        $(f);
        return f;
    }());
    $(function () {
        //Load the Visualization API and the piechart package.
        $('#myTab a').click(function (event) {
            event.preventDefault();
            $(this).tab('show');
        });
        var drawChart = function (targetElement, rowData) {
            return function () {
                // Create the data table.
                var dataTable = new google.visualization.DataTable({
                    cols: [
                        { id: 'Entity', label: 'Entity', type: 'string' },
                        { id: 'Weight', label: 'Weight', type: 'number' }
                    ]
                });
                dataTable.addRows(rowData);
                // Set chart options
                var options = {
                    title: 'Top Results',
                    width: 800,
                    height: 500
                };
                // Instantiate and draw our chart, passing in some options.
                var chart = new google.visualization.BarChart(targetElement);
                chart.draw(dataTable, options);

            };
        };
        var charts = $('.chart-container');
        charts.each(function () {
            var rowData = JSON.parse($($(this).children('span')[0]).text());
            google.setOnLoadCallback(drawChart(this, rowData));
        });

    });
    return { enableActiveHighlighting: enableActiveHighlighting };
});