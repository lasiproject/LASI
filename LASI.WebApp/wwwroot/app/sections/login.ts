'use strict';
import { User } from 'app/models';

export default class LoginController {
    static $inject = ['$http', '$q', 'data'];

    constructor(private $http: ng.IHttpService, private $q: ng.IQService, data) { }

    login(): ng.IPromise<User> {
        var deferred = this.$q.defer<User>();
        this.$http
            .post<User>('/Account/Login', { email: this.username, password: this.password })
            .then(response => {
                this.user = response.data;
            })
            .catch(console.error.bind(console));
        return deferred.promise;
    }
    username: string;
    password: string;
    user: User;
}