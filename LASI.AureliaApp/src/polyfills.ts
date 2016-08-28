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
        correlate: function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            let i,
                j,
                results: any[] = [],
                outerKeyed = this.map((element) => ({ element, key: outerKeySelector(element) })),
                innerKeyed = inner.map(element => ({ element, key: innerKeySelector(element) }));

            for (i = 0; i < outerKeyed.length; i += 1) {
                for (j = 0; j < innerKeyed.length; j += 1) {
                    if (outerKeyed[i].key === innerKeyed[j].key) {
                        results.push({ first: outerKeyed[i].element, second: innerKeyed[j].element });
                    }
                }
            }
            // If a function was supplied, map it over the array of result pairs and return the result.
            // otherwise, just return the array of result pairs.
            return resultSelector
                ? results.map(e => resultSelector(e.first, e.second))
                : results;
        },
        sum: function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            const projection = valueSelector || Number;
            return this.length === 0
                ? 0
                : this.reduce((total, element) => total + projection(element), 0);
        },
        average: function (valueSelector) {
            return this.sum(valueSelector || Number) / this.length;
        },
        first: function (predicate) {
            if (!predicate) {
                return this[0];
            }
            for (let i = 0; i < this.length; i += 1) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        },
        last: function (predicate) {
            if (!predicate) {
                return this[this.length - 1];
            }
            for (let i = this.length - 1; i > 0; i -= 1) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        }
    };
    Object.keys(arrayAgumentations)
        .filter(key => !Object.prototype.hasOwnProperty.call(Array.prototype, key))
        .forEach(key => {
            Object.defineProperty(Array.prototype, key, {
                writable: false,
                enumerable: false,
                configurable: false,
                value: arrayAgumentations[key]
            });
        });
})();