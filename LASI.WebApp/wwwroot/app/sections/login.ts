'use strict';

import $ from 'jquery';
import { UserService } from 'app/user-service';

export class LoginController {
    static $inject = ['$http', '$q', '$uibModal', '$state', 'UserService'];

    constructor(
        private $http: ng.IHttpService,
        private $q: ng.IQService,
        private $uibModal: ng.ui.bootstrap.IModalService,
        private $state: ng.ui.IStateService,
        private userService: UserService
    ) {
        this.rememberMe = this.user && this.user.rememberMe;
    }

    login(): ng.IPromise<any> {
        return this.userService.login({
            email: this.username,
            password: this.password,
            rememberMe: this.rememberMe || this.user && this.user.rememberMe
        }).then(user => {
            this.username = user.email;
            this.user = user;
            return this.$state.go('app.home', {}, { reload: true });
        }).catch(error => {
            return this.$uibModal.open({
                controller: class {
                    static $inject = ['$uibModalInstance'];
                    error = error;
                    constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
                    }
                    ok() { this.$uibModalInstance.close(); return this.$uibModalInstance.result; }
                },
                controllerAs: 'modal',
                template: `
                        <div class="modal-header">
                            <h3 class="modal-title">Login Failed</h3>
                        </div>
                        <div class="modal-body">
                            {{modal.error}}
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" ng-click="modal.ok()">OK</button>
                        </div>`
            }).result;
        });
    }


    user: User;
    username: string;
    password: string;
    rememberMe: boolean;
}