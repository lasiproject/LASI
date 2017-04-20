import { autoinject } from 'aurelia-framework';
import ClientStorage from './storage';

@autoinject export default class TokenService {

  constructor(readonly storage: ClientStorage) { }

  get token(): string {
    return this.storage.retreive('auth_token');
  }

  set token(value) {
    this.storage.store('auth_token', value);
  }

  clearToken() {
    this.storage.clear('auth_token');
  }
}
