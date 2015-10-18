'use strict';
define(["require", "exports"], function (require, exports) {
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
    exports.documentsService = documentsService;
});
