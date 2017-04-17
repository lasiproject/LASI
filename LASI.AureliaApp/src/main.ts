import 'app/enhance-array';
import 'font-awesome';
import 'bootstrap-css';
import 'app/styles/site.css';
import 'app/styles/lexical.css';
import 'bootstrap';
import 'core-js/client/core';
import { Aurelia } from 'aurelia-framework';
import { HttpClientConfiguration, HttpClient } from 'aurelia-fetch-client';
import { getHostElement } from './helpers';
import TokenService from './app/services/token';

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
        .rejectErrorResponses()
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

  const a = await aurelia.start();
  await a.setRoot('src/app/app', getHostElement());
}