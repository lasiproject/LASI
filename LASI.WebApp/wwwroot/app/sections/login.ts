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
    ) { }

    login(): PromiseLike<User> {
        const deferred = this.$q.defer<User>();
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $(document).find($(`input[name="${antiforgeryTokenName}"`)).val();
        this.userService.loginUser({
            email: this.username,
            password: this.password,
            antiforgeryTokenName,
            antiforgeryTokenValue
        }).then(user => {
            this.username = user.email;
            deferred.resolve(user);
            this.user = user;
        }, error => {
            deferred.reject(error);
            console.error(error);
        });
        return deferred.promise;
    }

    logoff(): PromiseLike<void> {
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $('form[name="login-form"]').find($(`input[name="${antiforgeryTokenName}"`)).val();

        return this.userService.logoff(antiforgeryTokenName, antiforgeryTokenValue);
    }
    user: User;
    username: string;
    password: string;
}