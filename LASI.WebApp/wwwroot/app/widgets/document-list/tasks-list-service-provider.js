var tasksListServiceProvider = function () {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';
    var provider = {
        setUpdateInterval: function (milliseconds) {
            updateInterval = milliseconds;
            return this;
        }, setTasksListUrl: function (url) {
            tasksListUrl = url;
            return this;
        },
        $get: function ($resource, $window) {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource(tasksListUrl, {}, {
                'get': {
                    method: 'GET', isArray: true
                }
            });
            var tasks = [];
            var update = function () {
                tasks = Tasks.get();
                updateDebugInfo(tasks);
            };
            var getActiveTasks = function () {
                $window.setInterval(update, updateInterval);
                return tasks;
            };
            return {
                getActiveTasks: getActiveTasks
            };
        }
    };
    provider.$get.$inject = ['$resource', '$window'];
    return provider;
    function createDebugInfoUpdator(element) {
        return function (tasks) { return element.html(tasks
            .map(function (task) { return "<div>" +
            Object.keys(task).map(function (key) { return "<span>&nbsp&nbsp" + task[key] + "</span>"; }) +
            "</div>"; })
            .join()); };
    }
}();
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('tasksListService', tasksListServiceProvider);
})();
