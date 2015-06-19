module LASI.documentList {
    export interface ITask {
        id: string;
        name: string;
        state: string;
        percentComplete: number;
        statusMessage: string;
    }
    export interface ITasksListServiceConfig {
        $get: ($resource: ng.resource.IResourceService, $window: ng.IWindowService) => ITasksListService;
        setTasksListUrl(url: string): ITasksListServiceConfig;
        setUpdateInterval(milliconds: number): ITasksListServiceConfig;
    }

    export interface ITasksListService {
        getActiveTasks(callback: (tasks: ITask[]) => any): ITask[];
    }
    var tasksListServiceProvider = function (): ITasksListServiceConfig {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';

        $get.$inject = ['$resource', '$window'];

        return {
            $get: $get,
            setUpdateInterval: function (milliseconds: number) {
                updateInterval = milliseconds;
                return this;
            }, setTasksListUrl: function (url: string) {
                tasksListUrl = url;
                return this;
            }

        };
        function $get($resource: ng.resource.IResourceService, $window: ng.IWindowService): ITasksListService {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource<ITask[]>(tasksListUrl, { cache: false }, {
                'get': {
                    method: 'GET', isArray: true
                }
            });
            var tasks: ITask[] = [];
            var update = function () {
                tasks = Tasks.get();
                updateDebugInfo(tasks);
            };
            var getActiveTasks = function (callback: (tasks: ITask[]) => any) {
                $window.setInterval(() => {
                    callback(tasks);
                    update();
                }, updateInterval);
                return tasks;
            };
            return {
                getActiveTasks
            };
        }
        function createDebugInfoUpdator(element: JQuery) {
            return (tasks: ITask[]) => element.html(tasks
                .map(task => '<div>' +
                Object.keys(task).map(key => '<span>&nbsp&nbsp' + task[key] + '</span>') +
                '</div>')
                .join());
        }
    } ();
    (function () {
        'use strict';
        angular
            .module(moduleName)
            .provider('tasksListService', tasksListServiceProvider);
    })();
}