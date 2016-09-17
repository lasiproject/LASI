(function () {
  'use strict';
  let arrayAgumentations = {
    flatMap: function (arraySelector, elementSelector) {
      arraySelector = arraySelector || function (array) {
        if (!Array.isArray(array)) {
          throw new TypeError('expected an array');
        }
        return array;
      };
      elementSelector = elementSelector || function (element) { return element; };
      return this.reduce(function (array, a) {
        return array.concat(arraySelector(a).map(elementSelector));
      }, []);
    },
    correlate: function (this: any[], inner: any[], outerKeySelector: (x) => any, innerKeySelector: (x) => any, resultSelector?: (x, y) => any) {
      let i,
        j,
        outerKeyed = this.map((element) => ({ element, key: outerKeySelector(element) })),
        innerKeyed = inner.map(element => ({ element, key: innerKeySelector(element) }));
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
    }
  };
  const {prototype } = Array;
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