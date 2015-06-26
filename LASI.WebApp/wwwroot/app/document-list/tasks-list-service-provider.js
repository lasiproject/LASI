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
            $get.$inject = ['$resource', '$window'];
            return { $get: $get, setUpdateInterval: setUpdateInterval, setTasksListUrl: setTasksListUrl };
            function setUpdateInterval(milliseconds) {
                updateInterval = milliseconds;
                return this;
            }
            function setTasksListUrl(url) {
                tasksListUrl = url;
                return this;
            }
            function $get($resource, $window) {
                var updateDebugInfo = function (tasks) { }; //createDebugInfoUpdator($('#debug-panel'));
                var Tasks = $resource(tasksListUrl, { cache: false }, {
                    get: {
                        method: 'GET', isArray: true
                    }
                });
                var tasks = [];
                var getActiveTasks = function (callback) {
                    $window.setInterval(function () {
                        callback(tasks);
                        tasks = Tasks.get();
                        updateDebugInfo(tasks);
                    }, updateInterval);
                    return tasks;
                };
                return {
                    getActiveTasks: getActiveTasks
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
