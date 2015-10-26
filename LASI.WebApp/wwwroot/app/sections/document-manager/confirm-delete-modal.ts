'use strict';
type DocumentListItem = LASI.documentList.DocumentListItem;
type IModalServiceInstance = ng.ui.bootstrap.IModalServiceInstance;

export default class ConfirmDeleteModal {
    static $inject = ['$modalInstance', 'data'];
    constructor(private $modalInstance: IModalServiceInstance, data: DocumentListItem) {
        this.document = data;
    }
    document: DocumentListItem;

    confirm() { this.$modalInstance.close(true); }
    cancel() { this.$modalInstance.close(false); }
    dismiss() { this.$modalInstance.dismiss(); }
}