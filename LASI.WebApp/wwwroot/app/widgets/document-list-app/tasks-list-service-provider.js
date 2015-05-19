/// <reference path="../../../typings/angularjs/angular.d.ts" />
/// <reference path="../../../typings/angularjs/angular-resource.d.ts" />
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('tasksListService', TasksListServiceProvider);
    function TasksListServiceProvider() {
        /* jshint validthis:true */
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';
        this.setUpdateInterval = function (millisconds) {
            updateInterval = millisconds;
            return this;
        };
        this.setTasksListUrl = function (url) {
            tasksListUrl = url;
            return this;
        };
        this.$get = tasksListServiceFactory;
        return this;
        function tasksListServiceFactory($resource, $window) {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource(tasksListUrl, {}, { 'get': { method: 'GET', isArray: true } });
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
        tasksListServiceFactory.$inject = ['$resource', '$window'];
        function createDebugInfoUpdator(element) {
            return function displayTaskInfo(tasks) {
                element.html(tasks.map(function (task) {
                    return "<div>" + Object.keys(task).map(function (key) {
                        return "<span>&nbsp&nbsp" + task[key] + "</span>";
                    }) + "</div>";
                }).join());
            };
        }
    }
})();
