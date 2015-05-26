// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    'use strict';

    export interface IDocumentModelService {
        getData: () => IDocumentModel;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];
        documentSource: ng.resource.IResourceClass<IDocumentModel>;
        constructor(private $resource: ng.resource.IResourceService) {
            this.documentSource = $resource<IDocumentModel>('Results/:documentId');
        }

        getData() {
            return this.$resource<IDocumentModel>('tests/dummy-data/doc.json').get();
        }
        processDocument(documentId: string) {
            return this.documentSource.get({ documentId });
        }
    }

    angular
        .module(LASI.documentViewer.ngName)
        .service('DocumentModelService', DocumentModelService);
}