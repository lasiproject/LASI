'use strict';
import { IQService, IIntervalService, IHttpService } from 'angular';

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
    getActiveTasks(): Promise<Task[]>;
    tasks: Task[];
}
export function tasksListServiceProvider(): TasksListServiceProvider {
    var updateInterval = 200;
    var tasksListUrl = 'api/Tasks';
    var tasks: Task[];

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
        var deferred = $q.defer<Task[]>();
        return {
            getActiveTasks() {
                $interval(() => $http.get<Task[]>(tasksListUrl)
                    .then(response=> tasks = response.data)
                    .then(deferred.resolve.bind(deferred))
                    .catch(deferred.reject.bind(deferred)), updateInterval);
                return deferred.promise;
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
