System.register([], function(exports_1) {
    'use strict';
    function tasksListServiceProvider() {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';
        var tasks = [];
        $get.$inject = ['$q', '$http', '$interval'];
        return { $get: $get, setUpdateInterval: setUpdateInterval, setTasksListUrl: setTasksListUrl };
        function setUpdateInterval(milliseconds) {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url) {
            tasksListUrl = url;
            return this;
        }
        function $get($q, $http, $interval) {
            return {
                getActiveTasks: function () {
                    var deferred = $q.defer();
                    $interval(function () {
                        $http.get(tasksListUrl).success(function (ts) {
                            deferred.resolve(ts);
                            tasks = ts;
                        });
                    }, updateInterval);
                    return tasks;
                },
                tasks: tasks
            };
        }
        function createDebugInfoUpdator(element) {
            return function (tasks) { return element.html(tasks.map(function (task) { return ("<div>" + Object.keys(task).map(function (key) { return ("<span>&nbsp&nbsp" + task[key] + "</span>"); }) + "</div>"); }).join()); };
        }
    }
    exports_1("tasksListServiceProvider", tasksListServiceProvider);
    return {
        setters:[],
        execute: function() {
        }
    }
});
