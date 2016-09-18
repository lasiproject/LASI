import 'core-js';
import 'reflect-metadata';
import test from 'tape';
import TokenService from 'src/app/services/token';
import StorageService from 'src/app/services/storage';

test('TokenService.token should initially be undefined', ({equal, end}) => {
  const service = createTokenService();
  equal(service.token, undefined);

  end();
});

test('TokenService.token.get should retreive value specified by TokenService.token.set', ({equal, end}) => {
  const service = createTokenService();
  const token = 'xyz';
  service.token = token;

  equal(service.token, token);

  end();
});


function createTokenService() {
  const storage: typeof window.sessionStorage = {
    items: {},
    clear() {
      this.items = {};
    },
    getItem(key) {
      return this.items[key];
    },
    key(index) {
      return Object.values(this.items)[index];
    },
    get length() {
      return Object.keys(this.items).length;
    },
    removeItem(key) {
      this.items[key] = undefined;
    },
    setItem(key, value) {
      this.items[key] = value;
    }
  };

  const storageService = new StorageService(storage);

  return new TokenService(storageService);
}