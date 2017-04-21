import './enhance-array';
import 'bootstrap-css';
import './styles/styles.scss';
import './styles/site.css';
import './styles/lexical.css';
import 'bootstrap';
import 'core-js/client/core';
import { Aurelia } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import TokenService from './services/token';

export async function configure(aurelia: Aurelia) {
  aurelia.use
    .instance(Storage, window.sessionStorage)
    .standardConfiguration()
    .developmentLogging()
    .container.get(HttpClient).configure(httpConfig => {
      const tokenService = aurelia.container.get(TokenService);
      return httpConfig
        .useStandardConfiguration()
        .withBaseUrl('//localhost:51641')
        .withDefaults({ credentials: 'include', mode: 'cors' })
        .withInterceptor({

          request(config) {
            if (tokenService.token) {
              config.headers.append('Authorization', `Bearer ${tokenService.token}`);
            }
            config.headers.append('Scheme', 'Bearer');
            config.headers.append('WWW-Authenticate', 'Bearer');
            return config;
          }
        });
    });

  await aurelia.start();
  aurelia.setRoot('');
}