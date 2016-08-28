import { autoinject } from 'aurelia-framework';
import WindowService from './storage';
const tokenKey = 'auth_token';

@autoinject export default class TokenService {
    constructor(readonly window: WindowService) { }

    get token(): string {
        return this.window.retreive(tokenKey);
    }

    set token(value) {
        this.window.store(tokenKey, value);
    }

    clearToken() {
        this.window.clear(tokenKey);
    }

}
