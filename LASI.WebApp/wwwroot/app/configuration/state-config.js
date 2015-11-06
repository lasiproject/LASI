'use strict';
System.register(['app/app.html', 'app/sections/home/home.html', 'app/document-list/list-controller', 'app/sections/navbar/navbar.html', 'app/sections/login.html', 'app/app-controller', 'app/sections/navbar/navbar', 'app/sections/login'], function(exports_1) {
    var app_html_1, home_html_1, list_controller_1, navbar_html_1, login_html_1, app_controller_1, navbar_1, login_1;
    function configureStates($stateProvider, $urlRouterProvider) {
        data.$inject = ['$q', 'documentListService'];
        function data($q, documentListService) {
            var deferred = $q.defer();
            $q.when(documentListService.get()).then(deferred.resolve.bind(deferred));
            return deferred.promise;
        }
        user.$inject = ['$q', 'UserService'];
        function user($q, userService) {
            var deferred = $q.defer();
            userService.getUser().then(deferred.resolve.bind(deferred), deferred.reject.bind(deferred));
            return deferred.promise;
        }
        $stateProvider
            .state({
            name: 'app',
            //abstract: true,
            controller: app_controller_1.default,
            template: app_html_1.default,
            controllerAs: 'app',
            resolve: {
                data: data
            }, data: data,
            views: {
                'app.navbar': {
                    name: 'app.navbar',
                    controller: navbar_1.default,
                    controllerAs: 'navbar',
                    template: navbar_html_1.default,
                    resolve: {
                        data: data
                    }, data: data
                },
                'app.home': {
                    name: 'app.home',
                    controllerAs: 'controller',
                    controller: list_controller_1.ListController,
                    template: home_html_1.default,
                    resolve: { data: data }, data: data
                },
                'app.login': {
                    name: 'app.login',
                    controller: login_1.default,
                    controllerAs: 'login',
                    template: login_html_1.default,
                    resolve: { data: data }, data: data
                },
                'app.logoff': {
                    name: 'app.logoff',
                    controller: login_1.default,
                    controllerAs: 'logoff',
                    template: login_html_1.default,
                    resolve: { data: data },
                    data: data
                }
            }
        });
        $urlRouterProvider.otherwise('');
    }
    exports_1("default", configureStates);
    return {
        setters:[
            function (app_html_1_1) {
                app_html_1 = app_html_1_1;
            },
            function (home_html_1_1) {
                home_html_1 = home_html_1_1;
            },
            function (list_controller_1_1) {
                list_controller_1 = list_controller_1_1;
            },
            function (navbar_html_1_1) {
                navbar_html_1 = navbar_html_1_1;
            },
            function (login_html_1_1) {
                login_html_1 = login_html_1_1;
            },
            function (app_controller_1_1) {
                app_controller_1 = app_controller_1_1;
            },
            function (navbar_1_1) {
                navbar_1 = navbar_1_1;
            },
            function (login_1_1) {
                login_1 = login_1_1;
            }],
        execute: function() {
            configureStates.$inject = ['$stateProvider', '$urlRouterProvider'];
        }
    }
});
