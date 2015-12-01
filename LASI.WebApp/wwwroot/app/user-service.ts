'use strict';

import { User } from './models';
export default class UserService {
    static $inject = ['$q', '$http', '$state'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService, private $state: ng.ui.IStateService) { }

    loginUser(credentials: Credentials): PromiseLike<User> {
        var deferred = this.$q.defer<User>();

        var data = {
            Email: credentials.email,
            Password: credentials.password
        };

        data[credentials.antiforgeryTokenName] = credentials.antiforgeryTokenValue;
        var headers: { [name: string]: any } = { Accept: 'application/json' };
        headers[credentials.antiforgeryTokenName] = credentials.antiforgeryTokenValue;
        headers['Upgrade-Insecure-Requests'] = '1';
        headers['Content-Type'] = 'application/x-www-form-urlencoded';
        var config = {

            xsrfHeaderName: credentials.antiforgeryTokenName,
            headers: headers
        };
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post<User>('/Account/Login', angular.element.param(data), config)
            .then(response => {
                this.user = {
                    loggedIn: true,
                    email: credentials.email
                };
                this.loggedIn = true;

                deferred.resolve(this.user);
            })
            .catch(error=> {
                deferred.reject(error);
                console.error(error);
            })
            .finally(() => this.$state.current.name === 'app.home' ? this.$state.reload() : this.$state.go('app.home'));
        return deferred.promise;
    }

    logoff(antiforgeryTokenName, antiforgeryTokenValue): PromiseLike<void> {
        var deferred = this.$q.defer<void>();
        var data = {};
        data[antiforgeryTokenName] = antiforgeryTokenValue;
        var config: ng.IRequestShortcutConfig = {

            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        config.headers[antiforgeryTokenName] = antiforgeryTokenValue;
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post('/Account/LogOff', angular.element.param(data), config)
            .then(response => {
                this.user = undefined;
            })
            .catch(error => console.error(error))
            .finally(() => this.$state.current.name === 'app.login' ? this.$state.reload() : this.$state.go('app.login'));
        return deferred.promise;

    }
    getUser(): PromiseLike<User> {
        var deferred = this.$q.defer<User>();
        if (this.user) {
            deferred.resolve(this.user);
        } else {
            deferred.reject('not logged in');
        }

        return deferred.promise;
    }
    user: User;
    loggedIn = false;
}
interface Credentials {
    email: string;
    password: string;
    antiforgeryTokenName: string;
    antiforgeryTokenValue: string;
}