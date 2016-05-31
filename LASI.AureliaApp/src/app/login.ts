import $ from 'jquery';
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

import {User} from 'models';
import {UserService} from './user-service';

@autoinject
export class Login {
    constructor(private userService: UserService) { }

    async login(): Promise<User> {
        this.user = await this.userService
            .login({
                email: this.username,
                password: this.password,
                rememberMe: this.rememberMe || this.user && this.user.rememberMe
            });

        [this.username, this.rememberMe] = [this.user.email, this.user.rememberMe];
        return this.user;

        // return this.$state.go('app.home', {}, { reload: true });
        // .catch(reason => {
        //     return this.$uibModal.open({
        //         controllerAs: 'modal',
        //         bindToController: true,
        //         template: errorModalTemplate,
        //         controller: class {
        //             static $inject = ['$uibModalInstance'];
        //             constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) { }

        //             header = 'Login Failed';

        //             error = reason;

        //             ok() {
        //                 this.$uibModalInstance.close();
        //                 return this.$uibModalInstance.result;
        //             }
        //         }
        //     }).result;
        // });
    }
    @bindable user: User;
    @bindable username: string;
    @bindable password: string;
    @bindable rememberMe: boolean;
}