module LASI.Results.ContextProviders {
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
    $(() => {
        var GetInfoForVerbal = (rawData: string): ContextProviders.VerbalContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        }; var GetInfoForReferencer = (rawData: string): ContextProviders.RefencerContext => {
            var dataSource: any = $.parseJSON(rawData);
            return dataSource;
        };
        var configureMenu = (controls: JQuery, relatedElementIds: number[], elementAction) => {
            controls
                .each((index, element) => relatedElementIds ? $(element).show() : $(element).hide())
                .click(event=> {
                    event.preventDefault();
                    (relatedElementIds || new Array<number>())
                        .map((element, index) => $("#" + element.toString()))
                        .forEach(elementAction);
                });
        };
        var contextMenu = $("div#verbalContextMenu");

        var verbals = $(".lexical-content-block span.verbal");

        verbals.on("contextmenu", event => {
            var verbalInfo: ContextProviders.VerbalContext;
            event.preventDefault();
            // Deselect any elements selected by a previous selection.
            $(".lexical-content-block span").removeClass("selected");
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
            var highlightRelatedElement = (element, index) => $(element).addClass("selected");

            configureMenu(
                contextMenu.find("ul").children(".view-subects-menu-item"),
                verbalInfo.subjects,
                highlightRelatedElement);
            configureMenu(
                contextMenu.find("ul").children(".view-directobjects-menu-item"),
                verbalInfo.directObjects,
                highlightRelatedElement);
            configureMenu(
                contextMenu.find("ul").children(".view-indirectobjects-menu-item"),
                verbalInfo.indirectObjects,
                highlightRelatedElement);
            return false;
        });
        (function () {
            var referencers = $(".lexical-content-block span.referencer");
            referencers.on("contextmenu", event=> {
                event.preventDefault();
                var referencerInfo: ContextProviders.RefencerContext;
            });
        })();
        $(document).click(event=> contextMenu.find("li").off().hide());
    });
};