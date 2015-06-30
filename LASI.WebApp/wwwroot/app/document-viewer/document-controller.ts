module LASI.documentViewer {
    'use strict';


    interface IDocumentController {
        title: string;
        processDocument: (documentId: string) => models.IDocumentModel;
    }

    class DocumentController implements IDocumentController {
        title: string = 'DocumentController';

        private documentModel: models.IDocumentModel;

        static $inject = ['MockDocumentModelService', '$location'];

        constructor(private documentModelService: IDocumentModelService, private $location: ng.ILocationService) {
        }
        processDocument(documentId: string) {
            if (this.documentModel.id !== documentId) {
                return this.documentModelService.processDocument(documentId);
            } else {
                return this.documentModel;
            }
        }
    }
    angular
        .module('documentViewer')
        .controller('DocumentController', DocumentController);

}