namespace LASI.documentList {
    'use strict';

    documentsService.$inject = ['$resource'];
    export interface DocumentsService {
        getbyId: (documentId: string) => DocumentListItem[];
        deleteById: (documentId: string) => DocumentListItem[];
    }
    function documentsService($resource: angular.resource.IResourceService): DocumentsService {
        var userDocouments = $resource<DocumentListItem[]>('api/UserDocuments/:documentId');
        function getbyId(documentId) {
            return userDocouments.get({ documentId });
        }
        function deleteById(documentId) {
            return userDocouments.delete({ documentId });
        }
        return {
            deleteById,
            getbyId
        };
    }
    angular
        .module('documentList')
        .factory({ documentsService });
}