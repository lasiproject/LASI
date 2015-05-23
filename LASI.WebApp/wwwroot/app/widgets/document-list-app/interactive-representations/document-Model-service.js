// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
var App;
(function (App) {
    'use strict';
    var DocumentModelService = (function () {
        function DocumentModelService($resource) {
            this.$resource = $resource;
        }
        DocumentModelService.prototype.getData = function () {
            return this.$resource('dummy-data/doc.json').get();
        };
        DocumentModelService.$inject = ['$resource'];
        return DocumentModelService;
    })();
    angular
        .module('interactiveRepresentations')
        .service('DocumentModelService', DocumentModelService);
})(App || (App = {}));
