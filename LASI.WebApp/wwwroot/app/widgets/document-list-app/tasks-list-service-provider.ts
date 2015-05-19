interface ITasksListServiceConfig {
    setTasksListUrl(url: string): ITasksListServiceConfig
    setUpdateInterval(milliconds: number): ITasksListServiceConfig
}
interface ITask {
    id: string
    name: string
    state: string
    percentComplete: number
    statusMessage: string
}
var tasksListServiceProvider = function () {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';
    var provider = {
        setUpdateInterval: function (milliseconds: number) {
            updateInterval = milliseconds;
            return this;
        }, setTasksListUrl: function (url: string) {
            tasksListUrl = url;
            return this;
        },
        $get: ($resource: ng.resource.IResourceService, $window: ng.IWindowService) => {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource<ITask[]>(tasksListUrl, {}, {
                'get': {
                    method: 'GET', isArray: true
                }
            });
            var tasks: ITask[] = [];
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
    }; provider.$get.$inject = ['$resource', '$window'];
    return provider;
    function createDebugInfoUpdator(element: JQuery) {
        return (tasks: ITask[]) => element.html(tasks
            .map(task => "<div>" +
            Object.keys(task).map(key => "<span>&nbsp&nbsp" + task[key] + "</span>") +
            "</div>")
            .join());
    }
} ();
(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .provider('tasksListService', tasksListServiceProvider);
})();