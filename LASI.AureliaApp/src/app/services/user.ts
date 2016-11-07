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
    const {json} = await this.http.fetch('/api/authenticate', getConfig);
    const user = await json() as User;
    if (!user) {
      throw Error('unable to retrieve user');
    }
    this.user = user;
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

    const loginSuccess = ({user, token}) => {
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
      const {user, token} = await this.loginPost(credentials);
      return await loginSuccess({ user, token });


    }
  }

  async logoff() {
    const {json} = await this.http.fetch('/api/authenticate/logoff', getConfig);
    this.tokenService.clearToken();
    this.loggedIn = false;
    this.user = undefined;
    return await json();
  }

  async getDetails() {
    const {json} = await this.http.fetch('/api/manage/account', getConfig);
    return await json();
  }

  async saveDetails(details) {
    const {json} = await this.http.fetch('/api/manage/account', postConfig.withBody(details));
    return await json();
  }

  user?: User;
  loggedIn = false;
}