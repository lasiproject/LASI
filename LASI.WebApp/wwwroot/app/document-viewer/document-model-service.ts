module LASI.documentViewer {
    'use strict';

    export interface DocumentModelService {
        processDocument(documentId: string): ng.IHttpPromise<models.DocumentModel>;
    }
    documentModelService.$inject = ['$http'];
    function documentModelService($http: ng.IHttpService): DocumentModelService {

        return {
            processDocument: (documentId) => $http.get(`Analysis/${documentId}`)
        };
    }
    angular
        .module('documentViewer')
        .factory('documentModelService', documentModelService);
}