/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    'use strict';
    var identity = function (x) { return x; };

    /** Adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
     * flatMap takes an array of items, projects each one to an array, using the provided
     * collectionSelector function,
     * and flattens the resulting array of arrays onto a single array.
     * A second, optional function performs a projection on each element of the resulting array.
     * This function is analogous to SelectMany in C#, and flatMap in Scala.
     */
    if (!Array.prototype.hasOwnProperty('flatMap').call) {
        Array.prototype.flatMap = function (arraySelector, itemSelector) {
            arraySelector = arraySelector || identity;
            itemSelector = itemSelector || identity;
            return this.reduce(function (result, array) {
                return result.concat(arraySelector(array).map(itemSelector));
            }, []);
        };
    }
    /** Adds the correlate function to  Array.prototype, endowing all
     * Arrays instances with it.
     * correlate takes pairs the two arrays of elements based on
     * the key selector functions, and returns the associated elements as a new array.
     * An optional function performs a projection on each pair.
     * This function is analogous to Join in C#, and inner join in SQL.
     */
    if (!Array.prototype.hasOwnProperty('correlate').call) {
        Array.prototype.correlate = function (inner, outerKeySelector, innerKeySelector, resultSelector) {
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
        };
    }
    if (!Array.prototype.hasOwnProperty('sum').call) {
        Array.prototype.sum = function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || function (x) { return Number(x); };
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        };
    }
    if (!Array.prototype.hasOwnProperty('product').call) {
        Array.prototype.product = function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || function (x) { return Number(x); };
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total * projection(element); }, 0);
        };
    }
    if (!Array.prototype.hasOwnProperty('average').call) {
        Array.prototype.average = function (valueSelector) {
            return this.sum(valueSelector || function (x) { return Number(x); }) / this.length;
        };
    }
}());