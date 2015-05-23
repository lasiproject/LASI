// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    'use strict';

    export interface IDocumentModelService {
        getData: () => IDocumentModel;
    }

    class DocumentModelService implements IDocumentModelService {
        static $inject: string[] = ['$resource'];

        constructor(private $resource: ng.resource.IResourceService) {
        }

        getData() {
            return this.$resource<IDocumentModel>('dummy-data/doc.json').get();
        }
    }

    angular
        .module('interactiveRepresentations')
        .service('DocumentModelService', DocumentModelService);
}