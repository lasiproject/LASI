import { autoinject } from 'aurelia-framework';
import ClientStorage from './storage';

@autoinject export default class TokenService {

  constructor(readonly window: ClientStorage) { }

  get token(): string {
    return this.window.retreive('auth_token');
  }

  set token(value) {
    this.window.store('auth_token', value);
  }

  clearToken() {
    this.window.clear('auth_token');
  }
}
