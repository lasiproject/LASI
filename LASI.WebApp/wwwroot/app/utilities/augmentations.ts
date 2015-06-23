/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */

(function () {
    'use strict';
    var arrayAgumentations: { name: string, value: PropertyDescriptor }[] =
        [
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
                        var outerKeyed, innerKeyed, i, j, results: { first: any, second: any }[] = [];
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
                    value: function (valueSelector: (any) => number) {
                        // If the a valueSelector was not provided, define a function which will attempt 
                        // to convert its argument to a number.
                        var projection = valueSelector || (x => Number(x));
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
                        return this.sum(valueSelector || (x => Number(x))) / this.length;
                    }
                }
            }
        ];
    arrayAgumentations
        .filter(property => !Object.prototype.hasOwnProperty.call(Array.prototype, property.name))
        .forEach(property => Object.defineProperty(Array.prototype, property.name, property.value));
})();
interface Array<T> {
    flatMap<TArray, TResult>(arraySelector: (element: T) => TArray[], elementSelector: (element: TArray) => TResult): TResult[];
    flatMap<U>(arraySelector: (element: T) => T, elementSelector: (element: T) => U): U[];
    flatMap(): any[];
    correlate<TInner, TKey>(inner: TInner[], outerKeySelector: (element: T) => TKey, innerKeySelector: (element: TInner) => TKey): { first: T, second: TInner }[];
    correlate<TInner, TKey, TResult>(inner: TInner[], outerKeySelector: (element: T) => TKey, innerKeySelector: (element: TInner) => TKey, resultSelector: (first: T, second: TInner) => TResult): {}[];
    sum(valueSelector?: (any) => number): number;
    average(valueSelector?: (any) => number): number;
} 