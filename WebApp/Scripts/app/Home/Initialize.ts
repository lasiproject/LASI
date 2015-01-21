// Declares additional Array methods.
interface Array<T> {

    /**
     * Takes an array of items, projects each one to an array,using the provided collectionSelector function,
     * and flattens the resulting array of arrays onto a single flat array. 
     * A second, optional function performs a projection on each element of the resulting flat array.
     * This function is analogous to SelectMany in C#, and flatMap in Scala. 
     * @param collectionSelector A function to transform each element in the array into an array. 
     * @param elementSelector A function to perform an additional transformation on each element in the flattened collection. 
     */
    flatMap<TIntermediate, TResult>(collectionSelector: (element: T) => Array<TIntermediate>,
        elementSelector?: (element: TIntermediate) => TResult): Array<TResult>;
    /** 
     * Correlates the elements from two sequences based on the strict ( === ) equality of the keys selected from each element
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
        resultSelector?: (o: T, i: TInner) => TResult): Array<TResult>;

    /**
     * Computes the sum of all elements of the Array.
     * If the array is empty, the result will be 0.
     * @param valueSelector A function to produce a numeric value from each element.
     */
    sum(valueSelector?: (element: T) => number): number;

    /**
     * Computes the average of all elements of the Array. 
     * If the array is empty, the result will be 0.
     * @param valueSelector A function to produce a numeric value from each element.
     */
    average(valueSelector?: (element: T) => number): number;
}
interface Map<K, V> {
    /** 
     * Returns an array containing all of the key value pairs in the map. The form of the output is { key:myKey;value:myValue} 
     */
    toArray();
}
interface String {
    formatAsPercent(num: number);
}

/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function () {
    'use strict';

    /** Adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
     * flatMap takes an array of items, projects each one to an array,using the provided
     * collectionSelector function,
     * and flattens the resulting array of arrays onto a single flat array. 
     * A second, optional function performs a projection on each element of the resulting flat array.
     * This function is analogous to SelectMany in C#, and flatMap in Scala. 
     */
    if (!Array.prototype.hasOwnProperty('flatMap')) {
        Array.prototype.flatMap = function <T, TIntermediate, TResult>(collectionSelector: (element: T) => Array<TIntermediate>,
            resultSelector: (element: TIntermediate) => TResult) {
            var i, results = [];
            for (i = 0; i < this.length; i += 1) {
                var collection = collectionSelector ? collectionSelector(this[i]) : this[i];
                collection.forEach(function (element, index) {
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
     * An optional function performs a projection on each pair.
     * This function is analogous to Join in C#, and inner join in SQL. 
     */
    if (!Array.prototype.hasOwnProperty('correlate')) {
        Array.prototype.correlate = function <T, TInner, TOuterKey, TInnerKey, TResult>(
            inner: TInner[],
            outerKeySelector: (o: T) => TOuterKey,
            innerKeySelector: (i: TInner) => TInnerKey,
            resultSelector?: (o: T, i: TInner) => TResult) {

            var outerKeyed, innerKeyed, i, j, results = [];
            outerKeyed = this.map(function (e) { return { element: e, key: outerKeySelector(e) }; });
            innerKeyed = inner.map(function (e) { return { element: e, key: innerKeySelector(e) }; });

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
    }
    if (!Array.prototype.hasOwnProperty('sum')) {
        Array.prototype.sum = function <T>(valueSelector?: (element: T) => number): number {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || (x => Number(x));
            return this.length === 0 ? 0 :
                this.reduce((total, element, index) => total + projection(element), 0);
        };
    }
    if (!Array.prototype.hasOwnProperty('average')) {
        Array.prototype.average = function <T>(valueSelector?: (element: T) => number): number {
            return this.sum(valueSelector || (x => Number(x))) / this.length;
        };
    }
    if (!String.prototype.hasOwnProperty('formatAsPercent')) {
        String.prototype.formatAsPercent = function (num: number) {
            return num.toString() + '%';
        };
    }
})();