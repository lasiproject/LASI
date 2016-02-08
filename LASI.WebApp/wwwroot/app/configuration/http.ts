'use strict';
import TokenService from 'app/token-service';

configureHttp.$inject = ['$httpProvider'];
export default function configureHttp($httpProvider: ng.IHttpProvider & { defaults: { headers } }) {
    $httpProvider.defaults.withCredentials = true;
    $httpProvider.defaults.headers.accept = 'application/json';
    $httpProvider.defaults.headers['Upgrade-Insecure-Requests'] = '1';

    createInterceptor.$inject = ['$rootScope', 'TokenService'];
    function createInterceptor($rootScope: ng.IRootScopeService, tokenService: TokenService): ng.IHttpInterceptor {
        return {
            request: (config: ng.IRequestConfig & { headers: { Authorization; Scheme; }; }) => {
                config.withCredentials = true;
                if (tokenService.token) {
                    config.headers.Authorization = `Bearer ${tokenService.token}`;

                }
                config.headers.Scheme = 'Bearer';
                config.headers['WWW-Authenticate'] = 'Bearer';
                return config;
            },
            responseError: reason => {
                switch (reason.status) {
                    case 401:
                        console.info('unauthorized', reason);
                        $rootScope.$broadcast('anauthorized');
                        break;
                    case 404:
                        console.info('not found');
                        break;
                    default:
                        return;
                }
            }
        };
    }
    $httpProvider.interceptors.push(createInterceptor);
}