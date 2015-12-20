'use strict';
import 'angular';
export class UserService {
    static $inject = ['$q', '$http', '$state'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService) { }

    loginUser({ email, password, antiforgeryToken }: Credentials): ng.IPromise<User> {
        var { reject, resolve, promise } = this.$q.defer<User>();
        var data = {
            email,
            password,
            [this.antiforgeryTokenName]: this.token || antiforgeryToken
        };


        var config: { [i: string]: any } = {
            xsrfHeaderName: this.antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [this.antiforgeryTokenName]: this.token,
                'Upgrade-Insecure-Requests': '1',
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post<User>('/Account/Login', angular.element.param(data), config)
            .then(response => {
                const user = response.data;
                this.loggedIn = true;
                this.user = user;
                resolve(user);
                this.token = response.data.token;
                return user;
            })
            .catch(error => {
                reject(error);
                console.error(error);
            });
        return promise;
    }

    logoff(antiforgeryTokenName: string, antiforgeryTokenValue: string): ng.IPromise<any> {
        var { resolve, reject, promise } = this.$q.defer();
        var data = {
            [antiforgeryTokenName]: this.token
        };
        var config: ng.IRequestShortcutConfig = {
            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [antiforgeryTokenName]: this.token,
                'Content-Type': 'application/x-www-form-urlencoded',
                'Upgrade-Insecure-Requests': '1'
            }
        };
        
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        this.$http
            .post<User>('/Account/LogOff', angular.element.param(data), config)
            .then(response => {
                console.log('Logged off');
                console.log(response);
                resolve(response);
                this.user = undefined;
                return this.user;
            })
            .catch(error => reject(error));

        return promise;

    }
    getUser(): PromiseLike<User> {
        const {resolve, reject, promise } = this.$q.defer<User>();
        const antiforgeryTokenValue = this.token;

        var config: ng.IRequestShortcutConfig = {
            xsrfHeaderName: this.antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [this.antiforgeryTokenName]: antiforgeryTokenValue,
                'Content-Type': 'application/x-www-form-urlencoded',
                'Upgrade-Insecure-Requests': '1'
            }
        };

        if (this.user) {
            resolve(this.user);
        } else {
            reject('not logged in');
        }
        return promise;
    }
    token: string;
    user: User;
    loggedIn = false;
    antiforgeryTokenName = '__RequestVerificationToken';

}

interface Credentials {
    email: string;
    password: string;
    antiforgeryToken: string;
}