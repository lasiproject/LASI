import {autoinject} from 'aurelia-framework';
import {WindowService} from './helpers';

@autoinject
export default class TokenService {
    constructor(private window: WindowService) { }

    get token(): string {
        return this.window.sessionStorage[this.tokenKey];
    }

    set token(value) {
        this.window.sessionStorage[this.tokenKey] = value;
    }

    clearToken() {
        this.window.sessionStorage.removeItem(this.tokenKey);
    }

   readonly tokenKey = 'auth_token';
}