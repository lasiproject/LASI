interface Array<T> {
    /**
    * Takes an array of items, projects each one to an array,using the provided collectionSelector function,
    * and flattens the resulting array of arrays onto a single flat array.
    * A second, optional function performs a projection on each element of the resulting flat array.
    * This function is analagous to SelectMany in Linq, and flatMap in Scala.
    * @param collectionSelector A function to transform each element in the array into an array.
    * @param elementSelector A function to perform an additional transformation on each element in the flattened collection.
    */
    flatMap<TIntermediate, TResult>(collectionSelector: (element: T) => TIntermediate[], elementSelector?: (element: TIntermediate) => TResult): TResult[];
    /**
    * Correlates the elements from two sequences based on the strict ( === ) equality of the keys selected from each element
    * by the provided selector functions. Models the semantics of a relational inner join.
    * Returns an array of tuple like elements containing all pairs of elements with equal keys.
    * @param inner An array whose elements will be correlated with the source array.
    * @param outerKeySelector A function to produce a key value from each element in the outer array.
    * @param innerKeySelector A function to produce a key value from each element in the inner array.
    * @param resultSelector An optional function to transform each pair.
    */
    correlate<TInner, TOuterKey, TInnerKey, TResult>(inner: TInner[], outerKeySelector: (o: T) => TOuterKey, innerKeySelector: (i: TInner) => TInnerKey, resultSelector?: (o: T, i: TInner) => TResult): TResult[];
    /**
    * Computes the sum of all elements of the Array.
    * If the array is empty, the result will be Nan.
    * @param valueSelector A function to produce a numeric value from each element.
    */
    sum(valueSelector?: (element: T) => number): number;
    /**
    * Computes the average of all elements of the Array.
    * If the array is empty, the result will be Nan.
    * @param valueSelector A function to produce a numeric value from each element.
    */
    average(valueSelector?: (element: T) => number): number;
}
interface Map<K, V> {
    /**
    * Returns an array containing all of the key value pairs in the map. The form of the output is { key:myKey;value:myValue}
    */
    toArray(): any;
}
interface String {
    formatAsPercent(num: number): any;
}
