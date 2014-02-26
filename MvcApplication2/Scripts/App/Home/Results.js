(function () {
    "use strict";
    $('#myTab a').click(function (e) {
        e.preventDefault()
        $(this).tab('show')
    })
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
            event.preventDefault();
            var relationshipInfo;

            $(".lexical-content-block span").removeClass('selected');
            relationshipInfo = $.parseJSON($.trim($(event.target).children("span").text()));
            contextMenu.css({
                zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
                position: "absolute",
                display: "block",
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

            contextMenu.find("li").off();
            contextMenu.hide();

        });
    });

    //Something about the context menu is off. Links in the Navbar don't work with it enabled. Not sure where the bug is.
    //We can work together on friday to fix it if you haven't figured it out by then. 

    //$(".view-subects-menu-item").click(function (data) { })
}());