'use strict';
import mainTemplate from 'app/main.html';
import MainController from 'app/main';
import homeTemplate from 'app/sections/home.html';
import HomeController from 'app/document-list/list-controller';

import loginTemplate from 'app/sections/login.html';

import AccountController from 'app/sections/account';
import accountTemplate from 'app/sections/account.html';

import LoginController from 'app/sections/login';
import { UserService } from 'app/user-service';

configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];
export default function configureStates($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) {
    user.$inject = ['UserService'];
    function user(userService: UserService) {
        return userService.getUser();
    }

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
                    resolve: {
                    }
                },
                'main': {
                    resolve: {
                    }
                },
                'navbar': {
                    name: 'navbar',
                    template: `<navbar user="resolved.user"></navbar>`,
                    controllerAs: 'resolved',
                    resolve: { user },
                    controller: class {
                        static $inject = ['user'];
                        constructor(private user: User) { }
                    },
                }
            }
        })
        .state({
            name: 'app.home',
            url: 'home',
            views: {
                'main': {
                    name: 'home',
                    controllerAs: 'home',
                    controller: HomeController,
                    template: homeTemplate
                }, 'navbar': {
                    name: 'navbar',
                    template: `<navbar user="resolved.user"></navbar>`,
                    controllerAs: 'resolved',
                    resolve: { user },
                    controller: class {
                        static $inject = ['user'];
                        constructor(private user: User) { }
                    }
                }
            }
        })
        .state({
            name: 'app.login',
            url: 'login',
            views: {
                'main': {
                    controller: LoginController,
                    controllerAs: 'login',
                    template: loginTemplate,
                    resolve: {
                    }
                },
                'navbar': {
                    name: 'navbar',
                    template: `<navbar user="resolved.user"></navbar>`,
                    controllerAs: 'resolved',
                    resolve: { user },
                    controller: class {
                        static $inject = ['user'];
                        constructor(private user: User) { }
                    }
                }
            }
        })
        .state({
            name: 'app.account',
            url: 'account',
            views: {
                'main': {
                    controller: AccountController,
                    controllerAs: 'account',
                    template: accountTemplate,
                    resolve: {
                    }
                },
                'navbar': {
                    name: 'navbar',
                    template: `<navbar user="resolved.user"></navbar>`,
                    controllerAs: 'resolved',
                    resolve: { user },
                    controller: class {
                        static $inject = ['user'];
                        constructor(private user: User) { }
                    }
                }
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