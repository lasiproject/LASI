/// <reference path='../../../typings/jquery/jquery.d.ts' />
/// <reference path='../../../typings/angularjs/angular.d.ts' />
var LASI;
(function (LASI) {
    var debug;
    (function (debug) {
        'use strict';
        DebugPanelController.$inject = ['tasksListService'];
        function DebugPanelController(tasksListService) {
            var _this = this;
            tasksListService.getActiveTasks().then(function (data) { return _this.tasks = data.sort(function (x, y) { return x.id.localeCompare(y.id); }); });
            this.show = false;
            this.getbuttonText = function () { return _this.show ? 'Hide' : 'Show'; };
            this.toggle = function () { return _this.show = !_this.show; };
        }
        angular
            .module('debug', [])
            .controller('DebugPanelController', DebugPanelController);
    })(debug = LASI.debug || (LASI.debug = {}));
})(LASI || (LASI = {}));
