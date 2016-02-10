export default class ConfirmDeleteModal {
    static $inject = ['$uibModalInstance', 'data'];
    constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, data: DocumentListItem) {
        this.document = data;
    }

    confirm() {
        this.$uibModalInstance.close(true);
    }

    cancel() {
        this.$uibModalInstance.close(false);
    }

    dismiss() {
        this.$uibModalInstance.dismiss();
    }

    document: DocumentListItem;
}