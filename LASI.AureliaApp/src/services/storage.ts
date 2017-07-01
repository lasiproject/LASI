import {autoinject} from 'aurelia-framework';

@autoinject export default class ClientStorage {

  constructor(readonly storage: Storage) {}

  store(key: StoreKey, value: {}) {
    this.storage[key] = JSON.stringify(value);
  }

  retreive(key: StoreKey): string {
    return JSON.parse(this.storage[key]);
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