import {autoinject} from 'aurelia-framework';
import {HttpClient, RequestInit} from 'aurelia-fetch-client';

import TokenService from './token-service';

@autoinject
export class UserService {

    constructor(private http: HttpClient, private tokenService: TokenService) { }

    async login({ email, password, rememberMe }: models.Credentials): Promise<models.User> {
        const promise = this.loggedIn ? this.getUser() : Promise.resolve({});

        const data = {
            email,
            password,
            rememberMe
        };
        try {
            var user = await this.loginGet();
            return user.user;
        }
        catch (e) {

            console.warn('not logged in');
            return await this.loginPost(data).then(this.loginSuccess);

        }


    }
    async loginPost(data: {}) {


        // TODO: Remove angular.element.param(data) as it silently depends on jQuery
        var response = await this.http.fetch('/api/authenticate', { method: 'POST', body: $.param(data), headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
        return await response.json() as models.AuthenticationResult;
    }

    async loginGet(): Promise<models.AuthenticationResult> {

        var response = await this.http.fetch('/api/authenticate', { method: 'GET', headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
        return await response.json() as models.User;

    }

    async logoff(): Promise<any> {

        var response = await this.http.fetch('/api/authenticate/logoff', { method: 'GET', headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
        console.log(response);
        this.tokenService.clearToken();
        this.loggedIn = false;
        this.user = undefined;
        return await response.json();
    }

    async getUser(): Promise<models.AuthenticationResult> {
        if (this.user) {
            return this.user;
        } else if (this.tokenService.token) {
            return await this.loginGet();
        } else {
            console.info('not logged in');
        }
    }

    async getDetails(): Promise<any> {
        var response = await this.http.fetch('/api/manage/account', { method: 'GET', headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
        return await response.json()
    }

    saveDetails(details: any): Promise<any> {
        return this.http.fetch('api/manage/account', { body: details, method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
    }

    loginSuccess = ({ user, token }) => {
        this.user = user;
        this.loggedIn = true;
        if (token) {
            this.tokenService.token = token;
        }
        return this.user;
    };

    user: models.User;
    loggedIn = false;

    static requestConfig: RequestInit = {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    };
} 