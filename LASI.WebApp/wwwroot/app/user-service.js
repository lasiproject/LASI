System.register([], function(exports_1) {
    'use strict';
    var UserService;
    return {
        setters:[],
        execute: function() {
            UserService = (function () {
                function UserService($http, $q) {
                    this.$http = $http;
                    this.$q = $q;
                    this.loggedIn = false;
                }
                UserService.prototype.loginUser = function (credentials) {
                    var _this = this;
                    var deferred = this.$q.defer();
                    var data = {
                        Email: credentials.email,
                        Password: credentials.password
                    };
                    data[credentials.antiforgeryTokenName] = credentials.antiforgeryTokenValue;
                    var headers = {};
                    headers[credentials.antiforgeryTokenName] = credentials.antiforgeryTokenValue;
                    headers['Upgrade-Insecure-Requests'] = '1';
                    headers['Content-Type'] = 'application/x-www-form-urlencoded';
                    var config = {
                        xsrfHeaderName: credentials.antiforgeryTokenName,
                        headers: headers
                    };
                    // TODO: Remove angular.element.param(data) as it silently depends on jQuery
                    this.$http
                        .post('/Account/Login', angular.element.param(data), config)
                        .then(function (response) {
                        _this.loggedIn = true;
                        deferred.resolve();
                    })
                        .catch(function (error) {
                        deferred.reject(error);
                        console.error(error);
                    });
                    return deferred.promise;
                };
                UserService.prototype.logoff = function (_a) {
                    var _this = this;
                    var antiforgeryTokenName = _a.antiforgeryTokenName, antiforgeryTokenValue = _a.antiforgeryTokenValue;
                    var deferred = this.$q.defer();
                    var data = {};
                    data[antiforgeryTokenName] = antiforgeryTokenValue;
                    var config = {
                        xsrfHeaderName: antiforgeryTokenName,
                        headers: {
                            'Content-Type': 'application/x-www-form-urlencoded'
                        }
                    };
                    config.headers[antiforgeryTokenName] = antiforgeryTokenValue;
                    // TODO: Remove angular.element.param(data) as it silently depends on jQuery
                    this.$http
                        .post('/Account/LogOff', angular.element.param(data), config)
                        .then(function (response) {
                        _this.user = undefined;
                    })
                        .catch(function (error) { return console.error(error); });
                    return deferred.promise;
                };
                UserService.prototype.getUser = function () {
                    var deferred = this.$q.defer();
                    if (this.user) {
                        deferred.resolve(this.user);
                    }
                    else {
                        deferred.reject('not logged in');
                    }
                    return deferred.promise;
                };
                UserService.$inject = ['$http', '$q'];
                return UserService;
            })();
            exports_1("default", UserService);
        }
    }
});
