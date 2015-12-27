'use strict';
import { IQService, IIntervalService, IHttpService } from 'angular';
import { UserService } from 'app/user-service';

export function tasksListServiceProvider(): TasksListServiceProvider {
    var updateInterval = 200;
    var tasksListUrl = '/api/Tasks';
    var tasks: Task[];

    $get.$inject = ['$q', '$http', '$interval', 'UserService'];

    return { $get, setUpdateInterval, setTasksListUrl };

    function setUpdateInterval(milliseconds: number): TasksListServiceProvider {
        updateInterval = milliseconds;
        return this;
    }
    function setTasksListUrl(url: string): TasksListServiceProvider {
        tasksListUrl = url;
        return this;
    }

    function $get($q: IQService, $http: IHttpService, $interval: IIntervalService, userService: UserService): TasksListService {
        var deferred = $q.defer<Task[]>();
        return {
            getActiveTasks() {
                if (userService.loggedIn) {
                    $interval(() => $http.get<Task[]>(tasksListUrl, { headers: { ['accept']: 'application/json' } })
                        .then(response=> tasks = response.data)
                        .then(deferred.resolve.bind(deferred))
                        .catch(deferred.reject.bind(deferred)), updateInterval);
                } else {
                    deferred.reject('Must login to retrieve tasks');
                }
                return deferred.promise.catch(error => {
                    console.error(error);
                    return [];
                });
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
