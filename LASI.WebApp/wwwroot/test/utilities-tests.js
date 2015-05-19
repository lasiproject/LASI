(function () {
    'use strict';

    var test = QUnit.test,
        deepEqual = QUnit.deepEqual,
        notStrictEqual = QUnit.notStrictEqual,
        ok = QUnit.ok,
        dataGenerator = (function (rand) {
            return {
                randomNumberRange: function (length) {
                    var i, range = [];
                    if (length < 0) {
                        throw 'length cannot be negative';
                    }
                    for (i = 0; i < length; i += 1) {
                        range.push(rand() * 100);
                    }
                    return range;
                }
            };
        }(Math.random));

    test('flatMap defined test', function () {
        ok(Array.prototype.hasOwnProperty('flatMap'), 'flatMap defined on Array.prototype!');
    });
    test('flatMap test', function () {
        var actual = [[1, 2, 3], [4, 5, 6], [7, 8]].flatMap(),
            expected = [1, 2, 3, 4, 5, 6, 7, 8];
        deepEqual(actual, expected, ('expected: ' + expected + ';  actual: ' + actual));
    });
    test('flatMap test1', function () {
        var actual = [[1, 2, 3], [4, 5, 6], [7, 8]].flatMap(undefined, function (x) { return x * x; }),
            expected = [1, 4, 9, 16, 25, 36, 49, 64];
        deepEqual(actual, expected, ('expected: ' + expected + '  actual: ' + actual));
    });
    test('flatMap test2', function () {
        var actual = [[1, 2, 3], [4, 5, 6], [7, 8]].flatMap(function (e) {
            var i,
                result = [];
            for (i = 0; i < e.length; i += 1) {
                result.push(e[i]);
            }
            return result;
        }, function (x) { return x * x; }),
            expected = [1, 4, 9, 16, 25, 36, 49, 64];
        deepEqual(actual, expected, ('expected: ' + expected + ';  actual: ' + actual));
    });
    test('flatMap test3', function () {
        $('body').append('<ul style="list-style:none;" hidden id="flatMapTest3"><li>18</li><li>67</li></ul>');
        var actual = [document.getElementById("flatMapTest3").childNodes, document.getElementById("flatMapTest3").childNodes].flatMap(function (e) {
            var i,
                result = [];
            for (i = 0; i < e.length; i += 1) {
                result.push(Number($(e[i]).text()));
            }
            return result;
        }, function (x) { return x * x; }),
            expected = [18 * 18, 67 * 67, 18 * 18, 67 * 67];
        deepEqual(actual, expected, ("expected: " + expected + ";  actual: " + actual));
    });
    test('correlate defined test', function () {
        ok(Array.prototype.hasOwnProperty('correlate'), 'correlate defined on Array.prototype!');
    });
    test('correlate test 1', function () {
        var outer = [9, 2, 3, 4],
            inner = [16, 95, 71, 81],
            actual = outer.correlate(inner, function (e) { return e % 2; }, function (e) { return e % 2; }),
            expected = [
                { first: 9, second: 95 },
                { first: 9, second: 71 },
                { first: 9, second: 81 },
                { first: 2, second: 16 },
                { first: 3, second: 95 },
                { first: 3, second: 71 },
                { first: 3, second: 81 },
                { first: 4, second: 16 }
            ];
        deepEqual(actual.map(function (e) { return e.toString(); }), expected.map(function (e) { return e.toString(); }), 'expected: ' + expected + '  actual: ' + actual);
    });
    test('correlate test 2', function () {
        var outer = [9, 2, 3, 4],
            inner = [16, 95, 71, 81],
            actual = outer.correlate(inner, function (e) { return e % 2; }, function (e) { return e % 2; }),
            expected = [
                { first: 9, second: 95 },
                { first: 9, second: 71 },
                { first: 9, second: 81 },
                { first: 2, second: 16 },
                { first: 3, second: 95 },
                { first: 3, second: 71 },
                { first: 3, second: 81 },
                { first: 4, second: 16 }
            ];
        notStrictEqual(actual.map(function (e) { return JSON.stringify(e); }), expected.map(function (e) { return JSON.stringify(e); }));
    });
    test('correlate test 3', function () {
        var outer = [9, 2, 3, 4],
            inner = [16, 95, 71, 81],
            actual = outer.correlate(inner, function (e) { return e % 2; }, function (e) { return e % 2; }, function (o, i) { return o * i; }),
            expected = [9 * 95, 9 * 71, 9 * 81, 2 * 16, 3 * 95, 3 * 71, 3 * 81, 4 * 16];
        notStrictEqual(actual, expected, 'expected: ' + expected + ';  actual: ' + actual);
    });
    test('correlate test 4', function () {
        var outer = [9, 2, 3, 4],
            inner = [16, 95, 71, 81],
            actual = outer.correlate(inner, function (e) { return e % 2; }, function (e) { return e % 2; }, function (o, i) { return o.toString() + i.toString(); }),
            expected = ['995', '971', '981', '216', '395', '371', '381', '416'];
        deepEqual(actual, expected, 'expected: ' + expected + ';  actual: ' + actual);
    });

    test('average defined test', function () {
        ok(Array.prototype.hasOwnProperty('average'), 'average defined on Array.prototype!');
    });
    test('average test1', function () {
        var numbers = dataGenerator.randomNumberRange(10),
            actual = numbers.average(function (x) { return x; }),
            expected = numbers.reduce(function (sum, x) { return sum + x; }, 0) / numbers.length;
        deepEqual(actual, expected, ('expected: ' + expected + ';  actual: ' + actual));
    });
}());
