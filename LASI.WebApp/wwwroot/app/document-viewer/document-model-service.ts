module LASI.documentViewer {
    'use strict';

    export interface IDocumentModelService {
        getData(): IDocumentModel;
        processDocument(documentId: string): IDocumentModel;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];
        documentSource: ng.resource.IResourceClass<IDocumentModel>;
        constructor(private $resource: ng.resource.IResourceService) {
            this.documentSource = $resource<IDocumentModel>('Analysis/:documentId');
        }

        getData() {
            return this.$resource<IDocumentModel>('tests/test-data/doc.json').get();
        }
        processDocument(documentId: string) {
            return this.documentSource.get({ documentId });
        }
    }

    angular
        .module(moduleName)
        .service('DocumentModelService', DocumentModelService);
}