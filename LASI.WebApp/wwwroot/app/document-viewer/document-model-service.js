var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        var DocumentModelService = (function () {
            function DocumentModelService($http) {
                this.$http = $http;
            }
            DocumentModelService.prototype.processDocument = function (documentId) {
                return this.$http.get("Analysis/" + documentId);
            };
            DocumentModelService.$inject = ['$http'];
            return DocumentModelService;
        })();
        angular
            .module('documentViewer')
            .service('DocumentModelService', DocumentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
