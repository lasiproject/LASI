/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../../../typings/angularjs/angular.d.ts" />

module LASI.debug {
    'use strict';

   
    class DebugPanelController {
        static $inject = ['$http', '$interval'];

        tasks: Task[] = [];

        constructor($http: ng.IHttpService, $interval: ng.IIntervalService) {
            $interval(() => {
                $http.get<Task[]>('api/Tasks', '$interval')
                    .success(data => this.tasks = data.sort((x, y) => x.id.localeCompare(y.id)))
                    .error((error, status) => log(`${status}: ${error}`));
            }, 50);
        }
    }

    interface Task {
        id: string;
        name: string;
        state: string;
        percentComplete: number;
        statusMessage: string;
    }
    angular.module('debug', [])
        .controller('DebugPanelController', DebugPanelController);

}