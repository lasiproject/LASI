'use strict';

import TokenService from 'app/token-service';

export class UserService {
    static $inject = ['$q', '$http', 'TokenService'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService, private tokenService: TokenService) { }

    loginUser({ email, password, rememberMe }: Credentials): ng.IPromise<User> {
        var { reject, resolve, promise } = this.$q.defer<User>();
        var requestConfig: { [i: string]: any } = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        var data = {
            email,
            password,
            rememberMe
        };
        this.loginViaGet(requestConfig)
            .catch(error => this.loginViaPost(data, requestConfig))
            .then(({ user, token }) => {
                return this.loginSuccess(resolve, user, token);
            }).catch(error=> {
                console.error(error);
                reject(error);
            });

        return promise;
    }
    loginViaPost(data: {}, config: ng.IRequestShortcutConfig) {


        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        return this.$http
            .post<AuthenticationResult>('/api/authenticate', angular.element.param(data), config)
            .then(response => response.data);
    }

    loginViaGet(requestConfig: ng.IRequestShortcutConfig) {
        if (this.tokenService.token) {
            return this.$http.get<AuthenticationResult>('/api/authenticate', requestConfig).then(response => response.data);
        } else {
            return this.$q.reject('not logged in');
        }
    }

    logoff(): ng.IPromise<any> {
        var { resolve, reject, promise } = this.$q.defer();

        var config: ng.IRequestShortcutConfig = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            }
        };

        this.$http
            .post<User>('/api/authenticate/logoff', {}, config)
            .then(response => {
                console.log('Logged off');
                console.log(response);
                this.tokenService.clearToken();
                this.user = undefined;
                resolve(response.data);
                return this.user;
            })
            .catch(error => reject(error));

        return promise;

    }
    getUser(): PromiseLike<User> {
        const {resolve, reject, promise } = this.$q.defer<User>();
        const antiforgeryTokenValue = this.tokenService.token;

        var config: ng.IRequestShortcutConfig = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            }
        };

        if (this.user && this.tokenService.token) {
            resolve(this.user);
        } else {
            this.loginViaGet(config)
                .then(({ user, token }) => {
                    return this.loginSuccess(resolve, user, token);
                }).catch(error=> {
                    reject(error);
                });
        }
        return promise;
    }
    loginSuccess = (resolve, user, token) => {
        this.user = user;
        this.loggedIn = true;
        if (token) {
            this.tokenService.token = token;
        }
        resolve(user);
        return this.user;
    }
    user: User;
    loggedIn = false;
}