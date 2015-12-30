'use strict';

import navbarTemplate from 'app/sections/navbar/navbar.html';
import manageDocumentsModalTemplate from 'app/sections/document-manager/manage-modal.html';
import { ManageDocumentsModalController } from 'app/sections/document-manager/manage-modal';
import { UserService } from 'app/user-service';
import { DocumentListService } from 'app/document-list/document-list-service-provider';

export class NavbarController {
    static $inject = ['$state', '$uibModal', 'UserService', 'documentListService'];

    constructor(
        private $state: ng.ui.IStateService,
        private $uibModal: ng.ui.bootstrap.IModalService,
        private userService: UserService,
        private documentListService: DocumentListService
    ) {
        this.activate();
    }

    activate() {
        return this.userService.getUser()
            .then(user => this.user = user, console.error.bind(console))
            .then(user => this.documentListService.get().then(documents=> this.documents = documents));
    }
    openManageDocumentsModal() {
        var navbarController = this;
        var modal = this.$uibModal.open({
            controllerAs: 'manager',
            controller: ManageDocumentsModalController,
            template: manageDocumentsModalTemplate,
            resolve: { documents: () => this.documents }
        });
        return modal.result.then(result => console.info(result));
    }
    // TODO: rename to toggleDropdownList
    toggleExpanded() {
        this.expanded = !this.expanded;
    }
    documents: DocumentListItemModel[];
    expanded = false;
    user: User;
    logoff() {
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $(document).find($(`input[name="${antiforgeryTokenName}"`)).val();
        return this.userService
            .logoff()
            .then(() => this.user = undefined)
            .finally(() => {
                return this.$state.transitionTo(this.$state.current.name, {}, { reload: true })
                    .then(window.location.reload.bind(window, true));

            });
    }
} 