module LASI.Results {
    "use strict";
    export interface VerbalContext {
        subjects: number[];
        directObjects: number[];
        indirectObjects: number[];
    };

    export interface RefencerContext {
        referredTo: number[];
    };
}

module LASI.Results {
    "use strict";
    var highlightRelatedElement = (element, index) => $(element).addClass("selected"),
        verbals = $(".lexical-content-block span.verbal"),
        referencers = $(".lexical-content-block span.referencer"),
        contextMenu = $("div#contextMenu"),
        displayContextMenuForElement = (element: JQuery, event: JQueryEventObject) => element.css({
            // get the z-index of the parent modal containing the element and offset it by 50.
            zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
            position: "absolute",
            display: "block",
            // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
            left: event.pageX,
            top: event.pageY
        }),
        getInfoForVerbal = (rawData: string): VerbalContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        },
        getInfoForReferencer = (rawData: string): RefencerContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        }, configureMenu = (controls: JQuery, relatedElementIds: number[], elementAction) => {
            controls
                .each((index, element) => relatedElementIds ? $(element).show() : $(element).hide())
                .click(event => {
                    event.preventDefault();
                    (relatedElementIds || [])
                        .map((element, index) => $("#" + element.toString()))
                        .forEach(elementAction);
                });
        }, createVerbalContextMenu = (event: JQueryEventObject) => {
            event.preventDefault();
            var verbalContext: VerbalContext;
            // Deselect any elements selected by a previous selection.
            $(".lexical-content-block span").removeClass("selected");
            verbalContext = getInfoForVerbal($.trim($(event.target).children("span").text()));
            if (verbalContext.directObjects && verbalContext.directObjects.length
                || verbalContext.indirectObjects && verbalContext.indirectObjects.length || verbalContext.subjects && verbalContext.subjects.length) {
                displayContextMenuForElement(contextMenu, event);
                configureMenu(
                    contextMenu.find("ul").children(".view-subects-menu-item"),
                    verbalContext.subjects,
                    highlightRelatedElement);
                configureMenu(
                    contextMenu.find("ul").children(".view-directobjects-menu-item"),
                    verbalContext.directObjects,
                    highlightRelatedElement);
                configureMenu(
                    contextMenu.find("ul").children(".view-indirectobjects-menu-item"),
                    verbalContext.indirectObjects,
                    highlightRelatedElement);
            }
        }, createReferencerContextMenu = (event: JQueryEventObject) => {
            event.preventDefault();
            var referencerInfo = getInfoForReferencer($.trim($(event.target).children("span").text()));
            // Deselect any elements selected by a previous selection.
            if (referencerInfo.referredTo && referencerInfo.referredTo.length) {
                $(".lexical-content-block span").removeClass("selected");
                contextMenu.css({
                    // get the z-index of the parent modal containing the element and offset it by 50.
                    zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
                    position: "absolute",
                    display: "block",
                    // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
                    left: event.pageX,
                    top: event.pageY
                });

                configureMenu(
                    contextMenu.find("ul").children(".view-referredto-menu-item"),
                    referencerInfo.referredTo,
                    highlightRelatedElement);
            }
            return false;
        };
    verbals.on("contextmenu", createVerbalContextMenu);
    referencers.on("contextmenu", createReferencerContextMenu);
    $(document).click(event => {
        contextMenu.hide();
        contextMenu.find("li").off().hide();
    });
    $('.lexical').tooltip('test');
};