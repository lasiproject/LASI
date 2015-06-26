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
            DocumentModelService.prototype.processDocument = function (documentId) {
                return this.documentSource.get({ documentId: documentId });
            };
            DocumentModelService.$inject = ['$resource'];
            return DocumentModelService;
        })();
        angular
            .module('documentViewer')
            .service('DocumentModelService', DocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
