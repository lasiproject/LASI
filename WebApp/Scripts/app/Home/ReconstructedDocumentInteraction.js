///<reference path=".\..\..\typings\jquery\jquery.d.ts" />
var GetInfoForVerbal = function (rawData) {
    var dataSource = $.parseJSON(rawData);
    return dataSource;
};

$(function () {
    var showApplicableMenuItems = function (controls, relatedElementIds, elementAction) {
        controls.each(function (index, element) {
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
    var verbals = $(".lexical-content-block span.verbal");
    var contextMenu = $("div#verbalContextMenu");
    var relatedElementAction = function (element, index) {
        $(element).addClass('selected');
    };
    verbals.on("contextmenu", function (event) {
        var verbalInfo;
        event.preventDefault();

        // Deselect any elements selected by a previous selection.
        $(".lexical-content-block span").removeClass('selected');
        verbalInfo = GetInfoForVerbal($.trim($(event.target).children("span").text()));
        contextMenu.css({
            // get the z-index of the parent modal containing the element and offset it by 50.
            zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
            position: "absolute",
            display: "block",
            // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
            left: event.pageX,
            top: event.pageY
        });
        showApplicableMenuItems(contextMenu.find("ul").children(".view-subects-menu-item"), verbalInfo.subjects, relatedElementAction);
        showApplicableMenuItems(contextMenu.find("ul").children(".view-directobjects-menu-item"), verbalInfo.directObjects, relatedElementAction);
        showApplicableMenuItems(contextMenu.find("ul").children(".view-indirectobjects-menu-item"), verbalInfo.indirectObjects, relatedElementAction);

        return false;
    });
    $(document).click(function (e) {
        // The conflic between the menus and the navbar seems to have been caused by this line.
        // Remove the previously bound event handlers from the items in the context menu so they will
        // start with an empty invocation list when reused by the next context menu event.
        contextMenu.find("li").off();
        contextMenu.hide();
    });
});
//# sourceMappingURL=ReconstructedDocumentInteraction.js.map
