'use strict';
import { IQService, IIntervalService, IPromise, IHttpService } from 'angular';

export interface TasksListServiceProvider {
    $get: ($q: IQService, http: IHttpService, $interval: IIntervalService) => TasksListService;
    setTasksListUrl: (url: string) => TasksListServiceProvider;
    setUpdateInterval: (milliconds: number) => TasksListServiceProvider;
}

export interface Task {
    id: string;
    name: string;
    state: string;
    percentComplete: number;
    statusMessage: string;
}

export interface TasksListService {
    getActiveTasks(): Task[];
    tasks: Task[];
}
export function tasksListServiceProvider(): TasksListServiceProvider {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';
    var tasks: Task[] = [];

    $get.$inject = ['$q', '$http', '$interval'];

    return { $get, setUpdateInterval, setTasksListUrl };

    function setUpdateInterval(milliseconds: number): TasksListServiceProvider {
        updateInterval = milliseconds;
        return this;
    }
    function setTasksListUrl(url: string): TasksListServiceProvider {
        tasksListUrl = url;
        return this;
    }

    function $get($q: IQService, $http: IHttpService, $interval: IIntervalService): TasksListService {
        return {
            getActiveTasks() {
                var deferred = $q.defer<Task[]>();

                $interval(() => {
                    $http.get<Task[]>(tasksListUrl).success(ts => {
                        deferred.resolve(ts);
                        tasks = ts;
                    });

                }, updateInterval);
                return tasks;
            },
            tasks
        };
    }
    function createDebugInfoUpdator(element: JQuery): (tasks: Task[]) => JQuery {
        return tasks => element.html(tasks.map(
            task => `<div>${Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`)}</div>`
        ).join());
    }
}
