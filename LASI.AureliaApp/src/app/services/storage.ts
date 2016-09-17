import { autoinject } from 'aurelia-framework';

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
      this.storage[key] = undefined;
    } else {
      this.storage.clear();
    }
  }
}

type StoreKey = 'auth_token';

export type Store = typeof window.sessionStorage;