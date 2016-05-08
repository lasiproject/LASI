import { BaseRequestOptions, BaseResponseOptions } from 'angular2/http';
import { Injectable } from 'app/ng2-utils';
import TokenService from 'app/token-service';

@Injectable export class RequestOptions extends BaseRequestOptions {
    constructor(tokenService: TokenService) {
        super();
        if (tokenService.token) {
            this.headers.append('Authorization', `Bearer ${tokenService.token}`);
        }
        this.headers.append('Scheme', 'Bearer');
        this.headers.append('WWW-Authenticate', 'Bearer');
        this.headers.append('Content-Type', 'application/x-www-form-urlencoded');
    }

    headers: typeof BaseRequestOptions.prototype.headers & { Authorization; Scheme };
    withCredentials = true;
}

@Injectable export class ResponseOptions extends BaseResponseOptions {
    constructor() {
        super();
    }
}