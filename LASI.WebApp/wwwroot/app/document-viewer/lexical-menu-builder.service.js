'use strict';
lexicalMenuBuilder.$inject = [];
function lexicalMenuBuilder() {
    var _a = [createForVerbalMenuBuilder({}), createForReferencerMenuBuilder({})], buildForVerbal = _a[0], buildForReferencer = _a[1];
    return {
        buildAngularMenu: function (source) {
            return referencerMenuIsViable(source) ?
                buildForReferencer(source) :
                verbalMenuIsViable(source) ?
                    buildForVerbal(source) :
                    undefined;
        }
    };
    function verbalMenuIsViable(menu) {
        return !!(menu && (menu.directObjectIds || menu.indirectObjectIds || menu.subjectIds));
    }
    function referencerMenuIsViable(menu) {
        return !!(menu && menu.refersToIds);
    }
}
exports.lexicalMenuBuilder = lexicalMenuBuilder;
function createForReferencerMenuBuilder(menuActionTargets) {
    var resetReferencerAsssotionCssClasses = function () {
        return Object.keys(menuActionTargets)
            .map(function (key) { return menuActionTargets[key]; })
            .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); });
    };
    return function (source) { return [
        ['View Referred To', function (itemScope, event) {
                resetReferencerAsssotionCssClasses();
                source.refersToIds.forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'); });
            }]
    ]; };
}
function createForVerbalMenuBuilder(menuActionTargets) {
    return (function (verbalMenuCssClassMap) {
        return function (source) {
            var menuItems = [];
            if (source.subjectIds) {
                menuItems.push(['View Subjects', function (itemScope, event) {
                        resetVerbalAssociationCssClasses();
                        source.subjectIds
                            .forEach(function (id) {
                            menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Subjects']);
                        });
                    }]);
            }
            if (source.directObjectIds) {
                menuItems.push(['View Direct Objects', function (itemScope, event) {
                        resetVerbalAssociationCssClasses();
                        source.directObjectIds
                            .forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Direct Objects']); });
                    }]);
            }
            if (source.indirectObjectIds) {
                menuItems.push(['View Indirect Objects', function (itemScope, event) {
                        resetVerbalAssociationCssClasses();
                        source.indirectObjectIds.forEach(function (id) {
                            menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                        });
                    }]);
            }
            return menuItems;
        };
        function resetVerbalAssociationCssClasses() {
            Object.keys(menuActionTargets)
                .map(function (key) { return menuActionTargets[key]; })
                .forEach(function ($e) {
                return Object.keys(verbalMenuCssClassMap)
                    .map(function (k) { return verbalMenuCssClassMap[k]; })
                    .forEach(function (cssClass) { return $e.removeClass(cssClass); });
            });
        }
    })({
        'View Subjects': 'subject-of-current',
        'View Direct Objects': 'direct-object-of-current',
        'View Indirect Objects': 'indirect-object-of-current'
    });
}
