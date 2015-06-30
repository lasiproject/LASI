/// <reference path='../../../typings/jquery/jquery.d.ts' />
/// <reference path='../../../typings/angularjs/angular.d.ts' />

module LASI.debug {
    'use strict';


    DebugPanelController.$inject = ['tasksListService'];


    function DebugPanelController(tasksListService: documentList.ITasksListService) {
        tasksListService.getActiveTasks().then(data => this.tasks = data.sort((x, y) => x.id.localeCompare(y.id)));
        this.show = false;
        this.getbuttonText = () => this.show ? 'Hide' : 'Show';
        this.toggle = () => this.show = !this.show;

    }

    angular
        .module('debug', [])
        .controller('DebugPanelController', DebugPanelController);

}