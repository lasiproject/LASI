var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        var tasksListServiceProvider = function () {
            var updateInterval = 200;
            var tasksListUrl = 'api/Tasks';
            $get.$inject = ['$resource', '$window'];
            return {
                $get: $get,
                setUpdateInterval: function (milliseconds) {
                    updateInterval = milliseconds;
                    return this;
                }, setTasksListUrl: function (url) {
                    tasksListUrl = url;
                    return this;
                }
            };
            function $get($resource, $window) {
                var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
                var Tasks = $resource(tasksListUrl, { cache: false }, {
                    'get': {
                        method: 'GET', isArray: true
                    }
                });
                var tasks = [];
                var update = function () {
                    tasks = Tasks.get();
                    updateDebugInfo(tasks);
                };
                var getActiveTasks = function (callback) {
                    $window.setInterval(function () {
                        callback(tasks);
                        update();
                    }, updateInterval);
                    return tasks;
                };
                return {
                    getActiveTasks: getActiveTasks
                };
            }
            function createDebugInfoUpdator(element) {
                return function (tasks) { return element.html(tasks
                    .map(function (task) { return '<div>' +
                    Object.keys(task).map(function (key) { return '<span>&nbsp&nbsp' + task[key] + '</span>'; }) +
                    '</div>'; })
                    .join()); };
            }
        }();
        (function () {
            'use strict';
            angular
                .module(documentList.moduleName)
                .provider('tasksListService', tasksListServiceProvider);
        })();
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=tasks-list-service-provider.js.map