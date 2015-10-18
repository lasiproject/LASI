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
