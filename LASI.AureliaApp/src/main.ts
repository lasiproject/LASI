import 'app/polyfills';
import 'jspm_packages/npm/font-awesome@4.7.0/css/font-awesome.min.css';
import 'bootstrap-css';
import 'src/styles/site.css';
import 'src/styles/lexical.css';
import 'bootstrap';
import 'core-js/client/core';
import { Aurelia } from 'aurelia-framework';
import { HttpClientConfiguration, HttpClient } from 'aurelia-fetch-client';
import { getHostElement } from './helpers';
import TokenService from './app/services/token';
import configureDialogs from './configuration/dialog';
import configureTypeahead from './configuration/typeahead';

export async function configure(aurelia: Aurelia) {
  aurelia.use
    .instance(Storage, window.sessionStorage)
    .standardConfiguration()
    .developmentLogging()
    .container.get(HttpClient).configure((httpConfig: HttpClientConfiguration) => {
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