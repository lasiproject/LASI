'use strict';

import navbarTemplate from 'app/sections/navbar/navbar.html';
import manageDocumentsModalTemplate from 'app/sections/document-manager/manage-modal.html';
import { ManageDocumentsModalController } from 'app/sections/document-manager/manage-modal';
import { UserService } from 'app/user-service';

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
            .then(user => this.user = user)
            .then(user => this.documentListService.get().then(documents=> this.documents = documents));
            //.catch(reason => {
            //    return this.$state.go('app.login');
            //});
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
    documents: DocumentListItem[];
    expanded = false;
    user: User;
    logoff() {
        return this.userService
            .logoff()
            .then(() => this.user = undefined)
            .finally(() => {
                return this.$state.go('app.login', {}, { reload: true });
            });
    }
} 