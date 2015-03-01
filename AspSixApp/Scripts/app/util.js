/**
 * Augments Array with the additional methods, if and only if a member with the same name as the method has not already been defined.
 */
(function (prototype) {
    'use strict';
    /** Adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
     * flatMap takes an array of items, projects each one to an array,using the provided
     * collectionSelector function,
     * and flattens the resulting array of arrays onto a single flat array.
     * A second, optional function performs a projection on each element of the resulting flat array.
     * This function is analogous to SelectMany in C#, and flatMap in Scala.
     */
    if (!prototype.hasOwnProperty('flatMap')) {
        prototype.flatMap = function (arraySelector, elementSelector) {
            arraySelector = arraySelector || function (array) { return array; };
            elementSelector = elementSelector || function (element) { return element; };
            return this.reduce(function (array, a) {
                return array.concat(arraySelector(a).map(elementSelector));
            }, []);
        };
    }
    /** Adds the correlate function to  Array.prototype, endowing all
     * Arrays instances with it.
     * correlate takes pairs the two arrays of elements based on
     * the key selector functions, and returns the associated elements as a new array.
     * An optional function performs a projection on each pair.
     * This function is analogous to Join in C#, and inner join in SQL.
     */
    if (!prototype.hasOwnProperty('correlate')) {
        prototype.correlate = function (inner, outerKeySelector, innerKeySelector, resultSelector) {
            var outerKeyed, innerKeyed, i, j, results = [];
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
        };
    }
    if (!prototype.hasOwnProperty('sum')) {
        prototype.sum = function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || function (x) { return Number(x); };
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        };
    }
    if (!prototype.hasOwnProperty('average')) {
        prototype.average = function (valueSelector) {
            return this.sum(valueSelector || function (x) { return Number(x); }) / this.length;
        };
    }
}(Array.prototype));

(function () {
    'use strict';

    var utilities = {};
    utilities.validateAndParseJson = function (data) {
        try {
            return JSON.parse(data);
        } catch (jsonParseException) {
            throw { toString: function () { return "Deserialization failed. Ensure response is valid a JSON structure."; } };
        }
    };
    utilities.post$Json = function (url, obj, success) {
        return $.ajax({
            type: "POST",
            // Note: The call to JSON.stringify is necessary. If a pojo is used directly, jQuery will url encode it.
            data: JSON.stringify(obj),
            url: url,
            contentType: 'application/json',
            success: success
        });
    };
    utilities.perform$Post = function (value) {
        return $.ajax({
            type: "POST",
            // Note: The call to JSON.stringify is necessary. 
            // If a pojo is used directly, jQuery will url encode it.
            data: JSON.stringify(value),
            url: "/api/jobs",
            contentType: "application/json"
        });
    };
    utilities.perform$Get = function (uri) {
        return this.validateAndParseJson($.ajax({
            type: "GET",
            url: uri
        }).responseJSON);
    };
    utilities.perform$Delete = function (id) {
        return $.ajax({
            type: "DELETE",
            url: "/api/jobs/" + id
        });
    };
    utilities.ResourceFactory = {
        fromUri: function (uri) {
            var that = this;
            return {
                value: {},
                location: uri,
                load: function () {
                    that.value = this.perform$Get(uri);
                    return that.value;
                },
                save: function () { return this.perform$Post(that.value, uri); },
                delete: function () { return this.perform$Delete(uri); }
            };
        },
        fromValue: function () {
            throw {
                toString: function () { return "not supported"; }
            };
        }
    };
    return utilities;
}());