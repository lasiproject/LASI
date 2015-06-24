var LASI;
(function (LASI) {
    'use strict';
    angular
        .module('documentViewer')
        .controller('VerbalContextController', VerbalContextController);
    var VerbalContextController = (function () {
        function VerbalContextController() {
            this.title = 'VerbalContextController';
        }
        //constructor(private $state: ng.ui.IStateService) {
        //    console.log($state);
        //    this.activate();
        //}
        VerbalContextController.prototype.activate = function () {
        };
        VerbalContextController.$inject = ['$state'];
        return VerbalContextController;
    })();
})(LASI || (LASI = {}));
