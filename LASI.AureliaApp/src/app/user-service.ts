import { autoinject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { HttpClient, RequestInit } from 'aurelia-fetch-client';
import $ from 'jquery';
import TokenService from './token-service';
import { Credentials, User, AuthenticationResult } from 'src/models';

const getConfig: RequestInit = {
    method: 'GET',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
    }
};
const postConfig: RequestInit & { withBody: <B>(body: B) => RequestInit & { body?: B } } = {
    withBody<B>(this: RequestInit, body: B) {
        this.body = body;
        return this;
    },
    method: 'POST',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
    }
};
//@apiClient
@autoinject export class UserService {
    constructor(
        readonly router: Router,
        readonly http: HttpClient,
        readonly tokenService: TokenService
    ) { }

    async loginGet(): Promise<User> {
        const response = await this.http.fetch('/api/authenticate', getConfig);
        this.user = await response.json() as User;
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
    async login({ email, password, rememberMe }: Credentials): Promise<AuthenticationResult> {
        const promise = this.loggedIn ? this.getUser() : Promise.resolve({});

        const data = {
            email,
            password,
            rememberMe
        };
        try {
            const user = await this.loginGet();
            return user || await this.loginPost({ data });
        } catch (e) {
            console.warn('not logged in');
            return await this.loginPost(data).then(loginSuccess);
        }
    }

    async logoff(): Promise<any> {
        const response = await this.http.fetch('/api/authenticate/logoff', getConfig);
        console.log(response);
        this.tokenService.clearToken();
        this.loggedIn = false;
        delete this.user;
        return await response.json();
    }




    async getDetails(): Promise<any> {
        const response = await this.http.fetch('/api/manage/account', getConfig);
        return await response.json();
    }

    saveDetails(details: any): Promise<any> {
        return this.http.fetch('/api/manage/account', postConfig.withBody(details));
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