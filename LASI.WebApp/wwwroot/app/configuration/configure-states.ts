'use strict';
import appTemplate from 'app/app.html';
import { AppController } from 'app/app-controller';
import homeTemplate from 'app/sections/home/home.html';
import { ListController as HomeController } from 'app/document-list/list-controller';

import loginTemplate from 'app/sections/login.html';

import { NavbarController } from 'app/sections/navbar/navbar';
import navbarTemplate from 'app/sections/navbar/navbar.html';

import { LoginController } from 'app/sections/login';
import { UserService } from 'app/user-service';
import { DocumentListService } from 'app/document-list/document-list-service-provider';

configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];
export default function configureStates($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) {

    documents.$inject = ['$q', 'documentListService'];
    function documents($q: ng.IQService, documentListService: DocumentListService) {
        var deferred = $q.defer<DocumentListItemModel[]>();
        documentListService.get()
            .then(deferred.resolve.bind(deferred), deferred.reject.bind(deferred));
        return deferred.promise;
    }
    tasks.$inject = ['tasksListService'];
    function tasks(tasksListService: TasksListService) {

        return tasksListService.getActiveTasks()
            .then(tasks => this.tasks = tasks.sort((x, y) => x.id.localeCompare(y.id)), console.error.bind(console));
    }

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
                        documents,
                        tasks
                    }
                },
                'main': {
                    resolve: {
                        documents,
                        tasks
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
                    template: homeTemplate,
                    resolve: {
                        documents,
                        tasks
                    }
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
                        documents,
                        tasks
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