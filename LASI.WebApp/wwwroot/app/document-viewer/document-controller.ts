module LASI.documentViewer {
    'use strict';

    class DocumentController {
        title: string = 'DocumentController';

        private documentModel: models.DocumentModel;

        static $inject = ['$q', '$location', 'MockDocumentModelService'];

        constructor(private $q: ng.IQService,
            private $location: ng.ILocationService,
            private documentModelService: DocumentModelService) {
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