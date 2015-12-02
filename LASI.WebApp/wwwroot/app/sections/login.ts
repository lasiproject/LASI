'use strict';
import $ from 'jquery';
import { UserService } from 'app/user-service';
import { inject } from 'app/decorators';
@inject
export class LoginController {
    static $inject = ['$http', '$q', '$uibModal', '$state', 'UserService'];

    constructor(private $http: ng.IHttpService, private $q: ng.IQService, private $uibModal: ng.ui.bootstrap.IModalService, private $state: ng.ui.IStateService, private userService: UserService) {
        if (userService.user) {
            this.user = userService.user;
            this.username = this.user.email;
        }
        $('form[name="login-form"]').find($('input[name="__RequestVerificationToken"')).val($(document).find('input[name="__RequestVerificationToken"').val());
    }

    login(): PromiseLike<User> {
        var deferred = this.$q.defer<User>();
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $('form[name="login-form"]').find($(`input[name="${antiforgeryTokenName}"`)).val();
        this.userService
            .loginUser({
                email: this.username,
                password: this.password,
                antiforgeryTokenName,
                antiforgeryTokenValue
            })
            .then(user => {
                this.user = user;
                this.username = user.email;
                deferred.resolve(user);
            },
            error=> {
                deferred.reject(error);
                return this.$uibModal.open({ template: `An error ocurred and we were unable to log you in.<br/ >${error}` });

            }).then(() => this.$state.go('app.home'));

        return deferred.promise;
    }

    logoff(): PromiseLike<void> {
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $('form[name="login-form"]').find($(`input[name="${antiforgeryTokenName}"`)).val();

        return this.userService.logoff(antiforgeryTokenName, antiforgeryTokenValue);
    }
    username: string;
    password: string;
    user: User;
}