export default class ConfirmDeleteModal {
    static $inject = ['$uibModalInstance', 'document'];
    constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, private document: DocumentListItem) { }

    confirm() {
        this.$uibModalInstance.close(true);
    }

    cancel() {
        this.$uibModalInstance.close(false);
    }

    dismiss() {
        this.$uibModalInstance.dismiss();
    }
}