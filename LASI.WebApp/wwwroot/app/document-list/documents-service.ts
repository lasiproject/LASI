module LASI.documentList {
    'use strict';

    angular
        .module('documentList')
        .factory('documentsService', documentsService);

    documentsService.$inject = ['$resource'];
    export interface DocumentsService {
        getbyId: (documentId: string) => DocumentListItem[];
        deleteById: (documentId: string) => DocumentListItem[];
    }
    function documentsService($resource: ng.resource.IResourceService): DocumentsService {
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
}