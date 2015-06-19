var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentModelService = (function () {
            function DocumentModelService($resource) {
                this.$resource = $resource;
                this.documentSource = $resource('Analysis/:documentId');
            }
            DocumentModelService.prototype.getData = function () {
                return this.$resource('tests/test-data/doc.json').get();
            };
            DocumentModelService.prototype.processDocument = function (documentId) {
                return this.documentSource.get({ documentId: documentId });
            };
            DocumentModelService.$inject = ['$resource'];
            return DocumentModelService;
        })();
        angular
            .module(documentViewer.moduleName)
            .service('DocumentModelService', DocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
