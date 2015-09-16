namespace LASI.documentViewer {
    'use strict';

    class DocumentController {
        private documentModel: models.DocumentModel;

        static $inject = ['$q', '$location', 'MockDocumentModelService'];

        constructor(private $q: angular.IQService,
            private $location: angular.ILocationService,
            private documentModelService: DocumentModelService) {
        }
        processDocument(id: string) {
            if (this.documentModel.id !== id) {
                return this.documentModelService.processDocument(id);
            } else {
                return this.$q.reject(this.documentModel);
            }
        }
    }
    angular
        .module('documentViewer')
        .controller({ DocumentController });

}