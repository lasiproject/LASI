/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */

(function () {
    'use strict';
    var arrayAgumentations = {
        flatMap: function (arraySelector, elementSelector) {
            arraySelector = arraySelector || function (array) {
                return array instanceof Array && array;
            };
            elementSelector = elementSelector || function (element) { return element; };
            return this.reduce(function (array, a) {
                return array.concat(arraySelector(a).map(elementSelector));
            }, []);
        },
        correlate: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            var outerKeyed, innerKeyed, i, j, results: { first: any, second: any }[] = [];
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
        },
        sum: function (valueSelector: (any) => number) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || (x => Number(x));
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        },
        average: function (valueSelector) {
            return this.sum(valueSelector || (x => Number(x))) / this.length;
        },
        first: function (predicate?: (element: any) => boolean) {
            if (!predicate) {
                return this[0];
            }
            for (let i = 0; i < this.length; ++i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        },
        last: function (predicate?: (element: any) => boolean) {
            if (!predicate) {
                return this[this.length - 1];
            }
            for (let i = this.length - 1; i > 0; --i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        }
    };
    Object.keys(arrayAgumentations)
        .filter(key => !Object.prototype.hasOwnProperty.call(Array.prototype, key))
        .map((key): ([string, PropertyDescriptor]) => [key,
        {
            writable: false,
            enumerable: false,
            configurable: false,
            value: arrayAgumentations[key]
        }])
        .forEach(([name, property]) => Object.defineProperty(Array.prototype, name, property));
})();
interface Array<T> {
    flatMap<TArray, TResult>(arraySelector: (element: T) => TArray[], elementSelector: (element: TArray) => TResult): TResult[];
    flatMap<TArray>(arraySelector: (element: T) => TArray[]): TArray[];
    //flatMap<U>(arraySelector: (element: T) => U[], elementSelector?: (element: T) => U): U[];
    //flatMap<U extends T[]>(): T[];
    flatMap(): any[];
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
    sum(valueSelector?: (any) => number): number;
    average(valueSelector?: (any) => number): number;

    first(predicate?: (element: T) => boolean): T;
    last(predicate?: (element: T) => boolean): T;

} 