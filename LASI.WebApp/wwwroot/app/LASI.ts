/// <reference path="../../typings/tsd.d.ts" />
'use strict';
import * as $ from 'jquery';

export { default as enableActiveHighlighting } from './results/result-chart-builder';
export var buildMenus = function () {
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
            var subjects = menu.subjects,
                directObjects = menu.directObjects,
                indirectObjects = menu.indirectObjects;
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
                    .map(id => $('#' + id))
                    .forEach($e => $e.css('background-color', 'red'));
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
                    } else {
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
};
//export var setupDraggableDialogs: () => void; 
export var log: (message?: any, ...optionalParams: any[]) => void = console.log.bind(console);
//export var editor = $('#free-editor').change(log); // TODO: parameterize selector.


import 'github:twbs/bootstrap@3.3.5/css/bootstrap.css!';
import 'font-awesome';
import 'dist/app.css!';
import 'bootstrap';
import 'angular-ui-router';
import './utilities/augmentations';
import * as LASI from './LASI';
import { module as registerAngularModule, bootstrap  as ngBootstrap} from 'angular';

import app from './app';
var modules = [app];
// Define the primary 'app' module, specifying all top level dependencies.

export function bootstrap() {
    function buildModule(m: AngularModuleOptions | string) {
        function isConfig(x): x is AngularModuleOptions {
            return typeof x !== 'string';
        }
        function validate() {
            if (isConfig(m)) {
                if (!m.name) {
                    throw new TypeError('name is required');
                } if (!m.requires) {
                    throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
                }
            } else if (typeof m !== 'string') {
                throw new TypeError('module must be a string or an AngularModuleOptions options object');
            }
        }
        if (isConfig(m)) {
            registerAngularModule(m.name, [...m.requires.map(buildModule)], m.configFn || (() => { }))
                .provider(m.providers || {})
                .factory(m.factories || {})
                .service(m.services || {})
                .filter(m.filters || {})
                .controller(m.controllers || {})
                .directive(m.directives || {})
                .value(m.values || {})
                .constant(m.constants || {})
                .run(m.runFn || (() => { }));

            return m.name;
        } else {
            return m;
        }
    }
    modules.forEach(buildModule);
    ngBootstrap(document, ['app'], { strictDi: true, debugInfoEnabled: true });
}