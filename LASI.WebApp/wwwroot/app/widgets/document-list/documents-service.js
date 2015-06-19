var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module(LASI.documentList.moduleName)
            .factory('documentsService', documentsService);
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
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
