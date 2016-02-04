'use strict';
import { UserService } from 'app/user-service';

export function logoff(): ng.IDirective {
    return {
        restrict: 'E',
        scope: true,
        controller: class {
            static $inject = ['$state', 'UserService'];
            constructor(private $state: ng.ui.IStateService, private userService: UserService) { }
            logoff() {
                return this.userService.logoff()
                    .then(() => this.user = undefined)
                    .finally(() => {
                        return this.$state.go('app.login', {}, { reload: true });
                    });
            }
            user: User;
        },
        controllerAs: 'logoffController',
        bindToController: {
            user: '='
        },
        template: `
            <form ng-submit="logoffController.logoff()" novalidate method="post" name="logout-form" id="logout-form" class="navbar-right">
                <button type="submit" class="navbar-inverse nav navbar-btn small"><i class="fa fa-sign-out fa-inverse"></i> <span style="color: white">Logoff</span></button>
            </form>
        `
    };
}