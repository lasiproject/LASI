module LASI.documentList {
    'use strict';

    angular
        .module('documentList')
        .factory('documentsService', documentsService);

    documentsService.$inject = ['$resource'];
    export interface IDocumentsService {
        getbyId: (documentId: string) => IDocumentListItem[];
        deleteById: (documentId: string) => IDocumentListItem[];
    }
    function documentsService($resource: ng.resource.IResourceService): IDocumentsService {
        var userDocouments = $resource<IDocumentListItem[]>('api/UserDocuments/:documentId');
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