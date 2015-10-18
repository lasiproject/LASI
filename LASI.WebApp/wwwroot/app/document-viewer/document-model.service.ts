'use strict';
import { DocumentModel } from 'app/models';
export interface DocumentModelService {
    processDocument(documentId: string): angular.IHttpPromise<DocumentModel>;
}
documentModelService.$inject = ['$http'];
export function documentModelService($http: angular.IHttpService): DocumentModelService {
    return {
        processDocument(documentId) {
            return $http.get(`Analysis/${documentId}`, { cache: false });
        }
    };
}