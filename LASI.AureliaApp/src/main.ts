import 'font-awesome';
import 'bootstrap-css!css';
import 'src/styles/site.css!css';
import 'src/styles/lexical.css!css';
import 'bootstrap';
import 'src/polyfills';

import { Aurelia, inject, InvocationHandler, invoker, Invoker, FactoryInvoker, Factory, Container } from 'aurelia-framework';
import { HttpClientConfiguration, HttpClient } from 'aurelia-fetch-client';
import { getHostElement } from './helpers';
import TokenService from './app/services/token';
import configureDialogs from './configuration/dialog';
import configureTypeahead from './configuration/typeahead';

export async function configure(aurelia: Aurelia) {
    aurelia.use
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