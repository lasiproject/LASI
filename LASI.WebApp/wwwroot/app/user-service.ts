'use strict';
import 'angular';
export class UserService {
    static $inject = ['$q', '$http', '$state'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService, private $state: ng.ui.IStateService) { }

    loginUser({ email, password, antiforgeryTokenName, antiforgeryTokenValue }: Credentials): PromiseLike<User> {
        var { reject, resolve, promise } = this.$q.defer<User>();
        var data = {
            email,
            password,
            [antiforgeryTokenName]: antiforgeryTokenValue
        };


        var config: { [i: string]: any } = {
            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [antiforgeryTokenName]: antiforgeryTokenValue,
                'Upgrade-Insecure-Requests': '1',
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post<User>('/Account/Login', angular.element.param(data), config)
            .then(response => {
                const user = response.data;
                console.log('valid');
                this.loggedIn = true;
                this.user = user;
                resolve(user);
                return user;
            })
            .catch(error => {
                reject(error);
                console.error(error);
            });
        return promise;
    }

    logoff(antiforgeryTokenName: string, antiforgeryTokenValue: string): PromiseLike<void> {
        var { resolve, reject, promise } = this.$q.defer<void>();
        var data = {
            [antiforgeryTokenName]: antiforgeryTokenValue
        };
        var config: ng.IRequestShortcutConfig = {
            [antiforgeryTokenName]: antiforgeryTokenValue,
            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                [antiforgeryTokenName]: antiforgeryTokenValue,
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post<User>('/Account/LogOff', undefined, config)
            .then(response => {

                console.log('valid');
                console.log('Logged off');
                console.log(response);
                resolve();
                this.user = undefined;
                return this.user;

            })
            .then(() => this.$state.go('app.login'))
            .catch(error => reject(error));

        return promise;

    }
    getUser(): PromiseLike<User> {
        var {resolve, reject, promise } = this.$q.defer<User>();
        if (this.user) {
            resolve(this.user);
        } else {
            reject('not logged in');
        }
        return promise;
    }
    private user: User;
    loggedIn = false;
}

interface Credentials {
    email: string;
    password: string;
    antiforgeryTokenName: string;
    antiforgeryTokenValue: string;
}