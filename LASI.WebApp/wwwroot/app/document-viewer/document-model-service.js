var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        documentModelService.$inject = ['$http'];
        function documentModelService($http) {
            return {
                processDocument: function (documentId) { return $http.get("Analysis/" + documentId, { cache: true }); }
            };
        }
        angular
            .module('documentViewer')
            .factory('documentModelService', documentModelService);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
