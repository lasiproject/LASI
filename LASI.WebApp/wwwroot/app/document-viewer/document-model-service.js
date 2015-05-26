// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    var DocumentModelService = (function () {
        function DocumentModelService($resource) {
            this.$resource = $resource;
            this.documentSource = $resource('Results/:documentId');
        }
        DocumentModelService.prototype.getData = function () {
            return this.$resource('tests/dummy-data/doc.json').get();
        };
        DocumentModelService.prototype.processDocument = function (documentId) {
            return this.documentSource.get({ documentId: documentId });
        };
        DocumentModelService.$inject = ['$resource'];
        return DocumentModelService;
    })();
    angular
        .module(LASI.documentViewer.ngName)
        .service('DocumentModelService', DocumentModelService);
})(App || (App = {}));
