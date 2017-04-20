import 'core-js';
import 'reflect-metadata';
import test from 'tape';
import TokenService from 'app/services/token';
import StorageService from 'app/services/storage';

test('TokenService.token should initially be undefined', ({ equal, end }) => {
  const service = createTokenService();
  equal(service.token, undefined);

  end();
});

test('TokenService.token.get should retrieve value specified by TokenService.token.set', ({ equal, end }) => {
  const service = createTokenService();
  const token = 'xyz';
  service.token = token;

  equal(service.token, token);

  end();
});

test('Failure must fail', ({ throws, end }) => {
  throws(() => { throw 'a real failure'; });

  end();
});

function createTokenService() {
  let items: { [key: string]: string | null } = {};
  const storage: Storage = {
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

  const mockStorageService = new class extends StorageService {
    constructor(storage) {
      super(storage);
    }
    store(key, value) {
      super.store(key, value + '123');
    }
    retreive(value) {
      return super.retreive('auth_token');
    }
  }(storage);

  const storageService = new StorageService(storage);

  return new TokenService(storageService);
}