var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        lexicalMenuBuilder.$inject = [];
        function lexicalMenuBuilder() {
            function verbalMenuIsViable(m) {
                return !!(m && (m.directObjectIds || m.indirectObjectIds || m.subjectIds));
            }
            function referencerMenuIsViable(m) {
                return !!(m && m.refersToIds);
            }
            var buildAngularMenuForReferencer = (function (menuActionTargets) {
                var resetCssClasses = function () { return Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); }); };
                return function (source) {
                    return [
                        [
                            'View Referred To',
                            function (s, e) {
                                resetCssClasses();
                                source.refersToIds.forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass('referred-to-by-current');
                                });
                            }
                        ]
                    ];
                };
            })({});
            var buildAngularMenuForVerbal = (function (menuActionTargets) {
                var verbalMenuTextToElementsMap = {
                    'View Subjects': 'subjects',
                    'View Direct Objects': 'directObjects',
                    'View Indirect Objects': 'indirectObjects'
                };
                var verbalMenuCssClassMap = {
                    'View Subjects': 'subject-of-current',
                    'View Direct Objects': 'direct-object-of-current',
                    'View Indirect Objects': 'indirect-object-of-current'
                };
                var resetCssClasses = function () { return Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) {
                    Object.keys(verbalMenuCssClassMap)
                        .map(function (k) { return verbalMenuCssClassMap[k]; })
                        .forEach(function (cssClass) { return $e.removeClass(cssClass); });
                }); };
                return function (source) {
                    var menuItems = [];
                    if (source.subjectIds) {
                        menuItems.push([
                            'View Subjects',
                            function (s, e) {
                                resetCssClasses();
                                source.subjectIds
                                    .forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Subjects']);
                                });
                            }
                        ]);
                    }
                    if (source.directObjectIds) {
                        menuItems.push([
                            'View Direct Objects',
                            function (s, e) {
                                resetCssClasses();
                                source.directObjectIds
                                    .forEach(function (id) { return menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Direct Objects']); });
                            }
                        ]);
                    }
                    if (source.indirectObjectIds) {
                        menuItems.push([
                            'View Indirect Objects',
                            function (s, e) {
                                resetCssClasses();
                                source.indirectObjectIds.forEach(function (id) {
                                    menuActionTargets[id] = $("#" + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                                });
                            }
                        ]);
                    }
                    return menuItems;
                };
            })({});
            var buildAngularMenu = function (m) {
                if (referencerMenuIsViable(m)) {
                    return buildAngularMenuForReferencer(m);
                }
                else if (verbalMenuIsViable(m)) {
                    return buildAngularMenuForVerbal(m);
                }
            };
            return {
                buildAngularMenu: buildAngularMenu
            };
        }
        angular
            .module(documentViewer.moduleName)
            .factory('lexicalMenuBuilder', lexicalMenuBuilder);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
