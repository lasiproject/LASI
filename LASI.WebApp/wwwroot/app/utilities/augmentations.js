/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    'use strict';
    var arrayAgumentations = [
        {
            name: 'flatMap',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (arraySelector, elementSelector) {
                    arraySelector = arraySelector || function (array) {
                        return array instanceof Array && array;
                    };
                    elementSelector = elementSelector || function (element) { return element; };
                    return this.reduce(function (array, a) {
                        return array.concat(arraySelector(a).map(elementSelector));
                    }, []);
                }
            }
        }, {
            name: 'correlate',
            value: {
                writable: false, enumerable: false, configurable: false, value: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
                    var outerKeyed, innerKeyed, i, j, results = [];
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
                }
            }
        }, {
            name: 'sum',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (valueSelector) {
                    // If the a valueSelector was not provided, define a function which will attempt 
                    // to convert its argument to a number.
                    var projection = valueSelector || (function (x) { return Number(x); });
                    return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
                }
            }
        },
        {
            name: 'average',
            value: {
                writable: false,
                enumerable: false,
                configurable: false,
                value: function (valueSelector) {
                    return this.sum(valueSelector || (function (x) { return Number(x); })) / this.length;
                }
            }
        }
    ];
    arrayAgumentations
        .filter(function (property) { return !Object.prototype.hasOwnProperty.call(Array.prototype, property.name); })
        .forEach(function (property) { return Object.defineProperty(Array.prototype, property.name, property.value); });
})();
