var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        documentsService.$inject = ['$resource'];
        function documentsService($resource) {
            var userDocouments = $resource('api/UserDocuments/:documentId');
            function getbyId(documentId) {
                return userDocouments.get({ documentId: documentId });
            }
            function deleteById(documentId) {
                return userDocouments.delete({ documentId: documentId });
            }
            return {
                deleteById: deleteById,
                getbyId: getbyId
            };
        }
        angular
            .module('documentList')
            .factory({ documentsService: documentsService });
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
