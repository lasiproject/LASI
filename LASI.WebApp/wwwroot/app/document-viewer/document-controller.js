var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentController = (function () {
            function DocumentController(documentModelService, $location) {
                this.documentModelService = documentModelService;
                this.$location = $location;
                this.title = 'DocumentController';
            }
            DocumentController.prototype.processDocument = function (documentId) {
                return this.documentModelService.processDocument(documentId);
            };
            DocumentController.$inject = ['MockDocumentModelService', '$location'];
            return DocumentController;
        })();
        angular
            .module('documentViewer')
            .controller('DocumentController', DocumentController);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
