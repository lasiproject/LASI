$(function () {
    'use strict';
    var idxs = document.getElementsByTagName('span'),
        actuals = [],
        i;
    for (i = 0; i < idxs.length; i += 1) {
        actuals.push(idxs[i].id);
    }
    /**
     * @description Creates a context menu builder which operates over the given context.
     * @param {Array} Context An array of objects representing the context. 
     * Each element exposes its lexical type and its instance id, associations with other 
     * lexicals are stored in a property which maps relationship types to the ids of the related elements.
     * @returns {Object} a context menu builder for the specified context.
     */
    var createForDocument = function (context) {

        var getAllBy$Ids = function (ids) { return $(ids.filter(function (id) { return id; }).map(function (id) { return '#' + id; }).join(',')); };
        var $contextElements = getAllBy$Ids(context.map(function (e) { return e.id || e; }));
        var getRelatedElements = function (element, targetIdCollection) {
            $contextElements.filter(function (x) {
                element[targetIdCollection].some(function (id) {
                    return x.id === id;
                });
            });
        };
        var forVerbal = function (rawData) {

            var verbal = $.parseJSON(rawData);
            var subjects = getRelatedElements(verbal, 'subjects'),
                directObjects = getRelatedElements(verbal, 'directObjects'),
                indirectObjects = getRelatedElements(verbal, 'indirectObjects');
            return {
                subjects: subjects,
                directObjects: directObjects,
                indirectObjects: indirectObjects
            };
        };
        $('.verbal').each(function (i, e) {
            $(e).contextmenu(function () {
                var data = forVerbal('#context' + e.id);
                i = i + 1;
                alert(e.data);
                return data;

            });
        });
    };
    createForDocument(actuals);
});
