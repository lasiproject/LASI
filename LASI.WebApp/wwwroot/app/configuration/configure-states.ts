'use strict';
import appTemplate from 'app/app.html';
import { AppController } from 'app/app-controller';
import homeTemplate from 'app/sections/home/home.html';
import { ListController as HomeController } from 'app/document-list/list-controller';

import loginTemplate from 'app/sections/login.html';

import { NavbarController } from 'app/sections/navbar/navbar';
import navbarTemplate from 'app/sections/navbar/navbar.html';

import { AccountController} from 'app/sections/account';
import accountTemplate from 'app/sections/account.html';

import { LoginController } from 'app/sections/login';
import { UserService } from 'app/user-service';

configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];
export default function configureStates($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) {
    $stateProvider
        .state({
            url: '/',
            name: 'app',
            //abstract: true,
            views: {
                '': {
                    controller: AppController,
                    controllerAs: 'app',
                    template: appTemplate,
                    resolve: {
                    }
                },
                'main': {
                    resolve: {
                    }
                },
                'navbar': {
                    name: 'navbar',
                    controller: NavbarController,
                    controllerAs: 'navbar',
                    template: navbarTemplate
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
                    controller: NavbarController,
                    controllerAs: 'navbar',
                    template: navbarTemplate
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
                    controller: NavbarController,
                    controllerAs: 'navbar',
                    template: navbarTemplate,

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
                    controller: NavbarController,
                    controllerAs: 'navbar',
                    template: navbarTemplate,

                }
            }
        })
        .state({
            name: 'app.logoff',
            controller: LoginController,
            controllerAs: 'logoff',
            template: loginTemplate
        });

    $urlRouterProvider.otherwise('/');
}