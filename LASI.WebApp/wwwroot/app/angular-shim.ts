'use strict';
import 'font-awesome';
import 'github:twbs/bootstrap@3.3.6/css/bootstrap.css!';
import 'dist/app.css!';
import $ from 'jquery';
import 'bootstrap';
import 'angular';
import 'angular-ui-router';
import 'angular-bootstrap';
import 'angular-bootstrap-contextmenu';
import 'angular-file-upload';

/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    var arrayAgumentations = {
        flatMap: function (arraySelector, elementSelector) {
            arraySelector = arraySelector || function (array) {
                if (!Array.isArray(array)) {
                    throw new TypeError('expected an array');
                }
                return array;
            };
            elementSelector = elementSelector || function (element) { return element; };
            return this.reduce(function (array, a) {
                return array.concat(arraySelector(a).map(elementSelector));
            }, []);
        },
        correlate: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            var outerKeyed, innerKeyed, i, j;
            var results = [];
            outerKeyed = this.map(function (e) {
                return { element: e, key: outerKeySelector(e) };
            });
            innerKeyed = inner.map(function (e) {
                return { element: e, key: innerKeySelector(e) };
            });
            for (i = 0; i < outerKeyed.length; i += 1) {
                for (j = 0; j < innerKeyed.length; j += 1) {
                    if (outerKeyed[i].key === innerKeyed[j].key) {
                        results.push({ first: outerKeyed[i].element, second: innerKeyed[j].element });
                    }
                }
            }
            // If a function was supplied, map it over the array of result pairs and return the result.
            // otherwise, just return the array of result pairs.
            return resultSelector ? results.map(function (e) {
                return resultSelector(e.first, e.second);
            }) : results;
        },
        sum: function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || (function (x) { return Number(x); });
            return this.length === 0 ?
                0 :
                this.reduce(function (total, element) {
                    return total + projection(element);
                }, 0);
        },
        average: function (valueSelector) {
            return this.sum(valueSelector || (function (x) { return Number(x); })) / this.length;
        },
        first: function (predicate) {
            if (!predicate) {
                return this[0];
            }
            for (var i = 0; i < this.length; ++i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        },
        last: function (predicate) {
            if (!predicate) {
                return this[this.length - 1];
            }
            for (var i = this.length - 1; i > 0; --i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        }
    };
    Object.keys(arrayAgumentations)
        .filter(function (key) { return !Object.prototype.hasOwnProperty.call(Array.prototype, key); })
        .forEach(function (key) {
            Object.defineProperty(Array.prototype, key, {
                writable: false,
                enumerable: false,
                configurable: false,
                value: arrayAgumentations[key]
            });
        });
})();

export function registerAngularModule(module: NgModuleConfig | string) {
    function isConfig(x): x is NgModuleConfig {
        return typeof x !== 'string';
    }
    function validate() {
        if (isConfig(module)) {
            if (!module.name) {
                throw new TypeError('name is required');
            } if (!module.requires) {
                throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
            }
        } else if (typeof module !== 'string') {
            throw new TypeError('module must be a string or an AngularModuleOptions options object');
        }
    }
    if (isConfig(module)) {
        const configs = module.configs;
        const runs = module.runs;
        const configBlocks = configs && (Array.isArray(configs) ? configs : [configs]) || [];
        const runBlocks = runs && (Array.isArray(runs) ? runs : [runs]) || [];
        const app = angular.module(module.name, [...module.requires.map(registerAngularModule)])
            .provider(module.providers || {})
            .factory(module.factories || {})
            .service(module.services || {})
            .filter(module.filters || {})
            .controller(module.controllers || {})
            .directive(module.directives || {})
            .value(module.values || {})
            .constant(module.constants || {});
        configBlocks.forEach(app.config.bind(module));
        runBlocks.forEach(app.run.bind(module));
        return module.name;
    } else {
        return module;
    }
}

export default {
    $
}

