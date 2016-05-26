import $ from 'jquery';
import {autoinject, bindable} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import { UserService } from './user-service';
@autoinject
export class Login {
    constructor(
        private http: HttpClient,
        private userService: UserService) { }

   async login(): Promise<any> {
       return await this.userService
           .login({
               email: this.username,
               password: this.password,
               rememberMe: this.rememberMe || this.user && this.user.rememberMe
           })
           .then(user => {
               [this.user, this.username, this.rememberMe] = [user, user.email, user.rememberMe];
                // return this.$state.go('app.home', {}, { reload: true });
            });
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


    @bindable user: models.User;
    @bindable username: string;
    @bindable password: string;
    @bindable rememberMe: boolean;
}