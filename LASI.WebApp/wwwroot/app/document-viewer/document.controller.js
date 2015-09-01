var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentController = (function () {
            function DocumentController($q, $location, documentModelService) {
                this.$q = $q;
                this.$location = $location;
                this.documentModelService = documentModelService;
            }
            DocumentController.prototype.processDocument = function (documentId) {
                if (this.documentModel.id !== documentId) {
                    return this.documentModelService.processDocument(documentId);
                }
                else {
                    return this.$q.reject(this.documentModel);
                }
            };
            DocumentController.$inject = ['$q', '$location', 'MockDocumentModelService'];
            return DocumentController;
        })();
        angular
            .module('documentViewer')
            .controller('DocumentController', DocumentController);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
