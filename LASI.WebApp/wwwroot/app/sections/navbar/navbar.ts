'use strict';

import ManageDocumentsModalController from 'app/sections/document-manager/manage-modal';
import manageDocumentsModalTemplate from 'app/sections/document-manager/manage-modal.html';

export default class NavbarController {
    static $inject = ['$uibModal', 'data'];

    constructor(private $uibModal: ng.ui.bootstrap.IModalService, data) { this.documents = data; }

    openManageDocumentsModal() {
        var navbarController = this;
        var modal = this.$uibModal.open({
            controllerAs: 'manager',
            controller: ManageDocumentsModalController,
            template: manageDocumentsModalTemplate,
            resolve: { data: () => this.documents },
            keyboard: true
            //size: 'lg'
        });
        modal.result.then(result => console.info(result));
    }
    // TODO: rename to toggleDropdownList
    toggleExpanded() {
        this.expanded = !this.expanded;
    }
    documents: any[];
    expanded = false;
}