import 'flat-map';

interface Array<T> {
  /**
  * @function correlate Correlates the elements of the array with the elements of another
  * based on the strict (===) equality of the keys selected from each element
  * by the provided selector functions. Models the semantics of a relational inner join.
  * Returns an array of elements produced by invoking the specified resultSelector function on each pair of correlated elements.
  * @param inner An array whose elements will be correlated with the source array.
  * @param outerKeySelector A function to produce a key value from each element in the outer array.
  * @param innerKeySelector A function to produce a key value from each element in the inner array.
  * @param resultSelector A function to transform each pair of correlated elements.
  * @returns An array of elements produced by invoking the specified resultSelector function on each pair of correlated elements.
  */
  correlate<TInner, TKey>(inner: TInner[], outerKeySelector: (e: T) => TKey, innerKeySelector: (e: TInner) => TKey): {first: T, second: TInner}[];
  /**
  * @function correlate Correlates the elements of the array with the elements of another
  * based on the strict (===) equality of the keys selected from each element
  * by the provided selector functions. Models the semantics of a relational inner join.
  * Returns an array of elements produced by invoking the specified resultSelector function on each pair of correlated elements.
  * @param inner An array whose elements will be correlated with the source array.
  * @param outerKeySelector A function to produce a key value from each element in the outer array.
  * @param innerKeySelector A function to produce a key value from each element in the inner array.
  * @param resultSelector A function to transform each pair of correlated elements.
  * @returns An array of elements produced by invoking the specified resultSelector function on each pair of correlated elements.
  */
  correlate<TInner, TKey, TResult>(
    inner: TInner[],
    outerKeySelector: (e: T) => TKey,
    innerKeySelector: (e: TInner) => TKey,
    resultSelector: (first: T, second: TInner) => TResult
  ): TResult[];

  average(this: number[]): number;
  average(this: T[], valueSelector: (any: T) => number): number;

  first<U extends T>(predicate?: (e: T) => e is U): U;
  first(predicate?: (e: T) => boolean): T;
  last<U extends T>(predicate?: (e: T) => e is U): U;
  last(predicate?: (e: T) => boolean): T;

  toMap<TKey, TValue>(this: {}[], keySelector: (e: T) => TKey, valueSelector: (e: T) => TValue): Map<TKey, TValue>;
  toMap<TKey, TValue>(this: {}[], keySelector: (e: T) => TKey): Map<TKey, T>;
}

(function () {

  const identity = <T>(x: T) => x;
  const allAre = <T>(args: {}[]): args is T[] => args.every(Array.isArray);

  const arrayAgumentations = {
    correlate<T, U, TKey, R>(
      this: T[],
      inner: U[],
      outerKeySelector: (x: T) => TKey,
      innerKeySelector: (x: U) => TKey,
      resultSelector?: (x: {first: T, second: U}) => R): R[] | {first: T, second: U}[] {
      const outerKeyed = this.map((element) => ({element, key: outerKeySelector(element)}));
      const innerKeyed = inner.map(element => ({element, key: innerKeySelector(element)}));
      const results = outerKeyed.flatMap(({element: first, key}) => {
        return innerKeyed.filter(({key: iKey}) => iKey === key).map(({element: second}) => ({first, second}));
      });

      // If a function was supplied, map it over the array of result pairs and return the result.
      // otherwise, just return the array of result pairs.
      return resultSelector ? results.map(resultSelector) : results;
    },

    average<T>(this: T[], valueSelector: (n: T) => number = Number): number {
      return this.reduce((total, value) => total + valueSelector(value), 0) / this.length;
    },

    groupBy(this: {}[], keySelector: (x: {}) => string | number, valueSelector: (x: {}) => {} = identity) {
      return this.reduce((groups, element) => {
        const key = keySelector(element);
        const value = valueSelector(element);
        return {...groups, [key]: value};
      }, {});
    },

    toMap(this: {}[], keySelector: (x: {}) => {}, valueSelector = identity) {
      return this.reduce<Map<{}, {}>>((map, element) => {
        map.set(keySelector(element), valueSelector(element));
        return map;
      }, new Map());
    }
  };
  const {prototype} = Array;
  Object.entries(arrayAgumentations)
    .filter(([key]) => !Object.prototype.hasOwnProperty.call(prototype, key))
    .forEach(([key, value]) => {
      Object.defineProperty(prototype, key, {
        writable: false,
        enumerable: false,
        configurable: false,
        value
      });
    });
}());
