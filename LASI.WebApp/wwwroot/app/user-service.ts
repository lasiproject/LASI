'use strict';
import { User } from './models';

export default class UserService {
    constructor(private $http: ng.IHttpService, private $q: ng.IQService) { }
    getUser(): PromiseLike<User> {
        if (this.user && this.user.loggedIn) {
            return this.$q.when(this.user);
        }
        var deferred = this.$q.defer<User>();
        this.$http
            .post<User>('/Account/Login', { email: this.user.email, password: this.user.password })
            .then(response => {
                this.user = response.data;
                deferred.resolve(this.user);
            })
            .catch(deferred.reject.bind(deferred));
        return deferred.promise;
    }
    user: User;
}