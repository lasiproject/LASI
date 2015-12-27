'use strict';
import 'angular';
export class UserService {
    static $inject = ['$q', '$http', '$state'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService) { }

    loginUser({ email, password, antiforgeryToken, rememberMe }: Credentials): ng.IPromise<User> {
        var { reject, resolve, promise } = this.$q.defer<User>();
        var requestConfig: { [i: string]: any } = {
            xsrfHeaderName: this.antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [this.antiforgeryTokenName]: this.token,
                'Upgrade-Insecure-Requests': '1',
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        var data = {
            email,
            password,
            rememberMe,
            [this.antiforgeryTokenName]: this.token || antiforgeryToken
        };

        this.loginViaGet(requestConfig)

            .catch(error => this.loginViaPost(data, requestConfig)).then(user => {
                this.user = user;
                this.loggedIn = true;
                resolve(user);
                this.token = user.token;
                return this.user;
            }).catch(error=> {
                console.error(error);
                reject(error);
            });

        return promise;

    }
    loginViaPost(data: {}, config: ng.IRequestShortcutConfig) {


        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        return this.$http
            .post<User>('/Account/Login', angular.element.param(data), config)
            .then(response => response.data);
    }

    loginViaGet(requestConfig: ng.IRequestShortcutConfig) {
        return this.$http.get<User>('/Account/Login', requestConfig).then(response=> response.data);
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