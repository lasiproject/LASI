﻿'use strict';
import { IQService, IIntervalService, IPromise } from 'angular';
import { IResourceService } from 'angular-resource';

export interface TasksListServiceProvider {
    $get: ($q, $resource: IResourceService, $interval: IIntervalService) => TasksListService;
    setTasksListUrl(url: string): TasksListServiceProvider;
    setUpdateInterval(milliconds: number): TasksListServiceProvider;
}

export interface Task {
    id: string;
    name: string;
    state: string;
    percentComplete: number;
    statusMessage: string;
}

export interface TasksListService {
    getActiveTasks(): IPromise<Task[]>;
    tasks: Task[];
}
export function tasksListServiceProvider(): TasksListServiceProvider {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';

    $get.$inject = ['$q', '$resource', '$interval'];

    return { $get, setUpdateInterval, setTasksListUrl };

    function setUpdateInterval(milliseconds: number): TasksListServiceProvider {
        updateInterval = milliseconds;
        return this;
    }
    function setTasksListUrl(url: string): TasksListServiceProvider {
        tasksListUrl = url;
        return this;
    }

    function $get($q: IQService, $resource: IResourceService, $interval: IIntervalService): TasksListService {
        var Tasks = $resource<Task[]>(tasksListUrl, {}, {
            get: {
                method: 'GET', isArray: true
            }
        });
        var getActiveTasks = function () {
            var deferred = $q.defer<Task[]>();
            $interval(() => {
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
    function createDebugInfoUpdator(element: JQuery): (tasks: Task[]) => JQuery {
        return tasks => element.html(tasks.map(
            task => `<div>${Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`) }</div>`
        ).join());
    }
}
