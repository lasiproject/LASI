var LASI;
(function (LASI) {
    (function (Results) {
        (function (LexicalContext) {
            "use strict";

            ;

            ;
        })(Results.LexicalContext || (Results.LexicalContext = {}));
        var LexicalContext = Results.LexicalContext;
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));

var LASI;
(function (LASI) {
    (function (Results) {
        "use strict";
        (function () {
            var highlightRelatedElement = function (element, index) {
                return $(element).addClass("selected");
            };
            var verbals = $(".lexical-content-block span.verbal");
            var referencers = $(".lexical-content-block span.referencer");

            var contextMenu = $("div#contextMenu");
            var displayContextMenuForElement = function (element, event) {
                return element.css({
                    // get the z-index of the parent modal containing the element and offset it by 50.
                    zIndex: Number($(event.target).parents(".modal").css("z-index")) + 50,
                    position: "absolute",
                    display: "block",
                    // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
                    left: event.pageX,
                    top: event.pageY
                });
            };
            var getInfoForVerbal = function (rawData) {
                var dataSource = $.parseJSON(rawData);
                return dataSource;
            };
            var getInfoForReferencer = function (rawData) {
                var dataSource = $.parseJSON(rawData);
                return dataSource;
            };

            var configureMenu = function (controls, relatedElementIds, elementAction) {
                controls.each(function (index, element) {
                    return relatedElementIds ? $(element).show() : $(element).hide();
                }).click(function (event) {
                    event.preventDefault();
                    (relatedElementIds || new Array()).map(function (element, index) {
                        return $("#" + element.toString());
                    }).forEach(elementAction);
                });
            };
            var createVerbalContextMenu = function (event) {
                event.preventDefault();
                var verbalContext;

                // Deselect any elements selected by a previous selection.
                $(".lexical-content-block span").removeClass("selected");
                verbalContext = getInfoForVerbal($.trim($(event.target).children("span").text()));
                if (verbalContext.directObjects && verbalContext.directObjects.length || verbalContext.indirectObjects && verbalContext.indirectObjects.length || verbalContext.subjects && verbalContext.subjects.length) {
                    displayContextMenuForElement(contextMenu, event);
                    configureMenu(contextMenu.find("ul").children(".view-subects-menu-item"), verbalContext.subjects, highlightRelatedElement);

                    configureMenu(contextMenu.find("ul").children(".view-directobjects-menu-item"), verbalContext.directObjects, highlightRelatedElement);

                    configureMenu(contextMenu.find("ul").children(".view-indirectobjects-menu-item"), verbalContext.indirectObjects, highlightRelatedElement);
                }
            };

            var createReferencerContextMenu = function (event) {
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

                    configureMenu(contextMenu.find("ul").children(".view-referredto-menu-item"), referencerInfo.referredTo, highlightRelatedElement);
                }
                return false;
            };

            verbals.on("contextmenu", createVerbalContextMenu);

            referencers.on("contextmenu", createReferencerContextMenu);

            $(document).click(function (event) {
                contextMenu.hide();
                contextMenu.find("li").off().hide();
            });

            $('.lexical').tooltip('test');
        })();
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=TextInteraction.js.map
