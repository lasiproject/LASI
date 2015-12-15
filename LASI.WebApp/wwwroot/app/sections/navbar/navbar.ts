'use strict';

import navbarTemplate from 'app/sections/navbar/navbar.html';
import { ManageDocumentsModalController } from 'app/sections/document-manager/manage-modal';
import manageDocumentsModalTemplate from 'app/sections/document-manager/manage-modal.html';
import { UserService } from 'app/user-service';

class NavbarController {
    static $inject = ['$uibModal', 'UserService'];

    constructor(private $uibModal: ng.ui.bootstrap.IModalService, private userService: UserService) {
        this.activate();
    }

    activate() {
        return this.userService.getUser().then(user => this.user = user, console.error.bind(console));
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
        const antiforgeryTokenValue = $('form[name="login-form"]').find($(`input[name="${antiforgeryTokenName}"`)).val();
        return this.userService.logoff(antiforgeryTokenName, antiforgeryTokenValue);
    }
}
export function navbar(): ng.IDirective {
    return {
        controller: NavbarController,
        controllerAs: 'navbar',
        template: navbarTemplate,
        bindToController: {
            documents: '=',
            user: '='
        },
        scope: true
    };
}