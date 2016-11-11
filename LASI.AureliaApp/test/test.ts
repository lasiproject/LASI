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
  let items: { [key: string]: string | null } = {};
  const storage: typeof window.sessionStorage = {
    clear() {
      items = {};
    },
    getItem(key) {
      return items[key];
    },
    key(index) {
      return Object.values(items)[index];
    },
    get length() {
      return Object.keys(items).length;
    },
    removeItem(key) {
      delete items[key];
    },
    setItem(key, value) {
      items[key] = value;
    }
  };

  const storageService = new StorageService(storage);

  return new TokenService(storageService);
}