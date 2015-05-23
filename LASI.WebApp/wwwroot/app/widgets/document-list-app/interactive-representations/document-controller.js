// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    var DocumentController = (function () {
        function DocumentController(documentModelService, $location) {
            this.documentModelService = documentModelService;
            this.$location = $location;
            this.title = 'DocumentController';
            this.activate();
        }
        DocumentController.prototype.activate = function () {
            this.documentModel = this.documentModelService.getData();
        };
        DocumentController.$inject = ['DocumentModelService', '$location'];
        return DocumentController;
    })();
    angular
        .module('interactiveRepresentations')
        .controller('DocumentController', DocumentController);
})(App || (App = {}));
