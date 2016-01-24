'use strict';

export function tasksListServiceProvider(): TasksListServiceProvider {
    var updateInterval = 200;
    var tasksListUrl = '/api/Tasks';
    var tasks: Task[] = [];

    $get.$inject = ['$q', '$http', '$interval', 'UserService'];

    return {
        $get, 
        setUpdateInterval, 
        setTasksListUrl
    };

    function setUpdateInterval(milliseconds: number): TasksListServiceProvider {
        updateInterval = milliseconds;
        return this;
    }

    function setTasksListUrl(url: string): TasksListServiceProvider {
        tasksListUrl = url;
        return this;
    }

    function $get($q: ng.IQService, $http: ng.IHttpService, $interval: ng.IIntervalService, userService: UserService): TasksListService {
        var { resolve, reject, promise } = $q.defer<Task[]>();
        return {
            tasks,
            getActiveTasks() {
                if (userService.loggedIn) {
                    var intervalPromise = $interval(() => {
                        if (!userService.loggedIn) {
                            $interval.cancel(intervalPromise);
                        }
                        else {
                            return $http.get<Task[]>(tasksListUrl)
                                .then(response => tasks = response.data)
                                .then(data => resolve(data))
                                .catch(reason => reject(reason));
                        }
                    }, updateInterval);
                } else {
                    reject('Must login to retrieve tasks');
                }
                return $q.resolve(tasks);
            }
        };
    }
    function createDebugInfoUpdator(element: JQuery): (tasks: Task[]) => JQuery {
        return tasks => element.html(tasks.map(task => 
            `<div>${Object.keys(task).map(key => `<span>&nbsp&nbsp${task[key]}</span>`)}</div>`
        ).join());
    }
}
