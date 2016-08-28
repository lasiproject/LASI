import { autoinject } from 'aurelia-framework';

@autoinject export default class ClientStorage {
    constructor(readonly storage: Store) { }

    store(key: string, value) {
        this.storage[key] = value;
    }

    retreive(key: string) {
        return this.storage[key];
    }

    clear(key?: string) {
        if (key) {
            this.storage[key] = undefined;
        } else {
            this.storage.clear();
        }
    }
}
export type Store = typeof window.sessionStorage;