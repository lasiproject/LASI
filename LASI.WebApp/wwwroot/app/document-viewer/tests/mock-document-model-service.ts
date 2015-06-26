module LASI.documentViewer {
    'use strict';

    export interface IDocumentModelService {
        processDocument(documentId: string): models.IDocumentModel;
    }

    class MockDocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];
        documentSource: ng.resource.IResourceClass<models.IDocumentModel>;
        constructor(private $resource: ng.resource.IResourceService) {
            this.documentSource = $resource<models.IDocumentModel>('Analysis/:documentId');
        }
        processDocument(documentId: string) {
            return this.$resource<models.IDocumentModel>('tests/test-data/doc.json').get();
        }
    }

    angular
        .module('documentViewer')
        .service('MockDocumentModelService', MockDocumentModelService);
}