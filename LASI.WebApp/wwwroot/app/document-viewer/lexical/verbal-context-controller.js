// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
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
    angular
        .module(LASI.documentViewer.ngName)
        .controller('VerbalContextController', VerbalContextController);
})(App || (App = {}));
