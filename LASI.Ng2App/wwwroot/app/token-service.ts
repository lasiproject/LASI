import { Injectable } from 'angular2/core';
@Injectable()
export class TokenService {
    $window = window;
    constructor() { }

    get token(): string {
        return this.$window.sessionStorage[this.tokenKey];
    }

    set token(value) {
        this.$window.sessionStorage[this.tokenKey] = value;
    }

    clearToken() {
        this.$window.sessionStorage.removeItem(this.tokenKey);
    }

    tokenKey = 'auth_token';
}