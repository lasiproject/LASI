var LASI;
(function (LASI) {
    (function (Results) {
        'use strict';

        ;

        ;
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));

var LASI;
(function (LASI) {
    (function (Results) {
        'use strict';
        var highlightRelatedElement = function (element, index) {
            return $(element).addClass('selected');
        }, verbals = $('.lexical-content-block span.verbal'), referencers = $('.lexical-content-block span.referencer'), contextMenu = $('div#contextMenu'), displayContextMenuForElement = function (element, event) {
            return element.css({
                // get the z-index of the parent modal containing the element and offset it by 50.
                zIndex: Number($(event.target).parents('.modal').css('z-index')) + 50,
                position: 'absolute',
                display: 'block',
                // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
                left: event.pageX,
                top: event.pageY
            });
        }, getInfoForVerbal = function (rawData) {
            var dataSource = $.parseJSON(rawData);
            return dataSource;
        }, getInfoForReferencer = function (rawData) {
            var dataSource = $.parseJSON(rawData);
            return dataSource;
        }, configureMenu = function (controls, relatedElementIds, elementAction) {
            controls.each(function (index, element) {
                return relatedElementIds ? $(element).show() : $(element).hide();
            }).click(function (event) {
                event.preventDefault();
                (relatedElementIds || []).map(function (element, index) {
                    return $('#' + element.toString());
                }).forEach(elementAction);
            });
        }, createVerbalContextMenu = function (event) {
            event.preventDefault();
            var verbalContext;

            // Deselect any elements selected by a previous selection.
            $('.lexical-content-block span').removeClass('selected');
            verbalContext = getInfoForVerbal($.trim($(event.target).children('span').text()));
            if (verbalContext.directObjects && verbalContext.directObjects.length || verbalContext.indirectObjects && verbalContext.indirectObjects.length || verbalContext.subjects && verbalContext.subjects.length) {
                displayContextMenuForElement(contextMenu, event);
                configureMenu(contextMenu.find('ul').children('.view-subects-menu-item'), verbalContext.subjects, highlightRelatedElement);
                configureMenu(contextMenu.find('ul').children('.view-directobjects-menu-item'), verbalContext.directObjects, highlightRelatedElement);
                configureMenu(contextMenu.find('ul').children('.view-indirectobjects-menu-item'), verbalContext.indirectObjects, highlightRelatedElement);
            }
        }, createReferencerContextMenu = function (event) {
            event.preventDefault();
            var referencerInfo = getInfoForReferencer($.trim($(event.target).children('span').text()));

            // Deselect any elements selected by a previous selection.
            if (referencerInfo.referredTo && referencerInfo.referredTo.length) {
                $('.lexical-content-block span').removeClass('selected');
                contextMenu.css({
                    // get the z-index of the parent modal containing the element and offset it by 50.
                    zIndex: Number($(event.target).parents('.modal').css('z-index')) + 50,
                    position: 'absolute',
                    display: 'block',
                    // Relocate the context menu div to the position of the click. This needs work to allow keyboard navigation.
                    left: event.pageX,
                    top: event.pageY
                });

                configureMenu(contextMenu.find('ul').children('.view-referredto-menu-item'), referencerInfo.referredTo, highlightRelatedElement);
            }
            return false;
        };
        verbals.on('contextmenu', createVerbalContextMenu);
        referencers.on('contextmenu', createReferencerContextMenu);
        $(document).click(function (event) {
            contextMenu.hide();
            contextMenu.find('li').off().hide();
        });
        $('.lexical').tooltip('test');
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=TextInteraction.js.map
