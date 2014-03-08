interface Array<T> {
    /**
    * Projects each element of an Array into an array and flattens the result onto a single array.
    * @param collectionSelector A function to transform each element in the array into an array.
    * @param elementProjection A function to perform an additional transformation on each element in the flattened collection.
    */
    flatMap<TIntermediate, TResult>(collectionSelector: (element: T) => TIntermediate[], elementProjection?: (element: TIntermediate) => TResult): TResult[];
    /**
    * Correlates the elements from two sequences based on the strict (===) equality of the keys selected from each element
    * by the provided selector functions. Models the semantics of a relational inner join.
    * Returns an array of tuple like elements containing all pairs of elements with equal keys.
    * @param inner An array whose elements will be correlated with the source array.
    * @param outerKeySelector A function to produce a key value from each element in the outer array.
    * @param innerKeySelector A function to produce a key value from each element in the inner array.
    * @param resultSelector An optional function to transform each pair.
    */
    correlate<TInner, TOuterKey, TInnerKey, TResult>(inner: TInner[], outerKeySelector: (o: T) => TOuterKey, innerKeySelector: (i: TInner) => TInnerKey, resultSelector: (o: T, i: TInner) => TResult): TResult[];
}
