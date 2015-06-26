var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var MockDocumentModelService = (function () {
            function MockDocumentModelService($resource) {
                this.$resource = $resource;
                this.documentSource = $resource('Analysis/:documentId');
            }
            MockDocumentModelService.prototype.processDocument = function (documentId) {
                return this.$resource('tests/test-data/doc.json').get();
            };
            MockDocumentModelService.$inject = ['$resource'];
            return MockDocumentModelService;
        })();
        angular
            .module('documentViewer')
            .service('MockDocumentModelService', MockDocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
