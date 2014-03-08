
interface Array<T> {

    /**
      * Projects each element of an Array into an array and flattens the result onto a single array.
      * @param collectionSelector A function to transform each element in the array into an array. 
      * @param elementProjection A function to perform an additional transformation on each element in the flattened collection. 
      */
    flatMap<TIntermediate, TResult>(collectionSelector: (element: T) => Array<TIntermediate>,
        elementProjection?: (element: TIntermediate) => TResult): Array<TResult>;
    /** 
      * Correlates the elements from two sequences based on the strict (===) equality of the keys selected from each element
      * by the provided selector functions. Models the semantics of a relational inner join.
      * Returns an array of tuple like elements containing all pairs of elements with equal keys.
      * @param inner An array whose elements will be correlated with the source array.  
      * @param outerKeySelector A function to produce a key value from each element in the outer array.
      * @param innerKeySelector A function to produce a key value from each element in the inner array.
      * @param resultSelector An optional function to transform each pair.
      */
    correlate<TInner, TOuterKey, TInnerKey, TResult>(inner: TInner[],
        outerKeySelector: (o: T) => TOuterKey,
        innerKeySelector: (i: TInner) => TInnerKey,
        resultSelector: (o: T, i: TInner) => TResult): Array<TResult>;



}
(function () {
    "use strict";
    Array.prototype.flatMap = function <T, TIntermediate, TResult>(collectionSelector: (element: T) => Array<TIntermediate>,
        elementProjection: (element: TIntermediate) => TResult) {

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
    Array.prototype.correlate = function <T, TInner, TOuterKey, TInnerKey, TResult>(inner: TInner[],
        outerKeySelector: (o: T) => TOuterKey,
        innerKeySelector: (i: TInner) => TInnerKey,
        resultSelector?: (o: T, i: TInner) => TResult) {

        var outerKeyed, innerKeyed, i, j, results;
        outerKeyed = this.map(function (e) { return { element: e, key: outerKeySelector(e) }; });
        innerKeyed = inner.map(function (e) { return { element: e, key: innerKeySelector(e) }; });
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
        return resultSelector ? results.map(function (e) { return resultSelector(e.first, e.second); }) : results;

    };

})();