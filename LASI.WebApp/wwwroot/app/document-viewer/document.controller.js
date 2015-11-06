System.register([], function(exports_1) {
    'use strict';
    var DocumentController;
    return {
        setters:[],
        execute: function() {
            DocumentController = (function () {
                function DocumentController($q, documentModelService) {
                    this.$q = $q;
                    this.documentModelService = documentModelService;
                }
                DocumentController.prototype.processDocument = function (id) {
                    if (this.documentModel.id !== id) {
                        return this.documentModelService.processDocument(id);
                    }
                    else {
                        return this.$q.when(this.documentModel);
                    }
                };
                DocumentController.$inject = ['$q', 'MockDocumentModelService'];
                return DocumentController;
            })();
            exports_1("DocumentController", DocumentController);
        }
    }
});
