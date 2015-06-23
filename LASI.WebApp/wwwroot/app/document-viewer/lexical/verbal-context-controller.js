// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var LASI;
(function (LASI) {
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
        .module(LASI.documentViewer.moduleName)
        .controller('VerbalContextController', VerbalContextController);
})(LASI || (LASI = {}));
//# sourceMappingURL=verbal-context-controller.js.map