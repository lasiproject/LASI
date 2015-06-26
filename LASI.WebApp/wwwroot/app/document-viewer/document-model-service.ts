module LASI.documentViewer {
    'use strict';

    export interface IDocumentModelService {
        processDocument(documentId: string): models.IDocumentModel;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];
        documentSource: ng.resource.IResourceClass<models.IDocumentModel>;
        constructor(private $resource: ng.resource.IResourceService) {
            this.documentSource = $resource<models.IDocumentModel>('Analysis/:documentId');
        }
        processDocument(documentId: string) {
            return this.documentSource.get({ documentId });
        }
    }

    angular
        .module('documentViewer')
        .service('DocumentModelService', DocumentModelService);
}