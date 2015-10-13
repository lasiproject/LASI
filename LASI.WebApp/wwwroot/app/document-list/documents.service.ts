'use strict';
import { IResourceService } from 'angular-resource';

type DocumentListItem = LASI.documentList.DocumentListItem;
documentsService.$inject = ['$resource'];
export interface DocumentsService {
    getbyId(documentId: string): DocumentListItem[];
    deleteById(documentId: string): DocumentListItem[];
}
export function documentsService($resource: IResourceService): DocumentsService {
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
