module LASI.documentList {

    'use strict';
    angular
        .module('documentList')
        .provider('tasksListService', tasksListServiceProvider);


    function tasksListServiceProvider(): ITasksListServiceConfig {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';

        $get.$inject = ['$resource', '$window'];

        return { $get, setUpdateInterval, setTasksListUrl };

        function setUpdateInterval(milliseconds: number): ITasksListServiceConfig {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url: string): ITasksListServiceConfig {
            tasksListUrl = url;
            return this;
        }

        function $get($resource: ng.resource.IResourceService, $window: ng.IWindowService): ITasksListService {
            var updateDebugInfo = createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource<ITask[]>(tasksListUrl, { cache: false }, {
                get: {
                    method: 'GET', isArray: true
                }
            });
            var tasks: ITask[] = [];

            var getActiveTasks = function (callback: (tasks: ITask[]) => any) {
                $window.setInterval(() => {
                    callback(tasks);
                    tasks = Tasks.get();
                    updateDebugInfo(tasks);
                }, updateInterval);
                return tasks;
            };
            return {
                getActiveTasks
            };
        }
        function createDebugInfoUpdator(element: JQuery) {
            return (tasks: ITask[]) => element.html(tasks.map(task => `<div>${ Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`) }</div>`)
                .join());
        }
    }
    export interface ITasksListServiceConfig {
        $get: ($resource: ng.resource.IResourceService, $window: ng.IWindowService) => ITasksListService;
        setTasksListUrl(url: string): ITasksListServiceConfig;
        setUpdateInterval(milliconds: number): ITasksListServiceConfig;
    }

    export interface ITask {
        id: string;
        name: string;
        state: string;
        percentComplete: number;
        statusMessage: string;
    }

    export interface ITasksListService {
        getActiveTasks(callback: (tasks: ITask[]) => any): ITask[];
    }
}