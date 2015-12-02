'use strict';

type IModalServiceInstance = ng.ui.bootstrap.IModalServiceInstance;

export default class ConfirmDeleteModal {
    static $inject = ['$uibModalInstance', 'data'];
    constructor(private $uibModalInstance: IModalServiceInstance, data: DocumentListItem) {
        this.document = data;
    }
    document: DocumentListItem;

    confirm() { this.$uibModalInstance.close(true); }
    cancel() { this.$uibModalInstance.close(false); }
    dismiss() { this.$uibModalInstance.dismiss(); }
}