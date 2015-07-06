module LASI.documentViewer {
    'use strict';

    export interface IDocumentModelService {
        processDocument(documentId: string): ng.IHttpPromise<models.IDocumentModel>;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject = ['$http'];
        processDocument(documentId: string) {
            return this.$http.get(`Analysis/${documentId}`);
        }
        constructor(private $http: ng.IHttpService) { }
    }

    angular
        .module('documentViewer')
        .service('DocumentModelService', DocumentModelService);
}