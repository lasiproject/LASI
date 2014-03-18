interface Array<T> {
    /**
    * Projects each element of an Array into an array and flattens the result onto a single array.
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
    /**
    * Transforms the Array into a Map of by using the provided key selector and value selector functions.
    * @param keySelector A function to extract a key from each element.
    * @param valueSelector A function to transform each element into. If not specified the identity projection is used.
    */
    toMap<T, K, V>(keySelector: (value: V) => K, valueSelector?: (value: V) => K): Map<K, V>;
}
interface Map<K, V> {
    /** Returns an array containing all of the key value pairs in the map. The form of the output is { key:myKey;value:myValue}
    */
    toArray(): any;
}
interface String {
    fmtAsPct(num: number): any;
}
