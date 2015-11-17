System.register(['jquery'], function(exports_1) {
    'use strict';
    var jquery_1;
    var LoginController;
    return {
        setters:[
            function (jquery_1_1) {
                jquery_1 = jquery_1_1;
            }],
        execute: function() {
            LoginController = (function () {
                function LoginController($http, $q, $uibModal, $state, userService) {
                    this.$http = $http;
                    this.$q = $q;
                    this.$uibModal = $uibModal;
                    this.$state = $state;
                    this.userService = userService;
                    if (userService.user) {
                        this.user = userService.user;
                        this.username = this.user.email;
                    }
                    jquery_1.default('form[name="login-form"]').find(jquery_1.default('input[name="__RequestVerificationToken"')).val(jquery_1.default(document).find('input[name="__RequestVerificationToken"').val());
                }
                LoginController.prototype.login = function () {
                    var _this = this;
                    var deferred = this.$q.defer();
                    var antiforgeryTokenName = '__RequestVerificationToken';
                    var antiforgeryTokenValue = jquery_1.default('form[name="login-form"]').find(jquery_1.default("input[name=\"" + antiforgeryTokenName + "\"")).val();
                    this.userService
                        .loginUser({
                        email: this.username,
                        password: this.password,
                        antiforgeryTokenName: antiforgeryTokenName,
                        antiforgeryTokenValue: antiforgeryTokenValue
                    })
                        .then(function () {
                        deferred.resolve();
                        return _this.$state.go('app.home');
                    }, function (error) {
                        deferred.reject(error);
                        return _this.$uibModal.open({ template: "An error ocurred and we were unable to log you in.<br/ >" + error });
                    });
                    return deferred.promise;
                };
                LoginController.prototype.logoff = function () {
                    var antiforgeryTokenName = '__RequestVerificationToken';
                    var antiforgeryTokenValue = jquery_1.default('form[name="login-form"]').find(jquery_1.default("input[name=\"" + antiforgeryTokenName + "\"")).val();
                    return this.userService.logoff({ antiforgeryTokenName: antiforgeryTokenName, antiforgeryTokenValue: antiforgeryTokenValue });
                };
                LoginController.$inject = ['$http', '$q', '$uibModal', '$state', 'UserService'];
                return LoginController;
            })();
            exports_1("default", LoginController);
        }
    }
});
