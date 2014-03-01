// Load the Visualization API and the piechart package.
google.load('visualization', '1.0', { 'packages': ['corechart'] });
(function () {
    "use strict";
    $('#myTab a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });
    var configureRelationshipMenu = function ($controls, relatedElementIds, elementAction) {
        $controls.each(function (index, element) {
            if (relatedElementIds) {
                $(element).show();
            } else {
                $(element).hide();
            }
        }).click(function (el) {
            el.preventDefault();

            relatedElementIds.map(function (element, index) {
                return $("#" + element.toString());
            }).forEach(elementAction);
        });
    };
    $(function () {
        var verbals = $(".lexical-content-block span.Verbal");
        var contextMenu = $("div#verbalContextMenu");
        var relatedElementAction = function (element, index) {
            $(element).addClass('selected');
        };
        verbals.on("contextmenu", function (event) {
            var relationshipInfo;
            event.preventDefault();
            // Deselect any elements selected by a previous selection.
            $(".lexical-content-block span").removeClass('selected');
            relationshipInfo = $.parseJSON($.trim($(event.target).children("span").text()));
            contextMenu.css({
                // get the z-index of the parent modal containing the element and offset it.
                zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
                position: "absolute",
                display: "block",
                // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
                left: event.pageX,
                top: event.pageY
            });
            configureRelationshipMenu(contextMenu.find("ul").children(".view-subects-menu-item"), relationshipInfo.Subjects, relatedElementAction);
            configureRelationshipMenu(contextMenu.find("ul").children(".view-directobjects-menu-item"), relationshipInfo.DirectObjects, relatedElementAction);
            configureRelationshipMenu(contextMenu.find("ul").children(".view-indirectobjects-menu-item"), relationshipInfo.IndirectObjects, relatedElementAction);

            return false;
        });
        $(document).click(function (e) {
            // The conflic between the menus and the navbar seems to have been caused by this line.
            //e.preventDefault();
            // Remove the previously bound event handlers from the items in the context menu so they will
            // start with an empty invocation list when reused by the next context menu event.
            contextMenu.find("li").off();
            contextMenu.hide();

        });
        var drawChart = function (id) {
            return function () {
                // Create the data table.
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Topping');
                data.addColumn('number', 'Slices');
                data.addRows([
                  ['Mushrooms', 3],
                  ['Onions', 1],
                  ['Olives', 1],
                  ['Zucchini', 1],
                  ['Pepperoni', 2]
                ]);

                // Set chart options
                var options = {
                    'title': 'Top Results',
                    'width': 500,
                    'height': 500
                };

                // Instantiate and draw our chart, passing in some options.
                var chart = new google.visualization.BarChart(document.getElementById(id));
                chart.draw(data, options);
            };
        };
        var charts = $(".chart-container");
        charts.each(function (index, element) {
            google.setOnLoadCallback(drawChart(element.id))
        });
    });
    // Set a callback to run when the Google Visualization API is loaded.
    //google.setOnLoadCallback(drawChart);

    //on('shown', function () {
    //    google.maps.event.trigger(map, "resize");
    //});
    // Callback that creates and populates a data table,
    // instantiates the pie chart, passes in the data and
    // draws it.

}());

