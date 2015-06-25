module LASI.documentViewer {
    'use strict';

    export interface IDocumentModelService {
        getData(): models.IDocumentModel;
        processDocument(documentId: string): models.IDocumentModel;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];
        documentSource: ng.resource.IResourceClass<models.IDocumentModel>;
        constructor(private $resource: ng.resource.IResourceService) {
            this.documentSource = $resource<models.IDocumentModel>('Analysis/:documentId');
        }

        getData() {
            return this.$resource<models.IDocumentModel>('tests/test-data/doc.json').get();
        }
        processDocument(documentId: string) {
            return this.documentSource.get({ documentId });
        }
    }

    angular
        .module('documentViewer')
        .service('DocumentModelService', DocumentModelService);
}