import confirmDeleteModalTemplate from './confirm-delete-modal.html';
import ConfirmDeleteModalController from './/confirm-delete-modal';

export default class ManageDocumentsModalController {
    static $inject = ['$uibModalInstance', '$uibModal', 'documentsService', 'documents'];
    constructor(
        private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance,
        private $modal: ng.ui.bootstrap.IModalService,
        private documentsService: DocumentsService,
        private documents: DocumentListItem[]) { }

    deleteById(documentId: string) {
        const document = this.documents.first(d => d.id === documentId);
        const confirmDelete = this.$modal.open({
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
            .catch(error => console.error(`Error deleting document ${document.name}!\n${error}!`));
    }

    dismiss = this.$uibModalInstance.dismiss.bind(this.$uibModalInstance);
}