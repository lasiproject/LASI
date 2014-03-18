
/**
* Augments Array with the additonal methods, if and only if a member with the same name as the method has not already been defined.
*/
(function () {
    "use strict";

    /** Adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
    * flatMap takes an array of items, projects each one to an array,using the provided collectionSelector function,
    * and flattens the resulting array of arrays onto a single flat array.
    * A second, optional function performs a projection on each element of the resulting flat array.
    * This function is analagous to SelectMany in Linq, and flatMap in Scala.
    */
    if (!Array.prototype.hasOwnProperty("flatMap")) {
        Array.prototype.flatMap = function (collectionSelector, resultSelector) {
            var i, results = [];
            for (i = 0; i < this.length; i += 1) {
                var col = collectionSelector ? collectionSelector(this[i]) : this[i];
                col.forEach(function (element, index) {
                    results.push(element);
                });
            }
            return resultSelector ? results.map(resultSelector) : results;
        };
    }

    /** Adds the correlate function to  Array.prototype, endowing all
    * Arrays instances with it.
    * correlate takes pairs the two arrays of elements based on
    * the key selector functions, and returns the associated elements as a new array.
    * An optional function performs a projection on each element of the resulting flat array.
    * This function is analagous to SelectMany in Linq, and flatMap in Scala.
    */
    if (!Array.prototype.hasOwnProperty("correlate")) {
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
    if (!Array.prototype.hasOwnProperty("sum")) {
        Array.prototype.sum = function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt
            // to convert its argument to a number.
            var projection = valueSelector || (function (x) {
                return Number(x);
            });
            return this.length === 0 ? NaN : this.reduce(function (total, element, index) {
                return total + projection(element);
            }, 0);
        };
    }
    if (!Array.prototype.hasOwnProperty("average")) {
        Array.prototype.average = function (valueSelector) {
            return this.sum(valueSelector || (function (x) {
                return Number(x);
            })) / this.length;
        };
    }
    if (!Array.prototype.hasOwnProperty("toMap")) {
        Array.prototype.toMap = function (keySelector, valueSelector) {
            var results = new Map(), valueProjection = valueSelector || (function (v) {
                return v;
            });
            this.forEach(function (element, index) {
                results.set(keySelector(element), valueSelector(element));
            });
            return results;
        };
    }
    if (!String.prototype.hasOwnProperty("fmtAsPct")) {
        String.prototype.fmtAsPct = function (num) {
            return num.toString() + "%";
        };
    }
}());
//# sourceMappingURL=Initialize.js.map
