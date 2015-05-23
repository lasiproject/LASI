// Install the angularjs.TypeScript.DefinitelyTyped NuGet package
module App {
    'use strict';

    interface IDocumentController {
        title: string;
        activate: () => void;
    }

    class DocumentController implements IDocumentController {
        title: string = 'DocumentController';
        private documentModel: App.IDocumentModel;
        static $inject: string[] = ['DocumentModelService', '$location'];

        constructor(private documentModelService: App.IDocumentModelService, private $location: ng.ILocationService) {
            this.activate();
        }

        activate() {
            this.documentModel = this.documentModelService.getData()
        }
    }

    angular
        .module('interactiveRepresentations')
        .controller('DocumentController', DocumentController);
}