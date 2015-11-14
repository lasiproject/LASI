'use strict';
import appTemplate from 'app/app.html';
import homeTemplate from 'app/sections/home/home.html';
import { ListController as HomeController } from 'app/document-list/list-controller';
import navbarTemplate from 'app/sections/navbar/navbar.html';
import loginTemplate from 'app/sections/login.html';
import AppController from 'app/app-controller';
import NavbarController from 'app/sections/navbar/navbar';
import LoginController from 'app/sections/login';
import UserService from 'app/user-service';
import { DocumentListService } from 'app/document-list/document-list-service-provider';
import { DocumentListItemModel } from 'app/document-list/document-list-service-provider';

import UrlRouterProvider = ng.ui.IUrlRouterProvider;
import StateProvider = ng.ui.IStateProvider;

configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];
export default function configureStates($stateProvider: StateProvider, $urlRouterProvider: UrlRouterProvider) {

    data.$inject = ['$q', 'documentListService'];
    function data($q: ng.IQService, documentListService: DocumentListService) {
        var deferred = $q.defer<DocumentListItemModel[]>();
        $q.when(documentListService.get()).then(deferred.resolve.bind(deferred));
        return deferred.promise;
    }
    user.$inject = ['$q', 'UserService'];

    function user($q: ng.IQService, userService: UserService) {
        var deferred = $q.defer();
        userService.getUser().then(deferred.resolve.bind(deferred), deferred.reject.bind(deferred));
        return deferred.promise;
    }
    $stateProvider
        .state({
            name: 'app',
            //abstract: true,
            controller: AppController,
            template: appTemplate,
            controllerAs: 'app',
            resolve: {
                data
            }, data: data,

            views: {
                navbar: {
                    name: 'navbar',
                    controller: NavbarController,
                    controllerAs: 'navbar',
                    template: navbarTemplate,
                    resolve: {
                        data
                    }, data: data
                },
                home: {
                    name: 'home',
                    controllerAs: 'controller',
                    controller: HomeController,
                    template: homeTemplate,
                    resolve: { data }, data: data
                },
                login: {
                    name: 'login',
                    controller: LoginController,
                    controllerAs: 'login',
                    template: loginTemplate,
                    resolve: { data }, data: data

                },
                logoff: {
                    name: 'logoff',
                    controller: LoginController,
                    controllerAs: 'logoff',
                    template: loginTemplate,
                    resolve: { data },
                    data: data

                }
            }
        });
    $urlRouterProvider.otherwise('');
}