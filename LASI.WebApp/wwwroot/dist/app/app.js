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
        },
        sum: function (valueSelector) {
            // If the a valueSelector was not provided, define a function which will attempt 
            // to convert its argument to a number.
            var projection = valueSelector || (function (x) { return Number(x); });
            return this.length === 0 ? 0 : this.reduce(function (total, element) { return total + projection(element); }, 0);
        },
        average: function (valueSelector) {
            return this.sum(valueSelector || (function (x) { return Number(x); })) / this.length;
        },
        first: function (predicate) {
            if (!predicate) {
                return this[0];
            }
            for (var i = 0; i < this.length; ++i) {
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
            for (var i = this.length - 1; i > 0; --i) {
                if (predicate(this[i])) {
                    return this[i];
                }
            }
            return undefined;
        }
    };
    Object.keys(arrayAgumentations)
        .filter(function (key) { return !Object.prototype.hasOwnProperty.call(Array.prototype, key); })
        .map(function (key) { return [key,
        {
            writable: false,
            enumerable: false,
            configurable: false,
            value: arrayAgumentations[key]
        }]; })
        .forEach(function (_a) {
        var name = _a[0], property = _a[1];
        return Object.defineProperty(Array.prototype, name, property);
    });
})();

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC91dGlsaXRpZXMvYXVnbWVudGF0aW9ucy50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTs7R0FFRztBQUVILENBQUM7SUFDRyxZQUFZLENBQUM7SUFDYixJQUFJLGtCQUFrQixHQUFHO1FBQ3JCLE9BQU8sRUFBRSxVQUFVLGFBQWEsRUFBRSxlQUFlO1lBQzdDLGFBQWEsR0FBRyxhQUFhLElBQUksVUFBVSxLQUFLO2dCQUM1QyxNQUFNLENBQUMsS0FBSyxZQUFZLEtBQUssSUFBSSxLQUFLLENBQUM7WUFDM0MsQ0FBQyxDQUFDO1lBQ0YsZUFBZSxHQUFHLGVBQWUsSUFBSSxVQUFVLE9BQU8sSUFBSSxNQUFNLENBQUMsT0FBTyxDQUFDLENBQUMsQ0FBQyxDQUFDO1lBQzVFLE1BQU0sQ0FBQyxJQUFJLENBQUMsTUFBTSxDQUFDLFVBQVUsS0FBSyxFQUFFLENBQUM7Z0JBQ2pDLE1BQU0sQ0FBQyxLQUFLLENBQUMsTUFBTSxDQUFDLGFBQWEsQ0FBQyxDQUFDLENBQUMsQ0FBQyxHQUFHLENBQUMsZUFBZSxDQUFDLENBQUMsQ0FBQztZQUMvRCxDQUFDLEVBQUUsRUFBRSxDQUFDLENBQUM7UUFDWCxDQUFDO1FBQ0QsU0FBUyxFQUFFLFVBQVUsS0FBSyxFQUFFLGdCQUFnQixFQUFFLGdCQUFnQixFQUFFLGNBQWM7WUFDMUUsSUFBSSxVQUFVLEVBQUUsVUFBVSxFQUFFLENBQUMsRUFBRSxDQUFDLEVBQUUsT0FBTyxHQUFrQyxFQUFFLENBQUM7WUFDOUUsVUFBVSxHQUFHLElBQUksQ0FBQyxHQUFHLENBQUMsVUFBVSxDQUFDO2dCQUM3QixNQUFNLENBQUMsRUFBRSxPQUFPLEVBQUUsQ0FBQyxFQUFFLEdBQUcsRUFBRSxnQkFBZ0IsQ0FBQyxDQUFDLENBQUMsRUFBRSxDQUFDO1lBQ3BELENBQUMsQ0FBQyxDQUFDO1lBQ0gsVUFBVSxHQUFHLEtBQUssQ0FBQyxHQUFHLENBQUMsVUFBVSxDQUFDO2dCQUM5QixNQUFNLENBQUMsRUFBRSxPQUFPLEVBQUUsQ0FB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LENBQUMsR0FBRyxJQUFJLENBQUMsTUFBTSxHQUFHLENBQUMsRUFBRSxDQUFDLEdBQUcsQ0FBQyxFQUFFLEVBQUUsQ0FBQyxFQUFFLENBQUM7Z0JBQ3ZDLEVBQUUsQ0FBQyxDQUFDLFNBQVMsQ0FBQyxJQUFJLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUM7b0JBQ3JCLE1BQU0sQ0FBQyxJQUFJLENBQUMsQ0FBQyxDQUFDLENBQUM7Z0JBQ25CLENBQUM7WUFDTCxDQUFDO1lBQ0QsTUFBTSxDQUFDLFNBQVMsQ0FBQztRQUNyQixDQUFDO0tBQ0osQ0FBQztJQUNGLE1BQU0sQ0FBQyxJQUFJLENBQUMsa0JBQWtCLENBQUM7U0FDMUIsTUFBTSxDQUFDLFVBQUEsR0FBRyxJQUFJLE9BQUEsQ0FBQyxNQUFNLENBQUMsU0FBUyxDQUFDLGNBQWMsQ0FBQyxJQUFJLENBQUMsS0FBSyxDQUFDLFNBQVMsRUFBRSxHQUFHLENBQUMsRUFBM0QsQ0FBMkQsQ0FBQztTQUMxRSxHQUFHLENBQUMsVUFBQyxHQUFHLElBQXFDLE9BQUEsQ0FBQyxHQUFHO1FBQ2xEO1lBQ0ksUUFBUSxFQUFFLEtBQUs7WUFDZixVQUFVLEVBQUUsS0FBSztZQUNqQixZQUFZLEVBQUUsS0FBSztZQUNuQixLQUFLLEVBQUUsa0JBQWtCLENBQUMsR0FBRyxDQUFDO1NBQ2pDLENBQUMsRUFONEMsQ0FNNUMsQ0FBQztTQUNGLE9BQU8sQ0FBQyxVQUFDLEVBQWdCO1lBQWYsSUFBSSxVQUFFLFFBQVE7ZUFBTSxNQUFNLENBQUMsY0FBYyxDQUFDLEtBQUssQ0FBQyxTQUFTLEVBQUUsSUFBSSxFQUFFLFFBQVEsQ0FBQztJQUF0RCxDQUFzRCxDQUFDLENBQUM7QUFDL0YsQ0FBQyxDQUFDLEVBQUUsQ0FBQyIsImZpbGUiOiJhdWdtZW50YXRpb25zLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

/// <reference path="../../typings/tsd.d.ts" />
'use strict';
define(["require", "exports", 'jquery'], function (require, exports, $) {
    //export var setupDraggableDialogs: () => void; 
    exports.log = console.log.bind(console);
    exports.editor = $('#free-editor').change(exports.log); // TODO: parameterize selector.
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9MQVNJLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLCtDQUErQztBQUMvQyxZQUFZLENBQUM7O0lBSWIsZ0RBQWdEO0lBQ3JDLFdBQUcsR0FBc0QsT0FBTyxDQUFDLEdBQUcsQ0FBQyxJQUFJLENBQUMsT0FBTyxDQUFDLENBQUM7SUFDbkYsY0FBTSxHQUFHLENBQUMsQ0FBQyxjQUFjLENBQUMsQ0FBQyxNQUFNLENBQUMsV0FBRyxDQUFDLENBQUMsQ0FBQywrQkFBK0IiLCJmaWxlIjoiTEFTSS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    function tasksListServiceProvider() {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';
        $get.$inject = ['$q', '$resource', '$interval'];
        return { $get: $get, setUpdateInterval: setUpdateInterval, setTasksListUrl: setTasksListUrl };
        function setUpdateInterval(milliseconds) {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url) {
            tasksListUrl = url;
            return this;
        }
        function $get($q, $resource, $interval) {
            var Tasks = $resource(tasksListUrl, {}, {
                get: {
                    method: 'GET', isArray: true
                }
            });
            var getActiveTasks = function () {
                var _this = this;
                var deferred = $q.defer();
                $interval(function () {
                    _this.tasks = Tasks.get();
                    deferred.resolve(_this.tasks);
                }, updateInterval);
                return deferred.promise;
            };
            return {
                getActiveTasks: getActiveTasks,
                tasks: []
            };
        }
        function createDebugInfoUpdator(element) {
            return function (tasks) { return element.html(tasks.map(function (task) { return ("<div>" + Object.keys(task).map(function (key) { return ("<span>&nbsp&nbsp" + task[key] + "</span>"); }) + "</div>"); }).join()); };
        }
    }
    exports.tasksListServiceProvider = tasksListServiceProvider;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L3Rhc2tzLWxpc3Qtc2VydmljZS5wcm92aWRlci50cyJdLCJuYW1lcyI6WyJ0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIiLCJ0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIuc2V0VXBkYXRlSW50ZXJ2YWwiLCJ0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIuc2V0VGFza3NMaXN0VXJsIiwidGFza3NMaXN0U2VydmljZVByb3ZpZGVyLiRnZXQiLCJ0YXNrc0xpc3RTZXJ2aWNlUHJvdmlkZXIuY3JlYXRlRGVidWdJbmZvVXBkYXRvciJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQXNCYjtRQUNJQSxJQUFJQSxjQUFjQSxHQUFHQSxHQUFHQSxDQUFDQTtRQUN6QkEsSUFBSUEsWUFBWUEsR0FBR0EsV0FBV0EsQ0FBQ0E7UUFFL0JBLElBQUlBLENBQUNBLE9BQU9BLEdBQUdBLENBQUNBLElBQUlBLEVBQUVBLFdBQVdBLEVBQUVBLFdBQVdBLENBQUNBLENBQUNBO1FBRWhEQSxNQUFNQSxDQUFDQSxFQUFFQSxNQUFBQSxJQUFJQSxFQUFFQSxtQkFBQUEsaUJBQWlCQSxFQUFFQSxpQkFBQUEsZUFBZUEsRUFBRUEsQ0FBQ0E7UUFFcERBLDJCQUEyQkEsWUFBb0JBO1lBQzNDQyxjQUFjQSxHQUFHQSxZQUFZQSxDQUFDQTtZQUM5QkEsTUFBTUEsQ0FBQ0EsSUFBSUEsQ0FBQ0E7UUFDaEJBLENBQUNBO1FBQ0RELHlCQUF5QkEsR0FBV0E7WUFDaENFLFlBQVlBLEdBQUdBLEdBQUdBLENBQUNBO1lBQ25CQSxNQUFNQSxDQUFDQSxJQUFJQSxDQUFDQTtRQUNoQkEsQ0FBQ0E7UUFFREYsY0FBY0EsRUFBYUEsRUFBRUEsU0FBMkJBLEVBQUVBLFNBQTJCQTtZQUNqRkcsSUFBSUEsS0FBS0EsR0FBR0EsU0FBU0EsQ0FBU0EsWUFBWUEsRUFBRUEsRUFBRUEsRUFBRUE7Z0JBQzVDQSxHQUFHQSxFQUFFQTtvQkFDREEsTUFBTUEsRUFBRUEsS0FBS0EsRUFBRUEsT0FBT0EsRUFBRUEsSUFBSUE7aUJBQy9CQTthQUNKQSxDQUFDQSxDQUFDQTtZQUNIQSxJQUFJQSxjQUFjQSxHQUFHQTtnQkFBQSxpQkFPcEI7Z0JBTkcsSUFBSSxRQUFRLEdBQUcsRUFBRSxDQUFDLEtBQUssRUFBVSxDQUFDO2dCQUNsQyxTQUFTLENBQUM7b0JBQ04sS0FBSSxDQUFDLEtBQUssR0FBRyxLQUFLLENBQUMsR0FBRyxFQUFFLENBQUM7b0JBQ3pCLFFBQVEsQ0FBQyxPQUFPLENBQUMsS0FBSSxDQUFDLEtBQUssQ0FBQyxDQUFDO2dCQUNqQyxDQUFDLEVBQUUsY0FBYyxDQUFDLENBQUM7Z0JBQ25CLE1BQU0sQ0FBQyxRQUFRLENBQUMsT0FBTyxDQUFDO1lBQzVCLENBQUMsQ0FBQ0E7WUFFRkEsTUFBTUEsQ0FBQ0E7Z0JBQ0hBLGdCQUFBQSxjQUFjQTtnQkFDZEEsS0FBS0EsRUFBRUEsRUFBRUE7YUFDWkEsQ0FBQ0E7UUFDTkEsQ0FBQ0E7UUFDREgsZ0NBQWdDQSxPQUFlQTtZQUMzQ0ksTUFBTUEsQ0FBQ0EsVUFBQUEsS0FBS0EsSUFBSUEsT0FBQUEsT0FBT0EsQ0FBQ0EsSUFBSUEsQ0FBQ0EsS0FBS0EsQ0FBQ0EsR0FBR0EsQ0FDbENBLFVBQUFBLElBQUlBLElBQUlBLE9BQUFBLFdBQVFBLE1BQU1BLENBQUNBLElBQUlBLENBQUNBLElBQUlBLENBQUNBLENBQUNBLEdBQUdBLENBQUNBLFVBQUFBLEdBQUdBLElBQUlBLE9BQUFBLHNCQUFtQkEsSUFBSUEsQ0FBQ0EsR0FBR0EsQ0FBQ0EsYUFBU0EsRUFBckNBLENBQXFDQSxDQUFDQSxZQUFTQSxFQUFwRkEsQ0FBb0ZBLENBQy9GQSxDQUFDQSxJQUFJQSxFQUFFQSxDQUFDQSxFQUZPQSxDQUVQQSxDQUFDQTtRQUNkQSxDQUFDQTtJQUNMSixDQUFDQTtJQTFDZSxnQ0FBd0IsMkJBMEN2QyxDQUFBIiwiZmlsZSI6ImRvY3VtZW50LWxpc3QvdGFza3MtbGlzdC1zZXJ2aWNlLnByb3ZpZGVyLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports"], function (require, exports) {
    var DebugPanelController = (function () {
        function DebugPanelController(tasksListService) {
            var _this = this;
            this.tasks = [];
            this.infoVisible = false;
            tasksListService.getActiveTasks().then(function (data) { return _this.tasks = data.sort(function (x, y) { return x.id.localeCompare(y.id); }); });
        }
        Object.defineProperty(DebugPanelController.prototype, "buttonText", {
            get: function () { return this.infoVisible ? 'Hide' : 'Show'; },
            enumerable: true,
            configurable: true
        });
        DebugPanelController.prototype.toggle = function () { this.infoVisible = !this.infoVisible; };
        DebugPanelController.$inject = ['tasksListService'];
        return DebugPanelController;
    })();
    function debugPanel() {
        return {
            template: require('app/debug/debug-panel.directive.html!'),
            controller: DebugPanelController,
            bindToController: {},
            controllerAs: 'debug'
        };
    }
    exports.debugPanel = debugPanel;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kZWJ1Zy9kZWJ1Zy1wYW5lbC5kaXJlY3RpdmUudHMiXSwibmFtZXMiOlsiRGVidWdQYW5lbENvbnRyb2xsZXIiLCJEZWJ1Z1BhbmVsQ29udHJvbGxlci5jb25zdHJ1Y3RvciIsIkRlYnVnUGFuZWxDb250cm9sbGVyLmJ1dHRvblRleHQiLCJEZWJ1Z1BhbmVsQ29udHJvbGxlci50b2dnbGUiLCJkZWJ1Z1BhbmVsIl0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBSWI7UUFFSUEsOEJBQVlBLGdCQUFrQ0E7WUFGbERDLGlCQVlDQTtZQUZHQSxVQUFLQSxHQUFXQSxFQUFFQSxDQUFDQTtZQUNuQkEsZ0JBQVdBLEdBQUdBLEtBQUtBLENBQUNBO1lBUmhCQSxnQkFBZ0JBLENBQUNBLGNBQWNBLEVBQUVBLENBQUNBLElBQUlBLENBQUNBLFVBQUFBLElBQUlBLElBQUlBLE9BQUFBLEtBQUlBLENBQUNBLEtBQUtBLEdBQUdBLElBQUlBLENBQUNBLElBQUlBLENBQUNBLFVBQUNBLENBQUNBLEVBQUVBLENBQUNBLElBQUtBLE9BQUFBLENBQUNBLENBQUNBLEVBQUVBLENBQUNBLGFBQWFBLENBQUNBLENBQUNBLENBQUNBLEVBQUVBLENBQUNBLEVBQXhCQSxDQUF3QkEsQ0FBQ0EsRUFBMURBLENBQTBEQSxDQUFDQSxDQUFDQTtRQUMvR0EsQ0FBQ0E7UUFFREQsc0JBQUlBLDRDQUFVQTtpQkFBZEEsY0FBbUJFLE1BQU1BLENBQUNBLElBQUlBLENBQUNBLFdBQVdBLEdBQUdBLE1BQU1BLEdBQUdBLE1BQU1BLENBQUNBLENBQUNBLENBQUNBOzs7V0FBQUY7UUFFL0RBLHFDQUFNQSxHQUFOQSxjQUFXRyxJQUFJQSxDQUFDQSxXQUFXQSxHQUFHQSxDQUFDQSxJQUFJQSxDQUFDQSxXQUFXQSxDQUFDQSxDQUFDQSxDQUFDQTtRQVAzQ0gsNEJBQU9BLEdBQUdBLENBQUNBLGtCQUFrQkEsQ0FBQ0EsQ0FBQ0E7UUFXMUNBLDJCQUFDQTtJQUFEQSxDQVpBLEFBWUNBLElBQUE7SUFDRDtRQUNJSSxNQUFNQSxDQUFDQTtZQUNIQSxRQUFRQSxFQUFFQSxPQUFPQSxDQUFDQSx1Q0FBdUNBLENBQUNBO1lBQzFEQSxVQUFVQSxFQUFFQSxvQkFBb0JBO1lBQ2hDQSxnQkFBZ0JBLEVBQUVBLEVBQUVBO1lBQ3BCQSxZQUFZQSxFQUFFQSxPQUFPQTtTQUN4QkEsQ0FBQ0E7SUFDTkEsQ0FBQ0E7SUFQZSxrQkFBVSxhQU96QixDQUFBIiwiZmlsZSI6ImRlYnVnL2RlYnVnLXBhbmVsLmRpcmVjdGl2ZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

define(["require", "exports", './debug-panel.directive'], function (require, exports, debug_panel_directive_1) {
    var m = {
        name: 'debug',
        requires: [],
        directives: { debugPanel: debug_panel_directive_1.debugPanel }
    };
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = m;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kZWJ1Zy9kZWJ1Zy5tb2R1bGUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IjtJQUVBLElBQUksQ0FBQyxHQUF5QjtRQUMxQixJQUFJLEVBQUUsT0FBTztRQUNiLFFBQVEsRUFBRSxFQUFFO1FBQ1osVUFBVSxFQUFFLEVBQUUsWUFBQSxrQ0FBVSxFQUFFO0tBQzdCLENBQUM7SUFDRjtzQkFBZSxDQUFDLENBQUMiLCJmaWxlIjoiZGVidWcvZGVidWcubW9kdWxlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

define(["require", "exports", 'angular', 'angular-resource', 'angular-bootstrap', 'angular-bootstrap-contextmenu', 'angular-file-upload', 'jquery'], function (require, exports, angular, resource, angularBootstrap, angularBootstrapContextmenu, angularFileUpload, $) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        $: $, angular: angular, resource: resource, angularBootstrap: angularBootstrap, angularBootstrapContextmenu: angularBootstrapContextmenu, angularFileUpload: angularFileUpload
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9hbmd1bGFyLXNoaW0udHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IjtJQU9BO3NCQUFlO1FBQ1gsR0FBQSxDQUFDLEVBQUUsU0FBQSxPQUFPLEVBQUUsVUFBQSxRQUFRLEVBQUUsa0JBQUEsZ0JBQWdCLEVBQUUsNkJBQUEsMkJBQTJCLEVBQUUsbUJBQUEsaUJBQWlCO0tBQ3pGLENBQUEiLCJmaWxlIjoiYW5ndWxhci1zaGltLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports"], function (require, exports) {
    uploadPanel.$inject = ['$window'];
    function uploadPanel($window) {
        return {
            restrict: 'E'
        };
    }
    exports.uploadPanel = uploadPanel;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC11cGxvYWQvdXBsb2FkLXBhbmVsLmRpcmVjdGl2ZS50cyJdLCJuYW1lcyI6WyJ1cGxvYWRQYW5lbCJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUViLFdBQVcsQ0FBQyxPQUFPLEdBQUcsQ0FBQyxTQUFTLENBQUMsQ0FBQztJQUVsQyxxQkFBNEIsT0FBTztRQUMvQkEsTUFBTUEsQ0FBQ0E7WUFDSEEsUUFBUUEsRUFBRUEsR0FBR0E7U0FDaEJBLENBQUNBO0lBQ05BLENBQUNBO0lBSmUsbUJBQVcsY0FJMUIsQ0FBQSIsImZpbGUiOiJkb2N1bWVudC11cGxvYWQvdXBsb2FkLXBhbmVsLmRpcmVjdGl2ZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports", 'app/angular-shim'], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentUpload',
        requires: ['ngFileUpload'],
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC11cGxvYWQvZG9jdW1lbnQtdXBsb2FkLm1vZHVsZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBS2I7c0JBQWU7UUFDWCxJQUFJLEVBQUUsZ0JBQWdCO1FBQ3RCLFFBQVEsRUFBRSxDQUFDLGNBQWMsQ0FBQztLQUM3QixDQUFBIiwiZmlsZSI6ImRvY3VtZW50LXVwbG9hZC9kb2N1bWVudC11cGxvYWQubW9kdWxlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports"], function (require, exports) {
    documentsService.$inject = ['$resource'];
    function documentsService($resource) {
        var userDocouments = $resource('api/UserDocuments/:documentId');
        function getbyId(documentId) {
            return userDocouments.get({ documentId: documentId });
        }
        function deleteById(documentId) {
            return userDocouments.delete({ documentId: documentId });
        }
        return {
            deleteById: deleteById,
            getbyId: getbyId
        };
    }
    exports.documentsService = documentsService;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50cy5zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbImRvY3VtZW50c1NlcnZpY2UiLCJkb2N1bWVudHNTZXJ2aWNlLmdldGJ5SWQiLCJkb2N1bWVudHNTZXJ2aWNlLmRlbGV0ZUJ5SWQiXSwibWFwcGluZ3MiOiJBQUFBLFlBQVksQ0FBQzs7SUFJYixnQkFBZ0IsQ0FBQyxPQUFPLEdBQUcsQ0FBQyxXQUFXLENBQUMsQ0FBQztJQUt6QywwQkFBaUMsU0FBMkI7UUFDeERBLElBQUlBLGNBQWNBLEdBQUdBLFNBQVNBLENBQXFCQSwrQkFBK0JBLENBQUNBLENBQUNBO1FBQ3BGQSxpQkFBaUJBLFVBQVVBO1lBQ3ZCQyxNQUFNQSxDQUFDQSxjQUFjQSxDQUFDQSxHQUFHQSxDQUFDQSxFQUFFQSxZQUFBQSxVQUFVQSxFQUFFQSxDQUFDQSxDQUFDQTtRQUM5Q0EsQ0FBQ0E7UUFDREQsb0JBQW9CQSxVQUFVQTtZQUMxQkUsTUFBTUEsQ0FBQ0EsY0FBY0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsRUFBRUEsWUFBQUEsVUFBVUEsRUFBRUEsQ0FBQ0EsQ0FBQ0E7UUFDakRBLENBQUNBO1FBQ0RGLE1BQU1BLENBQUNBO1lBQ0hBLFlBQUFBLFVBQVVBO1lBQ1ZBLFNBQUFBLE9BQU9BO1NBQ1ZBLENBQUNBO0lBQ05BLENBQUNBO0lBWmUsd0JBQWdCLG1CQVkvQixDQUFBIiwiZmlsZSI6ImRvY3VtZW50LWxpc3QvZG9jdW1lbnRzLnNlcnZpY2UuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    lexicalMenuBuilder.$inject = [];
    function lexicalMenuBuilder() {
        var _a = [createForVerbalMenuBuilder({}), createForReferencerMenuBuilder({})], buildForVerbal = _a[0], buildForReferencer = _a[1];
        return {
            buildAngularMenu: function (source) {
                return referencerMenuIsViable(source) ?
                    buildForReferencer(source) :
                    verbalMenuIsViable(source) ?
                        buildForVerbal(source) :
                        undefined;
            }
        };
        function verbalMenuIsViable(menu) {
            return !!(menu && (menu.directObjectIds || menu.indirectObjectIds || menu.subjectIds));
        }
        function referencerMenuIsViable(menu) {
            return !!(menu && menu.refersToIds);
        }
    }
    exports.lexicalMenuBuilder = lexicalMenuBuilder;
    function createForReferencerMenuBuilder(menuActionTargets) {
        var resetReferencerAsssotionCssClasses = function () {
            return Object.keys(menuActionTargets)
                .map(function (key) { return menuActionTargets[key]; })
                .forEach(function ($e) { return $e.removeClass('referred-to-by-current'); });
        };
        return function (source) { return [
            ['View Referred To', function (itemScope, event) {
                    resetReferencerAsssotionCssClasses();
                    source.refersToIds.forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass('referred-to-by-current'); });
                }]
        ]; };
    }
    function createForVerbalMenuBuilder(menuActionTargets) {
        return (function (verbalMenuCssClassMap) {
            return function (source) {
                var menuItems = [];
                if (source.subjectIds) {
                    menuItems.push(['View Subjects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.subjectIds
                                .forEach(function (id) {
                                menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Subjects']);
                            });
                        }]);
                }
                if (source.directObjectIds) {
                    menuItems.push(['View Direct Objects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.directObjectIds
                                .forEach(function (id) { return menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Direct Objects']); });
                        }]);
                }
                if (source.indirectObjectIds) {
                    menuItems.push(['View Indirect Objects', function (itemScope, event) {
                            resetVerbalAssociationCssClasses();
                            source.indirectObjectIds.forEach(function (id) {
                                menuActionTargets[id] = $('#' + id).addClass(verbalMenuCssClassMap['View Indirect Objects']);
                            });
                        }]);
                }
                return menuItems;
            };
            function resetVerbalAssociationCssClasses() {
                Object.keys(menuActionTargets)
                    .map(function (key) { return menuActionTargets[key]; })
                    .forEach(function ($e) {
                    return Object.keys(verbalMenuCssClassMap)
                        .map(function (k) { return verbalMenuCssClassMap[k]; })
                        .forEach(function (cssClass) { return $e.removeClass(cssClass); });
                });
            }
        })({
            'View Subjects': 'subject-of-current',
            'View Direct Objects': 'direct-object-of-current',
            'View Indirect Objects': 'indirect-object-of-current'
        });
    }
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvbGV4aWNhbC1tZW51LWJ1aWxkZXIuc2VydmljZS50cyJdLCJuYW1lcyI6WyJsZXhpY2FsTWVudUJ1aWxkZXIiLCJsZXhpY2FsTWVudUJ1aWxkZXIudmVyYmFsTWVudUlzVmlhYmxlIiwibGV4aWNhbE1lbnVCdWlsZGVyLnJlZmVyZW5jZXJNZW51SXNWaWFibGUiLCJjcmVhdGVGb3JSZWZlcmVuY2VyTWVudUJ1aWxkZXIiLCJjcmVhdGVGb3JWZXJiYWxNZW51QnVpbGRlciIsInJlc2V0VmVyYmFsQXNzb2NpYXRpb25Dc3NDbGFzc2VzIl0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBSWIsa0JBQWtCLENBQUMsT0FBTyxHQUFHLEVBQUUsQ0FBQztJQUVoQztRQUNJQSxJQUFJQSxLQUF1Q0EsQ0FBQ0EsMEJBQTBCQSxDQUFDQSxFQUFFQSxDQUFDQSxFQUFFQSw4QkFBOEJBLENBQUNBLEVBQUVBLENBQUNBLENBQUNBLEVBQTFHQSxjQUFjQSxVQUFFQSxrQkFBa0JBLFFBQXdFQSxDQUFDQTtRQUVoSEEsTUFBTUEsQ0FBQ0E7WUFDSEEsZ0JBQWdCQSxFQUFFQSxVQUFBQSxNQUFNQTt1QkFDcEJBLHNCQUFzQkEsQ0FBa0NBLE1BQU1BLENBQUNBO29CQUMzREEsa0JBQWtCQSxDQUFrQ0EsTUFBTUEsQ0FBQ0E7b0JBQzNEQSxrQkFBa0JBLENBQThCQSxNQUFNQSxDQUFDQTt3QkFDbkRBLGNBQWNBLENBQThCQSxNQUFNQSxDQUFDQTt3QkFDbkRBLFNBQVNBO1lBSmpCQSxDQUlpQkE7U0FDeEJBLENBQUNBO1FBRUZBLDRCQUE0QkEsSUFBaUNBO1lBQ3pEQyxNQUFNQSxDQUFDQSxDQUFDQSxDQUFDQSxDQUFDQSxJQUFJQSxJQUFJQSxDQUFDQSxJQUFJQSxDQUFDQSxlQUFlQSxJQUFJQSxJQUFJQSxDQUFDQSxpQkFBaUJBLElBQUlBLElBQUlBLENBQUNBLFVBQVVBLENBQUNBLENBQUNBLENBQUNBO1FBQzNGQSxDQUFDQTtRQUNE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

define(["require", "exports"], function (require, exports) {
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJtb2RlbHMuanMiLCJzb3VyY2VzQ29udGVudCI6W10sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports", 'app/LASI'], function (require, exports, LASI_1) {
    resultsService.$inject = ['$http', '$q'];
    function resultsService($http, $q) {
        var tasks = [];
        var processDocument = function (documentId, documentName) {
            tasks[documentId] = { percentComplete: 0 };
            var deferred = $q.defer();
            $http.get('Analysis/' + documentId)
                .success(success)
                .error(error);
            return deferred.promise;
            function success(data) {
                var markupHeader = $('<div class="panel panel-default">' +
                    '<div class="panel-heading"><h4 class="panel-title"><a href="#' +
                    documentId +
                    '" data-toggle="collapse" data-parent="#accordion">' +
                    documentName +
                    '</a></h4></div></div>');
                var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' +
                    JSON.stringify(data) + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(markupHeader).append(markupPanel);
                }
                else {
                    $('#' + documentId).remove();
                    $('#accordion').append(markupPanel);
                }
                LASI_1.buildMenus();
                LASI_1.enableActiveHighlighting();
                tasks[documentId].percentComplete = 100;
                deferred.resolve(data);
                alert(JSON.stringify(data));
            }
            function error(xhr, message, detail) {
                deferred.reject(message);
            }
        };
        return {
            tasks: tasks,
            processDocument: processDocument
        };
    }
    exports.resultsService = resultsService;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L3Jlc3VsdHMuc2VydmljZS50cyJdLCJuYW1lcyI6WyJyZXN1bHRzU2VydmljZSIsInN1Y2Nlc3MiLCJlcnJvciJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUtiLGNBQWMsQ0FBQyxPQUFPLEdBQUcsQ0FBQyxPQUFPLEVBQUUsSUFBSSxDQUFDLENBQUM7SUFNekMsd0JBQXdCLEtBQTJCLEVBQUUsRUFBcUI7UUFDdEVBLElBQUlBLEtBQUtBLEdBQUdBLEVBQUVBLENBQUNBO1FBQ2ZBLElBQUlBLGVBQWVBLEdBQUdBLFVBQVVBLFVBQVVBLEVBQUVBLFlBQVlBO1lBQ3BELEtBQUssQ0FBQyxVQUFVLENBQUMsR0FBRyxFQUFFLGVBQWUsRUFBRSxDQUFDLEVBQUUsQ0FBQztZQUUzQyxJQUFJLFFBQVEsR0FBRyxFQUFFLENBQUMsS0FBSyxFQUFpQixDQUFDO1lBQ3pDLEtBQUssQ0FBQyxHQUFHLENBQWdCLFdBQVcsR0FBRyxVQUFVLENBQUM7aUJBQzdDLE9BQU8sQ0FBQyxPQUFPLENBQUM7aUJBQ2hCLEtBQUssQ0FBQyxLQUFLLENBQUMsQ0FBQztZQUNsQixNQUFNLENBQUMsUUFBUSxDQUFDLE9BQU8sQ0FBQztZQUV4QixpQkFBaUIsSUFBSTtnQkFDakJDLElBQUlBLFlBQVlBLEdBQUdBLENBQUNBLENBQUNBLG1DQUFtQ0E7b0JBQ3BEQSwrREFBK0RBO29CQUMvREEsVUFBVUE7b0JBQ1ZBLG9EQUFvREE7b0JBQ3BEQSxZQUFZQTtvQkFDWkEsdUJBQXVCQSxDQUFDQSxDQUFDQTtnQkFDN0JBLElBQUlBLFdBQVdBLEdBQUdBLENBQUNBLENBQUNBLFdBQVdBLEdBQUdBLFVBQVVBLEdBQUdBLHVDQUF1Q0E7b0JBQ2xGQSxJQUFJQSxDQUFDQSxTQUFTQSxDQUFDQSxJQUFJQSxDQUFDQSxHQUFHQSxRQUFRQSxHQUFHQSxRQUFRQSxDQUFDQSxDQUFDQTtnQkFDaERBLEVBQUVBLENBQUNBLENBQUNBLENBQUNBLENBQUNBLENBQUNBLEdBQUdBLEdBQUdBLFVBQVVBLENBQUNBLENBQUNBLE1BQU1BLENBQUNBLENBQUNBLENBQUNBO29CQUM5QkEsQ0FBQ0EsQ0FBQ0EsWUFBWUEsQ0FBQ0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsWUFBWUEsQ0FBQ0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsV0FBV0EsQ0FBQ0EsQ0FBQ0E7Z0JBQzdEQSxDQUFDQTtnQkFBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0E7b0JBQ0pBLENBQUNBLENBQUNBLEdBQUdBLEdBQUdBLFVBQVVBLENBQUNBLENBQUNBLE1BQU1BLEVBQUVBLENBQUNBO29CQUM3QkEsQ0FBQ0EsQ0FBQ0EsWUFBWUEsQ0FBQ0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsV0FBV0EsQ0FBQ0EsQ0FBQ0E7Z0JBQ3hDQSxDQUFDQTtnQkFDREEsaUJBQVVBLEVBQUVBLENBQUNBO2dCQUNiQSwrQkFBd0JBLEVBQUVBLENBQUNBO2dCQUMzQkEsS0FBS0EsQ0FBQ0EsVUFBVUEsQ0FBQ0EsQ0FBQ0EsZUFBZUEsR0FBR0EsR0FBR0EsQ0FBQ0E7Z0JBQ3hDQSxRQUFRQSxDQUFDQSxPQUFPQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQTtnQkFDdkJBLEtBQUtBLENBQUNBLElBQUlBLENBQUNBLFNBQVNBLENBQUNBLElBQUlBLENBQUNBLENBQUNBLENBQUNBO1lBQ2hDQSxDQUFDQTtZQUNELGVBQWUsR0FBRyxFQUFFLE9BQU8sRUFBRSxNQUFNO2dCQUMvQkMsUUFBUUEsQ0FBQ0EsTUFBTUEsQ0FBQ0EsT0FBT0EsQ0FBQ0EsQ0FBQ0E7WUFDN0JBLENBQUNBO1FBQ0wsQ0FBQyxDQUFDRjtRQUNGQSxNQUFNQSxDQUFDQTtZQUNIQSxPQUFBQSxLQUFLQTtZQUNMQSxpQkFBQUEsZUFBZUE7U0FDbEJBLENBQUNBO0lBQ05BLENBQUNBO0lBQ1Esc0JBQWMsa0JBRHRCO0lBQ3dCIiwiZmlsZSI6ImRvY3VtZW50LWxpc3QvcmVzdWx0cy5zZXJ2aWNlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports"], function (require, exports) {
    documentModelService.$inject = ['$http'];
    function documentModelService($http) {
        return {
            processDocument: function (documentId) {
                return $http.get("Analysis/" + documentId, { cache: false });
            }
        };
    }
    exports.documentModelService = documentModelService;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtbW9kZWwuc2VydmljZS50cyJdLCJuYW1lcyI6WyJkb2N1bWVudE1vZGVsU2VydmljZSIsImRvY3VtZW50TW9kZWxTZXJ2aWNlLnByb2Nlc3NEb2N1bWVudCJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUtiLG9CQUFvQixDQUFDLE9BQU8sR0FBRyxDQUFDLE9BQU8sQ0FBQyxDQUFDO0lBQ3pDLDhCQUFxQyxLQUEyQjtRQUM1REEsTUFBTUEsQ0FBQ0E7WUFDSEEsZUFBZUEsWUFBQ0EsVUFBVUE7Z0JBQ3RCQyxNQUFNQSxDQUFDQSxLQUFLQSxDQUFDQSxHQUFHQSxDQUFDQSxjQUFZQSxVQUFZQSxFQUFFQSxFQUFFQSxLQUFLQSxFQUFFQSxLQUFLQSxFQUFFQSxDQUFDQSxDQUFBQTtZQUNoRUEsQ0FBQ0E7U0FDSkQsQ0FBQ0E7SUFDTkEsQ0FBQ0E7SUFOZSw0QkFBb0IsdUJBTW5DLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LW1vZGVsLnNlcnZpY2UuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    var DocumentListServiceProvider = (function () {
        function DocumentListServiceProvider() {
            this.$inject = ['$resource'];
            this.$get.$inject = ['$resource'];
        }
        DocumentListServiceProvider.prototype.setDocumentListUrl = function (url) {
            this.documentListUrl = url;
            return this;
        };
        DocumentListServiceProvider.prototype.setRecentDocumentCount = function (count) {
            this.recentDocumentCount = count;
            return this;
        };
        /**
         * @param $resource an instance of the Resource Service supplied by the angular-resource module.
         */
        DocumentListServiceProvider.prototype.$get = function ($resource) {
            var resource = $resource(this.documentListUrl + '?limit=' + this.recentDocumentCount, {}, {
                get: {
                    method: 'GET',
                    isArray: true
                },
                delete: {
                    method: 'DELETE',
                    isArray: false
                }
            });
            return {
                deleteDocument: function (documentId) {
                    return resource.delete({ documentId: documentId })[0];
                },
                get: resource.get
            };
        };
        ;
        return DocumentListServiceProvider;
    })();
    exports.DocumentListServiceProvider = DocumentListServiceProvider;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3Qtc2VydmljZS5wcm92aWRlci50cyJdLCJuYW1lcyI6WyJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuY29uc3RydWN0b3IiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuc2V0RG9jdW1lbnRMaXN0VXJsIiwiRG9jdW1lbnRMaXN0U2VydmljZVByb3ZpZGVyLnNldFJlY2VudERvY3VtZW50Q291bnQiLCJEb2N1bWVudExpc3RTZXJ2aWNlUHJvdmlkZXIuJGdldCJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQW9CYjtRQUdJQTtZQVlBQyxZQUFPQSxHQUFHQSxDQUFDQSxXQUFXQSxDQUFDQSxDQUFDQTtZQVhwQkEsSUFBSUEsQ0FBQ0EsSUFBSUEsQ0FBQ0EsT0FBT0EsR0FBR0EsQ0FBQ0EsV0FBV0EsQ0FBQ0EsQ0FBQ0E7UUFDdENBLENBQUNBO1FBQ0RELHdEQUFrQkEsR0FBbEJBLFVBQW1CQSxHQUFXQTtZQUMxQkUsSUFBSUEsQ0FBQ0EsZUFBZUEsR0FBR0EsR0FBR0EsQ0FBQ0E7WUFDM0JBLE1BQU1BLENBQUNBLElBQUlBLENBQUNBO1FBQ2hCQSxDQUFDQTtRQUNERiw0REFBc0JBLEdBQXRCQSxVQUF1QkEsS0FBYUE7WUFDaENHLElBQUlBLENBQUNBLG1CQUFtQkEsR0FBR0EsS0FBS0EsQ0FBQ0E7WUFDakNBLE1BQU1BLENBQUNBLElBQUlBLENBQUNBO1FBQ2hCQSxDQUFDQTtRQUdESDs7V0FFR0E7UUFDSEEsMENBQUlBLEdBQUpBLFVBQUtBLFNBQTRDQTtZQUM3Q0ksSUFBSUEsUUFBUUEsR0FBR0EsU0FBU0EsQ0FBMEJBLElBQUlBLENBQUNBLGVBQWVBLEdBQUdBLFNBQVNBLEdBQUdBLElBQUlBLENBQUNBLG1CQUFtQkEsRUFBRUEsRUFBRUEsRUFBRUE7Z0JBQy9HQSxHQUFHQSxFQUFFQTtvQkFDREEsTUFBTUEsRUFBRUEsS0FBS0E7b0JBQ2JBLE9BQU9BLEVBQUVBLElBQUlBO2lCQUNoQkE7Z0JBQ0RBLE1BQU1BLEVBQUVBO29CQUNKQSxNQUFNQSxFQUFFQSxRQUFRQTtvQkFDaEJBLE9BQU9BLEVBQUVBLEtBQUtBO2lCQUNqQkE7YUFDSkEsQ0FBQ0EsQ0FBQ0E7WUFFSEEsTUFBTUEsQ0FBQ0E7Z0JBQ0hBLGNBQWNBLEVBQUVBLFVBQVVBLFVBQWtCQTtvQkFDeEMsTUFBTSxDQUFDLFFBQVEsQ0FBQyxNQUFNLENBQUMsRUFBRSxZQUFBLFVBQVUsRUFBRSxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUM7Z0JBQzlDLENBQUM7Z0JBQ0RBLEdBQUdBLEVBQUVBLFFBQVFBLENBQUNBLEdBQUdBO2FBQ3BCQSxDQUFDQTtRQUNOQSxDQUFDQTs7UUFDTEosa0NBQUNBO0lBQURBLENBdENBLEFBc0NDQSxJQUFBO0lBdENZLG1DQUEyQiw4QkFzQ3ZDLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LXNlcnZpY2UucHJvdmlkZXIuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    var log = console.log.bind(console);
    var UploadController = (function () {
        function UploadController($scope, $q, uploadService) {
            this.$scope = $scope;
            this.$q = $q;
            this.uploadService = uploadService;
            this.files = [];
            this.$scope.$watch('upload.files', this.uploadFiles.bind(this));
        }
        UploadController.prototype.uploadFiles = function () {
            var _this = this;
            this.logInvalidFiles();
            var files = this.files;
            return this.$q.when((Array.isArray(files) ? files : [files]).map(function (file) { return _this.uploadFile(file); }));
        };
        UploadController.prototype.logInvalidFiles = function () {
            var files = this.files;
            (Array.isArray(files) ? files : [files]).filter(function (file) { return UploadController.formats.every(function (format) { return file.type.localeCompare(format) !== 0; }); })
                .map(function (file) { return ("File " + file.name + " has unaccepted format " + file.type); })
                .forEach(log);
        };
        UploadController.prototype.uploadFile = function (file) {
            return this.uploadService
                .upload({
                file: file,
                url: 'api/UserDocuments',
                method: 'POST',
                fileName: file.name
            })
                .progress(function (data) { return log("Progress: " + 100.0 * data.progress / data.percentComplete + "% " + file.name); })
                .success(function (data) { return log("File " + file.name + " uploaded. \nResponse: " + JSON.stringify(data)); })
                .error(function (data, status) { return log("Http: " + status + " Failed to upload file. \nDetails: " + data); });
        };
        UploadController.prototype.removeFile = function (file, index) {
            var files = this.files;
            this.files = (Array.isArray(files) ? files : [files]).filter(function (f) { return f.name !== file.name; });
            $('#file-upload-list').remove("#upload-list-item-" + index);
        };
        UploadController.$inject = ['$scope', '$q', 'Upload'];
        UploadController.formats = [
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
            'application/msword',
            'application/pdf',
            'text/plain'
        ];
        return UploadController;
    })();
    exports.UploadController = UploadController;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L3VwbG9hZC5jb250cm9sbGVyLnRzIl0sIm5hbWVzIjpbIlVwbG9hZENvbnRyb2xsZXIiLCJVcGxvYWRDb250cm9sbGVyLmNvbnN0cnVjdG9yIiwiVXBsb2FkQ29udHJvbGxlci51cGxvYWRGaWxlcyIsIlVwbG9hZENvbnRyb2xsZXIubG9nSW52YWxpZEZpbGVzIiwiVXBsb2FkQ29udHJvbGxlci51cGxvYWRGaWxlIiwiVXBsb2FkQ29udHJvbGxlci5yZW1vdmVGaWxlIl0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBR2IsSUFBSSxHQUFHLEdBQUcsT0FBTyxDQUFDLEdBQUcsQ0FBQyxJQUFJLENBQUMsT0FBTyxDQUFDLENBQUM7SUFDcEM7UUFHSUEsMEJBQW9CQSxNQUFzQkEsRUFDOUJBLEVBQWdCQSxFQUNoQkEsYUFBaUNBO1lBRnpCQyxXQUFNQSxHQUFOQSxNQUFNQSxDQUFnQkE7WUFDOUJBLE9BQUVBLEdBQUZBLEVBQUVBLENBQWNBO1lBQ2hCQSxrQkFBYUEsR0FBYkEsYUFBYUEsQ0FBb0JBO1lBa0M3Q0EsVUFBS0EsR0FBa0JBLEVBQUVBLENBQUNBO1lBakN0QkEsSUFBSUEsQ0FBQ0EsTUFBTUEsQ0FBQ0EsTUFBTUEsQ0FBQ0EsY0FBY0EsRUFBRUEsSUFBSUEsQ0FBQ0EsV0FBV0EsQ0FBQ0EsSUFBSUEsQ0FBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7UUFDcEVBLENBQUNBO1FBRURELHNDQUFXQSxHQUFYQTtZQUFBRSxpQkFJQ0E7WUFIR0EsSUFBSUEsQ0FBQ0EsZUFBZUEsRUFBRUEsQ0FBQ0E7WUFDdkJBLElBQUlBLEtBQUtBLEdBQUdBLElBQUlBLENBQUNBLEtBQUtBLENBQUNBO1lBQ3ZCQSxNQUFNQSxDQUFDQSxJQUFJQSxDQUFDQSxFQUFFQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQSxLQUFLQSxDQUFDQSxPQUFPQSxDQUFDQSxLQUFLQSxDQUFDQSxHQUFHQSxLQUFLQSxHQUFHQSxDQUFDQSxLQUFLQSxDQUFDQSxDQUFDQSxDQUFDQSxHQUFHQSxDQUFDQSxVQUFBQSxJQUFJQSxJQUFJQSxPQUFBQSxLQUFJQSxDQUFDQSxVQUFVQSxDQUFDQSxJQUFJQSxDQUFDQSxFQUFyQkEsQ0FBcUJBLENBQUNBLENBQUNBLENBQUNBO1FBQ3JHQSxDQUFDQTtRQUNERiwwQ0FBZUEsR0FBZkE7WUFDSUcsSUFBSUEsS0FBS0EsR0FBR0EsSUFBSUEsQ0FBQ0EsS0FBS0EsQ0FBQ0E7WUFDdkJBLENBQUNBLEtBQUtBLENBQUNBLE9BQU9BLENBQUNBLEtBQUtBLENBQUNBLEdBQUdBLEtBQUtBLEdBQUdBLENBQUNBLEtBQUtBLENBQUNBLENBQUNBLENBQUNBLE1BQU1BLENBQUNBLFVBQUFBLElBQUlBLElBQUlBLE9BQUFBLGdCQUFnQkEsQ0FBQ0EsT0FBT0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsVUFBQUEsTUFBTUEsSUFBSUEsT0FBQUEsSUFBSUEsQ0FBQ0EsSUFBSUEsQ0FBQ0EsYUFBYUEsQ0FBQ0EsTUFBTUEsQ0FBQ0EsS0FBS0EsQ0FBQ0EsRUFBckNBLENBQXFDQSxDQUFDQSxFQUEvRUEsQ0FBK0VBLENBQUNBO2lCQUNuSUEsR0FBR0EsQ0FBQ0EsVUFBQUEsSUFBSUEsSUFBSUEsT0FBQUEsV0FBUUEsSUFBSUEsQ0FBQ0EsSUFBSUEsK0JBQTBCQSxJQUFJQSxDQUFDQSxJQUFJQSxDQUFFQSxFQUF0REEsQ0FBc0RBLENBQUNBO2lCQUNuRUEsT0FBT0EsQ0FBQ0EsR0FBR0EsQ0FBQ0EsQ0FBQ0E7UUFDdEJBLENBQUNBO1FBQ0RILHFDQUFVQSxHQUFWQSxVQUFXQSxJQUFVQTtZQUNqQkksTUFBTUEsQ0FBQ0EsSUFBSUEsQ0FBQ0EsYUFBYUE7aUJBQ3BCQSxNQUFNQSxDQUF3QkE7Z0JBQzNCQSxNQUFBQSxJQUFJQTtnQkFDSkEsR0FBR0EsRUFBRUEsbUJBQW1CQTtnQkFDeEJBLE1BQU1BLEVBQUVBLE1BQU1BO2dCQUNkQSxRQUFRQSxFQUFFQSxJQUFJQSxDQUFDQSxJQUFJQTthQUN0QkEsQ0FBQ0E7aUJBQ0RBLFFBQVFBLENBQUNBLFVBQUFBLElBQUlBLElBQUlBLE9BQUFBLEdBQUdBLENBQUNBLGVBQWFBLEtBQUtBLEdBQUdBLElBQUlBLENBQUNBLFFBQVFBLEdBQUdBLElBQUlBLENBQUNBLGVBQWVBLFVBQUtBLElBQUlBLENBQUNBLElBQU1BLENBQUNBLEVBQTlFQSxDQUE4RUEsQ0FBQ0E7aUJBQ2hHQSxPQUFPQSxDQUFDQSxVQUFBQSxJQUFJQSxJQUFJQSxPQUFBQSxHQUFHQSxDQUFDQSxVQUFRQSxJQUFJQSxDQUFDQSxJQUFJQSwrQkFBMEJBLElBQUlBLENBQUNBLFNBQVNBLENBQUNBLElBQUlBLENBQUlBLENBQUNBLEVBQXZFQSxDQUF1RUEsQ0FBQ0E7aUJBQ3hGQSxLQUFLQSxDQUFDQSxVQUFDQSxJQUFJQSxFQUFFQSxNQUFNQSxJQUFLQSxPQUFBQSxHQUFHQSxDQUFDQSxXQUFTQSxNQUFNQSwyQ0FBc0NBLElBQU1BLENBQUNBLEVBQWhFQSxDQUFnRUEsQ0FBQ0EsQ0FBQ0E7UUFDbkdBLENBQUNBO1FBRURKLHFDQUFVQSxHQUFWQSxVQUFXQSxJQUFVQSxFQUFFQSxLQUFhQTtZQUNoQ0ssSUFBSUEsS0FBS0EsR0FBR0EsSUFBSUEsQ0FBQ0EsS0FBS0EsQ0FBQ0E7WUFDdkJBLElBQUlBLENBQUNBLEtBQUtBLEdBQUdBLENBQUNBLEtBQUtBLENBQUNBLE9BQU9BLENBQUNBLEtBQUtBLENBQUNBLEdBQUdBLEtBQUtBLEdBQUdBLENBQUNBLEtBQUtBLENBQUNBLENBQUNBLENBQUNBLE1BQU1BLENBQUNBLFVBQUFBLENBQUNBLElBQUlBLE9BQUFBLENBQUNBLENBQUNBLElBQUlBLEtBQUtBLElBQUlBLENBQUNBLElBQUlBLEVBQXBCQSxDQUFvQkEsQ0FBQ0EsQ0FBQ0E7WUFDeEZBLENBQUNBLENBQUNBLG1CQUFtQkEsQ0FBQ0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsdUJBQXFCQSxLQUFPQSxDQUFDQSxDQUFDQTtRQUNoRUEsQ0FBQ0E7UUFwQ01MLHdCQUFPQSxHQUFHQSxDQUFDQSxRQUFRQSxFQUFFQSxJQUFJQSxFQUFFQSxRQUFRQSxDQUFDQSxDQUFDQTtRQXdDN0JBLHdCQUFPQSxHQUFHQTtZQUNyQkEseUVBQXlFQTtZQUN6RUEsb0JBQW9CQTtZQUNwQkEsaUJBQWlCQTtZQUNqQkEsWUFBWUE7U0FDZkEsQ0FBQ0E7UUFFTkEsdUJBQUNBO0lBQURBLENBaERBLEFBZ0RDQSxJQUFBO0lBaERZLHdCQUFnQixtQkFnRDVCLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtbGlzdC91cGxvYWQuY29udHJvbGxlci5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    var ListController = (function () {
        function ListController($q, documentListService, tasksListService, documentsService, documentModelService) {
            this.$q = $q;
            this.documentListService = documentListService;
            this.tasksListService = tasksListService;
            this.documentsService = documentsService;
            this.documentModelService = documentModelService;
            this.expanded = false;
            this.documents = [];
            this.activate();
        }
        ListController.prototype.toggleExpanded = function () {
            this.expanded = !this.expanded;
        };
        ListController.prototype.deleteById = function (id) {
            var deleteResult = this.documentsService.deleteById(id);
            console.log(deleteResult);
            this.documents = this.documentListService.get();
        };
        Object.defineProperty(ListController.prototype, "documentCount", {
            get: function () {
                return this.documents.length;
            },
            enumerable: true,
            configurable: true
        });
        ListController.prototype.processDocument = function (document) {
            if (!this.documents.some(function (d) { return d.raeification && d.id === document.id; })) {
                this.documentModelService.processDocument(document.id)
                    .success(function (processed) { return document.raeification = processed; })
                    .error(function (error) { return console.log(error); });
            }
        };
        ListController.prototype.activate = function () {
            var _this = this;
            return this.$q.all([
                this.$q.when(this.documentListService.get()),
                this.tasksListService.getActiveTasks().then(function (tasks) { return tasks.map(function (task) {
                    _this.tasks[task.id] = task;
                    var t = _this.documents.first(function (d) { return d.name === task.name; });
                    (t && t).task = task;
                    return t;
                }); })
            ]).then(function (data) {
                var _a = data, documents = _a[0], tasks = _a[1];
                var associated = documents.correlate(tasks, function (document) { return document.id; }, function (task) { return task.id; }, function (document, task) {
                    document.showProgress = task.state === 'Ongoing' || task.state === 'Complete';
                    document.progress = Math.round(task.percentComplete);
                    document.statusMessage = task.statusMessage;
                });
                tasks.forEach(function (task) { _this.tasks[task.id] = task; });
                _b = [documents, tasks], _this.documents = _b[0], _this.tasks = _b[1];
                var _b;
            });
        };
        ListController.$inject = ['$q', 'documentListService', 'tasksListService', 'documentsService', 'documentModelService'];
        return ListController;
    })();
    exports.ListController = ListController;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2xpc3QuY29udHJvbGxlci50cyJdLCJuYW1lcyI6WyJMaXN0Q29udHJvbGxlciIsIkxpc3RDb250cm9sbGVyLmNvbnN0cnVjdG9yIiwiTGlzdENvbnRyb2xsZXIudG9nZ2xlRXhwYW5kZWQiLCJMaXN0Q29udHJvbGxlci5kZWxldGVCeUlkIiwiTGlzdENvbnRyb2xsZXIuZG9jdW1lbnRDb3VudCIsIkxpc3RDb250cm9sbGVyLnByb2Nlc3NEb2N1bWVudCIsIkxpc3RDb250cm9sbGVyLmFjdGl2YXRlIl0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBS2I7UUFLSUEsd0JBQ1lBLEVBQXFCQSxFQUNyQkEsbUJBQXdDQSxFQUN4Q0EsZ0JBQWtDQSxFQUNsQ0EsZ0JBQWtDQSxFQUNsQ0Esb0JBQTBDQTtZQUoxQ0MsT0FBRUEsR0FBRkEsRUFBRUEsQ0FBbUJBO1lBQ3JCQSx3QkFBbUJBLEdBQW5CQSxtQkFBbUJBLENBQXFCQTtZQUN4Q0EscUJBQWdCQSxHQUFoQkEsZ0JBQWdCQSxDQUFrQkE7WUFDbENBLHFCQUFnQkEsR0FBaEJBLGdCQUFnQkEsQ0FBa0JBO1lBQ2xDQSx5QkFBb0JBLEdBQXBCQSxvQkFBb0JBLENBQXNCQTtZQVR0REEsYUFBUUEsR0FBR0EsS0FBS0EsQ0FBQ0E7WUFDakJBLGNBQVNBLEdBQTRCQSxFQUFFQSxDQUFDQTtZQVNwQ0EsSUFBSUEsQ0FBQ0EsUUFBUUEsRUFBRUEsQ0FBQ0E7UUFDcEJBLENBQUNBO1FBRURELHVDQUFjQSxHQUFkQTtZQUNJRSxJQUFJQSxDQUFDQSxRQUFRQSxHQUFHQSxDQUFDQSxJQUFJQSxDQUFDQSxRQUFRQSxDQUFDQTtRQUNuQ0EsQ0FBQ0E7UUFFREYsbUNBQVVBLEdBQVZBLFVBQVdBLEVBQVVBO1lBQ2pCRyxJQUFJQSxZQUFZQSxHQUFHQSxJQUFJQSxDQUFDQSxnQkFBZ0JBLENBQUNBLFVBQVVBLENBQUNBLEVBQUVBLENBQUNBLENBQUNBO1lBQ3hEQSxPQUFPQSxDQUFDQSxH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QSxLQUFLQSxLQUFLQSxVQUFVQSxDQUFDQTtvQkFDOUVBLFFBQVFBLENBQUNBLFFBQVFBLEdBQUdBLElBQUlBLENBQUNBLEtBQUtBLENBQUNBLElBQUlBLENBQUNBLGVBQWVBLENBQUNBLENBQUNBO29CQUNyREEsUUFBUUEsQ0FBQ0EsYUFBYUEsR0FBR0EsSUFBSUEsQ0FBQ0EsYUFBYUEsQ0FBQ0E7Z0JBQ2hEQSxDQUFDQSxDQUFDQSxDQUFDQTtnQkFFUEEsS0FBS0EsQ0FBQ0EsT0FBT0EsQ0FBQ0EsVUFBQUEsSUFBSUEsSUFBTUEsS0FBSUEsQ0FBQ0EsS0FBS0EsQ0FBQ0EsSUFBSUEsQ0FBQ0EsRUFBRUEsQ0FBQ0EsR0FBR0EsSUFBSUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7Z0JBQ3ZEQSxLQUErQkEsQ0FBQ0EsU0FBU0EsRUFBRUEsS0FBS0EsQ0FBQ0EsRUFBaERBLEtBQUlBLENBQUNBLFNBQVNBLFVBQUVBLEtBQUlBLENBQUNBLEtBQUtBLFFBQXNCQSxDQUFDQTs7WUFDdERBLENBQUNBLENBQUNBLENBQUNBO1FBRVBBLENBQUNBO1FBckRNTixzQkFBT0EsR0FBR0EsQ0FBQ0EsSUFBSUEsRUFBRUEscUJBQXFCQSxFQUFFQSxrQkFBa0JBLEVBQUVBLGtCQUFrQkEsRUFBRUEsc0JBQXNCQSxDQUFDQSxDQUFDQTtRQXNEbkhBLHFCQUFDQTtJQUFEQSxDQTFEQSxBQTBEQ0EsSUFBQTtJQTFEWSxzQkFBYyxpQkEwRDFCLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtbGlzdC9saXN0LmNvbnRyb2xsZXIuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    documentListTabsetItem.$inject = ['resultsService'];
    function documentListTabsetItem(resultsService) {
        return {
            restrict: 'E',
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.stopPropagation();
                    resultsService.processDocument(scope.documentId, scope.name);
                    event.preventDefault();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () { return scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete; });
                });
                console.log(attrs);
            },
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '='
            },
            template: require('/app/document-list/document-list-tabset-item.directive.html!')
        };
    }
    exports.documentListTabsetItem = documentListTabsetItem;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QtdGFic2V0LWl0ZW0uZGlyZWN0aXZlLnRzIl0sIm5hbWVzIjpbImRvY3VtZW50TGlzdFRhYnNldEl0ZW0iXSwibWFwcGluZ3MiOiJBQUFBLFlBQVksQ0FBQzs7SUFFYixzQkFBc0IsQ0FBQyxPQUFPLEdBQUcsQ0FBQyxnQkFBZ0IsQ0FBQyxDQUFDO0lBRXBELGdDQUF1QyxjQUFjO1FBRWpEQSxNQUFNQSxDQUFDQTtZQUNIQSxRQUFRQSxFQUFFQSxHQUFHQTtZQUNiQSxJQUFJQSxFQUFFQSxVQUFDQSxLQUFrQ0EsRUFBRUEsT0FBT0EsRUFBRUEsS0FBS0E7Z0JBQ3JEQSxPQUFPQSxDQUFDQSxLQUFLQSxDQUFDQSxVQUFBQSxLQUFLQTtvQkFDZkEsS0FBS0EsQ0FBQ0EsZUFBZUEsRUFBRUEsQ0FBQ0E7b0JBQ3hCQSxjQUFjQSxDQUFDQSxlQUFlQSxDQUFDQSxLQUFLQSxDQUFDQSxVQUFVQSxFQUFFQSxLQUFLQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQTtvQkFBQ0EsS0FBS0EsQ0FBQ0EsY0FBY0EsRUFBRUEsQ0FBQ0E7b0JBQ3JGQSxJQUFJQSxPQUFPQSxHQUFHQSxjQUFjQSxDQUFDQSxlQUFlQSxDQUFDQSxLQUFLQSxDQUFDQSxVQUFVQSxFQUFFQSxLQUFLQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQTtvQkFDM0VBLEtBQUtBLENBQUNBLGdCQUFnQkEsR0FBR0EsY0FBY0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsVUFBVUEsQ0FBQ0EsQ0FBQ0EsZUFBZUEsQ0FBQ0E7b0JBQ2hGQSxLQUFLQSxDQUFDQSxZQUFZQSxHQUFHQSxJQUFJQSxDQUFDQTtvQkFDMUJBLE9BQU9BLENBQUNBLElBQUlBLENBQUNBLGNBQU1BLE9BQUFBLEtBQUtBLENBQUNBLGdCQUFnQkEsR0FBR0EsY0FBY0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsVUFBVUEsQ0FBQ0EsQ0FBQ0EsZUFBZUEsRUFBL0VBLENBQStFQSxDQUFDQSxDQUFDQTtnQkFDeEdBLENBQUNBLENBQUNBLENBQUNBO2dCQUNIQSxPQUFPQSxDQUFDQSxHQUFHQSxDQUFDQSxLQUFLQSxDQUFDQSxDQUFDQTtZQUN2QkEsQ0FBQ0E7WUFDREEsS0FBS0EsRUFBRUE7Z0JBQ0hBLFVBQVVBLEVBQUVBLEdBQUdBO2dCQUNmQSxJQUFJQSxFQUFFQSxHQUFHQTtnQkFDVEEsZUFBZUEsRUFBRUEsR0FBR0E7YUFDdkJBO1lBQ0RBLFFBQVFBLEVBQUVBLE9BQU9BLENBQUNBLDhEQUE4REEsQ0FBQ0E7U0FDcEZBLENBQUNBO0lBQ05BLENBQUNBO0lBdEJlLDhCQUFzQix5QkFzQnJDLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LXRhYnNldC1pdGVtLmRpcmVjdGl2ZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    documentListMenuItem.$inject = ['$window', 'resultsService'];
    function documentListMenuItem($window, resultsService) {
        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            template: require('/app/document-list/document-list-menu-item.directive.html!'),
            scope: {
                name: '=',
                documentId: '='
            },
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.preventDefault();
                    event.stopPropagation();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () {
                        scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    });
                });
            }
        };
    }
    exports.documentListMenuItem = documentListMenuItem;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QtbWVudS1pdGVtLmRpcmVjdGl2ZS50cyJdLCJuYW1lcyI6WyJkb2N1bWVudExpc3RNZW51SXRlbSJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUNiLG9CQUFvQixDQUFDLE9BQU8sR0FBRyxDQUFDLFNBQVMsRUFBRSxnQkFBZ0IsQ0FBQyxDQUFDO0lBUTdELDhCQUFxQyxPQUFPLEVBQUUsY0FBYztRQUV4REEsTUFBTUEsQ0FBQ0E7WUFDSEEsVUFBVUEsRUFBRUEsSUFBSUE7WUFDaEJBLE9BQU9BLEVBQUVBLElBQUlBO1lBQ2JBLFFBQVFBLEVBQUVBLEdBQUdBO1lBQ2JBLFFBQVFBLEVBQUVBLE9BQU9BLENBQUNBLDREQUE0REEsQ0FBQ0E7WUFDL0VBLEtBQUtBLEVBQUVBO2dCQUNIQSxJQUFJQSxFQUFFQSxHQUFHQTtnQkFDVEEsVUFBVUEsRUFBRUEsR0FBR0E7YUFDbEJBO1lBQ0RBLElBQUlBLEVBQUVBLFVBQVVBLEtBQTRCQSxFQUFFQSxPQUFlQSxFQUFFQSxLQUEwQkE7Z0JBQ3JGLE9BQU8sQ0FBQyxLQUFLLENBQUMsVUFBVSxLQUFLO29CQUN6QixLQUFLLENBQUMsY0FBYyxFQUFFLENBQUM7b0JBQ3ZCLEtBQUssQ0FBQyxlQUFlLEVBQUUsQ0FBQztvQkFDeEIsSUFBSSxPQUFPLEdBQUcsY0FBYyxDQUFDLGVBQWUsQ0FBQyxLQUFLLENBQUMsVUFBVSxFQUFFLEtBQUssQ0FBQyxJQUFJLENBQUMsQ0FBQztvQkFDM0UsS0FBSyxDQUFDLGdCQUFnQixHQUFHLGNBQWMsQ0FBQyxLQUFLLENBQUMsS0FBSyxDQUFDLFVBQVUsQ0FBQyxDQUFDLGVBQWUsQ0FBQztvQkFDaEYsS0FBSyxDQUFDLFlBQVksR0FBRyxJQUFJLENBQUM7b0JBQzFCLE9BQU8sQ0FBQyxJQUFJLENBQUM7d0JBQ1QsS0FBSyxDQUFDLGdCQUFnQixHQUFHLGNBQWMsQ0FBQyxLQUFLLENBQUMsS0FBSyxDQUFDLFVBQVUsQ0FBQyxDQUFDLGVBQWUsQ0FBQztvQkFDcEYsQ0FBQyxDQUFDLENBQUM7Z0JBQ1AsQ0FBQyxDQUFDLENBQUM7WUFFUCxDQUFDO1NBQ0pBLENBQUNBO0lBQ05BLENBQUNBO0lBekJlLDRCQUFvQix1QkF5Qm5DLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtbGlzdC9kb2N1bWVudC1saXN0LW1lbnUtaXRlbS5kaXJlY3RpdmUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports", './documents.service', './results.service', './document-list-service.provider', './tasks-list-service.provider', './upload.controller', './list.controller', './document-list-tabset-item.directive', './document-list-menu-item.directive'], function (require, exports, documents_service_1, results_service_1, document_list_service_provider_1, tasks_list_service_provider_1, upload_controller_1, list_controller_1, document_list_tabset_item_directive_1, document_list_menu_item_directive_1) {
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(5)
            .setDocumentListUrl('api/UserDocuments/List');
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentList',
        requires: [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu',
            'ngFileUpload',
            'documentViewer',
            'debug'
        ],
        configFn: configure,
        factories: { resultsService: results_service_1.resultsService, documentsService: documents_service_1.documentsService },
        providers: { documentListService: document_list_service_provider_1.DocumentListServiceProvider, tasksListService: tasks_list_service_provider_1.tasksListServiceProvider },
        directives: { documentListTabsetItem: document_list_tabset_item_directive_1.documentListTabsetItem, documentListMenuItem: document_list_menu_item_directive_1.documentListMenuItem },
        controllers: { ListController: list_controller_1.ListController, UploadController: upload_controller_1.UploadController },
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QubW9kdWxlLnRzIl0sIm5hbWVzIjpbImNvbmZpZ3VyZSJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQVdiLFNBQVMsQ0FBQyxPQUFPLEdBQUcsQ0FBQywwQkFBMEIsRUFBRSw2QkFBNkIsQ0FBQyxDQUFDO0lBRWhGLG1CQUFtQix3QkFBa0QsRUFBRSwyQkFBc0Q7UUFDekhBLHdCQUF3QkE7YUFDbkJBLGlCQUFpQkEsQ0FBQ0EsR0FBR0EsQ0FBQ0E7YUFDdEJBLGVBQWVBLENBQUNBLFdBQVdBLENBQUNBLENBQUNBO1FBQ2xDQSwyQkFBMkJBO2FBQ3RCQSxzQkFBc0JBLENBQUNBLENBQUNBLENBQUNBO2FBQ3pCQSxrQkFBa0JBLENBQUNBLHdCQUF3QkEsQ0FBQ0EsQ0FBQ0E7SUFDdERBLENBQUNBO0lBRUQ7c0JBQWU7UUFDWCxJQUFJLEVBQUUsY0FBYztRQUNwQixRQUFRLEVBQUU7WUFDTixZQUFZO1lBQ1osY0FBYztZQUNkLDBCQUEwQjtZQUMxQixjQUFjO1lBQ2QsZ0JBQWdCO1lBQ2hCLE9BQU87U0FDVjtRQUNELFFBQVEsRUFBRSxTQUFTO1FBQ25CLFNBQVMsRUFBRSxFQUFFLGdCQUFBLGdDQUFjLEVBQUUsa0JBQUEsb0NBQWdCLEVBQUU7UUFDL0MsU0FBUyxFQUFFLEVBQUUsbUJBQW1CLEVBQUUsNERBQTJCLEVBQUUsZ0JBQWdCLEVBQUUsc0RBQXdCLEVBQUU7UUFDM0csVUFBVSxFQUFFLEVBQUUsd0JBQUEsNERBQXNCLEVBQUUsc0JBQUEsd0RBQW9CLEVBQUU7UUFDNUQsV0FBVyxFQUFFLEVBQUUsZ0JBQUEsZ0NBQWMsRUFBRSxrQkFBQSxvQ0FBZ0IsRUFBRTtLQUNwRCxDQUFBIiwiZmlsZSI6ImRvY3VtZW50LWxpc3QvZG9jdW1lbnQtbGlzdC5tb2R1bGUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    var DocumentController = (function () {
        function DocumentController($q, documentModelService) {
            this.$q = $q;
            this.documentModelService = documentModelService;
        }
        DocumentController.prototype.processDocument = function (id) {
            if (this.documentModel.id !== id) {
                return this.documentModelService.processDocument(id);
            }
            else {
                return this.$q.reject(this.documentModel);
            }
        };
        DocumentController.$inject = ['$q', 'MockDocumentModelService'];
        return DocumentController;
    })();
    exports.DocumentController = DocumentController;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQuY29udHJvbGxlci50cyJdLCJuYW1lcyI6WyJEb2N1bWVudENvbnRyb2xsZXIiLCJEb2N1bWVudENvbnRyb2xsZXIuY29uc3RydWN0b3IiLCJEb2N1bWVudENvbnRyb2xsZXIucHJvY2Vzc0RvY3VtZW50Il0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBR2I7UUFNSUEsNEJBQW9CQSxFQUFxQkEsRUFDN0JBLG9CQUEwQ0E7WUFEbENDLE9BQUVBLEdBQUZBLEVBQUVBLENBQW1CQTtZQUM3QkEseUJBQW9CQSxHQUFwQkEsb0JBQW9CQSxDQUFzQkE7UUFDdERBLENBQUNBO1FBQ0RELDRDQUFlQSxHQUFmQSxVQUFnQkEsRUFBVUE7WUFDdEJFLEVBQUVBLENBQUNBLENBQUNBLElBQUlBLENBQUNBLGFBQWFBLENBQUNBLEVBQUVBLEtBQUtBLEVBQUVBLENBQUNBLENBQUNBLENBQUNBO2dCQUMvQkEsTUFBTUEsQ0FBQ0EsSUFBSUEsQ0FBQ0Esb0JBQW9CQSxDQUFDQSxlQUFlQSxDQUFDQSxFQUFFQSxDQUFDQSxDQUFDQTtZQUN6REEsQ0FBQ0E7WUFBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0E7Z0JBQ0pBLE1BQU1BLENBQUNBLElBQUlBLENBQUNBLEVBQUVBLENBQUNBLE1BQU1BLENBQUNBLElBQUlBLENBQUNBLGFBQWFBLENBQUNBLENBQUNBO1lBQzlDQSxDQUFDQTtRQUNMQSxDQUFDQTtRQWRNRiwwQkFBT0EsR0FBR0EsQ0FBQ0EsSUFBSUEsRUFBRUEsMEJBQTBCQSxDQUFDQSxDQUFDQTtRQWV4REEseUJBQUNBO0lBQURBLENBaEJBLEFBZ0JDQSxJQUFBO0lBaEJZLDBCQUFrQixxQkFnQjlCLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LmNvbnRyb2xsZXIuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports", './document.controller', './document-model.service', './lexical-menu-builder.service'], function (require, exports, document_controller_1, document_model_service_1, lexical_menu_builder_service_1) {
    var module = {
        name: 'documentViewer',
        requires: [
            'documentViewer.search',
            'widgets',
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu'
        ],
        controllers: { DocumentController: document_controller_1.DocumentController },
        factories: { documentModelService: document_model_service_1.documentModelService, lexicalMenuBuilder: lexical_menu_builder_service_1.lexicalMenuBuilder }
    };
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = module;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtdmlld2VyLm1vZHVsZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBS2IsSUFBSSxNQUFNLEdBQXlCO1FBQy9CLElBQUksRUFBRSxnQkFBZ0I7UUFDdEIsUUFBUSxFQUFFO1lBQ04sdUJBQXVCO1lBQ3ZCLFNBQVM7WUFDVCxZQUFZO1lBQ1osY0FBYztZQUNkLDBCQUEwQjtTQUM3QjtRQUNELFdBQVcsRUFBRSxFQUFFLG9CQUFBLHdDQUFrQixFQUFFO1FBQ25DLFNBQVMsRUFBRSxFQUFFLHNCQUFBLDZDQUFvQixFQUFFLG9CQUFBLGlEQUFrQixFQUFFO0tBQzFELENBQUM7SUFDRjtzQkFBZSxNQUFNLENBQUMiLCJmaWxlIjoiZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LXZpZXdlci5tb2R1bGUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

'use strict';
define(["require", "exports"], function (require, exports) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'widgets',
        requires: ['ui.bootstrap']
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC93aWRnZXRzL3dpZGdldHMubW9kdWxlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLFlBQVksQ0FBQzs7SUFFYjtzQkFBZTtRQUNYLElBQUksRUFBRSxTQUFTO1FBQ2YsUUFBUSxFQUFFLENBQUMsY0FBYyxDQUFDO0tBQzdCLENBQUEiLCJmaWxlIjoid2lkZ2V0cy93aWRnZXRzLm1vZHVsZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    var template = require('app/document-viewer/search/search-box.directive.html!html');
    var SearchBoxController = (function () {
        function SearchBoxController($q) {
            this.$q = $q;
        }
        SearchBoxController.prototype.getWords = function () {
            return (this.phrases || []).flatMap(function (p) { return p.words; });
        };
        SearchBoxController.prototype.search = function (searchOptions, searchContext) {
            var deferred = this.$q.defer();
            var value = searchOptions.value;
            var term = typeof value === 'string' ? value :
                typeof value !== 'undefined' ? value.detailText : undefined;
            if (!term) {
                deferred.reject('search term was undefined');
            }
            else if (!searchContext) {
                deferred.reject('nothing to search');
                this.phrases.forEach(function (phrase) { return phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''); });
            }
            else {
                this.phrases = this.phrases || searchContext.flatMap(function (m) { return m.paragraphs; }).flatMap(function (p) { return p.sentences; }).flatMap(function (s) { return s.phrases; });
                var results = [];
                this.phrases.forEach(function (phrase) {
                    var matched = phrase.words.some(function (word) { return word.text === value; });
                    if (!matched) {
                        phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
                    }
                    else {
                        phrase.style.cssClass += ' matched-by-search';
                        results.push(phrase);
                    }
                });
                deferred.resolve(results.map(function (r) { return r.text; }));
            }
            return deferred.promise;
        };
        SearchBoxController.$inject = ['$q'];
        return SearchBoxController;
    })();
    function searchBox() {
        return {
            restrict: 'E',
            controller: SearchBoxController,
            controllerAs: 'search',
            scope: {},
            bindToController: {
                searchContext: '=',
                find: '='
            },
            template: template
        };
    }
    exports.searchBox = searchBox;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvc2VhcmNoL3NlYXJjaC1ib3guZGlyZWN0aXZlLnRzIl0sIm5hbWVzIjpbIlNlYXJjaEJveENvbnRyb2xsZXIiLCJTZWFyY2hCb3hDb250cm9sbGVyLmNvbnN0cnVjdG9yIiwiU2VhcmNoQm94Q29udHJvbGxlci5nZXRXb3JkcyIsIlNlYXJjaEJveENvbnRyb2xsZXIuc2VhcmNoIiwic2VhcmNoQm94Il0sIm1hcHBpbmdzIjoiQUFBQSxZQUFZLENBQUM7O0lBRWIsSUFBSSxRQUFRLEdBQUcsT0FBTyxDQUFDLDJEQUEyRCxDQUFDLENBQUM7SUFFcEY7UUFFSUEsNkJBQW9CQSxFQUFxQkE7WUFBckJDLE9BQUVBLEdBQUZBLEVBQUVBLENBQW1CQTtRQUFJQSxDQUFDQTtRQUk5Q0Qsc0NBQVFBLEdBQVJBO1lBQ0lFLE1BQU1BLENBQUNBLENBQUNBLElBQUlBLENBQUNBLE9BQU9BLElBQUlBLEVBQUVBLENBQUNBLENBQUNBLE9BQU9BLENBQUNBLFVBQUFBLENBQUNBLElBQUlBLE9BQUFBLENBQUNBLENBQUNBLEtBQUtBLEVBQVBBLENBQU9BLENBQUNBLENBQUNBO1FBQ3REQSxDQUFDQTtRQUlERixvQ0FBTUEsR0FBTkEsVUFBT0EsYUFBNEJBLEVBQUVBLGFBQWtDQTtZQUNuRUcsSUFBSUEsUUFBUUEsR0FBR0EsSUFBSUEsQ0FBQ0EsRUFBRUEsQ0FBQ0EsS0FBS0EsRUFBaUJBLENBQUNBO1lBQzlDQSxJQUFJQSxLQUFLQSxHQUFHQSxhQUFhQSxDQUFDQSxLQUFLQSxDQUFDQTtZQUNoQ0EsSUFBTUEsSUFBSUEsR0FDTkEsT0FBT0EsS0FBS0EsS0FBS0EsUUFBUUEsR0FBR0EsS0FBS0E7Z0JBQzdCQSxPQUFPQSxLQUFLQSxLQUFLQSxXQUFXQSxHQUFHQSxLQUFLQSxDQUFDQSxVQUFVQSxHQUFHQSxTQUFTQSxDQUFDQTtZQUNwRUEsRUFBRUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7Z0JBQ1JBLFFBQVFBLENBQUNBLE1BQU1BLENBQUNBLDJCQUEyQkEsQ0FBQ0EsQ0FBQ0E7WUFDakRBLENBQUNBO1lBQUNBLElBQUlBLENBQUNBLEVBQUVBLENBQUNBLENBQUNBLENBQUNBLGFBQWFBLENBQUNBLENBQUNBLENBQUNBO2dCQUN4QkEsUUFBUUEsQ0FBQ0EsTUFBTUEsQ0FBQ0EsbUJBQW1CQSxDQUFDQSxDQUFDQTtnQkFDckNBLElBQUlBLENBQUNBLE9BQU9BLENBQUNBLE9BQU9BLENBQUNBLFVBQUFBLE1BQU1BLElBQUlBLE9BQUFBLE1BQU1BLENBQUNBLEtBQUtBLENBQUNBLFFBQVFBLEdBQUdBLE1BQU1BLENBQUNBLEtBQUtBLENBQUNBLFFBQVFBLENBQUNBLE9BQU9BLENBQUNBLG1CQUFtQkEsRUFBRUEsRUFBRUEsQ0FBQ0EsRUFBOUVBLENBQThFQSxDQUFDQSxDQUFDQTtZQUNuSEEsQ0FBQ0E7WUFBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0E7Z0JBQ0pBLElBQUlBLENBQUNBLE9BQU9BLEdBQUdBLElBQUlBLENBQUNBLE9BQU9BLElBQUlBLGFBQWFBLENBQUNBLE9BQU9BLENBQUNBLFVBQUFBLENBQUNBLElBQUlBLE9BQUFBLENBQUNBLENBQUNBLFVBQVVBLEVBQVpBLENBQVlBLENBQUNBLENBQUNBLE9BQU9BLENBQUNBLFVBQUFBLENBQUNBLElBQUlBLE9BQUFBLENBQUNBLENBQUNBLFNBQVNBLEVBQVhBLENBQVdBLENBQUNBLENBQUNBLE9BQU9BLENBQUNBLFVBQUFBLENBQUNBLElBQUlBLE9BQUFBLENBQUNBLENBQUNBLE9BQU9BLEVBQVRBLENBQVNBLENBQUNBLENBQUNBO2dCQUUxSEEsSUFBSUEsT0FBT0EsR0FBa0JBLEVBQUVBLENBQUNBO2dCQUNoQ0EsSUFBSUEsQ0FBQ0EsT0FBT0EsQ0FBQ0EsT0FBT0EsQ0FBQ0EsVUFBQUEsTUFBTUE7b0JBQ3ZCQSxJQUFJQSxPQUFPQSxHQUFHQSxNQUFNQSxDQUFDQSxLQUFLQSxDQUFDQSxJQUFJQSxDQUFDQSxVQUFBQSxJQUFJQSxJQUFJQSxPQUFBQSxJQUFJQSxDQUFDQSxJQUFJQSxLQUFLQSxLQUFLQSxFQUFuQkEsQ0FBbUJBLENBQUNBLENBQUNBO29CQUM3REEsRUFBRUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsT0FBT0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7d0JBQ1hBLE1BQU1BLENBQUNBLEtBQUtBLENBQUNBLFFBQVFBLEdBQUdBLE1BQU1BLENBQUNBLEtBQUtBLENBQUNBLFFBQVFBLENBQUNBLE9BQU9BLENBQUNBLG1CQUFtQkEsRUFBRUEsRUFBRUEsQ0FBQ0EsQ0FBQ0E7b0JBQ25GQSxDQUFDQTtvQkFBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0E7d0JBQ0pBLE1BQU1BLENBQUNBLEtBQUtBLENBQUNBLFFBQVFBLElBQUlBLG9CQUFvQkEsQ0FBQ0E7d0JBQzlDQSxPQUFPQSxDQUFDQSxJQUFJQSxDQUFDQSxNQUFNQSxDQUFDQSxDQUFDQTtvQkFDekJBLENBQUNBO2dCQUNMQSxDQUFDQSxDQUFDQSxDQUFDQTtnQkFDSEEsUUFBUUEsQ0FBQ0EsT0FBT0EsQ0FBQ0EsT0FBT0EsQ0FBQ0EsR0FBR0EsQ0FBQ0EsVUFBQUEsQ0FBQ0EsSUFBSUEsT0FBQUEsQ0FBQ0EsQ0FBQ0EsSUFBSUEsRUFBTkEsQ0FBTUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7WUFDL0NBLENBQUNBO1lBQ0RBLE1BQU1BLENBQUNBLFFBQVFBLENBQUNBLE9BQU9BLENBQUNBO1FBQzVCQSxDQUFDQTtRQXRDTUgsMkJBQU9BLEdBQUdBLENBQUNBLElBQUlBLENBQUNBLENBQUNBO1FBdUM1QkEsMEJBQUNBO0lBQURBLENBeENBLEFBd0NDQSxJQUFBO0lBQ0Q7UUFDSUksTUFBTUEsQ0FBQ0E7WUFDSEEsUUFBUUEsRUFBRUEsR0FBR0E7WUFDYkEsVUFBVUEsRUFBRUEsbUJBQW1CQTtZQUMvQkEsWUFBWUEsRUFBRUEsUUFBUUE7WUFDdEJBLEtBQUtBLEVBQUVBLEVBQUVBO1lBQ1RBLGdCQUFnQkEsRUFBRUE7Z0JBQ2RBLGFBQWFBLEVBQUVBLEdBQUdBO2dCQUNsQkEsSUFBSUEsRUFBRUEsR0FBR0E7YUFDWkE7WUFDREEsUUFBUUEsRUFBRUEsUUFBUUE7U0FDckJBLENBQUNBO0lBQ05BLENBQUNBO0lBWmUsaUJBQVMsWUFZeEIsQ0FBQSIsImZpbGUiOiJkb2N1bWVudC12aWV3ZXIvc2VhcmNoL3NlYXJjaC1ib3guZGlyZWN0aXZlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports", './search-box.directive'], function (require, exports, search_box_directive_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentViewer.search',
        requires: ['ui.bootstrap.typeahead'],
        directives: { searchBox: search_box_directive_1.searchBox }
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvc2VhcmNoL3NlYXJjaC5tb2R1bGUudHMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUliO3NCQUFlO1FBQ1gsSUFBSSxFQUFFLHVCQUF1QjtRQUM3QixRQUFRLEVBQUUsQ0FBQyx3QkFBd0IsQ0FBQztRQUNwQyxVQUFVLEVBQUUsRUFBRSxXQUFBLGdDQUFTLEVBQUU7S0FDNUIsQ0FBQSIsImZpbGUiOiJkb2N1bWVudC12aWV3ZXIvc2VhcmNoL3NlYXJjaC5tb2R1bGUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==



//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJhbmd1bGFyLW1vZHVsZS11dGlscy5qcyIsInNvdXJjZXNDb250ZW50IjpbXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

/// <reference path = '../../typings/angular-bootstrap-contextMenu/angular-bootstrap-contextmenu.d.ts'/>
var contextmenuTests;
(function (contextmenuTests) {
    'use strict';
    var item1 = [
        function (s, e) { return 'item1'; },
        function (s, e) { return console.log('item1 clicked'); },
        function (s, e) { return true; }
    ];
    var item2 = [
        'item2',
        function (s) { return console.log(s); }
    ];
    var item3 = [
        'item3',
        function (s, e) { return console.log('item3 clicked'); },
        function (s, e) { return Object.keys(s).every(function (k1) { return Object.keys(e).some(function (k2) { return k1 === k2; }); }); }
    ];
    var menu = [
        item1, item2
    ];
})(contextmenuTests || (contextmenuTests = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9jb250ZXh0bWVudS1kZWZpbml0aW9ucy10ZXN0cy50cyJdLCJuYW1lcyI6WyJjb250ZXh0bWVudVRlc3RzIl0sIm1hcHBpbmdzIjoiQUFBQSx3R0FBd0c7QUFDeEcsSUFBTyxnQkFBZ0IsQ0EyQnRCO0FBM0JELFdBQU8sZ0JBQWdCLEVBQUMsQ0FBQztJQUNyQkEsWUFBWUEsQ0FBQ0E7SUFTYkEsSUFBSUEsS0FBS0EsR0FBU0E7UUFDZEEsVUFBQ0EsQ0FBb0JBLEVBQUVBLENBQW9CQSxJQUFLQSxPQUFBQSxPQUFPQSxFQUFQQSxDQUFPQTtRQUN2REEsVUFBQ0EsQ0FBb0JBLEVBQUVBLENBQW9CQSxJQUFLQSxPQUFBQSxPQUFPQSxDQUFDQSxHQUFHQSxDQUFDQSxlQUFlQSxDQUFDQSxFQUE1QkEsQ0FBNEJBO1FBQzVFQSxVQUFDQSxDQUFvQkEsRUFBRUEsQ0FBb0JBLElBQUtBLE9BQUFBLElBQUlBLEVBQUpBLENBQUlBO0tBQ3ZEQSxDQUFDQTtJQUNGQSxJQUFJQSxLQUFLQSxHQUFTQTtRQUNkQSxPQUFPQTtRQUNQQSxVQUFDQSxDQUFDQSxJQUFLQSxPQUFBQSxPQUFPQSxDQUFDQSxHQUFHQSxDQUFDQSxDQUFDQSxDQUFDQSxFQUFkQSxDQUFjQTtLQUN4QkEsQ0FBQ0E7SUFDRkEsSUFBSUEsS0FBS0EsR0FBU0E7UUFDZEEsT0FBT0E7UUFDUEEsVUFBQ0EsQ0FBb0JBLEVBQUVBLENBQW9CQSxJQUFLQSxPQUFBQSxPQUFPQSxDQUFDQSxHQUFHQSxDQUFDQSxlQUFlQSxDQUFDQSxFQUE1QkEsQ0FBNEJBO1FBQzVFQSxVQUFDQSxDQUFDQSxFQUFFQSxDQUFDQSxJQUFLQSxPQUFBQSxNQUFNQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQSxDQUFDQSxDQUFDQSxLQUFLQSxDQUFDQSxVQUFBQSxFQUFFQSxJQUFJQSxPQUFBQSxNQUFNQSxDQUFDQSxJQUFJQSxDQUFDQSxDQUFDQSxDQUFDQSxDQUFDQSxJQUFJQSxDQUFDQSxVQUFBQSxFQUFFQSxJQUFJQSxPQUFBQSxFQUFFQSxLQUFLQSxFQUFFQSxFQUFUQSxDQUFTQSxDQUFDQSxFQUFwQ0EsQ0FBb0NBLENBQUNBLEVBQWhFQSxDQUFnRUE7S0FDN0VBLENBQUNBO0lBQ0ZBLElBQUlBLElBQUlBLEdBQVdBO1FBQ2ZBLEtBQUtBLEVBQUVBLEtBQUtBO0tBQ2ZBLENBQUNBO0FBQ05BLENBQUNBLEVBM0JNLGdCQUFnQixLQUFoQixnQkFBZ0IsUUEyQnRCIiwiZmlsZSI6ImNvbnRleHRtZW51LWRlZmluaXRpb25zLXRlc3RzLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

/// <reference path="../../../typings/jquery/jquery.d.ts" />
var LASI;
(function (LASI) {
    var accountManagement;
    (function (accountManagement) {
        'use strict';
        $(function () { return $('#cancel-profile-edits').click(function () { return document.location.href = "http://" + document.domain + ":" + document.location.port; }); });
    })(accountManagement = LASI.accountManagement || (LASI.accountManagement = {}));
})(LASI || (LASI = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9hY2NvdW50L21hbmFnZS50cyJdLCJuYW1lcyI6WyJMQVNJIiwiTEFTSS5hY2NvdW50TWFuYWdlbWVudCJdLCJtYXBwaW5ncyI6IkFBQUEsNERBQTREO0FBQzVELElBQVUsSUFBSSxDQUdiO0FBSEQsV0FBVSxJQUFJO0lBQUNBLElBQUFBLGlCQUFpQkEsQ0FHL0JBO0lBSGNBLFdBQUFBLGlCQUFpQkEsRUFBQ0EsQ0FBQ0E7UUFDOUJDLFlBQVlBLENBQUNBO1FBQ2JBLENBQUNBLENBQUNBLGNBQU1BLE9BQUFBLENBQUNBLENBQUNBLHVCQUF1QkEsQ0FBQ0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsY0FBTUEsT0FBQUEsUUFBUUEsQ0FBQ0EsUUFBUUEsQ0FBQ0EsSUFBSUEsR0FBR0EsWUFBVUEsUUFBUUEsQ0FBQ0EsTUFBTUEsU0FBSUEsUUFBUUEsQ0FBQ0EsUUFBUUEsQ0FBQ0EsSUFBTUEsRUFBOUVBLENBQThFQSxDQUFDQSxFQUF0SEEsQ0FBc0hBLENBQUNBLENBQUNBO0lBQ3BJQSxDQUFDQSxFQUhjRCxpQkFBaUJBLEdBQWpCQSxzQkFBaUJBLEtBQWpCQSxzQkFBaUJBLFFBRy9CQTtBQUFEQSxDQUFDQSxFQUhTLElBQUksS0FBSixJQUFJLFFBR2IiLCJmaWxlIjoiYWNjb3VudC9tYW5hZ2UuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==



//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QtaXRlbS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFlQyIsImZpbGUiOiJkb2N1bWVudC1saXN0L2RvY3VtZW50LWxpc3QtaXRlbS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    function documentPage() {
        function link(scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        }
        return {
            restrict: 'E',
            link: link,
            template: require('/app/document-viewer/document-page.directive.html!'),
            replace: true,
            scope: {
                page: '=',
                document: '='
            }
        };
    }
    exports.documentPage = documentPage;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtcGFnZS5kaXJlY3RpdmUudHMiXSwibmFtZXMiOlsiZG9jdW1lbnRQYWdlIiwiZG9jdW1lbnRQYWdlLmxpbmsiXSwibWFwcGluZ3MiOiJBQUFBLFlBQVksQ0FBQzs7SUFFYjtRQUNJQSxjQUFjQSxLQUFxQkEsRUFBRUEsT0FBaUNBLEVBQUVBLEtBQTBCQTtZQUM5RkMsT0FBT0EsQ0FBQ0EsR0FBR0EsQ0FBQ0EsS0FBS0EsQ0FBQ0EsQ0FBQ0E7WUFDbkJBLE9BQU9BLENBQUNBLEdBQUdBLENBQUNBLE9BQU9BLENBQUNBLENBQUNBO1lBQ3JCQSxPQUFPQSxDQUFDQSxHQUFHQSxDQUFDQSxLQUFLQSxDQUFDQSxDQUFDQTtRQUN2QkEsQ0FBQ0E7UUFDREQsTUFBTUEsQ0FBQ0E7WUFDSEEsUUFBUUEsRUFBRUEsR0FBR0E7WUFDYkEsSUFBSUEsRUFBRUEsSUFBSUE7WUFDVkEsUUFBUUEsRUFBRUEsT0FBT0EsQ0FBQ0Esb0RBQW9EQSxDQUFDQTtZQUN2RUEsT0FBT0EsRUFBRUEsSUFBSUE7WUFDYkEsS0FBS0EsRUFBRUE7Z0JBQ0hBLElBQUlBLEVBQUVBLEdBQUdBO2dCQUNUQSxRQUFRQSxFQUFFQSxHQUFHQTthQUNoQkE7U0FDSkEsQ0FBQ0E7SUFDTkEsQ0FBQ0E7SUFoQmUsb0JBQVksZUFnQjNCLENBQUEiLCJmaWxlIjoiZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LXBhZ2UuZGlyZWN0aXZlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

'use strict';
define(["require", "exports"], function (require, exports) {
    documentViewerDirective.$inject = [];
    function documentViewerDirective() {
        var link = function (scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        };
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/document-viewer.directive.html',
            replace: true,
            scope: {
                document: '='
            },
            link: link
        };
    }
    exports.documentViewerDirective = documentViewerDirective;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvZG9jdW1lbnQtdmlld2VyLmRpcmVjdGl2ZS50cyJdLCJuYW1lcyI6WyJkb2N1bWVudFZpZXdlckRpcmVjdGl2ZSJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUViLHVCQUF1QixDQUFDLE9BQU8sR0FBRyxFQUFFLENBQUM7SUFDckM7UUFFSUEsSUFBSUEsSUFBSUEsR0FBNkJBLFVBQVVBLEtBQXFCQSxFQUFFQSxPQUFpQ0EsRUFBRUEsS0FBMEJBO1lBQy9ILE9BQU8sQ0FBQyxHQUFHLENBQUMsS0FBSyxDQUFDLENBQUM7WUFDbkIsT0FBTyxDQUFDLEdBQUcsQ0FBQyxPQUFPLENBQUMsQ0FBQztZQUNyQixPQUFPLENBQUMsR0FBRyxDQUFDLEtBQUssQ0FBQyxDQUFDO1FBQ3ZCLENBQUMsQ0FBQ0E7UUFDRkEsTUFBTUEsQ0FBQ0E7WUFDSEEsUUFBUUEsRUFBRUEsR0FBR0E7WUFDYkEsV0FBV0EsRUFBRUEscURBQXFEQTtZQUNsRUEsT0FBT0EsRUFBRUEsSUFBSUE7WUFDYkEsS0FBS0EsRUFBRUE7Z0JBQ0hBLFFBQVFBLEVBQUVBLEdBQUdBO2FBQ2hCQTtZQUNEQSxNQUFBQSxJQUFJQTtTQUNQQSxDQUFDQTtJQUNOQSxDQUFDQTtJQWhCZSwrQkFBdUIsMEJBZ0J0QyxDQUFBIiwiZmlsZSI6ImRvY3VtZW50LXZpZXdlci9kb2N1bWVudC12aWV3ZXIuZGlyZWN0aXZlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/paragraph.directive.html',
                scope: {
                    paragraph: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive({ paragraph: paragraph });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvcGFyYWdyYXBoLmRpcmVjdGl2ZS50cyJdLCJuYW1lcyI6WyJMQVNJIiwiTEFTSS5kb2N1bWVudFZpZXdlciIsIkxBU0kuZG9jdW1lbnRWaWV3ZXIucGFyYWdyYXBoIl0sIm1hcHBpbmdzIjoiQUFBQSxJQUFVLElBQUksQ0FrQmI7QUFsQkQsV0FBVSxJQUFJO0lBQUNBLElBQUFBLGNBQWNBLENBa0I1QkE7SUFsQmNBLFdBQUFBLGNBQWNBLEVBQUNBLENBQUNBO1FBQzNCQyxZQUFZQSxDQUFDQTtRQUViQSxTQUFTQSxDQUFDQSxPQUFPQSxHQUFHQSxDQUFDQSxTQUFTQSxDQUFDQSxDQUFDQTtRQUNoQ0EsbUJBQW1CQSxPQUErQkE7WUFDOUNDLE1BQU1BLENBQUNBO2dCQUNIQSxRQUFRQSxFQUFFQSxHQUFHQTtnQkFDYkEsV0FBV0EsRUFBRUEsK0NBQStDQTtnQkFDNURBLEtBQUtBLEVBQUVBO29CQUNIQSxTQUFTQSxFQUFFQSxHQUFHQTtvQkFDZEEsUUFBUUEsRUFBRUEsR0FBR0E7aUJBQ2hCQTthQUNKQSxDQUFDQTtRQUVOQSxDQUFDQTtRQUNERCxPQUFPQTthQUNGQSxNQUFNQSxDQUFDQSxnQkFBZ0JBLENBQUNBO2FBQ3hCQSxTQUFTQSxDQUFDQSxFQUFFQSxXQUFBQSxTQUFTQSxFQUFFQSxDQUFDQSxDQUFDQTtJQUNsQ0EsQ0FBQ0EsRUFsQmNELGNBQWNBLEdBQWRBLG1CQUFjQSxLQUFkQSxtQkFBY0EsUUFrQjVCQTtBQUFEQSxDQUFDQSxFQWxCUyxJQUFJLEtBQUosSUFBSSxRQWtCYiIsImZpbGUiOiJkb2N1bWVudC12aWV3ZXIvcGFyYWdyYXBoLmRpcmVjdGl2ZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

'use strict';
define(["require", "exports"], function (require, exports) {
    phrase.$inject = ['lexicalMenuBuilder'];
    function phrase(lexicalMenuBuilder) {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/phrase.directive.html',
            scope: {
                phrase: '=',
                parentId: '='
            },
            link: link
        };
        function link(scope, element, attrs) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                scope.phrase.contextmenu = contextmenu;
            }
        }
    }
    exports.phrase = phrase;
    angular
        .module('documentViewer')
        .directive({ phrase: phrase });
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvcGhyYXNlLmRpcmVjdGl2ZS50cyJdLCJuYW1lcyI6WyJwaHJhc2UiLCJwaHJhc2UubGluayJdLCJtYXBwaW5ncyI6IkFBQUEsWUFBWSxDQUFDOztJQUdiLE1BQU0sQ0FBQyxPQUFPLEdBQUcsQ0FBQyxvQkFBb0IsQ0FBQyxDQUFDO0lBRXhDLGdCQUF1QixrQkFBNkM7UUFDaEVBLE1BQU1BLENBQUNBO1lBQ0hBLFFBQVFBLEVBQUVBLEdBQUdBO1lBQ2JBLFdBQVdBLEVBQUVBLDRDQUE0Q0E7WUFDekRBLEtBQUtBLEVBQUVBO2dCQUNIQSxNQUFNQSxFQUFFQSxHQUFHQTtnQkFDWEEsUUFBUUEsRUFBRUEsR0FBR0E7YUFDaEJBO1lBQ0RBLE1BQUFBLElBQUlBO1NBQ1BBLENBQUNBO1FBRUZBLGNBQWNBLEtBQWtCQSxFQUFFQSxPQUFpQ0EsRUFBRUEsS0FBMEJBO1lBQzNGQyxJQUFJQSxXQUFXQSxHQUFHQSxrQkFBa0JBLENBQUNBLGdCQUFnQkEsQ0FBQ0EsS0FBS0EsQ0FBQ0EsTUFBTUEsQ0FBQ0EsV0FBV0EsQ0FBQ0EsQ0FBQ0E7WUFDaEZBLEtBQUtBLENBQUNBLE1BQU1BLENBQUNBLGtCQUFrQkEsR0FBR0EsQ0FBQ0EsQ0FBQ0EsV0FBV0EsQ0FBQ0E7WUFDaERBLEVBQUVBLENBQUNBLENBQUNBLEtBQUtBLENBQUNBLE1BQU1BLENBQUNBLGtCQUFrQkEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7Z0JBQzVCQSxLQUFLQSxDQUFDQSxNQUFPQSxDQUFDQSxXQUFXQSxHQUFHQSxXQUFXQSxDQUFDQTtZQUNsREEsQ0FBQ0E7UUFDTEEsQ0FBQ0E7SUFDTEQsQ0FBQ0E7SUFsQmUsY0FBTSxTQWtCckIsQ0FBQTtJQVNELE9BQU87U0FDRixNQUFNLENBQUMsZ0JBQWdCLENBQUM7U0FDeEIsU0FBUyxDQUFDLEVBQUUsUUFBQSxNQUFNLEVBQUUsQ0FBQyxDQUFDIiwiZmlsZSI6ImRvY3VtZW50LXZpZXdlci9waHJhc2UuZGlyZWN0aXZlLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.directive.html',
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive({ sentence: sentence });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvc2VudGVuY2UuZGlyZWN0aXZlLnRzIl0sIm5hbWVzIjpbIkxBU0kiLCJMQVNJLmRvY3VtZW50Vmlld2VyIiwiTEFTSS5kb2N1bWVudFZpZXdlci5zZW50ZW5jZSJdLCJtYXBwaW5ncyI6IkFBQUEsSUFBVSxJQUFJLENBaUJiO0FBakJELFdBQVUsSUFBSTtJQUFDQSxJQUFBQSxjQUFjQSxDQWlCNUJBO0lBakJjQSxXQUFBQSxjQUFjQSxFQUFDQSxDQUFDQTtRQUMzQkMsWUFBWUEsQ0FBQ0E7UUFFYkE7WUFDSUMsTUFBTUEsQ0FBQ0E7Z0JBQ0hBLFFBQVFBLEVBQUVBLEdBQUdBO2dCQUNiQSxXQUFXQSxFQUFFQSw4Q0FBOENBO2dCQUMzREEsS0FBS0EsRUFBRUE7b0JBQ0hBLFFBQVFBLEVBQUVBLEdBQUdBO29CQUNiQSxRQUFRQSxFQUFFQSxHQUFHQTtpQkFDaEJBO2FBQ0pBLENBQUNBO1FBQ05BLENBQUNBO1FBRURELE9BQU9BO2FBQ0ZBLE1BQU1BLENBQUNBLGdCQUFnQkEsQ0FBQ0E7YUFDeEJBLFNBQVNBLENBQUNBLEVBQUVBLFVBQUFBLFFBQVFBLEVBQUVBLENBQUNBLENBQUNBO0lBQ2pDQSxDQUFDQSxFQWpCY0QsY0FBY0EsR0FBZEEsbUJBQWNBLEtBQWRBLG1CQUFjQSxRQWlCNUJBO0FBQURBLENBQUNBLEVBakJTLElBQUksS0FBSixJQUFJLFFBaUJiIiwiZmlsZSI6ImRvY3VtZW50LXZpZXdlci9zZW50ZW5jZS5kaXJlY3RpdmUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../lasi.ts"/>
(function (app) {
    'use strict';
    app.buildMenus = (function (contextualElementIdSelectors) {
        var verbalMenuTextToElementsMap = {
            'View Subjects': 'subjects',
            'View Direct Objects': 'directObjects',
            'View Indirect Objects': 'indirectObjects'
        };
        var relationshipCssClassNameMap = {
            'View Subjects': 'subject-of-current',
            'View Direct Objects': 'direct-object-of-current',
            'View Indirect Objects': 'indirect-object-of-current'
        };
        return function () {
            var forVerbal = function (context) {
                var menu = JSON.parse($('#context' + context[0].id).text());
                var subjects = menu.subjects, directObjects = menu.directObjects, indirectObjects = menu.indirectObjects;
                var temp = {
                    subjects: subjects,
                    directObjects: directObjects,
                    indirectObjects: indirectObjects
                }, result = {};
                Object.keys(temp)
                    .filter(function (key) { return temp[key]; })
                    .forEach(function (key) { result[key] = temp[key]; });
                return result;
            }, forReferencer = function (context) {
                var menu = JSON.parse($('#context' + context[0].id).text());
                return menu;
            };
            $('span.referencer').contextmenu({
                target: '#referencer-context-menu',
                before: function (event, context) {
                    var data = forReferencer(context);
                    event.target.lexicalContextMenu = data;
                    return data.referredTo && data.referredTo.length > 0;
                },
                onItem: function (context) {
                    context[0].lexicalContextMenu.referredTo.map(function (id) {
                        return $('#' + id);
                    }).forEach(function (element) {
                        element.css('background-color', 'red');
                    });
                }
            });
            $('span.verbal').contextmenu({
                target: '#verbal-context-menu',
                before: function (e, context) {
                    var count = 0;
                    var menu = forVerbal(context);
                    e.target.lexicalContextMenu = {};
                    Object.keys(menu).forEach(function (key) {
                        e.target.lexicalContextMenu[key] = menu[key].map(function (id) {
                            var idSelector = '#' + id;
                            if (!contextualElementIdSelectors.some(function (e) { return e === idSelector; })) {
                                contextualElementIdSelectors.push(idSelector);
                            }
                            return idSelector;
                        });
                    });
                    [
                        { name: 'subjects', id: '#subjects-item' },
                        { name: 'directObjects', id: '#directobjects-item' },
                        { name: 'indirectObjects', id: '#indirectobjects-item' }
                    ].forEach(function (x) {
                        if (!menu[x.name]) {
                            $(x.id).hide();
                        }
                        else {
                            count += 1;
                            $(x.id).show();
                        }
                    });
                    return count > 0;
                },
                onItem: function (context, event) {
                    var menu = context[0].lexicalContextMenu;
                    contextualElementIdSelectors
                        .flatMap(function (e) { return $(e).toArray(); }, $)
                        .forEach(function (e) {
                        Object.keys(relationshipCssClassNameMap).forEach(function (key) {
                            e.removeClass(relationshipCssClassNameMap[key]);
                        });
                    });
                    menu[verbalMenuTextToElementsMap[event.target.text]]
                        .map($)
                        .forEach(function (e) {
                        e.addClass(relationshipCssClassNameMap[event.target.text]);
                    });
                }
            });
            $(document).on('click', function () {
                $('#verbal-context-menu').hide();
                $('#referencer-context-menu').hide();
            });
        };
    }([]));
    $(app.buildMenus);
}(LASI));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9yZXN1bHRzL2NvbnRleHQtbWVudS1wcm92aWRlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSwyREFBMkQ7QUFDM0Qsa0NBQWtDO0FBR2xDLENBQUMsVUFBVSxHQUFHO0lBQ1YsWUFBWSxDQUFDO0lBQ2IsR0FBRyxDQUFDLFVBQVUsR0FBRyxDQUFDLFVBQVUsNEJBQTRCO1FBQ3BELElBQUksMkJBQTJCLEdBQUc7WUFDOUIsZUFBZSxFQUFFLFVBQVU7WUFDM0IscUJBQXFCLEVBQUUsZUFBZTtZQUN0Qyx1QkFBdUIsRUFBRSxpQkFBaUI7U0FDN0MsQ0FBQztRQUNGLElBQUksMkJBQTJCLEdBQUc7WUFDOUIsZUFBZSxFQUFFLG9CQUFvQjtZQUNyQyxxQkFBcUIsRUFBRSwwQkFBMEI7WUFDakQsdUJBQXVCLEVBQUUsNEJBQTRCO1NBQ3hELENBQUM7UUFDRixNQUFNLENBQUM7WUFDSCxJQUFJLFNBQVMsR0FBRyxVQUFVLE9BQU87Z0JBRTdCLElBQUksSUFBSSxHQUFHLElBQUksQ0FBQyxLQUFLLENBQUMsQ0FBQyxDQUFDLFVBQVUsR0FBRyxPQUFPLENBQUMsQ0FBQyxDQUFDLENBQUMsRUFBRSxDQUFDLENBQUMsSUFBSSxFQUFFLENBQUMsQ0FBQztnQkFDNUQsSUFBSSxRQUFRLEdBQUcsSUFBSSxDQUFDLFFBQVEsRUFDeEIsYUFBYSxHQUFHLElBQUksQ0FBQyxhQUFhLEVBQ2xDLGVBQWUsR0FBRyxJQUFJLENBQUMsZUFBZSxDQUFDO2dCQUMzQyxJQUFJLElBQUksR0FBRztvQkFDUCxRQUFRLEVBQUUsUUFBUTtvQkFDbEIsYUFBYSxFQUFFLGFBQWE7b0JBQzVCLGVBQWUsRUFBRSxlQUFlO2lCQUNuQyxFQUFFLE1BQU0sR0FBRyxFQUFFLENBQUM7Z0JBQ2YsTUFBTSxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUM7cUJBQ1osTUFBTSxDQUFDLFVBQVUsR0FBRyxJQUFJLE1BQU0sQ0FBQyxJQUFJLENBQUMsR0FBRyxDQUFDLENBQUMsQ0FBQyxDQUFD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TUFBTSxDQUFDLElBQUksQ0FBQyxDQUFDLENBQUM7b0JBQy9ELENBQUMsQ0FBQyxDQUFDO2dCQUVQLENBQUM7YUFDSixDQUFDLENBQUM7WUFDSCxDQUFDLENBQUMsUUFBUSxDQUFDLENBQUMsRUFBRSxDQUFDLE9BQU8sRUFBRTtnQkFDcEIsQ0FBQyxDQUFDLHNCQUFzQixDQUFDLENBQUMsSUFBSSxFQUFFLENBQUM7Z0JBQ2pDLENBQUMsQ0FBQywwQkFBMEIsQ0FBQyxDQUFDLElBQUksRUFBRSxDQUFDO1lBQ3pDLENBQUMsQ0FBQyxDQUFDO1FBQ1AsQ0FBQyxDQUFDO0lBQ04sQ0FBQyxDQUFFLEVBQUUsQ0FBQyxDQUFDLENBQUM7SUFDUixDQUFDLENBQUMsR0FBRyxDQUFDLFVBQVUsQ0FBQyxDQUFDO0FBQ3RCLENBQUMsQ0FBRSxJQUFJLENBQUMsQ0FBQyxDQUFDIiwiZmlsZSI6InJlc3VsdHMvY29udGV4dC1tZW51LXByb3ZpZGVyLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

define(["require", "exports", 'app/LASI'], function (require, exports, LASI) {
    LASI.enableActiveHighlighting = function () {
        'use strict';
        var enableActiveHighlighting = function () {
            var phrasalTextSpans = $('span.phrase');
            var highlightClass = 'active-phrase-highlight';
            var recolor = function () {
                phrasalTextSpans.each(function () {
                    $(this).removeClass(highlightClass);
                });
                $(this).addClass(highlightClass);
            };
            phrasalTextSpans.click(recolor);
            phrasalTextSpans.on('contextmenu', recolor);
            // bootstrap requires that tooltips be manually enabled. The data-toggle="tooltip" attributes set on each element
            // have no effect without this or an equivalent call. By setting container to 'body', we allow the contents of the 
            // tooltip to overflow its container. This is generally close to the desired behavior as it is difficult to predict width
            // and this gives good flexibility. There is probably a cleaner and more precise/obvious way of doing this, change to that if discovered.
            $('[data-toggle="tooltip"]').tooltip({
                delay: 250,
                container: 'body'
            });
            // TODO: look into fixing tooltips on elements containing a line break or remove such breaks.
        };
        $(enableActiveHighlighting);
        return enableActiveHighlighting;
    };
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9yZXN1bHRzL3Jlc3VsdC1jaGFydC1wcm92aWRlci50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiO0lBRUEsSUFBSSxDQUFDLHdCQUF3QixHQUFHO1FBQzVCLFlBQVksQ0FBQztRQUNiLElBQUksd0JBQXdCLEdBQUc7WUFDM0IsSUFBSSxnQkFBZ0IsR0FBRyxDQUFDLENBQUMsYUFBYSxDQUFDLENBQUM7WUFDeEMsSUFBSSxjQUFjLEdBQUcseUJBQXlCLENBQUM7WUFDL0MsSUFBSSxPQUFPLEdBQUc7Z0JBQ1YsZ0JBQWdCLENBQUMsSUFBSSxDQUFDO29CQUNsQixDQUFDLENBQUMsSUFBSSxDQUFDLENBQUMsV0FBVyxDQUFDLGNBQWMsQ0FBQyxDQUFDO2dCQUN4QyxDQUFDLENBQUMsQ0FBQztnQkFDSCxDQUFDLENBQUMsSUFBSSxDQUFDLENBQUMsUUFBUSxDQUFDLGNBQWMsQ0FBQyxDQUFDO1lBQ3JDLENBQUMsQ0FBQztZQUNGLGdCQUFnQixDQUFDLEtBQUssQ0FBQyxPQUFPLENBQUMsQ0FBQztZQUNoQyxnQkFBZ0IsQ0FBQyxFQUFFLENBQUMsYUFBYSxFQUFFLE9BQU8sQ0FBQyxDQUFDO1lBRTVDLGlIQUFpSDtZQUNqSCxtSEFBbUg7WUFDbkgseUhBQXlIO1lBQ3pILHlJQUF5STtZQUN6SSxDQUFDLENBQUMseUJBQXlCLENBQUMsQ0FBQyxPQUFPLENBQUM7Z0JBQ2pDLEtBQUssRUFBRSxHQUFHO2dCQUNWLFNBQVMsRUFBRSxNQUFNO2FBQ3BCLENBQUMsQ0FBQztZQUNILDZGQUE2RjtRQUNqRyxDQUFDLENBQUM7UUFDRixDQUFDLENBQUMsd0JBQXdCLENBQUMsQ0FBQztRQUM1QixNQUFNLENBQUMsd0JBQXdCLENBQUM7SUFDcEMsQ0FBQyxDQUFDIiwiZmlsZSI6InJlc3VsdHMvcmVzdWx0LWNoYXJ0LXByb3ZpZGVyLmpzIiwic291cmNlc0NvbnRlbnQiOltudWxsXSwic291cmNlUm9vdCI6Ii9zb3VyY2UvIn0=

/* global:draggable */
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var draggable = window.draggable;
        var createHeaderMarkup = function (documentId, documentName) {
            return '<div>' +
                '<div class="panel-heading">' +
                '<h4 class="panel-title">' +
                '<a href="#' + documentId + '">'
                + documentName + '</a></h4></div></div>';
        };
        $(function () {
            window.setTimeout(function () {
                //setupDraggableDialogs = function () {
                //    var enableDragging = function (e, h?) {
                //        var handle = $(e).find('.handle')[0];
                //        draggable(e, handle);
                //        e.style.position = '';
                //    };
                //    // Make all confirm delete modals draggable.
                //    $('[id^=confirm-delete-modal]').toArray().forEach(enableDragging);
                //    // Make the manage documents modal draggable.
                //    var draggableDialog = $('#manage-documents-modal');
                //    var dragHandle = draggableDialog.find('.handle')[0];
                //    if (draggableDialog[0] || dragHandle) {
                //        enableDragging(draggableDialog[0], dragHandle);
                //    }
                //};
                //setupDraggableDialogs();
                var $tabs = $('.document-viewer-tab-heading');
                var $listItemRefs = $('.document-list-item > a');
                var click = function (event) {
                    event.preventDefault();
                    var $listItem = $(this);
                    var $element = $(event.target);
                    //var documentName = $element[0].text;
                    var documentName = $element[0].innerText;
                    var documentId = $element.next('.item-id-hidden')[0].innerText.trim();
                    console.log('clicked Name: ' + documentName + ', Id: ' + documentId);
                    var $parentListItem = $($(event.target).parent());
                    var $progress = $parentListItem.find('.progress hidden')
                        .find('.progress-bar')
                        .removeClass('.hidden').css('width', '100');
                    $.get('Analysis/' + documentId).done(function (data, status, xhr) {
                        var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                        var panelMarkup = $("<div id=\"" + documentId + "\">\n                           <document-viewer-directive document=\"document\"></document-viewer-directive></div></div>");
                        if (!$('#' + documentId).length) {
                            $('#accordion').append(headerMarkup).append(panelMarkup);
                        }
                        else {
                            $('#' + documentId).remove();
                            $('#accordion').append(panelMarkup);
                        }
                        xhr.progress('100%');
                        //LASI.buildMenus();
                        //enableActiveHighlighting();
                    }).fail(function (xhr, message, detail) {
                        log(message);
                    }).progress(function (data) {
                        $progress.css('width', data);
                    });
                };
                $listItemRefs.click(click);
                //$tabs.click(click);
            }, 1000);
        });
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC93aWRnZXRzL2RvY3VtZW50LWxpc3QudHMiXSwibmFtZXMiOlsiTEFTSSIsIkxBU0kuZG9jdW1lbnRMaXN0Il0sIm1hcHBpbmdzIjoiQUFBQSxzQkFBc0I7QUFDdEIsSUFBVSxJQUFJLENBbUViO0FBbkVELFdBQVUsSUFBSTtJQUFDQSxJQUFBQSxZQUFZQSxDQW1FMUJBO0lBbkVjQSxXQUFBQSxZQUFZQSxFQUFDQSxDQUFDQTtRQUN6QkMsWUFBWUEsQ0FBQ0E7UUFDYkEsSUFBSUEsU0FBU0EsR0FBSUEsTUFBY0EsQ0FBQ0EsU0FBU0EsQ0FBQ0E7UUFDMUNBLElBQUlBLGtCQUFrQkEsR0FBR0EsVUFBVUEsVUFBVUEsRUFBRUEsWUFBWUE7WUFDdkQsTUFBTSxDQUFDLE9BQU87Z0JBQ1YsNkJBQTZCO2dCQUM3QiwwQkFBMEI7Z0JBQzFCLFlBQVksR0FBRyxVQUFVLEdBQUcsSUFBSTtrQkFDOUIsWUFBWSxHQUFHLHVCQUF1QixDQUFDO1FBQ2pELENBQUMsQ0FBQ0E7UUFDRkEsQ0FBQ0EsQ0FBQ0E7WUFDRSxNQUFNLENBQUMsVUFBVSxDQUFDO2dCQUNkLHVDQUF1QztnQkFDdkMsNkNBQTZDO2dCQUM3QywrQ0FBK0M7Z0JBQy9DLCtCQUErQjtnQkFDL0IsZ0NBQWdDO2dCQUNoQyxRQUFRO2dCQUNSLGtEQUFrRDtnQkFDbEQsd0VBQXdFO2dCQUN4RSxtREFBbUQ7Z0JBQ25ELHlEQUF5RDtnQkFDekQsMERBQTBEO2dCQUMxRCw2Q0FBNkM7Z0JBQzdDLHlEQUF5RDtnQkFDekQsT0FBTztnQkFDUCxJQUFJO2dCQUNKLDBCQUEwQjtnQkFDMUIsSUFBSSxLQUFLLEdBQUcsQ0FBQyxDQUFDLDhCQUE4QixDQUFDLENBQUM7Z0JBQzlDLElBQUksYUFBYSxHQUFHLENBQUMsQ0FBQyx5QkFBeUIsQ0FBQyxDQUFDO2dCQUNqRCxJQUFJLEtBQUssR0FBRyxVQUFVLEtBQUs7b0JBQ3ZCLEtBQUssQ0FBQyxjQUFjLEVBQUUsQ0FBQztvQkFDdkIsSUFBSSxTQUFTLEdBQUcsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDO29CQUN4QixJQUFJLFFBQVEsR0FBRyxDQUFDLENBQUMsS0FBSyxDQUFDLE1BQU0sQ0FBQyxDQUFDO29CQUMvQixzQ0FBc0M7b0JBQ3RDLElBQUksWUFBWSxHQUFHLFFBQVEsQ0FBQyxDQUFDLENBQUMsQ0FBQyxTQUFTLENBQUM7b0JBQ3pDLElBQUksVUFBVSxHQUFHLFFBQVEsQ0FBQyxJQUFJLENBQUMsaUJBQWlCLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQyxTQUFTLENBQUMsSUFBSSxFQUFFLENBQUM7b0JBQ3RFLE9BQU8sQ0FBQyxHQUFHLENBQUMsZ0JBQWdCLEdBQUcsWUFBWSxHQUFHLFFBQVEsR0FBRyxVQUFVLENBQUMsQ0FBQztvQkFDckUsSUFBSSxlQUFlLEdBQUcsQ0FBQyxDQUFDLENBQUMsQ0FBQyxLQUFLLENBQUMsTUFBTSxDQUFDLENBQUMsTUFBTSxFQUFFLENBQUMsQ0FBQztvQkFDbEQsSUFBSSxTQUFTLEdBQUcsZUFBZSxDQUFDLElBQUksQ0FBQyxrQkFBa0IsQ0FBQzt5QkFDbkQsSUFBSSxDQUFDLGVBQWUsQ0FBQzt5QkFDckIsV0FBVyxDQUFDLFNBQVMsQ0FBQyxDQUFDLEdBQUcsQ0FBQyxPQUFPLEVBQUUsS0FBSyxDQUFDLENBQUM7b0JBQ2hELENBQUMsQ0FBQyxHQUFHLENBQUMsV0FBVyxHQUFHLFVBQVUsQ0FBQyxDQUFDLElBQUksQ0FBQyxVQUFVLElBQUksRUFBRSxNQUFNLEVBQUUsR0FBRzt3QkFDNUQsSUFBSSxZQUFZLEdBQUcsQ0FBQyxDQUFDLGtCQUFrQixDQUFDLFVBQVUsRUFBRSxZQUFZLENBQUMsQ0FBQyxDQUFDO3dCQUNuRSxJQUFJLFdBQVcsR0FDWCxDQUFDLENBQUMsZUFBWSxVQUFVLDhIQUNtRSxDQUFDLENBQUM7d0JBQ2pHLEVBQUUsQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLEdBQUcsR0FBRyxVQUFVLENBQUMsQ0FBQyxNQUFNLENBQUMsQ0FBQyxDQUFDOzRCQUM5QixDQUFDLENBQUMsWUFBWSxDQUFDLENBQUMsTUFBTSxDQUFDLFlBQVksQ0FBQyxDQUFDLE1BQU0sQ0FBQyxXQUFXLENBQUMsQ0FBQzt3QkFFN0QsQ0FBQzt3QkFBQyxJQUFJLENBQUMsQ0FBQzs0QkFDSixDQUFDLENBQUMsR0FBRyxHQUFHLFVBQVUsQ0FBQyxDQUFDLE1BQU0sRUFBRSxDQUFDOzRCQUM3QixDQUFDLENBQUMsWUFBWSxDQUFDLENBQUMsTUFBTSxDQUFDLFdBQVcsQ0FBQyxDQUFDO3dCQUN4QyxDQUFDO3dCQUNELEdBQUcsQ0FBQyxRQUFRLENBQUMsTUFBTSxDQUFDLENBQUM7d0JBQ3JCLG9CQUFvQjt3QkFDcEIsNkJBQTZCO29CQUNqQyxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsVUFBVSxHQUFHLEVBQUUsT0FBTyxFQUFFLE1BQU07d0JBQ2xDLEdBQUcsQ0FBQyxPQUFPLENBQUMsQ0FBQztvQkFDakIsQ0FBQyxDQUFDLENBQUMsUUFBUSxDQUFDLFVBQVUsSUFBSTt3QkFDdEIsU0FBUyxDQUFDLEdBQUcsQ0FBQyxPQUFPLEVBQUUsSUFBSSxDQUFDLENBQUM7b0JBQ2pDLENBQUMsQ0FBQyxDQUFDO2dCQUNQLENBQUMsQ0FBQztnQkFDRixhQUFhLENBQUMsS0FBSyxDQUFDLEtBQUssQ0FBQyxDQUFDO2dCQUMzQixxQkFBcUI7WUFDekIsQ0FBQyxFQUFFLElBQUksQ0FBQyxDQUFDO1FBQ2IsQ0FBQyxDQUFDQSxDQUFDQTtJQUNQQSxDQUFDQSxFQW5FY0QsWUFBWUEsR0FBWkEsaUJBQVlBLEtBQVpBLGlCQUFZQSxRQW1FMUJBO0FBQURBLENBQUNBLEVBbkVTLElBQUksS0FBSixJQUFJLFFBbUViIiwiZmlsZSI6IndpZGdldHMvZG9jdW1lbnQtbGlzdC5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../lasi.ts" />
(function (app) {
    'use strict';
    app.validateFileExtension = (function () {
        var accepted = Object.freeze(['.txt', '.docx', '.pdf', 'doc']);
        return function (fileName) {
            var extension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
            return accepted.some(function (ext) {
                return ext === extension;
            });
        };
    })();
    $(function () {
        var $uploadList = $('#document-upload-list');
        app.$uploadList = $uploadList;
        $(document)
            .find('.btn-file :file')
            .change(function () {
            var $input = $(this), fileCount = $uploadList.find('span.file-index').length, files = $input[0].files;
            app.files = files;
            app.fileCount = fileCount;
            var generateUploadListItemMarkup = function (file, index) {
                return '<div class="list-group-item text-left">' +
                    '<span class="glyphicon glyphicon-remove remove-file"/>' +
                    '&nbsp;&nbsp;&nbsp;<span class="file-index">' +
                    [fileCount, index, 1].sum() +
                    '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">' +
                    file.name + '</span></div>';
            }, label = $input.val().replace(/\\/g, '/').replace(/[.]*\//, '');
            $(files).filter(function (index, element) { return app.validateFileExtension(element.name); })
                .toArray()
                .filter(function (file) {
                return !($uploadList.children('span').toArray().some(function () {
                    return $(this).text() === file.name;
                }));
            }).forEach(function (file, index) {
                $uploadList.append(generateUploadListItemMarkup(file, index));
                $('span.glyphicon.glyphicon-remove.remove-file')
                    .click(function () {
                    $(this).removeData(file.name)
                        .parent()
                        .parent()
                        .find('span.file-name')
                        .filter(function () { return $(this).text() === file.name; })
                        .each(function () { return $(this).parent('div').remove(); });
                    $uploadList.find('span.file-index')
                        .each(function (index) { $(this).text(index + 1); });
                });
            });
            $input.trigger('fileselect', [files && files.length, label]);
        });
    });
})(LASI);

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC93aWRnZXRzL2RvY3VtZW50LXVwbG9hZC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSw0REFBNEQ7QUFDNUQsbUNBQW1DO0FBQ25DLENBQUMsVUFBVSxHQUFHO0lBQ1YsWUFBWSxDQUFDO0lBQ2IsR0FBRyxDQUFDLHFCQUFxQixHQUFHLENBQUM7UUFDekIsSUFBSSxRQUFRLEdBQUcsTUFBTSxDQUFDLE1BQU0sQ0FBQyxDQUFDLE1BQU0sRUFBRSxPQUFPLEVBQUUsTUFBTSxFQUFFLEtBQUssQ0FBQyxDQUFDLENBQUM7UUFDL0QsTUFBTSxDQUFDLFVBQVUsUUFBUTtZQUNyQixJQUFJLFNBQVMsR0FBRyxRQUFRLENBQUMsU0FBUyxDQUFDLFFBQVEsQ0FBQyxXQUFXLENBQUMsR0FBRyxDQUFDLENBQUMsQ0FBQyxXQUFXLEVBQUUsQ0FBQztZQUM1RSxNQUFNLENBQUMsUUFBUSxDQUFDLElBQUksQ0FBQyxVQUFVLEdBQUc7Z0JBQzlCLE1BQU0sQ0FBQyxHQUFHLEtBQUssU0FBUyxDQUFDO1lBQzdCLENBQUMsQ0FBQyxDQUFDO1FBQ1AsQ0FBQyxDQUFDO0lBQ04sQ0FBQyxDQUFDLEVBQUUsQ0FBQztJQUNMLENBQUMsQ0FBQztRQUNFLElBQUksV0FBVyxHQUFHLENBQUMsQ0FBQyx1QkFBdUIsQ0FBQyxDQUFDO1FBQzdDLEdBQUcsQ0FBQyxXQUFXLEdBQUcsV0FBVyxDQUFDO1FBQzlCLENBQUMsQ0FBQyxRQUFRLENBQUM7YUFDTixJQUFJLENBQUMsaUJBQWlCLENBQUM7YUFDdkIsTUFBTSxDQUFDO1lBQ1IsSUFBSSxNQUFNLEdBQUcsQ0FBQyxDQUFDLElBQUksQ0FBQyxFQUNoQixTQUFTLEdBQUcsV0FBVyxDQUFDLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxDQUFDLE1BQU0sRUFDdEQsS0FBSyxHQUFzQixNQUFNLENBQUMsQ0FBQyxDQUFFLENBQUMsS0FBSyxDQUFDO1lBQ2hELEdBQUcsQ0FBQyxLQUFLLEdBQUcsS0FBSyxDQUFDO1lBQ2xCLEdBQUcsQ0FBQyxTQUFTLEdBQUcsU0FBUyxDQUFDO1lBQzFCLElBQUksNEJBQTRCLEdBQUcsVUFBVSxJQUFJLEVBQUUsS0FBSztnQkFDcEQsTUFBTSxDQUFDLHlDQUF5QztvQkFDNUMsd0RBQXdEO29CQUN4RCw2Q0FBNkM7b0JBQzdDLENBQUMsU0FBUyxFQUFFLEtBQUssRUFBRSxDQUFDLENBQUMsQ0FBQyxHQUFHLEVBQUU7b0JBQzNCLHlEQUF5RDtvQkFDekQsSUFBSSxDQUFDLElBQUksR0FBRyxlQUFlLENBQUM7WUFDcEMsQ0FBQyxFQUNHLEtBQUssR0FBRyxNQUFNLENBQUMsR0FBRyxFQUFFLENBQUMsT0FBTyxDQUFDLEtBQUssRUFBRSxHQUFHLENBQUMsQ0FBQyxPQUFPLENBQUMsUUFBUSxFQUFFLEVBQUUsQ0FBQyxDQUFDO1lBQ25FLENBQUMsQ0FBQyxLQUFLLENBQUMsQ0FBQyxNQUFNLENBQUMsVUFBQyxLQUFLLEVBQUUsT0FBTyxJQUFLLE9BQUEsR0FBRyxDQUFDLHFCQUFxQixDQUFvQixPQUFRLENBQUMsSUFBSSxDQUFDLEVBQTNELENBQTJELENBQUM7aUJBQzNGLE9BQU8sRUFBRTtpQkFDVCxNQUFNLENBQUMsVUFBVSxJQUFJO2dCQUN0QixNQUFNLENBQUMsQ0FBQyxDQUFDLFdBQVcsQ0FBQyxRQUFRLENBQUMsTUFBTSxDQUFDLENBQUMsT0FBTyxFQUFFLENBQUMsSUFBSSxDQUFDO29CQUNqRCxNQUFNLENBQUMsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLElBQUksRUFBRSxLQUFLLElBQUksQ0FBQyxJQUFJLENBQUM7Z0JBQ3hDLENBQUMsQ0FBQyxDQUFDLENBQUM7WUFDUixDQUFDLENBQUMsQ0FBQyxPQUFPLENBQUMsVUFBVSxJQUFJLEVBQUUsS0FBSztnQkFDNUIsV0FBVyxDQUFDLE1BQU0sQ0FBQyw0QkFBNEIsQ0FBQyxJQUFJLEVBQUUsS0FBSyxDQUFDLENBQUMsQ0FBQztnQkFDOUQsQ0FBQyxDQUFDLDZDQUE2QyxDQUFDO3FCQUMzQyxLQUFLLENBQUM7b0JBQ1AsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLFVBQVUsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDO3lCQUN4QixNQUFNLEVBQUU7eUJBQ1IsTUFBTSxFQUFFO3lCQUNSLElBQUksQ0FBQyxnQkFBZ0IsQ0FBQzt5QkFDdEIsTUFBTSxDQUFDLGNBQWMsTUFBTSxDQUFDLENBQUMsQ0FBQyxJQUFJLENBQUMsQ0FBQyxJQUFJLEVBQUUsS0FBSyxJQUFJLENBQUMsSUFBSSxDQUFDLENBQUMsQ0FBQyxDQUFDO3lCQUM1RCxJQUFJLENBQUMsY0FBYyxNQUFNLENBQUMsQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLE1BQU0sQ0FBQyxLQUFLLENBQUMsQ0FBQyxNQUFNLEVBQUUsQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDO29CQUNsRSxXQUFXLENBQUMsSUFBSSxDQUFDLGlCQUFpQixDQUFDO3lCQUM5QixJQUFJLENBQUMsVUFBVSxLQUFLLElBQUksQ0FBQyxDQUFDLElBQUksQ0FBQyxDQUFDLElBQUksQ0FBQyxLQUFLLEdBQUcsQ0FBQyxDQUFDLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQztnQkFDN0QsQ0FBQyxDQUFDLENBQUM7WUFDUCxDQUFDLENBQUMsQ0FBQztZQUNILE1BQU0sQ0FBQyxPQUFPLENBQUMsWUFBWSxFQUFFLENBQUMsS0FBSyxJQUFJLEtBQUssQ0FBQyxNQUFNLEVBQUUsS0FBSyxDQUFDLENBQUMsQ0FBQztRQUNqRSxDQUFDLENBQUMsQ0FBQztJQUNQLENBQUMsQ0FBQyxDQUFDO0FBQ1AsQ0FBQyxDQUFDLENBQUMsSUFBSSxDQUFDLENBQUMiLCJmaWxlIjoid2lkZ2V0cy9kb2N1bWVudC11cGxvYWQuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

var LASI;
(function (LASI) {
    var widgets;
    (function (widgets) {
        'use strict';
        processingTaskProgressBar.$inject = [];
        function processingTaskProgressBar() {
            return {
                restrict: 'E',
                scope: {
                    task: '='
                },
                templateUrl: '/app/widgets/processing-task-progress-bar.directive.html'
            };
        }
        angular
            .module('widgets')
            .directive({ processingTaskProgressBar: processingTaskProgressBar });
    })(widgets = LASI.widgets || (LASI.widgets = {}));
})(LASI || (LASI = {}));

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC93aWRnZXRzL3Byb2Nlc3NpbmctdGFzay1wcm9ncmVzcy1iYXIuZGlyZWN0aXZlLnRzIl0sIm5hbWVzIjpbIkxBU0kiLCJMQVNJLndpZGdldHMiLCJMQVNJLndpZGdldHMucHJvY2Vzc2luZ1Rhc2tQcm9ncmVzc0JhciJdLCJtYXBwaW5ncyI6IkFBQUEsSUFBVSxJQUFJLENBaUJiO0FBakJELFdBQVUsSUFBSTtJQUFDQSxJQUFBQSxPQUFPQSxDQWlCckJBO0lBakJjQSxXQUFBQSxPQUFPQSxFQUFDQSxDQUFDQTtRQUNwQkMsWUFBWUEsQ0FBQ0E7UUFFYkEseUJBQXlCQSxDQUFDQSxPQUFPQSxHQUFHQSxFQUFFQSxDQUFDQTtRQUN2Q0E7WUFDSUMsTUFBTUEsQ0FBQ0E7Z0JBQ0hBLFFBQVFBLEVBQUVBLEdBQUdBO2dCQUNiQSxLQUFLQSxFQUFFQTtvQkFDSEEsSUFBSUEsRUFBRUEsR0FBR0E7aUJBQ1pBO2dCQUNEQSxXQUFXQSxFQUFFQSwwREFBMERBO2FBQzFFQSxDQUFDQTtRQUNOQSxDQUFDQTtRQUVERCxPQUFPQTthQUNGQSxNQUFNQSxDQUFDQSxTQUFTQSxDQUFDQTthQUNqQkEsU0FBU0EsQ0FBQ0EsRUFBRUEsMkJBQUFBLHlCQUF5QkEsRUFBRUEsQ0FBQ0EsQ0FBQ0E7SUFDbERBLENBQUNBLEVBakJjRCxPQUFPQSxHQUFQQSxZQUFPQSxLQUFQQSxZQUFPQSxRQWlCckJBO0FBQURBLENBQUNBLEVBakJTLElBQUksS0FBSixJQUFJLFFBaUJiIiwiZmlsZSI6IndpZGdldHMvcHJvY2Vzc2luZy10YXNrLXByb2dyZXNzLWJhci5kaXJlY3RpdmUuanMiLCJzb3VyY2VzQ29udGVudCI6W251bGxdLCJzb3VyY2VSb290IjoiL3NvdXJjZS8ifQ==

//module LASI.documentViewer {
//    'use strict';
//    export interface IDocumentModelService {
//        processDocument(documentId: string): models.IDocumentModel;
//    }
//    class MockDocumentModelService implements IDocumentModelService {
//        static $inject: string[] = ['$resource'];
//        documentSource: ng.resource.IResourceClass<models.IDocumentModel>;
//        constructor(private $resource: ng.resource.IResourceService) {
//            this.documentSource = $resource<models.IDocumentModel>('Analysis/:documentId');
//        }
//        processDocument(documentId: string) {
//            return this.$resource<models.IDocumentModel>('tests/test-data/doc.json').get();
//        }
//    }
//    angular
//        .module('documentViewer')
//        .service('MockDocumentModelService', MockDocumentModelService);
//} 

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC9kb2N1bWVudC12aWV3ZXIvdGVzdHMvbW9jay1kb2N1bWVudC1tb2RlbC1zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBLDhCQUE4QjtBQUM5QixtQkFBbUI7QUFFbkIsOENBQThDO0FBQzlDLHFFQUFxRTtBQUNyRSxPQUFPO0FBRVAsdUVBQXVFO0FBQ3ZFLG1EQUFtRDtBQUNuRCw0RUFBNEU7QUFDNUUsd0VBQXdFO0FBQ3hFLDZGQUE2RjtBQUM3RixXQUFXO0FBQ1gsK0NBQStDO0FBQy9DLDZGQUE2RjtBQUM3RixXQUFXO0FBQ1gsT0FBTztBQUVQLGFBQWE7QUFDYixtQ0FBbUM7QUFDbkMseUVBQXlFO0FBQ3pFLEdBQUciLCJmaWxlIjoiZG9jdW1lbnQtdmlld2VyL3Rlc3RzL21vY2stZG9jdW1lbnQtbW9kZWwtc2VydmljZS5qcyIsInNvdXJjZXNDb250ZW50IjpbbnVsbF0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9

/// <amd-dependency path="angular" />
/// <amd-dependency path="angular-resource" />
/// <amd-dependency path="angular-bootstrap" />
/// <amd-dependency path="angular-bootstrap-contextmenu" />
/// <amd-dependency path="angular-file-upload" />
/// <amd-dependency path="jquery" />
/// <amd-dependency path="app/utilities/augmentations"
define(["require", "exports", 'angular', './app/debug/debug.module', './app/widgets/widgets.module', './app/document-list/document-list.module', './app/document-upload/document-upload.module', './app/document-viewer/document-viewer.module', './app/document-viewer/search/search.module', "angular", "angular-resource", "angular-bootstrap", "angular-bootstrap-contextmenu", "angular-file-upload", "jquery", "app/utilities/augmentations", 'app/utilities/augmentations'], function (require, exports, angular_1, debug_module_1, widgets_module_1, document_list_module_1, document_upload_module_1, document_viewer_module_1, search_module_1) {
    function register(m) {
        function validate() {
            if (!m.name) {
                throw new TypeError("name is required");
            }
            if (!m.requires) {
                throw new TypeError("requires must be an array. Did you intend to invoke the setter?");
            }
        }
        angular_1.module(m.name, m.requires, m.configFn || (function () { }))
            .provider(m.providers || {})
            .factory(m.factories || {})
            .service(m.services || {})
            .filter(m.filters || {})
            .controller(m.controllers || {})
            .directive(m.directives || {})
            .value(m.values || {})
            .constant(m.constants || {})
            .run(m.runFn || (function () { }));
    }
    function angularBootstrap() {
        function registerAll() {
            var modules = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                modules[_i - 0] = arguments[_i];
            }
            modules.forEach(register);
        }
        return function () {
            registerAll(debug_module_1.default, widgets_module_1.default, document_list_module_1.default, document_upload_module_1.default, document_viewer_module_1.default, search_module_1.default);
            angular_1.bootstrap(document.body, ['documentList']);
        };
    }
    exports.angularBootstrap = angularBootstrap;
});

//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm1haW4udHMiXSwibmFtZXMiOlsicmVnaXN0ZXIiLCJyZWdpc3Rlci52YWxpZGF0ZSIsImFuZ3VsYXJCb290c3RyYXAiLCJhbmd1bGFyQm9vdHN0cmFwLnJlZ2lzdGVyQWxsIl0sIm1hcHBpbmdzIjoiQUFBQSxxQ0FBcUM7QUFDckMsOENBQThDO0FBQzlDLCtDQUErQztBQUMvQywyREFBMkQ7QUFDM0QsaURBQWlEO0FBQ2pELG9DQUFvQztBQUNwQyxzREFBc0Q7O0lBV3RELGtCQUFrQixDQUF1QjtRQUNyQ0E7WUFDSUMsRUFBRUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsSUFBSUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7Z0JBQUNBLE1BQU1BLElBQUlBLFNBQVNBLENBQUNBLGtCQUFrQkEsQ0FBQ0EsQ0FBQ0E7WUFBQ0EsQ0FBQ0E7WUFDekRBLEVBQUVBLENBQUNBLENBQUNBLENBQUNBLENBQUNBLENBQUNBLFFBQVFBLENBQUNBLENBQUNBLENBQUNBO2dCQUFDQSxNQUFNQSxJQUFJQSxTQUFTQSxDQUFDQSxpRUFBaUVBLENBQUNBLENBQUNBO1lBQUNBLENBQUNBO1FBQ2hIQSxDQUFDQTtRQUNERCxnQkFBTUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsSUFBSUEsRUFBRUEsQ0FBQ0EsQ0FBQ0EsUUFBUUEsRUFBRUEsQ0FBQ0EsQ0FBQ0EsUUFBUUEsSUFBSUEsQ0FBQ0EsY0FBUUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7YUFDaERBLFFBQVFBLENBQUNBLENBQUNBLENBQUNBLFNBQVNBLElBQUlBLEVBQUVBLENBQUNBO2FBQzNCQSxPQUFPQSxDQUFDQSxDQUFDQSxDQUFDQSxTQUFTQSxJQUFJQSxFQUFFQSxDQUFDQTthQUMxQkEsT0FBT0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsUUFBUUEsSUFBSUEsRUFBRUEsQ0FBQ0E7YUFDekJBLE1BQU1BLENBQUNBLENBQUNBLENBQUNBLE9BQU9BLElBQUlBLEVBQUVBLENBQUNBO2FBQ3ZCQSxVQUFVQSxDQUFDQSxDQUFDQSxDQUFDQSxXQUFXQSxJQUFJQSxFQUFFQSxDQUFDQTthQUMvQkEsU0FBU0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsVUFBVUEsSUFBSUEsRUFBRUEsQ0FBQ0E7YUFDN0JBLEtBQUtBLENBQUNBLENBQUNBLENBQUNBLE1BQU1BLElBQUlBLEVBQUVBLENBQUNBO2FBQ3JCQSxRQUFRQSxDQUFDQSxDQUFDQSxDQUFDQSxTQUFTQSxJQUFJQSxFQUFFQSxDQUFDQTthQUMzQkEsR0FBR0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsS0FBS0EsSUFBSUEsQ0FBQ0EsY0FBUUEsQ0FBQ0EsQ0FBQ0EsQ0FBQ0EsQ0FBQ0E7SUFDckNBLENBQUNBO0lBQ0Q7UUFDSUU7WUFBcUJDLGlCQUFrQ0E7aUJBQWxDQSxXQUFrQ0EsQ0FBbENBLHNCQUFrQ0EsQ0FBbENBLElBQWtDQTtnQkFBbENBLGdDQUFrQ0E7O1lBQ25EQSxPQUFPQSxDQUFDQSxPQUFPQSxDQUFDQSxRQUFRQSxDQUFDQSxDQUFDQTtRQUM5QkEsQ0FBQ0E7UUFFREQsTUFBTUEsQ0FBQ0E7WUFDSCxXQUFXLENBQUMsc0JBQUssRUFBRSx3QkFBTyxFQUFFLDhCQUFZLEVBQUUsZ0NBQWMsRUFBRSxnQ0FBYyxFQUFFLHVCQUFvQixDQUFDLENBQUM7WUFDaEcsbUJBQVMsQ0FBQyxRQUFRLENBQUMsSUFBSSxFQUFFLENBQUMsY0FBYyxDQUFDLENBQUMsQ0FBQztRQUMvQyxDQUFDLENBQUNBO0lBQ05BLENBQUNBO0lBVGUsd0JBQWdCLG1CQVMvQixDQUFBIiwiZmlsZSI6Im1haW4uanMiLCJzb3VyY2VzQ29udGVudCI6WyIvLy8gPGFtZC1kZXBlbmRlbmN5IHBhdGg9XCJhbmd1bGFyXCIgLz5cclxuLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwiYW5ndWxhci1yZXNvdXJjZVwiIC8+XHJcbi8vLyA8YW1kLWRlcGVuZGVuY3kgcGF0aD1cImFuZ3VsYXItYm9vdHN0cmFwXCIgLz5cclxuLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwiYW5ndWxhci1ib290c3RyYXAtY29udGV4dG1lbnVcIiAvPlxyXG4vLy8gPGFtZC1kZXBlbmRlbmN5IHBhdGg9XCJhbmd1bGFyLWZpbGUtdXBsb2FkXCIgLz5cclxuLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwianF1ZXJ5XCIgLz5cclxuLy8vIDxhbWQtZGVwZW5kZW5jeSBwYXRoPVwiYXBwL3V0aWxpdGllcy9hdWdtZW50YXRpb25zXCJcclxuXHJcbmltcG9ydCB7IG1vZHVsZSwgYm9vdHN0cmFwIH0gZnJvbSAnYW5ndWxhcic7XHJcbmltcG9ydCAnYXBwL3V0aWxpdGllcy9hdWdtZW50YXRpb25zJztcclxuaW1wb3J0IGRlYnVnIGZyb20gJy4vYXBwL2RlYnVnL2RlYnVnLm1vZHVsZSc7XHJcbmltcG9ydCB3aWRnZXRzIGZyb20gJy4vYXBwL3dpZGdldHMvd2lkZ2V0cy5tb2R1bGUnO1xyXG5pbXBvcnQgZG9jdW1lbnRMaXN0IGZyb20gJy4vYXBwL2RvY3VtZW50LWxpc3QvZG9jdW1lbnQtbGlzdC5tb2R1bGUnO1xyXG5pbXBvcnQgZG9jdW1lbnRVcGxvYWQgZnJvbSAnLi9hcHAvZG9jdW1lbnQtdXBsb2FkL2RvY3VtZW50LXVwbG9hZC5tb2R1bGUnO1xyXG5pbXBvcnQgZG9jdW1lbnRWaWV3ZXIgZnJvbSAnLi9hcHAvZG9jdW1lbnQtdmlld2VyL2RvY3VtZW50LXZpZXdlci5tb2R1bGUnO1xyXG5pbXBvcnQgZG9jdW1lbnRWaWV3ZXJTZWFyY2ggZnJvbSAnLi9hcHAvZG9jdW1lbnQtdmlld2VyL3NlYXJjaC9zZWFyY2gubW9kdWxlJztcclxuXHJcbmZ1bmN0aW9uIHJlZ2lzdGVyKG06IEFuZ3VsYXJNb2R1bGVPcHRpb25zKSB7XHJcbiAgICBmdW5jdGlvbiB2YWxpZGF0ZSgpIHtcclxuICAgICAgICBpZiAoIW0ubmFtZSkgeyB0aHJvdyBuZXcgVHlwZUVycm9yKFwibmFtZSBpcyByZXF1aXJlZFwiKTsgfVxyXG4gICAgICAgIGlmICghbS5yZXF1aXJlcykgeyB0aHJvdyBuZXcgVHlwZUVycm9yKFwicmVxdWlyZXMgbXVzdCBiZSBhbiBhcnJheS4gRGlkIHlvdSBpbnRlbmQgdG8gaW52b2tlIHRoZSBzZXR0ZXI/XCIpOyB9XHJcbiAgICB9XHJcbiAgICBtb2R1bGUobS5uYW1lLCBtLnJlcXVpcmVzLCBtLmNvbmZpZ0ZuIHx8ICgoKSA9PiB7IH0pKVxyXG4gICAgICAgIC5wcm92aWRlcihtLnByb3ZpZGVycyB8fCB7fSlcclxuICAgICAgICAuZmFjdG9yeShtLmZhY3RvcmllcyB8fCB7fSlcclxuICAgICAgICAuc2VydmljZShtLnNlcnZpY2VzIHx8IHt9KVxyXG4gICAgICAgIC5maWx0ZXIobS5maWx0ZXJzIHx8IHt9KVxyXG4gICAgICAgIC5jb250cm9sbGVyKG0uY29udHJvbGxlcnMgfHwge30pXHJcbiAgICAgICAgLmRpcmVjdGl2ZShtLmRpcmVjdGl2ZXMgfHwge30pXHJcbiAgICAgICAgLnZhbHVlKG0udmFsdWVzIHx8IHt9KVxyXG4gICAgICAgIC5jb25zdGFudChtLmNvbnN0YW50cyB8fCB7fSlcclxuICAgICAgICAucnVuKG0ucnVuRm4gfHwgKCgpID0+IHsgfSkpO1xyXG59XHJcbmV4cG9ydCBmdW5jdGlvbiBhbmd1bGFyQm9vdHN0cmFwKCkge1xyXG4gICAgZnVuY3Rpb24gcmVnaXN0ZXJBbGwoLi4ubW9kdWxlczogQW5ndWxhck1vZHVsZU9wdGlvbnNbXSkge1xyXG4gICAgICAgIG1vZHVsZXMuZm9yRWFjaChyZWdpc3Rlcik7XHJcbiAgICB9XHJcblxyXG4gICAgcmV0dXJuIGZ1bmN0aW9uICgpIHtcclxuICAgICAgICByZWdpc3RlckFsbChkZWJ1Zywgd2lkZ2V0cywgZG9jdW1lbnRMaXN0LCBkb2N1bWVudFVwbG9hZCwgZG9jdW1lbnRWaWV3ZXIsIGRvY3VtZW50Vmlld2VyU2VhcmNoKTtcclxuICAgICAgICBib290c3RyYXAoZG9jdW1lbnQuYm9keSwgWydkb2N1bWVudExpc3QnXSk7XHJcbiAgICB9O1xyXG59Il0sInNvdXJjZVJvb3QiOiIvc291cmNlLyJ9
