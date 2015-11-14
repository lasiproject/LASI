System.register(['jquery', './results/result-chart-builder', 'github:twbs/bootstrap@3.3.5/css/bootstrap.css!', 'font-awesome', 'dist/app.css!', 'bootstrap', 'angular-ui-router', './utilities/augmentations', 'angular', './app'], function(exports_1) {
    /// <reference path="../../typings/tsd.d.ts" />
    'use strict';
    var $, angular_1, app_1;
    var buildMenus, log, modules;
    // Define the primary 'app' module, specifying all top level dependencies.
    function bootstrap() {
        function buildModule(m) {
            function isConfig(x) {
                return typeof x !== 'string';
            }
            function validate() {
                if (isConfig(m)) {
                    if (!m.name) {
                        throw new TypeError('name is required');
                    }
                    if (!m.requires) {
                        throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
                    }
                }
                else if (typeof m !== 'string') {
                    throw new TypeError('module must be a string or an AngularModuleOptions options object');
                }
            }
            if (isConfig(m)) {
                angular_1.module(m.name, m.requires.map(buildModule).slice(), m.configFn || (function () { }))
                    .provider(m.providers || {})
                    .factory(m.factories || {})
                    .service(m.services || {})
                    .filter(m.filters || {})
                    .controller(m.controllers || {})
                    .directive(m.directives || {})
                    .value(m.values || {})
                    .constant(m.constants || {})
                    .run(m.runFn || (function () { }));
                return m.name;
            }
            else {
                return m;
            }
        }
        modules.forEach(buildModule);
        angular_1.bootstrap(document, ['app'], { strictDi: true, debugInfoEnabled: true });
    }
    exports_1("bootstrap", bootstrap);
    return {
        setters:[
            function ($_1) {
                $ = $_1;
            },
            function (result_chart_builder_1_1) {
                exports_1({
                    "enableActiveHighlighting": result_chart_builder_1_1["default"]
                });
            },
            function (_1) {},
            function (_2) {},
            function (_3) {},
            function (_4) {},
            function (_5) {},
            function (_6) {},
            function (angular_1_1) {
                angular_1 = angular_1_1;
            },
            function (app_1_1) {
                app_1 = app_1_1;
            }],
        execute: function() {
            exports_1("buildMenus", buildMenus = function () {
                var contextualElementIdSelectors = [];
                var verbalMenuTextToElementsMap = {
                    'View Subjects': 'subjects',
                    'View Direct Objects': 'directObjects',
                    'View Indirect Objects': 'indirectObjects'
                };
                var relationshipCssClassNameMap = {
                    'View Subjects': 'subject-of-current',
                    'View Direct Objects': 'direct-object-of-current',
                    'View Indirect Objects': 'indirect-object-of-current'
                };
                return function () {
                    var forVerbal = function (context) {
                        var menu = JSON.parse($('#context' + context[0].id).text());
                        var subjects = menu.subjects, directObjects = menu.directObjects, indirectObjects = menu.indirectObjects;
                        var temp = {
                            subjects: subjects,
                            directObjects: directObjects,
                            indirectObjects: indirectObjects
                        }, result = {};
                        Object.keys(temp)
                            .filter(function (key) { return temp[key]; })
                            .forEach(function (key) { result[key] = temp[key]; });
                        return result;
                    }, forReferencer = function (context) {
                        var menu = JSON.parse($('#context' + context[0].id).text());
                        return menu;
                    };
                    $('span.referencer').contextmenu({
                        target: '#referencer-context-menu',
                        before: function (event, context) {
                            var data = forReferencer(context);
                            event.target.lexicalContextMenu = data;
                            return data.referredTo && data.referredTo.length > 0;
                        },
                        onItem: function (context) {
                            context[0].lexicalContextMenu.referredTo
                                .map(function (id) { return $('#' + id); })
                                .forEach(function ($e) { return $e.css('background-color', 'red'); });
                        }
                    });
                    $('span.verbal').contextmenu({
                        target: '#verbal-context-menu',
                        before: function (e, context) {
                            var count = 0;
                            var menu = forVerbal(context);
                            e.target.lexicalContextMenu = {};
                            Object.keys(menu).forEach(function (key) {
                                e.target.lexicalContextMenu[key] = menu[key].map(function (id) {
                                    var idSelector = '#' + id;
                                    if (!contextualElementIdSelectors.some(function (e) { return e === idSelector; })) {
                                        contextualElementIdSelectors.push(idSelector);
                                    }
                                    return idSelector;
                                });
                            });
                            [
                                { name: 'subjects', id: '#subjects-item' },
                                { name: 'directObjects', id: '#directobjects-item' },
                                { name: 'indirectObjects', id: '#indirectobjects-item' }
                            ].forEach(function (x) {
                                if (!menu[x.name]) {
                                    $(x.id).hide();
                                }
                                else {
                                    count += 1;
                                    $(x.id).show();
                                }
                            });
                            return count > 0;
                        },
                        onItem: function (context, event) {
                            var menu = context[0].lexicalContextMenu;
                            contextualElementIdSelectors
                                .flatMap(function (e) { return $(e).toArray(); }, $)
                                .forEach(function (e) {
                                Object.keys(relationshipCssClassNameMap).forEach(function (key) {
                                    e.removeClass(relationshipCssClassNameMap[key]);
                                });
                            });
                            menu[verbalMenuTextToElementsMap[event.target.text]]
                                .map($)
                                .forEach(function (e) {
                                e.addClass(relationshipCssClassNameMap[event.target.text]);
                            });
                        }
                    });
                    $(document).on('click', function () {
                        $('#verbal-context-menu').hide();
                        $('#referencer-context-menu').hide();
                    });
                };
            });
            //export var setupDraggableDialogs: () => void; 
            exports_1("log", log = console.log.bind(console));
            modules = [app_1.default];
        }
    }
});
