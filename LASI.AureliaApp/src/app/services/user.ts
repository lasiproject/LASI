import { autoinject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { HttpClient, RequestInit } from 'aurelia-fetch-client';
import $ from 'jquery';
import TokenService from './token';
import { getConfig, postConfig } from './http-utilities';
import { Credentials, User, AuthenticationResult } from 'src/models';

@autoinject export default class UserService {
    constructor(
        readonly router: Router,
        readonly http: HttpClient,
        readonly tokenService: TokenService
    ) { }

    async loginGet() {
        const response = await this.http.fetch('/api/authenticate', getConfig);
        this.user = await response.json();
        if (!this.user) {
            throw Error('unable to retrieve user');
        }
        return this.user;
    }

    async getUser(): Promise<AuthenticationResult> {
        if (this.user) {
            return this.user;
        } else if (this.tokenService.token) {
            return await this.loginGet();
        } else {
            return this.router.navigateToRoute('login');
        }
    }
    async loginPost(data: any) {
        const response = await this.http.fetch('/api/authenticate', postConfig.withBody($.param(data)));
        return await response.json() as AuthenticationResult;
    }
    async login(credentials: Credentials): Promise<AuthenticationResult> {
        const promise = this.loggedIn ? this.getUser() : Promise.resolve({});


        try {
            const user = await this.loginGet();
            return user || await this.loginPost(credentials);
        } catch (e) {
            console.warn('not logged in');
            return await this.loginPost(credentials).then(loginSuccess);
        }
    }

    async logoff() {
        const response = await this.http.fetch('/api/authenticate/logoff', getConfig);
        console.log(response);
        this.tokenService.clearToken();
        this.loggedIn = false;
        delete this.user;
        return await response.json();
    }

    async getDetails() {
        const response = await this.http.fetch('/api/manage/account', getConfig);
        return await response.json();
    }

    async saveDetails(details) {
        const response = await this.http.fetch('/api/manage/account', postConfig.withBody(details));
        return await response.json();
    }

    user: User;
    loggedIn = false;
}

function loginSuccess({ user, token }) {
    this.user = user;
    this.loggedIn = true;
    if (token) {
        this.tokenService.token = token;
    }
    return this.user;
};