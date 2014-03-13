var LASI;
(function (LASI) {
    (function (Results) {
        (function (ContextProviders) {
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
        $(function () {
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
                    relatedElementIds.map(function (element, index) {
                        return $("#" + element.toString());
                    }).forEach(elementAction);
                });
            };
            var contextMenu = $("div#verbalContextMenu");

            var verbals = $(".lexical-content-block span.verbal");

            verbals.on("contextmenu", function (event) {
                var verbalInfo;
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
                var highlightRelatedElement = function (element, index) {
                    return $(element).addClass("selected");
                };

                configureMenu(contextMenu.find("ul").children(".view-subects-menu-item"), verbalInfo.subjects, highlightRelatedElement);
                configureMenu(contextMenu.find("ul").children(".view-directobjects-menu-item"), verbalInfo.directObjects, highlightRelatedElement);
                configureMenu(contextMenu.find("ul").children(".view-indirectobjects-menu-item"), verbalInfo.indirectObjects, highlightRelatedElement);
                return false;
            });
            (function () {
                var referencers = $(".lexical-content-block span.referencer");
                referencers.on("contextmenu", function (event) {
                    event.preventDefault();
                    var referencerInfo;
                });
            });

            $(document).click(function (event) {
                return contextMenu.find("li").off().hide();
            });
        });
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=ReconstructedDocumentInteraction.js.map
