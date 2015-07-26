module LASI.documentList {
    'use strict';
    angular
        .module('documentList')
        .provider('tasksListService', tasksListServiceProvider);


    function tasksListServiceProvider(): TasksListServiceConfig {
        var updateInterval = 200;
        var tasksListUrl = 'api/Tasks';

        $get.$inject = ['$q', '$resource', '$interval'];

        return { $get, setUpdateInterval, setTasksListUrl };

        function setUpdateInterval(milliseconds: number): TasksListServiceConfig {
            updateInterval = milliseconds;
            return this;
        }
        function setTasksListUrl(url: string): TasksListServiceConfig {
            tasksListUrl = url;
            return this;
        }

        function $get($q: angular.IQService, $resource: angular.resource.IResourceService, $interval: angular.IIntervalService): TasksListService {
            //var updateDebugInfo = function (tasks) { }; //createDebugInfoUpdator($('#debug-panel'));
            var Tasks = $resource<Task[]>(tasksListUrl, {}, {
                get: {
                    method: 'GET', isArray: true
                }
            });
            var getActiveTasks = function (callback?: (tasks: Task[]) => any) {
                var deferred = $q.defer<Task[]>();
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
            return (tasks: Task[]) => element
                .html(tasks
                    .map(task => `<div>${ Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`) }</div>`)
                    .join());
        }
    }
    export interface TasksListServiceConfig {
        $get: ($q, $resource: angular.resource.IResourceService, $interval: angular.IIntervalService) => TasksListService;
        setTasksListUrl(url: string): TasksListServiceConfig;
        setUpdateInterval(milliconds: number): TasksListServiceConfig;
    }

    export interface Task {
        id: string;
        name: string;
        state: string;
        percentComplete: number;
        statusMessage: string;
    }

    export interface TasksListService {
        getActiveTasks(callback?: (tasks: Task[]) => any): angular.IPromise<Task[]>;
        tasks: Task[];
    }
}