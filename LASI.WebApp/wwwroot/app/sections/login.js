'use strict';
System.register([], function(exports_1) {
    var LoginController;
    return {
        setters:[],
        execute: function() {
            LoginController = (function () {
                function LoginController($http, $q, data) {
                    this.$http = $http;
                    this.$q = $q;
                }
                LoginController.prototype.login = function () {
                    var _this = this;
                    var deferred = this.$q.defer();
                    this.$http
                        .post('/Account/Login', { email: this.username, password: this.password })
                        .then(function (response) {
                        _this.user = response.data;
                    })
                        .catch(console.error.bind(console));
                    return deferred.promise;
                };
                LoginController.$inject = ['$http', '$q', 'data'];
                return LoginController;
            })();
            exports_1("default", LoginController);
        }
    }
});
