var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .provider('tasksListService', tasksListServiceProvider);
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
                //var updateDebugInfo = function (tasks) { }; //createDebugInfoUpdator($('#debug-panel'));
                var Tasks = $resource(tasksListUrl, {}, {
                    get: {
                        method: 'GET', isArray: true
                    }
                });
                var getActiveTasks = function (callback) {
                    var _this = this;
                    var deferred = $q.defer();
                    $interval(function () {
                        callback && callback(_this.tasks);
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
                return function (tasks) { return element
                    .html(tasks
                    .map(function (task) { return ("<div>" + Object.keys(task).map(function (key) { return ("<span>&nbsp&nbsp" + task[key] + "</span>"); }) + "</div>"); })
                    .join()); };
            }
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
