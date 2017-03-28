export { }
(function () {
  'use strict';
  const identity = <T>(x: T) => x;
  const allAre = <T>(args: any[]): args is T[] => args.every(Array.isArray);

  let arrayAgumentations = {
    flatMap(this: any[][], arraySelector: (element: any) => any[], elementSelector: any) {
      if (!arraySelector && !allAre(this)) {
        throw TypeError('No arraySelector sepecified and at least one element was not an Array');
      }
      arraySelector = arraySelector || identity;
      elementSelector = elementSelector || identity;

      return this.map(arraySelector || identity)
        .reduce((flattened, array) => flattened.concat(array), [])
        .map(elementSelector || identity);
    },

    correlate(this: any[], inner: any[], outerKeySelector: (x: {}) => any, innerKeySelector: (x: {}) => {}, resultSelector?: (x: {}, y: {}) => {}) {
      const outerKeyed = this.map((element) => ({ element, key: outerKeySelector(element) }));
      const innerKeyed = inner.map(element => ({ element, key: innerKeySelector(element) }));
      const results = outerKeyed.flatMap(({ element: first, key }) => {
        return innerKeyed.filter(({ key: iKey }) => iKey === key).map(({ element: second }) => ({ first, second }));
      });

      // If a function was supplied, map it over the array of result pairs and return the result.
      // otherwise, just return the array of result pairs.
      return resultSelector ? results.map(resultSelector) : results;
    },

    sum(this: any[], valueSelector = Number) {
      return this.length === 0
        ? 0
        : this.reduce((total, element) => total + valueSelector(element), 0);
    },

    average(this: number[], valueSelector: (n: {}) => number = Number) {
      return this.sum(valueSelector) / this.length;
    },

    groupBy(this: any[], keySelector: (x: {}) => string | number, valueSelector: (x: {}) => any = identity) {
      return this.reduce((groups, element) => {
        const key = keySelector(element);
        const value = valueSelector(element);
        if (!groups[key]) {
          groups[key] = [value];
        } else {
          groups[key].push(value);
        }
        return groups;
      }, {});
    },

    toMap(this: any[], keySelector: (x: any) => any, valueSelector = identity) {
      return this.reduce((map, element) => map.set(keySelector(element), valueSelector(element)), new Map<{}, {}>());
    }
  };
  const { prototype } = Array;
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
})();

declare global {
  interface Array<T> {
    flatMap<TArray, TResult>(arraySelector: (element: T) => TArray[], elementSelector: (element: TArray) => TResult): TResult[];
    flatMap<TArray>(arraySelector: (element: T) => TArray[]): TArray[];
    //flatMap<U>(arraySelector: (element: T) => U[], elementSelector?: (element: T) => U): U[];
    //flatMap<U extends T[]>(): T[];

    flatMap<TArray extends T[]>(): T[];
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
    correlate<TInner, TKey>(inner: TInner[], outerKeySelector: (e: T) => TKey, innerKeySelector: (e: TInner) => TKey): { first: T, second: TInner }[];
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

    sum(valueSelector?: (e: T) => number): number;
    average(valueSelector?: (any: T) => number): number;

    first<U extends T>(predicate?: (e: T) => e is U): U;
    first(predicate?: (e: T) => boolean): T;
    last<U extends T>(predicate?: (e: T) => e is U): U;
    last(predicate?: (e: T) => boolean): T;

    toMap<TKey, TValue>(this: any[], keySelector: (e: T) => TKey, valueSelector: (e: T) => TValue): Map<TKey, TValue>;
    toMap<TKey, TValue>(this: any[], keySelector: (e: T) => TKey): Map<TKey, T>;
  }

}
declare const ROOT: string;
import path from 'path';
export function root(...pathSegments: string[]) {
  return path.join(ROOT, ...pathSegments);
}