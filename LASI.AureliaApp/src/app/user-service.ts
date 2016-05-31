import {autoinject} from 'aurelia-framework';
import {HttpClient, RequestInit} from 'aurelia-fetch-client';
import {Credentials, User, AuthenticationResult} from 'src/models';
import TokenService from './token-service';

const getConfig: RequestInit = {
    method: 'GET',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
    }
};
const postConfig: RequestInit = {
    method: 'POST',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded'
    }
};

@autoinject export class UserService {

    constructor(private http: HttpClient, private tokenService: TokenService) { }

    async login({ email, password, rememberMe }: Credentials): Promise<User> {
        const promise = this.loggedIn ? this.getUser() : Promise.resolve({});

        const data = {
            email,
            password,
            rememberMe
        };
        try {
            const { user } = await this.loginGet();
            return user;
        } catch (e) {

            console.warn('not logged in');
            return await this.loginPost(data).then(this.loginSuccess);

        }


    }
    async loginPost(data: {}) {
        const response = await this.http.fetch('/api/authenticate', postConfig);
        return await response.json() as AuthenticationResult;
    }

    async loginGet(): Promise<AuthenticationResult> {
        const response = await this.http.fetch('/api/authenticate', getConfig);
        return await response.json() as User;
    }

    async logoff(): Promise<any> {
        const response = await this.http.fetch('/api/authenticate/logoff', getConfig);
        console.log(response);
        this.tokenService.clearToken();
        this.loggedIn = false;
        this.user = undefined;
        return await response.json();
    }

    async getUser(): Promise<AuthenticationResult> {
        if (this.user) {
            return this.user;
        } else if (this.tokenService.token) {
            return await this.loginGet();
        } else {
            console.info('not logged in');
        }
    }

    async getDetails(): Promise<any> {
        const response = await this.http.fetch('/api/manage/account', getConfig);
        return await response.json();
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

    user: User;
    loggedIn = false;


} 