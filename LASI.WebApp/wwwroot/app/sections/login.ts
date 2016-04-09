import $ from 'jquery';
import { UserService } from 'app/user-service';
import errorModalTemplate from 'app/widgets/error-modal.html';

export default class LoginController {
    static $inject = ['$http', '$uibModal', '$state', 'UserService'];
    constructor(
        private $http: ng.IHttpService,
        private $uibModal: ng.ui.bootstrap.IModalService,
        private $state: ng.ui.IStateService,
        private userService: UserService) { }

    login(): ng.IPromise<any> {
        return this.userService
            .login({
                email: this.username,
                password: this.password,
                rememberMe: this.rememberMe || this.user && this.user.rememberMe
            })
            .then(user => {
                [this.user, this.username, this.rememberMe] = [user, user.email, user.rememberMe];
                return this.$state.go('app.home', {}, { reload: true });
            })
            .catch(reason => {
                return this.$uibModal.open({
                    controllerAs: 'modal',
                    bindToController: true,
                    template: errorModalTemplate,
                    controller: class {
                        static $inject = ['$uibModalInstance'];
                        constructor(private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance) { }

                        header = 'Login Failed';

                        error = reason;

                        ok() {
                            this.$uibModalInstance.close();
                            return this.$uibModalInstance.result;
                        }
                    }
                }).result;
            });
    }


    user: User;
    username: string;
    password: string;
    rememberMe: boolean;
}