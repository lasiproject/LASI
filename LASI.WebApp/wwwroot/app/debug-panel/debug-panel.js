/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../../../typings/angularjs/angular.d.ts" />
var LASI;
(function (LASI) {
    var debug;
    (function (debug) {
        'use strict';
        var DebugPanelController = (function () {
            function DebugPanelController($http, $interval) {
                var _this = this;
                this.tasks = [];
                $interval(function () {
                    $http.get('api/Tasks', '$interval')
                        .success(function (data) { return _this.tasks = data.sort(function (x, y) { return x.id.localeCompare(y.id); }); })
                        .error(function (error, status) { return LASI.log(status + ": " + error); });
                }, 50);
            }
            DebugPanelController.$inject = ['$http', '$interval'];
            return DebugPanelController;
        })();
        angular.module('debug', [])
            .controller('DebugPanelController', DebugPanelController);
    })(debug = LASI.debug || (LASI.debug = {}));
})(LASI || (LASI = {}));
