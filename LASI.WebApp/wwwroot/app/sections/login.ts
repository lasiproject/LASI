import $ from 'jquery';
import { UserService } from 'app/user-service';
import errorModalTemplate from 'app/widgets/error-modal.html';
export default class LoginController {
    static $inject = ['$http', '$uibModal', '$state', 'UserService'];
    constructor(
        private $http: ng.IHttpService,
        private $uibModal: ng.ui.bootstrap.IModalService,
        private $state: ng.ui.IStateService,
        private userService: UserService) {
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
                bindToController: true,
                controller: class {
                    static $inject = ['$uibModalInstance'];
                    constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) {
                    }
                    header = 'Login Failed';
                    error = error;
                    ok() { this.$uibModalInstance.close(); return this.$uibModalInstance.result; }
                },
                controllerAs: 'modal',
                template: errorModalTemplate
            }).result;
        });
    }


    user: User;
    username: string;
    password: string;
    rememberMe: boolean;
}