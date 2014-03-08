(function () {
    "use strict";
    Array.prototype.flatMap = function (collectionSelector, elementProjection) {
        var i, results = [];
        for (i = 0; i < this.length; i += 1) {
            var col = collectionSelector ? collectionSelector(this[i]) : this[i];
            col.forEach(function (element, index) {
                results.push(element);
            });
        }
        return elementProjection ? results.map(elementProjection) : results;
    };

    /// adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
    /// flatMap takes an array of items, projects each one to an array,using the provided collectionSelector function,
    /// and flattens the resulting array of arrays onto a single flat array.
    /// A second, optional function performs a projection on each element of the resulting flat array.
    /// This function is analagous to SelectMany in Linq, and flatMap in Scala.
    Array.prototype.correlate = function (inner, outerKeySelector, innerKeySelector, resultSelector) {
        var outerKeyed, innerKeyed, i, j, results;
        outerKeyed = this.map(function (e) {
            return { element: e, key: outerKeySelector(e) };
        });
        innerKeyed = inner.map(function (e) {
            return { element: e, key: innerKeySelector(e) };
        });
        results = [];
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
})();
//# sourceMappingURL=Initialize.js.map
