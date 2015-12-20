'use strict';
import 'angular';
export class UserService {
    static $inject = ['$q', '$http', '$state'];

    constructor(private $q: ng.IQService, private $http: ng.IHttpService) { }

    loginUser({ email, password, antiforgeryTokenName, antiforgeryTokenValue }: Credentials): ng.IPromise<User> {
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

    logoff(antiforgeryTokenName: string, antiforgeryTokenValue: string): ng.IPromise<any> {
        var { resolve, reject, promise } = this.$q.defer();
        var data = {
            [antiforgeryTokenName]: antiforgeryTokenValue
        };
        var config: ng.IRequestShortcutConfig = {
            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [antiforgeryTokenName]: antiforgeryTokenValue,
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
        const antiforgeryTokenName = '__RequestVerificationToken';
        const antiforgeryTokenValue = $(document).find($(`input[name="${antiforgeryTokenName}"`)).val();

        var config: ng.IRequestShortcutConfig = {
            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                accept: 'application/json',
                [antiforgeryTokenName]: antiforgeryTokenValue,
                'Content-Type': 'application/x-www-form-urlencoded',
                'Upgrade-Insecure-Requests': '1'
            }
        };

        if (this.user) {
            resolve(this.user);
        } else {
            this.$http.get<User>('Account/Login', config)
                .then(response => {
                    if (response.status !== 200) {
                        reject(response.statusText);
                    } else {
                        this.user = response.data;
                        resolve(this.user);
                    }
                }).catch(() => reject('not logged in'));
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