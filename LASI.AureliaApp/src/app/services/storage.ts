import {autoinject} from 'aurelia-framework';

@autoinject export default class ClientStorage {

  constructor(readonly storage: Store) { }

  store(key: StoreKey, value) {
    this.storage[key] = value;
  }

  retreive(key: StoreKey) {
    return this.storage[key];
  }

  clear(key?: StoreKey) {
    if (key) {
      delete this.storage[key];
    } else {
      this.storage.clear();
    }
  }
}

type StoreKey = 'auth_token';

export type Store = typeof window.sessionStorage;