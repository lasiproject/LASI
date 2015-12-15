'use strict';
export class UserService {
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
                console.log('valid');
                this.loggedIn = true;
                this.user = response.data;
                deferred.resolve(response.data);

            })
            .catch(error => {
                deferred.reject(error);
                console.error(error);
            })
        //.finally(() => this.$state.current.name === 'app.home' ? this.$state.reload() : this.$state.go('app.home'));
        return deferred.promise;
    }

    logoff(antiforgeryTokenName, antiforgeryTokenValue): PromiseLike<void> {
        var { resolve, reject } = this.$q.defer<void>();
        var data = {};
        data[antiforgeryTokenName] = antiforgeryTokenValue;
        var config: ng.IRequestShortcutConfig = {

            xsrfHeaderName: antiforgeryTokenName,
            headers: {
                [antiforgeryTokenName]: antiforgeryTokenValue,
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        };
        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        return this.$http
            .post<User>('/Account/LogOff', angular.element.param(data), config)
            .then(response => {
                if (JSON.parse(response.data.toString())) {
                    console.log('valid');

                    console.log('Logged off');
                    console.log(response);
                    resolve();
                }
            })
            .catch(() => reject());

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
    private user: User;
    loggedIn = false;
}

interface Credentials {
    email: string;
    password: string;
    antiforgeryTokenName: string;
    antiforgeryTokenValue: string;
}