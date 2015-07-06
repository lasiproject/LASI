module LASI.documentViewer {
    'use strict';


    interface IDocumentController {
        title: string;
        processDocument: (documentId: string) => ng.IPromise<models.IDocumentModel>;
    }

    class DocumentController implements IDocumentController {
        title: string = 'DocumentController';

        private documentModel: models.IDocumentModel;

        static $inject = ['$q', '$location', 'MockDocumentModelService'];

        constructor(private $q: ng.IQService,
            private $location: ng.ILocationService,
            private documentModelService: IDocumentModelService) {
        }
        processDocument(documentId: string) {
            if (this.documentModel.id !== documentId) {
                return this.documentModelService.processDocument(documentId);
            } else {
                return this.$q.reject(this.documentModel);
            }
        }
    }
    angular
        .module('documentViewer')
        .controller('DocumentController', DocumentController);

}