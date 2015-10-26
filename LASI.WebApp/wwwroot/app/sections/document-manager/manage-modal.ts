'use strict';
type DocumentListItem = LASI.documentList.DocumentListItem;

import { DocumentsService } from 'app/document-list/documents-service';
import confirmDeleteModalTemplate from 'app/sections/document-manager/confirm-delete-modal.html';
import ConfirmDeleteModalController from 'app/sections/document-manager/confirm-delete-modal';
export default class DocumentManager {
    static $inject = ['$modalInstance', '$uibModal', 'documentsService', 'data'];
    constructor(private $modalInstance: ng.ui.bootstrap.IModalServiceInstance, private $modal: ng.ui.bootstrap.IModalService, private documentsService: DocumentsService, private documents: DocumentListItem[]) { }

    deleteById(documentId: string) {
        let document = this.documents.first(d => d.id === documentId);
        let confirmDelete = this.$modal.open({
            controller: ConfirmDeleteModalController,
            controllerAs: 'confirmDelete',
            template: confirmDeleteModalTemplate,
            resolve: { data: () => document }
        });
        return confirmDelete.result
            .then(result => {
                if (result === true) {
                    console.info(`Delete: ${document.name}...`);
                    return this.documentsService.deleteById(documentId);
                }
            })
            .then(() => `Successfully deleted: ${document.name}.`)
            .catch(error=> console.error(`Error deleting document ${document.name}!\n${error}!`));
    }

    dismiss = this.$modalInstance.dismiss.bind(this.$modalInstance);
}