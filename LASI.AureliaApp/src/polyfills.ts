(function () {
  'use strict';
  const identity = <T>(x: T) => x;
  const allAre = <T>(args: any[]): args is T[] => args.every(Array.isArray);

  let arrayAgumentations = {
    flatMap: function (this: any[][], arraySelector: (element) => any[], elementSelector) {
      if (!arraySelector && !allAre(this)) {
        throw TypeError('No arraySelector sepecified and at least one element was not an Array');
      }
      arraySelector = arraySelector || identity;
      elementSelector = elementSelector || identity;

      return this.map(arraySelector || identity)
        .reduce((flattened, array) => flattened.concat(array), [])
        .map(elementSelector || identity);
    },

    correlate: function (this: any[], inner: any[], outerKeySelector: (x) => any, innerKeySelector: (x) => any, resultSelector?: (x, y) => any) {
      const outerKeyed = this.map((element) => ({ element, key: outerKeySelector(element) }));
      const innerKeyed = inner.map(element => ({ element, key: innerKeySelector(element) }));
      const results = outerKeyed.flatMap(({element: first, key}) => {
        return innerKeyed.filter(({key: iKey}) => iKey === key).map(({element: second}) => ({ first, second }));
      });

      // If a function was supplied, map it over the array of result pairs and return the result.
      // otherwise, just return the array of result pairs.
      return resultSelector ? results.map(resultSelector) : results;
    },

    sum: function (valueSelector = Number) {
      return this.length === 0
        ? 0
        : this.reduce((total, element) => total + valueSelector(element), 0);
    },

    average: function (this: number[], valueSelector: (n) => number = Number) {
      return this.sum(valueSelector) / this.length;
    },

    groupBy: function (this: any[], keySelector: (x) => string | number, valueSelector: (x) => any = identity) {
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

    toMap: function (this: any[], keySelector, valueSelector: (x) => any = identity) {
      return this.reduce((map, element) => map.set(keySelector(element), valueSelector(element)), new Map<any, any>());
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
})();