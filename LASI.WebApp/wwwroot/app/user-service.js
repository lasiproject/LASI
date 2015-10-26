'use strict';
System.register([], function(exports_1) {
    var UserService;
    return {
        setters:[],
        execute: function() {
            UserService = (function () {
                function UserService($http, $q) {
                    this.$http = $http;
                    this.$q = $q;
                }
                UserService.prototype.getUser = function () {
                    var _this = this;
                    if (this.user && this.user.loggedIn) {
                        return this.$q.when(this.user);
                    }
                    var deferred = this.$q.defer();
                    this.$http
                        .post('/Account/Login', { email: this.user.email, password: this.user.password })
                        .then(function (response) {
                        _this.user = response.data;
                        deferred.resolve(_this.user);
                    })
                        .catch(deferred.reject.bind(deferred));
                    return deferred.promise;
                };
                return UserService;
            })();
            exports_1("default", UserService);
        }
    }
});
