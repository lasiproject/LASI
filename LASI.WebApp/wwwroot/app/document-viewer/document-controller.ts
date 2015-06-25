module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer')
        .controller('DocumentController', DocumentController);

    interface IDocumentController {
        title: string;
        processDocument: (documentId: string) => models.IDocumentModel;
        activate: () => void;
    }

    class DocumentController implements IDocumentController {
        title: string = 'DocumentController';
        private documentModel: models.IDocumentModel;
        static $inject = ['DocumentModelService', '$location'];

        constructor(private documentModelService: IDocumentModelService, private $location: ng.ILocationService) {
            this.activate();
        }
        processDocument(documentId: string) {
            return this.documentModelService.processDocument(documentId);
        }
        activate() {
            //this.documentModel = this.documentModelService.getData();
        }
    }

}