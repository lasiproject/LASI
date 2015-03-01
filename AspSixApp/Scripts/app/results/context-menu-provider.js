$(function () {
    'use strict';
    /**
     * @description Creates a context menu builder which operates over the given context.
     * @param {Array} Context An array of objects representing the context. 
     * Each element exposes its lexical type and its instance id, associations with other 
     * lexicals are stored in a property which maps relationship types to the ids of the related elements.
     * @returns {Object} a context menu builder for the specified context.
     */
    var createForDocument = function (context) {

        var getAllBy$Ids = function (ids) { return $(ids.map(function (id) { return '#' + id; }).join(',')); };
        var $contextElements = getAllBy$Ids(context.map(function (e) { return e.id || e; }));
        var getRelatedElements = function (element, targetIdCollection) {
            $contextElements.filter(function (x) {
                element[targetIdCollection].some(function (id) {
                    return x.id === id;
                });
            });
        };
        return {
            verbal: function (rawData) {

                var verbal = $.parseJSON(rawData);
                var subjects = getRelatedElements(verbal, 'subjects'),
                    directObjects = getRelatedElements(verbal, 'directObjects'),
                    indirectObjects = getRelatedElements(verbal, 'indirectObjects');
                return {
                    subjects: subjects,
                    directObjects: directObjects,
                    indirectObjects: indirectObjects
                };
            }
        };
    };
    createForDocument(document);
});
