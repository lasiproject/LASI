import './augmentations';
import './enhance-array';
import 'bootstrap-css';
import './styles/styles.scss';
import './styles/site.css';
import './styles/lexical.css';
import 'bootstrap';
import {Aurelia, autoinject, inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import TokenService from './services/token';
import {apiBaseUrl} from 'env';

export async function configure(aurelia: Aurelia) {
  aurelia.use
    .instance(Storage, window.sessionStorage)
    .standardConfiguration()
    .developmentLogging()
    .singleton(HttpClient, (tokenService: TokenService) => new HttpClient().configure(httpConfig => {
      return httpConfig
        .useStandardConfiguration()
        .withBaseUrl(apiBaseUrl)
        .withDefaults({credentials: 'include', mode: 'cors'})
        .withInterceptor({
          request(config) {
            if (tokenService.token) {
              config.headers.append('Authorization', `Bearer ${tokenService.token}`);
            }
            config.headers.append('Scheme', 'Bearer');
            config.headers.append('WWW-Authenticate', 'Bearer');
            return config;
          }
        })
    }), [TokenService]);

  await aurelia.start();
  aurelia.setRoot('');
}
