module LASI.documentList {

    'use strict';
    angular
        .module('documentList')
        .provider('tasksListService', tasksListServiceProvider);


    function tasksListServiceProvider(): ITasksListServiceConfig {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';

        $get.$inject = ['$q', '$resource', '$interval'];

        return { $get, setUpdateInterval, setTasksListUrl };

        function setUpdateInterval(milliseconds: number): ITasksListServiceConfig {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url: string): ITasksListServiceConfig {
            tasksListUrl = url;
            return this;
        }

        function $get($q: ng.IQService, $resource: ng.resource.IResourceService, $interval: ng.IIntervalService): ITasksListService {
            //var updateDebugInfo = function (tasks) { }; //createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource<ITask[]>(tasksListUrl, {}, {
                get: {
                    method: 'GET', isArray: true
                }
            });
            var getActiveTasks = function (callback?: (tasks: ITask[]) => any) {
                var deferred = $q.defer<ITask[]>();
                $interval(() => {
                    callback && callback(this.tasks);
                    this.tasks = Tasks.get();
                    deferred.resolve(this.tasks);
                }, updateInterval);
                return deferred.promise;
            };

            return {
                getActiveTasks,
                tasks: []
            };
        }
        function createDebugInfoUpdator(element: JQuery) {
            return (tasks: ITask[]) => element
                .html(tasks
                .map(task => `<div>${ Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`) }</div>`)
                .join());
        }
    }
    export interface ITasksListServiceConfig {
        $get: ($q, $resource: ng.resource.IResourceService, $interval: ng.IIntervalService) => ITasksListService;
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
        getActiveTasks(callback?: (tasks: ITask[]) => any): ng.IPromise<ITask[]>;
        tasks: ITask[];
    }
}