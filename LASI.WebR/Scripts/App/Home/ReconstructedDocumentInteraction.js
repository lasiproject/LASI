var LASI;
(function (LASI) {
    (function (Results) {
        (function (ContextProviders) {
            "use strict";

            ;

            ;
        })(Results.ContextProviders || (Results.ContextProviders = {}));
        var ContextProviders = Results.ContextProviders;
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

            var contextMenu = $("div#verbalContextMenu");
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
            var GetInfoForVerbal = function (rawData) {
                var dataSource = $.parseJSON(rawData);
                return dataSource;
            };
            var GetInfoForReferencer = function (rawData) {
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
            verbals.on("contextmenu", function (event) {
                event.preventDefault();
                var verbalContext;

                // Deselect any elements selected by a previous selection.
                $(".lexical-content-block span").removeClass("selected");
                verbalContext = GetInfoForVerbal($.trim($(event.target).children("span").text()));
                displayContextMenuForElement(contextMenu, event);

                configureMenu(contextMenu.find("ul").children(".view-subects-menu-item"), verbalContext.subjects, highlightRelatedElement);
                configureMenu(contextMenu.find("ul").children(".view-directobjects-menu-item"), verbalContext.directObjects, highlightRelatedElement);
                configureMenu(contextMenu.find("ul").children(".view-indirectobjects-menu-item"), verbalContext.indirectObjects, highlightRelatedElement);
                //return false;
            });

            referencers.on("contextmenu", function (event) {
                event.preventDefault();
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

                var referencerInfo = GetInfoForReferencer($.trim($(event.target).children("span").text()));

                // Deselect any elements selected by a previous selection.
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
                return false;
            });
            $(document).click(function (event) {
                contextMenu.hide();
                contextMenu.find("li").off().hide();
            });
        })();
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=ReconstructedDocumentInteraction.js.map
