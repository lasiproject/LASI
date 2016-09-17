import test from 'tape';
import TokenService from 'src/app/services/token';
import StorageService from 'src/app/services/storage';

export default test(({equal, end}) => {
  const storageService = new StorageService(window.sessionStorage);
  const tokenService = new TokenService(storageService);



  end();
});