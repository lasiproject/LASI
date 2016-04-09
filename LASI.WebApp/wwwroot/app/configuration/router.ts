import mainTemplate from 'app/main.html';
import MainController from 'app/main';
import homeTemplate from 'app/sections/home.html';
import HomeController from 'app/document-list/list-controller';

import loginTemplate from 'app/sections/login.html';
import LoginController from 'app/sections/login';

import accountTemplate from 'app/sections/account.html';
import AccountController from 'app/sections/account';

import { UserService } from 'app/user-service';

configureRouter.$inject = ['$stateProvider', '$urlRouterProvider'];
export default function configureRouter($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) {

    const resolve = {
        user: (function () {
            let cachedUser: User = undefined;
            var user = function user(userService: UserService) {
                return cachedUser
                    ? cachedUser
                    : userService.getUser().then(user => cachedUser = user);
            }
            user.$inject = ['UserService'];
            return user;
        })()
    };

    const navbar: ng.ui.IState = {
        name: 'navbar',
        template: `<navbar user="resolved.user"></navbar>`,
        controllerAs: 'resolved',
        resolve,
        controller: class {
            static $inject = ['user'];
            constructor(private user: User) { }
        }
    };
    $stateProvider
        .state({
            url: '/',
            name: 'app',
            abstract: true,
            views: {
                '': {
                    controller: MainController,
                    controllerAs: 'app',
                    template: mainTemplate,
                    resolve
                },
                main: { resolve },
                navbar
            }
        })
        .state({
            name: 'app.home',
            url: 'home',
            views: {
                main: {
                    name: 'home',
                    controllerAs: 'home',
                    controller: HomeController,
                    template: homeTemplate
                },
                navbar
            }
        })
        .state({
            name: 'app.login',
            url: 'login',
            views: {
                main: {
                    controller: LoginController,
                    controllerAs: 'login',
                    template: loginTemplate,
                    resolve
                },
                navbar
            }
        })
        .state({
            name: 'app.account',
            url: 'account',
            views: {
                main: {
                    controller: AccountController,
                    controllerAs: 'account',
                    template: accountTemplate,
                    resolve
                },
                navbar
            }
        })
        .state({
            name: 'app.logoff',
            controller: LoginController,
            controllerAs: 'logoff',
            template: loginTemplate
        });

    $urlRouterProvider.otherwise('/home');
}