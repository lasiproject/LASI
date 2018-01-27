import { autoinject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { HttpClient, RequestInit } from 'aurelia-fetch-client';
import $ from 'jquery';
import TokenService from './token';
import { getConfig, postConfig } from './http-utilities';
import User from 'models/user';
import Credentials from 'models/credentials';
import AuthenticationResult from 'models/authentication-result';

@autoinject export default class UserService {
  constructor(
    readonly router: Router,
    readonly http: HttpClient,
    readonly tokenService: TokenService
  ) { }

  async loginGet() {
    const { json } = await this.http.fetch('/api/authenticate', getConfig);
    const user = await json() as User;
    if (!user) {
      throw Error('unable to retrieve user');
    }
    this.user = user;
    return this.user;
  }

  async getUser() {
    if (this.user) {
      return this.user;
    } else if (this.tokenService.token) {
      return await this.loginGet();
    } else {
      return this.router.navigateToRoute('login');
    }
  }

  async loginPost(data: Credentials) {
    const response = await this.http.fetch('/api/authenticate', postConfig.withBody($.param(data)));
    return await response.json() as AuthenticationResult;
  }
  async login(credentials: Credentials) {

    const loginSuccess = ({ user, token }: { user?: User, token?: string }) => {
      this.user = user;
      this.loggedIn = true;
      if (token) {
        this.tokenService.token = token;
      }
      return user;
    };

    const promise = this.loggedIn ? this.getUser() : Promise.resolve({});
    try {
      const user = await this.loginGet();
      return user || await this.loginPost(credentials);
    } catch (e) {
      console.warn('not logged in');
      const { user, token } = await this.loginPost(credentials);
      return await loginSuccess({ user, token });
    }
  }

  async logoff() {
    const response = await this.http.fetch('/api/authenticate/logoff', getConfig);
    this.tokenService.clearToken();
    this.loggedIn = false;
    this.user = undefined;
    return await response.json();
  }

  async getDetails() {
    const response = await this.http.fetch('/api/manage/account', getConfig);
    return await response.json();
  }

  async saveDetails(details: User) {
    const response = await this.http.fetch('/api/manage/account', postConfig.withBody(details));
    return await response.json();
  }

  user?: User;
  loggedIn = false;
}
