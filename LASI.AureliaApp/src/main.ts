import {Aurelia, inject, Lazy} from 'aurelia-framework';
import {HttpClientConfiguration, HttpClient} from 'aurelia-fetch-client';
import {WindowService} from './app/helpers';
import TokenService from './app/token-service';
import getHostElement from './get-host-element';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging()
        .defaultBindingLanguage()
        .defaultResources()
        .singleton(WindowService)
        .plugin('aurelia-dialog', config => {
            config.useDefaults();
            config.settings.lock = false;
            config.settings.centerHorizontalOnly = false;
            config.settings.startingZIndex = 1005;
        })
        .plugin('aurelia-typeahead')
        .singleton(HttpClient, class extends HttpClient {
            static inject = [TokenService];
            constructor(private tokenService: TokenService) {
                super();
                super.configure(config => {
                    return config
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
            }
        });

    aurelia.start().then(() => aurelia.setRoot('src/app/app', getHostElement()));
}