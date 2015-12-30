'use strict';

import { TokenService } from 'app/token-service';

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
            rememberMe,
            token: this.token,
            auth_token: this.token
        };
        this.loginViaGet(requestConfig)
            .catch(error => this.loginViaPost(data, requestConfig))
            .then(({ user, token }) => {
                this.user = user;
                this.loggedIn = true;
                this.tokenService.token = token;
                resolve(user);
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
            .post<AuthenticationResult>('/api/authenticate', angular.element.param(data), config)
            .then(response => response.data);
    }

    loginViaGet(requestConfig: ng.IRequestShortcutConfig) {
        return this.$http.get<AuthenticationResult>('/api/authenticate', requestConfig).then(response => response.data);
    }

    logoff(): ng.IPromise<any> {
        var { resolve, reject, promise } = this.$q.defer();

        var config: ng.IRequestShortcutConfig = {
            withCredentials: true,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            }
        };

        this.$http
            .post<User>('/api/authenticate/logoff', {}, config)
            .then(response => {
                console.log('Logged off');
                console.log(response);
                this.tokenService.token = undefined;
                this.user = undefined;
                resolve(response.data);
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
                [this.antiforgeryTokenName]: antiforgeryTokenValue,
                'Content-Type': 'application/x-www-form-urlencoded',
            }
        };

        if (this.user) {
            resolve(this.user);
        } else {
            reject('not logged in');
        }
        return promise;
    }
    get token() { return this.tokenService.token; }
    user: User;
    loggedIn = false;
    antiforgeryTokenName = '__RequestVerificationToken';

}