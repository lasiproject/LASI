'use strict';
System.register(['app/sections/document-manager/confirm-delete-modal.html', 'app/sections/document-manager/confirm-delete-modal'], function(exports_1) {
    var confirm_delete_modal_html_1, confirm_delete_modal_1;
    var DocumentManager;
    return {
        setters:[
            function (confirm_delete_modal_html_1_1) {
                confirm_delete_modal_html_1 = confirm_delete_modal_html_1_1;
            },
            function (confirm_delete_modal_1_1) {
                confirm_delete_modal_1 = confirm_delete_modal_1_1;
            }],
        execute: function() {
            DocumentManager = (function () {
                function DocumentManager($modalInstance, $modal, documentsService, documents) {
                    this.$modalInstance = $modalInstance;
                    this.$modal = $modal;
                    this.documentsService = documentsService;
                    this.documents = documents;
                    this.dismiss = this.$modalInstance.dismiss.bind(this.$modalInstance);
                }
                DocumentManager.prototype.deleteById = function (documentId) {
                    var _this = this;
                    var document = this.documents.first(function (d) { return d.id === documentId; });
                    var confirmDelete = this.$modal.open({
                        controller: confirm_delete_modal_1.default,
                        controllerAs: 'confirmDelete',
                        template: confirm_delete_modal_html_1.default,
                        resolve: { data: function () { return document; } }
                    });
                    return confirmDelete.result
                        .then(function (result) {
                        if (result === true) {
                            console.info("Delete: " + document.name + "...");
                            return _this.documentsService.deleteById(documentId);
                        }
                    })
                        .then(function () { return ("Successfully deleted: " + document.name + "."); })
                        .catch(function (error) { return console.error("Error deleting document " + document.name + "!\n" + error + "!"); });
                };
                DocumentManager.$inject = ['$modalInstance', '$uibModal', 'documentsService', 'data'];
                return DocumentManager;
            })();
            exports_1("default", DocumentManager);
        }
    }
});
