module LASI.Results.LexicalContext {
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
    (function () {
        var highlightRelatedElement = (element, index) => $(element).addClass("selected");
        var verbals = $(".lexical-content-block span.verbal");
        var referencers = $(".lexical-content-block span.referencer");


        var contextMenu = $("div#contextMenu");
        var displayContextMenuForElement = (element: JQuery, event: JQueryEventObject) => element.css({
            // get the z-index of the parent modal containing the element and offset it by 50.
            zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
            position: "absolute",
            display: "block",
            // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
            left: event.pageX,
            top: event.pageY
        });
        var getInfoForVerbal = (rawData: string): LexicalContext.VerbalContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        }; var getInfoForReferencer = (rawData: string): LexicalContext.RefencerContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        };


        var configureMenu = (controls: JQuery, relatedElementIds: number[], elementAction) => {
            controls
                .each((index, element) => relatedElementIds ? $(element).show() : $(element).hide())
                .click(event => {
                    event.preventDefault();
                    (relatedElementIds || new Array<number>())
                        .map((element, index) => $("#" + element.toString()))
                        .forEach(elementAction);
                });
        };
        var createVerbalContextMenu = (event: JQueryEventObject) => {
            event.preventDefault();
            var verbalContext: LexicalContext.VerbalContext;
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
        };

        var createReferencerContextMenu = (event: JQueryEventObject) => {
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




        verbals
            .on("contextmenu", createVerbalContextMenu);

        referencers
            .on("contextmenu", createReferencerContextMenu);

        $(document).click(event => {
            contextMenu.hide();
            contextMenu.find("li").off().hide();
        });

        $('.lexical').tooltip('test');
    })();
};